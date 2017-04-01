Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports WiimoteLib

Namespace WiimoteTest
	Partial Public Class MultipleWiimoteForm
		Inherits Form
		' map a wiimote to a specific state user control dealie
		Private mWiimoteMap As Dictionary(Of Guid,WiimoteInfo) = New Dictionary(Of Guid,WiimoteInfo)()
		Private mWC As WiimoteCollection

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub MultipleWiimoteForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' find all wiimotes connected to the system
			mWC = New WiimoteCollection()
			Dim index As Integer = 1

			Try
				mWC.FindAllWiimotes()
			Catch ex As WiimoteNotFoundException
				MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Catch ex As WiimoteException
				MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Catch ex As Exception
				MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try

			For Each wm As Wiimote In mWC
				' create a new tab
				Dim tp As New TabPage("Wiimote " & index)
				tabWiimotes.TabPages.Add(tp)

				' create a new user control
				Dim wi As New WiimoteInfo(wm)
				tp.Controls.Add(wi)

				' setup the map from this wiimote's ID to that control
				mWiimoteMap(wm.ID) = wi

				' connect it and set it up as always
				AddHandler wm.WiimoteChanged, AddressOf wm_WiimoteChanged
				AddHandler wm.WiimoteExtensionChanged, AddressOf wm_WiimoteExtensionChanged

				wm.Connect()
				If wm.WiimoteState.ExtensionType <> ExtensionType.BalanceBoard Then
					wm.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, True)
				End If

				wm.SetLEDs(index)
				index += 1
			Next wm
		End Sub

		Private Sub wm_WiimoteChanged(ByVal sender As Object, ByVal e As WiimoteChangedEventArgs)
			Dim wi As WiimoteInfo = mWiimoteMap((CType(sender, Wiimote)).ID)
			wi.UpdateState(e)
		End Sub

		Private Sub wm_WiimoteExtensionChanged(ByVal sender As Object, ByVal e As WiimoteExtensionChangedEventArgs)
			' find the control for this Wiimote
			Dim wi As WiimoteInfo = mWiimoteMap((CType(sender, Wiimote)).ID)
			wi.UpdateExtension(e)

			If e.Inserted Then
				CType(sender, Wiimote).SetReportType(InputReport.IRExtensionAccel, True)
			Else
				CType(sender, Wiimote).SetReportType(InputReport.IRAccel, True)
			End If
		End Sub

		Private Sub MultipleWiimoteForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			For Each wm As Wiimote In mWC
				wm.Disconnect()
			Next wm
		End Sub
	End Class
End Namespace
