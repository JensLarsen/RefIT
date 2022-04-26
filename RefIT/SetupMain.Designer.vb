<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetupMain
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetupMain))
        Me.Gem_BTN = New System.Windows.Forms.Button()
        Me.Afbryd_BTN = New System.Windows.Forms.Button()
        Me.Email_TXT = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Gem_BTN
        '
        Me.Gem_BTN.Location = New System.Drawing.Point(17, 106)
        Me.Gem_BTN.Name = "Gem_BTN"
        Me.Gem_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Gem_BTN.TabIndex = 14
        Me.Gem_BTN.Text = "Save"
        Me.Gem_BTN.UseVisualStyleBackColor = True
        '
        'Afbryd_BTN
        '
        Me.Afbryd_BTN.Location = New System.Drawing.Point(146, 106)
        Me.Afbryd_BTN.Name = "Afbryd_BTN"
        Me.Afbryd_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Afbryd_BTN.TabIndex = 15
        Me.Afbryd_BTN.Text = "Abort"
        Me.Afbryd_BTN.UseVisualStyleBackColor = True
        '
        'Email_TXT
        '
        Me.Email_TXT.Location = New System.Drawing.Point(17, 48)
        Me.Email_TXT.Name = "Email_TXT"
        Me.Email_TXT.Size = New System.Drawing.Size(204, 20)
        Me.Email_TXT.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Software error notification email"
        '
        'SetupMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(237, 141)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Email_TXT)
        Me.Controls.Add(Me.Afbryd_BTN)
        Me.Controls.Add(Me.Gem_BTN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SetupMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SetupMain"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CPR_TXT As TextBox
    Friend WithEvents PrvDato_TXT As TextBox
    Friend WithEvents Kvn_TXT As TextBox
    Friend WithEvents Res_TXT As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Enhed_TXT As TextBox
    Friend WithEvents Gem_BTN As Button
    Friend WithEvents Afbryd_BTN As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Status_TXT As TextBox
    Friend WithEvents Email_TXT As TextBox
    Friend WithEvents Label2 As Label
End Class
