Public Class patients_min_max
    Private Sub patients_min_max_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'try
        'Dim dt As DataTable = Mainmenu.Analyse_DS.Tables.Item(Mainmenu.CurrentTable).Copy()
        'Dim min As Long
        'Dim max As Long
        'Dim avg As Long
        'Dim n As Integer

        'Dim dview As New DataView(dt)
        'dview.RowFilter = "_Include_ = True"
        'dview.Sort = "_ID_ ASC, _Result_ ASC"
        'dt = TryCast(dview.ToTable, DataTable).Copy()

        'Dim IDnr = From v In dt.AsEnumerable Select v.Field(Of String)("_ID_") Distinct

        'If Not IsNothing(IDnr) Then
         '       For Each t As String In IDnr
          '          Dim selectrows = From v In dt.AsEnumerable Where v.Item("_ID_") = t Select v
           '     If Not IsNothing(selectrows) Then
            '        n = 0
             '       avg = 0
              '      min = 0
               '     max = 0
                '    For Each r As DataRow In selectrows
                 '       If n = 0 Then min = r.Item("_Result_")
                  '      avg = avg + r.Item("_Result_")
                   '     max = r.Item("_Result_")
                    'Next
                   ' avg = avg / n
                  '  MsgBox(min & " - " & avg & " - " & max)
               ' End If

              '  selectrows = Nothing
             '   Next
           ' End If



        'Catch ex As Exception
        'LogFejl(ex.ToString)
        MsgBox("Undergoing construction!")
        'End Try



    End Sub
End Class