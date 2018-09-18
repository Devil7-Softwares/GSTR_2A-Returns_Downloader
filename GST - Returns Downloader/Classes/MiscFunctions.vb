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

Namespace Classes
    Module MiscFunctions

        Function GetAssessmentMonths() As List(Of Objects.ReturnsDetails)
            Dim R As New List(Of Objects.ReturnsDetails)

            Dim MonthsOrder As List(Of Integer) = New List(Of Integer)({3, 2, 1, 12, 11, 10, 9, 8, 7, 6, 5, 4})

            Dim CurrentMonth As Integer = Now.Month
            Dim CurrentYear As Integer = ToFinancialYear(Now) - 1

            For i As Integer = 0 To CurrentYear - 2017 'GST Is Implemented in 2017
                Dim Year As Integer = CurrentYear - i
                Dim AssessmentYear As String = Year & "-" & CInt(Year + 1).ToString.Substring(2, 2)

                For j As Integer = 0 To MonthsOrder.Count - 1
                    Dim Month As Integer = MonthsOrder(j)
                    If Year = 2017 AndAlso j > 8 Then Continue For
                    If Year = CurrentYear AndAlso j < MonthsOrder.IndexOf(CurrentMonth - 2) Then Continue For

                    R.Add(New Objects.ReturnsDetails(DateAndTime.MonthName(Month), AssessmentYear))
                Next
            Next

            Return R
        End Function

        Function GetAssessmentQuarters() As List(Of Objects.ReturnsDetails)
            Dim R As New List(Of Objects.ReturnsDetails)

            Dim MonthsOrder As List(Of Integer) = New List(Of Integer)({1, 3, 10, 12, 7, 9, 4, 6})

            Dim CurrentMonth As Integer = Now.Month
            Dim CurrentYear As Integer = ToFinancialYear(Now) - 1

            For i As Integer = 0 To CurrentYear - 2017 'GST Is Implemented in 2017
                Dim Year As Integer = CurrentYear - i
                Dim AssessmentYear As String = Year & "-" & CInt(Year + 1).ToString.Substring(2, 2)

                For j As Integer = 0 To MonthsOrder.Count - 2 Step 2
                    Dim BeginMonth As Integer = MonthsOrder(j)
                    Dim EndMonth As Integer = MonthsOrder(j + 1)
                    If Year = 2017 AndAlso j > 4 Then Continue For
                    If Year = CurrentYear Then
                        Dim SkipIndex As Integer = 0
                        Select Case CurrentMonth
                            Case 4, 5, 6
                                SkipIndex = 6
                            Case 7, 8, 9
                                SkipIndex = 4
                            Case 10, 11, 12
                                SkipIndex = 2
                            Case 1, 2, 3
                                SkipIndex = 0
                        End Select
                        If j < SkipIndex Then
                            Continue For
                        End If
                    End If
                    R.Add(New Objects.ReturnsDetails(DateAndTime.MonthName(BeginMonth).Substring(0, 3) & "-" & DateAndTime.MonthName(EndMonth).Substring(0, 3), AssessmentYear))
                Next
            Next

            Return R
        End Function

        Public Function ToFinancialYear(ByVal dateTime_ As Date) As Integer
            Return If(dateTime_.Month >= 4, dateTime_.Year + 1, dateTime_.Year)
        End Function

        Public Function GetDriverPath() As String
            Return IO.Path.Combine(Application.StartupPath, "Drivers", If(Environment.Is64BitOperatingSystem, "x64", "x86"))
        End Function

        Public Function isNetworkConnected(Optional Host As String = "www.google.com") As Boolean
            If Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable Then
                Return My.Computer.Network.Ping(Host)
            End If
            Return False
        End Function

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

        Sub KillGeckoProcesses()
            On Error Resume Next
            For Each i As Process In Process.GetProcessesByName("geckodriver")
                i.Kill()
            Next
        End Sub

        Function GetMonth(ByVal ZipPath As String) As String
            Dim Zip As New Ionic.Zip.ZipFile(ZipPath)
            Try
                For Each i As Ionic.Zip.ZipEntry In Zip.Entries
                    If i.FileName.ToLower.EndsWith(".json") Then
                        Using stream As New IO.MemoryStream
                            i.Extract(stream)
                            Try
                                Dim DateValue As String = Newtonsoft.Json.Linq.JObject.Parse(Text.Encoding.ASCII.GetString(stream.ToArray)).SelectToken("fp")
                                Dim Month As Integer = CInt(DateValue.Substring(0, 2))
                                Return DateAndTime.MonthName(Month)
                            Catch ex As Exception
                            End Try
                        End Using
                    End If
                Next
            Catch ex As Exception
            Finally
                Zip.Dispose()
            End Try
            Return ""
        End Function

    End Module
End Namespace