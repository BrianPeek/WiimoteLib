Imports Microsoft.VisualBasic
Imports System
Namespace WiimoteTest
	Partial Public Class WiimoteInfo
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

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.clbButtons = New System.Windows.Forms.CheckedListBox()
			Me.lblTriggerR = New System.Windows.Forms.Label()
			Me.lblTriggerL = New System.Windows.Forms.Label()
			Me.lblIR3 = New System.Windows.Forms.Label()
			Me.lblIR4 = New System.Windows.Forms.Label()
			Me.lblCCJoy2 = New System.Windows.Forms.Label()
			Me.lblCCJoy1 = New System.Windows.Forms.Label()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.lblIR3Raw = New System.Windows.Forms.Label()
			Me.lblIR1Raw = New System.Windows.Forms.Label()
			Me.lblIR4Raw = New System.Windows.Forms.Label()
			Me.lblIR2Raw = New System.Windows.Forms.Label()
			Me.lblIR1 = New System.Windows.Forms.Label()
			Me.lblIR2 = New System.Windows.Forms.Label()
			Me.chkFound3 = New System.Windows.Forms.CheckBox()
			Me.chkFound4 = New System.Windows.Forms.CheckBox()
			Me.chkFound1 = New System.Windows.Forms.CheckBox()
			Me.chkFound2 = New System.Windows.Forms.CheckBox()
			Me.pbIR = New System.Windows.Forms.PictureBox()
			Me.lblGuitarWhammy = New System.Windows.Forms.Label()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.clbTouchbar = New System.Windows.Forms.CheckedListBox()
			Me.lblGuitarType = New System.Windows.Forms.Label()
			Me.lblGuitarJoy = New System.Windows.Forms.Label()
			Me.clbGuitarButtons = New System.Windows.Forms.CheckedListBox()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.clbCCButtons = New System.Windows.Forms.CheckedListBox()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.pbBattery = New System.Windows.Forms.ProgressBar()
			Me.lblBattery = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.chkLED2 = New System.Windows.Forms.CheckBox()
			Me.chkLED4 = New System.Windows.Forms.CheckBox()
			Me.chkLED3 = New System.Windows.Forms.CheckBox()
			Me.chkLED1 = New System.Windows.Forms.CheckBox()
			Me.chkRumble = New System.Windows.Forms.CheckBox()
			Me.chkZ = New System.Windows.Forms.CheckBox()
			Me.chkC = New System.Windows.Forms.CheckBox()
			Me.lblChuk = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.lblChukJoy = New System.Windows.Forms.Label()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.lblAccel = New System.Windows.Forms.Label()
			Me.chkExtension = New System.Windows.Forms.CheckBox()
			Me.groupBox9 = New System.Windows.Forms.GroupBox()
			Me.lblCOG = New System.Windows.Forms.Label()
			Me.chkLbs = New System.Windows.Forms.CheckBox()
			Me.lblBBBR = New System.Windows.Forms.Label()
			Me.lblBBTR = New System.Windows.Forms.Label()
			Me.lblBBBL = New System.Windows.Forms.Label()
			Me.lblBBTotal = New System.Windows.Forms.Label()
			Me.lblBBTL = New System.Windows.Forms.Label()
			Me.lblDevicePath = New System.Windows.Forms.Label()
			Me.groupBox10 = New System.Windows.Forms.GroupBox()
			Me.lbDrumVelocity = New System.Windows.Forms.ListBox()
			Me.lblDrumJoy = New System.Windows.Forms.Label()
			Me.clbDrums = New System.Windows.Forms.CheckedListBox()
			Me.groupBox8.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			CType(Me.pbIR, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.groupBox7.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.groupBox9.SuspendLayout()
			Me.groupBox10.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox8
			' 
			Me.groupBox8.Controls.Add(Me.clbButtons)
			Me.groupBox8.Location = New System.Drawing.Point(0, 0)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(72, 220)
			Me.groupBox8.TabIndex = 37
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "Wiimote"
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
			' lblCCJoy2
			' 
			Me.lblCCJoy2.Location = New System.Drawing.Point(76, 52)
			Me.lblCCJoy2.Name = "lblCCJoy2"
			Me.lblCCJoy2.Size = New System.Drawing.Size(108, 32)
			Me.lblCCJoy2.TabIndex = 24
			Me.lblCCJoy2.Text = "Right Joystick"
			' 
			' lblCCJoy1
			' 
			Me.lblCCJoy1.Location = New System.Drawing.Point(76, 16)
			Me.lblCCJoy1.Name = "lblCCJoy1"
			Me.lblCCJoy1.Size = New System.Drawing.Size(108, 32)
			Me.lblCCJoy1.TabIndex = 24
			Me.lblCCJoy1.Text = "Left Joystick"
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
			Me.groupBox5.Location = New System.Drawing.Point(184, 0)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(176, 188)
			Me.groupBox5.TabIndex = 34
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
			' pbIR
			' 
			Me.pbIR.Location = New System.Drawing.Point(4, 248)
			Me.pbIR.Name = "pbIR"
			Me.pbIR.Size = New System.Drawing.Size(256, 192)
			Me.pbIR.TabIndex = 28
			Me.pbIR.TabStop = False
			' 
			' lblGuitarWhammy
			' 
			Me.lblGuitarWhammy.AutoSize = True
			Me.lblGuitarWhammy.Location = New System.Drawing.Point(92, 140)
			Me.lblGuitarWhammy.Name = "lblGuitarWhammy"
			Me.lblGuitarWhammy.Size = New System.Drawing.Size(51, 13)
			Me.lblGuitarWhammy.TabIndex = 24
			Me.lblGuitarWhammy.Text = "Whammy"
			' 
			' groupBox7
			' 
			Me.groupBox7.Controls.Add(Me.clbTouchbar)
			Me.groupBox7.Controls.Add(Me.lblGuitarType)
			Me.groupBox7.Controls.Add(Me.lblGuitarWhammy)
			Me.groupBox7.Controls.Add(Me.lblGuitarJoy)
			Me.groupBox7.Controls.Add(Me.clbGuitarButtons)
			Me.groupBox7.Location = New System.Drawing.Point(364, 272)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(188, 176)
			Me.groupBox7.TabIndex = 36
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "Guitar"
			' 
			' clbTouchbar
			' 
			Me.clbTouchbar.FormattingEnabled = True
			Me.clbTouchbar.Items.AddRange(New Object() { "Green", "Red", "Yellow", "Blue", "Orange"})
			Me.clbTouchbar.Location = New System.Drawing.Point(88, 16)
			Me.clbTouchbar.Name = "clbTouchbar"
			Me.clbTouchbar.Size = New System.Drawing.Size(80, 79)
			Me.clbTouchbar.TabIndex = 25
			' 
			' lblGuitarType
			' 
			Me.lblGuitarType.AutoSize = True
			Me.lblGuitarType.Location = New System.Drawing.Point(4, 156)
			Me.lblGuitarType.Name = "lblGuitarType"
			Me.lblGuitarType.Size = New System.Drawing.Size(31, 13)
			Me.lblGuitarType.TabIndex = 24
			Me.lblGuitarType.Text = "Type"
			' 
			' lblGuitarJoy
			' 
			Me.lblGuitarJoy.Location = New System.Drawing.Point(92, 104)
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
			' groupBox6
			' 
			Me.groupBox6.Controls.Add(Me.lblTriggerR)
			Me.groupBox6.Controls.Add(Me.lblTriggerL)
			Me.groupBox6.Controls.Add(Me.lblCCJoy2)
			Me.groupBox6.Controls.Add(Me.lblCCJoy1)
			Me.groupBox6.Controls.Add(Me.clbCCButtons)
			Me.groupBox6.Location = New System.Drawing.Point(364, 0)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(188, 268)
			Me.groupBox6.TabIndex = 35
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Classic Controller"
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
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.pbBattery)
			Me.groupBox4.Controls.Add(Me.lblBattery)
			Me.groupBox4.Location = New System.Drawing.Point(184, 188)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(176, 52)
			Me.groupBox4.TabIndex = 33
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Battery"
			' 
			' pbBattery
			' 
			Me.pbBattery.Location = New System.Drawing.Point(8, 20)
			Me.pbBattery.Maximum = 200
			Me.pbBattery.Name = "pbBattery"
			Me.pbBattery.Size = New System.Drawing.Size(128, 23)
			Me.pbBattery.Step = 1
			Me.pbBattery.TabIndex = 6
			' 
			' lblBattery
			' 
			Me.lblBattery.AutoSize = True
			Me.lblBattery.Location = New System.Drawing.Point(140, 24)
			Me.lblBattery.Name = "lblBattery"
			Me.lblBattery.Size = New System.Drawing.Size(35, 13)
			Me.lblBattery.TabIndex = 9
			Me.lblBattery.Text = "label1"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.chkLED2)
			Me.groupBox3.Controls.Add(Me.chkLED4)
			Me.groupBox3.Controls.Add(Me.chkLED3)
			Me.groupBox3.Controls.Add(Me.chkLED1)
			Me.groupBox3.Controls.Add(Me.chkRumble)
			Me.groupBox3.Location = New System.Drawing.Point(264, 248)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(96, 120)
			Me.groupBox3.TabIndex = 32
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Outputs"
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
'			Me.chkLED2.CheckedChanged += New System.EventHandler(Me.chkLED_CheckedChanged);
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
'			Me.chkLED4.CheckedChanged += New System.EventHandler(Me.chkLED_CheckedChanged);
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
'			Me.chkLED3.CheckedChanged += New System.EventHandler(Me.chkLED_CheckedChanged);
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
'			Me.chkLED1.CheckedChanged += New System.EventHandler(Me.chkLED_CheckedChanged);
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
			' lblChuk
			' 
			Me.lblChuk.Location = New System.Drawing.Point(8, 20)
			Me.lblChuk.Name = "lblChuk"
			Me.lblChuk.Size = New System.Drawing.Size(92, 40)
			Me.lblChuk.TabIndex = 13
			Me.lblChuk.Text = "Accel Values"
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.chkZ)
			Me.groupBox2.Controls.Add(Me.chkC)
			Me.groupBox2.Controls.Add(Me.lblChuk)
			Me.groupBox2.Controls.Add(Me.lblChukJoy)
			Me.groupBox2.Location = New System.Drawing.Point(76, 76)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(104, 136)
			Me.groupBox2.TabIndex = 31
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "Nunchuk"
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
			Me.groupBox1.Location = New System.Drawing.Point(76, 0)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(104, 72)
			Me.groupBox1.TabIndex = 30
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Wiimote Accel"
			' 
			' lblAccel
			' 
			Me.lblAccel.Location = New System.Drawing.Point(8, 20)
			Me.lblAccel.Name = "lblAccel"
			Me.lblAccel.Size = New System.Drawing.Size(88, 48)
			Me.lblAccel.TabIndex = 2
			Me.lblAccel.Text = "Accel Values"
			' 
			' chkExtension
			' 
			Me.chkExtension.AutoSize = True
			Me.chkExtension.Location = New System.Drawing.Point(4, 224)
			Me.chkExtension.Name = "chkExtension"
			Me.chkExtension.Size = New System.Drawing.Size(52, 17)
			Me.chkExtension.TabIndex = 29
			Me.chkExtension.Text = "None"
			Me.chkExtension.UseVisualStyleBackColor = True
			' 
			' groupBox9
			' 
			Me.groupBox9.Controls.Add(Me.lblCOG)
			Me.groupBox9.Controls.Add(Me.chkLbs)
			Me.groupBox9.Controls.Add(Me.lblBBBR)
			Me.groupBox9.Controls.Add(Me.lblBBTR)
			Me.groupBox9.Controls.Add(Me.lblBBBL)
			Me.groupBox9.Controls.Add(Me.lblBBTotal)
			Me.groupBox9.Controls.Add(Me.lblBBTL)
			Me.groupBox9.Location = New System.Drawing.Point(556, 0)
			Me.groupBox9.Name = "groupBox9"
			Me.groupBox9.Size = New System.Drawing.Size(136, 112)
			Me.groupBox9.TabIndex = 38
			Me.groupBox9.TabStop = False
			Me.groupBox9.Text = "Balance Board"
			' 
			' lblCOG
			' 
			Me.lblCOG.AutoSize = True
			Me.lblCOG.Location = New System.Drawing.Point(8, 92)
			Me.lblCOG.Name = "lblCOG"
			Me.lblCOG.Size = New System.Drawing.Size(30, 13)
			Me.lblCOG.TabIndex = 2
			Me.lblCOG.Text = "COG"
			' 
			' chkLbs
			' 
			Me.chkLbs.AutoSize = True
			Me.chkLbs.Location = New System.Drawing.Point(28, 68)
			Me.chkLbs.Name = "chkLbs"
			Me.chkLbs.Size = New System.Drawing.Size(62, 17)
			Me.chkLbs.TabIndex = 1
			Me.chkLbs.Text = "Pounds"
			Me.chkLbs.UseVisualStyleBackColor = True
			' 
			' lblBBBR
			' 
			Me.lblBBBR.AutoSize = True
			Me.lblBBBR.Location = New System.Drawing.Point(76, 48)
			Me.lblBBBR.Name = "lblBBBR"
			Me.lblBBBR.Size = New System.Drawing.Size(22, 13)
			Me.lblBBBR.TabIndex = 0
			Me.lblBBBR.Text = "BR"
			' 
			' lblBBTR
			' 
			Me.lblBBTR.AutoSize = True
			Me.lblBBTR.Location = New System.Drawing.Point(76, 16)
			Me.lblBBTR.Name = "lblBBTR"
			Me.lblBBTR.Size = New System.Drawing.Size(22, 13)
			Me.lblBBTR.TabIndex = 0
			Me.lblBBTR.Text = "TR"
			' 
			' lblBBBL
			' 
			Me.lblBBBL.AutoSize = True
			Me.lblBBBL.Location = New System.Drawing.Point(8, 48)
			Me.lblBBBL.Name = "lblBBBL"
			Me.lblBBBL.Size = New System.Drawing.Size(20, 13)
			Me.lblBBBL.TabIndex = 0
			Me.lblBBBL.Text = "BL"
			' 
			' lblBBTotal
			' 
			Me.lblBBTotal.AutoSize = True
			Me.lblBBTotal.Location = New System.Drawing.Point(36, 32)
			Me.lblBBTotal.Name = "lblBBTotal"
			Me.lblBBTotal.Size = New System.Drawing.Size(31, 13)
			Me.lblBBTotal.TabIndex = 0
			Me.lblBBTotal.Text = "Total"
			' 
			' lblBBTL
			' 
			Me.lblBBTL.AutoSize = True
			Me.lblBBTL.Location = New System.Drawing.Point(8, 16)
			Me.lblBBTL.Name = "lblBBTL"
			Me.lblBBTL.Size = New System.Drawing.Size(20, 13)
			Me.lblBBTL.TabIndex = 0
			Me.lblBBTL.Text = "TL"
			' 
			' lblDevicePath
			' 
			Me.lblDevicePath.AutoSize = True
			Me.lblDevicePath.Location = New System.Drawing.Point(8, 444)
			Me.lblDevicePath.Name = "lblDevicePath"
			Me.lblDevicePath.Size = New System.Drawing.Size(63, 13)
			Me.lblDevicePath.TabIndex = 39
			Me.lblDevicePath.Text = "DevicePath"
			' 
			' groupBox10
			' 
			Me.groupBox10.Controls.Add(Me.lbDrumVelocity)
			Me.groupBox10.Controls.Add(Me.lblDrumJoy)
			Me.groupBox10.Controls.Add(Me.clbDrums)
			Me.groupBox10.Location = New System.Drawing.Point(556, 112)
			Me.groupBox10.Name = "groupBox10"
			Me.groupBox10.Size = New System.Drawing.Size(136, 180)
			Me.groupBox10.TabIndex = 40
			Me.groupBox10.TabStop = False
			Me.groupBox10.Text = "Drums"
			' 
			' lbDrumVelocity
			' 
			Me.lbDrumVelocity.FormattingEnabled = True
			Me.lbDrumVelocity.Location = New System.Drawing.Point(68, 16)
			Me.lbDrumVelocity.Name = "lbDrumVelocity"
			Me.lbDrumVelocity.Size = New System.Drawing.Size(56, 121)
			Me.lbDrumVelocity.TabIndex = 41
			' 
			' lblDrumJoy
			' 
			Me.lblDrumJoy.Location = New System.Drawing.Point(8, 144)
			Me.lblDrumJoy.Name = "lblDrumJoy"
			Me.lblDrumJoy.Size = New System.Drawing.Size(92, 32)
			Me.lblDrumJoy.TabIndex = 27
			Me.lblDrumJoy.Text = "Joystick Values"
			' 
			' clbDrums
			' 
			Me.clbDrums.FormattingEnabled = True
			Me.clbDrums.Items.AddRange(New Object() { "Red", "Blue", "Green", "Yellow", "Orange", "Pedal", "-", "+"})
			Me.clbDrums.Location = New System.Drawing.Point(4, 16)
			Me.clbDrums.Name = "clbDrums"
			Me.clbDrums.Size = New System.Drawing.Size(60, 124)
			Me.clbDrums.TabIndex = 26
			' 
			' WiimoteInfo
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.groupBox10)
			Me.Controls.Add(Me.lblDevicePath)
			Me.Controls.Add(Me.groupBox9)
			Me.Controls.Add(Me.groupBox8)
			Me.Controls.Add(Me.groupBox5)
			Me.Controls.Add(Me.pbIR)
			Me.Controls.Add(Me.groupBox7)
			Me.Controls.Add(Me.groupBox6)
			Me.Controls.Add(Me.groupBox4)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.chkExtension)
			Me.Name = "WiimoteInfo"
			Me.Size = New System.Drawing.Size(696, 464)
			Me.groupBox8.ResumeLayout(False)
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			CType(Me.pbIR, System.ComponentModel.ISupportInitialize).EndInit()
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox9.ResumeLayout(False)
			Me.groupBox9.PerformLayout()
			Me.groupBox10.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Public groupBox8 As System.Windows.Forms.GroupBox
		Public clbButtons As System.Windows.Forms.CheckedListBox
		Public lblTriggerR As System.Windows.Forms.Label
		Public lblTriggerL As System.Windows.Forms.Label
		Public lblIR3 As System.Windows.Forms.Label
		Public lblIR4 As System.Windows.Forms.Label
		Public lblCCJoy2 As System.Windows.Forms.Label
		Public lblCCJoy1 As System.Windows.Forms.Label
		Public groupBox5 As System.Windows.Forms.GroupBox
		Public lblIR3Raw As System.Windows.Forms.Label
		Public lblIR1Raw As System.Windows.Forms.Label
		Public lblIR4Raw As System.Windows.Forms.Label
		Public lblIR2Raw As System.Windows.Forms.Label
		Public lblIR1 As System.Windows.Forms.Label
		Public lblIR2 As System.Windows.Forms.Label
		Public chkFound3 As System.Windows.Forms.CheckBox
		Public chkFound4 As System.Windows.Forms.CheckBox
		Public chkFound1 As System.Windows.Forms.CheckBox
		Public chkFound2 As System.Windows.Forms.CheckBox
		Public pbIR As System.Windows.Forms.PictureBox
		Public lblGuitarWhammy As System.Windows.Forms.Label
		Public groupBox7 As System.Windows.Forms.GroupBox
		Public lblGuitarJoy As System.Windows.Forms.Label
		Public clbGuitarButtons As System.Windows.Forms.CheckedListBox
		Public groupBox6 As System.Windows.Forms.GroupBox
		Public clbCCButtons As System.Windows.Forms.CheckedListBox
		Public groupBox4 As System.Windows.Forms.GroupBox
		Public pbBattery As System.Windows.Forms.ProgressBar
		Public lblBattery As System.Windows.Forms.Label
		Public groupBox3 As System.Windows.Forms.GroupBox
		Public WithEvents chkLED2 As System.Windows.Forms.CheckBox
		Public WithEvents chkLED4 As System.Windows.Forms.CheckBox
		Public WithEvents chkLED3 As System.Windows.Forms.CheckBox
		Public WithEvents chkLED1 As System.Windows.Forms.CheckBox
		Public WithEvents chkRumble As System.Windows.Forms.CheckBox
		Public chkZ As System.Windows.Forms.CheckBox
		Public chkC As System.Windows.Forms.CheckBox
		Public lblChuk As System.Windows.Forms.Label
		Public groupBox2 As System.Windows.Forms.GroupBox
		Public lblChukJoy As System.Windows.Forms.Label
		Public groupBox1 As System.Windows.Forms.GroupBox
		Public lblAccel As System.Windows.Forms.Label
		Public chkExtension As System.Windows.Forms.CheckBox
		Private groupBox9 As System.Windows.Forms.GroupBox
		Private chkLbs As System.Windows.Forms.CheckBox
		Private lblBBBR As System.Windows.Forms.Label
		Private lblBBTR As System.Windows.Forms.Label
		Private lblBBBL As System.Windows.Forms.Label
		Private lblBBTotal As System.Windows.Forms.Label
		Private lblBBTL As System.Windows.Forms.Label
		Private lblCOG As System.Windows.Forms.Label
		Private lblDevicePath As System.Windows.Forms.Label
		Public clbTouchbar As System.Windows.Forms.CheckedListBox
		Public lblGuitarType As System.Windows.Forms.Label
		Private groupBox10 As System.Windows.Forms.GroupBox
		Public clbDrums As System.Windows.Forms.CheckedListBox
		Public lblDrumJoy As System.Windows.Forms.Label
		Private lbDrumVelocity As System.Windows.Forms.ListBox

	End Class
End Namespace
