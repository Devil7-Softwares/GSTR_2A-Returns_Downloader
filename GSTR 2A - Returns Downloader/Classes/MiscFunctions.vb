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
                    If Year = CurrentYear AndAlso j < MonthsOrder.IndexOf(CurrentMonth) Then Continue For

                    R.Add(New Objects.ReturnsDetails(DateAndTime.MonthName(Month), AssessmentYear))
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

    End Module
End Namespace