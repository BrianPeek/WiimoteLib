'////////////////////////////////////////////////////////////////////////////////
'	DataTypes.cs
'	Managed Wiimote Library
'	Written by Brian Peek (http://www.brianpeek.com/)
'	for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'  and http://www.codeplex.com/WiimoteLib
'	for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System

' if we're building the MSRS version, we need to bring in the MSRS Attributes
' if we're not doing the MSRS build then define some fake attribute classes for DataMember/DataContract
#If MSRS Then
	Imports Microsoft.Dss.Core.Attributes
#Else
	Friend NotInheritable Class DataContract
		Inherits Attribute
	End Class

	Friend NotInheritable Class DataMember
		Inherits Attribute
	End Class
#End If

Namespace WiimoteLib
#If MSRS Then
	<DataContract> _
	Public Structure RumbleRequest
		<DataMember> _
		Public Rumble As Boolean
	End Structure
#End If

	''' <summary>
	''' Point structure for floating point 2D positions (X, Y)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure PointF
		''' <summary>
		''' X, Y coordinates of this point
		''' </summary>
		<DataMember> _
		Public X, Y As Single

		''' <summary>
		''' Convert to human-readable string
		''' </summary>
		''' <returns>A string that represents the point</returns>
		Public Overrides Function ToString() As String
			Return String.Format("{{X={0}, Y={1}}}", X, Y)
		End Function

	End Structure

	''' <summary>
	''' Point structure for int 2D positions (X, Y)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure Point
		''' <summary>
		''' X, Y coordinates of this point
		''' </summary>
		<DataMember> _
		Public X, Y As Integer

		''' <summary>
		''' Convert to human-readable string
		''' </summary>
		''' <returns>A string that represents the point.</returns>
		Public Overrides Function ToString() As String
			Return String.Format("{{X={0}, Y={1}}}", X, Y)
		End Function
	End Structure

	''' <summary>
	''' Point structure for floating point 3D positions (X, Y, Z)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure Point3F
		''' <summary>
		''' X, Y, Z coordinates of this point
		''' </summary>
		<DataMember> _
		Public X, Y, Z As Single

		''' <summary>
		''' Convert to human-readable string
		''' </summary>
		''' <returns>A string that represents the point</returns>
		Public Overrides Function ToString() As String
			Return String.Format("{{X={0}, Y={1}, Z={2}}}", X, Y, Z)
		End Function

	End Structure

	''' <summary>
	''' Point structure for int 3D positions (X, Y, Z)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure Point3
		''' <summary>
		''' X, Y, Z coordinates of this point
		''' </summary>
		<DataMember> _
		Public X, Y, Z As Integer

		''' <summary>
		''' Convert to human-readable string
		''' </summary>
		''' <returns>A string that represents the point.</returns>
		Public Overrides Function ToString() As String
			Return String.Format("{{X={0}, Y={1}, Z={2}}}", X, Y, Z)
		End Function
	End Structure

	''' <summary>
	''' Current overall state of the Wiimote and all attachments
	''' </summary>
	<Serializable, DataContract> _
	Public Class WiimoteState
		''' <summary>
		''' Current calibration information
		''' </summary>
		<DataMember> _
		Public AccelCalibrationInfo As AccelCalibrationInfo
		''' <summary>
		''' Current state of accelerometers
		''' </summary>
		<DataMember> _
		Public AccelState As AccelState
		''' <summary>
		''' Current state of buttons
		''' </summary>
		<DataMember> _
		Public ButtonState As ButtonState
		''' <summary>
		''' Current state of IR sensors
		''' </summary>
		<DataMember> _
		Public IRState As IRState
		''' <summary>
		''' Raw byte value of current battery level
		''' </summary>
		<DataMember> _
		Public BatteryRaw As Byte
		''' <summary>
		''' Calculated current battery level
		''' </summary>
		<DataMember> _
		Public Battery As Single
		''' <summary>
		''' Current state of rumble
		''' </summary>
		<DataMember> _
		Public Rumble As Boolean
		''' <summary>
		''' Is an extension controller inserted?
		''' </summary>
		<DataMember> _
		Public Extension As Boolean
		''' <summary>
		''' Extension controller currently inserted, if any
		''' </summary>
		<DataMember> _
		Public ExtensionType As ExtensionType
		''' <summary>
		''' Current state of Nunchuk extension
		''' </summary>
		<DataMember> _
		Public NunchukState As NunchukState
		''' <summary>
		''' Current state of Classic Controller extension
		''' </summary>
		<DataMember> _
		Public ClassicControllerState As ClassicControllerState
		''' <summary>
		''' Current state of Guitar extension
		''' </summary>
		<DataMember> _
		Public GuitarState As GuitarState
		''' <summary>
		''' Current state of Drums extension
		''' </summary>
		<DataMember> _
		Public DrumsState As DrumsState
		''' <summary>
		''' Current state of the Wii Fit Balance Board
		''' </summary>
		Public BalanceBoardState As BalanceBoardState
		''' <summary>
		''' Current state of LEDs
		''' </summary>
		<DataMember> _
		Public LEDState As LEDState

		''' <summary>
		''' Constructor for WiimoteState class
		''' </summary>
		Public Sub New()
			IRState.IRSensors = New IRSensor(3){}
		End Sub
	End Class

	''' <summary>
	''' Current state of LEDs
	''' </summary>
	<Serializable, DataContract> _
	Public Structure LEDState
		''' <summary>
		''' LED on the Wiimote
		''' </summary>
		<DataMember> _
		Public LED1, LED2, LED3, LED4 As Boolean
	End Structure

	''' <summary>
	''' Calibration information stored on the Nunchuk
	''' </summary>
	<Serializable, DataContract> _
	Public Structure NunchukCalibrationInfo
		''' <summary>
		''' Accelerometer calibration data
		''' </summary>
		Public AccelCalibration As AccelCalibrationInfo
		''' <summary>
		''' Joystick X-axis calibration
		''' </summary>
		<DataMember> _
		Public MinX, MidX, MaxX As Byte
		''' <summary>
		''' Joystick Y-axis calibration
		''' </summary>
		<DataMember> _
		Public MinY, MidY, MaxY As Byte
	End Structure

	''' <summary>
	''' Calibration information stored on the Classic Controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure ClassicControllerCalibrationInfo
		''' <summary>
		''' Left joystick X-axis 
		''' </summary>
		<DataMember> _
		Public MinXL, MidXL, MaxXL As Byte
		''' <summary>
		''' Left joystick Y-axis
		''' </summary>
		<DataMember> _
		Public MinYL, MidYL, MaxYL As Byte
		''' <summary>
		''' Right joystick X-axis
		''' </summary>
		<DataMember> _
		Public MinXR, MidXR, MaxXR As Byte
		''' <summary>
		''' Right joystick Y-axis
		''' </summary>
		<DataMember> _
		Public MinYR, MidYR, MaxYR As Byte
		''' <summary>
		''' Left analog trigger
		''' </summary>
		<DataMember> _
		Public MinTriggerL, MaxTriggerL As Byte
		''' <summary>
		''' Right analog trigger
		''' </summary>
		<DataMember> _
		Public MinTriggerR, MaxTriggerR As Byte
	End Structure

	''' <summary>
	''' Current state of the Nunchuk extension
	''' </summary>
	<Serializable, DataContract> _
	Public Structure NunchukState
		''' <summary>
		''' Calibration data for Nunchuk extension
		''' </summary>
		<DataMember> _
		Public CalibrationInfo As NunchukCalibrationInfo
		''' <summary>
		''' State of accelerometers
		''' </summary>
		<DataMember> _
		Public AccelState As AccelState
		''' <summary>
		''' Raw joystick position before normalization.  Values range between 0 and 255.
		''' </summary>
		<DataMember> _
		Public RawJoystick As Point
		''' <summary>
		''' Normalized joystick position.  Values range between -0.5 and 0.5
		''' </summary>
		<DataMember> _
		Public Joystick As PointF
		''' <summary>
		''' Digital button on Nunchuk extension
		''' </summary>
		<DataMember> _
		Public C, Z As Boolean
	End Structure

	''' <summary>
	''' Curernt button state of the Classic Controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure ClassicControllerButtonState
		''' <summary>
		''' Digital button on the Classic Controller extension
		''' </summary>
		<DataMember> _
		Public A, B, Plus, Home, Minus, Up, Down, Left, Right, X, Y, ZL, ZR As Boolean
		''' <summary>
		''' Analog trigger - false if released, true for any pressure applied
		''' </summary>
		<DataMember> _
		Public TriggerL, TriggerR As Boolean
	End Structure

	''' <summary>
	''' Current state of the Classic Controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure ClassicControllerState
		''' <summary>
		''' Calibration data for Classic Controller extension
		''' </summary>
		<DataMember> _
		Public CalibrationInfo As ClassicControllerCalibrationInfo
		''' <summary>
		''' Current button state
		''' </summary>
		<DataMember> _
		Public ButtonState As ClassicControllerButtonState
		''' <summary>
		''' Raw value of left joystick.  Values range between 0 - 255.
		''' </summary>
		<DataMember> _
		Public RawJoystickL As Point
		''' <summary>
		''' Raw value of right joystick.  Values range between 0 - 255.
		''' </summary>
		<DataMember> _
		Public RawJoystickR As Point
		''' <summary>
		''' Normalized value of left joystick.  Values range between -0.5 - 0.5
		''' </summary>
		<DataMember> _
		Public JoystickL As PointF
		''' <summary>
		''' Normalized value of right joystick.  Values range between -0.5 - 0.5
		''' </summary>
		<DataMember> _
		Public JoystickR As PointF
		''' <summary>
		''' Raw value of analog trigger.  Values range between 0 - 255.
		''' </summary>
		<DataMember> _
		Public RawTriggerL, RawTriggerR As Byte
		''' <summary>
		''' Normalized value of analog trigger.  Values range between 0.0 - 1.0.
		''' </summary>
		<DataMember> _
		Public TriggerL, TriggerR As Single
	End Structure

	''' <summary>
	''' Current state of the Guitar controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure GuitarState
		''' <summary>
		''' Guitar type
		''' </summary>
		<DataMember> _
		Public GuitarType As GuitarType
		''' <summary>
		''' Current button state of the Guitar
		''' </summary>
		<DataMember> _
		Public ButtonState As GuitarButtonState
		''' <summary>
		''' Current fret button state of the Guitar
		''' </summary>
		<DataMember> _
		Public FretButtonState As GuitarFretButtonState
		''' <summary>
		''' Current touchbar state of the Guitar
		''' </summary>
		<DataMember> _
		Public TouchbarState As GuitarFretButtonState
		''' <summary>
		''' Raw joystick position.  Values range between 0 - 63.
		''' </summary>
		<DataMember> _
		Public RawJoystick As Point
		''' <summary>
		''' Normalized value of joystick position.  Values range between 0.0 - 1.0.
		''' </summary>
		<DataMember> _
		Public Joystick As PointF
		''' <summary>
		''' Raw whammy bar position.  Values range between 0 - 10.
		''' </summary>
		<DataMember> _
		Public RawWhammyBar As Byte
		''' <summary>
		''' Normalized value of whammy bar position.  Values range between 0.0 - 1.0.
		''' </summary>
		<DataMember> _
		Public WhammyBar As Single
	End Structure

	''' <summary>
	''' Current fret button state of the Guitar controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure GuitarFretButtonState
		''' <summary>
		''' Fret buttons
		''' </summary>
		<DataMember> _
		Public Green, Red, Yellow, Blue, Orange As Boolean
	End Structure


	''' <summary>
	''' Current button state of the Guitar controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure GuitarButtonState
		''' <summary>
		''' Strum bar
		''' </summary>
		<DataMember> _
		Public StrumUp, StrumDown As Boolean
		''' <summary>
		''' Other buttons
		''' </summary>
		<DataMember> _
		Public Minus, Plus As Boolean
	End Structure

	''' <summary>
	''' Current state of the Drums controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure DrumsState
		''' <summary>
		''' Drum pads
		''' </summary>
		Public Red, Green, Blue, Orange, Yellow, Pedal As Boolean
		''' <summary>
		''' Speed at which the pad is hit.  Values range from 0 (very hard) to 6 (very soft)
		''' </summary>
		Public RedVelocity, GreenVelocity, BlueVelocity, OrangeVelocity, YellowVelocity, PedalVelocity As Integer
		''' <summary>
		''' Other buttons
		''' </summary>
		Public Plus, Minus As Boolean
		''' <summary>
		''' Raw value of analong joystick.  Values range from 0 - 15
		''' </summary>
		Public RawJoystick As Point
		''' <summary>
		''' Normalized value of analog joystick.  Values range from 0.0 - 1.0
		''' </summary>
		Public Joystick As PointF
	End Structure

	''' <summary>
	''' Current state of the Wii Fit Balance Board controller
	''' </summary>
	<Serializable, DataContract> _
	Public Structure BalanceBoardState
		''' <summary>
		''' Calibration information for the Balance Board
		''' </summary>
		<DataMember> _
		Public CalibrationInfo As BalanceBoardCalibrationInfo
		''' <summary>
		''' Raw values of each sensor
		''' </summary>
		<DataMember> _
		Public SensorValuesRaw As BalanceBoardSensors
		''' <summary>
		''' Kilograms per sensor
		''' </summary>
		<DataMember> _
		Public SensorValuesKg As BalanceBoardSensorsF
		''' <summary>
		''' Pounds per sensor
		''' </summary>
		<DataMember> _
		Public SensorValuesLb As BalanceBoardSensorsF
		''' <summary>
		''' Total kilograms on the Balance Board
		''' </summary>
		<DataMember> _
		Public WeightKg As Single
		''' <summary>
		''' Total pounds on the Balance Board
		''' </summary>
		<DataMember> _
		Public WeightLb As Single
		''' <summary>
		''' Center of gravity of Balance Board user
		''' </summary>
		<DataMember> _
		Public CenterOfGravity As PointF
	End Structure

	''' <summary>
	''' Calibration information
	''' </summary>
	<Serializable, DataContract> _
	Public Structure BalanceBoardCalibrationInfo
		''' <summary>
		''' Calibration information at 0kg
		''' </summary>
		<DataMember> _
		Public Kg0 As BalanceBoardSensors
		''' <summary>
		''' Calibration information at 17kg
		''' </summary>
		<DataMember> _
		Public Kg17 As BalanceBoardSensors
		''' <summary>
		''' Calibration information at 34kg
		''' </summary>
		<DataMember> _
		Public Kg34 As BalanceBoardSensors
	End Structure

	''' <summary>
	''' The 4 sensors on the Balance Board (short values)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure BalanceBoardSensors
		''' <summary>
		''' Sensor at top right
		''' </summary>
		<DataMember> _
		Public TopRight As Short
		''' <summary>
		''' Sensor at top left
		''' </summary>
		<DataMember> _
		Public TopLeft As Short
		''' <summary>
		''' Sensor at bottom right
		''' </summary>
		<DataMember> _
		Public BottomRight As Short
		''' <summary>
		''' Sensor at bottom left
		''' </summary>
		<DataMember> _
		Public BottomLeft As Short
	End Structure

	''' <summary>
	''' The 4 sensors on the Balance Board (float values)
	''' </summary>
	<Serializable, DataContract> _
	Public Structure BalanceBoardSensorsF
		''' <summary>
		''' Sensor at top right
		''' </summary>
		<DataMember> _
		Public TopRight As Single
		''' <summary>
		''' Sensor at top left
		''' </summary>
		<DataMember> _
		Public TopLeft As Single
		''' <summary>
		''' Sensor at bottom right
		''' </summary>
		<DataMember> _
		Public BottomRight As Single
		''' <summary>
		''' Sensor at bottom left
		''' </summary>
		<DataMember> _
		Public BottomLeft As Single
	End Structure

	''' <summary>
	''' Current state of a single IR sensor
	''' </summary>
	<Serializable, DataContract> _
	Public Structure IRSensor
		''' <summary>
		''' Raw values of individual sensor.  Values range between 0 - 1023 on the X axis and 0 - 767 on the Y axis.
		''' </summary>
		<DataMember> _
		Public RawPosition As Point
		''' <summary>
		''' Normalized values of the sensor position.  Values range between 0.0 - 1.0.
		''' </summary>
		<DataMember> _
		Public Position As PointF
		''' <summary>
		''' Size of IR Sensor.  Values range from 0 - 15
		''' </summary>
		<DataMember> _
		Public Size As Integer
		''' <summary>
		''' IR sensor seen
		''' </summary>
		<DataMember> _
		Public Found As Boolean
		''' <summary>
		''' Convert to human-readable string
		''' </summary>
		''' <returns>A string that represents the point.</returns>
		Public Overrides Function ToString() As String
			Return String.Format("{{{0}, Size={1}, Found={2}}}", Position, Size, Found)
		End Function
	End Structure

	''' <summary>
	''' Current state of the IR camera
	''' </summary>
	<Serializable, DataContract> _
	Public Structure IRState
		''' <summary>
		''' Current mode of IR sensor data
		''' </summary>
		<DataMember> _
		Public Mode As IRMode
		''' <summary>
		''' Current state of IR sensors
		''' </summary>
		<DataMember> _
		Public IRSensors() As IRSensor
		''' <summary>
		''' Raw midpoint of IR sensors 1 and 2 only.  Values range between 0 - 1023, 0 - 767
		''' </summary>
		<DataMember> _
		Public RawMidpoint As Point
		''' <summary>
		''' Normalized midpoint of IR sensors 1 and 2 only.  Values range between 0.0 - 1.0
		''' </summary>
		<DataMember> _
		Public Midpoint As PointF
	End Structure

	''' <summary>
	''' Current state of the accelerometers
	''' </summary>
	<Serializable, DataContract> _
	Public Structure AccelState
		''' <summary>
		''' Raw accelerometer data.
		''' <remarks>Values range between 0 - 255</remarks>
		''' </summary>
		<DataMember> _
		Public RawValues As Point3
		''' <summary>
		''' Normalized accelerometer data.  Values range between 0 - ?, but values > 3 and &lt; -3 are inaccurate.
		''' </summary>
		<DataMember> _
		Public Values As Point3F
	End Structure

	''' <summary>
	''' Accelerometer calibration information
	''' </summary>
	<Serializable, DataContract> _
	Public Structure AccelCalibrationInfo
		''' <summary>
		''' Zero point of accelerometer
		''' </summary>
		<DataMember> _
		Public X0, Y0, Z0 As Byte
		''' <summary>
		''' Gravity at rest of accelerometer
		''' </summary>
		<DataMember> _
		Public XG, YG, ZG As Byte
	End Structure

	''' <summary>
	''' Current button state
	''' </summary>
	<Serializable, DataContract> _
	Public Structure ButtonState
		''' <summary>
		''' Digital button on the Wiimote
		''' </summary>
		<DataMember> _
		Public A, B, Plus, Home, Minus, One, Two, Up, Down, Left, Right As Boolean
	End Structure

	''' <summary>
	''' The extension plugged into the Wiimote
	''' </summary>
	<DataContract> _
	Public Enum ExtensionType As Long
		''' <summary>
		''' No extension
		''' </summary>
		None = &H000000000000
		''' <summary>
		''' Nunchuk extension
		''' </summary>
		Nunchuk = &H0000a4200000
		''' <summary>
		''' Classic Controller extension
		''' </summary>
		ClassicController = &H0000a4200101
		''' <summary>
		''' Guitar controller from Guitar Hero 3/WorldTour
		''' </summary>
		Guitar = &H0000a4200103
		''' <summary>
		''' Drum controller from Guitar Hero: World Tour
		''' </summary>
		Drums = &H0100a4200103
		''' <summary>
		''' Wii Fit Balance Board controller
		''' </summary>
		BalanceBoard = &H0000a4200402
		''' <summary>
		''' Partially inserted extension.  This is an error condition.
		''' </summary>
		ParitallyInserted = &Hffffffffffff
	End Enum

	''' <summary>
	''' The mode of data reported for the IR sensor
	''' </summary>
	<DataContract> _
	Public Enum IRMode As Byte
		''' <summary>
		''' IR sensor off
		''' </summary>
		[Off] = &H00
		''' <summary>
		''' Basic mode
		''' </summary>
		Basic = &H01 ' 10 bytes
		''' <summary>
		''' Extended mode
		''' </summary>
		Extended = &H03 ' 12 bytes
		''' <summary>
		''' Full mode (unsupported)
		''' </summary>
		Full = &H05 ' 16 bytes * 2 (format unknown)
	End Enum

	''' <summary>
	''' The report format in which the Wiimote should return data
	''' </summary>
	Public Enum InputReport As Byte
		''' <summary>
		''' Status report
		''' </summary>
		Status = &H20
		''' <summary>
		''' Read data from memory location
		''' </summary>
		ReadData = &H21
		''' <summary>
		''' Register write complete
		''' </summary>
		OutputReportAck = &H22
		''' <summary>
		''' Button data only
		''' </summary>
		Buttons = &H30
		''' <summary>
		''' Button and accelerometer data
		''' </summary>
		ButtonsAccel = &H31
		''' <summary>
		''' IR sensor and accelerometer data
		''' </summary>
		IRAccel = &H33
		''' <summary>
		''' Button and extension controller data
		''' </summary>
		ButtonsExtension = &H34
		''' <summary>
		''' Extension and accelerometer data
		''' </summary>
		ExtensionAccel = &H35
		''' <summary>
		''' IR sensor, extension controller and accelerometer data
		''' </summary>
		IRExtensionAccel = &H37
	End Enum

	''' <summary>
	''' Sensitivity of the IR camera on the Wiimote
	''' </summary>

	Public Enum IRSensitivity
		''' <summary>
		''' Equivalent to level 1 on the Wii console
		''' </summary>
		WiiLevel1
		''' <summary>
		''' Equivalent to level 2 on the Wii console
		''' </summary>
		WiiLevel2
		''' <summary>
		''' Equivalent to level 3 on the Wii console (default)
		''' </summary>
		WiiLevel3
		''' <summary>
		''' Equivalent to level 4 on the Wii console
		''' </summary>
		WiiLevel4
		''' <summary>
		''' Equivalent to level 5 on the Wii console
		''' </summary>
		WiiLevel5
		''' <summary>
		''' Maximum sensitivity
		''' </summary>
		Maximum
	End Enum

	''' <summary>
	''' Type of guitar extension: Guitar Hero 3 or Guitar Hero World Tour
	''' </summary>
	Public Enum GuitarType
		''' <summary>
		'''  Guitar Hero 3 guitar controller
		''' </summary>
		GuitarHero3
		''' <summary>
		''' Guitar Hero: World Tour guitar controller
		''' </summary>
		GuitarHeroWorldTour
	End Enum
End Namespace
