obtion strict

Public Class ImpEx

    Public Shared Function yuuuy(Filepath As String) As datatable

        Dim workBook As New XLWorkbook(Filepath)
        'Read the first Sheet from Excel file.
        Dim workSheet As IXLWorksheet = workBook.Worksheets(0)

        Progress_TXT.Visible = True
        Progress_TXT.Text = "Loading file"
        Progress_PB.Maximum = workSheet.Rows.Count
        Progress_PB.Visible = True
        Application.DoEvents()
        'Loop through the Worksheet rows.
        Dim firstRow As Boolean = True
        For Each row As IXLRow In workSheet.Rows()
            'Use the first row to add columns to DataTable.
            If firstRow Then
                For Each cell As IXLCell In row.Cells()
                    dt.Columns.Add(cell.Value.ToString())
                    ExcelHeaders.Add(cell.Value)
                Next
                firstRow = False
            Else
                'Add rows to DataTable.
                dt.Rows.Add()
                Dim i As Integer = 0
                For Each cell As IXLCell In row.Cells(1, OriginalDT.Columns.Count)
                    dt.Rows(OriginalDT.Rows.Count - 1)(i) = cell.Value.ToString()
                    i += 1
                Next

            End If
            Progress_PB.PerformStep()
            Application.DoEvents()
        Next

        Return Import

    End Function
End Class
