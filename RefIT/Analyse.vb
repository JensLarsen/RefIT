Module Analyse

    Public Sub analyser(dt As DataTable)

        Dim dtdraw As DataTable
        Dim Output() As Integer
        Dim NewRow As DataRow = Mainmenu.Analyse_DS.Tables("AnalyseDT").NewRow()
        Dim dview As New DataView(dt)

        dview.RowFilter = SorterDataTilGraf()
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
        '*****************************************************************************************

        dtdraw = OnlyInclude(dt)
        '*******************************************************
        Output = Statistics(dtdraw, NewRow)

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

        If Mainmenu.UpdateChart = True Then Charts(dtdraw, Output)

        dt.DefaultView.Sort = "_ID_ ASC, _Analysis Date_ ASC"
        dt = dt.DefaultView.ToTable.Copy()

        dt.TableName = NewRow.Item("Analysis") & "_" & Mainmenu.Analyse_DS.Tables("AnalyseDT").Rows.Count - 1
        Mainmenu.Analyse_DS.Tables.Add(dt)

        If Mainmenu.UpdateDGV = True Then Mainmenu.DataGridView1.DataSource = dt.Copy()

        NewRow = Nothing

    End Sub
    Private Function OnlyInclude(drawDT As DataTable)

        Dim dview As New DataView(drawDT)

        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"
        Return (TryCast(dview.ToTable, DataTable))

    End Function
    Private Function PatientNr(ByRef dt As DataTable)

        Dim PNo As Integer
        Dim Patients = From row In dt.AsEnumerable()
                       Select row.Field(Of String)("_ID_") Distinct

        PNo = Patients.Count

        Return PNo

    End Function

    Private Function SorterDataTilGraf()

        Dim sorter As String = ""

        If Mainmenu.DataGridView1.Columns.Contains("_Gender_") Then
            Select Case Mainmenu.Kon_CBX.SelectedItem
                Case "Females"
                    sorter = "_Gender_ = 'F'"
                Case "Males"
                    sorter = "_Gender_ = 'M'"
                Case Else
            End Select
        End If

        If Mainmenu.DataGridView1.Columns.Contains("_Age_") Then
            If Mainmenu.MinNr.Value > 0 Or Mainmenu.MaxNr.Value < 100 Then
                If Mainmenu.MinNr.Value >= Mainmenu.MaxNr.Value Then
                    MsgBox("Error in age setting")
                    Mainmenu.MinNr.Value = 0
                    Mainmenu.MaxNr.Value = 100
                Else
                    If sorter <> "" Then
                        sorter = sorter & " AND _Age_ >= " & Mainmenu.MinNr.Value & " AND _Age_ <= " & Mainmenu.MaxNr.Value
                    Else
                        sorter = sorter & "_Age_ >= " & Mainmenu.MinNr.Value & " AND _Age_ <= " & Mainmenu.MaxNr.Value
                    End If
                End If
            End If
        End If
        Return sorter
    End Function

    Public Function Statistics(dt As DataTable, ByRef NewRow As DataRow)

        Dim LF As Double = (Mainmenu.LavFrac_Nr.Value / 100) * dt.Rows.Count - 1
        Dim HF As Double = (Mainmenu.HojFrac_Nr.Value / 100) * dt.Rows.Count - 1
        Dim md As Double = Math.Round(0.5 * dt.Rows.Count, 0) ' Median
        Dim LQ As Double = 0.25 * dt.Rows.Count - 1
        Dim HQ As Double = 0.75 * dt.Rows.Count - 1
        Dim Middel As Integer
        Dim SD As Integer
        Dim MinDate As Date
        Dim MaxDate As Date
        Dim PerQ() As Double '1-2 Percentiles, 3-4 Quartiles and 5-6 min max,7 median

        Mainmenu.Progress_TXT.Text = "Analyzing Data"
        Mainmenu.Progress_PB.Value = 0

        PerQ = Percentiles(dt)

        For r As Integer = 0 To dt.Rows.Count - 1
            Middel = Middel + dt.Rows(r)("_Result_")

            If r = 0 Then
                MinDate = dt.Rows(r)("_Analysis Date_")
                MaxDate = dt.Rows(r)("_Analysis Date_")
            End If

            If MinDate > dt.Rows(r)("_Analysis Date_") Then MinDate = dt.Rows(r)("_Analysis Date_")
            If MaxDate < dt.Rows(r)("_Analysis Date_") Then MaxDate = dt.Rows(r)("_Analysis Date_")

            Mainmenu.Progress_PB.PerformStep()
            Application.DoEvents()
        Next

        Middel = Middel / dt.Rows.Count 'calculate average
        SD = STDafv(dt, Middel) 'calculate standard deviation

        '*******************Copy data to datarow**********************************
        NewRow.Item("From Date") = MinDate.ToShortDateString
        NewRow.Item("To Date") = MaxDate.ToShortDateString
        NewRow.Item("Percentile") = PerQ(0) & " < X < " & PerQ(1)
        NewRow.Item("Median") = PerQ(4)
        NewRow.Item("Average") = Middel
        NewRow.Item("Min") = PerQ(2)
        NewRow.Item("Max") = PerQ(3)
        Select Case Mainmenu.PerMeth_CBX.SelectedItem
            Case "Interpolated C=0 - Excel after 2013"
                NewRow.Item("Percentile Analysis") = "Inter. C=0 - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
            Case "Interpolated C=1 - Excel to 2013"
                NewRow.Item("Percentile Analysis") = "Inter. C=1 - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
            Case "Nearest-Rank"
                NewRow.Item("Percentile Analysis") = "Nearest-Rank - " & Mainmenu.LavFrac_Nr.Value & "-" & Mainmenu.HojFrac_Nr.Value & "% - " & My.Settings.TidsInterval & " months"
        End Select
        NewRow.Item("2SD") = Middel - 2 * SD & " < X < " & Middel + 2 * SD

        '**********************Copy data to output to be used for drawing charts*****************************
        Dim Output() As Integer = {CInt(PerQ(0)), CInt(PerQ(1)), Middel, SD, CInt(PerQ(3))}

        Return Output

    End Function
    Private Function Percentiles(ByRef dt As DataTable)

        Dim Nn As Integer = dt.Rows.Count - 1
        Dim n(2) As Double
        Dim LF As Double = Mainmenu.LavFrac_Nr.Value / 100
        Dim HF As Double = Mainmenu.HojFrac_Nr.Value / 100
        Dim LowP As Double
        Dim HighP As Double
        Dim v1(2) As Double
        Dim v2(2) As Double
        Dim k(2) As Integer
        Dim d(2) As Double

        Dim min As Double = dt.Rows(0).Item("_Result_")
        Dim max As Double = dt.Rows(Nn).Item("_Result_")
        Dim median As Double = dt.Rows(Nn * 0.5).Item("_Result_")

        Select Case Mainmenu.PerMeth_CBX.SelectedItem
            Case "Interpolated C=0 - Excel after 2013" 'according to Wikipedia
                n(1) = (LF) * (Nn + 1)
                n(2) = (HF) * (Nn + 1)
                k(1) = CInt(Fix(n(1)))
                k(2) = CInt(Fix(n(2)))
                d(1) = n(1) - k(1)
                d(2) = n(2) - k(2)
                LowP = dt.Rows(k(1) - 1).Item("_Result_") + d(1) * (dt.Rows(k(1)).Item("_Result_") - dt.Rows(k(1) - 1).Item("_Result_"))
                HighP = dt.Rows(k(2) - 1).Item("_Result_") + d(2) * (dt.Rows(k(2)).Item("_Result_") - dt.Rows(k(2) - 1).Item("_Result_"))
            Case "Interpolated C=1 - Excel to 2013" 'according to Wikipedia
                n(1) = (LF) * (Nn - 1) + 1
                n(2) = (HF) * (Nn - 1) + 1
                k(1) = CInt(Fix(n(1)))
                k(2) = CInt(Fix(n(2)))
                d(1) = n(1) - k(1)
                d(2) = n(2) - k(2)
                LowP = dt.Rows(k(1) - 1).Item("_Result_") + d(1) * (dt.Rows(k(1)).Item("_Result_") - dt.Rows(k(1) - 1).Item("_Result_"))
                HighP = dt.Rows(k(2) - 1).Item("_Result_") + d(2) * (dt.Rows(k(2)).Item("_Result_") - dt.Rows(k(2) - 1).Item("_Result_"))
            Case "Nearest-Rank"
                n(1) = CInt((Mainmenu.LavFrac_Nr.Value / 100) * dt.Rows.Count - 1)
                n(2) = CInt((Mainmenu.HojFrac_Nr.Value / 100) * dt.Rows.Count - 1)
                LowP = dt.Rows(n(1)).Item("_Result_")
                HighP = dt.Rows(n(2)).Item("_Result_")
            Case Else

        End Select

        Dim Output() As Double = {Math.Round(LowP, 2), Math.Round(HighP, 2), min, max, median}

        Return Output

    End Function

    Public Function STDafv(ByVal Table As DataTable, ByRef middel As Double)

        Dim SD As Double
        Dim uppser As Double

        Try
            For Each dt As DataRow In Table.Rows
                uppser = uppser + (dt.Item("_Result_") - middel) ^ 2
            Next

            SD = Math.Sqrt(uppser / Table.Rows.Count - 2)

        Catch ex As Exception
            LogFejl(ex.ToString)
            SD = 1
        End Try

        Return SD

    End Function

    Private Function TukeyFences(dt As DataTable)

        Dim TukeyQ1 As Integer
        Dim TukeyQ2 As Integer
        Dim Q1Q2() As Integer
        Dim dview As New DataView(dt)

        dview.RowFilter = "_Include_ = True"
        dview.Sort = "_Result_ ASC"

        Try

            Dim dt2 As DataTable = TryCast(dview.ToTable, DataTable).Copy()

            TukeyQ1 = dt2.Rows(Math.Round(0.25 * dview.Count, 0)).Item("_Result_")
            TukeyQ2 = dt2.Rows(Math.Round(0.75 * dview.Count, 0)).Item("_Result_")

            Q1Q2 = {CInt(TukeyQ1 - 1.5 * (TukeyQ2 - TukeyQ1)), CInt(TukeyQ2 + 1.5 * (TukeyQ2 - TukeyQ1))}

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error removing Outliers")
        End Try

        Return (Q1Q2)
    End Function
    Private Function Tukey(dt As DataTable, ByRef NewRow As DataRow)

        Dim TukeyQ1Q2() As Integer = TukeyFences(dt)
        Dim Q1 As Integer = TukeyQ1Q2(0)
        Dim Q2 As Integer = TukeyQ1Q2(1)
        Dim OutliersNr As Integer

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

        NewRow.Item("Tukey Fences") = Q1 & " < x < " & Q2
        NewRow.Item("#Outliers") = OutliersNr

        Return dt

    End Function

    Public Sub Charts(dt As DataTable, Output As Integer())

        Dim middel As Integer = Output(2)
        Dim SD As Integer = Output(3)
        Dim lavfraktil As Integer = Output(0)
        Dim hojfraktil As Integer = Output(1)
        Dim max As Integer = Output(4)
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

            Mainmenu.Normal.ChartAreas(0).AxisX.Interval = 1
            Mainmenu.Normal.ChartAreas(0).AxisX.Minimum = -4
            Mainmenu.Normal.ChartAreas(0).AxisX.Maximum = 4
            Mainmenu.Normal.ChartAreas("Normal").AxisY.Interval = 0.005
            Mainmenu.Normal.ChartAreas(0).AxisY.Minimum = 0
            Mainmenu.Normal.ChartAreas(0).AxisY.Maximum = Math.Round(Math.Exp(-(1 / 2) * ((middel - middel) / SD) ^ 2) / (SD * Math.Sqrt(2 * Math.PI)), 4)
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

End Module
