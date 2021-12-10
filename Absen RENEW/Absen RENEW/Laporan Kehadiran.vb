Imports MySql.Data.MySqlClient
Public Class Laporan_Kehadiran
    Sub tampilData()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub withName()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen where nip like '%" _
        & Me.TextBox1.Text & "%'", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub withNameHari()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen where nip like '%" _
        & Me.TextBox1.Text & "%' And tgl_kerja like '%" _
        & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "%' ", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub Hari()
        koneksi()
        Try
            da = New MySqlDataAdapter("Select nama_karyawan, tgl_kerja, jam_masuk, jam_keluar, status from tbabsen where tgl_kerja Like '%" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "%'", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub withNametanggal()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen where nip like '%" _
        & Me.TextBox1.Text & "%' And tgl_kerja between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND  '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' ", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub tanggal()
        koneksi()
        Try
            da = New MySqlDataAdapter("select nama_karyawan,tgl_kerja,jam_masuk,jam_keluar,status from tbabsen where tgl_kerja between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND  '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' ", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbabsen")
            Me.DataGridView1.DataSource = (ds.Tables("tbabsen"))
        Catch ex As Exception

        End Try
    End Sub
    Sub bersih()
        DateTimePicker1.Visible = False
        DateTimePicker2.Visible = False
        Label1.Visible = False
    End Sub
    Sub pilih()
        DateTimePicker1.Visible = True
        DateTimePicker2.Visible = True
        Label1.Visible = True
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "All" Then
            bersih()
            tampilData()
        ElseIf ComboBox1.Text = "Day" Then
            bersih()
            DateTimePicker1.Visible = True
        ElseIf ComboBox1.Text = "Selection" Then
            pilih()
            tanggal()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = "Day" And DateTimePicker1.Visible = True And DateTimePicker2.Visible = False And TextBox1.Text = "" Then
            Hari()
        ElseIf ComboBox1.Text = "Day" And DateTimePicker1.Visible = True And DateTimePicker2.Visible = False And Not TextBox1.Text = "" Then
            withNameHari()
        ElseIf ComboBox1.Text = "Selection" And DateTimePicker2.Visible = True And TextBox1.Text = "" Then
            tanggal()
        ElseIf ComboBox1.Text = "Selection" And DateTimePicker2.Visible = True And Not TextBox1.Text = "" Then
            withNametanggal()
        ElseIf ComboBox1.Text = "All" And TextBox1.Text = "" Then
            tampilData()
        ElseIf ComboBox1.Text = "All" And Not TextBox1.Text = "" Then
            tampilData()
            withName()
        End If
    End Sub
End Class