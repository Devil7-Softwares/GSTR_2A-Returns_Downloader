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

Imports OpenQA.Selenium
Imports OpenQA.Selenium.Firefox
Imports OpenQA.Selenium.Support.UI

Namespace Classes
    Module WebDriver

        Public Driver As FirefoxDriver

        Function GetMimeType(ByVal Type As Integer)
            Select Case Type
                Case 0, 1
                    Return "application/zip"
                Case 2
                    Return "application/pdf"
                Case Else
                    Return "application/zip"
            End Select
        End Function

        Function StartDriver(ByVal DownloadDir As String, ByVal FileType As Integer ) As Boolean
            If Driver IsNot Nothing Then
                Try
                    Driver.Close()
                Catch ex As Exception

                End Try
            End If
            If Not My.Computer.FileSystem.DirectoryExists(DownloadDir) Then My.Computer.FileSystem.CreateDirectory(DownloadDir)
            Dim FirefoxOpt As New FirefoxOptions
            FirefoxOpt.Profile = New FirefoxProfile
            FirefoxOpt.Profile.AcceptUntrustedCertificates = True
            FirefoxOpt.Profile.SetPreference("browser.download.folderList", 2)
            FirefoxOpt.Profile.SetPreference("browser.download.dir", DownloadDir)
            FirefoxOpt.Profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", GetMimeType(FileType))
            FirefoxOpt.Profile.SetPreference("plugin.disable_full_page_plugin_for_types", "application/pdf")
            FirefoxOpt.Profile.SetPreference("pdfjs.disabled", True)
            FirefoxOpt.AcceptInsecureCertificates = True

            If My.Settings.FireFoxLocation <> "" AndAlso My.Computer.FileSystem.FileExists(My.Settings.FireFoxLocation) Then
                FirefoxOpt.BrowserExecutableLocation = My.Settings.FireFoxLocation
            End If

            Dim driverservice As FirefoxDriverService = FirefoxDriverService.CreateDefaultService(GetDriverPath)
            driverservice.HideCommandPromptWindow = True
            Try
                Driver = New FirefoxDriver(driverservice, FirefoxOpt, New TimeSpan(1, 0, 0, 0))
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Sub WaitForLoad(ByVal Owner As frm_Main)
            On Error Resume Next
            Owner.Write2Console(" Waiting for Page to Load..." & vbNewLine & vbNewLine, Color.Yellow)
            Driver.Manage.Timeouts.ImplicitWait = New TimeSpan(0, 0, 5)
            Dim wait As WebDriverWait = New WebDriverWait(Driver, New TimeSpan(10000))
            Dim Func As System.Func(Of IWebDriver, Boolean) = AddressOf CheckPageLoaded
            wait.Until(Func)

            Dim Dimmer As IWebElement = Driver.FindElementByClassName("dimmer-holder")
            Do Until Dimmer.Displayed = False
                Threading.Thread.Sleep(1000)
            Loop
        End Sub

        Function CheckPageLoaded(ByVal Driver As IWebDriver) As Boolean
            Try
                Return CType(Driver, IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete")
            Catch ex As Exception
                Return True
            End Try
        End Function

        Function ClickButtonByText(ByVal BtnName As String) As Boolean
            Return ClickButtonByText(BtnName, Driver)
        End Function

        Function ClickButtonByText(ByVal BtnName As String, ByVal Parent As Object) As Boolean
            For Each i As IWebElement In Parent.FindElements(By.TagName("button"))
                If i.Text = BtnName Then
                    Dim tries As Integer = 0
Click:
                    tries += 1
                    Try
                        i.Click()
                        Return True
                    Catch ex As InvalidOperationException
                        If tries < 11 Then
                            Threading.Thread.Sleep(1000)
                            GoTo Click
                        End If
                    End Try
                    Exit For
                End If
            Next
            For Each i As IWebElement In Parent.FindElements(By.TagName("a"))
                If i.Text = BtnName AndAlso i.GetAttribute("type") = "button" Then
                    Try
                        i.Click()
                        Return True
                    Catch ex As InvalidOperationException

                    End Try
                    Exit For
                End If
            Next
            Return False
        End Function

        Function ViewGSTR3B()
            For Each i As IWebElement In Driver.FindElements(By.TagName("div"))
                If i.Text.Contains("GSTR3B") AndAlso Not i.Text.Contains("GSTR1") AndAlso Not i.Text.Contains("GSTR2A") Then
                    If i.Text.Contains("Filed") And i.Text.Contains("VIEW") Then
                        For Each b As IWebElement In i.FindElements(By.TagName("button"))
                            If b.Text = "VIEW" AndAlso b.GetAttribute("data-ng-click") = "page_rtp(x.return_ty,x.due_dt,x.status)" Then
                                Dim tries As Integer = 0
Click:
                                tries += 1
                                Try
                                    b.Click()
                                    Return True
                                Catch ex As InvalidOperationException
                                    If tries < 11 Then
                                        Threading.Thread.Sleep(1000)
                                        GoTo Click
                                    End If
                                End Try
                                Exit For
                            End If
                        Next
                    Else
                        Return False
                    End If
                    Exit For
                End If
            Next
            Return True
        End Function

        Private Function DownloadGSTR4A()
            For Each i As IWebElement In Driver.FindElements(By.TagName("div"))
                If i.Text.Contains("Auto drafted details for registered persons opting composition levy") And Not i.Text.Contains("Quarterly Return for registered person opting for composition levy") Then
                    If i.Text.Contains("DOWNLOAD") Then
                        For Each b As IWebElement In i.FindElements(By.TagName("button"))
                            If b.Text = "DOWNLOAD" Then
                                Dim tries As Integer = 0
Click:
                                tries += 1
                                Try
                                    b.Click()
                                    Return True
                                Catch ex As InvalidOperationException
                                    If tries < 11 Then
                                        Threading.Thread.Sleep(1000)
                                        GoTo Click
                                    End If
                                End Try
                                Exit For
                            End If
                        Next
                    Else
                        Return False
                    End If
                    Exit For
                End If
            Next
            Return True
        End Function

        Sub SetAttribute(ByVal element As IWebElement, ByVal attName As String, ByVal attValue As String)
            Driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, attName, attValue)
        End Sub

        Function HideAllDialogs() As Boolean
            For Each b As IWebElement In Driver.FindElements(By.TagName("button"))
                If b.Text = "OK" AndAlso b.GetAttribute("data-dismiss") = "modal" Then
                    Dim tries As Integer = 0
Click:
                    tries += 1
                    Try
                        b.Click()
                        Threading.Thread.Sleep(3000)
                        Return True
                    Catch ex As InvalidOperationException
                        If tries < 11 Then
                            Threading.Thread.Sleep(1000)
                            GoTo Click
                        End If
                    End Try
                    Exit For
                End If
            Next
            Return False
        End Function

        Sub SearchReturns(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            Threading.Thread.Sleep(2000)
            Owner.Write2Console("Filling Year & Month..." & vbNewLine & vbNewLine, Color.Yellow)

            Dim Year_ = Driver.FindElement(By.Name("fin"))
            SelectValue(Year_, Year)
            Threading.Thread.Sleep(500)
            Dim Month_ = Driver.FindElement(By.Name("mon"))
            SelectValue(Month_, Month)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Searching for returns..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("SEARCH")
            Threading.Thread.Sleep(2000)
        End Sub

        Sub DownloadGSTR2A(ByVal Month As String, ByVal Year As String, ByVal FileType As Enums.FileType, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            ClickButtonByText("DOWNLOAD")
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending generate file request..." & vbNewLine & vbNewLine, Color.Yellow)
            Select Case FileType
                Case Enums.FileType.JSON
                    ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
                Case Enums.FileType.Excel
                    ClickButtonByText("GENERATE EXCEL FILE TO DOWNLOAD")
            End Select
            Threading.Thread.Sleep(3000)

            Owner.Write2Console("Processing response..." & vbNewLine, Color.Yellow)
            For Each i As IWebElement In Driver.FindElements(By.TagName("alert-message"))
                If i.GetAttribute("ng-show") = "showMsg" Then
                    If i.Text.Contains("Your request for generation has been accepted kindly wait for") Then
                        Owner.Write2Console(Month & " - File not generated. Generation Requested." & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("File Generation is in progress, please try after sometime..") Then
                        Owner.Write2Console(Month & " - Generation is in progress" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("click on generate file again") Or i.Text.Contains("click on the download button again") Then
                        For Each link As IWebElement In Driver.FindElements(By.TagName("a"))
                            If link.Text.Contains("Click here to download ") Then
                                Owner.Write2Console(Month & " - File Found. Downloading." & vbNewLine & vbNewLine, Color.Green)
                                link.Click()
                            End If
                        Next
                    End If
                End If
            Next
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Going back to previous page..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("BACK")
            Threading.Thread.Sleep(3000)
        End Sub

        Sub RequestGSTR2A(ByVal Month As String, ByVal Year As String, ByVal FileType As Enums.FileType, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            ClickButtonByText("DOWNLOAD")
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending generate file request..." & vbNewLine & vbNewLine, Color.Yellow)
            Select Case FileType
                Case Enums.FileType.JSON
                    ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
                Case Enums.FileType.Excel
                    ClickButtonByText("GENERATE EXCEL FILE TO DOWNLOAD")
            End Select
            Threading.Thread.Sleep(3000)

            Owner.Write2Console("Processing response..." & vbNewLine, Color.Yellow)
            For Each i As IWebElement In Driver.FindElements(By.TagName("alert-message"))
                If i.GetAttribute("ng-show") = "showMsg" Then
                    If i.Text.Contains("Your request for generation has been accepted kindly wait for") Then
                        Owner.Write2Console(Month & " - Success" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("File Generation is in progress, please try after sometime..") Then
                        Owner.Write2Console(Month & " - Generation is in progress" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("click on generate file again") Or i.Text.Contains("click on the download button again") Then
                        Owner.Write2Console(Month & " - Old Generation Found. Generating New." & vbNewLine & vbNewLine, Color.Green)
                        Select Case FileType
                            Case Enums.FileType.JSON
                                ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
                            Case Enums.FileType.Excel
                                ClickButtonByText("GENERATE EXCEL FILE TO DOWNLOAD")
                        End Select
                    End If
                End If
            Next
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Going back to previous page..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("BACK")
            Threading.Thread.Sleep(3000)
        End Sub

        Sub DownloadGSTR3B(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            If Not ViewGSTR3B() Then
                Owner.Write2Console("GSTR 3B Not Filed for " & Month & vbNewLine, Color.Red)
                Exit Sub
            End If
            Do Until Driver.Url = "https://return.gst.gov.in/returns/auth/gstr3b"
                Threading.Thread.Sleep(1000)
            Loop
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            HideAllDialogs()

            Owner.Write2Console("Downloading GSTR3B...", Color.Green)
            If ClickButtonByText("DOWNLOAD FILED GSTR-3B") Then
                Owner.Write2Console("Done...", Color.Green)
            Else
                Owner.Write2Console("Failed...", Color.Red)
            End If
            Threading.Thread.Sleep(10000)

            Owner.Write2Console("Going back to previous page..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("BACK")
            Threading.Thread.Sleep(3000)
        End Sub

        Sub DownloadGSTR1(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            ClickButtonByText("VIEW GSTR1")
            Do Until Driver.Url = "https://return.gst.gov.in/returns/auth/gstr1"
                Threading.Thread.Sleep(1000)
            Loop
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Downloading GSTR1...", Color.Green)
            If ClickButtonByText("PREVIEW") Then
                Owner.Write2Console("Done..." & vbNewLine, Color.Green)
            Else
                Owner.Write2Console("Failed..." & vbNewLine, Color.Red)
            End If
            Threading.Thread.Sleep(10000)

            Owner.Write2Console("Going back to previous page...", Color.Yellow)
            If ClickButtonByText("BACK") Then
                Owner.Write2Console("Done." & vbNewLine & vbNewLine, Color.Yellow)
            Else
                Owner.Write2Console("Failed." & vbNewLine & vbNewLine, Color.Red)
            End If
            Threading.Thread.Sleep(3000)
        End Sub

        Sub DownloadGSTR4(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            ClickButtonByText("VIEW GSTR4")
            Do Until Driver.Url = "https://return.gst.gov.in/returns2/auth/gstr4"
                Threading.Thread.Sleep(1000)
            Loop
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Downloading GSTR4...", Color.Green)
            If ClickButtonByText("PREVIEW") Then
                Owner.Write2Console("Done..." & vbNewLine, Color.Green)
            Else
                Owner.Write2Console("Failed..." & vbNewLine, Color.Red)
            End If
            Threading.Thread.Sleep(10000)

            Owner.Write2Console("Going back to previous page...", Color.Yellow)
            If ClickButtonByText("BACK") Then
                Owner.Write2Console("Done." & vbNewLine & vbNewLine, Color.Yellow)
            Else
                Owner.Write2Console("Failed." & vbNewLine & vbNewLine, Color.Red)
            End If
            Threading.Thread.Sleep(3000)
        End Sub

        Sub RequestGSTR4A(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            DownloadGSTR4A()
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending generate file request..." & vbNewLine & vbNewLine, Color.Yellow)

            ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")

            Threading.Thread.Sleep(3000)

            Owner.Write2Console("Processing response..." & vbNewLine, Color.Yellow)
            For Each i As IWebElement In Driver.FindElements(By.TagName("alert-message"))
                If i.GetAttribute("ng-show") = "showMsg" Then
                    If i.Text.Contains("Your request for generation has been accepted kindly wait for") Then
                        Owner.Write2Console(Month & " - Success" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("File Generation is in progress, please try after sometime..") Then
                        Owner.Write2Console(Month & " - Generation is in progress" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("click on generate file again") Or i.Text.Contains("click on the download button again") Then
                        Owner.Write2Console(Month & " - Old Generation Found. Generating New." & vbNewLine & vbNewLine, Color.Green)
                        ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
                    End If
                End If
            Next
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Going back to previous page..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("BACK")
            Threading.Thread.Sleep(3000)
        End Sub

        Sub DownloadGSTR4A(ByVal Month As String, ByVal Year As String, ByVal Owner As frm_Main)
            SearchReturns(Month, Year, Owner)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            DownloadGSTR4A()
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending generate file request..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
            Threading.Thread.Sleep(3000)

            Owner.Write2Console("Processing response..." & vbNewLine, Color.Yellow)
            For Each i As IWebElement In Driver.FindElements(By.TagName("alert-message"))
                If i.GetAttribute("ng-show") = "showMsg" Then
                    If i.Text.Contains("Your request for generation has been accepted kindly wait for") Then
                        Owner.Write2Console(Month & " - File not generated. Generation Requested." & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("File Generation is in progress, please try after sometime..") Then
                        Owner.Write2Console(Month & " - Generation is in progress" & vbNewLine & vbNewLine, Color.Green)
                    ElseIf i.Text.Contains("click on generate file again") Or i.Text.Contains("click on the download button again") Then
                        For Each link As IWebElement In Driver.FindElements(By.TagName("a"))
                            If link.Text.Contains("Click here to download ") Then
                                Owner.Write2Console(Month & " - File Found. Downloading." & vbNewLine & vbNewLine, Color.Green)
                                link.Click()
                            End If
                        Next
                    End If
                End If
            Next
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Going back to previous page..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("BACK")
            Threading.Thread.Sleep(3000)
        End Sub

        Sub SelectValue(ByVal SelectElement As IWebElement, ByVal Value As String)
            For Each i As IWebElement In SelectElement.FindElements(By.TagName("option"))
                If i.GetAttribute("label") = Value Then
                    i.Click()
                    Exit For
                End If
            Next
        End Sub

    End Module
End Namespace