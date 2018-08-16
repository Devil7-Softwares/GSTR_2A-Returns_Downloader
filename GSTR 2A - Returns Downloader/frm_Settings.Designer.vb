<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Settings
    Inherits Classes.XtraFormTemp

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Settings))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txt_FirefoxLocation = New DevExpress.XtraEditors.ButtonEdit()
        Me.OpenFile_FireFox = New System.Windows.Forms.OpenFileDialog()
        Me.btn_OK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txt_FirefoxLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(18, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(86, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "FireFox Location :"
        '
        'txt_FirefoxLocation
        '
        Me.txt_FirefoxLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_FirefoxLocation.Location = New System.Drawing.Point(110, 12)
        Me.txt_FirefoxLocation.Name = "txt_FirefoxLocation"
        Me.txt_FirefoxLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txt_FirefoxLocation.Properties.NullValuePrompt = "Select firefox.exe"
        Me.txt_FirefoxLocation.Properties.NullValuePromptShowForEmptyValue = True
        Me.txt_FirefoxLocation.Properties.ReadOnly = True
        Me.txt_FirefoxLocation.Size = New System.Drawing.Size(299, 20)
        Me.txt_FirefoxLocation.TabIndex = 1
        '
        'OpenFile_FireFox
        '
        Me.OpenFile_FireFox.FileName = "firefox.exe"
        Me.OpenFile_FireFox.Filter = "Firefox.exe|firefox.exe"
        Me.OpenFile_FireFox.Title = "Select Firefox Executable"
        '
        'btn_OK
        '
        Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_OK.Location = New System.Drawing.Point(334, 44)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(75, 23)
        Me.btn_OK.TabIndex = 2
        Me.btn_OK.Text = "OK"
        '
        'frm_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 79)
        Me.Controls.Add(Me.btn_OK)
        Me.Controls.Add(Me.txt_FirefoxLocation)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        CType(Me.txt_FirefoxLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txt_FirefoxLocation As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents OpenFile_FireFox As OpenFileDialog
    Friend WithEvents btn_OK As DevExpress.XtraEditors.SimpleButton
End Class
