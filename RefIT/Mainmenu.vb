Option Explicit On

'    RefIT - Reference Ranges for Therapeutic Drug Monitoring
'    Copyright(C) 2022  Jens Borggaard Larsen - jen7lar@gmail.com
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

    Public Shared Check As Boolean
    Public CBXForm As String
    Public AnalyseNr As Integer = 0
    Public Shared Raw_DS As New DataSet
    Public Shared OriginalDT As New DataTable
    Public Shared CleanDT As New DataTable
    Public Shared CurrentTable As String
    Public Shared UpdateChart As Boolean
    Public Shared UpdateDGV As Boolean
    Public Shared Analyse_DS As New DataSet

    Private Sub SetAnalyseTable()

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
    End Sub

    Private Sub ImportToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem1.Click
        DataGridView1.DataSource = Nothing
        Analyse_DGV.DataSource = Nothing
        Try
            Me.InfoChart.Series("Status").Points.Clear()
            Me.Udenmaal_LBL.Text = "0"
            Me.Udentid_LBL.Text = "0"
            Me.Ikkeafs_LBL.Text = "0"
            Me.TotalP_LBL.Text = "0"

            Raw_DS.Tables.Clear()
            Analyse_DS.Tables.Clear()
            If Not IsNothing(OriginalDT) Then
                OriginalDT.Clear()
                OriginalDT.Columns.Clear()
                Raw_DS.Tables.Add(OriginalDT)
            End If


            Me.Kvnt_CBX.Items.Clear()
            Progress_TXT.Visible = True
            Progress_TXT.Text = "Please be patient......."
            reset()
        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error while importing file!" & vbNewLine & "- Please check that the file is not open in Excel")
            Progress_TXT.Text = "Error while importing file."
        End Try
        'OpenFileDialog1.InitialDirectory = "c: \"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = OpenFileDialog1.FileName
            OriginalDT = Import(filePath)
            If IsNothing(OriginalDT) Then Exit Sub
            Try

                Progress_PB.Style = ProgressBarStyle.Continuous


                If Check = False Then
                    Progress_TXT.Text = "Loading aborted"
                    Progress_PB.Visible = False
                    Exit Sub 'Exit if error during import
                End If

                CleanDT = Rens(OriginalDT)
                CleanDT = Datokontrol(CleanDT)

                If CheckCPR(CleanDT) = True Then
                    CleanDT = CalculateID(CleanDT)
                Else
                    CleanDT = NonCPR(CleanDT)
                End If

                Dim colname As String
                For i As Integer = CleanDT.Columns.Count - 1 To 0 Step -1
                    colname = CleanDT.Columns(i).ColumnName
                    If colname.Substring(0, 1) <> "_" Then
                        CleanDT.Columns.Remove(colname)
                    End If
                Next

                KolonneRaekke(CleanDT)
                FyldKvnTilCBX(CleanDT)


                Me.Kvnt_CBX.SelectedIndex = 0

                Progress_PB.Visible = False
                Progress_TXT.Text = "Finished loading"

            Catch ex As Exception
                LogFejl(ex.ToString)
                MsgBox("Error while importing file!" & vbNewLine & "- Please check that the file is not open in Excel")
            End Try

        End If
    End Sub

    Private Sub FyldKvnTilCBX(dt As DataTable)
        Dim KvntNames = From v In dt.AsEnumerable Select v.Field(Of String)("_Analysis_") Distinct
        If Not IsNothing(KvntNames) Then
            For Each t As String In KvntNames
                Me.Kvnt_CBX.Items.Add(t)
            Next
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
                NyTabel = JansAlgo(NyTabel)
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

            Return (NyTabel)

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error in Kvantiteter!")
        End Try
    End Function

    Private Function JansAlgo(dt As DataTable)

        Try

            dt.DefaultView.Sort = "_ID_ ASC, _Analysis Date_ ASC"
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
                                Dato1 = r.Item("_Analysis Date_")
                                rCount = rCount + 1
                            Else
                                Dato2 = r.Item("_Analysis Date_")
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
                                Dato1 = r.Item("_Analysis Date_")
                                PreRow = r
                                rCount = rCount + 1
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

    Private Sub BeregnEnkelt()
        Dim dt As DataTable

        Progress_PB.Visible = True
        Progress_TXT.Visible = True

        My.Settings.TidsInterval = Me.Tid_Nr.Value
        dt = Kvantiteter(Me.Kvnt_CBX.SelectedItem)

        If dt.Rows.Count - 1 > 0 Then
            DataGridView1.DataSource = dt
            CurrentTable = dt.TableName
            Analyze()
        Else
            MsgBox("No samples selected based on this setting.")
        End If

        Progress_PB.Visible = False
        Progress_TXT.Visible = False

        DataGridView1.Columns("_Include_").ReadOnly = False

    End Sub
    Private Sub BatchCalc()
        UpdateChart = False
        UpdateDGV = False

        Dim dt As DataTable
        Dim Str As String

        Progress_PB.Visible = True
        Progress_TXT.Visible = True

        My.Settings.TidsInterval = Me.Tid_Nr.Value

        For i As Integer = 0 To Me.Kvnt_CBX.Items.Count - 1
            Str = Me.Kvnt_CBX.Items.Item(i) 'SelectedIndex = i
            dt = Kvantiteter(Str)
            CurrentTable = dt.TableName
            analyser(dt)
            If i = Me.Kvnt_CBX.Items.Count - 1 Then
                DataGridView1.DataSource = dt
                drawchart(dt)
                Me.Kvnt_CBX.SelectedIndex = i
                Analyse_DGV.ClearSelection()
            End If
        Next

        UpdateChart = True
        UpdateDGV = True

        Progress_PB.Visible = False
        Progress_TXT.Visible = False
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
        Dim dt As DataTable = TryCast(DataGridView1.DataSource, DataTable).Copy()
        CurrentTable = dt.TableName

    End Sub

    Private Sub Reset_BTN_Click(sender As Object, e As EventArgs) Handles Reset_BTN.Click
        Try
            Dim tekst As String = "Erase all analysed datasets - Proceed? "
            If MsgBox(tekst, vbYesNo) = vbNo Then Exit Sub
            Analyse_DS.Tables.Clear()
            Analyse_DGV.DataSource = Nothing
            Analyse_DGV.Refresh()
            reset()
        Catch ex As Exception
            LogFejl(ex.ToString)
        End Try

    End Sub
    Private Sub reset()

        Me.Kon_CBX.SelectedIndex = 0
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

        'dr = Analyse_DGV.SelectedRows
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

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = SaveFileDialog1.FileName
            Export(filePath, ds)
        End If

        ds = Nothing
        dt = Nothing
        dta = Nothing

    End Sub

    Private Sub ExportAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAllToolStripMenuItem.Click
        'GemExcel()
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = SaveFileDialog1.FileName
            Export(filePath, Analyse_DS)
        End If

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
        End If
    End Sub

    Private Sub drawchart(dt As DataTable)
        Dim dview As New DataView(dt)
        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"
        dt = TryCast(dview.ToTable, DataTable).Copy()

        Dim NewRow As DataRow = Mainmenu.Analyse_DS.Tables("AnalyseDT").NewRow() ' empty row used as dummy
        Dim Output() As Integer = Analyse.Statistics(dt, NewRow) 'Calculates fractile limits, avg and SD
        NewRow = Nothing ' row is set to nothing
        Analyse.Charts(dt, Output) ' draws the charts using the datatable 
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

    Private Sub LavFrac_Nr_ValueChanged(sender As Object, e As EventArgs) Handles LavFrac_Nr.ValueChanged
        If Me.LavFrac_Nr.Value >= Me.HojFrac_Nr.Value Then
            Me.LavFrac_Nr.Value = 10
            Me.HojFrac_Nr.Value = 90
        End If
    End Sub

    Private Sub HojFrac_Nr_ValueChanged(sender As Object, e As EventArgs) Handles HojFrac_Nr.ValueChanged
        If Me.LavFrac_Nr.Value >= Me.HojFrac_Nr.Value Then
            Me.LavFrac_Nr.Value = 10
            Me.HojFrac_Nr.Value = 90
        End If
    End Sub

    Private Sub Mainmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.PerMeth_CBX.SelectedIndex = 2
        SetAnalyseTable()
    End Sub

End Class
