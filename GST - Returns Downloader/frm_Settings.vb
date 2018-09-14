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
'                                                                          '
'=========================================================================='

Imports DevExpress.XtraEditors.Controls

Public Class frm_Settings

    Private Sub txt_FirefoxLocation_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles txt_FirefoxLocation.ButtonClick
        If My.Computer.FileSystem.FileExists(txt_FirefoxLocation.EditValue) Then OpenFile_FireFox.FileName = txt_FirefoxLocation.EditValue
        If OpenFile_FireFox.ShowDialog = DialogResult.OK Then
            txt_FirefoxLocation.EditValue = OpenFile_FireFox.FileName
        End If
    End Sub

    Private Sub frm_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txt_FirefoxLocation.EditValue = My.Settings.FireFoxLocation
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If txt_FirefoxLocation.EditValue <> "" AndAlso My.Computer.FileSystem.FileExists(txt_FirefoxLocation.EditValue) Then My.Settings.FireFoxLocation = txt_FirefoxLocation.EditValue
        My.Settings.Save()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class