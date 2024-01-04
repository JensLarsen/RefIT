Option Explicit On

'    RefIT - Reference Ranges for Therapeutic Drug Monitoring
'    Copyright(C) 2023 v. 1.1 Jens Borggaard Larsen - jen7lar@gmail.com
'
'    This program Is free software: you can redistribute it And/Or modify
'    it under the terms Of the GNU General Public License As published by
'    the Free Software Foundation, either version 3 Of the License, Or
'    (at your option) any later version.
'
'    This program Is distributed In the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty Of
'    MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License For more details.
'
'    You should have received a copy Of the GNU General Public License
'    along with this program.  If Not, see < https: //www.gnu.org/licenses/>.


Imports System.Drawing.Printing

Public Class Mainmenu

    Public Shared Check As Boolean 'dummy for aborting
    Public CBXForm As String 'used to get value from various selection boxes
    Public AnalyseNr As Integer = 0
    Public Shared Raw_DS As New DataSet 'dataset containing all rawdata
    Public Shared OriginalDT As New DataTable 'table all data are loaded into
    Public Shared CleanDT As New DataTable
    Public Shared MinMaxDate As New DataTable 'Datatable containing dates to analyze from and to for each drug - Filled during import check of dates and
    Public Shared CurrentTable As String 'currently selected datatable that is viewed
    Public Shared UpdateChart As Boolean 'used with batch cal culation, to prevent charting drawing after eache
    Public Shared UpdateDGV As Boolean   'used with batch calculation, to prevent datagridview for being updated after eache
    Public Shared Analyse_DS As New DataSet 'contains calculated datatables
    Public Shared MinMaxDT As New DataTable ' Datatable to pass to min_max form
    Public Shared LowLimit As Double 'Public variable holdning current percentile lower limit 
    Public Shared HighLimit As Double 'Public variable holdning current percentile higher limit 
    Public Shared PMedian As Double 'Public variable holdning current percentile median 
    Public Shared Average As Double 'Public variable holdning current plot average 
    Public Shared STD As Double 'Public variable holdning current plot standard deviation 

    Private Sub Mainmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.PerMeth_CBX.SelectedIndex = 2
        Me.View_CBX.SelectedIndex = 0

        SetAnalyseTable()

        '***************************************************************************
        '**            Generates the Analysis Date To From Table                  **
        '***************************************************************************
        MinMaxDate.Columns.Add("_Analysis_")
        MinMaxDate.Columns("_Analysis_").ColumnName = ("Analysis")
        MinMaxDate.Columns.Add("_MinDate_")
        MinMaxDate.Columns("_MinDate_").ColumnName = ("From Date")
        MinMaxDate.Columns.Add("_MaxDate_")
        MinMaxDate.Columns("_MaxDate_").ColumnName = ("To Date")

    End Sub


    Private Sub SetAnalyseTable()
        '***************************************************************************
        '**            Generates the AnalysisDT table with sample statistics      **
        '***************************************************************************
        Dim AnalyseDT As New DataTable

        AnalyseDT.TableName = "AnalyseDT"

        AnalyseDT.Columns.Add("#")
        AnalyseDT.Columns("#").ColumnName = "#"

        AnalyseDT.Columns.Add("Analysis")
        AnalyseDT.Columns("Analysis").ColumnName = "Analysis"

        AnalyseDT.Columns.Add("Gender")
        AnalyseDT.Columns("Gender").ColumnName = "Gender"

        AnalyseDT.Columns.Add("Age", GetType(String))
        AnalyseDT.Columns("Age").ColumnName = "Age"

        AnalyseDT.Columns.Add("FromDate", GetType(Date))
        AnalyseDT.Columns("FromDate").ColumnName = "From Date"

        AnalyseDT.Columns.Add("ToDate", GetType(Date))
        AnalyseDT.Columns("ToDate").ColumnName = "To Date"

        AnalyseDT.Columns.Add("IndkProv", GetType(Integer))
        AnalyseDT.Columns("IndkProv").ColumnName = "# Samples"

        AnalyseDT.Columns.Add("Approved_samples", GetType(Integer))
        AnalyseDT.Columns("Approved_samples").ColumnName = "Incl. Samples"

        AnalyseDT.Columns.Add("Distinct_patients", GetType(Integer))
        AnalyseDT.Columns("Distinct_patients").ColumnName = "# Patients"

        AnalyseDT.Columns.Add("Percentile")
        AnalyseDT.Columns("Percentile").ColumnName = ("Percentile")

        AnalyseDT.Columns.Add("PerMethod")
        AnalyseDT.Columns("PerMethod").ColumnName = ("Percentile Analysis")

        AnalyseDT.Columns.Add("2SD")
        AnalyseDT.Columns("2SD").ColumnName = ("2SD")

        AnalyseDT.Columns.Add("Min")
        AnalyseDT.Columns("min").ColumnName = ("Min")

        AnalyseDT.Columns.Add("Max")
        AnalyseDT.Columns("max").ColumnName = ("Max")

        AnalyseDT.Columns.Add("Average")
        AnalyseDT.Columns("Average").ColumnName = ("Average")

        AnalyseDT.Columns.Add("Median")
        AnalyseDT.Columns("Median").ColumnName = ("Median")

        AnalyseDT.Columns.Add("Tukey")
        AnalyseDT.Columns("Tukey").ColumnName = ("Tukey Fences")

        AnalyseDT.Columns.Add("Outliers")
        AnalyseDT.Columns("Outliers").ColumnName = ("#Outliers")

        Analyse_DS.Tables.Add(AnalyseDT)

        Analyse_DGV.DataSource = AnalyseDT

        For Each c As DataGridViewColumn In Analyse_DGV.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

    End Sub

    Private Sub ImportToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem1.Click
        '***********************************************************************************
        '**Imports and perform initial calculations. This is the main code branching out  **
        '***********************************************************************************

        '***initially clears everything, if a previous file have been loaded****************

        DataGridView1.DataSource = Nothing
        Analyse_DGV.DataSource = Nothing

        Try
            Me.InfoChart.Series("Status").Points.Clear()
            Me.Udenmaal_LBL.Text = "-"
            Me.Udentid_LBL.Text = "-"
            Me.Ikkeafs_LBL.Text = "-"
            Me.TotalP_LBL.Text = "-"

            Raw_DS.Tables.Clear()
            Analyse_DS.Tables.Clear()
            If Not IsNothing(OriginalDT) Then
                OriginalDT.Clear()
                OriginalDT.Columns.Clear()
                Raw_DS.Tables.Add(OriginalDT)
            End If
            Me.Kvnt_CBX.Items.Clear()

            reset()

            Progress_TXT.Visible = True
            Progress_TXT.Text = "Please be patient......."

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error while importing file!" & vbNewLine & "- Please check that the file is not open in Excel")
            Progress_TXT.Text = "Error while importing file."
        End Try

        '*******************************Opens filedialogue to load file*********************************************
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = OpenFileDialog1.FileName
            Dim fi As New IO.FileInfo(filePath)
            Dim extn As String = fi.Extension
            Select Case extn
                Case ".xlsx", ".xlsm", ".xltx", ".xltm"
                Case Else
                    MsgBox("Wrong file type!" & vbNewLine & "Please convert it to .xlsx, .xlsm, .xltx, or xltm and try again.")
                    Progress_TXT.Visible = False
                    Progress_PB.Visible = False
                    Progress_TXT.Text = ""
                    Exit Sub
            End Select

            Me.UseWaitCursor = True
            Application.DoEvents()

            OriginalDT = Import(filePath) ' calls function for reading file


            If IsNothing(OriginalDT) Then Exit Sub 'if nothing is returned then exit sub

            Try
                Progress_PB.Style = ProgressBarStyle.Continuous

                If Check = False Then
                    Progress_TXT.Text = "Loading aborted"
                    Progress_PB.Visible = False
                    Me.UseWaitCursor = False
                    Exit Sub 'Exit if error during import
                End If

                CleanDT = Rens(OriginalDT) 'cleans the dataset from unwanted samples

                CleanDT = Datokontrol(CleanDT) 'checks the dates and deletes row with errors
                If CleanDT.Rows.Count = 0 Then 'if dates could not be found in the table abort
                    Me.UseWaitCursor = False
                    MsgBox("The selected field for date has an incorrect format." & vbNewLine & "Please correct this in excell, and load file again!")
                    Exit Sub
                End If

                If CheckCPR(CleanDT) = True Then 'check if sample id is a danish social security number
                    CleanDT = CalculateID(CleanDT) 'if so calculate birthday and anonymize
                Else
                    CleanDT = NonCPR(CleanDT) 'else anonymize
                End If

                '********************Removes all columns that are not added containing "_" eg "_Age_"
                Dim colname As String
                For i As Integer = CleanDT.Columns.Count - 1 To 0 Step -1
                    colname = CleanDT.Columns(i).ColumnName
                    If colname.Substring(0, 1) <> "_" Then
                        CleanDT.Columns.Remove(colname)
                    End If
                Next

                If CleanDT.Rows.Count > 0 Then
                    KolonneRaekke(CleanDT)
                    FyldKvnTilCBX(CleanDT)
                Else
                    Me.UseWaitCursor = False
                    MsgBox("The selected field for date has an incorrect format." & vbNewLine & "Please correct this in excell, and load file again!")
                    Exit Sub
                End If

                Me.Kvnt_CBX.SelectedIndex = 0

                Progress_PB.Visible = False
                Progress_TXT.Text = "Finished loading"

            Catch ex As Exception
                LogFejl(ex.ToString)
                Me.UseWaitCursor = False
                MsgBox("Error while importing file!" & vbNewLine & "Please check that the columns and format of import file is correct.")
            End Try
            Me.UseWaitCursor = False
        End If
    End Sub

    Private Sub FyldKvnTilCBX(dt As DataTable)
        Dim KvntNames = From v In dt.AsEnumerable Select v.Field(Of String)("_Analysis_") Distinct
        Dim dview As New DataView(dt)
        Dim MinDate As Date
        Dim MaxDate As Date
        Dim AddRow As DataRow

        MinMaxDate.Rows.Clear()

        If Not IsNothing(KvntNames) Then
            For Each t As String In KvntNames
                Me.Kvnt_CBX.Items.Add(t)

                dview.RowFilter = "_Analysis_ = '" & t & "'"
                dview.Sort = "_Analysis_Date_ DESC"
                MaxDate = dview.Item(0)("_Analysis_Date_")
                dview.Sort = "_Analysis_Date_ ASC"
                MinDate = dview.Item(0)("_Analysis_Date_")

                AddRow = MinMaxDate.NewRow()
                AddRow.Item("Analysis") = t
                AddRow.Item("From Date") = MinDate
                AddRow.Item("To Date") = MaxDate

                MinMaxDate.Rows.Add(AddRow)
            Next
            Me.Kvnt_CBX.Sorted = True
        End If
    End Sub

    Private Function Kvantiteter(t As String)

        Dim NyTabel As New DataTable(t)
        Dim lav As Integer
        Dim hoj As Integer
        Dim ref As Integer

        Try

            Dim selectrows() As DataRow = CleanDT.Select("_Analysis_ = '" & t & "'")
                    If Not IsNothing(selectrows) Then

                NyTabel = selectrows.CopyToDataTable()

                Select Case Me.Model_CBX.SelectedItem
                    Case "TDM model", "TDM model -last sample"
                        NyTabel = TDMmodel(NyTabel)
                    Case "First result from patient", "Last result from patient"
                        NyTabel = FirstLastmodel(NyTabel)
                    Case "All data from patient"
                        NyTabel = Allmodel(NyTabel)
                End Select

                NyTabel.TableName = t

            End If

            lav = CInt(Me.TotalP_LBL.Text) * Me.LavFrac_Nr.Value / 100
            hoj = CInt(Me.TotalP_LBL.Text) * ((100 - Me.HojFrac_Nr.Value) / 100)
            ref = CInt(Me.TotalP_LBL.Text) - (lav + hoj)

            Me.Stat1.Text = "#Excluded"
            Me.Stat2.Text = "#Low Percentile"
            Me.Stat3.Text = "#Ref. Range"
            Me.Stat4.Text = "#High Percentile"

            Me.Ikkeafs_LBL.Text = Me.Udentid_LBL.Text
            Me.Udenmaal_LBL.Text = lav
            Me.Udentid_LBL.Text = ref
            Me.TotalP_LBL.Text = hoj

            InfoChart.Titles(0).Text = t

            InfoChart.Series("Status").Points.Clear()
            InfoChart.Series("Status").Points.AddXY("#Excluded", CInt(Me.Udentid_LBL.Text))

            InfoChart.Series("Status").Points.AddXY("#Low", lav)
            InfoChart.Series("Status").Points.AddXY("#Ref. Range", ref)
            InfoChart.Series("Status").Points.AddXY("#High", hoj)

            InfoChart.Series("Status").Points(0).Color = Color.CornflowerBlue
            InfoChart.Series("Status").Points(1).Color = Color.IndianRed
            InfoChart.Series("Status").Points(2).Color = Color.ForestGreen
            InfoChart.Series("Status").Points(3).Color = Color.IndianRed

            InfoChart.ChartAreas("CleanC").RecalculateAxesScale()

            Return (NyTabel)

        Catch ex As Exception
            LogFejl(ex.ToString)
            'MsgBox("Error in Kvantiteter!")
        End Try
    End Function

    Private Function TDMmodel(dt As DataTable)

        Try

            dt.DefaultView.Sort = "_ID_ ASC, _Analysis_Date_ ASC"
            dt = dt.DefaultView.ToTable

            Dim IDnr = From v In dt.AsEnumerable Select v.Field(Of String)("_ID_") Distinct
            Dim rCount As Integer = 0
            Dim Dato1 As Date
            Dim Dato2 As Date
            Dim PreRow As DataRow
            Dim ExNr As Integer = 0
            Dim InNr As Integer = 0

            Progress_TXT.Text = "Applying selection rules"
            Progress_PB.Maximum = dt.Rows.Count

            If Not IsNothing(IDnr) Then
                For Each t As String In IDnr
                    Dim selectrows = From v In dt.AsEnumerable Where v.Item("_ID_") = t Select v
                    If Not IsNothing(selectrows) Then

                        For Each r As DataRow In selectrows

                            InNr = InNr + 1
                            If rCount = 0 Then
                                r.Item("_Include_") = True
                                PreRow = r
                                Dato1 = r.Item("_Analysis_Date_")
                                rCount = rCount + 1
                            Else
                                Dato2 = r.Item("_Analysis_Date_")
                                If DateDiff(DateInterval.Month, Dato1, Dato2) < Me.Tid_Nr.Value Then
                                    PreRow.Item("_Include_") = False
                                    r.Item("_Include_") = True

                                    ExNr = ExNr + 1
                                    InNr = InNr - 1

                                    PreRow.Item("_Comment_") = DateDiff(DateInterval.Month, Dato1, Dato2) & " < Timeinterval"
                                Else
                                    PreRow.Item("_Include_") = True
                                    r.Item("_Include_") = True
                                End If
                                Dato1 = r.Item("_Analysis_Date_")
                                PreRow = r
                                rCount = rCount + 1
                            End If
                            Progress_PB.PerformStep()
                        Next
                        If Me.Model_CBX.SelectedItem = "TDM model -last sample" Then
                            PreRow.Item("_Include_") = False
                            PreRow.Item("_Comment_") = "Last sample removed"
                        End If
                    End If
                    Progress_PB.PerformStep()
                    Application.DoEvents()
                    selectrows = Nothing
                    rCount = 0
                Next
            End If

            Me.Udentid_LBL.Text = ExNr
            Me.TotalP_LBL.Text = InNr

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error performing selection rules")
        End Try

        Return dt
    End Function

    Private Function FirstLastmodel(dt As DataTable)

        Try

            Dim IDnr = From v In dt.AsEnumerable Select v.Field(Of String)("_ID_") Distinct
            Dim rCount As Integer = 0
            Dim comment As String
            Dim ExNr As Integer = 0
            Dim InNr As Integer = 0

            If Me.Model_CBX.SelectedItem = "First result from patient" Then
                dt.DefaultView.Sort = "_ID_ ASC, _Analysis_Date_ ASC"
                comment = "First measurement from patient"
            Else
                dt.DefaultView.Sort = "_ID_ ASC, _Analysis_Date_ DESC"
                comment = "Last measurement from patient"
            End If

            dt = dt.DefaultView.ToTable

            Progress_TXT.Text = "Applying selection rules"
            Progress_PB.Maximum = dt.Rows.Count

            If Not IsNothing(IDnr) Then
                For Each t As String In IDnr
                    Dim selectrows = From v In dt.AsEnumerable Where v.Item("_ID_") = t Select v
                    If Not IsNothing(selectrows) Then

                        For Each r As DataRow In selectrows

                            InNr = InNr + 1
                            If rCount = 0 Then
                                r.Item("_Include_") = True
                                r.Item("_Comment_") = comment
                                rCount = rCount + 1
                            Else
                                r.Item("_Include_") = False
                                ExNr = ExNr + 1
                                InNr = InNr - 1
                            End If
                            Progress_PB.PerformStep()
                        Next
                    End If
                    Progress_PB.PerformStep()
                    Application.DoEvents()
                    selectrows = Nothing
                    rCount = 0
                Next
            End If

            Me.Udentid_LBL.Text = ExNr
            Me.TotalP_LBL.Text = InNr

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error performing selection rules")
        End Try

        Return dt
    End Function
    Private Function Allmodel(dt As DataTable)

        Try

            dt.DefaultView.Sort = "_ID_ ASC, _Analysis_Date_ ASC"
            dt = dt.DefaultView.ToTable

            Dim IDnr = From v In dt.AsEnumerable Select v.Field(Of String)("_ID_") Distinct

            Dim ExNr As Integer = 0
            Dim InNr As Integer = 0

            Progress_TXT.Text = "Applying selection rules"
            Progress_PB.Maximum = dt.Rows.Count

            If Not IsNothing(IDnr) Then
                For Each t As String In IDnr
                    Dim selectrows = From v In dt.AsEnumerable Where v.Item("_ID_") = t Select v
                    If Not IsNothing(selectrows) Then
                        For Each r As DataRow In selectrows
                            r.Item("_Include_") = True
                            ExNr = ExNr + 1
                            InNr = InNr - 1
                            Progress_PB.PerformStep()
                        Next
                    End If
                    Progress_PB.PerformStep()
                    Application.DoEvents()
                    selectrows = Nothing
                Next
            End If

            Me.Udentid_LBL.Text = ExNr
            Me.TotalP_LBL.Text = InNr

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error performing selection rules")
        End Try

        Return dt
    End Function
    Private Sub BeregnEnkelt()
        Dim dt As DataTable

        If Me.Kvnt_CBX.Items.Count = 0 Then Exit Sub

        Try
            Progress_PB.Visible = True
            Progress_TXT.Visible = True
            Me.UseWaitCursor = True
            Application.DoEvents()

            My.Settings.TidsInterval = Me.Tid_Nr.Value
            dt = Kvantiteter(Me.Kvnt_CBX.SelectedItem)

            If dt.Rows.Count - 1 > 0 Then
                DataGridView1.DataSource = dt
                CurrentTable = dt.TableName
                Analyze()
                DataGridView1.Columns("_Include_").ReadOnly = False
            Else
                MsgBox("No samples selected based on this setting.")
            End If

            Progress_PB.Visible = False
            Progress_TXT.Visible = False
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            LogFejl(ex.ToString)
            Me.UseWaitCursor = False
            MsgBox("Database is empty - please check import file")
        End Try
        Me.UseWaitCursor = False
        Me.TabControl1.SelectedIndex = 1
    End Sub
    Private Sub BatchCalc()

        If Me.Kvnt_CBX.Items.Count = 0 Then Exit Sub

        UpdateChart = False
        UpdateDGV = False

        Dim dt As DataTable
        Dim Str As String

        Progress_PB.Visible = True
        Progress_TXT.Visible = True
        Me.UseWaitCursor = True
        Application.DoEvents()

        My.Settings.TidsInterval = Me.Tid_Nr.Value

        For i As Integer = 0 To Me.Kvnt_CBX.Items.Count - 1
            If i = Me.Kvnt_CBX.Items.Count - 1 Then
                UpdateChart = True
                UpdateDGV = True
            End If
            If Me.Kvnt_CBX.Items.Item(i).ToString <> "" Then
                Str = Me.Kvnt_CBX.Items.Item(i)
                dt = Kvantiteter(Str)
                CurrentTable = dt.TableName
                analyser(dt)
                If i = Me.Kvnt_CBX.Items.Count - 1 Then
                    DataGridView1.DataSource = dt
                    drawchart(dt)
                    Me.Kvnt_CBX.SelectedIndex = i
                    Analyse_DGV.ClearSelection()
                End If
            End If
        Next

        Progress_PB.Visible = False
        Progress_TXT.Visible = False
        Me.UseWaitCursor = False
        Me.TabControl1.SelectedIndex = 1

    End Sub
    Private Sub Analyze()

        Dim dt As DataTable = TryCast(DataGridView1.DataSource, DataTable).Copy()
        Call analyser(dt)

    End Sub
    Private Sub BatchCalc_BTN_Click(sender As Object, e As EventArgs) Handles BatchCalc_BTN.Click
        BatchCalc()
    End Sub

    Private Sub Analyse_BTN_Click(sender As Object, e As EventArgs) Handles Analyse_BTN.Click

        UpdateChart = True
        BeregnEnkelt()
        If DataGridView1.RowCount - 1 > 0 Then
            Dim dt As DataTable = TryCast(DataGridView1.DataSource, DataTable).Copy()
            CurrentTable = dt.TableName
        End If

    End Sub

    Private Sub Reset_BTN_Click(sender As Object, e As EventArgs) Handles Reset_BTN.Click
        Try
            Dim tekst As String = "Erase all analysed datasets - Proceed? "
            If MsgBox(tekst, vbYesNo) = vbNo Then Exit Sub
            reset()
        Catch ex As Exception
            LogFejl(ex.ToString)
        End Try

    End Sub
    Private Sub reset()

        Analyse_DS.Tables.Clear()
        Analyse_DGV.DataSource = Nothing
        Analyse_DGV.Refresh()

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        Me.RefChart.Series("Lav").Points.Clear()
        Me.RefChart.Series("Ref").Points.Clear()
        Me.RefChart.Series("Hoj").Points.Clear()
        Me.RefChart.Titles.Item("Title1").Text = ""

        Me.Normal.Series("Lav").Points.Clear()
        Me.Normal.Series("Normal").Points.Clear()
        Me.Normal.Series("Hoj").Points.Clear()
        Me.Normal.Titles.Item("Title1").Text = ""

        Me.DataPlot.Series("OutsideRange").Points.Clear()
        Me.DataPlot.Series("Outliers").Points.Clear()
        Me.DataPlot.Series("WithinRange").Points.Clear()
        Me.DataPlot.Series("LowP").Points.Clear()
        Me.DataPlot.Series("HighP").Points.Clear()
        Me.DataPlot.Series("Median").Points.Clear()
        Me.DataPlot.Series("LowSD").Points.Clear()
        Me.DataPlot.Series("HighSD").Points.Clear()
        Me.DataPlot.Series("Average").Points.Clear()
        Me.DataPlot.Titles.Item("Title1").Text = ""

        For s As Integer = 0 To Me.InfoChart.Series.Count - 1
            Me.InfoChart.Series(s).Points.Clear()
        Next

        Me.InfoChart.Titles(0).Text = "Statistics"

        Me.Model_CBX.SelectedIndex = 0
        Me.Kon_CBX.SelectedIndex = 0
        Me.View_CBX.SelectedIndex = 0
        Me.Tukey_CHK.Checked = False
        Me.MinNr.Value = 0
        Me.MaxNr.Value = 100
        Me.LavFrac_Nr.Value = 10
        Me.HojFrac_Nr.Value = 90
        SetAnalyseTable()

    End Sub

    Private Sub ExportCurrentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCurrentToolStripMenuItem.Click

        Dim ds As New DataSet
        Dim dt As DataTable
        Dim dta As DataTable
        Dim Tname As String

        If Analyse_DGV.SelectedRows.Count = 0 Then
            MsgBox("No table selected")
            Exit Sub
        End If

        dt = CType(Analyse_DGV.DataSource, DataTable).Clone()
        ds.Tables.Add(dt)

        For Each dr As DataGridViewRow In Analyse_DGV.SelectedRows
            Dim dra As DataRow = dt.NewRow
            For Each c As DataGridViewCell In dr.Cells
                dra.Item(c.ColumnIndex) = c.Value
            Next
            dt.Rows.Add(dra)
            Tname = dra.Item("Analysis") & "_" & CInt(dra.Item("#")) - 1
            If Analyse_DS.Tables.Contains(Tname) = True Then
                ds.Tables.Add(Analyse_DS.Tables(Tname).Copy)
            End If
            dra = Nothing
        Next
        Export(ds)

        ds = Nothing
        dt = Nothing
        dta = Nothing

    End Sub

    Private Sub ExportAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAllToolStripMenuItem.Click

        Export(Analyse_DS)

    End Sub

    Private Sub SetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetupToolStripMenuItem.Click
        SetupMain.Show()
    End Sub
    Private Sub CloseToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub Analyse_DGV_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Analyse_DGV.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim selectedtablename As String = Analyse_DGV.Rows(e.RowIndex).Cells("Analysis").Value & "_" & Analyse_DGV.Rows(e.RowIndex).Index.ToString
            Dim dt As DataTable = Analyse_DS.Tables.Item(selectedtablename).Copy()
            CurrentTable = dt.TableName
            Me.Kvnt_CBX.SelectedItem = Analyse_DGV.Rows(e.RowIndex).Cells("Analysis").Value
            DataGridView1.DataSource = dt

            Call drawchart(dt)
            '********************************************************************************
            '**                     Fill Data plot                                         **
            '********************************************************************************
            Dim dRow As DataRow = Mainmenu.Analyse_DS.Tables("AnalyseDT").NewRow()
            Dim n As Integer = 0

            For Each ec As DataGridViewCell In Analyse_DGV.Rows(e.RowIndex).Cells
                dRow.Item(n) = ec.Value
                n = n + 1
            Next

            Analyse.drawdataplot(dRow, dt)

        End If

    End Sub

    Private Sub drawchart(dt As DataTable)
        Dim dview As New DataView(dt)
        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"
        dt = TryCast(dview.ToTable, DataTable).Copy()

        Dim NewRow As DataRow = Mainmenu.Analyse_DS.Tables("AnalyseDT").NewRow() ' empty row used as dummy
        Dim Output() As Double = Analyse.Statistics(dt, NewRow) 'Calculates fractile limits, avg and SD
        Analyse.Charts(dt, Output) ' draws the charts using the datatable 
        NewRow = Nothing ' row is set to nothing
    End Sub


    Private mRow As Integer = 1
    Private newpage As Boolean = True

    Private Sub PrintDocument1_PrintPage(sender As System.Object,
                    e As PrintPageEventArgs) Handles PrintDocument1.PrintPage

        ' sets it to show '...' for long text
        Dim fmt As StringFormat = New StringFormat(StringFormatFlags.LineLimit)
        fmt.LineAlignment = StringAlignment.Center
        fmt.Trimming = StringTrimming.EllipsisCharacter
        Dim y As Int32 = e.MarginBounds.Top + 100
        Dim rc As Rectangle
        Dim x As Int32
        Dim h As Int32 = 0
        Dim row As DataGridViewRow
        Dim HeadFont As Font = New Font("Arial Black", 10)
        Dim drawFont As Font = New Font("Arial Black", 6)
        Dim nrFont As Font = New Font("Arial", 6)
        Dim username As String = "Printed by : " & Environment.UserName & vbNewLine & "Date : " & Date.Now.ToString


        Try
            ' print the header text for a new page
            '   use a grey bg just like the control
            If newpage Then
                '***************************************************
                x = e.MarginBounds.Left
                rc = New Rectangle(x, y - 100, 400, 80)
                e.Graphics.FillRectangle(Brushes.LightGray, rc)
                e.Graphics.DrawString(username, HeadFont, Brushes.Black, rc, fmt)

                '***************************************************
                row = Analyse_DGV.Rows(mRow)

                For Each cell As DataGridViewCell In row.Cells
                    ' since we are printing the control's view,
                    ' skip invidible columns
                    If cell.Visible Then
                        rc = New Rectangle(x, y, cell.Size.Width, cell.Size.Height)

                        e.Graphics.FillRectangle(Brushes.LightGray, rc)
                        e.Graphics.DrawRectangle(Pens.Black, rc)

                        ' reused in the data pront - should be a function
                        Select Case Analyse_DGV.Columns(cell.ColumnIndex).DefaultCellStyle.Alignment
                            Case DataGridViewContentAlignment.BottomRight,
                         DataGridViewContentAlignment.MiddleRight
                                fmt.Alignment = StringAlignment.Far
                                rc.Offset(-1, 0)
                            Case DataGridViewContentAlignment.BottomCenter,
                        DataGridViewContentAlignment.MiddleCenter
                                fmt.Alignment = StringAlignment.Center
                            Case Else
                                fmt.Alignment = StringAlignment.Near
                                rc.Offset(2, 0)
                        End Select

                        e.Graphics.DrawString(Analyse_DGV.Columns(cell.ColumnIndex).HeaderText, drawFont, Brushes.Black, rc, fmt)
                        x += rc.Width
                        h = Math.Max(h, rc.Height)
                    End If
                Next
                y += h

            End If
            newpage = False

            ' now print the data for each row
            Dim thisNDX As Int32
            For thisNDX = mRow To Analyse_DGV.RowCount - 1
                ' no need to try to print the new row
                If Analyse_DGV.Rows(thisNDX).IsNewRow Then Exit For

                row = Analyse_DGV.Rows(thisNDX)
                x = e.MarginBounds.Left
                h = 0

                ' reset X for data
                x = e.MarginBounds.Left

                ' print the data
                For Each cell As DataGridViewCell In row.Cells
                    If cell.Visible Then
                        rc = New Rectangle(x, y, cell.Size.Width, cell.Size.Height)

                        e.Graphics.DrawRectangle(Pens.Black, rc)

                        Select Case Analyse_DGV.Columns(cell.ColumnIndex).DefaultCellStyle.Alignment
                            Case DataGridViewContentAlignment.BottomRight,
                         DataGridViewContentAlignment.MiddleRight
                                fmt.Alignment = StringAlignment.Far
                                rc.Offset(-1, 0)
                            Case DataGridViewContentAlignment.BottomCenter,
                        DataGridViewContentAlignment.MiddleCenter
                                fmt.Alignment = StringAlignment.Center
                            Case Else
                                fmt.Alignment = StringAlignment.Near
                                rc.Offset(2, 0)
                        End Select

                        e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                      nrFont, Brushes.Black, rc, fmt)

                        x += rc.Width
                        h = Math.Max(h, rc.Height)
                    End If

                Next
                y += h
                ' next row to print

                mRow = thisNDX + 1

                If y + h > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    ' mRow -= 1   causes last row to rePrint on next page
                    newpage = True
                    Return
                End If
            Next
        Catch ex As Exception
            MsgBox("Error in printer settings." & vbNewLine & "Please check that automated printer selection is not selected in windows.")
            LogFejl(ex.ToString)
        End Try

    End Sub

    Private Sub PrintDocument1_BeginPrint(sender As Object,
          e As PrintEventArgs) Handles PrintDocument1.BeginPrint
        mRow = 0
        newpage = True
        PrintPreviewDialog1.PrintPreviewControl.StartPage = 0
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1.0
    End Sub

    Private Sub PrintReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintReportToolStripMenuItem.Click

        Dim ps As PaperSize
        Dim xCustomSize As New PaperSize("A4", 827, 1169) ''''''''''''''''''''''''''

        ps = xCustomSize

        If Me.DataGridView1.Rows.Count > 0 Then
            PrintDocument1.DefaultPageSettings.Landscape = True
            PrintDocument1.DefaultPageSettings.PaperSize = ps
            PrintDocument1.DefaultPageSettings.Margins.Top = 100
            PrintDocument1.DefaultPageSettings.Margins.Bottom = 100
            PrintDocument1.DefaultPageSettings.Margins.Left = 25
            PrintDocument1.DefaultPageSettings.Margins.Right = 25
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.Size = New System.Drawing.Size(1169, 827)
            PrintPreviewDialog1.ShowDialog()
        Else
            MsgBox("No data selected.")
        End If
    End Sub

    Private Sub LavFrac_Nr_ValueChanged(sender As Object, e As EventArgs)
        If Me.LavFrac_Nr.Value >= Me.HojFrac_Nr.Value Then
            Me.LavFrac_Nr.Value = 10
            Me.HojFrac_Nr.Value = 90
        End If
    End Sub

    Private Sub HojFrac_Nr_ValueChanged(sender As Object, e As EventArgs)
        If Me.LavFrac_Nr.Value >= Me.HojFrac_Nr.Value Then
            Me.LavFrac_Nr.Value = 10
            Me.HojFrac_Nr.Value = 90
        End If
    End Sub

    Private Sub SaveChartsAsImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChartsAsImageToolStripMenuItem.Click

        Dim savefile As SaveFileDialog = New SaveFileDialog
        Dim savechart As New System.Windows.Forms.DataVisualization.Charting.Chart
        Dim mystream As New System.IO.MemoryStream
        Dim title As String
        Dim unitofconc As String

        PrintChart.ShowDialog()

        title = InputBox("Please enter title of the chart")
        If title = "" Then Exit Sub

        If CBXForm = "Percentile chart" Then
            Me.RefChart.Serializer.Save(mystream)
            savechart.Serializer.Load(mystream)
            unitofconc = InputBox("Please enter concentration unit of the analysis")
            savechart.ChartAreas(0).AxisX.Title = "Concentration / " & unitofconc
        ElseIf CBXForm = "Normal distribution chart" Then
            Me.Normal.Serializer.Save(mystream)
            savechart.Serializer.Load(mystream)
        ElseIf CBXForm = "Data plot chart" Then
            Me.DataPlot.Serializer.Save(mystream)
            savechart.Serializer.Load(mystream)
            unitofconc = InputBox("Please enter concentration unit of the analysis")
            savechart.ChartAreas(0).AxisY.Title = "Concentration / " & unitofconc
            savechart.ChartAreas(0).AxisX.Title = "Date"
        End If

        savechart.Titles.Item(0).Text = title
        savechart.Titles.Item(1).Visible = False

        chartstyle(savechart)

        savefile.Filter = "PNG Image (*.png*)|*.png|JPEG Image (*.jpg*)|*.jpg|Bitmap Image (*.bmp*)|*.bmp|TIFF Image (*.tiff*)|*.tiff|emf Image (*.emf*)|*.emf|wmf Image (*.wmf*)|*.wmf"
        If savefile.ShowDialog = DialogResult.OK Then
            Dim path As String = savefile.FileName
            Dim fi As New IO.FileInfo(path)
            Dim extn As String = fi.Extension
            Select Case extn
                Case ".png"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Png)
                Case ".jpg"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                Case ".bmp"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                Case ".tiff"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                Case ".emf"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Emf)
                Case ".wmf"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Wmf)
            End Select
        End If

        savechart.Titles.Item(1).Visible = True
    End Sub

    Private Sub chartstyle(ByRef savechart As System.Windows.Forms.DataVisualization.Charting.Chart)

        savechart.Size = New Size(2400, 2400)
        savechart.BackColor = Color.White
        savechart.ChartAreas(0).BackColor = Color.White

        savechart.Titles.Item(0).Font = New Font("Arial", 72, FontStyle.Bold)
        savechart.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisY.LabelStyle.Font = New Font("Arial", 48, FontStyle.Bold)

        savechart.ChartAreas(0).AxisX.LineWidth = 5
        savechart.ChartAreas(0).AxisY.LineWidth = 5
        savechart.ChartAreas(0).AxisX.LineColor = Color.Black
        savechart.ChartAreas(0).AxisY.LineColor = Color.Black
        savechart.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.Black
        savechart.ChartAreas(0).AxisX.MajorGrid.LineWidth = 3
        savechart.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.Black
        savechart.ChartAreas(0).AxisY.MajorGrid.LineWidth = 3

        Select Case CBXForm
            Case "Percentile chart", "Normal distrubution chart"
                savechart.Size = New Size(2400, 2400)
                savechart.Series("Lav").BackGradientStyle = DataVisualization.Charting.GradientStyle.None
                savechart.Series("Lav").Color = Color.Red
                If CBXForm = "Percentile chart" Then
                    savechart.Series("Ref").BackGradientStyle = DataVisualization.Charting.GradientStyle.None
                    savechart.Series("Ref").Color = Color.Green
                Else
                    savechart.Series("Normal").BackGradientStyle = DataVisualization.Charting.GradientStyle.None
                    savechart.Series("Normal").Color = Color.Green
                End If
                savechart.Series("Hoj").BackGradientStyle = DataVisualization.Charting.GradientStyle.None
                savechart.Series("Hoj").Color = Color.Red
            Case "Data plot chart"
                savechart.Size = New Size(4800, 2400)

                savechart.Series("OutsideRange").MarkerSize = 24
                savechart.Series("Outliers").MarkerSize = 24
                savechart.Series("WithinRange").MarkerSize = 24

                savechart.Series("LowP").MarkerSize = 36
                savechart.Series("HighP").MarkerSize = 36
                savechart.Series("Median").MarkerSize = 36
                savechart.Series("LowSD").MarkerSize = 36
                savechart.Series("HighSD").MarkerSize = 36
                savechart.Series("Average").MarkerSize = 36

                savechart.Legends("Legend1").Font = New Font("Arial", 36, FontStyle.Bold)

                savechart.Series("LowP").BorderWidth = 12
                savechart.Series("HighP").BorderWidth = 12
                savechart.Series("Median").BorderWidth = 12
                savechart.Series("LowSD").BorderWidth = 12
                savechart.Series("HighSD").BorderWidth = 12
                savechart.Series("Average").BorderWidth = 12

                savechart.Series("OutsideRange").Color = Color.Red
                savechart.Series("Outliers").Color = Color.Black
                savechart.Series("WithinRange").Color = Color.Green
                savechart.Series("Median").Color = Color.Blue
                savechart.Series("Average").Color = Color.Blue

        End Select
    End Sub

    Private Sub AboutRefITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutRefITToolStripMenuItem.Click
        About_Box.Show()
    End Sub

    Private Sub PeriodSet_BTN_Click(sender As Object, e As EventArgs) Handles PeriodSet_BTN.Click

        If MinMaxDate.Rows.Count > 0 Then
            AnalyzeDates.Show()
        End If
    End Sub

    Private Sub Yaxis_BAR_Scroll(sender As Object, e As ScrollEventArgs) Handles Yaxis_BAR.Scroll
        Try
            If Me.Yaxis_BAR.Value <= 0 Then
                Me.Yaxis_BAR.Value = 1
            ElseIf Me.Yaxis_BAR.Value > Me.Yaxis_BAR.Maximum Then
                Me.Yaxis_BAR.Value = Me.Yaxis_BAR.Maximum
            End If
            Me.DataPlot.ChartAreas(0).AxisY.Maximum = CDbl(Me.Yaxis_BAR.Value)
            Me.DataPlot.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            MsgBox(Me.Yaxis_BAR.Value & " - " & Me.Yaxis_BAR.Maximum & " - " & Me.DataPlot.ChartAreas(0).AxisY.Maximum)
        End Try

    End Sub

    Private Sub View_CBX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles View_CBX.SelectedIndexChanged
        Select Case Me.View_CBX.SelectedItem
            Case "Percentile"
                Me.DataPlot.Series("LowSD").Enabled = False
                Me.DataPlot.Series("HighSD").Enabled = False
                Me.DataPlot.Series("Average").Enabled = False
                Me.DataPlot.Series("LowSD").IsVisibleInLegend = False
                Me.DataPlot.Series("HighSD").IsVisibleInLegend = False
                Me.DataPlot.Series("Average").IsVisibleInLegend = False

                Me.DataPlot.Series("LowP").Enabled = True
                Me.DataPlot.Series("HighP").Enabled = True
                Me.DataPlot.Series("Median").Enabled = True
                Me.DataPlot.Series("LowP").IsVisibleInLegend = True
                Me.DataPlot.Series("HighP").IsVisibleInLegend = True
                Me.DataPlot.Series("Median").IsVisibleInLegend = True
            Case "Standard Deviation"
                Me.DataPlot.Series("LowSD").Enabled = True
                Me.DataPlot.Series("HighSD").Enabled = True
                Me.DataPlot.Series("Average").Enabled = True
                Me.DataPlot.Series("LowSD").IsVisibleInLegend = True
                Me.DataPlot.Series("HighSD").IsVisibleInLegend = True
                Me.DataPlot.Series("Average").IsVisibleInLegend = True

                Me.DataPlot.Series("LowP").Enabled = False
                Me.DataPlot.Series("HighP").Enabled = False
                Me.DataPlot.Series("Median").Enabled = False
                Me.DataPlot.Series("LowP").IsVisibleInLegend = False
                Me.DataPlot.Series("HighP").IsVisibleInLegend = False
                Me.DataPlot.Series("Median").IsVisibleInLegend = False
        End Select

        Me.DataPlot.ChartAreas("ChartArea1").RecalculateAxesScale()
    End Sub

    Private Sub Comedic_BTN_Click(sender As Object, e As EventArgs) Handles Comedic_BTN.Click
        MsgBox("Feature under development")
    End Sub

    Private Sub Excluded_CHK_CheckedChanged(sender As Object, e As EventArgs) Handles Excluded_CHK.CheckedChanged
        If Me.Excluded_CHK.Checked = True Then
            Me.DataPlot.Series("OutsideRange").Enabled = True
        Else
            Me.DataPlot.Series("OutsideRange").Enabled = False
        End If
    End Sub

    Private Sub Outliers_CHK_CheckedChanged(sender As Object, e As EventArgs) Handles Outliers_CHK.CheckedChanged
        If Me.Outliers_CHK.Checked = True Then
            Me.DataPlot.Series("Outliers").Enabled = True
        Else
            Me.DataPlot.Series("Outliers").Enabled = False
        End If
    End Sub

    Private Sub PatientMinmaxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PatientMinmaxToolStripMenuItem.Click

        Try
            If Not IsNothing(DataGridView1.DataSource) Then
                MinMaxDT = TryCast(DataGridView1.DataSource, DataTable).Copy()
                patients_min_max.Show()
            End If

        Catch
        End Try

    End Sub
End Class
