'////////////////////////////////////////////////////////////////////////////////
'	MultipleWiimoteForm.cs
'	Managed Wiimote Library Tester
'	Written by Brian Peek (http://www.brianpeek.com/)
'  for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'  and http://www.codeplex.com/WiimoteLib
'  for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports WiimoteLib

Namespace WiimoteTest
	Partial Public Class WiimoteInfo
		Inherits UserControl
		Private Delegate Sub UpdateWiimoteStateDelegate(ByVal args As WiimoteChangedEventArgs)
		Private Delegate Sub UpdateExtensionChangedDelegate(ByVal args As WiimoteExtensionChangedEventArgs)

		Private b As New Bitmap(256, 192, PixelFormat.Format24bppRgb)
		Private g As Graphics
		Private mWiimote As Wiimote

		Public Sub New()
			InitializeComponent()
			g = Graphics.FromImage(b)
		End Sub

		Public Sub New(ByVal wm As Wiimote)
			Me.New()
			mWiimote = wm
		End Sub

		Public Sub UpdateState(ByVal args As WiimoteChangedEventArgs)
			BeginInvoke(New UpdateWiimoteStateDelegate(AddressOf UpdateWiimoteChanged), args)
		End Sub

		Public Sub UpdateExtension(ByVal args As WiimoteExtensionChangedEventArgs)
			BeginInvoke(New UpdateExtensionChangedDelegate(AddressOf UpdateExtensionChanged), args)
		End Sub

		Private Sub chkLED_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLED2.CheckedChanged, chkLED4.CheckedChanged, chkLED3.CheckedChanged, chkLED1.CheckedChanged
			mWiimote.SetLEDs(chkLED1.Checked, chkLED2.Checked, chkLED3.Checked, chkLED4.Checked)
		End Sub

		Private Sub chkRumble_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRumble.CheckedChanged
			mWiimote.SetRumble(chkRumble.Checked)
		End Sub

		Private Sub UpdateWiimoteChanged(ByVal args As WiimoteChangedEventArgs)
			Dim ws As WiimoteState = args.WiimoteState

			clbButtons.SetItemChecked(0, ws.ButtonState.A)
			clbButtons.SetItemChecked(1, ws.ButtonState.B)
			clbButtons.SetItemChecked(2, ws.ButtonState.Minus)
			clbButtons.SetItemChecked(3, ws.ButtonState.Home)
			clbButtons.SetItemChecked(4, ws.ButtonState.Plus)
			clbButtons.SetItemChecked(5, ws.ButtonState.One)
			clbButtons.SetItemChecked(6, ws.ButtonState.Two)
			clbButtons.SetItemChecked(7, ws.ButtonState.Up)
			clbButtons.SetItemChecked(8, ws.ButtonState.Down)
			clbButtons.SetItemChecked(9, ws.ButtonState.Left)
			clbButtons.SetItemChecked(10, ws.ButtonState.Right)

			lblAccel.Text = ws.AccelState.Values.ToString()

			chkLED1.Checked = ws.LEDState.LED1
			chkLED2.Checked = ws.LEDState.LED2
			chkLED3.Checked = ws.LEDState.LED3
			chkLED4.Checked = ws.LEDState.LED4

			Select Case ws.ExtensionType
				Case ExtensionType.Nunchuk
					lblChuk.Text = ws.NunchukState.AccelState.Values.ToString()
					lblChukJoy.Text = ws.NunchukState.Joystick.ToString()
					chkC.Checked = ws.NunchukState.C
					chkZ.Checked = ws.NunchukState.Z

				Case ExtensionType.ClassicController
					clbCCButtons.SetItemChecked(0, ws.ClassicControllerState.ButtonState.A)
					clbCCButtons.SetItemChecked(1, ws.ClassicControllerState.ButtonState.B)
					clbCCButtons.SetItemChecked(2, ws.ClassicControllerState.ButtonState.X)
					clbCCButtons.SetItemChecked(3, ws.ClassicControllerState.ButtonState.Y)
					clbCCButtons.SetItemChecked(4, ws.ClassicControllerState.ButtonState.Minus)
					clbCCButtons.SetItemChecked(5, ws.ClassicControllerState.ButtonState.Home)
					clbCCButtons.SetItemChecked(6, ws.ClassicControllerState.ButtonState.Plus)
					clbCCButtons.SetItemChecked(7, ws.ClassicControllerState.ButtonState.Up)
					clbCCButtons.SetItemChecked(8, ws.ClassicControllerState.ButtonState.Down)
					clbCCButtons.SetItemChecked(9, ws.ClassicControllerState.ButtonState.Left)
					clbCCButtons.SetItemChecked(10, ws.ClassicControllerState.ButtonState.Right)
					clbCCButtons.SetItemChecked(11, ws.ClassicControllerState.ButtonState.ZL)
					clbCCButtons.SetItemChecked(12, ws.ClassicControllerState.ButtonState.ZR)
					clbCCButtons.SetItemChecked(13, ws.ClassicControllerState.ButtonState.TriggerL)
					clbCCButtons.SetItemChecked(14, ws.ClassicControllerState.ButtonState.TriggerR)

					lblCCJoy1.Text = ws.ClassicControllerState.JoystickL.ToString()
					lblCCJoy2.Text = ws.ClassicControllerState.JoystickR.ToString()

					lblTriggerL.Text = ws.ClassicControllerState.TriggerL.ToString()
					lblTriggerR.Text = ws.ClassicControllerState.TriggerR.ToString()

				Case ExtensionType.Guitar
					clbGuitarButtons.SetItemChecked(0, ws.GuitarState.FretButtonState.Green)
					clbGuitarButtons.SetItemChecked(1, ws.GuitarState.FretButtonState.Red)
					clbGuitarButtons.SetItemChecked(2, ws.GuitarState.FretButtonState.Yellow)
					clbGuitarButtons.SetItemChecked(3, ws.GuitarState.FretButtonState.Blue)
					clbGuitarButtons.SetItemChecked(4, ws.GuitarState.FretButtonState.Orange)
					clbGuitarButtons.SetItemChecked(5, ws.GuitarState.ButtonState.Minus)
					clbGuitarButtons.SetItemChecked(6, ws.GuitarState.ButtonState.Plus)
					clbGuitarButtons.SetItemChecked(7, ws.GuitarState.ButtonState.StrumUp)
					clbGuitarButtons.SetItemChecked(8, ws.GuitarState.ButtonState.StrumDown)

					clbTouchbar.SetItemChecked(0, ws.GuitarState.TouchbarState.Green)
					clbTouchbar.SetItemChecked(1, ws.GuitarState.TouchbarState.Red)
					clbTouchbar.SetItemChecked(2, ws.GuitarState.TouchbarState.Yellow)
					clbTouchbar.SetItemChecked(3, ws.GuitarState.TouchbarState.Blue)
					clbTouchbar.SetItemChecked(4, ws.GuitarState.TouchbarState.Orange)

					lblGuitarJoy.Text = ws.GuitarState.Joystick.ToString()
					lblGuitarWhammy.Text = ws.GuitarState.WhammyBar.ToString()
					lblGuitarType.Text = ws.GuitarState.GuitarType.ToString()

				Case ExtensionType.Drums
					clbDrums.SetItemChecked(0, ws.DrumsState.Red)
					clbDrums.SetItemChecked(1, ws.DrumsState.Blue)
					clbDrums.SetItemChecked(2, ws.DrumsState.Green)
					clbDrums.SetItemChecked(3, ws.DrumsState.Yellow)
					clbDrums.SetItemChecked(4, ws.DrumsState.Orange)
					clbDrums.SetItemChecked(5, ws.DrumsState.Pedal)
					clbDrums.SetItemChecked(6, ws.DrumsState.Minus)
					clbDrums.SetItemChecked(7, ws.DrumsState.Plus)

					lbDrumVelocity.Items.Clear()
					lbDrumVelocity.Items.Add(ws.DrumsState.RedVelocity)
					lbDrumVelocity.Items.Add(ws.DrumsState.BlueVelocity)
					lbDrumVelocity.Items.Add(ws.DrumsState.GreenVelocity)
					lbDrumVelocity.Items.Add(ws.DrumsState.YellowVelocity)
					lbDrumVelocity.Items.Add(ws.DrumsState.OrangeVelocity)
					lbDrumVelocity.Items.Add(ws.DrumsState.PedalVelocity)

					lblDrumJoy.Text = ws.DrumsState.Joystick.ToString()

				Case ExtensionType.BalanceBoard
					If chkLbs.Checked Then
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesLb.TopLeft.ToString()
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesLb.TopRight.ToString()
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesLb.BottomLeft.ToString()
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesLb.BottomRight.ToString()
						lblBBTotal.Text = ws.BalanceBoardState.WeightLb.ToString()
					Else
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesKg.TopLeft.ToString()
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesKg.TopRight.ToString()
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesKg.BottomLeft.ToString()
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesKg.BottomRight.ToString()
						lblBBTotal.Text = ws.BalanceBoardState.WeightKg.ToString()
					End If
					lblCOG.Text = ws.BalanceBoardState.CenterOfGravity.ToString()
			End Select

			g.Clear(Color.Black)

			UpdateIR(ws.IRState.IRSensors(0), lblIR1, lblIR1Raw, chkFound1, Color.Red)
			UpdateIR(ws.IRState.IRSensors(1), lblIR2, lblIR2Raw, chkFound2, Color.Blue)
			UpdateIR(ws.IRState.IRSensors(2), lblIR3, lblIR3Raw, chkFound3, Color.Yellow)
			UpdateIR(ws.IRState.IRSensors(3), lblIR4, lblIR4Raw, chkFound4, Color.Orange)

			If ws.IRState.IRSensors(0).Found AndAlso ws.IRState.IRSensors(1).Found Then
				g.DrawEllipse(New Pen(Color.Green), CInt(Fix(ws.IRState.RawMidpoint.X / 4)), CInt(Fix(ws.IRState.RawMidpoint.Y / 4)), 2, 2)
			End If

			pbIR.Image = b

			If ws.Battery > &Hc8 Then
				pbBattery.Value = (&Hc8)
			Else
				pbBattery.Value = (CInt(Fix(ws.Battery)))
			End If
			lblBattery.Text = ws.Battery.ToString()
			lblDevicePath.Text = "Device Path: " & mWiimote.HIDDevicePath
		End Sub

		Private Sub UpdateIR(ByVal irSensor As IRSensor, ByVal lblNorm As Label, ByVal lblRaw As Label, ByVal chkFound As CheckBox, ByVal color As Color)
			chkFound.Checked = irSensor.Found

			If irSensor.Found Then
				lblNorm.Text = irSensor.Position.ToString() & ", " & irSensor.Size
				lblRaw.Text = irSensor.RawPosition.ToString()
				g.DrawEllipse(New Pen(color), CInt(Fix(irSensor.RawPosition.X / 4)), CInt(Fix(irSensor.RawPosition.Y / 4)), irSensor.Size+1, irSensor.Size+1)
			End If
		End Sub

		Private Sub UpdateExtensionChanged(ByVal args As WiimoteExtensionChangedEventArgs)
			chkExtension.Text = args.ExtensionType.ToString()
			chkExtension.Checked = args.Inserted
		End Sub

		Public WriteOnly Property Wiimote() As Wiimote
			Set(ByVal value As Wiimote)
				mWiimote = value
			End Set
		End Property
	End Class
End Namespace
