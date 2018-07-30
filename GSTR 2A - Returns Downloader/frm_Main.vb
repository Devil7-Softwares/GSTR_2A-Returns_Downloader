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

Public Class frm_Main

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

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gc_Months.DataSource = Classes.MiscFunctions.GetAssessmentMonths
        LoadSettings()
    End Sub

    Private Sub txt_DownloadsLocation_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles txt_DownloadsLocation.ButtonClick
        SelectDownloadsDialog.SelectedPath = txt_DownloadsLocation.EditValue
        If SelectDownloadsDialog.ShowDialog() = DialogResult.OK Then
            txt_DownloadsLocation.EditValue = SelectDownloadsDialog.SelectedPath
            SaveSettings()
        End If
    End Sub
End Class
