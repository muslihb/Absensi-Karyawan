Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Public Class AbsenKaryawan
    Sub cekabsen()
        Dim data As String = "select count(*) from tbabsen where nip = '" & TextBox2.Text & "' and tgl_kerja = '" & Format(Now, "yyyy-MM-dd") & "'"
        cmd = New MySqlCommand(data, conn)
        Dim rsu As Integer = cmd.ExecuteScalar
        Label4.Text = rsu
    End Sub
    Private Sub AbsenKaryawan_Load(sender As Object, e As EventArgs) Handles Me.Load
        koneksi()
        GroupBox2.Visible = False
        cekabsen()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = Format(Now, "G")
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        ComboBox1.Text = ""
        PictureBox1.Image = Nothing
    End Sub
    Sub cek()
        koneksi()
        Try
            Dim gambar() As Byte
            str = "SELECT nip,nama_karyawan,jenis_kelamin,tempat_lahir,tgl_lahir,jabatan,foto FROM tbkaryawan WHERE nip='" _
            & Me.TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If rd.HasRows Then
                TextBox2.Text = rd!nip
                TextBox3.Text = rd!nama_karyawan
                TextBox4.Text = rd!jenis_kelamin
                TextBox5.Text = rd!tempat_lahir + ", " + Format(rd!tgl_lahir, "D")
                TextBox6.Text = rd!jabatan
                gambar = rd!foto
                Dim ms As New MemoryStream(gambar)
                PictureBox1.Image = Image.FromStream(ms)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                MsgBox("NIP tidak diketahui")
                TextBox1.Text = ""
                GroupBox2.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
        If e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Masuk" Then
            GroupBox2.Visible = True
            cek()
            Button1.Text = "In"
        ElseIf e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Pulang" Then
            GroupBox2.Visible = True
            cek()
            Button1.Text = "Out"
        End If
    End Sub
    Sub masuk()
        koneksi()
        Try
            str = "insert into tbabsen(no,nip,nama_karyawan,tgl_kerja,jam_masuk,status) values('" & Format(Now, "ddMMyyyyhhmmss") _
            & "','" & Me.TextBox2.Text _
            & "','" & Me.TextBox3.Text _
            & "','" & Format(Now, "yyyy-MM-dd") _
            & "','" & TimeOfDay _
            & "','" & Me.ComboBox1.Text _
            & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Welcome " + TextBox3.Text)
        Catch ex As Exception

        End Try
    End Sub
    Sub data()
        koneksi()
        Try
            str = "SELECT * FROM tbabsen WHERE nip='" & TextBox1.Text & "' AND tgl_kerja='" & Format(Now, "yyyy-MM-dd") & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
        Catch ex As Exception

        End Try
    End Sub
    Sub pulang()
        koneksi()
        Try
            data()
            If Not rd.HasRows Then
                MsgBox("Nip tidak terdaftar")
            ElseIf rd.Item("status").ToString = "Masuk" Then
                conn.Close()
                koneksi()
                str = "update tbabsen set jam_keluar ='" & TimeOfDay & "', status ='" & ComboBox1.Text _
                & "' where nip ='" & TextBox2.Text & "'"
                cmd = New MySqlCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("sampai Jumpai lagi " + TextBox3.Text)
            ElseIf rd.Item("status").ToString = "Pulang" Then
                MsgBox("status sudah pulang")
            Else
                MsgBox("Absen hari ini sudah selesai")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        cekabsen()
        If Not Label4.Text < 1 And Button1.Text = "In" Then
            MsgBox("Sudah tidak bisa melakukan Absensi hadir lagi hari ini" + TextBox3.Text, MsgBoxStyle.OkOnly)
            GroupBox2.Visible = False
            bersih()
            TextBox1.Enabled = False
        ElseIf Button1.Text = "In" And Label4.Text < 1 Then
            masuk()
            Label4.Text = Label4.Text + 1
            bersih()
            GroupBox2.Visible = False
            TextBox1.Enabled = False
        ElseIf Label4.Text = 0 And Button1.Text = "Out" Then
            MsgBox("Anda belum absen hari ini silakan masuk dulu " + TextBox3.Text, MsgBoxStyle.OkOnly)
            GroupBox2.Visible = False
            bersih()
            TextBox1.Enabled = False
        ElseIf Label4.Text = 1 And Button1.Text = "Out" Then
            pulang()
            bersih()
            GroupBox2.Visible = False
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class