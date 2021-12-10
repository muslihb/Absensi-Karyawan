Public Class Home
    Private Sub Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        PanelMenu.Visible = False
    End Sub
    Sub removecombo()
        Form1.ComboBox1.Items.Remove("Masuk")
        Form1.ComboBox1.Items.Remove("Pulang")
        Form1.ComboBox1.Items.Remove("Ijin")
        Form1.ComboBox1.Items.Remove("Sakit")
    End Sub
    Sub linked()
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label1.Text = "Admin" And PanelMenu.Visible = False Then
            PanelMenu.Visible = True
            PanelMenu.Height = 97
            Button3.Visible = True
        ElseIf Label1.Text = "Users" Then
            conn.Close()
            koneksi()
            inputpanel(AbsenKaryawan)
            Button1.Enabled = False
        Else
            PanelMenu.Visible = False
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Me.Panel2.Controls.Count > 0 Then
            Me.Panel2.Controls.RemoveAt(0)
        End If
        PanelMenu.Visible = False
        Button1.Enabled = True
        Me.Hide()
        LoginForm1.Show()
        linked()
        Button5.Enabled = False
        Form1.ComboBox1.ResetText()
        removecombo()
        conn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        linked()
        Button2.Enabled = False
        PanelMenu.Visible = False
        Form1.TampilData()
        inputpanel(Form1)
    End Sub
    Public Sub inputpanel(ByVal formpanel As Object)
        If Me.Panel2.Controls.Count > 0 Then
            Me.Panel2.Controls.RemoveAt(0)
        End If
        Dim fp As Form = TryCast(formpanel, Form)
        fp.TopLevel = False
        fp.FormBorderStyle = FormBorderStyle.None
        fp.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(fp)
        Me.Panel2.Tag = fp
        fp.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Registrasi_Absen.TampilData()
        Registrasi_Absen.Button3.Visible = False
        inputpanel(Registrasi_Absen)
        linked()
        Button3.Enabled = False
        PanelMenu.Visible = False
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        If Label1.Text = "Admin" Then
            Button4.Visible = True
        Else
            Button4.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Laporan_Kehadiran.tampilData()
        Laporan_Kehadiran.ComboBox1.Text = "All"
        inputpanel(Laporan_Kehadiran)
        linked()
        Button4.Enabled = False
    End Sub
End Class