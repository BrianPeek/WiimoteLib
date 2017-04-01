'////////////////////////////////////////////////////////////////////////////////
'	SingleWiimoteForm.cs
'	Managed Wiimote Library Tester
'	Written by Brian Peek (http://www.brianpeek.com/)
'  for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'  and http://www.codeplex.com/WiimoteLib
'  for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports WiimoteLib

Namespace WiimoteTest
	Partial Public Class SingleWiimoteForm
		Inherits Form
		Private wm As New Wiimote()

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			wiimoteInfo1.Wiimote = wm

			AddHandler wm.WiimoteChanged, AddressOf wm_WiimoteChanged
			AddHandler wm.WiimoteExtensionChanged, AddressOf wm_WiimoteExtensionChanged
			wm.Connect()
			wm.SetReportType(InputReport.IRAccel, True)
			wm.SetLEDs(False, True, True, False)
		End Sub

		Private Sub wm_WiimoteChanged(ByVal sender As Object, ByVal args As WiimoteChangedEventArgs)
			wiimoteInfo1.UpdateState(args)
		End Sub

		Private Sub wm_WiimoteExtensionChanged(ByVal sender As Object, ByVal args As WiimoteExtensionChangedEventArgs)
			wiimoteInfo1.UpdateExtension(args)

			If args.Inserted Then
				wm.SetReportType(InputReport.IRExtensionAccel, True)
			Else
				wm.SetReportType(InputReport.IRAccel, True)
			End If
		End Sub

		Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			wm.Disconnect()
		End Sub
	End Class
End Namespace
