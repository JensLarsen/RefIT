Module NonCPRcalc

    Public Function NonCPR(dt As DataTable)
        dt.Columns(My.Settings.CPR).ColumnName = "_ID_"
        dt.Columns.Add("_Gender_", GetType(String))
        dt = CheckSex(dt)
        dt.Columns.Add("_Age_", GetType(Integer))
        dt = CheckAge(dt)

        Return dt

    End Function

    Private Function CheckSex(dt As DataTable)
        For Each dr As DataRow In dt.Rows
            If My.Settings.Kon <> "None" Then
                Select Case dr.Item(My.Settings.Kon).ToString
                    Case "Kvinde", "kvinde", "K", "k", "Female", "female", "F", "f"
                        dr.Item("_Gender_") = "F"
                    Case "Mand", "mand", "M", "m", "Male", "male"
                        dr.Item("_Gender_") = "M"
                    Case Else
                        dr.Item("_Gender_") = "N"
                End Select
            Else
                dr.Item("_Gender_") = "N"
            End If
        Next
        Return dt

    End Function
    Private Function CheckAge(dt As DataTable)

        Dim faar As DateTime = DateTime.MinValue
        Dim Alder As Integer

        If My.Settings.Alder <> "None" Then
            For Each dr As DataRow In dt.Rows
                If IsNumeric(dr.Item(My.Settings.Alder).ToString) Then
                    dr.Item("_Age_") = CInt(dr.Item(My.Settings.Alder))
                Else
                    dr.Item("_Age_") = 111
                End If
            Next
        ElseIf My.Settings.Birthday <> "None" And My.Settings.ProveDato <> "None" Then
            For Each dr As DataRow In dt.Rows
                If IsDate(dr.Item(My.Settings.Birthday).ToString) And IsDate(My.Settings.ProveDato).ToString Then
                    Alder = DateDiff(DateInterval.Year, faar, dr.Item("_Analysis_Date_"))
                    If Alder < 0 Or Alder > 130 Then
                        Alder = 2000
                    End If
                    dr.Item("_Age_") = Alder
                Else
                    dr.Item("_Age_") = 111
                End If
            Next
        Else
            MsgBox("No age column set - all patients are set as 50 years old!")
            For Each dr As DataRow In dt.Rows
                dr.Item("_Age_") = 50
            Next
        End If

        Return dt

    End Function
End Module
