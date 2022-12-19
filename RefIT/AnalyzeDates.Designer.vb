<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalyzeDates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalyzeDates))
        Me.DateChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.StartDate = New System.Windows.Forms.DateTimePicker()
        Me.EndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Select_BTN = New System.Windows.Forms.Button()
        Me.Exit_BTN = New System.Windows.Forms.Button()
        Me.ToFromDate_DGW = New System.Windows.Forms.DataGridView()
        Me.Reset_BTN = New System.Windows.Forms.Button()
        CType(Me.DateChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToFromDate_DGW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateChart
        '
        ChartArea1.Name = "ChartArea1"
        Me.DateChart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.DateChart.Legends.Add(Legend1)
        Me.DateChart.Location = New System.Drawing.Point(37, 24)
        Me.DateChart.Name = "DateChart"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series1.IsVisibleInLegend = False
        Series1.Legend = "Legend1"
        Series1.MarkerColor = System.Drawing.Color.OliveDrab
        Series1.Name = "DateRange"
        Series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Date]
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series2.IsVisibleInLegend = False
        Series2.Legend = "Legend1"
        Series2.MarkerColor = System.Drawing.Color.Brown
        Series2.Name = "Low"
        Series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Date]
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.MarkerColor = System.Drawing.Color.Brown
        Series3.Name = "High"
        Series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Date]
        Me.DateChart.Series.Add(Series1)
        Me.DateChart.Series.Add(Series2)
        Me.DateChart.Series.Add(Series3)
        Me.DateChart.Size = New System.Drawing.Size(728, 475)
        Me.DateChart.TabIndex = 0
        Me.DateChart.Text = "Chart1"
        '
        'StartDate
        '
        Me.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.StartDate.Location = New System.Drawing.Point(178, 519)
        Me.StartDate.Name = "StartDate"
        Me.StartDate.Size = New System.Drawing.Size(97, 23)
        Me.StartDate.TabIndex = 1
        '
        'EndDate
        '
        Me.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.EndDate.Location = New System.Drawing.Point(328, 519)
        Me.EndDate.Name = "EndDate"
        Me.EndDate.Size = New System.Drawing.Size(97, 23)
        Me.EndDate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(74, 523)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Analyse from :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(290, 523)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To :"
        '
        'Select_BTN
        '
        Me.Select_BTN.Location = New System.Drawing.Point(448, 512)
        Me.Select_BTN.Name = "Select_BTN"
        Me.Select_BTN.Size = New System.Drawing.Size(113, 37)
        Me.Select_BTN.TabIndex = 5
        Me.Select_BTN.Text = "Save settings"
        Me.Select_BTN.UseVisualStyleBackColor = True
        '
        'Exit_BTN
        '
        Me.Exit_BTN.Location = New System.Drawing.Point(1032, 512)
        Me.Exit_BTN.Name = "Exit_BTN"
        Me.Exit_BTN.Size = New System.Drawing.Size(113, 37)
        Me.Exit_BTN.TabIndex = 6
        Me.Exit_BTN.Text = "Exit"
        Me.Exit_BTN.UseVisualStyleBackColor = True
        '
        'ToFromDate_DGW
        '
        Me.ToFromDate_DGW.AllowUserToAddRows = False
        Me.ToFromDate_DGW.AllowUserToDeleteRows = False
        Me.ToFromDate_DGW.AllowUserToResizeColumns = False
        Me.ToFromDate_DGW.AllowUserToResizeRows = False
        Me.ToFromDate_DGW.BackgroundColor = System.Drawing.Color.White
        Me.ToFromDate_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ToFromDate_DGW.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ToFromDate_DGW.Location = New System.Drawing.Point(784, 24)
        Me.ToFromDate_DGW.MultiSelect = False
        Me.ToFromDate_DGW.Name = "ToFromDate_DGW"
        Me.ToFromDate_DGW.ReadOnly = True
        Me.ToFromDate_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ToFromDate_DGW.Size = New System.Drawing.Size(361, 475)
        Me.ToFromDate_DGW.TabIndex = 7
        '
        'Reset_BTN
        '
        Me.Reset_BTN.Location = New System.Drawing.Point(580, 512)
        Me.Reset_BTN.Name = "Reset_BTN"
        Me.Reset_BTN.Size = New System.Drawing.Size(113, 37)
        Me.Reset_BTN.TabIndex = 8
        Me.Reset_BTN.Text = "Reset Dates"
        Me.Reset_BTN.UseVisualStyleBackColor = True
        '
        'AnalyzeDates
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1181, 554)
        Me.Controls.Add(Me.Reset_BTN)
        Me.Controls.Add(Me.ToFromDate_DGW)
        Me.Controls.Add(Me.Exit_BTN)
        Me.Controls.Add(Me.Select_BTN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EndDate)
        Me.Controls.Add(Me.StartDate)
        Me.Controls.Add(Me.DateChart)
        Me.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AnalyzeDates"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Analysis Dates"
        Me.TopMost = True
        CType(Me.DateChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToFromDate_DGW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateChart As DataVisualization.Charting.Chart
    Friend WithEvents StartDate As DateTimePicker
    Friend WithEvents EndDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Select_BTN As Button
    Friend WithEvents Exit_BTN As Button
    Friend WithEvents ToFromDate_DGW As DataGridView
    Friend WithEvents Reset_BTN As Button
End Class
