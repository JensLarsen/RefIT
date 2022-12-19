Public Class PrintChart
    Private Sub PrintChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ChartSelect_CBX.SelectedIndex = 0
    End Sub

    Private Sub Select_BTN_Click(sender As Object, e As EventArgs) Handles Select_BTN.Click
        Mainmenu.CBXForm = Me.ChartSelect_CBX.SelectedItem
        Me.Close()
    End Sub
End Class