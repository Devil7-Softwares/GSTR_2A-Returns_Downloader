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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Main
    Inherits Classes.XtraFormTemp

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
        Me.grp_Credential = New DevExpress.XtraEditors.GroupControl()
        Me.txt_Password = New DevExpress.XtraEditors.TextEdit()
        Me.txt_LoginID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_LoginID = New DevExpress.XtraEditors.LabelControl()
        Me.grp_Jobs = New DevExpress.XtraEditors.GroupControl()
        Me.Jobs = New DevExpress.XtraEditors.RadioGroup()
        Me.btn_Start = New DevExpress.XtraEditors.SimpleButton()
        Me.grp_Process = New DevExpress.XtraEditors.GroupControl()
        Me.btn_Stop = New DevExpress.XtraEditors.SimpleButton()
        Me.grp_Months = New DevExpress.XtraEditors.GroupControl()
        Me.gc_Months = New DevExpress.XtraGrid.GridControl()
        Me.gv_Months = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grp_Downloads = New DevExpress.XtraEditors.GroupControl()
        Me.txt_DownloadsLocation = New DevExpress.XtraEditors.ButtonEdit()
        Me.grp_Console = New DevExpress.XtraEditors.GroupControl()
        Me.txt_Console = New System.Windows.Forms.RichTextBox()
        Me.SelectDownloadsDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.Worker = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar = New DevExpress.XtraEditors.ProgressBarControl()
        Me.btn_About = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_Update = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_FeedBack = New DevExpress.XtraEditors.SimpleButton()
        Me.grp_Type = New DevExpress.XtraEditors.GroupControl()
        Me.Types = New DevExpress.XtraEditors.RadioGroup()
        Me.btn_Settings = New DevExpress.XtraEditors.SimpleButton()
        Me.grp_Returns = New DevExpress.XtraEditors.GroupControl()
        Me.Returns = New DevExpress.XtraEditors.RadioGroup()
        CType(Me.grp_Credential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Credential.SuspendLayout()
        CType(Me.txt_Password.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_LoginID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Jobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Jobs.SuspendLayout()
        CType(Me.Jobs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Process, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Process.SuspendLayout()
        CType(Me.grp_Months, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Months.SuspendLayout()
        CType(Me.gc_Months, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Months, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Downloads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Downloads.SuspendLayout()
        CType(Me.txt_DownloadsLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Console, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Console.SuspendLayout()
        CType(Me.ProgressBar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Type, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Type.SuspendLayout()
        CType(Me.Types.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp_Returns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_Returns.SuspendLayout()
        CType(Me.Returns.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grp_Credential
        '
        Me.grp_Credential.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Credential.Controls.Add(Me.txt_Password)
        Me.grp_Credential.Controls.Add(Me.txt_LoginID)
        Me.grp_Credential.Controls.Add(Me.LabelControl2)
        Me.grp_Credential.Controls.Add(Me.lbl_LoginID)
        Me.grp_Credential.Location = New System.Drawing.Point(12, 12)
        Me.grp_Credential.Name = "grp_Credential"
        Me.grp_Credential.Size = New System.Drawing.Size(278, 79)
        Me.grp_Credential.TabIndex = 0
        Me.grp_Credential.Text = "Credentials"
        '
        'txt_Password
        '
        Me.txt_Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Password.EditValue = ""
        Me.txt_Password.Location = New System.Drawing.Point(91, 52)
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.Properties.UseSystemPasswordChar = True
        Me.txt_Password.Size = New System.Drawing.Size(182, 20)
        Me.txt_Password.TabIndex = 1
        '
        'txt_LoginID
        '
        Me.txt_LoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LoginID.EditValue = ""
        Me.txt_LoginID.Location = New System.Drawing.Point(91, 24)
        Me.txt_LoginID.Name = "txt_LoginID"
        Me.txt_LoginID.Size = New System.Drawing.Size(182, 20)
        Me.txt_LoginID.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(32, 55)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl2.TabIndex = 8
        Me.LabelControl2.Text = "Password :"
        '
        'lbl_LoginID
        '
        Me.lbl_LoginID.Location = New System.Drawing.Point(17, 27)
        Me.lbl_LoginID.Name = "lbl_LoginID"
        Me.lbl_LoginID.Size = New System.Drawing.Size(68, 13)
        Me.lbl_LoginID.TabIndex = 7
        Me.lbl_LoginID.Text = "GST Login ID :"
        '
        'grp_Jobs
        '
        Me.grp_Jobs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Jobs.Controls.Add(Me.Jobs)
        Me.grp_Jobs.Location = New System.Drawing.Point(73, 97)
        Me.grp_Jobs.Name = "grp_Jobs"
        Me.grp_Jobs.Size = New System.Drawing.Size(81, 82)
        Me.grp_Jobs.TabIndex = 9
        Me.grp_Jobs.Text = "Job"
        '
        'Jobs
        '
        Me.Jobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Jobs.EditValue = 0
        Me.Jobs.Location = New System.Drawing.Point(2, 20)
        Me.Jobs.Name = "Jobs"
        Me.Jobs.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Generate"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Download")})
        Me.Jobs.Size = New System.Drawing.Size(77, 60)
        Me.Jobs.TabIndex = 2
        '
        'btn_Start
        '
        Me.btn_Start.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Start.Location = New System.Drawing.Point(5, 23)
        Me.btn_Start.Name = "btn_Start"
        Me.btn_Start.Size = New System.Drawing.Size(52, 24)
        Me.btn_Start.TabIndex = 3
        Me.btn_Start.Text = "Start"
        '
        'grp_Process
        '
        Me.grp_Process.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Process.Controls.Add(Me.btn_Stop)
        Me.grp_Process.Controls.Add(Me.btn_Start)
        Me.grp_Process.Location = New System.Drawing.Point(228, 97)
        Me.grp_Process.Name = "grp_Process"
        Me.grp_Process.Size = New System.Drawing.Size(62, 82)
        Me.grp_Process.TabIndex = 11
        Me.grp_Process.Text = "Process"
        '
        'btn_Stop
        '
        Me.btn_Stop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Stop.Location = New System.Drawing.Point(5, 53)
        Me.btn_Stop.Name = "btn_Stop"
        Me.btn_Stop.Size = New System.Drawing.Size(52, 24)
        Me.btn_Stop.TabIndex = 5
        Me.btn_Stop.Text = "Stop"
        Me.btn_Stop.Visible = False
        '
        'grp_Months
        '
        Me.grp_Months.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Months.Controls.Add(Me.gc_Months)
        Me.grp_Months.Location = New System.Drawing.Point(12, 251)
        Me.grp_Months.Name = "grp_Months"
        Me.grp_Months.Size = New System.Drawing.Size(278, 169)
        Me.grp_Months.TabIndex = 12
        Me.grp_Months.Text = "Months"
        '
        'gc_Months
        '
        Me.gc_Months.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_Months.Location = New System.Drawing.Point(2, 20)
        Me.gc_Months.MainView = Me.gv_Months
        Me.gc_Months.Name = "gc_Months"
        Me.gc_Months.Size = New System.Drawing.Size(274, 147)
        Me.gc_Months.TabIndex = 0
        Me.gc_Months.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Months})
        '
        'gv_Months
        '
        Me.gv_Months.GridControl = Me.gc_Months
        Me.gv_Months.Name = "gv_Months"
        Me.gv_Months.OptionsView.ShowGroupPanel = False
        '
        'grp_Downloads
        '
        Me.grp_Downloads.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Downloads.Controls.Add(Me.txt_DownloadsLocation)
        Me.grp_Downloads.Location = New System.Drawing.Point(12, 202)
        Me.grp_Downloads.Name = "grp_Downloads"
        Me.grp_Downloads.Size = New System.Drawing.Size(278, 43)
        Me.grp_Downloads.TabIndex = 14
        Me.grp_Downloads.Text = "Downloads Location"
        '
        'txt_DownloadsLocation
        '
        Me.txt_DownloadsLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_DownloadsLocation.Location = New System.Drawing.Point(2, 20)
        Me.txt_DownloadsLocation.Name = "txt_DownloadsLocation"
        Me.txt_DownloadsLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txt_DownloadsLocation.Properties.ReadOnly = True
        Me.txt_DownloadsLocation.Size = New System.Drawing.Size(274, 20)
        Me.txt_DownloadsLocation.TabIndex = 0
        '
        'grp_Console
        '
        Me.grp_Console.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Console.Controls.Add(Me.txt_Console)
        Me.grp_Console.Location = New System.Drawing.Point(12, 426)
        Me.grp_Console.Name = "grp_Console"
        Me.grp_Console.Size = New System.Drawing.Size(278, 107)
        Me.grp_Console.TabIndex = 15
        Me.grp_Console.Text = "Output"
        '
        'txt_Console
        '
        Me.txt_Console.BackColor = System.Drawing.Color.Black
        Me.txt_Console.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Console.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_Console.ForeColor = System.Drawing.Color.White
        Me.txt_Console.Location = New System.Drawing.Point(2, 20)
        Me.txt_Console.Name = "txt_Console"
        Me.txt_Console.Size = New System.Drawing.Size(274, 85)
        Me.txt_Console.TabIndex = 0
        Me.txt_Console.Text = ""
        '
        'SelectDownloadsDialog
        '
        Me.SelectDownloadsDialog.Description = "Select folder to store downloaded files"
        '
        'Worker
        '
        Me.Worker.WorkerSupportsCancellation = True
        '
        'ProgressBar
        '
        Me.ProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar.Location = New System.Drawing.Point(12, 185)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(278, 11)
        Me.ProgressBar.TabIndex = 16
        '
        'btn_About
        '
        Me.btn_About.Location = New System.Drawing.Point(248, 539)
        Me.btn_About.Name = "btn_About"
        Me.btn_About.Size = New System.Drawing.Size(42, 23)
        Me.btn_About.TabIndex = 17
        Me.btn_About.Text = "About"
        '
        'btn_Update
        '
        Me.btn_Update.Location = New System.Drawing.Point(71, 539)
        Me.btn_Update.Name = "btn_Update"
        Me.btn_Update.Size = New System.Drawing.Size(55, 23)
        Me.btn_Update.TabIndex = 18
        Me.btn_Update.Text = "Update"
        '
        'btn_FeedBack
        '
        Me.btn_FeedBack.Location = New System.Drawing.Point(132, 539)
        Me.btn_FeedBack.Name = "btn_FeedBack"
        Me.btn_FeedBack.Size = New System.Drawing.Size(110, 23)
        Me.btn_FeedBack.TabIndex = 19
        Me.btn_FeedBack.Text = "Support / Feedback"
        '
        'grp_Type
        '
        Me.grp_Type.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grp_Type.Controls.Add(Me.Types)
        Me.grp_Type.Location = New System.Drawing.Point(160, 97)
        Me.grp_Type.Name = "grp_Type"
        Me.grp_Type.Size = New System.Drawing.Size(62, 82)
        Me.grp_Type.TabIndex = 20
        Me.grp_Type.Text = "Type"
        '
        'Types
        '
        Me.Types.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Types.EditValue = 0
        Me.Types.Location = New System.Drawing.Point(2, 20)
        Me.Types.Name = "Types"
        Me.Types.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "JSON"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Excel"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "PDF")})
        Me.Types.Size = New System.Drawing.Size(58, 60)
        Me.Types.TabIndex = 2
        '
        'btn_Settings
        '
        Me.btn_Settings.Location = New System.Drawing.Point(12, 539)
        Me.btn_Settings.Name = "btn_Settings"
        Me.btn_Settings.Size = New System.Drawing.Size(53, 23)
        Me.btn_Settings.TabIndex = 21
        Me.btn_Settings.Text = "Settings"
        '
        'grp_Returns
        '
        Me.grp_Returns.Controls.Add(Me.Returns)
        Me.grp_Returns.Location = New System.Drawing.Point(12, 97)
        Me.grp_Returns.Name = "grp_Returns"
        Me.grp_Returns.Size = New System.Drawing.Size(57, 82)
        Me.grp_Returns.TabIndex = 22
        Me.grp_Returns.Text = "Return"
        '
        'Returns
        '
        Me.Returns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Returns.EditValue = -1
        Me.Returns.Location = New System.Drawing.Point(2, 20)
        Me.Returns.Name = "Returns"
        Me.Returns.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "2A"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "3B")})
        Me.Returns.Size = New System.Drawing.Size(53, 60)
        Me.Returns.TabIndex = 23
        '
        'frm_Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(302, 570)
        Me.Controls.Add(Me.grp_Returns)
        Me.Controls.Add(Me.btn_Settings)
        Me.Controls.Add(Me.grp_Type)
        Me.Controls.Add(Me.btn_FeedBack)
        Me.Controls.Add(Me.btn_Update)
        Me.Controls.Add(Me.btn_About)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.grp_Console)
        Me.Controls.Add(Me.grp_Downloads)
        Me.Controls.Add(Me.grp_Months)
        Me.Controls.Add(Me.grp_Process)
        Me.Controls.Add(Me.grp_Jobs)
        Me.Controls.Add(Me.grp_Credential)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm_Main"
        Me.Text = "GSTR 2A - Returns Downloader"
        CType(Me.grp_Credential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Credential.ResumeLayout(False)
        Me.grp_Credential.PerformLayout()
        CType(Me.txt_Password.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_LoginID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Jobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Jobs.ResumeLayout(False)
        CType(Me.Jobs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Process, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Process.ResumeLayout(False)
        CType(Me.grp_Months, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Months.ResumeLayout(False)
        CType(Me.gc_Months, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Months, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Downloads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Downloads.ResumeLayout(False)
        CType(Me.txt_DownloadsLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Console, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Console.ResumeLayout(False)
        CType(Me.ProgressBar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Type, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Type.ResumeLayout(False)
        CType(Me.Types.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp_Returns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_Returns.ResumeLayout(False)
        CType(Me.Returns.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grp_Credential As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txt_Password As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txt_LoginID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_LoginID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grp_Jobs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Jobs As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents btn_Start As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grp_Process As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btn_Stop As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grp_Months As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grp_Downloads As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grp_Console As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txt_DownloadsLocation As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents txt_Console As RichTextBox
    Friend WithEvents gc_Months As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_Months As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SelectDownloadsDialog As FolderBrowserDialog
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents btn_About As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_FeedBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_Update As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grp_Type As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Types As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents btn_Settings As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grp_Returns As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Returns As DevExpress.XtraEditors.RadioGroup
End Class
