Option Explicit On

Imports ClosedXML.Excel

Module ImpExp

    Public AbortLoad As Boolean

    Public Function Import(Filepath As String)
        '***************************************************************************
        '**                       reads excel file                                **
        '***************************************************************************
        Try
            Dim Importdt As DataTable = New DataTable
            Dim ExcelHeaders As New ArrayList()
            ExcelHeaders.Add("None")

            Dim workBook As New XLWorkbook(Filepath)
            Dim workSheet As IXLWorksheet = workBook.Worksheets(0)

            'Read the first Sheet from Excel file.
            If workBook.Worksheets.Count > 1 Then 'if more than one worksheet in the workbook, select one to load
                Dim wsarray As New ArrayList()
                For i As Integer = 1 To workBook.Worksheets.Count
                    wsarray.Add(workBook.Worksheet(i).Name)
                Next
                workSheet = workBook.Worksheet(SelectWS(wsarray))
            Else 'Else Read the first Sheet from Excel file.
                workSheet = workBook.Worksheets(0)
            End If

            'set progress bar
            Mainmenu.Progress_TXT.Text = ""
            Mainmenu.Progress_TXT.Text = "Loading file....."
            Mainmenu.Progress_PB.Maximum = workSheet.Rows.Count
            Mainmenu.Progress_PB.Visible = True
            Application.DoEvents()

            'Loop through the Worksheet rows.
            Dim firstRow As Boolean = True
            For Each row As IXLRow In workSheet.Rows()
                'Use the first row to add columns to DataTable.
                If firstRow Then
                    For Each cell As IXLCell In row.Cells()
                        Importdt.Columns.Add(cell.Value.ToString())
                        ExcelHeaders.Add(cell.Value)
                    Next
                    firstRow = False
                Else
                    'Add rows to DataTable.
                    Importdt.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As IXLCell In row.Cells(1, Importdt.Columns.Count)
                        Importdt.Rows(Importdt.Rows.Count - 1)(i) = cell.Value.ToString()
                        i += 1
                    Next

                End If
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next


            If CheckHeaders(ExcelHeaders, Importdt) = False Then
                Mainmenu.Check = False 'terminate loading
                Mainmenu.Progress_TXT.Text = ""
                Mainmenu.Progress_PB.Visible = True
            Else
                Mainmenu.Check = True
            End If

            Return Importdt

        Catch ex As Exception
            MsgBox("Error while importing file!" & vbNewLine & "Please check data, and that file is not open in another program.")
            LogFejl(ex.ToString)
        End Try

    End Function
    Public Function SelectWS(ByRef WS As ArrayList)
        '***************************************************************************
        '**Selection of worksheet in excel file to load, if multiple are present  **
        '***************************************************************************
        VaelgKolonneNavn.KolonneNavn_CBX.DataSource = WS
        VaelgKolonneNavn.Kolonne_LBL.Text = "Please select worksheet"
        VaelgKolonneNavn.Label1.Text = "to import"
        VaelgKolonneNavn.ShowDialog()

        Return Mainmenu.CBXForm

    End Function
    Public Function CheckHeaders(ByRef Headers As ArrayList, dt As DataTable) As Boolean

        '****************************************************************************
        'Perform user selection of the columns from the excel spreadsheet that data
        'are imported from. The variable checkheaders is used to exit the function.
        '****************************************************************************

        VaelgKolonneNavn.KolonneNavn_CBX.DataSource = Headers
        CheckHeaders = True
        AbortLoad = False

        VaelgKolonneNavn.Kolonne_LBL.Text = "Please select column for"

        If CheckHeaders = True Then
            VaelgKolonneNavn.Label1.Text = "Patient ID"
            VaelgKolonneNavn.ShowDialog()
            My.Settings.CPR = Mainmenu.CBXForm
            If My.Settings.CPR = "None" Then
                MsgBox("ID column has to be present.")
                CheckHeaders = False
            ElseIf AbortLoad = True Then
                CheckHeaders = False
            End If
        End If

        If CheckHeaders = True Then
            VaelgKolonneNavn.Label1.Text = "Analysis name or ID"
            VaelgKolonneNavn.ShowDialog()
            My.Settings.Kvantitet = Mainmenu.CBXForm
            If My.Settings.Kvantitet = "None" Then
                MsgBox("Analysis ID has to be present.")
                CheckHeaders = False
            ElseIf AbortLoad = True Then
                CheckHeaders = False
            End If
        End If

        If CheckHeaders = True Then
            VaelgKolonneNavn.Label1.Text = "Analysis Date"
            VaelgKolonneNavn.ShowDialog()
            My.Settings.ProveDato = Mainmenu.CBXForm
            If My.Settings.ProveDato = "None" Then
                MsgBox("Analysis Date has to be present.")
                CheckHeaders = False
            ElseIf AbortLoad = True Then
                CheckHeaders = False
            End If
        End If

        If CheckHeaders = True Then
            VaelgKolonneNavn.Label1.Text = "Result"
            VaelgKolonneNavn.ShowDialog()
            My.Settings.Resultat = Mainmenu.CBXForm
            If My.Settings.Resultat = "None" Then
                MsgBox("Result has to be present.")
                CheckHeaders = False
            ElseIf AbortLoad = True Then
                CheckHeaders = False
            End If
        End If

        If CheckHeaders = True Then
            VaelgKolonneNavn.Label1.Text = "Gender"
            VaelgKolonneNavn.ShowDialog()
            My.Settings.Kon = Mainmenu.CBXForm
            If AbortLoad = True Then
                CheckHeaders = False
            End If
        End If

        If CheckHeaders = True Then
                VaelgKolonneNavn.Label1.Text = "Age"
                VaelgKolonneNavn.ShowDialog()
                My.Settings.Alder = Mainmenu.CBXForm
                If AbortLoad = True Then
                    CheckHeaders = False
                End If
            End If

            Return CheckHeaders

    End Function

    Public Sub Export(ByRef ds As DataSet)
        '*********************************************************************************
        'Export the result sheets and raw datasheets to excel.
        '*********************************************************************************
        Try
            Dim ws As IXLWorksheet
            Dim savefile As SaveFileDialog = New SaveFileDialog

            Using wb As New XLWorkbook()
                For Each dtable As DataTable In ds.Tables
                    ws = wb.Worksheets.Add(dtable, "_" & wb.Worksheets.Count & "_" & Truncate(dtable.TableName))
                    ws.Columns().AdjustToContents()
                Next

                savefile.Filter = "xlsx files (*.xlsx*)|*.xlsx|xlsm files (*.xlsm*)|*.xlsm"

                If savefile.ShowDialog() = DialogResult.OK Then
                    Dim path As String = savefile.FileName
                    Dim fi As New IO.FileInfo(path)
                    Dim extn As String = fi.Extension
                    Select Case extn
                        Case ".xlsx"
                            wb.SaveAs(path + ".xlsx")
                        Case ".xlsm"
                            wb.SaveAs(path + ".xlsm")
                    End Select
                End If
            End Using

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error exporting data")
        End Try
    End Sub
    Public Function Truncate(wbname As String) 'Verify Worksheet names
        '*****************************************************************************
        '**Corrects autogenerated tablename, to prevent error if loaded into excell **
        '*****************************************************************************
        Dim sIllegal As String = "\,/,:,*,?," & Chr(34) & ",<,>,|"
        Dim arIllegal() As String = Split(sIllegal, ",")

        For i = 0 To arIllegal.Length - 1
            wbname = Replace(wbname, arIllegal(i), "")
        Next

        Dim MaxLength As Integer = 25

        If MaxLength > wbname.Length Then
            Return wbname
        Else
            Return wbname.Substring(0, MaxLength)
        End If
    End Function

End Module
