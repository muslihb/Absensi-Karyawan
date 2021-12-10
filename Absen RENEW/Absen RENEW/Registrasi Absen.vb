Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Public Class Registrasi_Absen
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Home.inputpanel(RegistAbsen)
        RegistAbsen.bersih()
        RegistAbsen.Button6.Visible = True
    End Sub
    Sub TampilData()
        koneksi()
        Try
            da = New MySqlDataAdapter("select * from tbkaryawan", conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "tbkaryawan")
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.RowTemplate.Height = 30
            Dim img As New DataGridViewImageColumn()
            Me.DataGridView1.DataSource = (ds.Tables("tbkaryawan"))
            img = DataGridView1.Columns(10)
            img.ImageLayout = DataGridViewImageCellLayout.Stretch
        Catch ex As Exception

        End Try
    End Sub
    Sub LoadGambar()
        koneksi()
        Try
            RegistAbsen.PictureBox1.Image = Nothing
            str = "SELECT foto FROM tbkaryawan where nip = '" & RegistAbsen.TxtNip.Text & "'"
            Dim cmd As New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader

            While rd.Read()
                Dim Byted() As Byte = rd.Item("foto")
                Dim Streamed As New MemoryStream(Byted)
                RegistAbsen.PictureBox1.Image = Image.FromStream(Streamed)
                Streamed.Close()
            End While
            rd.Close()
            cmd.Dispose()
        Catch ex As Exception
            MessageBox.Show("There was an error: " & ex.Message, "Error",
             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Registrasi_Absen_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV()
    End Sub
    Sub DGV()
        DataGridView1.Columns(0).HeaderText = "NIP"
        DataGridView1.Columns(1).HeaderText = "Nama Karyawan"
        DataGridView1.Columns(2).HeaderText = "Jenis Kelamin"
        DataGridView1.Columns(3).HeaderText = "Tempat Lahir"
        DataGridView1.Columns(4).HeaderText = "Tanggal Lahir"
        DataGridView1.Columns(5).HeaderText = "Jabatan"
        DataGridView1.Columns(6).HeaderText = "Agama"
        DataGridView1.Columns(7).HeaderText = "Email"
        DataGridView1.Columns(8).HeaderText = "No Telfon"
        DataGridView1.Columns(9).HeaderText = "Alamat"
        DataGridView1.Columns(10).HeaderText = "Foto"

    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Try
            RegistAbsen.bersih()
            RegistAbsen.TxtNip.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            RegistAbsen.TxtNama.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            RegistAbsen.ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            RegistAbsen.TxtTL.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            RegistAbsen.TglLahir.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            RegistAbsen.ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            RegistAbsen.ComboBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            RegistAbsen.TxtEmail.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
            RegistAbsen.TxtPhone.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
            RegistAbsen.TxtAlamat.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value
            LoadGambar()
            RegistAbsen.Button1.Text = "Update"
            Home.inputpanel(RegistAbsen)
            RegistAbsen.TxtNip.ReadOnly = True
            RegistAbsen.Button6.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RegistAbsen.hapus()
        Button3.Visible = False
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            RegistAbsen.bersih()
            RegistAbsen.TxtNip.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            Button3.Visible = True
        Catch ex As Exception

        End Try
    End Sub
End Class