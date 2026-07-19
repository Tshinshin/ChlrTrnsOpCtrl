Imports AxIPLINKLib6
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Runtime.Remoting.Contexts
Imports Newtonsoft.Json.Linq

Public Class ChlrTrnsOpCtrlMain

    Private tag_AR1FLsp As String
    Private tag_AR5FLsp As String
    Private tag_AR1HWOutValve As String
    Private tag_HWBypassValve As String

    Private HWBypassValve As Double
    Private AR1FLsp As Double
    Private AR5FLsp As Double
    Private AR1HWOutValve As Double

    Private isStarted As Boolean

    Private Sub FormDiffPressEmulatorMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 操作対象のタグの設定
        tag_AR1FLsp = "UAL.AR1.AO0029"
        tag_AR5FLsp = "UAL.AR5.AO0061"
        tag_AR1HWOutValve = "UA.AR1.AI0030"
        tag_HWBypassValve = "UCL.ST.AO0023"

        ' TagのIPLink登録
        AxIPLink1.AddTag(tag_AR1FLsp, True)
        AxIPLink1.AddTag(tag_AR5FLsp, True)
        AxIPLink1.AddTag(tag_AR1HWOutValve, True)
        AxIPLink1.AddTag(tag_HWBypassValve, True)

        ' 画面の更新
        Dim vVal As Object

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR1FLsp, vVal, 1) Then
            AR1FLsp = Convert.ToDouble(vVal)
            NumericUpDownAR1FLsp.Value = AR1FLsp
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR5FLsp, vVal, 1) Then
            AR5FLsp = Convert.ToDouble(vVal)
            NumericUpDownAR5FLsp.Value = AR5FLsp
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR1HWOutValve, vVal, 1) Then
            AR1HWOutValve = Convert.ToDouble(vVal)
            NumericUpDownAR1HWOutValve.Value = AR1HWOutValve
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_HWBypassValve, vVal, 1) Then
            HWBypassValve = Convert.ToDouble(vVal)
            NumericUpDownHWBypassValve.Value = HWBypassValve
        End If

        ' AR-1起動時処理実行
        isStarted = False
        StartUpSetting()
    End Sub

    Private Sub NumericUpDownTimerInterval_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownTimerInterval.ValueChanged
        TimerTrigger.Interval = NumericUpDownTimerInterval.Value * 1000
    End Sub

    Private Sub TimerTrigger_Tick(sender As Object, e As EventArgs) Handles TimerTrigger.Tick
        Dim now As DateTime = DateTime.Now
        AppendLog("Timer tiriggered DiffPressEmulator @ " & now.ToString("yyyy/MM/dd HH:mm:ss"), True)
        FlowAndValveControl()
    End Sub

    Private Sub StartUpSetting()
        ' AR-1起動時、温水流量設定を356に設定
        AR1FLsp = 356
        AppendLog("Writing: " & tag_AR1FLsp & " = " & Convert.ToString(AR1FLsp), False)
        If AxIPLink1.WriteVal(tag_AR1FLsp, AR1FLsp) Then
            NumericUpDownAR1FLsp.Value = AR1FLsp
        Else
            AppendLog("Error at WriteVal", False)
        End If
    End Sub

    Private Sub FlowAndValveControl()
        Dim vVal As Object

        If Not isStarted Then
            ' AR-1 温水送水弁開度 確認
            vVal = Nothing
            If Not AxIPLink1.ReadVal(tag_AR1HWOutValve, vVal, 1) Then
                AppendLog("Error at ReadVal of tag_AR1HWOutValve", False)
                Exit Sub
            Else
                AR1HWOutValve = Convert.ToDouble(vVal)
                AppendLog("Reading:" & tag_AR1HWOutValve & " = " & Convert.ToString(AR1HWOutValve), False)
                NumericUpDownAR1HWOutValve.Value = AR1HWOutValve
            End If

            If AR1HWOutValve >= 700 Then
                HWBypassValve = 300
                AppendLog("Writing: " & tag_HWBypassValve & " = " & Convert.ToString(HWBypassValve), False)
                If AxIPLink1.WriteVal(tag_HWBypassValve, HWBypassValve) Then
                    NumericUpDownHWBypassValve.Value = HWBypassValve
                    isStarted = True
                Else
                    AppendLog("Error at WriteVal", False)
                End If

            End If
        End If

        If isStarted Then

            ' AR-1 温水流量設定 減
            vVal = Nothing
            If Not AxIPLink1.ReadVal(tag_AR1FLsp, vVal, 1) Then
                AppendLog("Error at ReadVal of tag_AR1FLsp", False)
                Exit Sub
            Else
                AR1FLsp = Convert.ToDouble(vVal)
                AppendLog("Reading:" & tag_AR1FLsp & " = " & Convert.ToString(AR1FLsp), False)
                NumericUpDownAR1FLsp.Value = AR1FLsp
            End If

            AR1FLsp = If(AR1FLsp - 1 > 250, AR1FLsp - 1, 250)
            AppendLog("Writing: " & tag_AR1FLsp & " = " & Convert.ToString(AR1FLsp), False)
            If AxIPLink1.WriteVal(tag_AR1FLsp, AR1FLsp) Then
                NumericUpDownAR1FLsp.Value = AR1FLsp
            Else
                AppendLog("Error at WriteVal", False)
                Exit Sub
            End If

            ' AR-5 温水流量設定 減
            vVal = Nothing
            If Not AxIPLink1.ReadVal(tag_AR5FLsp, vVal, 1) Then
                AppendLog("Error at ReadVal of tag_AR5FLsp", False)
                Exit Sub
            Else
                AR5FLsp = Convert.ToDouble(vVal)
                AppendLog("Reading:" & tag_AR5FLsp & " = " & Convert.ToString(AR5FLsp), False)
                NumericUpDownAR5FLsp.Value = AR5FLsp
            End If

            AR5FLsp = If(AR5FLsp - 1 > 250, AR5FLsp - 1, 250)
            AppendLog("Writing: " & tag_AR5FLsp & " = " & Convert.ToString(AR5FLsp), False)
            If AxIPLink1.WriteVal(tag_AR5FLsp, AR5FLsp) Then
                NumericUpDownAR5FLsp.Value = AR5FLsp
            Else
                AppendLog("Error at WriteVal", False)
                Exit Sub
            End If

            ' 温水バイパス弁開度 減
            vVal = Nothing
            If Not AxIPLink1.ReadVal(tag_HWBypassValve, vVal, 1) Then
                AppendLog("Error at ReadVal of tag_HWBypassValve", False)
                Exit Sub
            Else
                HWBypassValve = Convert.ToDouble(vVal)
                AppendLog("Reading:" & tag_HWBypassValve & " = " & Convert.ToString(HWBypassValve), False)
                NumericUpDownHWBypassValve.Value = HWBypassValve
            End If

            HWBypassValve = If(HWBypassValve - 5 > 30, HWBypassValve - 5, 30)
            AppendLog("Writing: " & tag_HWBypassValve & " = " & Convert.ToString(HWBypassValve), False)
            If AxIPLink1.WriteVal(tag_HWBypassValve, HWBypassValve) Then
                NumericUpDownHWBypassValve.Value = HWBypassValve
            Else
                AppendLog("Error at WriteVal", False)
                Exit Sub
            End If

            ' 流量・開度確認
            If AR1FLsp <= 250 And AR5FLsp <= 250 And HWBypassValve <= 30 Then
                Application.Exit()
            End If

        End If







    End Sub

    Private Sub AppendLog(message As String, prependNewLineAndBold As Boolean)

        If prependNewLineAndBold Then
            message = vbCrLf & message
        End If

        Dim startIndex As Integer = RichTextBoxConsole.TextLength
        RichTextBoxConsole.AppendText(message & vbCrLf)

        If prependNewLineAndBold Then
            RichTextBoxConsole.Select(startIndex, message.Length)
            RichTextBoxConsole.SelectionFont = New Font(RichTextBoxConsole.Font, FontStyle.Bold)
            RichTextBoxConsole.SelectionLength = 0
            RichTextBoxConsole.SelectionStart = RichTextBoxConsole.TextLength
        Else
            RichTextBoxConsole.SelectionStart = RichTextBoxConsole.TextLength
        End If

        RichTextBoxConsole.ScrollToCaret()
    End Sub

End Class
