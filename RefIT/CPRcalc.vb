Option Explicit On

Module CPRcalc
    Public Function CheckCPR(ByRef dt As DataTable) As Boolean

        'Check first row to see if it contains cpr number, returns false if not
        Dim IDnr As String

        Try
            If dt.Rows(1)(My.Settings.CPR).ToString <> "" Then
                IDnr = dt.Rows(1)(My.Settings.CPR).ToString
            Else
                IDnr = dt.Rows(2)(My.Settings.CPR).ToString
            End If

            If IDnr.Length() = 11 Then
                If IsNumeric(IDnr.Substring(0, 6)) = True And IDnr.Substring(6, 1) = "-" Then
                    CheckCPR = True
                Else
                    CheckCPR = False
                End If
            Else
                CheckCPR = False
            End If

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error while checking Patient ID!")
            CheckCPR = False
        End Try


        Return CheckCPR

    End Function

    Public Function CalculateID(dt As DataTable)

        dt.Columns.Add("_ID_", GetType(String))

        dt = Anonymiser(dt)

        dt.Columns.Add("_Gender_", GetType(String))

        dt.Columns.Add("_Age_", GetType(Integer))

        dt = BeregnAlder(dt)

        dt = FindKon(dt)

        dt.Columns.Remove(My.Settings.CPR)

        Return dt

    End Function


    Public Function Anonymiser(dt As DataTable)

        Dim IDnr = From v In dt.AsEnumerable Select v.Field(Of String)(My.Settings.CPR) Distinct
        Dim NewID As Integer = 0

        Mainmenu.Progress_TXT.Text = "Performing anonymization"
        Mainmenu.Progress_PB.Maximum = dt.Rows.Count
        Mainmenu.Progress_PB.Value = 0
        Mainmenu.UseWaitCursor = True
        Application.DoEvents()

        Try

            If Not IsNothing(IDnr) Then
                For Each t As String In IDnr
                    Dim selectrows = From v In dt.AsEnumerable Where v.Item(My.Settings.CPR) = t Select v
                    If Not IsNothing(selectrows) Then
                        NewID = NewID + 1
                        For Each r As DataRow In selectrows
                            r.Item("_ID_") = NewID
                        Next
                    End If
                    selectrows = Nothing
                    Mainmenu.Progress_PB.PerformStep()
                    Application.DoEvents()
                Next
            End If
        Catch ex As Exception
            LogFejl(ex.ToString)
            Mainmenu.UseWaitCursor = False
            MsgBox("Error during anonymization!")
            Mainmenu.Progress_TXT.Text = ""
        End Try

        Mainmenu.Progress_TXT.Text = "Please be patient...."
        Mainmenu.Progress_PB.Value = 0
        Mainmenu.UseWaitCursor = False

        Return dt
    End Function
    Private Function FindKon(dt As DataTable)

        Dim Kon As String = ""
        Dim EndeTal As Long
        Dim CPR As String

        Try
            Mainmenu.Progress_TXT.Text = "Calculating gender"
            Mainmenu.Progress_PB.Maximum = dt.Rows.Count
            Mainmenu.Progress_PB.Value = 0
            Mainmenu.UseWaitCursor = True
            Application.DoEvents()

            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i)(My.Settings.CPR).ToString <> "" Then
                    CPR = dt.Rows(i)(My.Settings.CPR)
                    If IsNumeric(Mid(CPR.ToString, 11, 1)) Then
                        EndeTal = CInt(Mid(CPR, 11, 1))
                        If EndeTal And 1 Then
                            dt.Rows(i)("_Gender_") = "M"
                        ElseIf (EndeTal And 1) = 0 Then
                            dt.Rows(i)("_Gender_") = "F"
                        Else
                            dt.Rows(i)("_Gender_") = "N"
                        End If
                    Else
                        dt.Rows(i)("_Gender_") = "N"
                    End If
                Else
                    dt.Rows(i).Delete()
                End If
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next

        Catch ex As Exception
            LogFejl(ex.ToString)
            Mainmenu.UseWaitCursor = False
            MsgBox("Error calculating male/female")
            Mainmenu.Progress_TXT.Text = ""
        End Try

        Mainmenu.UseWaitCursor = False

        Return dt

    End Function
    Private Function BeregnAlder(dt As DataTable)

        Dim faar As DateTime = DateTime.MinValue
        Dim Alder As Integer
        Dim hundredenr As Integer
        Dim aarnr As String
        Dim datonr As String
        Dim CPR As String = ""

        Try
            Mainmenu.Progress_TXT.Text = "Calculating age"
            Mainmenu.Progress_PB.Maximum = dt.Rows.Count
            Mainmenu.Progress_PB.Value = 0
            Mainmenu.UseWaitCursor = True
            Application.DoEvents()

            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i)(My.Settings.CPR).ToString <> "" Then
                    If Len(dt.Rows(i)(My.Settings.CPR)) < 11 Then
                        CPR = "1" & CPR
                    Else
                        CPR = dt.Rows(i)(My.Settings.CPR)
                    End If

                    If CPR.IndexOf("-") = 6 And Len(dt.Rows(i)(My.Settings.CPR)) = 11 Then
                        hundredenr = CInt(CPR.Chars(7).ToString)
                        aarnr = Mid(CPR, 5, 2)
                        datonr = Mid(CPR, 1, 4)
                        Select Case hundredenr
                            Case Is = 0, 1, 2, 3
                                If (DateTime.TryParseExact(datonr & "19" & aarnr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, faar)) Then

                                End If
                            Case Is = 4, 9
                                If CInt(aarnr) < 37 Then
                                    If (DateTime.TryParseExact(datonr & "20" & aarnr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, faar)) Then

                                    End If
                                Else
                                    If (DateTime.TryParseExact(datonr & "19" & aarnr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, faar)) Then
                                    End If
                                End If
                            Case Is = 5, 6, 7, 8
                                If CInt(aarnr) < 57 Then
                                    If (DateTime.TryParseExact(datonr & "20" & aarnr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, faar)) Then
                                    End If
                                Else
                                    If (DateTime.TryParseExact(datonr & "18" & aarnr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, faar)) Then
                                    End If
                                End If
                            Case Else

                        End Select

                        Alder = DateDiff(DateInterval.Year, faar, dt.Rows(i)("_Analysis_Date_"))
                        If Alder < 0 Then
                            Alder = 111
                        End If
                    Else
                        MsgBox("Age could not be calculated for the sample with birthday : " & faar & vbNewLine & "Analysis date : " & dt.Rows(i)("_Analysis_Date_"))
                        Alder = 111
                    End If
                Else
                    Alder = 111
                End If

                dt.Rows(i)("_Age_") = Alder
                Mainmenu.Progress_PB.PerformStep()
                Application.DoEvents()
            Next

        Catch ex As Exception
            LogFejl(ex.ToString)
            Mainmenu.UseWaitCursor = False
            MsgBox("Error calculating age")
            Mainmenu.Progress_TXT.Text = ""
        End Try
        Mainmenu.UseWaitCursor = False
        Return dt

    End Function
End Module
