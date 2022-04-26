Public Class SetupMain
    Private Sub SetupMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Email_TXT.Text = My.Settings.Notificationemail
    End Sub

    Private Sub Gem_BTN_Click(sender As Object, e As EventArgs) Handles Gem_BTN.Click
        My.Settings.Notificationemail = Me.Email_TXT.Text

        MsgBox("The settings have been updated.")

        Me.Close()
    End Sub

    Private Sub Afbryd_BTN_Click(sender As Object, e As EventArgs) Handles Afbryd_BTN.Click
        Me.Close()
    End Sub

End Class