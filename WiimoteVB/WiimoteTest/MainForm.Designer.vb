Imports Microsoft.VisualBasic
Imports System
Namespace WiimoteTest
	Partial Public Class MainForm
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
			Me.clbButtons = New System.Windows.Forms.CheckedListBox()
			Me.lblAccel = New System.Windows.Forms.Label()
			Me.chkLED4 = New System.Windows.Forms.CheckBox()
			Me.chkLED3 = New System.Windows.Forms.CheckBox()
			Me.chkLED2 = New System.Windows.Forms.CheckBox()
			Me.chkLED1 = New System.Windows.Forms.CheckBox()
			Me.chkRumble = New System.Windows.Forms.CheckBox()
			Me.pbBattery = New System.Windows.Forms.ProgressBar()
			Me.lblIR1 = New System.Windows.Forms.Label()
			Me.lblIR2 = New System.Windows.Forms.Label()
			Me.chkFound1 = New System.Windows.Forms.CheckBox()
			Me.chkFound2 = New System.Windows.Forms.CheckBox()
			Me.lblBattery = New System.Windows.Forms.Label()
			Me.pbIR = New System.Windows.Forms.PictureBox()
			Me.chkExtension = New System.Windows.Forms.CheckBox()
			Me.lblChuk = New System.Windows.Forms.Label()
			Me.lblChukJoy = New System.Windows.Forms.Label()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.lblIR3Raw = New System.Windows.Forms.Label()
			Me.lblIR1Raw = New System.Windows.Forms.Label()
			Me.lblIR4Raw = New System.Windows.Forms.Label()
			Me.lblIR2Raw = New System.Windows.Forms.Label()
			Me.lblIR3 = New System.Windows.Forms.Label()
			Me.lblIR4 = New System.Windows.Forms.Label()
			Me.chkFound3 = New System.Windows.Forms.CheckBox()
			Me.chkFound4 = New System.Windows.Forms.CheckBox()
			Me.clbCCButtons = New System.Windows.Forms.CheckedListBox()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.lblTriggerR = New System.Windows.Forms.Label()
			Me.lblTriggerL = New System.Windows.Forms.Label()
			Me.lblCCJoy2 = New System.Windows.Forms.Label()
			Me.lblCCJoy1 = New System.Windows.Forms.Label()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.lblGuitarWhammy = New System.Windows.Forms.Label()
			Me.lblGuitarJoy = New System.Windows.Forms.Label()
			Me.clbGuitarButtons = New System.Windows.Forms.CheckedListBox()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.chkC = New System.Windows.Forms.CheckBox()
			Me.chkZ = New System.Windows.Forms.CheckBox()
			CType(Me.pbIR, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox7.SuspendLayout()
			Me.groupBox8.SuspendLayout()
			Me.SuspendLayout()
			' 
			' clbButtons
			' 
			Me.clbButtons.FormattingEnabled = True
			Me.clbButtons.Items.AddRange(New Object() { "A", "B", "-", "Home", "+", "1", "2", "Up", "Down", "Left", "Right"})
			Me.clbButtons.Location = New System.Drawing.Point(8, 16)
			Me.clbButtons.Name = "clbButtons"
			Me.clbButtons.Size = New System.Drawing.Size(56, 184)
			Me.clbButtons.TabIndex = 1
			' 
			' lblAccel
			' 
			Me.lblAccel.Location = New System.Drawing.Point(8, 20)
			Me.lblAccel.Name = "lblAccel"
			Me.lblAccel.Size = New System.Drawing.Size(88, 48)
			Me.lblAccel.TabIndex = 2
			Me.lblAccel.Text = "Accel Values"
			' 
			' chkLED4
			' 
			Me.chkLED4.AutoSize = True
			Me.chkLED4.Location = New System.Drawing.Point(8, 76)
			Me.chkLED4.Name = "chkLED4"
			Me.chkLED4.Size = New System.Drawing.Size(53, 17)
			Me.chkLED4.TabIndex = 3
			Me.chkLED4.Text = "LED4"
			Me.chkLED4.UseVisualStyleBackColor = True
'			Me.chkLED4.CheckedChanged += New System.EventHandler(Me.chkLED1_CheckedChanged);
			' 
			' chkLED3
			' 
			Me.chkLED3.AutoSize = True
			Me.chkLED3.Location = New System.Drawing.Point(8, 56)
			Me.chkLED3.Name = "chkLED3"
			Me.chkLED3.Size = New System.Drawing.Size(53, 17)
			Me.chkLED3.TabIndex = 3
			Me.chkLED3.Text = "LED3"
			Me.chkLED3.UseVisualStyleBackColor = True
'			Me.chkLED3.CheckedChanged += New System.EventHandler(Me.chkLED1_CheckedChanged);
			' 
			' chkLED2
			' 
			Me.chkLED2.AutoSize = True
			Me.chkLED2.Location = New System.Drawing.Point(8, 36)
			Me.chkLED2.Name = "chkLED2"
			Me.chkLED2.Size = New System.Drawing.Size(53, 17)
			Me.chkLED2.TabIndex = 3
			Me.chkLED2.Text = "LED2"
			Me.chkLED2.UseVisualStyleBackColor = True
'			Me.chkLED2.CheckedChanged += New System.EventHandler(Me.chkLED1_CheckedChanged);
			' 
			' chkLED1
			' 
			Me.chkLED1.AutoSize = True
			Me.chkLED1.Location = New System.Drawing.Point(8, 16)
			Me.chkLED1.Name = "chkLED1"
			Me.chkLED1.Size = New System.Drawing.Size(53, 17)
			Me.chkLED1.TabIndex = 3
			Me.chkLED1.Text = "LED1"
			Me.chkLED1.UseVisualStyleBackColor = True
'			Me.chkLED1.CheckedChanged += New System.EventHandler(Me.chkLED1_CheckedChanged);
			' 
			' chkRumble
			' 
			Me.chkRumble.AutoSize = True
			Me.chkRumble.Location = New System.Drawing.Point(8, 96)
			Me.chkRumble.Name = "chkRumble"
			Me.chkRumble.Size = New System.Drawing.Size(62, 17)
			Me.chkRumble.TabIndex = 4
			Me.chkRumble.Text = "Rumble"
			Me.chkRumble.UseVisualStyleBackColor = True
'			Me.chkRumble.CheckedChanged += New System.EventHandler(Me.chkRumble_CheckedChanged);
			' 
			' pbBattery
			' 
			Me.pbBattery.Location = New System.Drawing.Point(8, 20)
			Me.pbBattery.Maximum = 200
			Me.pbBattery.Name = "pbBattery"
			Me.pbBattery.Size = New System.Drawing.Size(100, 23)
			Me.pbBattery.Step = 1
			Me.pbBattery.TabIndex = 6
			' 
			' lblIR1
			' 
			Me.lblIR1.AutoSize = True
			Me.lblIR1.Location = New System.Drawing.Point(8, 16)
			Me.lblIR1.Name = "lblIR1"
			Me.lblIR1.Size = New System.Drawing.Size(24, 13)
			Me.lblIR1.TabIndex = 7
			Me.lblIR1.Text = "IR1"
			' 
			' lblIR2
			' 
			Me.lblIR2.AutoSize = True
			Me.lblIR2.Location = New System.Drawing.Point(8, 32)
			Me.lblIR2.Name = "lblIR2"
			Me.lblIR2.Size = New System.Drawing.Size(24, 13)
			Me.lblIR2.TabIndex = 7
			Me.lblIR2.Text = "IR2"
			' 
			' chkFound1
			' 
			Me.chkFound1.AutoSize = True
			Me.chkFound1.Location = New System.Drawing.Point(8, 148)
			Me.chkFound1.Name = "chkFound1"
			Me.chkFound1.Size = New System.Drawing.Size(46, 17)
			Me.chkFound1.TabIndex = 8
			Me.chkFound1.Text = "IR 1"
			Me.chkFound1.UseVisualStyleBackColor = True
			' 
			' chkFound2
			' 
			Me.chkFound2.AutoSize = True
			Me.chkFound2.Location = New System.Drawing.Point(8, 164)
			Me.chkFound2.Name = "chkFound2"
			Me.chkFound2.Size = New System.Drawing.Size(46, 17)
			Me.chkFound2.TabIndex = 8
			Me.chkFound2.Text = "IR 2"
			Me.chkFound2.UseVisualStyleBackColor = True
			' 
			' lblBattery
			' 
			Me.lblBattery.AutoSize = True
			Me.lblBattery.Location = New System.Drawing.Point(112, 24)
			Me.lblBattery.Name = "lblBattery"
			Me.lblBattery.Size = New System.Drawing.Size(35, 13)
			Me.lblBattery.TabIndex = 9
			Me.lblBattery.Text = "label1"
			' 
			' pbIR
			' 
			Me.pbIR.Location = New System.Drawing.Point(8, 252)
			Me.pbIR.Name = "pbIR"
			Me.pbIR.Size = New System.Drawing.Size(256, 192)
			Me.pbIR.TabIndex = 10
			Me.pbIR.TabStop = False
			' 
			' chkExtension
			' 
			Me.chkExtension.AutoSize = True
			Me.chkExtension.Location = New System.Drawing.Point(8, 228)
			Me.chkExtension.Name = "chkExtension"
			Me.chkExtension.Size = New System.Drawing.Size(52, 17)
			Me.chkExtension.TabIndex = 12
			Me.chkExtension.Text = "None"
			Me.chkExtension.UseVisualStyleBackColor = True
			' 
			' lblChuk
			' 
			Me.lblChuk.Location = New System.Drawing.Point(8, 20)
			Me.lblChuk.Name = "lblChuk"
			Me.lblChuk.Size = New System.Drawing.Size(92, 40)
			Me.lblChuk.TabIndex = 13
			Me.lblChuk.Text = "Accel Values"
			' 
			' lblChukJoy
			' 
			Me.lblChukJoy.Location = New System.Drawing.Point(8, 64)
			Me.lblChukJoy.Name = "lblChukJoy"
			Me.lblChukJoy.Size = New System.Drawing.Size(92, 28)
			Me.lblChukJoy.TabIndex = 16
			Me.lblChukJoy.Text = "Joystick Values"
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.lblAccel)
			Me.groupBox1.Location = New System.Drawing.Point(80, 4)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(104, 72)
			Me.groupBox1.TabIndex = 18
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Wiimote Accel"
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.chkZ)
			Me.groupBox2.Controls.Add(Me.chkC)
			Me.groupBox2.Controls.Add(Me.lblChuk)
			Me.groupBox2.Controls.Add(Me.lblChukJoy)
			Me.groupBox2.Location = New System.Drawing.Point(80, 80)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(104, 136)
			Me.groupBox2.TabIndex = 19
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "Nunchuk"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.chkLED2)
			Me.groupBox3.Controls.Add(Me.chkLED4)
			Me.groupBox3.Controls.Add(Me.chkLED3)
			Me.groupBox3.Controls.Add(Me.chkLED1)
			Me.groupBox3.Controls.Add(Me.chkRumble)
			Me.groupBox3.Location = New System.Drawing.Point(268, 252)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(72, 120)
			Me.groupBox3.TabIndex = 20
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Outputs"
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.pbBattery)
			Me.groupBox4.Controls.Add(Me.lblBattery)
			Me.groupBox4.Location = New System.Drawing.Point(188, 192)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(156, 52)
			Me.groupBox4.TabIndex = 21
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Battery"
			' 
			' groupBox5
			' 
			Me.groupBox5.Controls.Add(Me.lblIR3Raw)
			Me.groupBox5.Controls.Add(Me.lblIR1Raw)
			Me.groupBox5.Controls.Add(Me.lblIR4Raw)
			Me.groupBox5.Controls.Add(Me.lblIR2Raw)
			Me.groupBox5.Controls.Add(Me.lblIR3)
			Me.groupBox5.Controls.Add(Me.lblIR1)
			Me.groupBox5.Controls.Add(Me.lblIR4)
			Me.groupBox5.Controls.Add(Me.lblIR2)
			Me.groupBox5.Controls.Add(Me.chkFound3)
			Me.groupBox5.Controls.Add(Me.chkFound4)
			Me.groupBox5.Controls.Add(Me.chkFound1)
			Me.groupBox5.Controls.Add(Me.chkFound2)
			Me.groupBox5.Location = New System.Drawing.Point(188, 4)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(156, 188)
			Me.groupBox5.TabIndex = 22
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "IR"
			' 
			' lblIR3Raw
			' 
			Me.lblIR3Raw.AutoSize = True
			Me.lblIR3Raw.Location = New System.Drawing.Point(8, 112)
			Me.lblIR3Raw.Name = "lblIR3Raw"
			Me.lblIR3Raw.Size = New System.Drawing.Size(46, 13)
			Me.lblIR3Raw.TabIndex = 10
			Me.lblIR3Raw.Text = "IR3Raw"
			' 
			' lblIR1Raw
			' 
			Me.lblIR1Raw.AutoSize = True
			Me.lblIR1Raw.Location = New System.Drawing.Point(8, 80)
			Me.lblIR1Raw.Name = "lblIR1Raw"
			Me.lblIR1Raw.Size = New System.Drawing.Size(46, 13)
			Me.lblIR1Raw.TabIndex = 10
			Me.lblIR1Raw.Text = "IR1Raw"
			' 
			' lblIR4Raw
			' 
			Me.lblIR4Raw.AutoSize = True
			Me.lblIR4Raw.Location = New System.Drawing.Point(8, 128)
			Me.lblIR4Raw.Name = "lblIR4Raw"
			Me.lblIR4Raw.Size = New System.Drawing.Size(46, 13)
			Me.lblIR4Raw.TabIndex = 9
			Me.lblIR4Raw.Text = "IR4Raw"
			' 
			' lblIR2Raw
			' 
			Me.lblIR2Raw.AutoSize = True
			Me.lblIR2Raw.Location = New System.Drawing.Point(8, 96)
			Me.lblIR2Raw.Name = "lblIR2Raw"
			Me.lblIR2Raw.Size = New System.Drawing.Size(46, 13)
			Me.lblIR2Raw.TabIndex = 9
			Me.lblIR2Raw.Text = "IR2Raw"
			' 
			' lblIR3
			' 
			Me.lblIR3.AutoSize = True
			Me.lblIR3.Location = New System.Drawing.Point(8, 48)
			Me.lblIR3.Name = "lblIR3"
			Me.lblIR3.Size = New System.Drawing.Size(24, 13)
			Me.lblIR3.TabIndex = 7
			Me.lblIR3.Text = "IR3"
			' 
			' lblIR4
			' 
			Me.lblIR4.AutoSize = True
			Me.lblIR4.Location = New System.Drawing.Point(8, 64)
			Me.lblIR4.Name = "lblIR4"
			Me.lblIR4.Size = New System.Drawing.Size(24, 13)
			Me.lblIR4.TabIndex = 7
			Me.lblIR4.Text = "IR4"
			' 
			' chkFound3
			' 
			Me.chkFound3.AutoSize = True
			Me.chkFound3.Location = New System.Drawing.Point(60, 148)
			Me.chkFound3.Name = "chkFound3"
			Me.chkFound3.Size = New System.Drawing.Size(46, 17)
			Me.chkFound3.TabIndex = 8
			Me.chkFound3.Text = "IR 3"
			Me.chkFound3.UseVisualStyleBackColor = True
			' 
			' chkFound4
			' 
			Me.chkFound4.AutoSize = True
			Me.chkFound4.Location = New System.Drawing.Point(60, 164)
			Me.chkFound4.Name = "chkFound4"
			Me.chkFound4.Size = New System.Drawing.Size(46, 17)
			Me.chkFound4.TabIndex = 8
			Me.chkFound4.Text = "IR 4"
			Me.chkFound4.UseVisualStyleBackColor = True
			' 
			' clbCCButtons
			' 
			Me.clbCCButtons.FormattingEnabled = True
			Me.clbCCButtons.Items.AddRange(New Object() { "A", "B", "X", "Y", "-", "Home", "+", "Up", "Down", "Left", "Right", "ZL", "ZR", "LTrigger", "RTrigger"})
			Me.clbCCButtons.Location = New System.Drawing.Point(4, 16)
			Me.clbCCButtons.Name = "clbCCButtons"
			Me.clbCCButtons.Size = New System.Drawing.Size(68, 244)
			Me.clbCCButtons.TabIndex = 23
			' 
			' groupBox6
			' 
			Me.groupBox6.Controls.Add(Me.lblTriggerR)
			Me.groupBox6.Controls.Add(Me.lblTriggerL)
			Me.groupBox6.Controls.Add(Me.lblCCJoy2)
			Me.groupBox6.Controls.Add(Me.lblCCJoy1)
			Me.groupBox6.Controls.Add(Me.clbCCButtons)
			Me.groupBox6.Location = New System.Drawing.Point(348, 168)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(188, 268)
			Me.groupBox6.TabIndex = 24
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Classic Controller"
			' 
			' lblTriggerR
			' 
			Me.lblTriggerR.AutoSize = True
			Me.lblTriggerR.Location = New System.Drawing.Point(76, 104)
			Me.lblTriggerR.Name = "lblTriggerR"
			Me.lblTriggerR.Size = New System.Drawing.Size(51, 13)
			Me.lblTriggerR.TabIndex = 25
			Me.lblTriggerR.Text = "Trigger R"
			' 
			' lblTriggerL
			' 
			Me.lblTriggerL.AutoSize = True
			Me.lblTriggerL.Location = New System.Drawing.Point(76, 88)
			Me.lblTriggerL.Name = "lblTriggerL"
			Me.lblTriggerL.Size = New System.Drawing.Size(49, 13)
			Me.lblTriggerL.TabIndex = 24
			Me.lblTriggerL.Text = "Trigger L"
			' 
			' lblCCJoy2
			' 
			Me.lblCCJoy2.Location = New System.Drawing.Point(76, 52)
			Me.lblCCJoy2.Name = "lblCCJoy2"
			Me.lblCCJoy2.Size = New System.Drawing.Size(92, 32)
			Me.lblCCJoy2.TabIndex = 24
			Me.lblCCJoy2.Text = "Right Joystick"
			' 
			' lblCCJoy1
			' 
			Me.lblCCJoy1.Location = New System.Drawing.Point(76, 16)
			Me.lblCCJoy1.Name = "lblCCJoy1"
			Me.lblCCJoy1.Size = New System.Drawing.Size(92, 32)
			Me.lblCCJoy1.TabIndex = 24
			Me.lblCCJoy1.Text = "Left Joystick"
			' 
			' groupBox7
			' 
			Me.groupBox7.Controls.Add(Me.lblGuitarWhammy)
			Me.groupBox7.Controls.Add(Me.lblGuitarJoy)
			Me.groupBox7.Controls.Add(Me.clbGuitarButtons)
			Me.groupBox7.Location = New System.Drawing.Point(348, 4)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(188, 160)
			Me.groupBox7.TabIndex = 26
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "Guitar"
			' 
			' lblGuitarWhammy
			' 
			Me.lblGuitarWhammy.AutoSize = True
			Me.lblGuitarWhammy.Location = New System.Drawing.Point(92, 52)
			Me.lblGuitarWhammy.Name = "lblGuitarWhammy"
			Me.lblGuitarWhammy.Size = New System.Drawing.Size(51, 13)
			Me.lblGuitarWhammy.TabIndex = 24
			Me.lblGuitarWhammy.Text = "Whammy"
			' 
			' lblGuitarJoy
			' 
			Me.lblGuitarJoy.Location = New System.Drawing.Point(92, 16)
			Me.lblGuitarJoy.Name = "lblGuitarJoy"
			Me.lblGuitarJoy.Size = New System.Drawing.Size(92, 32)
			Me.lblGuitarJoy.TabIndex = 24
			Me.lblGuitarJoy.Text = "Joystick Values"
			' 
			' clbGuitarButtons
			' 
			Me.clbGuitarButtons.FormattingEnabled = True
			Me.clbGuitarButtons.Items.AddRange(New Object() { "Green", "Red", "Yellow", "Blue", "Orange", "-", "+", "StrumUp", "StrumDown"})
			Me.clbGuitarButtons.Location = New System.Drawing.Point(4, 16)
			Me.clbGuitarButtons.Name = "clbGuitarButtons"
			Me.clbGuitarButtons.Size = New System.Drawing.Size(80, 139)
			Me.clbGuitarButtons.TabIndex = 23
			' 
			' groupBox8
			' 
			Me.groupBox8.Controls.Add(Me.clbButtons)
			Me.groupBox8.Location = New System.Drawing.Point(4, 4)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(72, 220)
			Me.groupBox8.TabIndex = 27
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "Wiimote"
			' 
			' chkC
			' 
			Me.chkC.AutoSize = True
			Me.chkC.Location = New System.Drawing.Point(8, 92)
			Me.chkC.Name = "chkC"
			Me.chkC.Size = New System.Drawing.Size(33, 17)
			Me.chkC.TabIndex = 17
			Me.chkC.Text = "C"
			Me.chkC.UseVisualStyleBackColor = True
			' 
			' chkZ
			' 
			Me.chkZ.AutoSize = True
			Me.chkZ.Location = New System.Drawing.Point(8, 112)
			Me.chkZ.Name = "chkZ"
			Me.chkZ.Size = New System.Drawing.Size(33, 17)
			Me.chkZ.TabIndex = 17
			Me.chkZ.Text = "Z"
			Me.chkZ.UseVisualStyleBackColor = True
			' 
			' MainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(541, 453)
			Me.Controls.Add(Me.groupBox8)
			Me.Controls.Add(Me.groupBox7)
			Me.Controls.Add(Me.groupBox6)
			Me.Controls.Add(Me.pbIR)
			Me.Controls.Add(Me.groupBox5)
			Me.Controls.Add(Me.groupBox4)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.chkExtension)
			Me.Controls.Add(Me.groupBox2)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.MaximizeBox = False
			Me.Name = "MainForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Wiimote Tester"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.Form1_FormClosing);
			CType(Me.pbIR, System.ComponentModel.ISupportInitialize).EndInit()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.groupBox8.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private clbButtons As System.Windows.Forms.CheckedListBox
		Private lblAccel As System.Windows.Forms.Label
		Private WithEvents chkLED4 As System.Windows.Forms.CheckBox
		Private WithEvents chkLED3 As System.Windows.Forms.CheckBox
		Private WithEvents chkLED2 As System.Windows.Forms.CheckBox
		Private WithEvents chkLED1 As System.Windows.Forms.CheckBox
		Private WithEvents chkRumble As System.Windows.Forms.CheckBox
		Private pbBattery As System.Windows.Forms.ProgressBar
		Private lblIR1 As System.Windows.Forms.Label
		Private lblIR2 As System.Windows.Forms.Label
		Private chkFound1 As System.Windows.Forms.CheckBox
		Private chkFound2 As System.Windows.Forms.CheckBox
		Private lblBattery As System.Windows.Forms.Label
		Private pbIR As System.Windows.Forms.PictureBox
		Private chkExtension As System.Windows.Forms.CheckBox
		Private lblChuk As System.Windows.Forms.Label
		Private lblChukJoy As System.Windows.Forms.Label
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private clbCCButtons As System.Windows.Forms.CheckedListBox
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private lblCCJoy2 As System.Windows.Forms.Label
		Private lblCCJoy1 As System.Windows.Forms.Label
		Private lblTriggerR As System.Windows.Forms.Label
		Private lblTriggerL As System.Windows.Forms.Label
		Private lblIR1Raw As System.Windows.Forms.Label
		Private lblIR2Raw As System.Windows.Forms.Label
		Private chkFound3 As System.Windows.Forms.CheckBox
		Private chkFound4 As System.Windows.Forms.CheckBox
		Private lblIR3Raw As System.Windows.Forms.Label
		Private lblIR4Raw As System.Windows.Forms.Label
		Private lblIR3 As System.Windows.Forms.Label
		Private lblIR4 As System.Windows.Forms.Label
		Private groupBox7 As System.Windows.Forms.GroupBox
		Private lblGuitarWhammy As System.Windows.Forms.Label
		Private lblGuitarJoy As System.Windows.Forms.Label
		Private clbGuitarButtons As System.Windows.Forms.CheckedListBox
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private chkZ As System.Windows.Forms.CheckBox
		Private chkC As System.Windows.Forms.CheckBox
	End Class
End Namespace

