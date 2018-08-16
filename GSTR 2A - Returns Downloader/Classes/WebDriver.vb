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

        Function StartDriver(ByVal DownloadDir As String) As Boolean
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
            FirefoxOpt.Profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/zip")
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

        Sub ClickButtonByText(ByVal BtnName As String)
            For Each i As IWebElement In Driver.FindElements(By.TagName("button"))
                If i.Text = BtnName Then
                    Dim tries As Integer = 0
Click:
                    tries += 1
                    Try
                        i.Click()
                    Catch ex As InvalidOperationException
                        If tries < 11 Then
                            Threading.Thread.Sleep(1000)
                            GoTo Click
                        End If
                    End Try
                    Exit For
                End If
            Next
        End Sub

        Sub DownloadGSTR(ByVal Month As String, ByVal Year As String, ByVal FileType As Enums.FileType, ByVal Owner As frm_Main)
            Threading.Thread.Sleep(5000)

            Owner.Write2Console("Filling Year & Month..." & vbNewLine & vbNewLine, Color.Yellow)
            Dim Year_ = Driver.FindElement(By.Name("fin"))
            Year_.SendKeys(Year)
            Threading.Thread.Sleep(1000)
            Dim Month_ = Driver.FindElement(By.Name("mon"))
            Month_.SendKeys(Month)
            Threading.Thread.Sleep(1000)

            Owner.Write2Console("Searching for returns..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("SEARCH")
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending download request...", Color.Yellow)
            Select Case FileType
                Case Enums.FileType.JSON
                    ClickButtonByText("GENERATE JSON FILE TO DOWNLOAD")
                Case Enums.FileType.Excel
                    ClickButtonByText("GENERATE EXCEL FILE TO DOWNLOAD")
            End Select
            WaitForLoad(Owner)
            Threading.Thread.Sleep(2000)

            Owner.Write2Console("Sending generate file request..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("GENERATE FILE")
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
                            If link.Text.Contains("Click here to download - File 1") Then
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
        Sub RequestGSTR(ByVal Month As String, ByVal Year As String, ByVal FileType As Enums.FileType, ByVal Owner As frm_Main)
            Threading.Thread.Sleep(5000)

            Owner.Write2Console("Filling Year & Month..." & vbNewLine & vbNewLine, Color.Yellow)
            Dim Year_ = Driver.FindElement(By.Name("fin"))
            Year_.SendKeys(Year)
            Threading.Thread.Sleep(1000)
            Dim Month_ = Driver.FindElement(By.Name("mon"))
            Month_.SendKeys(Month)
            Threading.Thread.Sleep(1000)

            Owner.Write2Console("Searching for returns..." & vbNewLine & vbNewLine, Color.Yellow)
            ClickButtonByText("SEARCH")
            Threading.Thread.Sleep(2000)

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

    End Module
End Namespace