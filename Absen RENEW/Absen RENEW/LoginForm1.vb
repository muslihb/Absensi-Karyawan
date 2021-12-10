Imports MySql.Data.MySqlClient
Public Class LoginForm1
    Sub bersih()
        UsernameTextBox.Text = ""
        PasswordTextBox.Text = ""
    End Sub
    Sub unlinked()
        Home.Button1.Enabled = True
        Home.Button2.Enabled = True
        Home.Button3.Enabled = True
        Home.Button4.Enabled = True
        Home.Button5.Enabled = True
    End Sub
    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Call koneksi()
        str = "SELECT * FROM tbmaster WHERE username='" & UsernameTextBox.Text & "' AND password='" & PasswordTextBox.Text & "'"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader()
        rd.Read()
        If Not rd.HasRows() Then
            MsgBox("Username atau password tidak diketahui")
            UsernameTextBox.Text = ""
            PasswordTextBox.Text = ""
        ElseIf rd.Item("status").ToString = "Users" Then
            Home.Show()
            Home.Button5.Enabled = True
            Home.Label1.Text = "Users"
            bersih()
            unlinked()
            Home.Button1.Text = "Absen"
            Home.Button1.Image = Image.FromFile("D:\Document\Data KP\Data KP\Absen RENEW\Absen RENEW\Image\id-card.png")
            Me.Hide()
        ElseIf rd.Item("status").ToString = "Admin" Then
            Home.Show()
            Form1.ComboBox1.Items.Add("Masuk")
            Form1.ComboBox1.Items.Add("Pulang")
            Form1.ComboBox1.Items.Add("Ijin")
            Form1.ComboBox1.Items.Add("Sakit")
            Home.Label1.Text = "Admin"
            Home.Button1.Text = "Master Data"
            Home.Button1.Image = Image.FromFile("D:\Document\Data KP\Data KP\Absen RENEW\Absen RENEW\Image\setting.png")
            Home.Button5.Enabled = True
            bersih()
            unlinked()
            Me.Hide()
        End If
    End Sub
End Class