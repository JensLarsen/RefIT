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
            Select Case dr.Item(My.Settings.Kon)
                Case "Kvinde", "kvinde", "K", "k", "Female", "female", "F", "f"
                    dr.Item("_Gender_") = "F"
                Case "Mand", "mand", "M", "m", "Male", "male"
                    dr.Item("_Gender_") = "M"
                Case Else
                    dr.Item("_Gender_") = "N"
            End Select
        Next
        Return dt

    End Function
    Private Function CheckAge(dt As DataTable)

        Dim faar As DateTime = DateTime.MinValue
        Dim Alder As Integer

        If My.Settings.Alder <> "Non" Then
            For Each dr As DataRow In dt.Rows
                If IsNumeric(dr.Item(My.Settings.Alder)) Then
                    dr.Item("_Age_") = dr.Item(My.Settings.Alder)
                Else
                    dr.Item("_Age_") = 111
                End If
            Next
        ElseIf My.Settings.Birthday <> "Non" And My.Settings.ProveDato <> "Non" Then
            For Each dr As DataRow In dt.Rows
                If IsDate(dr.Item(My.Settings.Birthday)) And IsDate(My.Settings.ProveDato) Then
                    Alder = DateDiff(DateInterval.Year, faar, dr.Item("_Analysis Date_"))
                    If Alder < 0 Or Alder > 130 Then
                        Alder = 2000
                    End If
                    dr.Item("_Age_") = Alder
                Else
                    dr.Item("_Age_") = 111
                End If
            Next
        Else
            For Each dr As DataRow In dt.Rows
                dr.Item("_Age_") = 111
            Next
        End If

        Return dt

    End Function
End Module
