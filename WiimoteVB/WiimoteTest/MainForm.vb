'////////////////////////////////////////////////////////////////////////////////
'	Form1.cs
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
Imports System.IO
Imports System.Windows.Forms
Imports WiimoteLib

Namespace WiimoteTest
	Partial Public Class MainForm
		Inherits Form
		Private Delegate Sub UpdateWiimoteStateDelegate(ByVal args As WiimoteChangedEventArgs)
		Private Delegate Sub UpdateExtensionChangedDelegate(ByVal args As WiimoteExtensionChangedEventArgs)

		Private wm As New Wiimote()
		Private b As New Bitmap(256, 192, PixelFormat.Format24bppRgb)
		Private g As Graphics

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			AddHandler wm.WiimoteChanged, AddressOf wm_WiimoteChanged
			AddHandler wm.WiimoteExtensionChanged, AddressOf wm_WiimoteExtensionChanged

			g = Graphics.FromImage(b)
			wm.Connect()
			wm.SetReportType(InputReport.IRAccel, True)
			wm.SetLEDs(False, True, True, False)
		End Sub

		Private Sub UpdateExtensionChanged(ByVal args As WiimoteExtensionChangedEventArgs)
			chkExtension.Text = args.ExtensionType.ToString()
			chkExtension.Checked = args.Inserted

			If args.Inserted Then
				wm.SetReportType(InputReport.IRExtensionAccel, True)
			Else
				wm.SetReportType(InputReport.IRAccel, True)
			End If
		End Sub

		Private Sub UpdateWiimoteState(ByVal args As WiimoteChangedEventArgs)
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
					clbGuitarButtons.SetItemChecked(0, ws.GuitarState.ButtonState.Green)
					clbGuitarButtons.SetItemChecked(1, ws.GuitarState.ButtonState.Red)
					clbGuitarButtons.SetItemChecked(2, ws.GuitarState.ButtonState.Yellow)
					clbGuitarButtons.SetItemChecked(3, ws.GuitarState.ButtonState.Blue)
					clbGuitarButtons.SetItemChecked(4, ws.GuitarState.ButtonState.Orange)
					clbGuitarButtons.SetItemChecked(5, ws.GuitarState.ButtonState.Minus)
					clbGuitarButtons.SetItemChecked(6, ws.GuitarState.ButtonState.Plus)
					clbGuitarButtons.SetItemChecked(7, ws.GuitarState.ButtonState.StrumUp)
					clbGuitarButtons.SetItemChecked(8, ws.GuitarState.ButtonState.StrumDown)

					lblGuitarJoy.Text = ws.GuitarState.Joystick.ToString()
					lblGuitarWhammy.Text = ws.GuitarState.WhammyBar.ToString()
			End Select

			g.Clear(Color.Black)

			If ws.IRState.IRSensors(0).Found Then
				lblIR1.Text = ws.IRState.IRSensors(0).Position.ToString() & ", " & ws.IRState.IRSensors(0).Size
				lblIR1Raw.Text = ws.IRState.IRSensors(0).RawPosition.ToString()
				g.DrawEllipse(New Pen(Color.Red), CInt(Fix(ws.IRState.IRSensors(0).RawPosition.X / 4)), CInt(Fix(ws.IRState.IRSensors(0).RawPosition.Y / 4)), ws.IRState.IRSensors(0).Size+1, ws.IRState.IRSensors(0).Size+1)
			End If
			If ws.IRState.IRSensors(1).Found Then
				lblIR2.Text = ws.IRState.IRSensors(1).Position.ToString() & ", " & ws.IRState.IRSensors(1).Size
				lblIR2Raw.Text = ws.IRState.IRSensors(1).RawPosition.ToString()
				g.DrawEllipse(New Pen(Color.Blue), CInt(Fix(ws.IRState.IRSensors(1).RawPosition.X / 4)), CInt(Fix(ws.IRState.IRSensors(1).RawPosition.Y / 4)), ws.IRState.IRSensors(1).Size+1, ws.IRState.IRSensors(1).Size+1)
			End If
			If ws.IRState.IRSensors(2).Found Then
				lblIR3.Text = ws.IRState.IRSensors(2).Position.ToString() & ", " & ws.IRState.IRSensors(2).Size
				lblIR3Raw.Text = ws.IRState.IRSensors(2).RawPosition.ToString()
				g.DrawEllipse(New Pen(Color.Yellow), CInt(Fix(ws.IRState.IRSensors(2).RawPosition.X / 4)), CInt(Fix(ws.IRState.IRSensors(2).RawPosition.Y / 4)), ws.IRState.IRSensors(2).Size+1, ws.IRState.IRSensors(2).Size+1)
			End If
			If ws.IRState.IRSensors(3).Found Then
				lblIR4.Text = ws.IRState.IRSensors(3).Position.ToString() & ", " & ws.IRState.IRSensors(3).Size
				lblIR4Raw.Text = ws.IRState.IRSensors(3).RawPosition.ToString()
				g.DrawEllipse(New Pen(Color.Orange), CInt(Fix(ws.IRState.IRSensors(3).RawPosition.X / 4)), CInt(Fix(ws.IRState.IRSensors(3).RawPosition.Y / 4)), ws.IRState.IRSensors(3).Size+1, ws.IRState.IRSensors(3).Size+1)
			End If

			If ws.IRState.IRSensors(0).Found AndAlso ws.IRState.IRSensors(1).Found Then
				g.DrawEllipse(New Pen(Color.Green), CInt(Fix(ws.IRState.RawMidpoint.X / 4)), CInt(Fix(ws.IRState.RawMidpoint.Y / 4)), 2, 2)
			End If

			pbIR.Image = b

			chkFound1.Checked = ws.IRState.IRSensors(0).Found
			chkFound2.Checked = ws.IRState.IRSensors(1).Found
			chkFound3.Checked = ws.IRState.IRSensors(2).Found
			chkFound4.Checked = ws.IRState.IRSensors(3).Found

			If ws.Battery > &Hc8 Then
				pbBattery.Value = (&Hc8)
			Else
				pbBattery.Value = (CInt(Fix(ws.Battery)))
			End If
			Dim f As Single = (((100.0f * 48.0f * CSng(ws.Battery / 48.0f))) / 192.0f)
			lblBattery.Text = f.ToString("F")
		End Sub

		Private Sub wm_WiimoteChanged(ByVal sender As Object, ByVal args As WiimoteChangedEventArgs)
			BeginInvoke(New UpdateWiimoteStateDelegate(AddressOf UpdateWiimoteState), args)
		End Sub

		Private Sub wm_WiimoteExtensionChanged(ByVal sender As Object, ByVal args As WiimoteExtensionChangedEventArgs)
			BeginInvoke(New UpdateExtensionChangedDelegate(AddressOf UpdateExtensionChanged), args)
		End Sub

		Private Sub chkLED1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLED4.CheckedChanged, chkLED3.CheckedChanged, chkLED2.CheckedChanged, chkLED1.CheckedChanged
			wm.SetLEDs(chkLED1.Checked, chkLED2.Checked, chkLED3.Checked, chkLED4.Checked)
		End Sub

		Private Sub chkRumble_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRumble.CheckedChanged
			wm.SetRumble(chkRumble.Checked)
		End Sub

		Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			wm.Disconnect()
		End Sub
	End Class
End Namespace
