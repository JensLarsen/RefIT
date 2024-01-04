Module Analyse

    Public Sub analyser(dt As DataTable)
        '***************************************************************************
        '**                    Main analysis function that branches out           **
        '***************************************************************************
        Dim dtdraw As DataTable
        Dim Output() As Double
        Dim NewRow As DataRow = Mainmenu.Analyse_DS.Tables("AnalyseDT").NewRow()
        Dim dview As New DataView(dt)

        Mainmenu.Check = False
        dview.RowFilter = SorterDataTilGraf(dt)
        dview.Sort = "_Result_ ASC"
        dt = TryCast(dview.ToTable, DataTable).Copy()

        '******************************************Tukey fences*************************************
        If Mainmenu.Tukey_CHK.Checked = True Then
            Dim totaloutliers As Integer
            Do
                dt = Tukey(dt, NewRow)
                totaloutliers = totaloutliers + CInt(NewRow.Item("#Outliers"))
            Loop While NewRow.Item("#Outliers") <> "0"
            NewRow.Item("#Outliers") = totaloutliers
        End If

        '*****************************Statistics*****************************
        dtdraw = OnlyInclude(dt) ' returns tablewith only included samples and sorted by result 

        If dtdraw.Rows.Count < 20 Then
            Mainmenu.Check = False
            MsgBox("Population size of " & dt.TableName & " < 20 - to low for calculation." & vbNewLine & "Try changing population criterias.")
            Exit Sub
        End If

        Output = Statistics(dtdraw, NewRow) ' calculates statistics and writes the data to row

        If Mainmenu.CurrentTable = "" Then
            NewRow.Item("Analysis") = Mainmenu.Kvnt_CBX.SelectedItem
        Else
            NewRow.Item("Analysis") = Mainmenu.CurrentTable.Split("_"c)(0)
        End If

        If dtdraw.Rows.Count < 20 Then
            MsgBox("Population size " & NewRow.Item("Analysis") & " < 20 - to low for calculation." & vbNewLine & "Try changing population criterias.")
            Exit Sub
        End If

        NewRow.Item("#") = Mainmenu.Analyse_DS.Tables("AnalyseDT").Rows.Count + 1
        NewRow.Item("# Samples") = dt.Rows.Count
        NewRow.Item("Incl. Samples") = dtdraw.Rows.Count
        NewRow.Item("# Patients") = PatientNr(dtdraw)
        NewRow.Item("Gender") = Mainmenu.Kon_CBX.SelectedItem
        NewRow.Item("Age") = Mainmenu.MinNr.Value & " - " & Mainmenu.MaxNr.Value

        Mainmenu.Analyse_DS.Tables("AnalyseDT").Rows.Add(NewRow)

        dt.DefaultView.Sort = "_ID_ ASC, _Analysis_Date_ ASC"
        dt = dt.DefaultView.ToTable.Copy()

        dt.TableName = NewRow.Item("Analysis") & "_" & Mainmenu.Analyse_DS.Tables("AnalyseDT").Rows.Count - 1
        Mainmenu.Analyse_DS.Tables.Add(dt)

        If Mainmenu.UpdateDGV = True Then Mainmenu.DataGridView1.DataSource = dt.Copy()

        If Mainmenu.UpdateChart = True Then
            Charts(dtdraw, Output)
            drawdataplot(NewRow, dt)
        End If

        NewRow = Nothing

    End Sub

    Private Function OnlyInclude(drawDT As DataTable)
        '********************************************************************************
        '**Returns a table with only included samples, and sorted based on result value**
        '********************************************************************************
        Dim dview As New DataView(drawDT)

        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"
        Return (TryCast(dview.ToTable, DataTable))

    End Function
    Private Function PatientNr(ByRef dt As DataTable)
        '***************************************************************************
        '**  calculates the number of individual patients in the sample set       **
        '***************************************************************************
        Dim PNo As Integer
        Dim Patients = From row In dt.AsEnumerable()
                       Select row.Field(Of String)("_ID_") Distinct

        PNo = Patients.Count

        Return PNo

    End Function

    Private Function SorterDataTilGraf(ByRef dt As DataTable)
        '***************************************************************************
        '**          Generates a string for sorting using linq                    **
        '***************************************************************************

        Dim MinDate As Date
        Dim MaxDate As Date
        Dim dateview As New DataView(Mainmenu.MinMaxDate)

        dateview.RowFilter = "Analysis = '" & dt.TableName & "'"
        MinDate = dateview.Item(0)("From Date")
        MaxDate = dateview.Item(0)("To Date")

        'Bug-fix: One day is subtracted from Mindate and one added to maxdate due to date containing time
        'this ensures all samples are included in the calculation. 
        MinDate = MinDate.AddDays(-1)
        MaxDate = MaxDate.AddDays(1)

        Dim sorter As String = "_Analysis_Date_ >= #" & MinDate.ToString("yyyy'/'MM'/'dd") & "# AND _Analysis_Date_ < #" & MaxDate.ToString("yyyy'/'MM'/'dd") & "#"

        If dt.Columns.Contains("_Gender_") Then
            Select Case Mainmenu.Kon_CBX.SelectedItem
                Case "Females"
                    sorter = sorter & " AND _Gender_ = 'F' "
                Case "Males"
                    sorter = sorter & " AND _Gender_ = 'M' "
                Case Else
                    sorter = sorter & " AND _Gender_ <> 'Both' " ' Both does not excist so all samples are selected
            End Select
        End If

        If dt.Columns.Contains("_Age_") Then
            If Mainmenu.MinNr.Value <> 0 Or Mainmenu.MaxNr.Value <> 100 Then
                If Mainmenu.MinNr.Value >= Mainmenu.MaxNr.Value Then
                    MsgBox("Error in age setting")
                    Mainmenu.MinNr.Value = 0
                    Mainmenu.MaxNr.Value = 100
                Else
                    sorter = sorter & "AND _Age_ >= " & Mainmenu.MinNr.Value & " AND _Age_ <= " & Mainmenu.MaxNr.Value
                End If
            Else
                sorter = sorter & "AND _Age_ >= 0"
            End If
        End If

        Return sorter
    End Function

    Public Function Statistics(dt As DataTable, ByRef NewRow As DataRow)

        Dim LF As Double = (Mainmenu.LavFrac_Nr.Value / 100) * dt.Rows.Count - 1 'lowest percentile
        Dim HF As Double = (Mainmenu.HojFrac_Nr.Value / 100) * dt.Rows.Count - 1 'highest percentile
        Dim md As Double = Math.Round(0.5 * dt.Rows.Count, 0) ' Median
        Dim LQ As Double = 0.25 * dt.Rows.Count - 1 'low quartile
        Dim HQ As Double = 0.75 * dt.Rows.Count - 1 'high quartile
        Dim Middel As Double 'average of sampleset
        Dim SD As Double     'standard deviation
        Dim MinDate As Date 'The first date in the sampleset
        Dim MaxDate As Date 'The last day in the sampleset
        Dim PerQ() As Double '1-2 Percentiles, 3-4 Quartiles and 5-6 min max,7 median
        Dim Output() As Double 'returning values to draw chart

        Try

            Mainmenu.Progress_TXT.Text = "Analyzing Data"
            Mainmenu.Progress_PB.Value = 0

            If dt.Rows.Count > 0 Then
                PerQ = Percentiles(dt)

                For r As Integer = 0 To dt.Rows.Count - 1
                    Middel = Middel + dt.Rows(r)("_Result_") 'Adding results for average calculation

                    If r = 0 Then
                        MinDate = dt.Rows(r)("_Analysis_Date_")
                        MaxDate = dt.Rows(r)("_Analysis_Date_")
                    End If
                    'Finds the start and end dates for the dataset
                    If MinDate > dt.Rows(r)("_Analysis_Date_") Then MinDate = dt.Rows(r)("_Analysis_Date_")
                    If MaxDate < dt.Rows(r)("_Analysis_Date_") Then MaxDate = dt.Rows(r)("_Analysis_Date_")

                    Mainmenu.Progress_PB.PerformStep()
                    Application.DoEvents()
                Next
            Else
                Mainmenu.Check = True
                Output = {0, 0, 0, 0, 0, 0}
                Return Output
            End If

            If dt.Rows.Count > 0 Then
                Middel = Math.Round(Middel / dt.Rows.Count, 2) 'calculate average
                SD = STDafv(dt, Middel) 'calculate standard deviation
            Else
                Middel = 0
                SD = 0
            End If

            Mainmenu.Average = Middel
            Mainmenu.STD = SD

            '*******************Copy data to datarow**********************************
            NewRow.Item("From Date") = MinDate.ToShortDateString
            NewRow.Item("To Date") = MaxDate.ToShortDateString
            NewRow.Item("Percentile") = PerQ(0) & " - " & PerQ(1)
            NewRow.Item("Median") = PerQ(4)
            NewRow.Item("Average") = Middel
            NewRow.Item("Min") = PerQ(2)
            NewRow.Item("Max") = PerQ(3)
            NewRow.Item("Percentile Analysis") = MethodName()
            NewRow.Item("2SD") = Math.Round(Middel - 2 * SD, 2) & " - " & Math.Round(Middel + 2 * SD, 2)

            '**********************Copy data to output to be used for drawing charts*****************************
            Output = {PerQ(0), PerQ(1), Middel, SD, PerQ(3), PerQ(4)}

        Catch ex As Exception
            MsgBox("No results for " & dt.TableName)
            Output = {0, 0, 0, 0, 0, 0}
        End Try

        Return Output

    End Function
    Private Function MethodName() As String
        '***************************************************************************
        '**                    Generates a describtion of the method              **
        '***************************************************************************

        MethodName = ""

        Select Case Mainmenu.Model_CBX.SelectedItem
            Case "TDM model"
                MethodName = "TDM - "
            Case "TDM model -last sample"
                MethodName = "TDM minus last - "
            Case "First result from patient"
                MethodName = "First sample - "
            Case "Last result from patient"
                MethodName = "Last sample - "
            Case "All data from patient"
                MethodName = "All samples - "
        End Select

        Select Case Mainmenu.PerMeth_CBX.SelectedItem
            Case "Interpolated C=0 - Excel after 2013"
                MethodName = MethodName & "Inter. C=0 - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
            Case "Interpolated C=1 - Excel to 2013"
                MethodName = MethodName & "Inter. C=1 - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
            Case "Nearest-Rank"
                MethodName = MethodName & "Nearest-Rank - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
        End Select

        Return MethodName
    End Function

    Private Function Percentiles(ByRef dt As DataTable)
        '**********************************************************
        '** Calculates percentiles depending on selected method  **
        '**          Methods taken from wikipedia                **
        '**********************************************************
        Dim Nn As Integer = dt.Rows.Count - 1
        Dim n(1) As Double
        Dim LF As Double = Mainmenu.LavFrac_Nr.Value / 100
        Dim HF As Double = Mainmenu.HojFrac_Nr.Value / 100
        Dim LowP As Double 'Lower percentile
        Dim HighP As Double 'High percentile
        Dim v1(1) As Double
        Dim v2(1) As Double
        Dim k(1) As Double
        Dim d(1) As Double

        Dim min As Double = Math.Round(dt.Rows(0).Item("_Result_"), 2)
        Dim max As Double = Math.Round(dt.Rows(Nn).Item("_Result_"), 2)
        Dim median As Double = Math.Round(dt.Rows(Nn * 0.5).Item("_Result_"), 2)

        Dim Output() As Double

        Try

            Select Case Mainmenu.PerMeth_CBX.SelectedItem
                Case "Interpolated C=0 - Excel after 2013" 'according to Wikipedia
                    n(0) = (LF) * (Nn + 1)
                    n(1) = (HF) * (Nn + 1)
                    k(0) = Fix(n(0))
                    k(1) = Fix(n(1))
                    d(0) = n(0) - k(0)
                    d(1) = n(1) - k(1)
                    LowP = dt.Rows(k(0) - 1).Item("_Result_") + d(0) * (dt.Rows(k(0)).Item("_Result_") - dt.Rows(k(0) - 1).Item("_Result_"))
                    HighP = dt.Rows(k(1) - 1).Item("_Result_") + d(1) * (dt.Rows(k(1)).Item("_Result_") - dt.Rows(k(1) - 1).Item("_Result_"))
                Case "Interpolated C=1 - Excel to 2013" 'according to Wikipedia
                    n(0) = (LF) * (Nn - 1) + 1
                    n(1) = (HF) * (Nn - 1) + 1
                    k(0) = Fix(n(0))
                    k(1) = Fix(n(1))
                    d(0) = n(0) - k(0)
                    d(1) = n(1) - k(1)
                    LowP = dt.Rows(k(0) - 1).Item("_Result_") + d(0) * (dt.Rows(k(0)).Item("_Result_") - dt.Rows(k(0) - 1).Item("_Result_"))
                    HighP = dt.Rows(k(1) - 1).Item("_Result_") + d(1) * (dt.Rows(k(1)).Item("_Result_") - dt.Rows(k(1) - 1).Item("_Result_"))
                Case "Nearest-Rank"
                    n(0) = Math.Round(LF * Nn, 0)
                    n(1) = Math.Round(HF * Nn, 0)

                    LowP = dt.Rows(n(0)).Item("_Result_")
                    HighP = dt.Rows(n(1)).Item("_Result_")

                Case Else

            End Select

            Mainmenu.LowLimit = LowP 'transfer values to public variable
            Mainmenu.HighLimit = HighP 'transfer value to public variable for use with min_max_patient
            Mainmenu.PMedian = median 'transfer value to public variable for use with min_max_patient

            Output = {Math.Round(LowP, 2), Math.Round(HighP, 2), min, max, median}

        Catch ex As Exception
            MsgBox("No results for " & dt.TableName)
            Output = {0, 0, 0, 0, 0, 0}
            LogFejl(ex.ToString)
        End Try

        Return Output

    End Function

    Public Function STDafv(ByVal Table As DataTable, ByRef middel As Double)
        '*******************************************************************
        '**       Calculates the standard deviation of the dataset        **
        '*******************************************************************

        Dim SD As Double '= 0.0
        Dim uppser As Double '= 0.0

        Try
            For Each dt As DataRow In Table.Rows
                If dt.Item("_Result_") Is DBNull.Value Then 'IMPORTANT! SOME RECORD HAS NULL VALUE SO MUST ADD A ZERO
                    uppser += 0
                Else
                    uppser += (dt.Item("_Result_") - middel) ^ 2
                End If

            Next

            SD = Math.Sqrt(uppser / (Table.Rows.Count - 1))

        Catch ex As Exception
            LogFejl(ex.ToString)
            SD = 1
        End Try

        Return (Math.Round(SD, 2))

    End Function

    Public Function TukeyFences(dt As DataTable)
        '*******************************************************************
        '**                 Calculates tukeys fences                      **
        '*******************************************************************
        Dim TukeyQ1 As Double
        Dim TukeyQ2 As Double
        Dim Q1Q2() As Double
        Dim dview As New DataView(dt)

        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"

        Try

            Dim dt2 As DataTable = TryCast(dview.ToTable, DataTable).Copy()

            TukeyQ1 = dt2.Rows(Math.Round(0.25 * dview.Count, 0)).Item("_Result_") 'Lower fence
            TukeyQ2 = dt2.Rows(Math.Round(0.75 * dview.Count, 0)).Item("_Result_") 'Upper fence

            Q1Q2 = {TukeyQ1 - 1.5 * (TukeyQ2 - TukeyQ1), TukeyQ2 + 1.5 * (TukeyQ2 - TukeyQ1)}

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error removing Outliers")
        End Try

        Return (Q1Q2)
    End Function
    Private Function Tukey(dt As DataTable, ByRef NewRow As DataRow)
        '*******************************************************************
        '**       Removes outliers based on tukeys fences                 **
        '*******************************************************************
        Dim TukeyQ1Q2() As Double = TukeyFences(dt)
        Dim Q1 As Double = TukeyQ1Q2(0)
        Dim Q2 As Double = TukeyQ1Q2(1)
        Dim OutliersNr As Integer

        Try
            For Each dr As DataRow In dt.Rows
                If dr.Item("_Include_") = True Then
                    If dr.Item("_Result_") < Q1 Then
                        dr.Item("_Include_") = False
                        dr.Item("_Comment_") = "Tukey < " & Q1
                        OutliersNr = OutliersNr + 1
                    ElseIf dr.Item("_Result_") > Q2 Then
                        dr.Item("_Include_") = False
                        dr.Item("_Comment_") = "Tukey > " & Q2
                        OutliersNr = OutliersNr + 1
                    End If
                End If
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next

            NewRow.Item("Tukey Fences") = Q1 & " - " & Q2
            NewRow.Item("#Outliers") = OutliersNr
        Catch ex As Exception
            NewRow.Item("Tukey Fences") = "N/D"
            NewRow.Item("#Outliers") = 0
        End Try

        Return dt

    End Function

    Public Sub Charts(dt As DataTable, Output As Double())
        '*******************************************************************
        '**       Draws percentile and normal distribution charts         **
        '*******************************************************************
        Dim middel As Double = Output(2)
        Dim SD As Double = Output(3)
        Dim lavfraktil As Double = Output(0)
        Dim hojfraktil As Double = Output(1)
        Dim max As Double = Output(4)
        Dim i As Integer = 0
        Dim dview As New DataView(dt)

        Try
            Mainmenu.RefChart.Series("Lav").Points.Clear()
            Mainmenu.RefChart.Series("Ref").Points.Clear()
            Mainmenu.RefChart.Series("Hoj").Points.Clear()

            Mainmenu.Normal.Series("Lav").Points.Clear()
            Mainmenu.Normal.Series("Normal").Points.Clear()
            Mainmenu.Normal.Series("Hoj").Points.Clear()

            '*********************************Draw percentile plot***********************

            Mainmenu.RefChart.ChartAreas("RefChart").AxisX.Interval = xint(max)
            Mainmenu.RefChart.ChartAreas("RefChart").AxisX.Minimum = 0
            Mainmenu.RefChart.ChartAreas("RefChart").AxisX.Title = "Concentration"
            Mainmenu.RefChart.ChartAreas("RefChart").AxisY.Title = "# Samples"

            '*************************Normal distribution****************************************
            Dim yakse As Double
            Dim xakse As Double
            Dim x As Double
            Dim maxx As Double = Math.Round(Math.Exp(-(1 / 2) * ((middel - middel) / SD) ^ 2) / (SD * Math.Sqrt(2 * Math.PI)), 4)


            Mainmenu.Normal.ChartAreas(0).AxisX.Interval = 1
            Mainmenu.Normal.ChartAreas(0).AxisX.Minimum = -4
            Mainmenu.Normal.ChartAreas(0).AxisX.Maximum = 4
            Mainmenu.Normal.ChartAreas(0).AxisX.MinorGrid.IntervalType = 0.25
            Mainmenu.Normal.ChartAreas(0).AxisX.MajorGrid.IntervalType = 1
            Mainmenu.Normal.ChartAreas(0).AxisY.Minimum = 0
            Mainmenu.Normal.ChartAreas(0).AxisY.Maximum = maxx + (maxx / 10)

            Mainmenu.Normal.ChartAreas("Normal").AxisY.Interval = pinty(maxx)
            Mainmenu.Normal.ChartAreas(0).AxisX.MajorGrid.IntervalType = 1
            Mainmenu.Normal.ChartAreas("Normal").AxisX.Title = "Standard deviation"
            Mainmenu.Normal.ChartAreas("Normal").AxisY.Title = "p(x)"
            Mainmenu.Progress_TXT.Text = "Filling Charts"
            Mainmenu.Progress_PB.Value = 0

            For r As Integer = 0 To dt.Rows.Count - 1
                '*******************************percentile plot*******************************
                If dt.Rows.Item(r)("_Result_") <= Output(0) Then
                    Mainmenu.RefChart.Series("Lav").Points.AddXY(dt.Rows(r)("_Result_"), r + 1)
                ElseIf dt.Rows.Item(r)("_Result_") <= Output(1) Then
                    Mainmenu.RefChart.Series("Ref").Points.AddXY(dt.Rows(r)("_Result_"), r + 1)
                ElseIf dt.Rows.Item(r)("_Result_") >= Output(1) Then
                    Mainmenu.RefChart.Series("Hoj").Points.AddXY(dt.Rows(r)("_Result_"), r + 1)
                End If

                '******************************Normal plot**********************************
                x = dt.Rows(r)("_Result_")
                yakse = Math.Round(Math.Exp(-(1 / 2) * ((x - middel) / SD) ^ 2) / (SD * Math.Sqrt(2 * Math.PI)), 5)
                xakse = Math.Round(((dt.Rows(r)("_Result_") - middel) / SD), 5)

                If xakse <= -2 Then
                    Mainmenu.Normal.Series("Lav").Points.AddXY(xakse, yakse)
                ElseIf xakse >= -2 And xakse <= 2 Then
                    Mainmenu.Normal.Series("Normal").Points.AddXY(xakse, yakse)
                ElseIf xakse >= 2 Then
                    Mainmenu.Normal.Series("Hoj").Points.AddXY(xakse, yakse)
                End If

                '****************************************************************************
                Mainmenu.Progress_PB.PerformStep()
            Next

            Mainmenu.Normal.Titles.Item("Title1").Text = dt.TableName
            Mainmenu.RefChart.Titles.Item("Title1").Text = dt.TableName

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error drawing charts")
        End Try

    End Sub
    Private Function xint(max As Integer) As Integer
        Select Case max
            Case > 50000
                xint = 10000
            Case > 25000
                xint = 5000
            Case > 10000
                xint = 2500
            Case > 5000
                xint = 1000
            Case > 2500
                xint = 500
            Case > 1000
                xint = 250
            Case > 500
                xint = 100
            Case > 100
                xint = 50
            Case > 50
                xint = 10
            Case > 25
                xint = 5
            Case > 10
                xint = 2.5
            Case > 5
                xint = 1
            Case > 1
                xint = 0.5
            Case Else
                xint = max / 10
        End Select
        Return xint
    End Function

    Private Function pinty(ByVal max As Double) As Double
        Select Case max
            Case > 0.1
                pinty = 0.1
            Case > 0.05
                pinty = 0.005
            Case > 0.01
                pinty = 0.001
            Case > 0.005
                pinty = 0.0005
            Case > 0.001
                pinty = 0.0001
            Case Else
                pinty = max / 10
        End Select
        Return pinty
    End Function

    Public Sub drawdataplot(ddrow As DataRow, ByRef dt As DataTable)
        '********************************************************************************
        '**                     Fill Data plot                                         **
        '********************************************************************************
        Mainmenu.DataPlot.Series("WithinRange").Points.Clear()
        Mainmenu.DataPlot.Series("OutsideRange").Points.Clear()
        Mainmenu.DataPlot.Series("Outliers").Points.Clear()
        Mainmenu.DataPlot.Series("LowP").Points.Clear()
        Mainmenu.DataPlot.Series("HighP").Points.Clear()
        Mainmenu.DataPlot.Series("Median").Points.Clear()
        Mainmenu.DataPlot.Series("LowSD").Points.Clear()
        Mainmenu.DataPlot.Series("HighSD").Points.Clear()
        Mainmenu.DataPlot.Series("Average").Points.Clear()

        Dim minDate As Date = Convert.ToDateTime(ddrow.Item("From Date").ToString)
        Dim maxDate As Date = Convert.ToDateTime(ddrow.Item("To Date").ToString)
        Dim median As Double
        Dim Average As Double
        Dim RangeP As String
        Dim RangeSD As String
        Select Case Mainmenu.View_CBX.SelectedItem
            Case "Percentile"
                Mainmenu.DataPlot.Series("LowSD").Enabled = False
                Mainmenu.DataPlot.Series("HighSD").Enabled = False
                Mainmenu.DataPlot.Series("Average").Enabled = False
                Mainmenu.DataPlot.Series("LowSD").IsVisibleInLegend = False
                Mainmenu.DataPlot.Series("HighSD").IsVisibleInLegend = False
                Mainmenu.DataPlot.Series("Average").IsVisibleInLegend = False

                Mainmenu.DataPlot.Series("LowP").Enabled = True
                Mainmenu.DataPlot.Series("HighP").Enabled = True
                Mainmenu.DataPlot.Series("Median").Enabled = True
                Mainmenu.DataPlot.Series("LowP").IsVisibleInLegend = True
                Mainmenu.DataPlot.Series("HighP").IsVisibleInLegend = True
                Mainmenu.DataPlot.Series("Median").IsVisibleInLegend = True
            Case "Standard Deviation"
                Mainmenu.DataPlot.Series("LowSD").Enabled = True
                Mainmenu.DataPlot.Series("HighSD").Enabled = True
                Mainmenu.DataPlot.Series("Average").Enabled = True
                Mainmenu.DataPlot.Series("LowSD").IsVisibleInLegend = True
                Mainmenu.DataPlot.Series("HighSD").IsVisibleInLegend = True
                Mainmenu.DataPlot.Series("Average").IsVisibleInLegend = True

                Mainmenu.DataPlot.Series("LowP").Enabled = False
                Mainmenu.DataPlot.Series("HighP").Enabled = False
                Mainmenu.DataPlot.Series("Median").Enabled = False
                Mainmenu.DataPlot.Series("LowP").IsVisibleInLegend = False
                Mainmenu.DataPlot.Series("HighP").IsVisibleInLegend = False
                Mainmenu.DataPlot.Series("Median").IsVisibleInLegend = False

            Case Else
                median = 0
                Average = 0
                RangeP = "0 - 0"
                RangeSD = "0 - 0"
        End Select
        median = ddrow.Item("Median")
        RangeP = ddrow.Item("Percentile")
        Mainmenu.DataPlot.Series("LowP").LegendText = Mainmenu.LavFrac_Nr.Value & "% Range"
        Mainmenu.DataPlot.Series("HighP").LegendText = Mainmenu.HojFrac_Nr.Value & "% Range"

        Average = ddrow.Item("Average")
        RangeSD = ddrow.Item("2SD")

        Dim RangePArray() As String
        RangePArray = RangeP.Split(" ")
        Dim LowP As Double = CDbl(RangePArray(0))
        Dim HighP As Double = CDbl(RangePArray(2))

        Dim RangeSDArray() As String
        RangeSDArray = RangeSD.Split(" ")
        Dim LowSD As Double = CDbl(RangeSDArray(0))
        Dim HighSD As Double = CDbl(RangeSDArray(2))

        Dim Result As Double
        Dim Comment As String


        Mainmenu.DataPlot.Titles.Item("Title1").Text = dt.TableName

        For Each drow As DataRow In dt.Rows
            Result = drow.Item("_Result_")
            Comment = drow.Item("_Comment_").ToString

            If drow.Item("_Include_") = False Then
                If Comment.Contains("Tukey") = True Then
                    Mainmenu.DataPlot.Series("Outliers").Points.AddXY(drow.Item("_Analysis_Date_"), Result)
                Else
                    Mainmenu.DataPlot.Series("OutsideRange").Points.AddXY(drow.Item("_Analysis_Date_"), Result)
                End If
            ElseIf drow.Item("_Include_") = True Then
                Mainmenu.DataPlot.Series("WithinRange").Points.AddXY(drow.Item("_Analysis_Date_"), Result)
            End If

        Next
        Mainmenu.DataPlot.Series("LowP").Points.AddXY(minDate, LowP)
        Mainmenu.DataPlot.Series("LowP").Points.AddXY(maxDate, LowP)

        Mainmenu.DataPlot.Series("HighP").Points.AddXY(minDate, HighP)
        Mainmenu.DataPlot.Series("HighP").Points.AddXY(maxDate, HighP)

        Mainmenu.DataPlot.Series("Median").Points.AddXY(minDate, median)
        Mainmenu.DataPlot.Series("Median").Points.AddXY(maxDate, median)

        Mainmenu.DataPlot.Series("LowSD").Points.AddXY(minDate, LowSD)
        Mainmenu.DataPlot.Series("LowSD").Points.AddXY(maxDate, LowSD)

        Mainmenu.DataPlot.Series("HighSD").Points.AddXY(minDate, HighSD)
        Mainmenu.DataPlot.Series("HighSD").Points.AddXY(maxDate, HighSD)

        Mainmenu.DataPlot.Series("Average").Points.AddXY(minDate, Average)
        Mainmenu.DataPlot.Series("Average").Points.AddXY(maxDate, Average)

        Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisX.Minimum = Double.NaN
        Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisY.Minimum = 0
        Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisX.Maximum = Double.NaN
        Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisY.Maximum = Double.NaN

        Mainmenu.DataPlot.ChartAreas("ChartArea1").RecalculateAxesScale()

        Mainmenu.Yaxis_BAR.Maximum = Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisY.Maximum
        Mainmenu.Yaxis_BAR.Value = Mainmenu.DataPlot.ChartAreas("ChartArea1").AxisY.Maximum

    End Sub

End Module
