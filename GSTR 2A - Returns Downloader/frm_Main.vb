'=========================================================================='
'                                                                          '
'                    (C) Copyright 2018 Devil7 Softwares.                  '
'                                                                          '
' Licensed under the Apache License, Version 2.0 (the "License");          '
' you may not use this file except in compliance with the License.         '
' You may obtain a copy of the License at                                  '
'                                                                          '
'                http://www.apache.org/licenses/LICENSE-2.0                '
'                                                                          '
' Unless required by applicable law or agreed to in writing, software      '
' distributed under the License is distributed on an "AS IS" BASIS,        '
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. '
' See the License for the specific language governing permissions and      '
' limitations under the License.                                           '
'                                                                          '
' Contributors :                                                           '
'     Dineshkumar T                                                        '
'=========================================================================='

Imports DevExpress.XtraEditors.Controls
Imports OpenQA.Selenium

Imports Devil7.Automation.GSTR2A.Downloader.Classes.WebDriver
Imports Devil7.Automation.GSTR2A.Downloader.Classes.Native
Imports Devil7.Automation.GSTR2A.Downloader.Objects

Public Class frm_Main

#Region "Variables"

    Dim TempDir As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "GSTR2A_" & (New Random).Next(111111, 999999))
    Dim StoreDir As String

    Dim CurrentMonth As String = ""
    Dim Files2Move As New List(Of File)

    Dim SelectedItems As List(Of ReturnsDetails)

#End Region

#Region "Settings"
    Sub LoadSettings()
        If My.Settings.DownloadsFolder = "" Then
            txt_DownloadsLocation.EditValue = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        Else
            txt_DownloadsLocation.EditValue = My.Settings.DownloadsFolder
        End If
    End Sub

    Sub SaveSettings()
        My.Settings.DownloadsFolder = txt_DownloadsLocation.EditValue
        My.Settings.Save()
    End Sub

    Sub EnableControls()
        Me.Invoke(Sub()
                      grp_Credential.Enabled = True
                      grp_Downloads.Enabled = True
                      grp_Jobs.Enabled = True
                      grp_Months.Enabled = True
                      grp_Type.Enabled = True
                      btn_Start.Enabled = True
                      btn_Stop.Enabled = False
                      btn_Stop.Visible = False
                      TopMost = False
                  End Sub)
    End Sub

    Sub DisableControls()
        Me.Invoke(Sub()
                      grp_Credential.Enabled = False
                      grp_Downloads.Enabled = False
                      grp_Jobs.Enabled = False
                      grp_Months.Enabled = False
                      grp_Type.Enabled = False
                      btn_Start.Enabled = False
                      btn_Stop.Enabled = True
                      btn_Stop.Visible = True
                      TopMost = True
                  End Sub)
    End Sub

    Public Sub Write2Console(ByVal Text As String, ByVal Color As Color)
        Try
            Me.Invoke(Sub()
                          txt_Console.SelectionColor = Color
                          txt_Console.AppendText(Text)
                          PostMessage(txt_Console.Handle, WM_VSCROLL, CType(SB_BOTTOM, IntPtr), CType(IntPtr.Zero, IntPtr))
                      End Sub)
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Sub FindFirefox()
        On Error Resume Next
        For Each i As String In My.Computer.FileSystem.GetFiles(Application.StartupPath, FileIO.SearchOption.SearchAllSubDirectories, "firefox.exe")
            My.Settings.FireFoxLocation = i
            My.Settings.Save()
            Exit For
        Next
    End Sub

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gc_Months.DataSource = Classes.MiscFunctions.GetAssessmentMonths
        LoadSettings()
        If My.Settings.FireFoxLocation = "" OrElse My.Computer.FileSystem.FileExists(My.Settings.FireFoxLocation) Then
            FindFireFox()
        End If
    End Sub

    Private Sub txt_DownloadsLocation_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles txt_DownloadsLocation.ButtonClick
        SelectDownloadsDialog.SelectedPath = txt_DownloadsLocation.EditValue
        If SelectDownloadsDialog.ShowDialog() = DialogResult.OK Then
            txt_DownloadsLocation.EditValue = SelectDownloadsDialog.SelectedPath
            SaveSettings()
        End If
    End Sub

    Private Sub btn_Start_Click(sender As Object, e As EventArgs) Handles btn_Start.Click
        SelectedItems = New List(Of ReturnsDetails)
        For Each i As ReturnsDetails In CType(gc_Months.DataSource, List(Of ReturnsDetails))
            If i.Process Then SelectedItems.Add(i)
        Next

        If SelectedItems.Count > 0 Then
            DisableControls()
            Me.Location = New Point(My.Computer.Screen.WorkingArea.Width - Me.Width, My.Computer.Screen.WorkingArea.Height - Me.Height)
            Worker.RunWorkerAsync()
            StoreDir = txt_DownloadsLocation.EditValue
            If My.Computer.FileSystem.DirectoryExists(StoreDir) = False Then
                My.Computer.FileSystem.CreateDirectory(StoreDir)
            End If
        Else
            MsgBox("Select atleast one month to start the process.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
        End If
    End Sub

    Private Sub Worker_Login_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Write2Console("Starting/Restarting Firefox Driver..." & vbNewLine & vbNewLine, Color.Yellow)
        If Not StartDriver(TempDir) Then Exit Sub

        Write2Console("Navigating to GST Login Page...", Color.Yellow)
        Driver.Navigate().GoToUrl("https://services.gst.gov.in/services/login")
        WaitForLoad(Me)
        Threading.Thread.Sleep(2000)

        Dim Username As IWebElement = Nothing
        Do Until Username IsNot Nothing
            Threading.Thread.Sleep(1000)
            Try
                Username = Driver.FindElement(By.Id("username"))
            Catch ex As Exception

            End Try
        Loop
        Write2Console("Filling Username & Password..." & vbNewLine & vbNewLine, Color.Yellow)
        Username.SendKeys(txt_LoginID.EditValue)
        Driver.FindElement(By.Id("user_pass")).SendKeys(txt_Password.EditValue)
        Driver.FindElement(By.Id("captcha")).Click()

        Write2Console("Waiting for User to Enter Captcha & Login...", Color.Red)
        Try
            Do Until Driver.Url = "https://services.gst.gov.in/services/auth/fowelcome"
                Threading.Thread.Sleep(1000)
            Loop
        Catch ex As Exception

        End Try
        WaitForLoad(Me)
        Threading.Thread.Sleep(3000)

        Write2Console("Navigating to GST Returns Dashboard...", Color.Yellow)
        ClickButtonByText("RETURN DASHBOARD")
        WaitForLoad(Me)

        Me.Invoke(Sub()
                      ProgressBar.Properties.Maximum = SelectedItems.Count
                      ProgressBar.EditValue = 0
                  End Sub)

        If Jobs.EditValue = 0 Then
            For Each i As ReturnsDetails In SelectedItems
                Me.Invoke(Sub() ProgressBar.EditValue += 1)
                RequestGSTR(i.Month, i.Year, Types.EditValue, Me)
            Next
        ElseIf Jobs.EditValue = 1 Then
            DownloadsWatcher.Path = TempDir
            DownloadsWatcher.EnableRaisingEvents = True
            For Each i As ReturnsDetails In SelectedItems
                Me.Invoke(Sub() ProgressBar.EditValue += 1)
                CurrentMonth = i.Month
                DownloadGSTR(i.Month, i.Year, Types.EditValue, Me)
            Next
        End If

        Write2Console("Logging Out..." & vbNewLine & vbNewLine, Color.Yellow)
        Driver.Navigate().GoToUrl("https://services.gst.gov.in/services/logout")
        DownloadsWatcher.EnableRaisingEvents = False
        Write2Console("Moving Files. Please Wait..." & vbNewLine & vbNewLine, Color.Yellow)
        For Each i As File In Files2Move
            Write2Console("Moving File of " & i.Month & "..." & vbNewLine & vbNewLine, Color.Green)
            i.Move()
        Next
        Write2Console("Process Completed... :-)" & vbNewLine & vbNewLine, Color.Green)
        EnableControls()
        Me.Invoke(Sub() MsgBox("Process Completed... :-)", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Done"))
        Try
            Driver.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_Stop_Click(sender As Object, e As EventArgs) Handles btn_Stop.Click
        EnableControls()
        If Worker.IsBusy Then
            Worker.CancelAsync()
        End If
        If Driver IsNot Nothing Then Driver.Close()
        Classes.MiscFunctions.KillGeckoProcesses()
    End Sub

    Private Sub DownloadsWatcher_Created(sender As Object, e As IO.FileSystemEventArgs) Handles DownloadsWatcher.Created
        Dim S As String() = IO.Path.GetFileNameWithoutExtension(e.FullPath).Split("_")
        Dim DestName As String = String.Format("{0}_{1}_{2}_{3}_{4}.zip", S(0), S(1), S(2), S(3), CurrentMonth.Substring(0, 3).ToUpper)
        txt_Console.Focus()
        txt_Console.AppendText(CurrentMonth & " - File Found." & IO.Path.GetFileName(e.FullPath) & vbNewLine)
        txt_Console.AppendText("Will move File to " & DestName & vbNewLine)
        Files2Move.Add(New File(CurrentMonth, e.FullPath, IO.Path.Combine(StoreDir, DestName)))
    End Sub

    Private Sub frm_Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Classes.MiscFunctions.KillGeckoProcesses()
    End Sub

    Private Sub btn_About_Click(sender As Object, e As EventArgs) Handles btn_About.Click
        Dim d As New frm_About
        d.ShowDialog()
    End Sub

    Private Sub frm_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If My.Settings.FirstRun Then
            My.Settings.FirstRun = False
            My.Settings.Save()
            Dim d As New frm_About
            d.ShowDialog()
        End If
    End Sub

    Private Sub btn_FeedBack_Click(sender As Object, e As EventArgs) Handles btn_FeedBack.Click
        Dim d As New frm_Feedback
        d.ShowDialog()
    End Sub

    Private Sub btn_Settings_Click(sender As Object, e As EventArgs) Handles btn_Settings.Click
        Dim d As New frm_Settings
        d.ShowDialog()
    End Sub

End Class
