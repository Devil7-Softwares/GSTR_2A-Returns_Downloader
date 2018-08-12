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

Public Class frm_Feedback

    Private Sub btn_Send_Click(sender As Object, e As EventArgs) Handles btn_Send.Click
        If Not FeedbackSender.IsBusy Then FeedbackSender.RunWorkerAsync()
    End Sub

    Function IsValidEmailFormat(ByVal s As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(s, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
    End Function

    Private Sub FeedbackSender_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FeedbackSender.DoWork
        If txt_Name.Text.Trim = "" Then
            MsgBox("'Full Name' cannot be empty.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
            txt_Name.Focus()
        ElseIf Not IsValidEmailFormat(txt_Email.Text.Trim) Then
            MsgBox("Invalid Email.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
            txt_Email.Focus()
        Else
            Me.Invoke(Sub()
                          Me.Enabled = False
                          frm_Wait.Show(Me)
                      End Sub)
            If Classes.RestAPI.PostFeedback(New Classes.FeedBack(My.Application.Info.AssemblyName, My.Application.Info.Version.ToString, Classes.MiscFunctions.RetrieveLinkerTimestamp, txt_Name.Text.Trim, txt_Email.Text.Trim, txt_Rating.Rating, txt_Message.Text.Trim)) Then
                Me.Invoke(Sub()
                              frm_Wait.Close()
                              Me.DialogResult = DialogResult.OK
                              Me.Close()
                          End Sub)
            End If
            Me.Invoke(Sub()
                          Try
                              Me.Enabled = True
                              frm_Wait.Close()
                          Catch ex As Exception
                          End Try
                      End Sub)
        End If
    End Sub

End Class