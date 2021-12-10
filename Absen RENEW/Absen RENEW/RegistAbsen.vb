Imports MySql.Data.MySqlClient
Imports System.IO
Public Class RegistAbsen
    Sub bersih()
        TxtPhone.Text = ""
        ComboBox3.Text = ""
        TxtNip.Text = ""
        TxtNama.Text = ""
        TglLahir.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        TxtTL.Text = ""
        TxtEmail.Text = ""
        TxtAlamat.Text = ""
        PictureBox1.Image = Nothing
    End Sub
    Sub inputdata()
        koneksi()
        Try
            Dim simpan As String
            Dim ms As New MemoryStream
            PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
            simpan = "insert into tbkaryawan(nip,nama_karyawan,jenis_kelamin,tempat_lahir,tgl_lahir,jabatan,agama,email,no_telfon,alamat,foto) values('" & Me.TxtNip.Text _
                & "','" & Me.TxtNama.Text _
                & "','" & Me.ComboBox1.Text _
                & "','" & Me.TxtTL.Text _
                & "','" & Format(TglLahir.Value, "yyyy-MM-dd") _
                & "','" & Me.ComboBox2.Text _
                & "','" & Me.ComboBox3.Text _
                & "','" & Me.TxtEmail.Text _
                & "','" & Me.TxtPhone.Text _
                & "','" & Me.TxtAlamat.Text _
                & "',@foto)"
            cmd = New MySqlCommand(simpan, conn)
            cmd.Parameters.Add("@foto", MySqlDbType.LongBlob).Value = ms.ToArray
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Sub updatedata()
        TxtTL.Enabled = False
    End Sub
    Sub hapus()
        koneksi()
        Try
            Dim str As String
            str = "delete from tbkaryawan where nip = '" & TxtNip.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("data telah dihapus")
            Registrasi_Absen.TampilData()
            bersih()
        Catch ex As Exception
            MsgBox("data gagal dihapus")
        End Try
    End Sub
    Sub ubah()
        Call koneksi()
        Try
            Dim ms As New MemoryStream
            PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
            str = "UPDATE tbkaryawan SET nama_karyawan = '" & TxtNama.Text _
                    & "', jenis_kelamin = '" & ComboBox1.Text _
                    & "', tempat_lahir = '" & TxtTL.Text _
                    & "', tgl_lahir = '" & Format(TglLahir.Value, "yyyy-MM-dd") _
                    & "', jabatan = '" & ComboBox2.Text _
                    & "', agama = '" & ComboBox3.Text _
                    & "', email= '" & TxtEmail.Text _
                    & "', no_telfon = '" & TxtPhone.Text _
                    & "', alamat = '" & TxtAlamat.Text _
                    & "', foto = @img WHERE nip = '" & TxtNip.Text & "' "
            cmd = New MySqlCommand(str, conn)
            cmd.Parameters.Add("@img", MySqlDbType.LongBlob).Value = ms.ToArray
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("data telah di update")
        Catch ex As Exception

        End Try
    End Sub
    Sub buka()
        TxtPhone.ReadOnly = False
        TxtNip.ReadOnly = True
        TxtNama.ReadOnly = False
        TglLahir.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        TxtTL.ReadOnly = False
        TxtTL.ReadOnly = False
        Button3.Enabled = True
        ComboBox3.Enabled = True
        TxtEmail.ReadOnly = False
        TxtAlamat.ReadOnly = False
        Button3.Visible = True
    End Sub
    Sub tutup()
        TxtNip.ReadOnly = True
        TxtPhone.ReadOnly = True
        TxtNama.ReadOnly = True
        TglLahir.Enabled = False
        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        TxtTL.ReadOnly = True
        TxtTL.ReadOnly = True
        Button3.Enabled = False
        ComboBox3.Enabled = False
        TxtEmail.ReadOnly = True
        TxtAlamat.ReadOnly = True
        Button3.Visible = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Save" Then
            If TxtNama.Text = "" Or TxtNip.Text = "" Or TxtTL.Text = "" Or TxtAlamat.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Or ComboBox3.Text = "" Then
                MsgBox("ada data yang kosong")
            Else
                Try
                    inputdata()
                    Button1.Text = "Edit"
                    Button2.Text = "Exit"
                    tutup()
                Catch ex As Exception
                    MsgBox("Tolong masukan gambar")
                End Try
            End If
        ElseIf Button1.Text = "Edit" Then
            Button1.Text = "Update"
            Button2.Text = "Discard"
            TxtNip.ReadOnly = True
            buka()
        ElseIf Button1.Text = "Update" Then
            Try
                ubah()
                tutup()
                TxtNip.ReadOnly = True
                Button1.Text = "Edit"
                Button2.Text = "Exit"
            Catch ex As Exception
                MsgBox("data gagal di update")
            End Try
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Exit" Then
            bersih()
            Button1.Text = "Save"
            Button2.Text = "Discard"
            Button6.Visible = False
            buka()
            Registrasi_Absen.Button3.Visible = False
            Registrasi_Absen.TampilData()
            Home.inputpanel(Registrasi_Absen)
            Registrasi_Absen.Button1.Text = "Create"
        ElseIf Button2.Text = "Discard" Then
            tutup()
            Button1.Text = "Edit"
            Button2.Text = "Exit"
        End If
    End Sub
    Private Sub Huruf(e As KeyPressEventArgs)
        'Hanya huruf A-Z atau a-z dan tombol BackSpace, Delete, Space serta Enter
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[A-Z a-z]" _
            OrElse KeyAscii = Keys.Back _
            OrElse KeyAscii = Keys.Space _
            OrElse KeyAscii = Keys.Return _
            OrElse KeyAscii = Keys.Delete) Then
            KeyAscii = 0
        End If
        e.Handled = CBool(KeyAscii)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Pilih Foto" And Panelopsi.Visible = False Then
            Panelopsi.Visible = True
        Else
            Panelopsi.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ambilgambar.Show()
        Panelopsi.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog1.Filter = "Image File(*.PNG;*.JPG;*.GIF)|*.PNG;*.JPG;*.GIF|ALL Files (*.*) | *.*"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            Label13.Text = OpenFileDialog1.FileName
        End If
        Panelopsi.Visible = False
    End Sub
    Sub jabatan()
        koneksi()
        Try
            cmd = New MySqlCommand("select jabatan from jabatan order by id_jabatan", conn)
            rd = cmd.ExecuteReader
            While rd.Read
                ComboBox2.Items.Add(rd("jabatan"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub agama()
        koneksi()
        Try
            cmd = New MySqlCommand("select agama from tbagama order by id", conn)
            rd = cmd.ExecuteReader
            While rd.Read
                ComboBox3.Items.Add(rd("agama"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RegistAbsen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        buka()
        jabatan()
        agama()
    End Sub

    Private Sub TxtNip_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TxtTL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTL.KeyPress
        Huruf(e)
    End Sub

    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TxtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNama.KeyPress
        Huruf(e)
    End Sub
    Sub nomor()
        koneksi()
        Try
            Dim DR As DataRow
            Dim tanggal, gen, jabatan, s, agama As String
            tanggal = Format(TglLahir.Value, "ddMMyyyy")
            If ComboBox1.Text = "Laki - Laki" Then
                gen = "1"
            Else
                gen = "2"
            End If
            If ComboBox2.Text = "Manager" Then
                jabatan = "1"
            Else
                jabatan = "2"
            End If
            If ComboBox3.Text = "Islam" Then
                agama = "1"
            ElseIf ComboBox3.Text = "Protestan" Then
                agama = "2"
            ElseIf ComboBox3.Text = "Katolik" Then
                agama = "3"
            ElseIf ComboBox3.Text = "Hindu" Then
                agama = "4"
            ElseIf ComboBox3.Text = "Budha" Then
                agama = "5"
            Else
                agama = "6"
            End If
            'mengambil 4 karakter dari kanan (yg merupakan nomer) dari field ID, kemudian dicari nilai yg paling besar (max)
            'kemudian hasilnya d tampung d field buatan dgn nama Nomor
            DR = SQLTable("select max(right(nip,4)) as nomor from tbkaryawan").Rows(0)

            'jika berisi null atau tdk ditemukan
            If DR.IsNull("Nomor") Then
                s = tanggal + gen + jabatan + agama + "0001" 'member nilai awal
            Else
                s = tanggal + gen + jabatan + agama & Format(DR("Nomor") + 1, "0000")
            End If

            TxtNip.Text = s
            TxtNip.Enabled = False
            Button6.Visible = False
        Catch ex As Exception
            MsgBox("NIP gagal didapatkan")
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        nomor()
    End Sub

    Private Sub TxtNip_TextChanged(sender As Object, e As EventArgs) Handles TxtNip.TextChanged

    End Sub
End Class