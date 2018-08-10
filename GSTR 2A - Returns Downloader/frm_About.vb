﻿'=========================================================================='
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

Public Class frm_About
    Dim SourceLink, LicenseLink, IssuesLink, ReleasesLink, WebsiteLink As String
    Function RetrieveLinkerTimestamp() As DateTime
        Const PeHeaderOffset As Integer = 60
        Const LinkerTimestampOffset As Integer = 8
        Dim b(2047) As Byte
        Dim s As IO.Stream = Nothing
        Try
            s = New IO.FileStream(Application.ExecutablePath, IO.FileMode.Open, IO.FileAccess.Read)
            s.Read(b, 0, 2048)
        Finally
            If Not s Is Nothing Then s.Close()
        End Try
        Dim i As Integer = BitConverter.ToInt32(b, PeHeaderOffset)
        Dim SecondsSince1970 As Integer = BitConverter.ToInt32(b, i + LinkerTimestampOffset)
        Dim dt As New DateTime(1970, 1, 1, 0, 0, 0)
        dt = dt.AddSeconds(SecondsSince1970)
        dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours)
        Return dt
    End Function

    Private Sub frm_About_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        lbl_ApplicationTitle.Text = My.Application.Info.Title
        lbl_Description.Text = My.Application.Info.Description
        lbl_BuildDate.Text = RetrieveLinkerTimestamp.ToString("dd/MM/yyyy hh:mm:ss")
        lbl_Company.Text = My.Application.Info.CompanyName
        lbl_Version.Text = My.Application.Info.Version.ToString
        lbl_ProjectTitle.Text = My.Application.Info.ProductName
        lbl_Email.Text = "devil7softwares@gmail.com"

        SourceLink = "https://github.com/Devil7-Softwares/GSTR_2A-Returns_Downloader"
        LicenseLink = "http://www.apache.org/licenses/LICENSE-2.0"
        IssuesLink = "https://github.com/Devil7-Softwares/GSTR_2A-Returns_Downloader/issues"
        ReleasesLink = "https://github.com/Devil7-Softwares/GSTR_2A-Returns_Downloader/releases"
        WebsiteLink = "https://devil7softwares.github.io"

    End Sub

    Private Sub lbl_Email_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_Email.LinkClicked
        Process.Start(String.Format("mailto:{0}", lbl_Email.Text))
    End Sub

    Private Sub lbl_License_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_License.LinkClicked
        Process.Start(LicenseLink)
    End Sub

    Private Sub lbl_Source_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_Source.LinkClicked
        Process.Start(SourceLink)
    End Sub

    Private Sub lbl_IssueTracker_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_IssueTracker.LinkClicked
        Process.Start(IssuesLink)
    End Sub

    Private Sub lbl_Website_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_Website.LinkClicked
        Process.Start(WebsiteLink)
    End Sub

    Private Sub lbl_Downlods_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbl_Downlods.LinkClicked
        Process.Start(ReleasesLink)
    End Sub
End Class