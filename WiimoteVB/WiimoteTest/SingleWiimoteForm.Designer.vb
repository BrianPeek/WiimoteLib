Imports Microsoft.VisualBasic
Imports System
Namespace WiimoteTest
	Partial Public Class SingleWiimoteForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.wiimoteInfo1 = New WiimoteTest.WiimoteInfo()
			Me.SuspendLayout()
			' 
			' wiimoteInfo1
			' 
			Me.wiimoteInfo1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.wiimoteInfo1.Location = New System.Drawing.Point(0, 0)
			Me.wiimoteInfo1.Name = "wiimoteInfo1"
			Me.wiimoteInfo1.Size = New System.Drawing.Size(698, 453)
			Me.wiimoteInfo1.TabIndex = 0
			' 
			' SingleWiimoteForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(698, 453)
			Me.Controls.Add(Me.wiimoteInfo1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.MaximizeBox = False
			Me.Name = "SingleWiimoteForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Wiimote Tester"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.Form1_FormClosing);
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private wiimoteInfo1 As WiimoteInfo

	End Class
End Namespace

