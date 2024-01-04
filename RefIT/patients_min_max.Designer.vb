<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class patients_min_max
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(patients_min_max))
        Me.Min_Max_Chart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Export_BTN = New System.Windows.Forms.Button()
        Me.Exit_BTN = New System.Windows.Forms.Button()
        Me.Sort_CBX = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SDorPercentile = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.Min_Max_Chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Min_Max_Chart
        '
        ChartArea1.Name = "ChartArea1"
        Me.Min_Max_Chart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Min_Max_Chart.Legends.Add(Legend1)
        Me.Min_Max_Chart.Location = New System.Drawing.Point(52, 12)
        Me.Min_Max_Chart.Name = "Min_Max_Chart"
        Me.Min_Max_Chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series1.Legend = "Legend1"
        Series1.LegendText = "Patient Average"
        Series1.MarkerColor = System.Drawing.Color.Black
        Series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross
        Series1.Name = "Avg"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series2.LabelForeColor = System.Drawing.Color.Red
        Series2.Legend = "Legend1"
        Series2.LegendText = "Highest Sample"
        Series2.MarkerColor = System.Drawing.Color.Red
        Series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
        Series2.Name = "Max"
        Series3.BorderWidth = 2
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series3.Color = System.Drawing.Color.White
        Series3.LabelForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series3.Legend = "Legend1"
        Series3.LegendText = "Lowest Sample"
        Series3.MarkerColor = System.Drawing.Color.Green
        Series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
        Series3.Name = "Min"
        Series3.ShadowColor = System.Drawing.Color.Green
        Series3.YValuesPerPoint = 2
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series4.LabelForeColor = System.Drawing.Color.Gray
        Series4.Legend = "Legend1"
        Series4.LegendText = "Remaining Samples"
        Series4.MarkerColor = System.Drawing.Color.Blue
        Series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
        Series4.Name = "Others"
        Series5.BorderWidth = 2
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Color = System.Drawing.Color.Black
        Series5.Legend = "Legend1"
        Series5.Name = "2xSD"
        Series5.ShadowColor = System.Drawing.Color.Black
        Series6.BorderWidth = 2
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Color = System.Drawing.Color.Black
        Series6.Legend = "Legend1"
        Series6.Name = "-2xSD"
        Series6.ShadowColor = System.Drawing.Color.Black
        Series7.BorderWidth = 2
        Series7.ChartArea = "ChartArea1"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series7.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series7.Legend = "Legend1"
        Series7.Name = "Median"
        Series7.ShadowColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series8.BorderWidth = 2
        Series8.ChartArea = "ChartArea1"
        Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series8.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series8.Legend = "Legend1"
        Series8.Name = "Average"
        Series8.ShadowColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series9.BorderWidth = 2
        Series9.ChartArea = "ChartArea1"
        Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series9.Color = System.Drawing.Color.Black
        Series9.Legend = "Legend1"
        Series9.LegendText = "Low Percentile"
        Series9.Name = "LowP"
        Series9.ShadowColor = System.Drawing.Color.Black
        Series10.BorderWidth = 2
        Series10.ChartArea = "ChartArea1"
        Series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series10.Color = System.Drawing.Color.Black
        Series10.Legend = "Legend1"
        Series10.LegendText = "High Percentile"
        Series10.Name = "HighP"
        Series10.ShadowColor = System.Drawing.Color.Black
        Me.Min_Max_Chart.Series.Add(Series1)
        Me.Min_Max_Chart.Series.Add(Series2)
        Me.Min_Max_Chart.Series.Add(Series3)
        Me.Min_Max_Chart.Series.Add(Series4)
        Me.Min_Max_Chart.Series.Add(Series5)
        Me.Min_Max_Chart.Series.Add(Series6)
        Me.Min_Max_Chart.Series.Add(Series7)
        Me.Min_Max_Chart.Series.Add(Series8)
        Me.Min_Max_Chart.Series.Add(Series9)
        Me.Min_Max_Chart.Series.Add(Series10)
        Me.Min_Max_Chart.Size = New System.Drawing.Size(736, 390)
        Me.Min_Max_Chart.TabIndex = 0
        Me.Min_Max_Chart.Text = "Chart1"
        Title1.Font = New System.Drawing.Font("Microsoft Tai Le", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Title1"
        Title1.Text = "Rank Plot"
        Me.Min_Max_Chart.Titles.Add(Title1)
        '
        'Export_BTN
        '
        Me.Export_BTN.Location = New System.Drawing.Point(632, 415)
        Me.Export_BTN.Name = "Export_BTN"
        Me.Export_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Export_BTN.TabIndex = 1
        Me.Export_BTN.Text = "Export Chart"
        Me.Export_BTN.UseVisualStyleBackColor = True
        '
        'Exit_BTN
        '
        Me.Exit_BTN.Location = New System.Drawing.Point(713, 415)
        Me.Exit_BTN.Name = "Exit_BTN"
        Me.Exit_BTN.Size = New System.Drawing.Size(75, 23)
        Me.Exit_BTN.TabIndex = 2
        Me.Exit_BTN.Text = "Exit"
        Me.Exit_BTN.UseVisualStyleBackColor = True
        '
        'Sort_CBX
        '
        Me.Sort_CBX.FormattingEnabled = True
        Me.Sort_CBX.Items.AddRange(New Object() {"Minimum", "Maximum"})
        Me.Sort_CBX.Location = New System.Drawing.Point(102, 417)
        Me.Sort_CBX.Name = "Sort_CBX"
        Me.Sort_CBX.Size = New System.Drawing.Size(121, 21)
        Me.Sort_CBX.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 420)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Rank by"
        '
        'SDorPercentile
        '
        Me.SDorPercentile.FormattingEnabled = True
        Me.SDorPercentile.Items.AddRange(New Object() {"Low/High Percentile", "2x Standard Deviation", "None"})
        Me.SDorPercentile.Location = New System.Drawing.Point(310, 417)
        Me.SDorPercentile.Name = "SDorPercentile"
        Me.SDorPercentile.Size = New System.Drawing.Size(121, 21)
        Me.SDorPercentile.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(241, 420)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Show Limits"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(455, 420)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(99, 17)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "Show all results"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'patients_min_max
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SDorPercentile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Sort_CBX)
        Me.Controls.Add(Me.Exit_BTN)
        Me.Controls.Add(Me.Export_BTN)
        Me.Controls.Add(Me.Min_Max_Chart)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "patients_min_max"
        Me.Text = "Rank plot of all patient data"
        CType(Me.Min_Max_Chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Min_Max_Chart As DataVisualization.Charting.Chart
    Friend WithEvents Export_BTN As Button
    Friend WithEvents Exit_BTN As Button
    Friend WithEvents Sort_CBX As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SDorPercentile As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBox1 As CheckBox
End Class
