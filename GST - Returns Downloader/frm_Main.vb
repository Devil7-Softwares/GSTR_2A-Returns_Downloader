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

Imports Devil7.Automation.GSTR.Downloader.Classes.WebDriver
Imports Devil7.Automation.GSTR.Downloader.Classes.Native
Imports Devil7.Automation.GSTR.Downloader.Objects

Public Class frm_Main

#Region "Variables"

    Dim TempDir As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "GSTR2A_" & (New Random).Next(111111, 999999))
    Dim StoreDir As String

    Dim CurrentMonth As String = ""

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
                      grp_Returns.Enabled = True
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
                      grp_Returns.Enabled = False
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
        LoadSettings()
        If My.Settings.FireFoxLocation = "" OrElse My.Computer.FileSystem.FileExists(My.Settings.FireFoxLocation) Then
            FindFirefox()
        End If

        For Each i As Integer In [Enum].GetValues(GetType(Classes.Returns))
            Returns.Properties.Items.Add(New RadioGroupItem(i, [Enum].GetName(GetType(Classes.Returns), i).Split("_")(1)))
        Next
        Returns.EditValue = 0
    End Sub

    Private Sub txt_DownloadsLocation_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles txt_DownloadsLocation.ButtonClick
        SelectDownloadsDialog.SelectedPath = txt_DownloadsLocation.EditValue
        If SelectDownloadsDialog.ShowDialog() = DialogResult.OK Then
            txt_DownloadsLocation.EditValue = SelectDownloadsDialog.SelectedPath
            SaveSettings()
        End If
    End Sub

    Private Sub btn_Start_Click(sender As Object, e As EventArgs) Handles btn_Start.Click
        If txt_LoginID.Text.Trim = "" Then
            MsgBox("Login ID cannot be empty...!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        If txt_Password.Text.Trim = "" Then
            MsgBox("Password cannot be empty...!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

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
        If Not StartDriver(TempDir, Types.EditValue) Then Exit Sub

        Write2Console("Navigating to GST Login Page...", Color.Yellow)
        Dim tries As Integer = 0
        Do Until tries >= 5
            Try
                Driver.Navigate().GoToUrl("https://services.gst.gov.in/services/login")
                Exit Do
            Catch ex As Exception

            End Try
        Loop
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

        Try
            Do Until Driver.Url = "https://return.gst.gov.in/returns/auth/dashboard"
                Threading.Thread.Sleep(1000)
            Loop
        Catch ex As Exception

        End Try

        Me.Invoke(Sub()
                      ProgressBar.Properties.Maximum = SelectedItems.Count
                      ProgressBar.EditValue = 0
                  End Sub)


        Select Case Returns.EditValue
            Case Classes.Returns.GSTR_1
                For Each i As ReturnsDetails In SelectedItems
                    Me.Invoke(Sub() ProgressBar.EditValue += 1)
                    If Jobs.EditValue = 0 Then
                        RequestGSTR1(i.Month, i.Year, Me)
                    ElseIf Jobs.EditValue = 1 Then
                        Select Case Types.EditValue
                            Case Enums.FileType.JSON
                                DownloadGSTR1JSON(i.Month, i.Year, Me)
                            Case Enums.FileType.PDF
                                DownloadGSTR1PDF(i.Month, i.Year, Me)
                        End Select
                    End If
                Next
            Case Classes.Returns.GSTR_2A
                If Jobs.EditValue = 0 Then
                    For Each i As ReturnsDetails In SelectedItems
                        Me.Invoke(Sub() ProgressBar.EditValue += 1)
                        RequestGSTR2A(i.Month, i.Year, Types.EditValue, Me)
                    Next
                ElseIf Jobs.EditValue = 1 Then
                    For Each i As ReturnsDetails In SelectedItems
                        Me.Invoke(Sub() ProgressBar.EditValue += 1)
                        CurrentMonth = i.Month
                        DownloadGSTR2A(i.Month, i.Year, Types.EditValue, Me)
                    Next
                End If
            Case Classes.Returns.GSTR_3B
                For Each i As ReturnsDetails In SelectedItems
                    Me.Invoke(Sub() ProgressBar.EditValue += 1)
                    DownloadGSTR3B(i.Month, i.Year, Me)
                Next
            Case Classes.Returns.GSTR_4
                For Each i As ReturnsDetails In SelectedItems
                    Me.Invoke(Sub() ProgressBar.EditValue += 1)
                    DownloadGSTR4(i.Month, i.Year, Me)
                Next
            Case Classes.Returns.GSTR_4A
                If Jobs.EditValue = 0 Then
                    For Each i As ReturnsDetails In SelectedItems
                        Me.Invoke(Sub() ProgressBar.EditValue += 1)
                        RequestGSTR4A(i.Month, i.Year, Me)
                    Next
                ElseIf Jobs.EditValue = 1 Then
                    For Each i As ReturnsDetails In SelectedItems
                        Me.Invoke(Sub() ProgressBar.EditValue += 1)
                        CurrentMonth = i.Month
                        DownloadGSTR4A(i.Month, i.Year, Me)
                    Next
                End If
        End Select

        Write2Console("Logging Out..." & vbNewLine & vbNewLine, Color.Yellow)
        Driver.Navigate().GoToUrl("https://services.gst.gov.in/services/logout")
        Write2Console("Moving Files. Please Wait..." & vbNewLine & vbNewLine, Color.Yellow)
        For Each i As String In My.Computer.FileSystem.GetFiles(TempDir)
            If Types.EditValue = 0 Then
                Dim Month As String = Classes.MiscFunctions.GetMonth(i)
                Write2Console("Moving File of " & Month & "..." & vbNewLine & vbNewLine, Color.Green)
                Dim S As String() = IO.Path.GetFileNameWithoutExtension(i).Split("_")
                Dim DestName As String = String.Format("{0}_{1}_{2}_{3}_{4}.zip", S(0), S(1), S(2), S(3), Month.Substring(0, 3).ToUpper)
                My.Computer.FileSystem.MoveFile(i, IO.Path.Combine(StoreDir, DestName))
            Else
                Dim Dest As String = IO.Path.GetFileName(i)
                Write2Console("Moving File """ & Dest & """..." & vbNewLine & vbNewLine, Color.Green)
                My.Computer.FileSystem.MoveFile(i, IO.Path.Combine(StoreDir, Dest), True)
            End If
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

    Private Sub frm_Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Classes.MiscFunctions.KillGeckoProcesses()
    End Sub

    Private Sub btn_About_Click(sender As Object, e As EventArgs) Handles btn_About.Click
        Dim d As New frm_About
        d.ShowDialog()
    End Sub

    Private Sub frm_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim EmptyMenu As New ContextMenu
        txt_LoginID.ContextMenu = EmptyMenu
        txt_Password.ContextMenu = EmptyMenu
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

    Private Sub Returns_EditValueChanged(sender As Object, e As EventArgs) Handles Returns.EditValueChanged
        On Error Resume Next
        Jobs.Properties.Items.Item(0).Enabled = (Returns.EditValue = Classes.Returns.GSTR_2A Or Returns.EditValue = Classes.Returns.GSTR_4A Or Returns.EditValue = Classes.Returns.GSTR_1)
        Types.Properties.Items.Item(0).Enabled = (Returns.EditValue = Classes.Returns.GSTR_2A Or Returns.EditValue = Classes.Returns.GSTR_4A Or Returns.EditValue = Classes.Returns.GSTR_1)
        Types.Properties.Items.Item(1).Enabled = (Returns.EditValue = Classes.Returns.GSTR_2A)
        Types.Properties.Items.Item(2).Enabled = (Returns.EditValue <> Classes.Returns.GSTR_2A AndAlso Returns.EditValue <> Classes.Returns.GSTR_4A)

        Jobs.EditValue = If(Returns.EditValue = Classes.Returns.GSTR_2A Or Returns.EditValue = Classes.Returns.GSTR_4A, 0, 1)
        Types.EditValue = If(Returns.EditValue = Classes.Returns.GSTR_2A Or Returns.EditValue = Classes.Returns.GSTR_4A, 0, 2)

        Select Case Returns.EditValue
            Case Classes.Returns.GSTR_1, Classes.Returns.GSTR_2A, Classes.Returns.GSTR_3B
                gc_Months.DataSource = Classes.MiscFunctions.GetAssessmentMonths
            Case Classes.Returns.GSTR_4, Classes.Returns.GSTR_4A
                gc_Months.DataSource = Classes.MiscFunctions.GetAssessmentQuarters
        End Select
    End Sub

    Private Sub txt_LoginID_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_LoginID.KeyDown
        If e.Control AndAlso e.KeyCode = System.Windows.Forms.Keys.V Then
            If My.Computer.Clipboard.GetText.Contains(vbTab) Then
                Try
                    Dim LoginID As String = My.Computer.Clipboard.GetText.Split(vbTab)(0)
                    Dim Password As String = My.Computer.Clipboard.GetText.Split(vbTab)(1)
                    If LoginID <> "" And Password <> "" Then
                        Application.DoEvents()
                        txt_LoginID.EditValue = LoginID
                        txt_Password.Text = Password
                        e.Handled = True
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub Jobs_EditValueChanged(sender As Object, e As EventArgs) Handles Jobs.EditValueChanged
        On Error Resume Next
        If Returns.EditValue = Classes.Returns.GSTR_1 AndAlso Jobs.EditValue = 1 Then
            Types.Properties.Items.Item(2).Enabled = True
        Else
            Types.Properties.Items.Item(2).Enabled = False
            Types.EditValue = 0
        End If
    End Sub

End Class
