Imports MySql.Data.MySqlClient
Module Module1
    Public conn As MySqlConnection  ' untuk menkoneksikan dengan database
    Public da As MySqlDataAdapter  ' untuk menkonesikkan dengan table
    Public ds As DataSet            ' unruk menkoneksikan dengan table dan field
    Public cmd As MySqlCommand      ' untuk membuak tombol koneksi
    Public rd As MySqlDataReader    ' untuk nanti pencarian
    Public str As String
    Public user As String
    Public status As String
    Sub koneksi()
        str = "server=localhost; username=root; password=; database=absen"
        conn = New MySqlConnection(str)
        If conn.State = ConnectionState.Closed Then conn.Open()
        'end if
    End Sub
    Public Function SQLTable(ByVal Source As String) As DataTable
        ' ---fungsi untuk membuat nomor otomatis dengan menghubungkan pada field yang ada di table---
        Try
            Dim Adp As New MySqlDataAdapter(Source, conn)
            Dim DT As New DataTable

            Adp.Fill(DT)
            SQLTable = DT
        Catch ex As MySqlException
            MsgBox(ex.Message)
            SQLTable = Nothing
        End Try
    End Function
End Module