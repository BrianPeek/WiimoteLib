Imports Microsoft.VisualBasic
Imports System
Namespace WiimoteTest
	Partial Public Class MultipleWiimoteForm
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
			Me.tabWiimotes = New System.Windows.Forms.TabControl()
			Me.SuspendLayout()
			' 
			' tabWiimotes
			' 
			Me.tabWiimotes.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tabWiimotes.Location = New System.Drawing.Point(0, 0)
			Me.tabWiimotes.Name = "tabWiimotes"
			Me.tabWiimotes.SelectedIndex = 0
			Me.tabWiimotes.Size = New System.Drawing.Size(710, 484)
			Me.tabWiimotes.TabIndex = 0
			' 
			' MultipleWiimoteForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(710, 484)
			Me.Controls.Add(Me.tabWiimotes)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.MaximizeBox = False
			Me.Name = "MultipleWiimoteForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Multiple Wiimote Tester"
'			Me.Load += New System.EventHandler(Me.MultipleWiimoteForm_Load);
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.MultipleWiimoteForm_FormClosing);
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private tabWiimotes As System.Windows.Forms.TabControl
	End Class
End Namespace