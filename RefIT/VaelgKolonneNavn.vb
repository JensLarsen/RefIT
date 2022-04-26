Public Class VaelgKolonneNavn
    Private Sub VaelgKolonneNavn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KolonneNavn_CBX.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Mainmenu.CBXForm = Me.KolonneNavn_CBX.SelectedItem
        Me.Close()
    End Sub

    Private Sub Abort_BTN_Click(sender As Object, e As EventArgs) Handles Abort_BTN.Click
        ImpExp.AbortLoad = True
        Me.Close()
    End Sub
End Class