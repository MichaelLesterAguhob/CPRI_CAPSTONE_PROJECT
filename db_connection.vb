Imports MySql.Data.MySqlClient

Module db_connection
    Public con As New MySqlConnection
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public ds As New DataSet
    Public dt As New DataTable

    Private ReadOnly server As String = "localhost"
    Private ReadOnly username As String = "root"
    Private ReadOnly password As String = ""
    Private ReadOnly database As String = "cpri_cdsga_db"

    Public Sub ConOpen()

        If con.State = System.Data.ConnectionState.Open Then con.Close()

        Try
            con.ConnectionString = "server=" & server & ";user=" & username & ";password=" & password & ";database=" & database
            con.Open()
            ' MessageBox.Show("Connection Stablished", "Successfully Connected to the Database", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Can't Connect to the Database.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Module
