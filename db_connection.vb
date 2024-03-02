Imports MySql.Data.MySqlClient

Module db_connection
    Public con As New MySqlConnection
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public ds As New DataSet
    Public dt As New DataTable

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = ""
    Dim database As String = "cpri_cdsga_db"



    Public Sub ConOpen()

        If con.State = System.Data.ConnectionState.Open Then con.Close()

        Try
            con.ConnectionString = "server='" & server & "';username='" & username & "';password='" & password & "';database='" & database & "'"
            con.Open()
            ' MessageBox.Show("Connection Stablished", "Successfully Connected to the Database", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Can't Connect to the Database.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Module
