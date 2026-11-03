Imports AxIPLINKLib6
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Runtime.Remoting.Contexts
Imports Newtonsoft.Json.Linq

Public Class ChlrTrnsOpCtrlMain

    Private startUpEquipment As String

    Private tag_AR1 As String
    Private tag_AR2 As String
    Private tag_AR3 As String
    Private tag_AR4 As String
    Private tag_AR5 As String
    Private tag_TR1 As String
    Private tag_TR2 As String
    Private tag_TR3 As String
    Private tag_TR4 As String

    Private tag_AR1AOSET As String
    Private tag_AR2AOSET As String
    Private tag_AR3AOSET As String
    Private tag_AR4AOSET As String
    Private tag_AR5AOSET As String
    Private tag_TR1AOSET As String
    Private tag_TR2AOSET As String
    Private tag_TR34AOSET As String
    Private tag_STAOSET As String

    Private tag_AR1FLsp As String
    Private tag_AR2FLsp As String
    Private tag_AR3FLsp As String
    Private tag_AR4FLsp As String
    Private tag_AR5FLsp As String
    Private tag_TR1FLsp As String
    Private tag_TR2FLsp As String
    Private tag_TR3FLsp As String
    Private tag_TR4FLsp As String

    Private tag_TR1FL As String
    Private tag_TR2FL As String

    Private tag_TR1CW_AUTO_MAN_SWITCH As String
    Private tag_TR2CW_AUTO_MAN_SWITCH As String

    Private tag_TR1CW_MAN As String
    Private tag_TR2CW_MAN As String

    Private tag_AR1CWOutValve As String
    Private tag_AR2CWOutValve As String
    Private tag_AR3CWOutValve As String
    Private tag_AR4CWOutValve As String
    Private tag_AR5CWOutValve As String
    Private tag_CWBypassValve As String
    Private tag_CWBypassValveSet As String

    Private AR1State As Boolean
    Private AR2State As Boolean
    Private AR3State As Boolean
    Private AR4State As Boolean
    Private AR5State As Boolean
    Private TR1State As Boolean
    Private TR2State As Boolean
    Private TR3State As Boolean
    Private TR4State As Boolean

    Private AR1FLrtd As Double
    Private AR2FLrtd As Double
    Private AR3FLrtd As Double
    Private AR4FLrtd As Double
    Private AR5FLrtd As Double
    Private TR1FLrtd As Double
    Private TR2FLrtd As Double
    Private TR3FLrtd As Double
    Private TR4FLrtd As Double

    Private AR1FLsp As Double
    Private AR2FLsp As Double
    Private AR3FLsp As Double
    Private AR4FLsp As Double
    Private AR5FLsp As Double
    Private TR1FLsp As Double
    Private TR2FLsp As Double
    Private TR3FLsp As Double
    Private TR4FLsp As Double

    Private TR1FL As Double
    Private TR2FL As Double

    Private AR1CWOutValve As Double
    Private AR2CWOutValve As Double
    Private AR3CWOutValve As Double
    Private AR4CWOutValve As Double
    Private AR5CWOutValve As Double


    Private CWBypassValve As Double

    Private isStarted As Boolean
    Private isCWBypassSet30Percent As Boolean
    Private isStabilized As Boolean

    ' 流量安定判定関数（isStabilized）呼び出し履歴（時刻, 値）を保持
    Private ReadOnly history As New List(Of (Timestamp As DateTime, Value As Double))

    ' 流量安定判定関数（isStabilized）監視期間（秒）と許容誤差（±2%）
    Private Const WindowSeconds As Double = 30.0
    Private Const TolerancePercent As Double = 0.01

    ' 冷水バイパス弁閉止までのリードタイム（秒）
    Private Const CWBypassCloseLeadTime As Double = 240

    Private Sub FormDiffPressEmulatorMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 起動時引数をメッセージとして画面に表示
        Dim commandLineArgs As String() = Environment.GetCommandLineArgs()
        startUpEquipment = commandLineArgs(1)
        If commandLineArgs.Length > 1 Then
            AppendLog("起動時引数（起動機器）: " & startUpEquipment, True)
        End If

        ' 操作対象のタグの設定

        tag_AR1 = "UA.AR1.DO0017"
        tag_AR2 = "UB.AR2.DO0017"
        tag_AR3 = "UA.AR3.DO0033"
        tag_AR4 = "UB.AR4.DO0033"
        tag_AR5 = "UA.AR5.DO0049"
        tag_TR1 = "UA.TR1.DO0001"
        tag_TR2 = "UB.TR2.DO0001"
        tag_TR3 = "UA.TR34.DO0097"
        tag_TR4 = "UA.TR34.DO0113"

        tag_AR1AOSET = "UA.AR1.AOSET"
        tag_AR2AOSET = "UB.AR2.AOSET"
        tag_AR3AOSET = "UA.AR3.AOSET"
        tag_AR4AOSET = "UB.AR4.AOSET"
        tag_AR5AOSET = "UA.AR5.AOSET"
        tag_TR1AOSET = "UA.TR1.AOSET"
        tag_TR2AOSET = "UB.TR2.AOSET"
        tag_TR34AOSET = "UA.TR34.AOSET"
        tag_STAOSET = "UC.ST.AOSET"

        tag_AR1FLsp = "UA.AR1.AO0025SET"
        tag_AR2FLsp = "UB.AR2.AO0025SET"
        tag_AR3FLsp = "UA.AR3.AO0041SET"
        tag_AR4FLsp = "UB.AR4.AO0041SET"
        tag_AR5FLsp = "UA.AR5.AO0057SET"
        tag_TR1FLsp = "UA.TR1.AO0009SET"
        tag_TR2FLsp = "UB.TR2.AO0009SET"
        tag_TR3FLsp = "UA.TR34.AO0089SET"
        tag_TR4FLsp = "UA.TR34.AO0105SET"

        tag_TR1FL = "UA.TR1.AI0018"
        tag_TR2FL = "UB.TR2.AI0018"

        tag_TR1CW_AUTO_MAN_SWITCH = "UAL.TR1.DO0005"
        tag_TR2CW_AUTO_MAN_SWITCH = "UBL.TR2.DO0005"

        tag_TR1CW_MAN = "UAL.TR1.DO0005_MAN"
        tag_TR2CW_MAN = "UBL.TR2.DO0005_MAN"

        tag_AR1CWOutValve = "UA.AR1.AI0029"
        tag_AR2CWOutValve = "UB.AR2.AI0029"
        tag_AR3CWOutValve = "UA.AR3.AI0053"
        tag_AR4CWOutValve = "UB.AR4.AI0053"
        tag_AR5CWOutValve = "UA.AR5.AI0077"

        tag_CWBypassValve = "UC.ST.AO0022"
        tag_CWBypassValveSet = "UC.ST.AO0022SET"

        ' TagのIPLink登録
        AxIPLink1.AddTag(tag_AR1, True)
        AxIPLink1.AddTag(tag_AR2, True)
        AxIPLink1.AddTag(tag_AR3, True)
        AxIPLink1.AddTag(tag_AR4, True)
        AxIPLink1.AddTag(tag_AR5, True)
        AxIPLink1.AddTag(tag_TR1, True)
        AxIPLink1.AddTag(tag_TR2, True)
        AxIPLink1.AddTag(tag_TR3, True)
        AxIPLink1.AddTag(tag_TR4, True)
        AxIPLink1.AddTag(tag_AR1AOSET, True)
        AxIPLink1.AddTag(tag_AR2AOSET, True)
        AxIPLink1.AddTag(tag_AR3AOSET, True)
        AxIPLink1.AddTag(tag_AR4AOSET, True)
        AxIPLink1.AddTag(tag_AR5AOSET, True)
        AxIPLink1.AddTag(tag_TR1AOSET, True)
        AxIPLink1.AddTag(tag_TR2AOSET, True)
        AxIPLink1.AddTag(tag_TR34AOSET, True)
        AxIPLink1.AddTag(tag_STAOSET, True)
        AxIPLink1.AddTag(tag_AR1FLsp, True)
        AxIPLink1.AddTag(tag_AR2FLsp, True)
        AxIPLink1.AddTag(tag_AR3FLsp, True)
        AxIPLink1.AddTag(tag_AR4FLsp, True)
        AxIPLink1.AddTag(tag_AR5FLsp, True)
        AxIPLink1.AddTag(tag_TR1FLsp, True)
        AxIPLink1.AddTag(tag_TR2FLsp, True)
        AxIPLink1.AddTag(tag_TR3FLsp, True)
        AxIPLink1.AddTag(tag_TR4FLsp, True)
        AxIPLink1.AddTag(tag_TR1FL, True)
        AxIPLink1.AddTag(tag_TR2FL, True)
        AxIPLink1.AddTag(tag_TR1CW_AUTO_MAN_SWITCH, True)
        AxIPLink1.AddTag(tag_TR2CW_AUTO_MAN_SWITCH, True)
        AxIPLink1.AddTag(tag_TR1CW_MAN, True)
        AxIPLink1.AddTag(tag_TR2CW_MAN, True)
        AxIPLink1.AddTag(tag_AR1CWOutValve, True)
        AxIPLink1.AddTag(tag_AR2CWOutValve, True)
        AxIPLink1.AddTag(tag_AR3CWOutValve, True)
        AxIPLink1.AddTag(tag_AR4CWOutValve, True)
        AxIPLink1.AddTag(tag_AR5CWOutValve, True)
        AxIPLink1.AddTag(tag_CWBypassValve, True)
        AxIPLink1.AddTag(tag_CWBypassValveSet, True)

        ' 各機器の定格流量の設定
        AR1FLrtd = 432
        AR2FLrtd = 432
        AR3FLrtd = 432
        AR4FLrtd = 432
        AR5FLrtd = 432
        TR1FLrtd = 430.8
        TR2FLrtd = 430.8
        TR3FLrtd = 215.4
        TR4FLrtd = 215.4

        ' 画面の更新
        UpdateForm()

        ' 起動時の機器に応じて、過渡運転が必要かどうかを設定
        Select Case startUpEquipment
            Case "AR1", "AR2", "AR3", "AR4", "AR5"
                isStarted = False
                isStabilized = True
            Case "TR1", "TR2"
                isStarted = True
                isStabilized = False
            Case "TR3", "TR4"
                isStarted = True
                isStabilized = True
        End Select

        isCWBypassSet30Percent = False

        StartUpSetting()
        CheckRunningEquipment()
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

        Select Case startUpEquipment
            Case "AR1"
                ' AR-1起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_AR1AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_AR1AOSET, 1) Then
                    AR1FLsp = AR1FLrtd
                    AppendLog("Writing: " & tag_AR1FLsp & " = " & Convert.ToString(AR1FLsp), False)
                    If AxIPLink1.WriteVal(tag_AR1FLsp, AR1FLsp) Then
                        TextBoxAR1FLsp.Text = AR1FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_AR1AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_AR1AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "AR2"
                ' AR-2起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_AR2AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_AR2AOSET, 1) Then
                    AR2FLsp = AR2FLrtd
                    AppendLog("Writing: " & tag_AR2FLsp & " = " & Convert.ToString(AR2FLsp), False)
                    If AxIPLink1.WriteVal(tag_AR2FLsp, AR2FLsp) Then
                        TextBoxAR2FLsp.Text = AR2FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_AR2AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_AR2AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "AR3"
                ' AR-3起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_AR3AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_AR3AOSET, 1) Then
                    AR3FLsp = AR3FLrtd
                    AppendLog("Writing: " & tag_AR3FLsp & " = " & Convert.ToString(AR3FLsp), False)
                    If AxIPLink1.WriteVal(tag_AR3FLsp, AR3FLsp) Then
                        TextBoxAR3FLsp.Text = AR3FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_AR3AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_AR3AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "AR4"
                ' AR-4起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_AR4AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_AR4AOSET, 1) Then
                    AR4FLsp = AR4FLrtd
                    AppendLog("Writing: " & tag_AR4FLsp & " = " & Convert.ToString(AR4FLsp), False)
                    If AxIPLink1.WriteVal(tag_AR4FLsp, AR4FLsp) Then
                        TextBoxAR4FLsp.Text = AR4FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_AR4AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_AR4AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "AR5"
                ' AR-5起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_AR5AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_AR5AOSET, 1) Then
                    AR5FLsp = AR5FLrtd
                    AppendLog("Writing: " & tag_AR5FLsp & " = " & Convert.ToString(AR5FLsp), False)
                    If AxIPLink1.WriteVal(tag_AR5FLsp, AR5FLsp) Then
                        TextBoxAR5FLsp.Text = AR5FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_AR5AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_AR5AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "TR1"
                ' TR-1起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_TR1AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_TR1AOSET, 1) Then
                    TR1FLsp = TR1FLrtd
                    AppendLog("Writing: " & tag_TR1FLsp & " = " & Convert.ToString(TR1FLsp), False)
                    If AxIPLink1.WriteVal(tag_TR1FLsp, TR1FLsp) Then
                        TextBoxTR1FLsp.Text = TR1FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_TR1AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_TR1AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "TR2"
                ' TR-2起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_TR2AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_TR2AOSET, 1) Then
                    TR2FLsp = TR2FLrtd
                    AppendLog("Writing: " & tag_TR2FLsp & " = " & Convert.ToString(TR2FLsp), False)
                    If AxIPLink1.WriteVal(tag_TR2FLsp, TR2FLsp) Then
                        TextBoxTR2FLsp.Text = TR2FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_TR2AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_TR2AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "TR3"
                ' TR-3起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_TR34AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_TR34AOSET, 1) Then
                    TR3FLsp = TR3FLrtd
                    AppendLog("Writing: " & tag_TR3FLsp & " = " & Convert.ToString(TR3FLsp), False)
                    If AxIPLink1.WriteVal(tag_TR3FLsp, TR3FLsp) Then
                        TextBoxTR3FLsp.Text = TR3FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_TR34AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_TR34AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
            Case "TR4"
                ' TR-4起動時、冷水流量設定を定格値に設定
                ' AOSETを1にしてから流量設定値を書き込み、書き込み後にAOSETを0に戻す
                AppendLog("Writing: " & tag_TR34AOSET & " = 1", False)
                If AxIPLink1.WriteVal(tag_TR34AOSET, 1) Then
                    TR4FLsp = TR4FLrtd
                    AppendLog("Writing: " & tag_TR4FLsp & " = " & Convert.ToString(TR4FLsp), False)
                    If AxIPLink1.WriteVal(tag_TR4FLsp, TR4FLsp) Then
                        TextBoxTR4FLsp.Text = TR4FLsp.ToString
                    Else
                        AppendLog("Error at WriteVal", False)
                    End If
                    AppendLog("Writing: " & tag_TR34AOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_TR34AOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                End If
        End Select

    End Sub

    Private Sub CheckRunningEquipment()
        Dim vVal As Object

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_AR1, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_AR1", False)
            Exit Sub
        Else
            AR1State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_AR1 & " = " & Convert.ToString(AR1State), False)
            If AR1State Then
                LampAR1.BackColor = Color.Lime
            Else
                LampAR1.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_AR2, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_AR2", False)
            Exit Sub
        Else
            AR2State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_AR2 & " = " & Convert.ToString(AR2State), False)
            If AR2State Then
                LampAR2.BackColor = Color.Lime
            Else
                LampAR2.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_AR3, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_AR3", False)
            Exit Sub
        Else
            AR3State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_AR3 & " = " & Convert.ToString(AR3State), False)
            If AR3State Then
                LampAR3.BackColor = Color.Lime
            Else
                LampAR3.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_AR4, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_AR4", False)
            Exit Sub
        Else
            AR4State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_AR4 & " = " & Convert.ToString(AR4State), False)
            If AR4State Then
                LampAR4.BackColor = Color.Lime
            Else
                LampAR4.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_AR5, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_AR5", False)
            Exit Sub
        Else
            AR5State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_AR5 & " = " & Convert.ToString(AR5State), False)
            If AR5State Then
                LampAR5.BackColor = Color.Lime
            Else
                LampAR5.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_TR1, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_TR1", False)
            Exit Sub
        Else
            TR1State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_TR1 & " = " & Convert.ToString(TR1State), False)
            If TR1State Then
                LampTR1.BackColor = Color.Lime
            Else
                LampTR1.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_TR2, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_TR2", False)
            Exit Sub
        Else
            TR2State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_TR2 & " = " & Convert.ToString(TR2State), False)
            If TR2State Then
                LampTR2.BackColor = Color.Lime
            Else
                LampTR2.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_TR3, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_TR3", False)
            Exit Sub
        Else
            TR3State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_TR3 & " = " & Convert.ToString(TR3State), False)
            If TR3State Then
                LampTR3.BackColor = Color.Lime
            Else
                LampTR3.BackColor = Color.Silver
            End If
        End If

        vVal = Nothing
        If Not AxIPLink1.ReadVal(tag_TR4, vVal, 1) Then
            AppendLog("Error at ReadVal of tag_TR4", False)
            Exit Sub
        Else
            TR4State = Convert.ToBoolean(vVal)
            AppendLog("Reading:" & tag_TR4 & " = " & Convert.ToString(TR4State), False)
            If TR4State Then
                LampTR4.BackColor = Color.Lime
            Else
                LampTR4.BackColor = Color.Silver
            End If
        End If

        Select Case startUpEquipment
            Case "AR1"
                LampAR1.BackColor = Color.Red
            Case "AR2"
                LampAR2.BackColor = Color.Red
            Case "AR3"
                LampAR3.BackColor = Color.Red
            Case "AR4"
                LampAR4.BackColor = Color.Red
            Case "AR5"
                LampAR5.BackColor = Color.Red
            Case "TR1"
                LampTR1.BackColor = Color.Red
            Case "TR2"
                LampTR2.BackColor = Color.Red
            Case "TR3"
                LampTR3.BackColor = Color.Red
            Case "TR4"
                LampTR4.BackColor = Color.Red
        End Select

    End Sub

    Private Sub FlowAndValveControl()
        Dim vVal As Object

        If Not isStarted Then

            Select Case startUpEquipment
                Case "AR1"
                    ' AR-1 冷水送水弁開度 確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_AR1CWOutValve, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_AR1CWOutValve", False)
                        Exit Sub
                    Else
                        AR1CWOutValve = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_AR1CWOutValve & " = " & Convert.ToString(AR1CWOutValve), False)
                        TextBoxAR1CWOutValve.Text = AR1CWOutValve.ToString()
                        ' 冷水バイパス弁開度を温水送水弁開度に応じて設定
                        CWBypassValve = If(AR1CWOutValve <= 200, 30, If(AR1CWOutValve >= 500, 300, 30 + (AR1CWOutValve - 200) / (500 - 200) * (300 - 30)))
                        AppendLog("Writing: " & tag_STAOSET & " = 1", False)
                        If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                            AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                            If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                                isCWBypassSet30Percent = True
                            Else
                                AppendLog("Error at WriteVal", False)
                            End If
                            AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                            If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                                AppendLog("Error at WriteVal", False)
                            End If
                        Else
                            AppendLog("Error at WriteVal", False)
                        End If
                    End If
                    If AR1CWOutValve >= 500 Then
                        isStarted = True
                    End If
                Case "AR2"
                    ' AR-2 冷水送水弁開度 確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_AR2CWOutValve, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_AR2CWOutValve", False)
                        Exit Sub
                    Else
                        AR2CWOutValve = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_AR2CWOutValve & " = " & Convert.ToString(AR2CWOutValve), False)
                        TextBoxAR2CWOutValve.Text = AR2CWOutValve.ToString()
                        ' 冷水バイパス弁開度を温水送水弁開度に応じて設定
                        CWBypassValve = If(AR2CWOutValve <= 200, 30, If(AR2CWOutValve >= 500, 300, 30 + (AR2CWOutValve - 200) / (500 - 200) * (300 - 30)))
                        AppendLog("Writing: " & tag_STAOSET & " = 1", False)
                        If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                            AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                            If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                                isCWBypassSet30Percent = True
                            Else
                                AppendLog("Error at WriteVal", False)
                            End If
                            AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                            If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                                AppendLog("Error at WriteVal", False)
                            End If
                        Else
                            AppendLog("Error at WriteVal", False)
                        End If
                    End If
                    If AR2CWOutValve >= 500 Then
                        isStarted = True
                    End If
                Case "AR3"
                    ' AR-3 冷水送水弁開度 確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_AR3CWOutValve, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_AR3CWOutValve", False)
                        Exit Sub
                    Else
                        AR3CWOutValve = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_AR3CWOutValve & " = " & Convert.ToString(AR3CWOutValve), False)
                        TextBoxAR3CWOutValve.Text = AR3CWOutValve.ToString()
                        ' 冷水バイパス弁開度を温水送水弁開度に応じて設定
                        CWBypassValve = If(AR3CWOutValve <= 200, 30, If(AR3CWOutValve >= 500, 300, 30 + (AR3CWOutValve - 200) / (500 - 200) * (300 - 30)))
                        AppendLog("Writing: " & tag_STAOSET & " = 1", False)
                        If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                            AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                            If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                                isCWBypassSet30Percent = True
                            Else
                                AppendLog("Error at WriteVal", False)
                            End If
                            AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                            If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                                AppendLog("Error at WriteVal", False)
                            End If
                        Else
                            AppendLog("Error at WriteVal", False)
                        End If
                    End If
                    If AR3CWOutValve >= 500 Then
                        isStarted = True
                    End If
                Case "AR4"
                    ' AR-4 冷水送水弁開度 確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_AR4CWOutValve, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_AR4CWOutValve", False)
                        Exit Sub
                    Else
                        AR4CWOutValve = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_AR4CWOutValve & " = " & Convert.ToString(AR4CWOutValve), False)
                        TextBoxAR4CWOutValve.Text = AR4CWOutValve.ToString()
                        ' 冷水バイパス弁開度を温水送水弁開度に応じて設定
                        CWBypassValve = If(AR4CWOutValve <= 200, 30, If(AR4CWOutValve >= 500, 300, 30 + (AR4CWOutValve - 200) / (500 - 200) * (300 - 30)))
                        AppendLog("Writing: " & tag_STAOSET & " = 1", False)
                        If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                            AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                            If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                                isCWBypassSet30Percent = True
                            Else
                                AppendLog("Error at WriteVal", False)
                            End If
                            AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                            If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                                AppendLog("Error at WriteVal", False)
                            End If
                        Else
                            AppendLog("Error at WriteVal", False)
                        End If
                    End If
                    If AR4CWOutValve >= 500 Then
                        isStarted = True
                    End If
                Case "AR5"
                    ' AR-5 冷水送水弁開度 確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_AR5CWOutValve, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_AR5CWOutValve", False)
                        Exit Sub
                    Else
                        AR5CWOutValve = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_AR5CWOutValve & " = " & Convert.ToString(AR5CWOutValve), False)
                        TextBoxAR5CWOutValve.Text = AR5CWOutValve.ToString()
                        ' 冷水バイパス弁開度を温水送水弁開度に応じて設定
                        CWBypassValve = If(AR5CWOutValve <= 200, 30, If(AR5CWOutValve >= 500, 300, 30 + (AR5CWOutValve - 200) / (500 - 200) * (300 - 30)))
                        AppendLog("Writing: " & tag_STAOSET & " = 1", False)
                        If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                            AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                            If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                                isCWBypassSet30Percent = True
                            Else
                                AppendLog("Error at WriteVal", False)
                            End If
                            AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                            If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                                AppendLog("Error at WriteVal", False)
                            End If
                        Else
                            AppendLog("Error at WriteVal", False)
                        End If
                    End If
                    If AR5CWOutValve >= 500 Then
                        isStarted = True
                    End If
            End Select

        End If


        If isStarted And Not isCWBypassSet30Percent Then
            CWBypassValve = 300
            AppendLog("Writing: " & tag_STAOSET & " = 1", False)
            If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                    TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                    isCWBypassSet30Percent = True
                Else
                    AppendLog("Error at WriteVal", False)
                End If
                AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                    AppendLog("Error at WriteVal", False)
                End If
            Else
                AppendLog("Error at WriteVal", False)
            End If
        End If


        If isStarted And isCWBypassSet30Percent And Not isStabilized Then

            Select Case startUpEquipment
                Case "TR1"
                    ' TR-1 冷水流量確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_TR1FL, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_TR1FL", False)
                        Exit Sub
                    Else
                        TR1FL = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_TR1FL & " = " & Convert.ToString(TR1FL), False)
                        TextBoxTR1FL.Text = TR1FL.ToString()
                    End If
                    ' TR-1 冷水流量が定格の±1%の範囲に安定しているか確認
                    If CheckStability(TR1FL) Then
                        ' 流量制御を手動に切換
                        AppendLog("Writing: " & tag_TR1CW_AUTO_MAN_SWITCH & " = " & "False", False)
                        If AxIPLink1.WriteVal(tag_TR1CW_AUTO_MAN_SWITCH, False) Then
                        Else
                            AppendLog("Error at WriteVal", False)
                            Exit Sub
                        End If
                        AppendLog("Writing: " & tag_TR1CW_MAN & " = " & "True", False)
                        If AxIPLink1.WriteVal(tag_TR1CW_MAN, True) Then
                            isStabilized = True
                        Else
                            AppendLog("Error at WriteVal", False)
                            Exit Sub
                        End If
                    End If
                Case "TR2"
                    ' TR-2 冷水流量確認
                    vVal = Nothing
                    If Not AxIPLink1.ReadVal(tag_TR2FL, vVal, 1) Then
                        AppendLog("Error at ReadVal of tag_TR2FL", False)
                        Exit Sub
                    Else
                        TR2FL = Convert.ToDouble(vVal)
                        AppendLog("Reading:" & tag_TR2FL & " = " & Convert.ToString(TR2FL), False)
                        TextBoxTR2FL.Text = TR2FL.ToString()
                    End If
                    ' TR-2 冷水流量が定格の±1%の範囲に安定しているか確認
                    If CheckStability(TR2FL) Then
                        ' 流量制御を手動に切換
                        AppendLog("Writing: " & tag_TR2CW_AUTO_MAN_SWITCH & " = " & "False", False)
                        If AxIPLink1.WriteVal(tag_TR2CW_AUTO_MAN_SWITCH, False) Then
                        Else
                            AppendLog("Error at WriteVal", False)
                            Exit Sub
                        End If
                        AppendLog("Writing: " & tag_TR2CW_MAN & " = " & "True", False)
                        If AxIPLink1.WriteVal(tag_TR2CW_MAN, True) Then
                            isStabilized = True
                        Else
                            AppendLog("Error at WriteVal", False)
                            Exit Sub
                        End If
                    End If
            End Select

        End If


        If isStarted And isCWBypassSet30Percent And isStabilized Then

            ' 冷水バイパス弁開度 減
            vVal = Nothing
            If Not AxIPLink1.ReadVal(tag_CWBypassValve, vVal, 1) Then
                AppendLog("Error at ReadVal of tag_CWBypassValve", False)
                Exit Sub
            Else
                CWBypassValve = Convert.ToDouble(vVal)
                AppendLog("Reading:" & tag_CWBypassValve & " = " & Convert.ToString(CWBypassValve), False)
                TextBoxCWBypassValve.Text = CWBypassValve.ToString()
            End If

            Dim CWBypassDecrement As Double
            CWBypassDecrement = (300 - 30) / (CWBypassCloseLeadTime / NumericUpDownTimerInterval.Value)

            CWBypassValve = If(CWBypassValve - CWBypassDecrement > 30, CWBypassValve - CWBypassDecrement, 30)
            AppendLog("Writing: " & tag_STAOSET & " = 1", False)
            If AxIPLink1.WriteVal(tag_STAOSET, 1) Then
                AppendLog("Writing: " & tag_CWBypassValveSet & " = " & Convert.ToString(CWBypassValve), False)
                If AxIPLink1.WriteVal(tag_CWBypassValveSet, CWBypassValve) Then
                    TextBoxCWBypassValve.Text = CWBypassValve.ToString()
                    AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                Else
                    AppendLog("Error at WriteVal", False)
                    AppendLog("Writing: " & tag_STAOSET & " = 0", False)
                    If Not AxIPLink1.WriteVal(tag_STAOSET, 0) Then
                        AppendLog("Error at WriteVal", False)
                    End If
                    Exit Sub
                End If
            Else
                AppendLog("Error at WriteVal", False)
                Exit Sub
            End If

            ' 冷水バイパス弁開度確認
            If CWBypassValve <= 30 Then
                Application.Exit()
            End If

        End If


    End Sub

    Private Sub UpdateForm()

        Dim vVal As Object

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR1FLsp, vVal, 1) Then
            AR1FLsp = Convert.ToDouble(vVal)
            TextBoxAR1FLsp.Text = AR1FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR2FLsp, vVal, 1) Then
            AR2FLsp = Convert.ToDouble(vVal)
            TextBoxAR2FLsp.Text = AR2FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR3FLsp, vVal, 1) Then
            AR3FLsp = Convert.ToDouble(vVal)
            TextBoxAR3FLsp.Text = AR3FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR4FLsp, vVal, 1) Then
            AR4FLsp = Convert.ToDouble(vVal)
            TextBoxAR4FLsp.Text = AR4FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR5FLsp, vVal, 1) Then
            AR5FLsp = Convert.ToDouble(vVal)
            TextBoxAR5FLsp.Text = AR5FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_TR1FLsp, vVal, 1) Then
            TR1FLsp = Convert.ToDouble(vVal)
            TextBoxTR1FLsp.Text = TR1FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_TR2FLsp, vVal, 1) Then
            TR2FLsp = Convert.ToDouble(vVal)
            TextBoxTR2FLsp.Text = TR2FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_TR3FLsp, vVal, 1) Then
            TR3FLsp = Convert.ToDouble(vVal)
            TextBoxTR3FLsp.Text = TR3FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_TR4FLsp, vVal, 1) Then
            TR4FLsp = Convert.ToDouble(vVal)
            TextBoxTR4FLsp.Text = TR4FLsp.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR1CWOutValve, vVal, 1) Then
            AR1CWOutValve = Convert.ToDouble(vVal)
            TextBoxAR1CWOutValve.Text = AR1CWOutValve.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR2CWOutValve, vVal, 1) Then
            AR2CWOutValve = Convert.ToDouble(vVal)
            TextBoxAR2CWOutValve.Text = AR2CWOutValve.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR3CWOutValve, vVal, 1) Then
            AR3CWOutValve = Convert.ToDouble(vVal)
            TextBoxAR3CWOutValve.Text = AR3CWOutValve.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR4CWOutValve, vVal, 1) Then
            AR4CWOutValve = Convert.ToDouble(vVal)
            TextBoxAR4CWOutValve.Text = AR4CWOutValve.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_AR5CWOutValve, vVal, 1) Then
            AR5CWOutValve = Convert.ToDouble(vVal)
            TextBoxAR5CWOutValve.Text = AR5CWOutValve.ToString
        End If

        vVal = Nothing
        If AxIPLink1.ReadVal(tag_CWBypassValve, vVal, 1) Then
            CWBypassValve = Convert.ToDouble(vVal)
            TextBoxCWBypassValve.Text = CWBypassValve.ToString
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

    Public Function CheckStability(RecentFL As Double) As Boolean

        Dim now As DateTime = DateTime.Now

        ' 今回の値を履歴に追加
        history.Add((now, RecentFL))

        ' 過去30秒より古い履歴を削除
        history.RemoveAll(Function(h) (now - h.Timestamp).TotalSeconds > WindowSeconds)

        ' 履歴が1件（今回分）しかない場合は判定不能としてFalse
        If history.Count <= 1 Then
            Return False
        End If

        ' 直近30秒間の最大値・最小値を取得
        Dim maxVal As Double = history.Max(Function(h) h.Value)
        Dim minVal As Double = history.Min(Function(h) h.Value)

        ' 基準値は (最大値 + 最小値) / 2
        Dim baseValue As Double = (maxVal + minVal) / 2.0

        If baseValue = 0 Then
            ' ゼロ割防止：基準値が0なら差の絶対値で判定
            Return (maxVal - minVal) = 0
        End If

        Dim upperLimit As Double = baseValue * (1.0 + TolerancePercent)
        Dim lowerLimit As Double = baseValue * (1.0 - TolerancePercent)

        ' 直近30秒間の全ての値が ±2% の範囲内に収まっているか判定
        Return (maxVal <= upperLimit) AndAlso (minVal >= lowerLimit)

    End Function

End Class
