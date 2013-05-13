﻿Imports System
Imports System.Data
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Management
Imports System.ServiceProcess
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Inherits System.Windows.Forms.Form
    Dim a0 As DateTime = #6/13/2010 1:00:00 PM#
    Dim a1 As DateTime = #6/13/2010 1:00:10 PM#
    Dim outtime As TimeSpan = a1 - a0
    Dim conn As MySqlConnection
    Dim data As DataTable
    Dim da As MySqlDataAdapter
    Dim cb As MySqlCommandBuilder


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim sspvpgn As New ServiceController("pvpgn")
        'Dim ssd2cs As New ServiceController("d2cs")
        'Dim ssd2dbs As New ServiceController("d2dbs")
        'Dim pvpgnname As String
        'Dim d2csname As String
        'Dim d2dbsname As String
        'Dim d2gsname As String
        'pvpgnname = sspvpgn.ServiceName.ToString
        'd2csname = ssd2cs.ServiceName.ToString
        'd2dbsname = ssd2dbs.ServiceName.ToString
        'd2gsname = ssd2gs.ServiceName.ToString
        'If pvpgnname = "" Then
        'Else
        'Select Case sspvpgn.Status
        '    Case ServiceControllerStatus.Running
        'Label15.Text = "正在运行"
        '   Case ServiceControllerStatus.Stopped
        'Label15.Text = "已停止"
        'End Select
        'End If

        '        If d2csname = "" Then
        'Select Case ssd2cs.Status
        '   Case ServiceControllerStatus.Running
        'Label16.Text = "正在运行"
        '    Case ServiceControllerStatus.Stopped
        'Label16.Text = "已停止"
        'End Select
        'End If

        '        If d2dbsname = "" Then
        'Select Case ssd2dbs.Status
        '   Case ServiceControllerStatus.Running
        'Label17.Text = "正在运行"
        '   Case ServiceControllerStatus.Stopped
        'Label17.Text = "已停止"
        'End Select
        '     End If

        If DateString > "2013-07-01" Then
            Close()
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Not conn Is Nothing Then conn.Close()

        Dim connStr As String
        connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", _
    TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text)
        Try
            conn = New MySqlConnection(connStr)
            conn.Open()
            Button1.Text = "已连接"
            Button1.Enabled = False
            'GetDatabases()
            'Catch ex As MySqlException
            '
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    MessageBox.Show("账号密码不对")
                Case 1042
                    MessageBox.Show("找不到服务器")

            End Select
            'MessageBox.Show(ex.Number)
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim selectpvpgn As New MySqlCommand("SELECT * FROM `pvpgn_bnet` LIMIT 0, 1000", conn)
        Dim setadminstr As String
        setadminstr = String.Format("UPDATE `pvpgn_bnet` SET `auth_admin`='ture' WHERE (`username`='{0}') LIMIT 1", TextBox5.Text)
        Dim setadmin As New MySqlCommand(setadminstr, conn)
        selectpvpgn.ExecuteNonQuery()
        setadmin.ExecuteNonQuery()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim selectpvpgn As New MySqlCommand("SELECT * FROM `pvpgn_bnet` LIMIT 0, 1000", conn)
        Dim unsetadminstr As String
        unsetadminstr = String.Format("UPDATE `pvpgn_bnet` SET `auth_admin`='false' WHERE (`username`='{0}') LIMIT 1", TextBox5.Text)
        Dim unsetadmin As New MySqlCommand(unsetadminstr, conn)
        selectpvpgn.ExecuteNonQuery()
        unsetadmin.ExecuteNonQuery()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim selectpvpgn As New MySqlCommand("SELECT * FROM `pvpgn_bnet` LIMIT 0, 1000", conn)
        Dim deluserstr As String
        deluserstr = String.Format("DELETE FROM `pvpgn_bnet` WHERE (`username`='{0}') LIMIT 1", TextBox5.Text)
        Dim deluser As New MySqlCommand(deluserstr, conn)
        selectpvpgn.ExecuteNonQuery()
        deluser.ExecuteNonQuery()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
        ElseIf TextBox5.Text <> "" And Button1.Enabled = False Then
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True

        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim pathbnet As New MySqlCommand("ALTER TABLE `pvpgn_bnet` ADD COLUMN `flags_initial`  int(11) NULL AFTER `acct_ctime`;", conn)
        pathbnet.ExecuteNonQuery()
    End Sub

    Private Sub Button1_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.EnabledChanged
        If Button1.Enabled = False Then
            Button6.Enabled = True
            Button22.Enabled = True
            Button33.Enabled = True
            Button34.Enabled = True
            Button35.Enabled = True
        End If
    End Sub
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If Button3.Enabled = True And TextBox6.Text <> "" Then
            Button7.Enabled = True
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim selectpvpgn As New MySqlCommand("SELECT * FROM `pvpgn_bnet` LIMIT 0, 1000", conn)
        Dim setflagsstr As String
        If TextBox6.Text = "NULL" Then
            setflagsstr = String.Format("UPDATE `pvpgn_bnet` SET `flags_initial`=NULL WHERE (`username`='{0}') LIMIT 1", TextBox5.Text)
        Else
            setflagsstr = String.Format("UPDATE `pvpgn_bnet` SET `flags_initial`='{0}' WHERE (`username`='{1}') LIMIT 1", TextBox6.Text, TextBox5.Text)
        End If
        Dim setflags As New MySqlCommand(setflagsstr, conn)
        selectpvpgn.ExecuteNonQuery()
        setflags.ExecuteNonQuery()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("mailto://yjfyy@163.com")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("http://hi.baidu.com/yjfyy")
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        'MessageBox.Show("安装开始后会出现CMD窗口，当显示""Installing Service""后关掉CMD窗口")
        Shell("PvPGNConsole.exe -s install", vbHide)
        'MessageBox.Show(i)
        MessageBox.Show("PvPGN已安装")
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim sspvpgn As New ServiceController("pvpgn")
        If sspvpgn.Status.Equals(ServiceControllerStatus.Running) Then
            MessageBox.Show("请停止PvPGN后重试")
        Else
            Shell("PvPGNConsole.exe -s uninstall", vbHide)
            MessageBox.Show("卸载完成")
        End If
    End Sub




  

  








 

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sspvpgn As New ServiceController("pvpgn")
        sspvpgn.Refresh()
        MessageBox.Show("已重启")
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ssd2cs As New ServiceController("d2cs")
        ssd2cs.Refresh()
        MessageBox.Show("已重启")
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ssd2dbs As New ServiceController("d2dbs")
        ssd2dbs.Refresh()
        MessageBox.Show("已重启")
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sspvpgn As New ServiceController("pvpgn")
        Dim ssd2cs As New ServiceController("d2cs")
        Dim ssd2dbs As New ServiceController("d2dbs")
        sspvpgn.Refresh()
        ssd2cs.Refresh()
        ssd2dbs.Refresh()
        MessageBox.Show("已全部重启")
    End Sub

   
 

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Shell("explorer.exe .\conf\", AppWinStyle.MaximizedFocus)
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Shell(".\d2gs\d2gs.reg", vbHide)
        
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub


    Private Sub Button24_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        d2gsdefconf()
        Shell(".\d2gs\d2gssvc.exe -i", vbHide)
        MsgBox("安装完成")
    End Sub

    Private Sub Button26_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim ssd2gs As New ServiceController("d2gs")
        If ssd2gs.Status.Equals(ServiceControllerStatus.Running) Then
            MessageBox.Show("请停止D2GS服务后重试")
        Else
            My.Computer.Registry.LocalMachine.DeleteSubKey("SOFTWARE\D2Server\D2GS")
            My.Computer.Registry.LocalMachine.DeleteSubKey("SOFTWARE\D2Server")
            Shell(".\d2gs\d2gssvc.exe -u", vbHide)
            MessageBox.Show("卸载完成")
        End If
    End Sub


    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Button31.Text = "正在复制"
        Button31.Enabled = False
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        Dim i
        Dim d2gsneedfiles(3) As String
        d2gsneedfiles(0) = "d2data.mpq"
        d2gsneedfiles(1) = "d2exp.mpq"
        d2gsneedfiles(2) = "d2speech.mpq"
        d2gsneedfiles(3) = "d2sfx.mpq"
        For i = 0 To 3
            If System.IO.File.Exists(TextBox7.Text + "\" + d2gsneedfiles(i)) Then
                ProgressBar1.Value = ProgressBar1.Value + 20
                System.IO.File.Copy(TextBox7.Text + "\" + d2gsneedfiles(i), ".\d2gs\" + d2gsneedfiles(i), True)
            Else : MsgBox(d2gsneedfiles(i) + "没有找到")
            End If
        Next
        ProgressBar1.Value = 100
        ProgressBar1.Visible = False
        Button31.Text = "复制所需文件"
        MsgBox("复制完成")
        Button31.Enabled = True
    End Sub






    Private Sub d2gsdefconf()
        Dim d2gsregname As String = "HKEY_LOCAL_MACHINE\SOFTWARE\D2Server\D2GS"
        Microsoft.Win32.Registry.SetValue(d2gsregname, "", """Diablo II Close Game Server""")
        Microsoft.Win32.Registry.SetValue(d2gsregname, "D2CSPort", 6113, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "D2DBSPort", 6114, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "D2CSSecrect", "")
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MaxPreferUsers", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MaxGameLife", 31372, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AdminPassword", "9e75a42100e1b9e0b5d3873045084fae699adcb0")
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AdminPort", 8888, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AdminTimeout", 3600, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnableNTMode", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnablePreCacheMode", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "IdleSleep", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "BusySleep", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "CharPendingTimeout", 600, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnableGELog", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "DebugNetPacket", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "DebugEventCallback", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnableGEMsg", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "IntervalReconnectD2CS", 50, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnableGEPatch", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AutoUpdate", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "GSShutdownInterval", 15, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MultiCPUMask", 1, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "EnableGSLog", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MOTD", "")
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AutoUpdateUrl", "http://166.111.4.64/update.script")
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AutoUpdateVer", 0, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "AutoUpdateTimeout", 3600, Microsoft.Win32.RegistryValueKind.DWord)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MaxPacketPerSecond", 1200, Microsoft.Win32.RegistryValueKind.DWord)
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        'Dim maxgames As Integer
        'maxgames = CInt(TextBox10.Text)
        Dim d2gsregname As String = "HKEY_LOCAL_MACHINE\SOFTWARE\D2Server\D2GS"
        Microsoft.Win32.Registry.SetValue(d2gsregname, "D2CSIP", TextBox8.Text)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "D2DBSIP", TextBox9.text)
        Microsoft.Win32.Registry.SetValue(d2gsregname, "MaxGames", TextBox10.Text, Microsoft.Win32.RegistryValueKind.DWord)
        MsgBox("设置成功")
    End Sub

    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        Dim deletepvpgnstr As String
        deletepvpgnstr = String.Format("drop database pvpgn")
        Dim deletepvpgn As New MySqlCommand(deletepvpgnstr, conn)
        Try
            deletepvpgn.ExecuteNonQuery()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 1007
                    MessageBox.Show("请勿重复初始化数据库！")
                    Exit Sub
            End Select
            MessageBox.Show(ex.Number)
            MessageBox.Show(ex.Message)
        End Try
        MsgBox("数据库以清除！")
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim createpvpgnstr As String
        createpvpgnstr = String.Format("create database pvpgn")
        Dim createpvpgn As New MySqlCommand(createpvpgnstr, conn)
        Try
            createpvpgn.ExecuteNonQuery()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 1007
                    MessageBox.Show("请勿重复初始化数据库！")
                    Exit Sub
            End Select
            'MessageBox.Show(ex.Number)
            'essageBox.Show(ex.Message)
        End Try
        MsgBox("数据库初始化成功！")
    End Sub



    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        Dim bakdatestr As String
        bakdatestr = Format(Now, "yyyy-MM-dd_HH.mm")
        Try
            Shell("mysqldump.exe --host=" + TextBox1.Text + " --user=" + TextBox2.Text + " --password=" + TextBox3.Text + " --databases pvpgn --result-file=.\sqlbak\pvpgnbak" + bakdatestr + ".sql", AppWinStyle.Hide)
            MessageBox.Show("备份数据成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        Try
            Shell("cmd /c mysql.exe --user=" + TextBox2.Text + " --password=" + TextBox3.Text + " < .\sqlbak\" + TextBox11.Text, AppWinStyle.Hide)
            MessageBox.Show("还数据成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click
        Shell("d2csConsole.exe -s install", vbHide)
        'MessageBox.Show(i)
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        Shell("d2dbsConsole.exe -s install", vbHide)
        'MessageBox.Show(i)
        MsgBox("D2DBS已安装")
    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        Dim ssd2cs As New ServiceController("d2cs109")
        If ssd2cs.Status.Equals(ServiceControllerStatus.Running)Then
            MessageBox.Show("请停止D2CS服务后重试")
        Else
            Shell("d2csConsole.exe -s uninstall", vbHide)
            'MessageBox.Show(i)
            MessageBox.Show("卸载完成")
        End If
    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        Dim ssd2dbs As New ServiceController("d2dbs109")

        If ssd2dbs.Status.Equals(ServiceControllerStatus.Running) Then
            MessageBox.Show("请停止D2DBS服务后重试")
        Else
            Shell("d2dbsConsole.exe -s uninstall", vbHide)
            'MessageBox.Show(i)
            MessageBox.Show("卸载完成")
        End If
    End Sub


    Public Sub stop_pvpgn_server()
        Dim sspvpgn As New ServiceController("pvpgn")
        Try
            If sspvpgn.Status <> ServiceControllerStatus.Stopped Then
                sspvpgn.Stop()
                sspvpgn.WaitForStatus(ServiceControllerStatus.Stopped, outtime)
            End If
        Catch ex As Exception
            MsgBox("PvPGN未能停止，请重试，多次重试仍然不行请重启计算机")
        End Try
    End Sub

    Public Sub run_pvpgn_server()
        Dim sspvpgn As New ServiceController("pvpgn")
        Try
            sspvpgn.Start()
            sspvpgn.WaitForStatus(ServiceControllerStatus.Running, outtime)
        Catch When sspvpgn.Status = ServiceControllerStatus.Running
            MessageBox.Show("PvPGN正在运行，不能重复启动")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("不能启动PvPGN,请检查bnet.conf各项配置")
            Exit Sub
        End Try
    End Sub

    Public Sub stop_d2cs_server()
        Dim ssd2cs As New ServiceController("d2cs109")
        Try
            If ssd2cs.Status <> ServiceControllerStatus.Stopped Then

                ssd2cs.Stop()
                ssd2cs.WaitForStatus(ServiceControllerStatus.Stopped, outtime)
            End If
        Catch ex As Exception
            MsgBox("D2CS未能停止，请重试，多次重试仍然不行请重启计算机")
        End Try
    End Sub

    Public Sub run_d2cs_server()
        Dim ssd2cs As New ServiceController("d2cs109")
        Try
            ssd2cs.Start()
            ssd2cs.WaitForStatus(ServiceControllerStatus.Running, outtime)
        Catch When ssd2cs.Status = ServiceControllerStatus.Running
            MessageBox.Show("D2CS正在运行，不能重复启动")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("不能启动D2CS,请检查d2cs.conf各项配置")
            Exit Sub
        End Try
    End Sub

    Public Sub stop_d2dbs_server()
        Dim ssd2dbs As New ServiceController("d2dbs109")
        Try
            If ssd2dbs.Status <> ServiceControllerStatus.Stopped Then
                ssd2dbs.Stop()
                ssd2dbs.WaitForStatus(ServiceControllerStatus.Stopped, outtime)
            End If
        Catch ex As Exception
            MsgBox("D2DBS未能停止，请重试，多次重试仍然不行请重启计算机")
        End Try
    End Sub

    Public Sub run_d2dbs_server()
        Dim ssd2dbs As New ServiceController("d2dbs109")
        Try
            ssd2dbs.Start()
            ssd2dbs.WaitForStatus(ServiceControllerStatus.Running, outtime)
        Catch When ssd2dbs.Status = ServiceControllerStatus.Running
            MessageBox.Show("D2DBS正在运行，不能重复启动")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("不能启动D2DBS,请检查d2dbs.conf各项配置")
            Exit Sub
        End Try
    End Sub

    Public Sub stop_d2gs_server()
        Dim ssd2gs As New ServiceController("d2gs")
        Try
            If ssd2gs.Status <> ServiceControllerStatus.Stopped Then
                ssd2gs.Stop()
                ssd2gs.WaitForStatus(ServiceControllerStatus.Stopped, outtime)
            End If
        Catch ex As Exception
            MsgBox("D2GS未能停止，请重试，多次重试仍然不行请重启计算机")
        End Try
    End Sub

    Public Sub run_d2gs_server()
        Dim ssd2gs As New ServiceController("d2gs")
        Try
            ssd2gs.Start()
            ssd2gs.WaitForStatus(ServiceControllerStatus.Running, outtime)
        Catch When ssd2gs.Status = ServiceControllerStatus.Running
            MessageBox.Show("D2GS正在运行，不能重复启动")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("不能启动D2GS,请检查D2GS配置")
            Exit Sub
        End Try
    End Sub

    Private Sub Button_restart_pvpgn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_restart_pvpgn.Click
        stop_pvpgn_server()
        run_pvpgn_server()
        MessageBox.Show("重启指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_restart_d2cs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_restart_d2cs.Click
        stop_d2cs_server()
        run_d2cs_server()
        MessageBox.Show("重启指令执行完毕,请点击刷新查看服务状态。")
    End Sub


    Private Sub Button_restart_d2dbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_restart_d2dbs.Click
        stop_d2dbs_server()
        run_d2dbs_server()
        MessageBox.Show("重启指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_restart_d2gs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_restart_d2gs.Click
        stop_d2gs_server()
        run_d2gs_server()
        MessageBox.Show("重启指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_stop_pvpgn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_stop_pvpgn.Click
        stop_pvpgn_server()
        MessageBox.Show("停止指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_stop_d2cs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_stop_d2cs.Click
        stop_d2cs_server()
        MessageBox.Show("停止指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_stop_d2dbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_stop_d2dbs.Click
        stop_d2dbs_server()
        MessageBox.Show("停止指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_stop_d2gs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_stop_d2gs.Click
        stop_d2gs_server()
        MessageBox.Show("停止指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_stop_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_stop_select.Click
        If CheckBox_pvpgn.Checked Then
            stop_pvpgn_server()
        End If

        If CheckBox_d2cs.Checked Then
            stop_d2cs_server()
        End If

        If CheckBox_d2dbs.Checked Then
            stop_d2dbs_server()
        End If

        If CheckBox_d2gs.Checked Then
            stop_d2gs_server()
        End If

        MsgBox("停止指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_restart_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_restart_select.Click
        If CheckBox_pvpgn.Checked = True Then
            stop_pvpgn_server()
            run_pvpgn_server()
        End If

        If CheckBox_d2cs.Checked = True Then
            stop_d2cs_server()
            run_d2cs_server()
        End If

        If CheckBox_d2dbs.Checked = True Then
            stop_d2dbs_server()
            run_d2dbs_server()
        End If

        If CheckBox_d2gs.Checked = True Then
            stop_d2gs_server()
            run_d2gs_server()
        End If
        MessageBox.Show("重启指令执行完毕,请点击刷新查看服务状态。")
    End Sub

    Private Sub Button_refurbish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_refurbish.Click
        Dim sspvpgn As New ServiceController("pvpgn")
        Dim ssd2cs As New ServiceController("d2cs109")
        Dim ssd2dbs As New ServiceController("d2dbs109")
        Dim ssd2gs As New ServiceController("d2gs")
        Try
            Select Case sspvpgn.Status
                Case ServiceControllerStatus.Running
                    Label15.Text = "正在运行"
                Case ServiceControllerStatus.Stopped
                    Label15.Text = "已停止"
            End Select
        Catch ex As Exception

        End Try

        Try
            Select Case ssd2cs.Status
                Case ServiceControllerStatus.Running
                    Label16.Text = "正在运行"
                Case ServiceControllerStatus.Stopped
                    Label16.Text = "已停止"
            End Select
        Catch ex As Exception

        End Try

        Try
            Select Case ssd2dbs.Status
                Case ServiceControllerStatus.Running
                    Label17.Text = "正在运行"
                Case ServiceControllerStatus.Stopped
                    Label17.Text = "已停止"
            End Select
        Catch ex As Exception

        End Try

        Try
            Select Case ssd2gs.Status
                Case ServiceControllerStatus.Running
                    Label18.Text = "正在运行"
                Case ServiceControllerStatus.Stopped
                    Label18.Text = "已停止"
            End Select
        Catch ex As Exception

        End Try
    End Sub


End Class
