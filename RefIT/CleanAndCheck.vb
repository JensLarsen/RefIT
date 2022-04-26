Module CleanAndCheck

    Public Sub KolonneRaekke(ByRef dt As DataTable)
        dt.Columns("_Include_").SetOrdinal(0)
        dt.Columns("_ID_").SetOrdinal(1)
        dt.Columns("_Analysis_").SetOrdinal(2)
        dt.Columns("_Gender_").SetOrdinal(3)
        dt.Columns("_Age_").SetOrdinal(4)
        dt.Columns("_Analysis Date_").SetOrdinal(5)
        dt.Columns("_Result_").SetOrdinal(6)
        dt.Columns("_Comment_").SetOrdinal(7)
    End Sub

    Public Function Rens(dt As DataTable)
        Dim UdenFor As Integer
        Dim Iku As Integer
        Dim Inkluderet As Integer

        dt.Columns.Add("_Include_", GetType(Boolean))
        dt.Columns.Add("_Analysis_", GetType(String))
        dt.Columns.Add("_Analysis Date_", GetType(Date))
        dt.Columns.Add("_Result_", GetType(Integer))
        dt.Columns.Add("_Comment_", GetType(String))

        Try
            Mainmenu.Progress_TXT.Text = "Cleaning dataset"
            Mainmenu.Progress_PB.Maximum = dt.Rows.Count
            Mainmenu.Progress_PB.Value = 0

            For r As Integer = dt.Rows.Count - 1 To 0 Step -1
                If Not IsNumeric(dt.Rows.Item(r)(My.Settings.Resultat)) Then
                    Select Case dt.Rows.Item(r)(My.Settings.Resultat).ToString.First
                        Case ">", "<"
                            UdenFor = UdenFor + 1
                            dt.Rows(r).Delete()
                        Case Else
                            Iku = Iku + 1
                            dt.Rows(r).Delete()
                    End Select
                Else
                    If My.Settings.IncludeZero = False And CInt(dt.Rows(r).Item(My.Settings.Resultat)) = 0 Then
                        dt.Rows(r).Delete()
                    Else
                        dt.Rows(r).Item("_Result_") = CInt(dt.Rows(r).Item(My.Settings.Resultat))
                        dt.Rows(r).Item("_Analysis_") = dt.Rows(r).Item(My.Settings.Kvantitet)
                    End If
                    Inkluderet = Inkluderet + 1
                End If
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next
            dt.Columns.Remove(My.Settings.Resultat)
            dt.Columns.Remove(My.Settings.Kvantitet)

            Mainmenu.Ikkeafs_LBL.Text = Iku
            Mainmenu.Udenmaal_LBL.Text = UdenFor
            Mainmenu.Udentid_LBL.Text = Inkluderet

            Mainmenu.InfoChart.Series("Status").Points.Clear()

            Mainmenu.InfoChart.Titles(0).Text = "Import Statistics"

            Mainmenu.InfoChart.Series("Status").Points.AddXY("#Not concluded", Iku)
            Mainmenu.InfoChart.Series("Status").Points.AddXY("#Out of Range", UdenFor)
            Mainmenu.InfoChart.Series("Status").Points.AddXY("#Approved", Inkluderet)

            Mainmenu.InfoChart.Series("Status").Points(0).Color = Color.CornflowerBlue
            Mainmenu.InfoChart.Series("Status").Points(1).Color = Color.IndianRed
            Mainmenu.InfoChart.Series("Status").Points(2).Color = Color.ForestGreen

            Mainmenu.Stat1.Text = "#Not concluded"
            Mainmenu.Stat2.Text = "#Out of Range"
            Mainmenu.Stat3.Text = "#Approved"
            Mainmenu.Stat4.Text = ""

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error cleaning data")
        End Try

        Return dt
    End Function
    Public Function Datokontrol(dt As DataTable)

        Try
            Mainmenu.Progress_TXT.Text = "Checking Analysis Dates"
            Mainmenu.Progress_PB.Maximum = dt.Rows.Count
            Mainmenu.Progress_PB.Value = 0

            For r As Integer = dt.Rows.Count - 1 To 0 Step -1
                If Not IsDBNull(dt.Rows(r)(My.Settings.ProveDato)) = True Then
                    If IsDate(dt.Rows(r)(My.Settings.ProveDato)) Then
                        dt.Rows(r)("_Analysis Date_") = dt.Rows(r)(My.Settings.ProveDato)
                    Else
                        dt.Rows(r).Delete()
                    End If
                End If
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next
            dt.Columns.Remove(My.Settings.ProveDato)

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error checking Analysis Date")
        End Try

        Return dt

    End Function

End Module
