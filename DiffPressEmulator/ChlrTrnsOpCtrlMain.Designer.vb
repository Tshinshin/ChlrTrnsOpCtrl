<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChlrTrnsOpCtrlMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChlrTrnsOpCtrlMain))
        Me.AxIPLink1 = New AxIPLINKLib6.AxIPLink()
        Me.RichTextBoxConsole = New System.Windows.Forms.RichTextBox()
        Me.NumericUpDownTimerInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TimerTrigger = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDownAR1FLsp = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.NumericUpDownAR1HWOutValve = New System.Windows.Forms.NumericUpDown()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.NumericUpDownHWBypassValve = New System.Windows.Forms.NumericUpDown()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.NumericUpDownAR5FLsp = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.AxIPLink1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownTimerInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownAR1FLsp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownAR1HWOutValve, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownHWBypassValve, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownAR5FLsp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxIPLink1
        '
        Me.AxIPLink1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxIPLink1.Enabled = True
        Me.AxIPLink1.Location = New System.Drawing.Point(752, 12)
        Me.AxIPLink1.Name = "AxIPLink1"
        Me.AxIPLink1.OcxState = CType(resources.GetObject("AxIPLink1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxIPLink1.Size = New System.Drawing.Size(36, 36)
        Me.AxIPLink1.TabIndex = 0
        '
        'RichTextBoxConsole
        '
        Me.RichTextBoxConsole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxConsole.Location = New System.Drawing.Point(12, 110)
        Me.RichTextBoxConsole.Name = "RichTextBoxConsole"
        Me.RichTextBoxConsole.Size = New System.Drawing.Size(776, 443)
        Me.RichTextBoxConsole.TabIndex = 2
        Me.RichTextBoxConsole.Text = ""
        '
        'NumericUpDownTimerInterval
        '
        Me.NumericUpDownTimerInterval.Location = New System.Drawing.Point(99, 10)
        Me.NumericUpDownTimerInterval.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.NumericUpDownTimerInterval.Name = "NumericUpDownTimerInterval"
        Me.NumericUpDownTimerInterval.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDownTimerInterval.TabIndex = 3
        Me.NumericUpDownTimerInterval.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Timer: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(225, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "秒"
        '
        'TimerTrigger
        '
        Me.TimerTrigger.Enabled = True
        Me.TimerTrigger.Interval = 1000
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "AR-1 温水送水弁開度:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(194, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "温水供給ﾊﾞｲﾊﾟｽ弁最小開度設定:"
        '
        'NumericUpDownAR1FLsp
        '
        Me.NumericUpDownAR1FLsp.Location = New System.Drawing.Point(212, 35)
        Me.NumericUpDownAR1FLsp.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.NumericUpDownAR1FLsp.Name = "NumericUpDownAR1FLsp"
        Me.NumericUpDownAR1FLsp.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDownAR1FLsp.TabIndex = 34
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 12)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "AR-1 温水流量設定:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(338, 37)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(32, 12)
        Me.Label21.TabIndex = 36
        Me.Label21.Text = "㎥/h"
        '
        'NumericUpDownAR1HWOutValve
        '
        Me.NumericUpDownAR1HWOutValve.Location = New System.Drawing.Point(212, 60)
        Me.NumericUpDownAR1HWOutValve.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.NumericUpDownAR1HWOutValve.Name = "NumericUpDownAR1HWOutValve"
        Me.NumericUpDownAR1HWOutValve.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDownAR1HWOutValve.TabIndex = 37
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(338, 62)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(38, 12)
        Me.Label22.TabIndex = 38
        Me.Label22.Text = "x10 %"
        '
        'NumericUpDownHWBypassValve
        '
        Me.NumericUpDownHWBypassValve.Location = New System.Drawing.Point(212, 85)
        Me.NumericUpDownHWBypassValve.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.NumericUpDownHWBypassValve.Name = "NumericUpDownHWBypassValve"
        Me.NumericUpDownHWBypassValve.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDownHWBypassValve.TabIndex = 39
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(338, 87)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(38, 12)
        Me.Label23.TabIndex = 40
        Me.Label23.Text = "x10 %"
        '
        'NumericUpDownAR5FLsp
        '
        Me.NumericUpDownAR5FLsp.Location = New System.Drawing.Point(539, 35)
        Me.NumericUpDownAR5FLsp.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.NumericUpDownAR5FLsp.Name = "NumericUpDownAR5FLsp"
        Me.NumericUpDownAR5FLsp.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDownAR5FLsp.TabIndex = 41
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(410, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 12)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "AR-5 温水流量設定:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(665, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 12)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "㎥/h"
        '
        'ChlrTrnsOpCtrlMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 565)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.NumericUpDownAR5FLsp)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.NumericUpDownHWBypassValve)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.NumericUpDownAR1HWOutValve)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.NumericUpDownAR1FLsp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownTimerInterval)
        Me.Controls.Add(Me.RichTextBoxConsole)
        Me.Controls.Add(Me.AxIPLink1)
        Me.Name = "ChlrTrnsOpCtrlMain"
        Me.Text = "Chiller Transient Operation Controller"
        CType(Me.AxIPLink1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownTimerInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownAR1FLsp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownAR1HWOutValve, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownHWBypassValve, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownAR5FLsp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AxIPLink1 As AxIPLINKLib6.AxIPLink
    Friend WithEvents RichTextBoxConsole As RichTextBox
    Friend WithEvents NumericUpDownTimerInterval As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TimerTrigger As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents NumericUpDownAR1FLsp As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents NumericUpDownAR1HWOutValve As NumericUpDown
    Friend WithEvents Label22 As Label
    Friend WithEvents NumericUpDownHWBypassValve As NumericUpDown
    Friend WithEvents Label23 As Label
    Friend WithEvents NumericUpDownAR5FLsp As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
