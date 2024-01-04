Public Class patients_min_max
    Private Sub patients_min_max_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Sort_CBX.SelectedIndex = 0
        SDorPercentile.SelectedIndex = 0
    End Sub

    Private Sub Exit_BTN_Click(sender As Object, e As EventArgs) Handles Exit_BTN.Click
        Me.Close()
    End Sub

    Private Sub Sort_CBX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sort_CBX.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtt As New DataTable
        dt = Mainmenu.MinMaxDT.Copy()
        Dim avg As Long
        Dim PatientN As Integer = 0
        Dim n As Integer = 0

        Try
            Min_Max_Chart.Series(0).Points.Clear()
            Min_Max_Chart.Series(1).Points.Clear()
            Min_Max_Chart.Series(2).Points.Clear()
            Min_Max_Chart.Series(3).Points.Clear()

            Dim dview As New DataView(dt)
            dview.RowFilter = "_Include_ = True"
            If Me.Sort_CBX.SelectedItem = "Maximum" Then
                dview.Sort = "_Result_ DESC, _ID_ ASC"
            ElseIf Me.Sort_CBX.SelectedItem = "Minimum" Then
                dview.Sort = "_Result_ ASC, _ID_ ASC"
            End If

            dtt = TryCast(dview.ToTable, DataTable).Copy()

            Dim IDnr = From v In dtt.AsEnumerable Select v.Field(Of String)("_ID_") Distinct

            If Not IsNothing(IDnr) Then
                For Each t As String In IDnr
                    Dim selectrows = From v In dtt.AsEnumerable Where v.Item("_ID_") = t Select v
                    If Not IsNothing(selectrows) Then
                        PatientN = PatientN + 1
                        avg = 0

                        For Each r As DataRow In selectrows
                            n = n + 1
                            Select Case n
                                Case = 1
                                    If Me.Sort_CBX.SelectedItem = "Maximum" Then
                                        Min_Max_Chart.Series("Max").Points.AddXY(PatientN, r.Item("_Result_"))
                                    ElseIf Me.Sort_CBX.SelectedItem = "Minimum" Then
                                        Min_Max_Chart.Series("Min").Points.AddXY(PatientN, r.Item("_Result_"))
                                    End If

                                Case = selectrows.Count
                                    If Me.Sort_CBX.SelectedItem = "Maximum" Then
                                        Min_Max_Chart.Series("Min").Points.AddXY(PatientN, r.Item("_Result_"))
                                    ElseIf Me.Sort_CBX.SelectedItem = "Minimum" Then
                                        Min_Max_Chart.Series("Max").Points.AddXY(PatientN, r.Item("_Result_"))
                                    End If

                                Case Else
                                    Min_Max_Chart.Series("Others").Points.AddXY(PatientN, r.Item("_Result_"))
                            End Select
                            avg = avg + r.Item("_Result_")
                        Next
                        If n > 1 Then
                            Min_Max_Chart.Series("Avg").Points.AddXY(PatientN, avg / selectrows.Count)
                        End If
                    End If
                    n = 0
                    selectrows = Nothing
                Next

                Min_Max_Chart.Series("LowP").Points.AddXY(0, Mainmenu.LowLimit)
                Min_Max_Chart.Series("LowP").Points.AddXY(IDnr.Count, Mainmenu.LowLimit)

                Min_Max_Chart.Series("HighP").Points.AddXY(0, Mainmenu.HighLimit)
                Min_Max_Chart.Series("HighP").Points.AddXY(IDnr.Count, Mainmenu.HighLimit)

                Min_Max_Chart.Series("Median").Points.AddXY(0, Mainmenu.PMedian)
                Min_Max_Chart.Series("Median").Points.AddXY(IDnr.Count, Mainmenu.PMedian)

                Min_Max_Chart.Series("-2xSD").Points.AddXY(0, Mainmenu.Average + Mainmenu.STD * -2)
                Min_Max_Chart.Series("-2xSD").Points.AddXY(IDnr.Count, Mainmenu.Average + Mainmenu.STD * -2)

                Min_Max_Chart.Series("2xSD").Points.AddXY(0, Mainmenu.Average + Mainmenu.STD * 2)
                Min_Max_Chart.Series("2xSD").Points.AddXY(IDnr.Count, Mainmenu.Average + Mainmenu.STD * 2)

                Min_Max_Chart.Series("Average").Points.AddXY(0, Mainmenu.Average)
                Min_Max_Chart.Series("Average").Points.AddXY(IDnr.Count, Mainmenu.Average)

                Min_Max_Chart.ChartAreas(0).AxisX.Minimum = 0
                Min_Max_Chart.ChartAreas(0).AxisY.Minimum = 0
                Min_Max_Chart.ChartAreas(0).AxisX.Maximum = IDnr.Count

                plotchange()
            End If

        Catch ex As Exception
            LogFejl(ex.ToString)
            MsgBox("Error while generating range chart!")
        End Try
    End Sub

    Private Sub Export_BTN_Click(sender As Object, e As EventArgs) Handles Export_BTN.Click

        Dim savefile As SaveFileDialog = New SaveFileDialog
        Dim savechart As New System.Windows.Forms.DataVisualization.Charting.Chart
        Dim mystream As New System.IO.MemoryStream
        Dim title As String
        Dim unitofconc As String

        title = InputBox("Please enter title of the chart")
        If title = "" Then Exit Sub

        Me.Min_Max_Chart.Serializer.Save(mystream)
        savechart.Serializer.Load(mystream)
        unitofconc = InputBox("Please enter concentration unit of the analysis")
        savechart.ChartAreas(0).AxisY.Title = "Concentration / " & unitofconc
        savechart.ChartAreas(0).AxisX.Title = "Patients ranged by their " & Me.Sort_CBX.SelectedItem.ToString & " result"

        savechart.Titles.Item(0).Text = title

        savechart.Size = New Size(2400, 2400)
        savechart.BackColor = Color.White
        savechart.ChartAreas(0).BackColor = Color.White

        savechart.Titles.Item(0).Font = New Font("Arial", 72, FontStyle.Bold)
        savechart.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 48, FontStyle.Bold)
        savechart.ChartAreas(0).AxisY.LabelStyle.Font = New Font("Arial", 48, FontStyle.Bold)

        savechart.ChartAreas(0).AxisX.LineWidth = 5
        savechart.ChartAreas(0).AxisY.LineWidth = 5
        savechart.ChartAreas(0).AxisX.LineColor = Color.Black
        savechart.ChartAreas(0).AxisY.LineColor = Color.Black
        savechart.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.Black
        savechart.ChartAreas(0).AxisX.MajorGrid.LineWidth = 3
        savechart.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.Black
        savechart.ChartAreas(0).AxisY.MajorGrid.LineWidth = 3

        savechart.Size = New Size(4800, 2400)
        savechart.Series("Others").MarkerSize = 24
        savechart.Series("Min").MarkerSize = 24
        savechart.Series("Max").MarkerSize = 24
        savechart.Series("Avg").MarkerSize = 24

        savechart.Series("LowP").MarkerSize = 36
        savechart.Series("HighP").MarkerSize = 36
        savechart.Series("Median").MarkerSize = 36
        savechart.Series("2xSD").MarkerSize = 36
        savechart.Series("-2xSD").MarkerSize = 36
        savechart.Series("Average").MarkerSize = 36

        savechart.Legends("Legend1").Font = New Font("Arial", 36, FontStyle.Bold)

        savechart.Series("LowP").BorderWidth = 12
        savechart.Series("HighP").BorderWidth = 12
        savechart.Series("Median").BorderWidth = 12
        savechart.Series("-2xSD").BorderWidth = 12
        savechart.Series("2xSD").BorderWidth = 12
        savechart.Series("Average").BorderWidth = 12

        savechart.Series("Max").Color = Color.Red
        savechart.Series("Others").Color = Color.CadetBlue
        savechart.Series("Avg").Color = Color.Black
        savechart.Series("Min").Color = Color.DarkOliveGreen
        savechart.Series("Median").Color = Color.Black
        savechart.Series("HighP").Color = Color.Blue
        savechart.Series("LowP").Color = Color.Blue
        savechart.Series("-2xSD").Color = Color.Blue
        savechart.Series("2xSD").Color = Color.Blue
        savechart.Series("Average").Color = Color.Black

        savefile.Filter = "PNG Image (*.png*)|*.png|JPEG Image (*.jpg*)|*.jpg|Bitmap Image (*.bmp*)|*.bmp|TIFF Image (*.tiff*)|*.tiff|emf Image (*.emf*)|*.emf|wmf Image (*.wmf*)|*.wmf"
        If savefile.ShowDialog = DialogResult.OK Then
            Dim path As String = savefile.FileName
            Dim fi As New IO.FileInfo(path)
            Dim extn As String = fi.Extension
            Select Case extn
                Case ".png"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Png)
                Case ".jpg"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                Case ".bmp"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                Case ".tiff"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                Case ".emf"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Emf)
                Case ".wmf"
                    savechart.SaveImage(savefile.FileName, System.Drawing.Imaging.ImageFormat.Wmf)
            End Select
        End If

        'savechart.Titles.Item(1).Visible = True

    End Sub

    Private Sub SDorPercentile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SDorPercentile.SelectedIndexChanged
        plotchange()
    End Sub

    Private Sub plotchange()

        Select Case Me.SDorPercentile.SelectedItem
            Case "Low/High Percentile"
                Min_Max_Chart.Series("LowP").IsVisibleInLegend = True
                Min_Max_Chart.Series("HighP").IsVisibleInLegend = True
                Min_Max_Chart.Series("Median").IsVisibleInLegend = True
                Min_Max_Chart.Series("LowP").Enabled = True
                Min_Max_Chart.Series("HighP").Enabled = True
                Min_Max_Chart.Series("Median").Enabled = True

                Min_Max_Chart.Series("-2xSD").IsVisibleInLegend = False
                Min_Max_Chart.Series("2xSD").IsVisibleInLegend = False
                Min_Max_Chart.Series("Average").IsVisibleInLegend = False
                Min_Max_Chart.Series("-2xSD").Enabled = False
                Min_Max_Chart.Series("2xSD").Enabled = False
                Min_Max_Chart.Series("Average").Enabled = False

            Case "2x Standard Deviation"
                Min_Max_Chart.Series("LowP").IsVisibleInLegend = False
                Min_Max_Chart.Series("HighP").IsVisibleInLegend = False
                Min_Max_Chart.Series("Median").IsVisibleInLegend = False
                Min_Max_Chart.Series("LowP").Enabled = False
                Min_Max_Chart.Series("HighP").Enabled = False
                Min_Max_Chart.Series("Median").Enabled = False

                Min_Max_Chart.Series("-2xSD").IsVisibleInLegend = True
                Min_Max_Chart.Series("2xSD").IsVisibleInLegend = True
                Min_Max_Chart.Series("Average").IsVisibleInLegend = True
                Min_Max_Chart.Series("-2xSD").Enabled = True
                Min_Max_Chart.Series("2xSD").Enabled = True
                Min_Max_Chart.Series("Average").Enabled = vbTrue
            Case Else
                Min_Max_Chart.Series("LowP").IsVisibleInLegend = False
                Min_Max_Chart.Series("HighP").IsVisibleInLegend = False
                Min_Max_Chart.Series("Median").IsVisibleInLegend = False
                Min_Max_Chart.Series("LowP").Enabled = False
                Min_Max_Chart.Series("HighP").Enabled = False
                Min_Max_Chart.Series("Median").Enabled = False

                Min_Max_Chart.Series("-2xSD").IsVisibleInLegend = False
                Min_Max_Chart.Series("2xSD").IsVisibleInLegend = False
                Min_Max_Chart.Series("Average").IsVisibleInLegend = False
                Min_Max_Chart.Series("-2xSD").Enabled = False
                Min_Max_Chart.Series("2xSD").Enabled = False
                Min_Max_Chart.Series("Average").Enabled = False
        End Select

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Min_Max_Chart.Series("Others").IsVisibleInLegend = True
            Min_Max_Chart.Series("Others").Enabled = True
        Else
            Min_Max_Chart.Series("Others").IsVisibleInLegend = False
            Min_Max_Chart.Series("Others").Enabled = False
        End If

        Min_Max_Chart.ChartAreas(0).AxisX.Minimum = 0
    End Sub
End Class

