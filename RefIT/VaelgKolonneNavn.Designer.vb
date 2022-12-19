<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VaelgKolonneNavn
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VaelgKolonneNavn))
        Me.KolonneNavn_CBX = New System.Windows.Forms.ComboBox()
        Me.Kolonne_LBL = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Abort_BTN = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'KolonneNavn_CBX
        '
        Me.KolonneNavn_CBX.FormattingEnabled = True
        Me.KolonneNavn_CBX.Location = New System.Drawing.Point(51, 57)
        Me.KolonneNavn_CBX.Name = "KolonneNavn_CBX"
        Me.KolonneNavn_CBX.Size = New System.Drawing.Size(157, 21)
        Me.KolonneNavn_CBX.TabIndex = 0
        '
        'Kolonne_LBL
        '
        Me.Kolonne_LBL.AutoSize = True
        Me.Kolonne_LBL.Location = New System.Drawing.Point(48, 9)
        Me.Kolonne_LBL.Name = "Kolonne_LBL"
        Me.Kolonne_LBL.Size = New System.Drawing.Size(166, 13)
        Me.Kolonne_LBL.TabIndex = 1
        Me.Kolonne_LBL.Text = "Select Column containing data for"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Abort_BTN
        '
        Me.Abort_BTN.Location = New System.Drawing.Point(161, 102)
        Me.Abort_BTN.Name = "Abort_BTN"
        Me.Abort_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Abort_BTN.TabIndex = 4
        Me.Abort_BTN.Text = "Abort"
        Me.Abort_BTN.UseVisualStyleBackColor = True
        '
        'VaelgKolonneNavn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(248, 140)
        Me.Controls.Add(Me.Abort_BTN)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Kolonne_LBL)
        Me.Controls.Add(Me.KolonneNavn_CBX)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VaelgKolonneNavn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Columns Selection"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents KolonneNavn_CBX As ComboBox
    Friend WithEvents Kolonne_LBL As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Abort_BTN As Button
End Class
