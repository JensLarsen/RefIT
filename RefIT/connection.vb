Imports System.Data
Imports outlook = Microsoft.Office.Interop.Outlook

Public Module Connection

    Public Sub LogFejl(ByVal fejl As String)


        Dim filename As String = String.Format("{0}\", Environment.CurrentDirectory) & "fejllog.txt"

        IO.File.AppendAllText(filename, String.Format("{0}[{1}]{2}", Environment.NewLine, DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss"), fejl.ToString()))
        If My.Settings.SendMail = True Then
            Sendmessage("RefIT : Ny registreret fejl", fejl, True)
        Else
            MsgBox("RefIT : Ny registreret fejl" & vbNewLine & fejl)
        End If

    End Sub

    Public Sub Sendmessage(ByVal Emessagesubject As String, ByVal Emessagebody As String, Fejl As Boolean)

        Dim OutlookMessage As outlook.MailItem
        Dim AppOutlook As New outlook.Application

        Dim objNS As outlook._NameSpace = AppOutlook.Session
        Dim objFolder As outlook.MAPIFolder
        objFolder = objNS.GetDefaultFolder(outlook.OlDefaultFolders.olFolderDrafts)
        Dim EmailAdr As String

        Try
            OutlookMessage = DirectCast(AppOutlook.CreateItem(outlook.OlItemType.olMailItem), outlook.MailItem)
            Dim Recipents As outlook.Recipients = OutlookMessage.Recipients
            If Fejl = True Then
                Emessagebody = Emessagebody & vbCrLf & vbCrLf & "Med Venlig Hilsen" & vbCrLf & Environment.UserName
                EmailAdr = My.Settings.Notificationemail
                Recipents.Add(My.Settings.Notificationemail)
            End If
            If ValidEmail(My.Settings.Notificationemail) = True Then
                Recipents.Add(My.Settings.Notificationemail)
                OutlookMessage.Subject = Emessagesubject
                OutlookMessage.Body = Emessagebody
                OutlookMessage.BodyFormat = outlook.OlBodyFormat.olFormatHTML
                OutlookMessage.Save()
                OutlookMessage.Send()
            End If

        Catch ex As Exception
            MessageBox.Show("Emailen kunne ikke sendes. " & vbNewLine & "Kontroller email adresse i setup.")
            LogFejl(ex.ToString)
        Finally
            OutlookMessage = Nothing
            AppOutlook = Nothing
        End Try


    End Sub

    Private Function ValidEmail(ByVal Email As String) As Boolean
        Try
            Dim EmailTo As New System.Net.Mail.MailAddress(Email)
            ValidEmail = True
        Catch ex As Exception
            MsgBox("Kontrollere at email adresser er tilstede og skrevet korrekt i opsætningen.")
            ValidEmail = False
        End Try

        Return ValidEmail

    End Function
End Module

