
Module public_variables
    Public pdfId As Integer
    Public isEditModeActive As Boolean = False
    Public iscloseUsingWinControl As Boolean = True
    Public on_edit_mode As Integer
    Public print_clearance_id As Integer
    Public to_view_work_id As Integer

    Public account_loggedin As String
    Public account_type_loggedin As String
    Public loggedin As Integer = 0
    Public loggedin_id As Integer = 0

    Public isForm1Closed As Boolean = False

    Public Function MD5(ByVal sPassword As String) As String
        Dim p As New Security.Cryptography.MD5CryptoServiceProvider()
        Dim bs As Byte() = Text.Encoding.UTF8.GetBytes(sPassword)
        bs = p.ComputeHash(bs)
        Dim s As New Text.StringBuilder()
        For Each b As Byte In bs
            s.Append(b.ToString("x²").ToLower())
        Next
        Return s.ToString()
    End Function
End Module
