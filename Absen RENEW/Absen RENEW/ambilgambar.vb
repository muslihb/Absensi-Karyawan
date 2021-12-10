Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Public Class ambilgambar
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()
        End If
    End Sub
    Private Sub Captured(sender As Object, eventArgs As NewFrameEventArgs)
        bmp = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox2.Image = PictureBox1.Image
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveFileDialog1.DefaultExt = ".jpg"
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox2.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
            RegistAbsen.PictureBox1.Image = PictureBox2.Image
            RegistAbsen.Label13.Text = SaveFileDialog1.FileName
            camera.Stop()
            PictureBox1.Image = Nothing
        End If
    End Sub
End Class