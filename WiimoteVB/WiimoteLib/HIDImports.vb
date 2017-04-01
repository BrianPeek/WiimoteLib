'////////////////////////////////////////////////////////////////////////////////
'	HIDImports.cs
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
Imports System.IO
Imports Microsoft.Win32.SafeHandles

Namespace WiimoteLib
	''' <summary>
	''' Win32 import information for use with the Wiimote library
	''' </summary>
	Friend Class HIDImports
		'
		' Flags controlling what is included in the device information set built
		' by SetupDiGetClassDevs
		'
		Public Const DIGCF_DEFAULT As Integer = &H00000001 ' only valid with DIGCF_DEVICEINTERFACE
		Public Const DIGCF_PRESENT As Integer = &H00000002
		Public Const DIGCF_ALLCLASSES As Integer = &H00000004
		Public Const DIGCF_PROFILE As Integer = &H00000008
		Public Const DIGCF_DEVICEINTERFACE As Integer = &H00000010

		<Flags> _
		Public Enum EFileAttributes As UInteger
		   [Readonly] = &H00000001
		   Hidden = &H00000002
		   System = &H00000004
		   Directory = &H00000010
		   Archive = &H00000020
		   Device = &H00000040
		   Normal = &H00000080
		   Temporary = &H00000100
		   SparseFile = &H00000200
		   ReparsePoint = &H00000400
		   Compressed = &H00000800
		   Offline = &H00001000
		   NotContentIndexed= &H00002000
		   Encrypted = &H00004000
		   Write_Through = &H80000000L
		   Overlapped = &H40000000
		   NoBuffering = &H20000000
		   RandomAccess = &H10000000
		   SequentialScan = &H08000000
		   DeleteOnClose = &H04000000
		   BackupSemantics = &H02000000
		   PosixSemantics = &H01000000
		   OpenReparsePoint = &H00200000
		   OpenNoRecall = &H00100000
		   FirstPipeInstance= &H00080000
		End Enum

		<StructLayout(LayoutKind.Sequential)> _
		Public Structure SP_DEVINFO_DATA
			Public cbSize As UInteger
			Public ClassGuid As Guid
			Public DevInst As UInteger
			Public Reserved As IntPtr
		End Structure

		<StructLayout(LayoutKind.Sequential)> _
		Public Structure SP_DEVICE_INTERFACE_DATA
			Public cbSize As Integer
			Public InterfaceClassGuid As Guid
			Public Flags As Integer
			Public RESERVED As IntPtr
		End Structure

		<StructLayout(LayoutKind.Sequential, Pack := 1)> _
		Public Structure SP_DEVICE_INTERFACE_DETAIL_DATA
			Public cbSize As UInt32
			<MarshalAs(UnmanagedType.ByValTStr, SizeConst := 256)> _
			Public DevicePath As String
		End Structure

		<StructLayout(LayoutKind.Sequential)> _
		Public Structure HIDD_ATTRIBUTES
			Public Size As Integer
			Public VendorID As Short
			Public ProductID As Short
			Public VersionNumber As Short
		End Structure

		<DllImport("hid.dll", CharSet:=CharSet.Auto, SetLastError := True)> _
		Public Shared Sub HidD_GetHidGuid(<System.Runtime.InteropServices.Out()> ByRef gHid As Guid)
		End Sub

		<DllImport("hid.dll")> _
		Public Shared Function HidD_GetAttributes(ByVal HidDeviceObject As IntPtr, ByRef Attributes As HIDD_ATTRIBUTES) As Boolean
		End Function

		<DllImport("hid.dll")> _
		Friend Shared Function HidD_SetOutputReport(ByVal HidDeviceObject As IntPtr, ByVal lpReportBuffer() As Byte, ByVal ReportBufferLength As UInteger) As Boolean
		End Function

		<DllImport("setupapi.dll", CharSet := CharSet.Auto, SetLastError := True)> _
		Public Shared Function SetupDiGetClassDevs(ByRef ClassGuid As Guid, <MarshalAs(UnmanagedType.LPTStr)> ByVal Enumerator As String, ByVal hwndParent As IntPtr, ByVal Flags As UInt32) As IntPtr
		End Function

			'ref SP_DEVINFO_DATA devInfo,
		<DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError := True)> _
		Public Shared Function SetupDiEnumDeviceInterfaces(ByVal hDevInfo As IntPtr, ByVal devInvo As IntPtr, ByRef interfaceClassGuid As Guid, ByVal memberIndex As Int32, ByRef deviceInterfaceData As SP_DEVICE_INTERFACE_DATA) As Boolean
		End Function

		<DllImport("setupapi.dll", SetLastError := True)> _
		Public Shared Function SetupDiGetDeviceInterfaceDetail(ByVal hDevInfo As IntPtr, ByRef deviceInterfaceData As SP_DEVICE_INTERFACE_DATA, ByVal deviceInterfaceDetailData As IntPtr, ByVal deviceInterfaceDetailDataSize As UInt32, <System.Runtime.InteropServices.Out()> ByRef requiredSize As UInt32, ByVal deviceInfoData As IntPtr) As Boolean
		End Function

		<DllImport("setupapi.dll", SetLastError := True)> _
		Public Shared Function SetupDiGetDeviceInterfaceDetail(ByVal hDevInfo As IntPtr, ByRef deviceInterfaceData As SP_DEVICE_INTERFACE_DATA, ByRef deviceInterfaceDetailData As SP_DEVICE_INTERFACE_DETAIL_DATA, ByVal deviceInterfaceDetailDataSize As UInt32, <System.Runtime.InteropServices.Out()> ByRef requiredSize As UInt32, ByVal deviceInfoData As IntPtr) As Boolean
		End Function

		<DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError := True)> _
		Public Shared Function SetupDiDestroyDeviceInfoList(ByVal hDevInfo As IntPtr) As UInt16
		End Function

		<DllImport("Kernel32.dll", SetLastError := True, CharSet := CharSet.Auto)> _
		Public Shared Function CreateFile(ByVal fileName As String, <MarshalAs(UnmanagedType.U4)> ByVal fileAccess As FileAccess, <MarshalAs(UnmanagedType.U4)> ByVal fileShare As FileShare, ByVal securityAttributes As IntPtr, <MarshalAs(UnmanagedType.U4)> ByVal creationDisposition As FileMode, <MarshalAs(UnmanagedType.U4)> ByVal flags As EFileAttributes, ByVal template As IntPtr) As SafeFileHandle
		End Function

			<DllImport("kernel32.dll", SetLastError:=True)> _
			Public Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
			End Function
	End Class
End Namespace
