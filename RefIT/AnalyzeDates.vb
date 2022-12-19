Option Explicit On

Public Class AnalyzeDates

    Dim CurrentAnalysis As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Exit_BTN.Click
        Me.Close()
    End Sub

    Private Sub AnalyzeDates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToFromDate_DGW.DataSource = Mainmenu.MinMaxDate
        ToFromDate_DGW.Columns(1).DefaultCellStyle.Format = "d"
        ToFromDate_DGW.Columns(2).DefaultCellStyle.Format = "d"

        CurrentAnalysis = ToFromDate_DGW.Rows(0).Cells("Analysis").Value.ToString
        Dim MinDate As Date = ToFromDate_DGW.Rows(0).Cells("From Date").Value
        Dim MaxDate As Date = ToFromDate_DGW.Rows(0).Cells("To Date").Value
        Me.StartDate.Value = MinDate
        Me.EndDate.Value = MaxDate
        plotdates(MinDate, MaxDate)
    End Sub

    Private Sub ToFromDate_DGW_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ToFromDate_DGW.CellMouseClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim selectedRow = ToFromDate_DGW.Rows(e.RowIndex)
            CurrentAnalysis = selectedRow.Cells("Analysis").Value.ToString
            Dim MinDate As Date = Convert.ToDateTime(selectedRow.Cells("From Date").Value.ToString)
            Dim MaxDate As Date = Convert.ToDateTime(selectedRow.Cells("To Date").Value.ToString)
            Me.StartDate.Value = MinDate
            Me.EndDate.Value = MaxDate
            plotdates(MinDate, MaxDate)
        End If
    End Sub

    Private Sub plotdates(MinDate As Date, MaxDate As Date)

        Dim dt As New DataTable
        Dim dview As New DataView(Mainmenu.CleanDT)
        Dim x As Date
        Dim y As Double
        dview.RowFilter = "_Analysis_ = '" & CurrentAnalysis & "'"
        dview.Sort = "_Analysis_Date_ ASC"

        DateChart.Series("Low").Points.Clear()
        DateChart.Series("DateRange").Points.Clear()
        DateChart.Series("High").Points.Clear()

        dt = dview.ToTable

        DateChart.Titles.Clear()
        DateChart.Titles.Add(CurrentAnalysis)

        For i As Integer = 0 To dt.Rows.Count - 1
            x = dt.Rows(i)("_Analysis_Date_")
            y = dt.Rows(i)("_Result_")

            If x < MinDate Then
                DateChart.Series("Low").Points.AddXY(x, y)
            ElseIf x >= MinDate And x <= MaxDate Then
                DateChart.Series("DateRange").Points.AddXY(x, y)
            ElseIf x > MaxDate Then
                DateChart.Series("High").Points.AddXY(x, y)
            End If

        Next

    End Sub

    Private Sub Select_BTN_Click(sender As Object, e As EventArgs) Handles Select_BTN.Click
        ToFromDate_DGW.SelectedRows(0).Cells("From Date").Value = StartDate.Value
        ToFromDate_DGW.SelectedRows(0).Cells("To Date").Value = EndDate.Value

        plotdates(StartDate.Value, EndDate.Value)

        Dim drow() As DataRow = Mainmenu.MinMaxDate.Select("Analysis = '" & CurrentAnalysis & "'")

    End Sub

    Private Sub Reset_BTN_Click(sender As Object, e As EventArgs) Handles Reset_BTN.Click

        Dim dview As DataView = New DataView(Mainmenu.CleanDT)
        dview.RowFilter = "_Analysis_ = '" & CurrentAnalysis & "'"
        dview.Sort = "_Analysis_Date_ ASC"

        Me.StartDate.Value = dview.ToTable().Compute("MIN(_Analysis_Date_)", "")
        Me.EndDate.Value = dview.ToTable().Compute("MAX(_Analysis_Date_)", "")

        ToFromDate_DGW.SelectedRows(0).Cells("From Date").Value = StartDate.Value
        ToFromDate_DGW.SelectedRows(0).Cells("To Date").Value = EndDate.Value

        plotdates(Me.StartDate.Value, Me.EndDate.Value)

    End Sub
End Class