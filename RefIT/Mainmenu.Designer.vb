<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Mainmenu
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint4 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mainmenu))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.RefChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Normal = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.MaxNr = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.MinNr = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Kon_CBX = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tukey_CHK = New System.Windows.Forms.CheckBox()
        Me.Reset_BTN = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TotalP_LBL = New System.Windows.Forms.Label()
        Me.Udentid_LBL = New System.Windows.Forms.Label()
        Me.InfoChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Udenmaal_LBL = New System.Windows.Forms.Label()
        Me.Ikkeafs_LBL = New System.Windows.Forms.Label()
        Me.Stat4 = New System.Windows.Forms.Label()
        Me.Stat3 = New System.Windows.Forms.Label()
        Me.Stat2 = New System.Windows.Forms.Label()
        Me.Stat1 = New System.Windows.Forms.Label()
        Me.Kvnt_CBX = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Analyse_DGV = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportCurrentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ST1 = New System.Windows.Forms.StatusStrip()
        Me.Progress_TXT = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Progress_PB = New System.Windows.Forms.ToolStripProgressBar()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tid_Nr = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HojFrac_Nr = New System.Windows.Forms.NumericUpDown()
        Me.LavFrac_Nr = New System.Windows.Forms.NumericUpDown()
        Me.PerMeth_CBX = New System.Windows.Forms.ComboBox()
        Me.Analyse_BTN = New System.Windows.Forms.Button()
        Me.BatchCalc_BTN = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RefChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Normal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxNr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MinNr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.InfoChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Analyse_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.ST1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Tid_Nr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HojFrac_Nr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LavFrac_Nr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(245, 47)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(572, 400)
        Me.DataGridView1.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'RefChart
        '
        Me.RefChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefChart.BackColor = System.Drawing.SystemColors.Control
        Me.RefChart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea1.BackColor = System.Drawing.SystemColors.ControlDark
        ChartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea1.Name = "RefChart"
        Me.RefChart.ChartAreas.Add(ChartArea1)
        Legend1.Enabled = False
        Legend1.Name = "Legend1"
        Me.RefChart.Legends.Add(Legend1)
        Me.RefChart.Location = New System.Drawing.Point(843, 47)
        Me.RefChart.Name = "RefChart"
        Series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series1.BackSecondaryColor = System.Drawing.Color.Transparent
        Series1.ChartArea = "RefChart"
        Series1.Color = System.Drawing.Color.Red
        Series1.CustomProperties = "LineTension=0.3"
        Series1.IsVisibleInLegend = False
        Series1.Legend = "Legend1"
        Series1.Name = "Lav"
        Series2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series2.BackSecondaryColor = System.Drawing.Color.Transparent
        Series2.ChartArea = "RefChart"
        Series2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series2.IsVisibleInLegend = False
        Series2.Legend = "Legend1"
        Series2.Name = "Ref"
        Series3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series3.BackSecondaryColor = System.Drawing.Color.Transparent
        Series3.ChartArea = "RefChart"
        Series3.Color = System.Drawing.Color.Red
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.Name = "Hoj"
        Me.RefChart.Series.Add(Series1)
        Me.RefChart.Series.Add(Series2)
        Me.RefChart.Series.Add(Series3)
        Me.RefChart.Size = New System.Drawing.Size(400, 400)
        Me.RefChart.TabIndex = 6
        Me.RefChart.Text = "Reference Range"
        Title1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Fractile plot"
        Title1.Text = "Percentile plot"
        Me.RefChart.Titles.Add(Title1)
        '
        'Normal
        '
        Me.Normal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Normal.BackColor = System.Drawing.SystemColors.Control
        Me.Normal.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea2.BackColor = System.Drawing.SystemColors.ControlDark
        ChartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea2.Name = "Normal"
        Me.Normal.ChartAreas.Add(ChartArea2)
        Legend2.Enabled = False
        Legend2.Name = "Normal fordelings plot"
        Me.Normal.Legends.Add(Legend2)
        Me.Normal.Location = New System.Drawing.Point(843, 472)
        Me.Normal.Name = "Normal"
        Series4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series4.BackSecondaryColor = System.Drawing.Color.Transparent
        Series4.ChartArea = "Normal"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
        Series4.Color = System.Drawing.Color.Red
        Series4.CustomProperties = "LineTension=0.3"
        Series4.Legend = "Normal fordelings plot"
        Series4.Name = "Lav"
        Series5.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series5.ChartArea = "Normal"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
        Series5.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series5.Legend = "Normal fordelings plot"
        Series5.Name = "Normal"
        Series6.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        Series6.ChartArea = "Normal"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
        Series6.Color = System.Drawing.Color.Red
        Series6.Legend = "Normal fordelings plot"
        Series6.Name = "Hoj"
        Me.Normal.Series.Add(Series4)
        Me.Normal.Series.Add(Series5)
        Me.Normal.Series.Add(Series6)
        Me.Normal.Size = New System.Drawing.Size(400, 400)
        Me.Normal.TabIndex = 7
        Me.Normal.Text = "Normal distribution"
        Title2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title2.Name = "Title1"
        Title2.Text = "Normaldistribution plot"
        Me.Normal.Titles.Add(Title2)
        '
        'MaxNr
        '
        Me.MaxNr.Location = New System.Drawing.Point(128, 62)
        Me.MaxNr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.MaxNr.Name = "MaxNr"
        Me.MaxNr.Size = New System.Drawing.Size(47, 20)
        Me.MaxNr.TabIndex = 22
        Me.MaxNr.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(113, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "to"
        '
        'MinNr
        '
        Me.MinNr.Location = New System.Drawing.Point(63, 62)
        Me.MinNr.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.MinNr.Name = "MinNr"
        Me.MinNr.Size = New System.Drawing.Size(47, 20)
        Me.MinNr.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Age from"
        '
        'Kon_CBX
        '
        Me.Kon_CBX.FormattingEnabled = True
        Me.Kon_CBX.Items.AddRange(New Object() {"Both", "Females", "Males"})
        Me.Kon_CBX.Location = New System.Drawing.Point(61, 21)
        Me.Kon_CBX.Name = "Kon_CBX"
        Me.Kon_CBX.Size = New System.Drawing.Size(114, 21)
        Me.Kon_CBX.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Gender"
        '
        'Tukey_CHK
        '
        Me.Tukey_CHK.AutoSize = True
        Me.Tukey_CHK.Location = New System.Drawing.Point(16, 268)
        Me.Tukey_CHK.Name = "Tukey_CHK"
        Me.Tukey_CHK.Size = New System.Drawing.Size(114, 17)
        Me.Tukey_CHK.TabIndex = 6
        Me.Tukey_CHK.Text = "Tukeys Outlier rule"
        Me.Tukey_CHK.UseVisualStyleBackColor = True
        '
        'Reset_BTN
        '
        Me.Reset_BTN.BackColor = System.Drawing.SystemColors.Control
        Me.Reset_BTN.Location = New System.Drawing.Point(20, 767)
        Me.Reset_BTN.Name = "Reset_BTN"
        Me.Reset_BTN.Size = New System.Drawing.Size(193, 37)
        Me.Reset_BTN.TabIndex = 24
        Me.Reset_BTN.Text = "Clear Analysis"
        Me.Reset_BTN.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TotalP_LBL)
        Me.GroupBox1.Controls.Add(Me.Udentid_LBL)
        Me.GroupBox1.Controls.Add(Me.InfoChart)
        Me.GroupBox1.Controls.Add(Me.Udenmaal_LBL)
        Me.GroupBox1.Controls.Add(Me.Ikkeafs_LBL)
        Me.GroupBox1.Controls.Add(Me.Stat4)
        Me.GroupBox1.Controls.Add(Me.Stat3)
        Me.GroupBox1.Controls.Add(Me.Stat2)
        Me.GroupBox1.Controls.Add(Me.Stat1)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 306)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sample Statistics"
        '
        'TotalP_LBL
        '
        Me.TotalP_LBL.AutoSize = True
        Me.TotalP_LBL.Location = New System.Drawing.Point(146, 86)
        Me.TotalP_LBL.Name = "TotalP_LBL"
        Me.TotalP_LBL.Size = New System.Drawing.Size(13, 13)
        Me.TotalP_LBL.TabIndex = 7
        Me.TotalP_LBL.Text = "0"
        Me.TotalP_LBL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Udentid_LBL
        '
        Me.Udentid_LBL.AutoSize = True
        Me.Udentid_LBL.Location = New System.Drawing.Point(146, 64)
        Me.Udentid_LBL.Name = "Udentid_LBL"
        Me.Udentid_LBL.Size = New System.Drawing.Size(13, 13)
        Me.Udentid_LBL.TabIndex = 6
        Me.Udentid_LBL.Text = "0"
        Me.Udentid_LBL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InfoChart
        '
        Me.InfoChart.BackColor = System.Drawing.Color.Transparent
        ChartArea3.BackColor = System.Drawing.Color.Transparent
        ChartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea3.BackSecondaryColor = System.Drawing.Color.Transparent
        ChartArea3.Name = "CleanC"
        Me.InfoChart.ChartAreas.Add(ChartArea3)
        Legend3.Enabled = False
        Legend3.Name = "#Not concluded"
        Legend4.Enabled = False
        Legend4.Name = "#Out of Range"
        Legend5.Enabled = False
        Legend5.Name = "#Excluded"
        Legend6.Enabled = False
        Legend6.Name = "#Total included"
        Me.InfoChart.Legends.Add(Legend3)
        Me.InfoChart.Legends.Add(Legend4)
        Me.InfoChart.Legends.Add(Legend5)
        Me.InfoChart.Legends.Add(Legend6)
        Me.InfoChart.Location = New System.Drawing.Point(6, 126)
        Me.InfoChart.Name = "InfoChart"
        Me.InfoChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series7.BorderColor = System.Drawing.Color.Transparent
        Series7.ChartArea = "CleanC"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut
        Series7.Color = System.Drawing.Color.Transparent
        Series7.CustomProperties = "DoughnutRadius=45, PieDrawingStyle=Concave"
        Series7.Legend = "#Not concluded"
        Series7.LegendText = "#Not concluded\n#Out of Range\n#Excluded\n#Total included"
        Series7.MarkerBorderColor = System.Drawing.Color.Transparent
        Series7.Name = "Status"
        Series7.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen
        DataPoint1.Label = ""
        DataPoint1.LegendText = "#Not concluded"
        DataPoint2.Label = ""
        DataPoint2.LegendText = "#Out of Range"
        DataPoint3.Label = ""
        DataPoint3.LegendText = "#Excluded"
        DataPoint4.Label = ""
        DataPoint4.LegendText = "#Total included"
        Series7.Points.Add(DataPoint1)
        Series7.Points.Add(DataPoint2)
        Series7.Points.Add(DataPoint3)
        Series7.Points.Add(DataPoint4)
        Series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[String]
        Series7.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Me.InfoChart.Series.Add(Series7)
        Me.InfoChart.Size = New System.Drawing.Size(178, 168)
        Me.InfoChart.TabIndex = 32
        Me.InfoChart.Text = "CleanChart"
        Title3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title3.Name = "Sample Statistics"
        Title3.Text = "Sample Statistics"
        Me.InfoChart.Titles.Add(Title3)
        '
        'Udenmaal_LBL
        '
        Me.Udenmaal_LBL.AutoSize = True
        Me.Udenmaal_LBL.Location = New System.Drawing.Point(146, 42)
        Me.Udenmaal_LBL.Name = "Udenmaal_LBL"
        Me.Udenmaal_LBL.Size = New System.Drawing.Size(13, 13)
        Me.Udenmaal_LBL.TabIndex = 5
        Me.Udenmaal_LBL.Text = "0"
        Me.Udenmaal_LBL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Ikkeafs_LBL
        '
        Me.Ikkeafs_LBL.AutoSize = True
        Me.Ikkeafs_LBL.Location = New System.Drawing.Point(146, 20)
        Me.Ikkeafs_LBL.Name = "Ikkeafs_LBL"
        Me.Ikkeafs_LBL.Size = New System.Drawing.Size(13, 13)
        Me.Ikkeafs_LBL.TabIndex = 4
        Me.Ikkeafs_LBL.Text = "0"
        Me.Ikkeafs_LBL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Stat4
        '
        Me.Stat4.AutoSize = True
        Me.Stat4.Location = New System.Drawing.Point(10, 86)
        Me.Stat4.Name = "Stat4"
        Me.Stat4.Size = New System.Drawing.Size(20, 13)
        Me.Stat4.TabIndex = 3
        Me.Stat4.Text = "# -"
        '
        'Stat3
        '
        Me.Stat3.AutoSize = True
        Me.Stat3.Location = New System.Drawing.Point(10, 64)
        Me.Stat3.Name = "Stat3"
        Me.Stat3.Size = New System.Drawing.Size(20, 13)
        Me.Stat3.TabIndex = 2
        Me.Stat3.Text = "# -"
        '
        'Stat2
        '
        Me.Stat2.AutoSize = True
        Me.Stat2.Location = New System.Drawing.Point(9, 42)
        Me.Stat2.Name = "Stat2"
        Me.Stat2.Size = New System.Drawing.Size(20, 13)
        Me.Stat2.TabIndex = 1
        Me.Stat2.Text = "# -"
        '
        'Stat1
        '
        Me.Stat1.AutoSize = True
        Me.Stat1.Location = New System.Drawing.Point(9, 20)
        Me.Stat1.Name = "Stat1"
        Me.Stat1.Size = New System.Drawing.Size(20, 13)
        Me.Stat1.TabIndex = 0
        Me.Stat1.Text = "# -"
        '
        'Kvnt_CBX
        '
        Me.Kvnt_CBX.FormattingEnabled = True
        Me.Kvnt_CBX.Location = New System.Drawing.Point(274, 2)
        Me.Kvnt_CBX.Name = "Kvnt_CBX"
        Me.Kvnt_CBX.Size = New System.Drawing.Size(194, 21)
        Me.Kvnt_CBX.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label3.Location = New System.Drawing.Point(219, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Assay ID"
        '
        'Analyse_DGV
        '
        Me.Analyse_DGV.AllowUserToAddRows = False
        Me.Analyse_DGV.AllowUserToDeleteRows = False
        Me.Analyse_DGV.AllowUserToResizeColumns = False
        Me.Analyse_DGV.AllowUserToResizeRows = False
        Me.Analyse_DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Analyse_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Analyse_DGV.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.Analyse_DGV.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Analyse_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Analyse_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Analyse_DGV.Location = New System.Drawing.Point(245, 472)
        Me.Analyse_DGV.Name = "Analyse_DGV"
        Me.Analyse_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Analyse_DGV.Size = New System.Drawing.Size(572, 400)
        Me.Analyse_DGV.TabIndex = 28
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem, Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1264, 24)
        Me.MenuStrip1.TabIndex = 29
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem1, Me.ExportCurrentToolStripMenuItem, Me.ExportAllToolStripMenuItem, Me.PrintReportToolStripMenuItem, Me.CloseToolStripMenuItem})
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ImportToolStripMenuItem.Text = "File"
        '
        'ImportToolStripMenuItem1
        '
        Me.ImportToolStripMenuItem1.Name = "ImportToolStripMenuItem1"
        Me.ImportToolStripMenuItem1.Size = New System.Drawing.Size(154, 22)
        Me.ImportToolStripMenuItem1.Text = "Import"
        '
        'ExportCurrentToolStripMenuItem
        '
        Me.ExportCurrentToolStripMenuItem.Name = "ExportCurrentToolStripMenuItem"
        Me.ExportCurrentToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ExportCurrentToolStripMenuItem.Text = "Export selected"
        '
        'ExportAllToolStripMenuItem
        '
        Me.ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem"
        Me.ExportAllToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ExportAllToolStripMenuItem.Text = "Export all"
        '
        'PrintReportToolStripMenuItem
        '
        Me.PrintReportToolStripMenuItem.Name = "PrintReportToolStripMenuItem"
        Me.PrintReportToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.PrintReportToolStripMenuItem.Text = "Print report"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetupToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'SetupToolStripMenuItem
        '
        Me.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem"
        Me.SetupToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.SetupToolStripMenuItem.Text = "Setup"
        '
        'ST1
        '
        Me.ST1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Progress_TXT, Me.Progress_PB})
        Me.ST1.Location = New System.Drawing.Point(0, 899)
        Me.ST1.Name = "ST1"
        Me.ST1.Size = New System.Drawing.Size(1264, 22)
        Me.ST1.TabIndex = 31
        Me.ST1.Text = "StatusStrip1"
        '
        'Progress_TXT
        '
        Me.Progress_TXT.Name = "Progress_TXT"
        Me.Progress_TXT.Size = New System.Drawing.Size(0, 17)
        '
        'Progress_PB
        '
        Me.Progress_PB.Name = "Progress_PB"
        Me.Progress_PB.Size = New System.Drawing.Size(100, 16)
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.MaxNr)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.MinNr)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Tid_Nr)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Kon_CBX)
        Me.GroupBox2.Controls.Add(Me.HojFrac_Nr)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.LavFrac_Nr)
        Me.GroupBox2.Controls.Add(Me.PerMeth_CBX)
        Me.GroupBox2.Controls.Add(Me.Tukey_CHK)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 362)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(193, 302)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Analysis"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(16, 183)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "Months"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(13, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 13)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Time between sample dates"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 213)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(146, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Percentile calculation method"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "High percentile "
        '
        'Tid_Nr
        '
        Me.Tid_Nr.Location = New System.Drawing.Point(118, 181)
        Me.Tid_Nr.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.Tid_Nr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Tid_Nr.Name = "Tid_Nr"
        Me.Tid_Nr.Size = New System.Drawing.Size(57, 20)
        Me.Tid_Nr.TabIndex = 24
        Me.Tid_Nr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Tid_Nr.Value = New Decimal(New Integer() {7, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Low percentile "
        '
        'HojFrac_Nr
        '
        Me.HojFrac_Nr.Location = New System.Drawing.Point(115, 130)
        Me.HojFrac_Nr.Name = "HojFrac_Nr"
        Me.HojFrac_Nr.Size = New System.Drawing.Size(60, 20)
        Me.HojFrac_Nr.TabIndex = 26
        Me.HojFrac_Nr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.HojFrac_Nr.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'LavFrac_Nr
        '
        Me.LavFrac_Nr.Location = New System.Drawing.Point(115, 102)
        Me.LavFrac_Nr.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.LavFrac_Nr.Name = "LavFrac_Nr"
        Me.LavFrac_Nr.Size = New System.Drawing.Size(60, 20)
        Me.LavFrac_Nr.TabIndex = 25
        Me.LavFrac_Nr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.LavFrac_Nr.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'PerMeth_CBX
        '
        Me.PerMeth_CBX.FormattingEnabled = True
        Me.PerMeth_CBX.Items.AddRange(New Object() {"Nearest-Rank", "Interpolated C=1 - Excel to 2013", "Interpolated C=0 - Excel after 2013"})
        Me.PerMeth_CBX.Location = New System.Drawing.Point(16, 229)
        Me.PerMeth_CBX.Name = "PerMeth_CBX"
        Me.PerMeth_CBX.Size = New System.Drawing.Size(159, 21)
        Me.PerMeth_CBX.TabIndex = 7
        '
        'Analyse_BTN
        '
        Me.Analyse_BTN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Analyse_BTN.Location = New System.Drawing.Point(20, 683)
        Me.Analyse_BTN.Name = "Analyse_BTN"
        Me.Analyse_BTN.Size = New System.Drawing.Size(193, 36)
        Me.Analyse_BTN.TabIndex = 30
        Me.Analyse_BTN.Text = "Calculate and Analyse"
        Me.Analyse_BTN.UseVisualStyleBackColor = False
        '
        'BatchCalc_BTN
        '
        Me.BatchCalc_BTN.BackColor = System.Drawing.SystemColors.Control
        Me.BatchCalc_BTN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BatchCalc_BTN.Location = New System.Drawing.Point(20, 725)
        Me.BatchCalc_BTN.Name = "BatchCalc_BTN"
        Me.BatchCalc_BTN.Size = New System.Drawing.Size(193, 36)
        Me.BatchCalc_BTN.TabIndex = 34
        Me.BatchCalc_BTN.Text = "Batch Calculate and Analyse"
        Me.BatchCalc_BTN.UseVisualStyleBackColor = False
        '
        'Mainmenu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1264, 921)
        Me.Controls.Add(Me.Analyse_BTN)
        Me.Controls.Add(Me.BatchCalc_BTN)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Reset_BTN)
        Me.Controls.Add(Me.ST1)
        Me.Controls.Add(Me.Analyse_DGV)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Kvnt_CBX)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Normal)
        Me.Controls.Add(Me.RefChart)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Mainmenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RefIT v. 0.9 - (C) 2022 - Jens Borggaard Larsen - Published under GPL"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RefChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Normal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxNr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MinNr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.InfoChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Analyse_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ST1.ResumeLayout(False)
        Me.ST1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.Tid_Nr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HojFrac_Nr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LavFrac_Nr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents RefChart As DataVisualization.Charting.Chart
    Friend WithEvents Normal As DataVisualization.Charting.Chart
    Friend WithEvents Tukey_CHK As CheckBox
    Friend WithEvents MaxNr As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents MinNr As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents Kon_CBX As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Reset_BTN As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Stat4 As Label
    Friend WithEvents Stat3 As Label
    Friend WithEvents Stat2 As Label
    Friend WithEvents Stat1 As Label
    Friend WithEvents TotalP_LBL As Label
    Friend WithEvents Udentid_LBL As Label
    Friend WithEvents Udenmaal_LBL As Label
    Friend WithEvents Ikkeafs_LBL As Label
    Friend WithEvents Kvnt_CBX As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Analyse_DGV As DataGridView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ImportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ExportCurrentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ST1 As StatusStrip
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Progress_TXT As ToolStripStatusLabel
    Friend WithEvents Progress_PB As ToolStripProgressBar
    Friend WithEvents InfoChart As DataVisualization.Charting.Chart
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents HojFrac_Nr As NumericUpDown
    Friend WithEvents LavFrac_Nr As NumericUpDown
    Friend WithEvents Tid_Nr As NumericUpDown
    Friend WithEvents PerMeth_CBX As ComboBox
    Friend WithEvents BatchCalc_BTN As Button
    Friend WithEvents Analyse_BTN As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
End Class
