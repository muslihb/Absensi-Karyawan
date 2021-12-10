Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV()
    End Sub
    Sub Cekjumlah()
        Dim data As String = "select count(*) from tbabsen where nip = '" & TextBox1.Text & "' and tgl_kerja = '" & Format(Now, "yyyy-MM-dd") & "'"
        cmd = New MySqlCommand(data, conn)
        Dim rsu As Integer = cmd.ExecuteScalar
        Label2.Text = rsu
    End Sub
    Sub DGV()
        DataGridView1.Columns(0).HeaderText = "AbsenKaryawan.TextBox3.Text Karyawan"
        DataGridView1.Columns(1).HeaderText = "Tanggal"
        DataGridView1.Columns(2).HeaderText = "Jam Mulai"
        DataGridView1.Columns(3).HeaderText = "Jam Pulang"
        DataGridView1.Columns(4).HeaderText = "Status"
    End Sub
    Sub TampilData()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen where tgl_kerja like '%" & Format(Now, "yyyy-MM-dd") & "%'", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub masuk()
        koneksi()
        Try
            str = "insert into tbabsen(no,nip,nama_karyawan,tgl_kerja,jam_masuk,status) values('" & Format(Now, "ddMMyyyyhhmmss") _
    & "','" & TextBox1.Text _
    & "','" & AbsenKaryawan.TextBox3.Text _
    & "','" & Format(Now, "yyyy-MM-dd") _
    & "','" & TimeOfDay _
    & "','" & Me.ComboBox1.Text _
    & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Sub TidakHadir()
        Try
            str = "insert into tbabsen(no,nip,nama_karyawan,tgl_kerja,status) values('" & Format(Now, "ddMMyyyyhhmmss") _
    & "','" & TextBox1.Text _
    & "','" & AbsenKaryawan.TextBox3.Text _
    & "','" & Format(Now, "yyyy-MM-dd") _
    & "','" & Me.ComboBox1.Text _
    & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = Format(Now, "G")
    End Sub
    Sub cekData()
        Call koneksi()
        Try
            str = "SELECT nip,nama_karyawan,jenis_kelamin,tempat_lahir,tgl_lahir,jabatan,foto FROM tbkaryawan WHERE nip='" _
        & Me.TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If rd.HasRows Then
                AbsenKaryawan.TextBox3.Text = rd!nama_karyawan
                Button3.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub keluar()
        koneksi()
        Try
            str = "SELECT * FROM tbabsen WHERE nip='" & TextBox1.Text & "' AND tgl_kerja='" & Format(Now, "yyyy-MM-dd") & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Nip tidak terdaftar")
            ElseIf rd.Item("status").ToString = "Masuk" Then
                conn.Close()
                koneksi()
                str = "update tbabsen set jam_keluar ='" & TimeOfDay & "', status ='" & ComboBox1.Text _
                & "' where nip ='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("sampai Jumpai lagi " + AbsenKaryawan.TextBox3.Text)
                TextBox1.Enabled = False
            ElseIf rd.Item("status").ToString = "Pulang" Then
                MsgBox("status sudah pulang")
            Else
                MsgBox("Absen hari ini sudah selesai")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
        If e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Masuk" Then
            cekData()
            Button3.Text = "In"
            Button3.Enabled = True
        ElseIf e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Pulang" Then
            cekData()
            Button3.Text = "Out"
            Button3.Enabled = True
        ElseIf e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Ijin" Then
            cekData()
            Button3.Text = "Simpan"
            Button3.Enabled = True
        ElseIf e.KeyChar = ChrW(Keys.Enter) And ComboBox1.Text = "Sakit" Then
            cekData()
            Button3.Text = "Simpan"
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        koneksi()
        Cekjumlah()
        If Not Label2.Text < 1 And Button3.Text = "In" Then
            MsgBox("Sudah tidak bisa melakukan Absensi hadir lagi hari ini" + AbsenKaryawan.TextBox3.Text, MsgBoxStyle.OkOnly)
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        ElseIf Button3.Text = "In" And Label2.Text < 1 Then
            masuk()
            MsgBox("Welcome " + AbsenKaryawan.TextBox3.Text)
            Label2.Text = Label2.Text + 1
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        ElseIf Label2.Text = 0 And Button3.Text = "Out" Then
            MsgBox("Anda belum absen hari ini silakan masuk dulu " + AbsenKaryawan.TextBox3.Text, MsgBoxStyle.OkOnly)
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        ElseIf Label2.Text = 1 And Button3.Text = "Out" Then
            keluar()
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        ElseIf Not Label2.Text < 1 And Button3.Text = "Simpan" Then
            MsgBox("data tidak tersimpan ", MsgBoxStyle.OkOnly)
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        ElseIf Label2.Text < 1 And Button3.Text = "Simpan" Then
            TidakHadir()
            TextBox1.Enabled = False
            TextBox1.Text = ""
            Button3.Enabled = False
            TampilData()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Enabled = True
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
