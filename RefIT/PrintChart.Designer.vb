<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintChart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintChart))
        Me.ChartSelect_CBX = New System.Windows.Forms.ComboBox()
        Me.Select_BTN = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ChartSelect_CBX
        '
        Me.ChartSelect_CBX.FormattingEnabled = True
        Me.ChartSelect_CBX.Items.AddRange(New Object() {"Data plot chart", "Percentile chart", "Normal distribution chart"})
        Me.ChartSelect_CBX.Location = New System.Drawing.Point(12, 50)
        Me.ChartSelect_CBX.Name = "ChartSelect_CBX"
        Me.ChartSelect_CBX.Size = New System.Drawing.Size(147, 21)
        Me.ChartSelect_CBX.TabIndex = 0
        '
        'Select_BTN
        '
        Me.Select_BTN.Location = New System.Drawing.Point(48, 94)
        Me.Select_BTN.Name = "Select_BTN"
        Me.Select_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Select_BTN.TabIndex = 1
        Me.Select_BTN.Text = "Select"
        Me.Select_BTN.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Please select chart to save"
        '
        'PrintChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(171, 129)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Select_BTN)
        Me.Controls.Add(Me.ChartSelect_CBX)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PrintChart"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PrintChart"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChartSelect_CBX As ComboBox
    Friend WithEvents Select_BTN As Button
    Friend WithEvents Label1 As Label
End Class
