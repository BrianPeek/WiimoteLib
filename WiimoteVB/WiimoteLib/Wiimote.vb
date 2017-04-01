'////////////////////////////////////////////////////////////////////////////////
'	Wiimote.cs
'	Managed Wiimote Library
'	Written by Brian Peek (http://www.brianpeek.com/)
'	for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'  and http://www.codeplex.com/WiimoteLib
'	for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.Serialization
Imports Microsoft.Win32.SafeHandles
Imports System.Threading

Namespace WiimoteLib
	''' <summary>
	''' Implementation of Wiimote
	''' </summary>
	Public Class Wiimote
		Implements IDisposable
		''' <summary>
		''' Event raised when Wiimote state is changed
		''' </summary>
		Public Event WiimoteChanged As EventHandler(Of WiimoteChangedEventArgs)

		''' <summary>
		''' Event raised when an extension is inserted or removed
		''' </summary>
		Public Event WiimoteExtensionChanged As EventHandler(Of WiimoteExtensionChangedEventArgs)

		' VID = Nintendo, PID = Wiimote
		Private Const VID As Integer = &H057e
		Private Const PID As Integer = &H0306

		' sure, we could find this out the hard way using HID, but trust me, it's 22
		Private Const REPORT_LENGTH As Integer = 22

		' Wiimote output commands
		Private Enum OutputReport As Byte
			LEDs = &H11
			Type = &H12
			IR = &H13
			Status = &H15
			WriteMemory = &H16
			ReadMemory = &H17
			IR2 = &H1a
		End Enum

		' Wiimote registers
		Private Const REGISTER_IR As Integer = &H04b00030
		Private Const REGISTER_IR_SENSITIVITY_1 As Integer = &H04b00000
		Private Const REGISTER_IR_SENSITIVITY_2 As Integer = &H04b0001a
		Private Const REGISTER_IR_MODE As Integer = &H04b00033

		Private Const REGISTER_EXTENSION_INIT_1 As Integer = &H04a400f0
		Private Const REGISTER_EXTENSION_INIT_2 As Integer = &H04a400fb
		Private Const REGISTER_EXTENSION_TYPE As Integer = &H04a400fa
		Private Const REGISTER_EXTENSION_CALIBRATION As Integer = &H04a40020

		' length between board sensors
		Private Const BSL As Integer = 43

		' width between board sensors
		Private Const BSW As Integer = 24

		' read/write handle to the device
		Private mHandle As SafeFileHandle

		' a pretty .NET stream to read/write from/to
		Private mStream As FileStream

		' report buffer
		Private ReadOnly mBuff(REPORT_LENGTH - 1) As Byte

		' read data buffer
		Private mReadBuff() As Byte

		' address to read from
		Private mAddress As Integer

		' size of requested read
		Private mSize As Short

		' current state of controller
		Private ReadOnly mWiimoteState As New WiimoteState()

		' event for read data processing
		Private ReadOnly mReadDone As New AutoResetEvent(False)
		Private ReadOnly mWriteDone As New AutoResetEvent(False)

		' event for status report
		Private ReadOnly mStatusDone As New AutoResetEvent(False)

		' use a different method to write reports
		Private mAltWriteMethod As Boolean

		' HID device path of this Wiimote
		Private mDevicePath As String = String.Empty

		' unique ID
		Private ReadOnly mID As Guid = Guid.NewGuid()

		' delegate used for enumerating found Wiimotes
		Friend Delegate Function WiimoteFoundDelegate(ByVal devicePath As String) As Boolean

		' kilograms to pounds
		Private Const KG2LB As Single = 2.20462262f

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
		End Sub

		Friend Sub New(ByVal devicePath As String)
			mDevicePath = devicePath
		End Sub

		''' <summary>
		''' Connect to the first-found Wiimote
		''' </summary>
		''' <exception cref="WiimoteNotFoundException">Wiimote not found in HID device list</exception>
		Public Sub Connect()
			If String.IsNullOrEmpty(mDevicePath) Then
				FindWiimote(AddressOf WiimoteFound)
			Else
				OpenWiimoteDeviceHandle(mDevicePath)
			End If
		End Sub

		Friend Shared Sub FindWiimote(ByVal wiimoteFound As WiimoteFoundDelegate)
			Dim index As Integer = 0
			Dim found As Boolean = False
			Dim guid As Guid
			Dim mHandle As SafeFileHandle

			' get the GUID of the HID class
			HIDImports.HidD_GetHidGuid(guid)

			' get a handle to all devices that are part of the HID class
			' Fun fact:  DIGCF_PRESENT worked on my machine just fine.  I reinstalled Vista, and now it no longer finds the Wiimote with that parameter enabled...
			Dim hDevInfo As IntPtr = HIDImports.SetupDiGetClassDevs(guid, Nothing, IntPtr.Zero, HIDImports.DIGCF_DEVICEINTERFACE) ' | HIDImports.DIGCF_PRESENT);

			' create a new interface data struct and initialize its size
			Dim diData As New HIDImports.SP_DEVICE_INTERFACE_DATA()
			diData.cbSize = Marshal.SizeOf(diData)

			' get a device interface to a single device (enumerate all devices)
			Do While HIDImports.SetupDiEnumDeviceInterfaces(hDevInfo, IntPtr.Zero, guid, index, diData)
				Dim size As UInt32

				' get the buffer size for this device detail instance (returned in the size parameter)
				HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, diData, IntPtr.Zero, 0, size, IntPtr.Zero)

				' create a detail struct and set its size
				Dim diDetail As New HIDImports.SP_DEVICE_INTERFACE_DETAIL_DATA()

				' yeah, yeah...well, see, on Win x86, cbSize must be 5 for some reason.  On x64, apparently 8 is what it wants.
				' someday I should figure this out.  Thanks to Paul Miller on this...
				If IntPtr.Size = 8 Then
					diDetail.cbSize = CUInt(8)
				Else
					diDetail.cbSize = CUInt(5)
				End If

				' actually get the detail struct
				If HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, diData, diDetail, size, size, IntPtr.Zero) Then
					Debug.WriteLine(String.Format("{0}: {1} - {2}", index, diDetail.DevicePath, Marshal.GetLastWin32Error()))

					' open a read/write handle to our device using the DevicePath returned
					mHandle = HIDImports.CreateFile(diDetail.DevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero)

					' create an attributes struct and initialize the size
					Dim attrib As New HIDImports.HIDD_ATTRIBUTES()
					attrib.Size = Marshal.SizeOf(attrib)

					' get the attributes of the current device
					If HIDImports.HidD_GetAttributes(mHandle.DangerousGetHandle(), attrib) Then
						' if the vendor and product IDs match up
						If attrib.VendorID = VID AndAlso attrib.ProductID = PID Then
							' it's a Wiimote
							Debug.WriteLine("Found one!")
							found = True

							' fire the callback function...if the callee doesn't care about more Wiimotes, break out
							If (Not wiimoteFound(diDetail.DevicePath)) Then
								Exit Do
							End If
						End If
					End If
					mHandle.Close()
				Else
					' failed to get the detail struct
					Throw New WiimoteException("SetupDiGetDeviceInterfaceDetail failed on index " & index)
				End If

				' move to the next device
				index += 1
			Loop

			' clean up our list
			HIDImports.SetupDiDestroyDeviceInfoList(hDevInfo)

			' if we didn't find a Wiimote, throw an exception
			If (Not found) Then
				Throw New WiimoteNotFoundException("No Wiimotes found in HID device list.")
			End If
		End Sub

		Private Function WiimoteFound(ByVal devicePath As String) As Boolean
			mDevicePath = devicePath

			' if we didn't find a Wiimote, throw an exception
			OpenWiimoteDeviceHandle(mDevicePath)

			Return False
		End Function

		Private Sub OpenWiimoteDeviceHandle(ByVal devicePath As String)
			' open a read/write handle to our device using the DevicePath returned
			mHandle = HIDImports.CreateFile(devicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero)

			' create an attributes struct and initialize the size
			Dim attrib As New HIDImports.HIDD_ATTRIBUTES()
			attrib.Size = Marshal.SizeOf(attrib)

			' get the attributes of the current device
			If HIDImports.HidD_GetAttributes(mHandle.DangerousGetHandle(), attrib) Then
				' if the vendor and product IDs match up
				If attrib.VendorID = VID AndAlso attrib.ProductID = PID Then
					' create a nice .NET FileStream wrapping the handle above
					mStream = New FileStream(mHandle, FileAccess.ReadWrite, REPORT_LENGTH, True)

					' start an async read operation on it
					BeginAsyncRead()

					' read the calibration info from the controller
					Try
						ReadWiimoteCalibration()
					Catch
						' if we fail above, try the alternate HID writes
						mAltWriteMethod = True
						ReadWiimoteCalibration()
					End Try

					' force a status check to get the state of any extensions plugged in at startup
					GetStatus()
				Else
					' otherwise this isn't the controller, so close up the file handle
					mHandle.Close()
					Throw New WiimoteException("Attempted to open a non-Wiimote device.")
				End If
			End If
		End Sub

		''' <summary>
		''' Disconnect from the controller and stop reading data from it
		''' </summary>
		Public Sub Disconnect()
			' close up the stream and handle
			If mStream IsNot Nothing Then
				mStream.Close()
			End If

			If mHandle IsNot Nothing Then
				mHandle.Close()
			End If
		End Sub

		''' <summary>
		''' Start reading asynchronously from the controller
		''' </summary>
		Private Sub BeginAsyncRead()
			' if the stream is valid and ready
			If mStream IsNot Nothing AndAlso mStream.CanRead Then
				' setup the read and the callback
				Dim buff(REPORT_LENGTH - 1) As Byte
				mStream.BeginRead(buff, 0, REPORT_LENGTH, New AsyncCallback(AddressOf OnReadData), buff)
			End If
		End Sub

		''' <summary>
		''' Callback when data is ready to be processed
		''' </summary>
		''' <param name="ar">State information for the callback</param>
		Private Sub OnReadData(ByVal ar As IAsyncResult)
			' grab the byte buffer
			Dim buff() As Byte = CType(ar.AsyncState, Byte())

			Try
				' end the current read
				mStream.EndRead(ar)

				' parse it
				If ParseInputReport(buff) Then
					' post an event
					RaiseEvent WiimoteChanged(Me, New WiimoteChangedEventArgs(mWiimoteState))
				End If

				' start reading again
				BeginAsyncRead()
			Catch e1 As OperationCanceledException
				Debug.WriteLine("OperationCanceledException")
			End Try
		End Sub

		''' <summary>
		''' Parse a report sent by the Wiimote
		''' </summary>
		''' <param name="buff">Data buffer to parse</param>
		''' <returns>Returns a boolean noting whether an event needs to be posted</returns>
		Private Function ParseInputReport(ByVal buff() As Byte) As Boolean
			Dim type As InputReport = CType(buff(0), InputReport)

			Select Case type
				Case InputReport.Buttons
					ParseButtons(buff)
				Case InputReport.ButtonsAccel
					ParseButtons(buff)
					ParseAccel(buff)
				Case InputReport.IRAccel
					ParseButtons(buff)
					ParseAccel(buff)
					ParseIR(buff)
				Case InputReport.ButtonsExtension
					ParseButtons(buff)
					ParseExtension(buff, 3)
				Case InputReport.ExtensionAccel
					ParseButtons(buff)
					ParseAccel(buff)
					ParseExtension(buff, 6)
				Case InputReport.IRExtensionAccel
					ParseButtons(buff)
					ParseAccel(buff)
					ParseIR(buff)
					ParseExtension(buff, 16)
				Case InputReport.Status
					ParseButtons(buff)
					mWiimoteState.BatteryRaw = buff(6)
					mWiimoteState.Battery = (((100.0f * 48.0f * CSng(CInt(Fix(buff(6))) / 48.0f))) / 192.0f)

					' get the real LED values in case the values from SetLEDs() somehow becomes out of sync, which really shouldn't be possible
					mWiimoteState.LEDState.LED1 = (buff(3) And &H10) <> 0
					mWiimoteState.LEDState.LED2 = (buff(3) And &H20) <> 0
					mWiimoteState.LEDState.LED3 = (buff(3) And &H40) <> 0
					mWiimoteState.LEDState.LED4 = (buff(3) And &H80) <> 0

					' extension connected?
					Dim extension As Boolean = (buff(3) And &H02) <> 0
					Debug.WriteLine("Extension: " & extension)

					If mWiimoteState.Extension <> extension Then
						mWiimoteState.Extension = extension

						If extension Then
							BeginAsyncRead()
							InitializeExtension()
						Else
							mWiimoteState.ExtensionType = ExtensionType.None
						End If

						' only fire the extension changed event if we have a real extension (i.e. not a balance board)
						If WiimoteExtensionChangedEvent IsNot Nothing AndAlso mWiimoteState.ExtensionType <> ExtensionType.BalanceBoard Then
							RaiseEvent WiimoteExtensionChanged(Me, New WiimoteExtensionChangedEventArgs(mWiimoteState.ExtensionType, mWiimoteState.Extension))
						End If
					End If
					mStatusDone.Set()
				Case InputReport.ReadData
					ParseButtons(buff)
					ParseReadData(buff)
				Case InputReport.OutputReportAck
					Debug.WriteLine("ack: " & buff(0) & " " & buff(1) & " " & buff(2) & " " & buff(3) & " " & buff(4))
					mWriteDone.Set()
				Case Else
					Debug.WriteLine("Unknown report type: " & type.ToString("x"))
					Return False
			End Select

			Return True
		End Function

		''' <summary>
		''' Handles setting up an extension when plugged in
		''' </summary>
		Private Sub InitializeExtension()
			WriteData(REGISTER_EXTENSION_INIT_1, &H55)
			WriteData(REGISTER_EXTENSION_INIT_2, &H00)

			' start reading again
			BeginAsyncRead()

			Dim buff() As Byte = ReadData(REGISTER_EXTENSION_TYPE, 6)
			Dim type As Long = (CLng(Fix(buff(0))) << 40) Or (CLng(Fix(buff(1))) << 32) Or (CLng(Fix(buff(2)))) << 24 Or (CLng(Fix(buff(3)))) << 16 Or (CLng(Fix(buff(4)))) << 8 Or buff(5)

			Select Case CType(type, ExtensionType)
				Case ExtensionType.None, ExtensionType.ParitallyInserted
					mWiimoteState.Extension = False
					mWiimoteState.ExtensionType = ExtensionType.None
					Return
				Case ExtensionType.Nunchuk, ExtensionType.ClassicController, ExtensionType.Guitar, ExtensionType.BalanceBoard, ExtensionType.Drums
					mWiimoteState.ExtensionType = CType(type, ExtensionType)
					Me.SetReportType(InputReport.ButtonsExtension, True)
				Case Else
					Throw New WiimoteException("Unknown extension controller found: " & type.ToString("x"))
			End Select

			Select Case mWiimoteState.ExtensionType
				Case ExtensionType.Nunchuk
					buff = ReadData(REGISTER_EXTENSION_CALIBRATION, 16)

					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.X0 = buff(0)
					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Y0 = buff(1)
					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Z0 = buff(2)
					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.XG = buff(4)
					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.YG = buff(5)
					mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.ZG = buff(6)
					mWiimoteState.NunchukState.CalibrationInfo.MaxX = buff(8)
					mWiimoteState.NunchukState.CalibrationInfo.MinX = buff(9)
					mWiimoteState.NunchukState.CalibrationInfo.MidX = buff(10)
					mWiimoteState.NunchukState.CalibrationInfo.MaxY = buff(11)
					mWiimoteState.NunchukState.CalibrationInfo.MinY = buff(12)
					mWiimoteState.NunchukState.CalibrationInfo.MidY = buff(13)
				Case ExtensionType.ClassicController
					buff = ReadData(REGISTER_EXTENSION_CALIBRATION, 16)

					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXL = CByte(buff(0) >> 2)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinXL = CByte(buff(1) >> 2)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MidXL = CByte(buff(2) >> 2)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYL = CByte(buff(3) >> 2)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinYL = CByte(buff(4) >> 2)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MidYL = CByte(buff(5) >> 2)

					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXR = CByte(buff(6) >> 3)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinXR = CByte(buff(7) >> 3)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MidXR = CByte(buff(8) >> 3)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYR = CByte(buff(9) >> 3)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinYR = CByte(buff(10) >> 3)
					mWiimoteState.ClassicControllerState.CalibrationInfo.MidYR = CByte(buff(11) >> 3)

					' this doesn't seem right...
'					mWiimoteState.ClassicControllerState.AccelCalibrationInfo.MinTriggerL = (byte)(buff[12] >> 3);
'					mWiimoteState.ClassicControllerState.AccelCalibrationInfo.MaxTriggerL = (byte)(buff[14] >> 3);
'					mWiimoteState.ClassicControllerState.AccelCalibrationInfo.MinTriggerR = (byte)(buff[13] >> 3);
'					mWiimoteState.ClassicControllerState.AccelCalibrationInfo.MaxTriggerR = (byte)(buff[15] >> 3);
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinTriggerL = 0
					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerL = 31
					mWiimoteState.ClassicControllerState.CalibrationInfo.MinTriggerR = 0
					mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerR = 31
				Case ExtensionType.Guitar, ExtensionType.Drums
					' there appears to be no calibration data returned by the guitar controller
				Case ExtensionType.BalanceBoard
					buff = ReadData(REGISTER_EXTENSION_CALIBRATION, 32)

					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopRight = CShort(Fix(CShort(Fix(buff(4))) << 8 Or buff(5)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomRight = CShort(Fix(CShort(Fix(buff(6))) << 8 Or buff(7)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopLeft = CShort(Fix(CShort(Fix(buff(8))) << 8 Or buff(9)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomLeft = CShort(Fix(CShort(Fix(buff(10))) << 8 Or buff(11)))

					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.TopRight = CShort(Fix(CShort(Fix(buff(12))) << 8 Or buff(13)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.BottomRight = CShort(Fix(CShort(Fix(buff(14))) << 8 Or buff(15)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.TopLeft = CShort(Fix(CShort(Fix(buff(16))) << 8 Or buff(17)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.BottomLeft = CShort(Fix(CShort(Fix(buff(18))) << 8 Or buff(19)))

					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.TopRight = CShort(Fix(CShort(Fix(buff(20))) << 8 Or buff(21)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.BottomRight = CShort(Fix(CShort(Fix(buff(22))) << 8 Or buff(23)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.TopLeft = CShort(Fix(CShort(Fix(buff(24))) << 8 Or buff(25)))
					mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.BottomLeft = CShort(Fix(CShort(Fix(buff(26))) << 8 Or buff(27)))
			End Select
		End Sub

		''' <summary>
		''' Decrypts data sent from the extension to the Wiimote
		''' </summary>
		''' <param name="buff">Data buffer</param>
		''' <returns>Byte array containing decoded data</returns>
		Private Function DecryptBuffer(ByVal buff() As Byte) As Byte()
			For i As Integer = 0 To buff.Length - 1
				buff(i) = CByte(((buff(i) Xor &H17) + &H17) And &Hff)
			Next i

			Return buff
		End Function

		''' <summary>
		''' Parses a standard button report into the ButtonState struct
		''' </summary>
		''' <param name="buff">Data buffer</param>
		Private Sub ParseButtons(ByVal buff() As Byte)
			mWiimoteState.ButtonState.A = (buff(2) And &H08) <> 0
			mWiimoteState.ButtonState.B = (buff(2) And &H04) <> 0
			mWiimoteState.ButtonState.Minus = (buff(2) And &H10) <> 0
			mWiimoteState.ButtonState.Home = (buff(2) And &H80) <> 0
			mWiimoteState.ButtonState.Plus = (buff(1) And &H10) <> 0
			mWiimoteState.ButtonState.One = (buff(2) And &H02) <> 0
			mWiimoteState.ButtonState.Two = (buff(2) And &H01) <> 0
			mWiimoteState.ButtonState.Up = (buff(1) And &H08) <> 0
			mWiimoteState.ButtonState.Down = (buff(1) And &H04) <> 0
			mWiimoteState.ButtonState.Left = (buff(1) And &H01) <> 0
			mWiimoteState.ButtonState.Right = (buff(1) And &H02) <> 0
		End Sub

		''' <summary>
		''' Parse accelerometer data
		''' </summary>
		''' <param name="buff">Data buffer</param>
		Private Sub ParseAccel(ByVal buff() As Byte)
			mWiimoteState.AccelState.RawValues.X = buff(3)
			mWiimoteState.AccelState.RawValues.Y = buff(4)
			mWiimoteState.AccelState.RawValues.Z = buff(5)

			mWiimoteState.AccelState.Values.X = CSng(CSng(mWiimoteState.AccelState.RawValues.X) - (CInt(Fix(mWiimoteState.AccelCalibrationInfo.X0)))) / (CSng(mWiimoteState.AccelCalibrationInfo.XG) - (CInt(Fix(mWiimoteState.AccelCalibrationInfo.X0))))
			mWiimoteState.AccelState.Values.Y = CSng(CSng(mWiimoteState.AccelState.RawValues.Y) - mWiimoteState.AccelCalibrationInfo.Y0) / (CSng(mWiimoteState.AccelCalibrationInfo.YG) - mWiimoteState.AccelCalibrationInfo.Y0)
			mWiimoteState.AccelState.Values.Z = CSng(CSng(mWiimoteState.AccelState.RawValues.Z) - mWiimoteState.AccelCalibrationInfo.Z0) / (CSng(mWiimoteState.AccelCalibrationInfo.ZG) - mWiimoteState.AccelCalibrationInfo.Z0)
		End Sub

		''' <summary>
		''' Parse IR data from report
		''' </summary>
		''' <param name="buff">Data buffer</param>
		Private Sub ParseIR(ByVal buff() As Byte)
			mWiimoteState.IRState.IRSensors(0).RawPosition.X = buff(6) Or ((buff(8) >> 4) And &H03) << 8
			mWiimoteState.IRState.IRSensors(0).RawPosition.Y = buff(7) Or ((buff(8) >> 6) And &H03) << 8

			Select Case mWiimoteState.IRState.Mode
				Case IRMode.Basic
					mWiimoteState.IRState.IRSensors(1).RawPosition.X = buff(9) Or ((buff(8) >> 0) And &H03) << 8
					mWiimoteState.IRState.IRSensors(1).RawPosition.Y = buff(10) Or ((buff(8) >> 2) And &H03) << 8

					mWiimoteState.IRState.IRSensors(2).RawPosition.X = buff(11) Or ((buff(13) >> 4) And &H03) << 8
					mWiimoteState.IRState.IRSensors(2).RawPosition.Y = buff(12) Or ((buff(13) >> 6) And &H03) << 8

					mWiimoteState.IRState.IRSensors(3).RawPosition.X = buff(14) Or ((buff(13) >> 0) And &H03) << 8
					mWiimoteState.IRState.IRSensors(3).RawPosition.Y = buff(15) Or ((buff(13) >> 2) And &H03) << 8

					mWiimoteState.IRState.IRSensors(0).Size = &H00
					mWiimoteState.IRState.IRSensors(1).Size = &H00
					mWiimoteState.IRState.IRSensors(2).Size = &H00
					mWiimoteState.IRState.IRSensors(3).Size = &H00

					mWiimoteState.IRState.IRSensors(0).Found = Not(buff(6) = &Hff AndAlso buff(7) = &Hff)
					mWiimoteState.IRState.IRSensors(1).Found = Not(buff(9) = &Hff AndAlso buff(10) = &Hff)
					mWiimoteState.IRState.IRSensors(2).Found = Not(buff(11) = &Hff AndAlso buff(12) = &Hff)
					mWiimoteState.IRState.IRSensors(3).Found = Not(buff(14) = &Hff AndAlso buff(15) = &Hff)
				Case IRMode.Extended
					mWiimoteState.IRState.IRSensors(1).RawPosition.X = buff(9) Or ((buff(11) >> 4) And &H03) << 8
					mWiimoteState.IRState.IRSensors(1).RawPosition.Y = buff(10) Or ((buff(11) >> 6) And &H03) << 8
					mWiimoteState.IRState.IRSensors(2).RawPosition.X = buff(12) Or ((buff(14) >> 4) And &H03) << 8
					mWiimoteState.IRState.IRSensors(2).RawPosition.Y = buff(13) Or ((buff(14) >> 6) And &H03) << 8
					mWiimoteState.IRState.IRSensors(3).RawPosition.X = buff(15) Or ((buff(17) >> 4) And &H03) << 8
					mWiimoteState.IRState.IRSensors(3).RawPosition.Y = buff(16) Or ((buff(17) >> 6) And &H03) << 8

					mWiimoteState.IRState.IRSensors(0).Size = buff(8) And &H0f
					mWiimoteState.IRState.IRSensors(1).Size = buff(11) And &H0f
					mWiimoteState.IRState.IRSensors(2).Size = buff(14) And &H0f
					mWiimoteState.IRState.IRSensors(3).Size = buff(17) And &H0f

					mWiimoteState.IRState.IRSensors(0).Found = Not(buff(6) = &Hff AndAlso buff(7) = &Hff AndAlso buff(8) = &Hff)
					mWiimoteState.IRState.IRSensors(1).Found = Not(buff(9) = &Hff AndAlso buff(10) = &Hff AndAlso buff(11) = &Hff)
					mWiimoteState.IRState.IRSensors(2).Found = Not(buff(12) = &Hff AndAlso buff(13) = &Hff AndAlso buff(14) = &Hff)
					mWiimoteState.IRState.IRSensors(3).Found = Not(buff(15) = &Hff AndAlso buff(16) = &Hff AndAlso buff(17) = &Hff)
			End Select

			mWiimoteState.IRState.IRSensors(0).Position.X = CSng(mWiimoteState.IRState.IRSensors(0).RawPosition.X / 1023.5f)
			mWiimoteState.IRState.IRSensors(1).Position.X = CSng(mWiimoteState.IRState.IRSensors(1).RawPosition.X / 1023.5f)
			mWiimoteState.IRState.IRSensors(2).Position.X = CSng(mWiimoteState.IRState.IRSensors(2).RawPosition.X / 1023.5f)
			mWiimoteState.IRState.IRSensors(3).Position.X = CSng(mWiimoteState.IRState.IRSensors(3).RawPosition.X / 1023.5f)

			mWiimoteState.IRState.IRSensors(0).Position.Y = CSng(mWiimoteState.IRState.IRSensors(0).RawPosition.Y / 767.5f)
			mWiimoteState.IRState.IRSensors(1).Position.Y = CSng(mWiimoteState.IRState.IRSensors(1).RawPosition.Y / 767.5f)
			mWiimoteState.IRState.IRSensors(2).Position.Y = CSng(mWiimoteState.IRState.IRSensors(2).RawPosition.Y / 767.5f)
			mWiimoteState.IRState.IRSensors(3).Position.Y = CSng(mWiimoteState.IRState.IRSensors(3).RawPosition.Y / 767.5f)

			If mWiimoteState.IRState.IRSensors(0).Found AndAlso mWiimoteState.IRState.IRSensors(1).Found Then
				mWiimoteState.IRState.RawMidpoint.X = (mWiimoteState.IRState.IRSensors(1).RawPosition.X + mWiimoteState.IRState.IRSensors(0).RawPosition.X) / 2
				mWiimoteState.IRState.RawMidpoint.Y = (mWiimoteState.IRState.IRSensors(1).RawPosition.Y + mWiimoteState.IRState.IRSensors(0).RawPosition.Y) / 2

				mWiimoteState.IRState.Midpoint.X = (mWiimoteState.IRState.IRSensors(1).Position.X + mWiimoteState.IRState.IRSensors(0).Position.X) / 2.0f
				mWiimoteState.IRState.Midpoint.Y = (mWiimoteState.IRState.IRSensors(1).Position.Y + mWiimoteState.IRState.IRSensors(0).Position.Y) / 2.0f
			Else
				mWiimoteState.IRState.Midpoint.Y = 0.0f
				mWiimoteState.IRState.Midpoint.X = mWiimoteState.IRState.Midpoint.Y
			End If
		End Sub

		''' <summary>
		''' Parse data from an extension controller
		''' </summary>
		''' <param name="buff">Data buffer</param>
		''' <param name="offset">Offset into data buffer</param>
		Private Sub ParseExtension(ByVal buff() As Byte, ByVal offset As Integer)
			Select Case mWiimoteState.ExtensionType
				Case ExtensionType.Nunchuk
					mWiimoteState.NunchukState.RawJoystick.X = buff(offset)
					mWiimoteState.NunchukState.RawJoystick.Y = buff(offset + 1)
					mWiimoteState.NunchukState.AccelState.RawValues.X = buff(offset + 2)
					mWiimoteState.NunchukState.AccelState.RawValues.Y = buff(offset + 3)
					mWiimoteState.NunchukState.AccelState.RawValues.Z = buff(offset + 4)

					mWiimoteState.NunchukState.C = (buff(offset + 5) And &H02) = 0
					mWiimoteState.NunchukState.Z = (buff(offset + 5) And &H01) = 0

					mWiimoteState.NunchukState.AccelState.Values.X = CSng(CSng(mWiimoteState.NunchukState.AccelState.RawValues.X) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.X0) / (CSng(mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.XG) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.X0)
					mWiimoteState.NunchukState.AccelState.Values.Y = CSng(CSng(mWiimoteState.NunchukState.AccelState.RawValues.Y) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Y0) / (CSng(mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.YG) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Y0)
					mWiimoteState.NunchukState.AccelState.Values.Z = CSng(CSng(mWiimoteState.NunchukState.AccelState.RawValues.Z) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Z0) / (CSng(mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.ZG) - mWiimoteState.NunchukState.CalibrationInfo.AccelCalibration.Z0)

					If mWiimoteState.NunchukState.CalibrationInfo.MaxX <> &H00 Then
						mWiimoteState.NunchukState.Joystick.X = CSng(CSng(mWiimoteState.NunchukState.RawJoystick.X) - mWiimoteState.NunchukState.CalibrationInfo.MidX) / (CSng(mWiimoteState.NunchukState.CalibrationInfo.MaxX) - mWiimoteState.NunchukState.CalibrationInfo.MinX)
					End If

					If mWiimoteState.NunchukState.CalibrationInfo.MaxY <> &H00 Then
						mWiimoteState.NunchukState.Joystick.Y = CSng(CSng(mWiimoteState.NunchukState.RawJoystick.Y) - mWiimoteState.NunchukState.CalibrationInfo.MidY) / (CSng(mWiimoteState.NunchukState.CalibrationInfo.MaxY) - mWiimoteState.NunchukState.CalibrationInfo.MinY)
					End If


				Case ExtensionType.ClassicController
					mWiimoteState.ClassicControllerState.RawJoystickL.X = CByte(buff(offset) And &H3f)
					mWiimoteState.ClassicControllerState.RawJoystickL.Y = CByte(buff(offset + 1) And &H3f)
					mWiimoteState.ClassicControllerState.RawJoystickR.X = CByte((buff(offset + 2) >> 7) Or (buff(offset + 1) And &Hc0) >> 5 Or (buff(offset) And &Hc0) >> 3)
					mWiimoteState.ClassicControllerState.RawJoystickR.Y = CByte(buff(offset + 2) And &H1f)

					mWiimoteState.ClassicControllerState.RawTriggerL = CByte(((buff(offset + 2) And &H60) >> 2) Or (buff(offset + 3) >> 5))
					mWiimoteState.ClassicControllerState.RawTriggerR = CByte(buff(offset + 3) And &H1f)

					mWiimoteState.ClassicControllerState.ButtonState.TriggerR = (buff(offset + 4) And &H02) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Plus = (buff(offset + 4) And &H04) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Home = (buff(offset + 4) And &H08) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Minus = (buff(offset + 4) And &H10) = 0
					mWiimoteState.ClassicControllerState.ButtonState.TriggerL = (buff(offset + 4) And &H20) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Down = (buff(offset + 4) And &H40) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Right = (buff(offset + 4) And &H80) = 0

					mWiimoteState.ClassicControllerState.ButtonState.Up = (buff(offset + 5) And &H01) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Left = (buff(offset + 5) And &H02) = 0
					mWiimoteState.ClassicControllerState.ButtonState.ZR = (buff(offset + 5) And &H04) = 0
					mWiimoteState.ClassicControllerState.ButtonState.X = (buff(offset + 5) And &H08) = 0
					mWiimoteState.ClassicControllerState.ButtonState.A = (buff(offset + 5) And &H10) = 0
					mWiimoteState.ClassicControllerState.ButtonState.Y = (buff(offset + 5) And &H20) = 0
					mWiimoteState.ClassicControllerState.ButtonState.B = (buff(offset + 5) And &H40) = 0
					mWiimoteState.ClassicControllerState.ButtonState.ZL = (buff(offset + 5) And &H80) = 0

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXL <> &H00 Then
						mWiimoteState.ClassicControllerState.JoystickL.X = CSng(CSng(mWiimoteState.ClassicControllerState.RawJoystickL.X) - mWiimoteState.ClassicControllerState.CalibrationInfo.MidXL) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXL - mWiimoteState.ClassicControllerState.CalibrationInfo.MinXL)
					End If

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYL <> &H00 Then
						mWiimoteState.ClassicControllerState.JoystickL.Y = CSng(CSng(mWiimoteState.ClassicControllerState.RawJoystickL.Y) - mWiimoteState.ClassicControllerState.CalibrationInfo.MidYL) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYL - mWiimoteState.ClassicControllerState.CalibrationInfo.MinYL)
					End If

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXR <> &H00 Then
						mWiimoteState.ClassicControllerState.JoystickR.X = CSng(CSng(mWiimoteState.ClassicControllerState.RawJoystickR.X) - mWiimoteState.ClassicControllerState.CalibrationInfo.MidXR) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxXR - mWiimoteState.ClassicControllerState.CalibrationInfo.MinXR)
					End If

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYR <> &H00 Then
						mWiimoteState.ClassicControllerState.JoystickR.Y = CSng(CSng(mWiimoteState.ClassicControllerState.RawJoystickR.Y) - mWiimoteState.ClassicControllerState.CalibrationInfo.MidYR) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxYR - mWiimoteState.ClassicControllerState.CalibrationInfo.MinYR)
					End If

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerL <> &H00 Then
						mWiimoteState.ClassicControllerState.TriggerL = (mWiimoteState.ClassicControllerState.RawTriggerL) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerL - mWiimoteState.ClassicControllerState.CalibrationInfo.MinTriggerL)
					End If

					If mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerR <> &H00 Then
						mWiimoteState.ClassicControllerState.TriggerR = (mWiimoteState.ClassicControllerState.RawTriggerR) / CSng(mWiimoteState.ClassicControllerState.CalibrationInfo.MaxTriggerR - mWiimoteState.ClassicControllerState.CalibrationInfo.MinTriggerR)
					End If

				Case ExtensionType.Guitar
					If ((buff(offset) And &H80) = 0) Then
						mWiimoteState.GuitarState.GuitarType = GuitarType.GuitarHeroWorldTour
					Else
						mWiimoteState.GuitarState.GuitarType = GuitarType.GuitarHero3
					End If

					mWiimoteState.GuitarState.ButtonState.Plus = (buff(offset + 4) And &H04) = 0
					mWiimoteState.GuitarState.ButtonState.Minus = (buff(offset + 4) And &H10) = 0
					mWiimoteState.GuitarState.ButtonState.StrumDown = (buff(offset + 4) And &H40) = 0

					mWiimoteState.GuitarState.ButtonState.StrumUp = (buff(offset + 5) And &H01) = 0
					mWiimoteState.GuitarState.FretButtonState.Yellow = (buff(offset + 5) And &H08) = 0
					mWiimoteState.GuitarState.FretButtonState.Green = (buff(offset + 5) And &H10) = 0
					mWiimoteState.GuitarState.FretButtonState.Blue = (buff(offset + 5) And &H20) = 0
					mWiimoteState.GuitarState.FretButtonState.Red = (buff(offset + 5) And &H40) = 0
					mWiimoteState.GuitarState.FretButtonState.Orange = (buff(offset + 5) And &H80) = 0

					' it appears the joystick values are only 6 bits
					mWiimoteState.GuitarState.RawJoystick.X = (buff(offset + 0) And &H3f)
					mWiimoteState.GuitarState.RawJoystick.Y = (buff(offset + 1) And &H3f)

					' and the whammy bar is only 5 bits
					mWiimoteState.GuitarState.RawWhammyBar = CByte(buff(offset + 3) And &H1f)

					mWiimoteState.GuitarState.Joystick.X = CSng(mWiimoteState.GuitarState.RawJoystick.X - &H1f) / &H3f ' not fully accurate, but close
					mWiimoteState.GuitarState.Joystick.Y = CSng(mWiimoteState.GuitarState.RawJoystick.Y - &H1f) / &H3f ' not fully accurate, but close
					mWiimoteState.GuitarState.WhammyBar = CSng(mWiimoteState.GuitarState.RawWhammyBar) / &H0a ' seems like there are 10 positions?

					mWiimoteState.GuitarState.TouchbarState.Yellow = False
					mWiimoteState.GuitarState.TouchbarState.Green = False
					mWiimoteState.GuitarState.TouchbarState.Blue = False
					mWiimoteState.GuitarState.TouchbarState.Red = False
					mWiimoteState.GuitarState.TouchbarState.Orange = False

					Select Case buff(offset + 2) And &H1f
						Case &H04
							mWiimoteState.GuitarState.TouchbarState.Green = True
						Case &H07
							mWiimoteState.GuitarState.TouchbarState.Green = True
							mWiimoteState.GuitarState.TouchbarState.Red = True
						Case &H0a
							mWiimoteState.GuitarState.TouchbarState.Red = True
						Case &H0c, &H0d
							mWiimoteState.GuitarState.TouchbarState.Red = True
							mWiimoteState.GuitarState.TouchbarState.Yellow = True
						Case &H12, &H13
							mWiimoteState.GuitarState.TouchbarState.Yellow = True
						Case &H14, &H15
							mWiimoteState.GuitarState.TouchbarState.Yellow = True
							mWiimoteState.GuitarState.TouchbarState.Blue = True
						Case &H17, &H18
							mWiimoteState.GuitarState.TouchbarState.Blue = True
						Case &H1a
							mWiimoteState.GuitarState.TouchbarState.Blue = True
							mWiimoteState.GuitarState.TouchbarState.Orange = True
						Case &H1f
							mWiimoteState.GuitarState.TouchbarState.Orange = True
					End Select

				Case ExtensionType.Drums
					' it appears the joystick values are only 6 bits
					mWiimoteState.DrumsState.RawJoystick.X = (buff(offset + 0) And &H3f)
					mWiimoteState.DrumsState.RawJoystick.Y = (buff(offset + 1) And &H3f)

					mWiimoteState.DrumsState.Plus = (buff(offset + 4) And &H04) = 0
					mWiimoteState.DrumsState.Minus = (buff(offset + 4) And &H10) = 0

					mWiimoteState.DrumsState.Pedal = (buff(offset + 5) And &H04) = 0
					mWiimoteState.DrumsState.Blue = (buff(offset + 5) And &H08) = 0
					mWiimoteState.DrumsState.Green = (buff(offset + 5) And &H10) = 0
					mWiimoteState.DrumsState.Yellow = (buff(offset + 5) And &H20) = 0
					mWiimoteState.DrumsState.Red = (buff(offset + 5) And &H40) = 0
					mWiimoteState.DrumsState.Orange = (buff(offset + 5) And &H80) = 0

					mWiimoteState.DrumsState.Joystick.X = CSng(mWiimoteState.DrumsState.RawJoystick.X - &H1f) / &H3f ' not fully accurate, but close
					mWiimoteState.DrumsState.Joystick.Y = CSng(mWiimoteState.DrumsState.RawJoystick.Y - &H1f) / &H3f ' not fully accurate, but close

					If (buff(offset + 2) And &H40) = 0 Then
						Dim pad As Integer = (buff(offset + 2) >> 1) And &H1f
						Dim velocity As Integer = (buff(offset + 3) >> 5)

						If velocity <> 7 Then
							Select Case pad
								Case &H1b
									mWiimoteState.DrumsState.PedalVelocity = velocity
								Case &H19
									mWiimoteState.DrumsState.RedVelocity = velocity
								Case &H11
									mWiimoteState.DrumsState.YellowVelocity = velocity
								Case &H0f
									mWiimoteState.DrumsState.BlueVelocity = velocity
								Case &H0e
									mWiimoteState.DrumsState.OrangeVelocity = velocity
								Case &H12
									mWiimoteState.DrumsState.GreenVelocity = velocity
							End Select
						End If
					End If


				Case ExtensionType.BalanceBoard
					mWiimoteState.BalanceBoardState.SensorValuesRaw.TopRight = CShort(Fix(CShort(Fix(buff(offset + 0))) << 8 Or buff(offset + 1)))
					mWiimoteState.BalanceBoardState.SensorValuesRaw.BottomRight = CShort(Fix(CShort(Fix(buff(offset + 2))) << 8 Or buff(offset + 3)))
					mWiimoteState.BalanceBoardState.SensorValuesRaw.TopLeft = CShort(Fix(CShort(Fix(buff(offset + 4))) << 8 Or buff(offset + 5)))
					mWiimoteState.BalanceBoardState.SensorValuesRaw.BottomLeft = CShort(Fix(CShort(Fix(buff(offset + 6))) << 8 Or buff(offset + 7)))

					mWiimoteState.BalanceBoardState.SensorValuesKg.TopLeft = GetBalanceBoardSensorValue(mWiimoteState.BalanceBoardState.SensorValuesRaw.TopLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.TopLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.TopLeft)
					mWiimoteState.BalanceBoardState.SensorValuesKg.TopRight = GetBalanceBoardSensorValue(mWiimoteState.BalanceBoardState.SensorValuesRaw.TopRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.TopRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.TopRight)
					mWiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft = GetBalanceBoardSensorValue(mWiimoteState.BalanceBoardState.SensorValuesRaw.BottomLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.BottomLeft, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.BottomLeft)
					mWiimoteState.BalanceBoardState.SensorValuesKg.BottomRight = GetBalanceBoardSensorValue(mWiimoteState.BalanceBoardState.SensorValuesRaw.BottomRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg17.BottomRight, mWiimoteState.BalanceBoardState.CalibrationInfo.Kg34.BottomRight)

					mWiimoteState.BalanceBoardState.SensorValuesLb.TopLeft = (mWiimoteState.BalanceBoardState.SensorValuesKg.TopLeft * KG2LB)
					mWiimoteState.BalanceBoardState.SensorValuesLb.TopRight = (mWiimoteState.BalanceBoardState.SensorValuesKg.TopRight * KG2LB)
					mWiimoteState.BalanceBoardState.SensorValuesLb.BottomLeft = (mWiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft * KG2LB)
					mWiimoteState.BalanceBoardState.SensorValuesLb.BottomRight = (mWiimoteState.BalanceBoardState.SensorValuesKg.BottomRight * KG2LB)

					mWiimoteState.BalanceBoardState.WeightKg = (mWiimoteState.BalanceBoardState.SensorValuesKg.TopLeft + mWiimoteState.BalanceBoardState.SensorValuesKg.TopRight + mWiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft + mWiimoteState.BalanceBoardState.SensorValuesKg.BottomRight) / 4.0f
					mWiimoteState.BalanceBoardState.WeightLb = (mWiimoteState.BalanceBoardState.SensorValuesLb.TopLeft + mWiimoteState.BalanceBoardState.SensorValuesLb.TopRight + mWiimoteState.BalanceBoardState.SensorValuesLb.BottomLeft + mWiimoteState.BalanceBoardState.SensorValuesLb.BottomRight) / 4.0f

					Dim Kx As Single = (mWiimoteState.BalanceBoardState.SensorValuesKg.TopLeft + mWiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft) / (mWiimoteState.BalanceBoardState.SensorValuesKg.TopRight + mWiimoteState.BalanceBoardState.SensorValuesKg.BottomRight)
					Dim Ky As Single = (mWiimoteState.BalanceBoardState.SensorValuesKg.TopLeft + mWiimoteState.BalanceBoardState.SensorValuesKg.TopRight) / (mWiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft + mWiimoteState.BalanceBoardState.SensorValuesKg.BottomRight)

					mWiimoteState.BalanceBoardState.CenterOfGravity.X = (CSng(Kx - 1) / CSng(Kx + 1)) * CSng(-BSL \ 2)
					mWiimoteState.BalanceBoardState.CenterOfGravity.Y = (CSng(Ky - 1) / CSng(Ky + 1)) * CSng(-BSW \ 2)
			End Select
		End Sub

		Private Function GetBalanceBoardSensorValue(ByVal sensor As Short, ByVal min As Short, ByVal mid As Short, ByVal max As Short) As Single
			If max = mid OrElse mid = min Then
				Return 0
			End If

			If sensor < mid Then
				Return 68.0f * (CSng(sensor - min) / (mid - min))
			Else
				Return 68.0f * (CSng(sensor - mid) / (max - mid)) + 68.0f
			End If
		End Function


		''' <summary>
		''' Parse data returned from a read report
		''' </summary>
		''' <param name="buff">Data buffer</param>
		Private Sub ParseReadData(ByVal buff() As Byte)
			If (buff(3) And &H08) <> 0 Then
				Throw New WiimoteException("Error reading data from Wiimote: Bytes do not exist.")
			End If

			If (buff(3) And &H07) <> 0 Then
				Throw New WiimoteException("Error reading data from Wiimote: Attempt to read from write-only registers.")
			End If

			' get our size and offset from the report
			Dim size As Integer = (buff(3) >> 4) + 1
			Dim offset As Integer = (buff(4) << 8 Or buff(5))

			' add it to the buffer
			Array.Copy(buff, 6, mReadBuff, offset - mAddress, size)

			' if we've read it all, set the event
			If mAddress + mSize = offset + size Then
				mReadDone.Set()
			End If
		End Sub

		''' <summary>
		''' Returns whether rumble is currently enabled.
		''' </summary>
		''' <returns>Byte indicating true (0x01) or false (0x00)</returns>
		Private Function GetRumbleBit() As Byte
			If mWiimoteState.Rumble Then
				Return CByte(&H01)
			Else
				Return CByte(&H00)
			End If
		End Function

		''' <summary>
		''' Read calibration information stored on Wiimote
		''' </summary>
		Private Sub ReadWiimoteCalibration()
			' this appears to change the report type to 0x31
			Dim buff() As Byte = ReadData(&H0016, 7)

			mWiimoteState.AccelCalibrationInfo.X0 = buff(0)
			mWiimoteState.AccelCalibrationInfo.Y0 = buff(1)
			mWiimoteState.AccelCalibrationInfo.Z0 = buff(2)
			mWiimoteState.AccelCalibrationInfo.XG = buff(4)
			mWiimoteState.AccelCalibrationInfo.YG = buff(5)
			mWiimoteState.AccelCalibrationInfo.ZG = buff(6)
		End Sub

		''' <summary>
		''' Set Wiimote reporting mode (if using an IR report type, IR sensitivity is set to WiiLevel3)
		''' </summary>
		''' <param name="type">Report type</param>
		''' <param name="continuous">Continuous data</param>
		Public Sub SetReportType(ByVal type As InputReport, ByVal continuous As Boolean)
			SetReportType(type, IRSensitivity.Maximum, continuous)
		End Sub

		''' <summary>
		''' Set Wiimote reporting mode
		''' </summary>
		''' <param name="type">Report type</param>
		''' <param name="irSensitivity">IR sensitivity</param>
		''' <param name="continuous">Continuous data</param>
		Public Sub SetReportType(ByVal type As InputReport, ByVal irSensitivity As IRSensitivity, ByVal continuous As Boolean)
			' only 1 report type allowed for the BB
			If mWiimoteState.ExtensionType = ExtensionType.BalanceBoard Then
				type = InputReport.ButtonsExtension
			End If

			Select Case type
				Case InputReport.IRAccel
					EnableIR(IRMode.Extended, irSensitivity)
				Case InputReport.IRExtensionAccel
					EnableIR(IRMode.Basic, irSensitivity)
				Case Else
					DisableIR()
			End Select

			ClearReport()
			mBuff(0) = CByte(OutputReport.Type)
			If continuous Then
				If mWiimoteState.Rumble Then
					mBuff(1) = CByte((&H04) Or CByte(&H01))
				Else
					mBuff(1) = CByte((&H04) Or CByte(&H00))
				End If
			Else
				If mWiimoteState.Rumble Then
					mBuff(1) = CByte((&H00) Or CByte(&H01))
				Else
					mBuff(1) = CByte((&H00) Or CByte(&H00))
				End If
			End If
			mBuff(2) = CByte(type)

			WriteReport()
		End Sub

		''' <summary>
		''' Set the LEDs on the Wiimote
		''' </summary>
		''' <param name="led1">LED 1</param>
		''' <param name="led2">LED 2</param>
		''' <param name="led3">LED 3</param>
		''' <param name="led4">LED 4</param>
		Public Sub SetLEDs(ByVal led1 As Boolean, ByVal led2 As Boolean, ByVal led3 As Boolean, ByVal led4 As Boolean)
			mWiimoteState.LEDState.LED1 = led1
			mWiimoteState.LEDState.LED2 = led2
			mWiimoteState.LEDState.LED3 = led3
			mWiimoteState.LEDState.LED4 = led4

			ClearReport()

			mBuff(0) = CByte(OutputReport.LEDs)
			If led1 Then
				If led2 Then
					If led3 Then
						If led4 Then
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If led4 Then
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				Else
					If led3 Then
						If led4 Then
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If led4 Then
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				End If
			Else
				If led2 Then
					If led3 Then
						If led4 Then
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If led4 Then
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				Else
					If led3 Then
						If led4 Then
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If led4 Then
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				End If
			End If

			WriteReport()
		End Sub

		''' <summary>
		''' Set the LEDs on the Wiimote
		''' </summary>
		''' <param name="leds">The value to be lit up in base2 on the Wiimote</param>
		Public Sub SetLEDs(ByVal leds As Integer)
			mWiimoteState.LEDState.LED1 = (leds And &H01) > 0
			mWiimoteState.LEDState.LED2 = (leds And &H02) > 0
			mWiimoteState.LEDState.LED3 = (leds And &H04) > 0
			mWiimoteState.LEDState.LED4 = (leds And &H08) > 0

			ClearReport()

			mBuff(0) = CByte(OutputReport.LEDs)
			If (leds And &H01) > 0 Then
				If (leds And &H02) > 0 Then
					If (leds And &H04) > 0 Then
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H20) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				Else
					If (leds And &H04) > 0 Then
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H10) Or (&H00) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				End If
			Else
				If (leds And &H02) > 0 Then
					If (leds And &H04) > 0 Then
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H20) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				Else
					If (leds And &H04) > 0 Then
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H40) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H40) Or (&H00) Or GetRumbleBit())
						End If
					Else
						If (leds And &H08) > 0 Then
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H00) Or (&H80) Or GetRumbleBit())
						Else
							mBuff(1) = CByte((&H00) Or (&H00) Or (&H00) Or (&H00) Or GetRumbleBit())
						End If
					End If
				End If
			End If

			WriteReport()
		End Sub

		''' <summary>
		''' Toggle rumble
		''' </summary>
		''' <param name="on">On or off</param>
		Public Sub SetRumble(ByVal [on] As Boolean)
			mWiimoteState.Rumble = [on]

			' the LED report also handles rumble
			SetLEDs(mWiimoteState.LEDState.LED1, mWiimoteState.LEDState.LED2, mWiimoteState.LEDState.LED3, mWiimoteState.LEDState.LED4)
		End Sub

		''' <summary>
		''' Retrieve the current status of the Wiimote and extensions.  Replaces GetBatteryLevel() since it was poorly named.
		''' </summary>
		Public Sub GetStatus()
			ClearReport()

			mBuff(0) = CByte(OutputReport.Status)
			mBuff(1) = GetRumbleBit()

			WriteReport()

			' signal the status report finished
			If (Not mStatusDone.WaitOne(3000, False)) Then
				Throw New WiimoteException("Timed out waiting for status report")
			End If
		End Sub

		''' <summary>
		''' Turn on the IR sensor
		''' </summary>
		''' <param name="mode">The data report mode</param>
		''' <param name="irSensitivity">IR sensitivity</param>
		Private Sub EnableIR(ByVal mode As IRMode, ByVal irSensitivity As IRSensitivity)
			mWiimoteState.IRState.Mode = mode

			ClearReport()
			mBuff(0) = CByte(OutputReport.IR)
			mBuff(1) = CByte(&H04 Or GetRumbleBit())
			WriteReport()

			ClearReport()
			mBuff(0) = CByte(OutputReport.IR2)
			mBuff(1) = CByte(&H04 Or GetRumbleBit())
			WriteReport()

			WriteData(REGISTER_IR, &H08)
			Select Case irSensitivity
				Case IRSensitivity.WiiLevel1
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H02, &H00, &H00, &H71, &H01, &H00, &H64, &H00, &Hfe})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&Hfd, &H05})
				Case IRSensitivity.WiiLevel2
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H02, &H00, &H00, &H71, &H01, &H00, &H96, &H00, &Hb4})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&Hb3, &H04})
				Case IRSensitivity.WiiLevel3
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H02, &H00, &H00, &H71, &H01, &H00, &Haa, &H00, &H64})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&H63, &H03})
				Case IRSensitivity.WiiLevel4
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H02, &H00, &H00, &H71, &H01, &H00, &Hc8, &H00, &H36})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&H35, &H03})
				Case IRSensitivity.WiiLevel5
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H07, &H00, &H00, &H71, &H01, &H00, &H72, &H00, &H20})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&H1, &H03})
				Case IRSensitivity.Maximum
					WriteData(REGISTER_IR_SENSITIVITY_1, 9, New Byte() {&H02, &H00, &H00, &H71, &H01, &H00, &H90, &H00, &H41})
					WriteData(REGISTER_IR_SENSITIVITY_2, 2, New Byte() {&H40, &H00})
				Case Else
					Throw New ArgumentOutOfRangeException("irSensitivity")
			End Select
			WriteData(REGISTER_IR_MODE, CByte(mode))
			WriteData(REGISTER_IR, &H08)
		End Sub

		''' <summary>
		''' Disable the IR sensor
		''' </summary>
		Private Sub DisableIR()
			mWiimoteState.IRState.Mode = IRMode.Off

			ClearReport()
			mBuff(0) = CByte(OutputReport.IR)
			mBuff(1) = GetRumbleBit()
			WriteReport()

			ClearReport()
			mBuff(0) = CByte(OutputReport.IR2)
			mBuff(1) = GetRumbleBit()
			WriteReport()
		End Sub

		''' <summary>
		''' Initialize the report data buffer
		''' </summary>
		Private Sub ClearReport()
			Array.Clear(mBuff, 0, REPORT_LENGTH)
		End Sub

		''' <summary>
		''' Write a report to the Wiimote
		''' </summary>
		Private Sub WriteReport()
			Debug.WriteLine("WriteReport: " & mBuff(0).ToString("x"))
			If mAltWriteMethod Then
				HIDImports.HidD_SetOutputReport(Me.mHandle.DangerousGetHandle(), mBuff, CUInt(mBuff.Length))
			ElseIf mStream IsNot Nothing Then
				mStream.Write(mBuff, 0, REPORT_LENGTH)
			End If

			If mBuff(0) = CByte(OutputReport.WriteMemory) Then
				Debug.WriteLine("Wait")
				If (Not mWriteDone.WaitOne(1000, False)) Then
					Debug.WriteLine("Wait failed")
				End If
				'throw new WiimoteException("Error writing data to Wiimote...is it connected?");
			End If
		End Sub

		''' <summary>
		''' Read data or register from Wiimote
		''' </summary>
		''' <param name="address">Address to read</param>
		''' <param name="size">Length to read</param>
		''' <returns>Data buffer</returns>
		Public Function ReadData(ByVal address As Integer, ByVal size As Short) As Byte()
			ClearReport()

			mReadBuff = New Byte(size - 1){}
			mAddress = address And &Hffff
			mSize = size

			mBuff(0) = CByte(OutputReport.ReadMemory)
			mBuff(1) = CByte(((address And &Hff000000L) >> 24) Or GetRumbleBit())
			mBuff(2) = CByte((address And &H00ff0000) >> 16)
			mBuff(3) = CByte((address And &H0000ff00) >> 8)
			mBuff(4) = CByte(address And &H000000ff)

			mBuff(5) = CByte((size And &Hff00) >> 8)
			mBuff(6) = CByte(size And &Hff)

			WriteReport()

			If (Not mReadDone.WaitOne(1000, False)) Then
				Throw New WiimoteException("Error reading data from Wiimote...is it connected?")
			End If

			Return mReadBuff
		End Function

		''' <summary>
		''' Write a single byte to the Wiimote
		''' </summary>
		''' <param name="address">Address to write</param>
		''' <param name="data">Byte to write</param>
		Public Sub WriteData(ByVal address As Integer, ByVal data As Byte)
			WriteData(address, 1, New Byte() { data })
		End Sub

		''' <summary>
		''' Write a byte array to a specified address
		''' </summary>
		''' <param name="address">Address to write</param>
		''' <param name="size">Length of buffer</param>
		''' <param name="buff">Data buffer</param>

		Public Sub WriteData(ByVal address As Integer, ByVal size As Byte, ByVal buff() As Byte)
			ClearReport()

			mBuff(0) = CByte(OutputReport.WriteMemory)
			mBuff(1) = CByte(((address And &Hff000000L) >> 24) Or GetRumbleBit())
			mBuff(2) = CByte((address And &H00ff0000) >> 16)
			mBuff(3) = CByte((address And &H0000ff00) >> 8)
			mBuff(4) = CByte(address And &H000000ff)
			mBuff(5) = size
			Array.Copy(buff, 0, mBuff, 6, size)

			WriteReport()
		End Sub

		''' <summary>
		''' Current Wiimote state
		''' </summary>
		Public ReadOnly Property WiimoteState() As WiimoteState
			Get
				Return mWiimoteState
			End Get
		End Property

		'''<summary>
		''' Unique identifier for this Wiimote (not persisted across application instances)
		'''</summary>
		Public ReadOnly Property ID() As Guid
			Get
				Return mID
			End Get
		End Property

		''' <summary>
		''' HID device path for this Wiimote (valid until Wiimote is disconnected)
		''' </summary>
		Public ReadOnly Property HIDDevicePath() As String
			Get
				Return mDevicePath
			End Get
		End Property

		#Region "IDisposable Members"

		''' <summary>
		''' Dispose Wiimote
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Dispose wiimote
		''' </summary>
		''' <param name="disposing">Disposing?</param>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
			' close up our handles
			If disposing Then
				Disconnect()
			End If
		End Sub
		#End Region
	End Class

	''' <summary>
	''' Thrown when no Wiimotes are found in the HID device list
	''' </summary>
	<Serializable> _
	Public Class WiimoteNotFoundException
		Inherits ApplicationException
		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Error message</param>
		Public Sub New(ByVal message As String)
			MyBase.New(message)
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Error message</param>
		''' <param name="innerException">Inner exception</param>
		Public Sub New(ByVal message As String, ByVal innerException As Exception)
			MyBase.New(message, innerException)
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="info">Serialization info</param>
		''' <param name="context">Streaming context</param>
		Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			MyBase.New(info, context)
		End Sub
	End Class

	''' <summary>
	''' Represents errors that occur during the execution of the Wiimote library
	''' </summary>
	<Serializable> _
	Public Class WiimoteException
		Inherits ApplicationException
		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Error message</param>
		Public Sub New(ByVal message As String)
			MyBase.New(message)
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Error message</param>
		''' <param name="innerException">Inner exception</param>
		Public Sub New(ByVal message As String, ByVal innerException As Exception)
			MyBase.New(message, innerException)
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="info">Serialization info</param>
		''' <param name="context">Streaming context</param>
		Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			MyBase.New(info, context)
		End Sub
	End Class
End Namespace