
Imports MySql.Data.MySqlClient

Public Class BorrowingAndReturning
    Dim selected_book_id As Integer = 0
    Dim selected_book_stat As String = 0
    Dim selected_book_title As String = 0
    Dim selected_book_type As String = 0
    Dim date_time As DateTime = DateTime.Now
    Dim current_year As Integer = date_time.Year


    Private Sub BorrowingAndReturning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        LoadBooksList()
        LoadBorrowersList()
        LoadBorrowedBooksList()
        LoadReturnedBooksList()
        LoadOverDues()

        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        GenerateBorrowerID()
        GenerateBorrowTransID()
    End Sub

    'GENERATING BORROWER ID AND CHECKING UNIQUENESS
    Dim initial_bor_id As Integer
    Dim borrower_id As Integer
    Private Sub GenerateBorrowerID()
        Dim rnd As New Random()
        initial_bor_id = rnd.Next(10000, 99999)
        Isinitial_bor_idUnique()
    End Sub

    Private Sub Isinitial_bor_idUnique()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT borrower_id FROM borrowers WHERE borrower_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & initial_bor_id)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateBorrowerID()
                Else
                    borrower_id = initial_bor_id
                    TxtGenaratedId.Text = current_year.ToString & borrower_id.ToString
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 001: Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub


    'GENERATING BORROW TRANSACTION ID AND CHECKING UNIQUENESS
    Dim initial_bor_trans_id As Integer
    Dim borrow_trans_id As Integer
    Private Sub GenerateBorrowTransID()
        Dim rnd As New Random()
        initial_bor_trans_id = rnd.Next(10000, 99999)
        Isinitial_bor_trans_id()
    End Sub

    Private Sub Isinitial_bor_trans_id()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT borrow_id FROM borrowed_books WHERE borrow_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & initial_bor_trans_id)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateBorrowerID()
                Else
                    borrow_trans_id = initial_bor_trans_id
                    TxtBorrowTransId.Text = current_year.ToString & borrow_trans_id.ToString
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 002: Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub



    '==============BOOKS
    Private Sub LoadBooksList()
        con.Close()

        Try
            con.Open()

            Dim query As String = "
                        SELECT 
                            scholarly_works.sw_id,
                            scholarly_works.title, 
                            authors.authors_name,
                            published_details.date_published,
                            qnty_loc.quantity,
                            CASE
                                    WHEN quantity < 1 THEN 'Unavailable'
                                    WHEN quantity = 1 THEN 'Internal Borrow Only'
                                    WHEN quantity > 1 THEN 'Available'
                            END AS quantity_stat

                        FROM scholarly_works

                        INNER JOIN authors 
                            ON authors.authors_id = scholarly_works.sw_id

                        LEFT JOIN published_details 
                            ON published_details.published_id = scholarly_works.sw_id

                        LEFT JOIN qnty_loc 
                            ON qnty_loc.sw_id = scholarly_works.sw_id

                            "
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvBooks.DataSource = dt
                    DgvBooks.Refresh()
                    For i = 0 To DgvBooks.Rows.Count - 1
                        DgvBooks.Rows(i).Height = 50
                    Next
                    DgvBooks.ClearSelection()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading Book List", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try

    End Sub



    '==============BORROWERS
    Private Sub LoadBorrowersList()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowers"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvBorrowers.DataSource = dt
                        DgvBorrowers.Refresh()
                    Else
                        DgvBorrowers.Rows.Add("No data")
                    End If

                    For i = 0 To DgvBorrowers.Rows.Count - 1
                        DgvBorrowers.Rows(i).Height = 50
                    Next
                    DgvBorrowers.ClearSelection()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Borrowers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try

    End Sub



    '==============BORROWED BOOKS
    Private Sub LoadBorrowedBooksList()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowed_books"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvBorrowed.DataSource = dt
                        DgvBorrowed.Refresh()
                    Else
                        DgvBorrowed.Rows.Add("No data")
                    End If

                    For i = 0 To DgvBorrowed.Rows.Count - 1
                        DgvBorrowed.Rows(i).Height = 50
                    Next
                    DgvBorrowed.ClearSelection()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Borrowers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub



    '==============RETURNED BOOKS
    Private Sub LoadReturnedBooksList()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM returned_books"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvReturned.DataSource = dt
                        DgvReturned.Refresh()
                    Else
                        DgvReturned.Rows.Add("No data")
                    End If

                    For i = 0 To DgvReturned.Rows.Count - 1
                        DgvReturned.Rows(i).Height = 50
                    Next
                    DgvReturned.ClearSelection()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Returned Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub



    '==============OVERDUES
    Private Sub LoadOverDues()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM overdues"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvOverdues.DataSource = dt
                        DgvOverdues.Refresh()
                    Else
                        DgvOverdues.Rows.Add("No data")
                    End If

                    For i = 0 To DgvOverdues.Rows.Count - 1
                        DgvOverdues.Rows(i).Height = 50
                    Next
                    DgvOverdues.ClearSelection()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Overdues Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub


    '
    Private Sub BtnBooks_Click(sender As Object, e As EventArgs) Handles BtnBooks.Click
        TabControls.SelectedIndex = 0
        TabControls.SelectedTab = TabPage1
        BtnBooks.BackColor = Color.PowderBlue

        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
    End Sub

    Private Sub BtnBorrower_Click(sender As Object, e As EventArgs) Handles BtnBorrower.Click
        TabControls.SelectedIndex = 1
        TabControls.SelectedTab = TabPage2
        BtnBorrower.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
    End Sub

    Private Sub BtnBorrowedBooks_Click(sender As Object, e As EventArgs) Handles BtnBorrowedBooks.Click
        TabControls.SelectedIndex = 2
        TabControls.SelectedTab = TabPage3
        BtnBorrowedBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
    End Sub

    Private Sub BtnReturnedBooks_Click(sender As Object, e As EventArgs) Handles BtnReturnedBooks.Click
        TabControls.SelectedIndex = 3
        TabControls.SelectedTab = TabPage4
        BtnReturnedBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
    End Sub

    Private Sub BtnOverduesBooks_Click(sender As Object, e As EventArgs) Handles BtnOverduesBooks.Click
        TabControls.SelectedIndex = 4
        TabControls.SelectedTab = TabPage5
        BtnOverduesBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
    End Sub


    'HANDLES BOOKS TAB DATA GRID CLICK
    Private Sub DgvBooks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBooks.CellClick
        Dim i As Integer = DgvBooks.CurrentRow.Index
        selected_book_id = DgvBooks.Item(0, i).Value
        selected_book_title = DgvBooks.Item(1, i).Value
        selected_book_stat = DgvBooks.Item(5, i).Value
        BtnBorrow.Enabled = True
    End Sub

    Dim borrow_type As String = ""
    Private Sub BtnBorrow_Click(sender As Object, e As EventArgs) Handles BtnBorrow.Click

        If selected_book_id = 0 Then
            MsgBox("No Selected")
        ElseIf selected_book_stat = "Unvailable" Then
            MsgBox("Book is Unavailable")

        ElseIf selected_book_stat = "Internal Borrow Only" Then
            MsgBox("Internal Borrow Only")
            borrow_type = "Internal Borrow Only"

            TabControls.SelectedTab = TabPage6
            BtnBooks.BackColor = Color.Transparent
            BtnBorrower.BackColor = Color.Transparent
            BtnBorrowedBooks.BackColor = Color.Transparent
            BtnReturnedBooks.BackColor = Color.Transparent
            BtnOverduesBooks.BackColor = Color.Transparent

            TxtBookId.Text = selected_book_id.ToString()
            TxtTitle.Text = selected_book_title
            TxtType.Text = borrow_type


        ElseIf selected_book_stat = "Available" Then
            MsgBox("Available")
            borrow_type = "Any"

            TabControls.SelectedTab = TabPage6
            BtnBooks.BackColor = Color.Transparent
            BtnBorrower.BackColor = Color.Transparent
            BtnBorrowedBooks.BackColor = Color.Transparent
            BtnReturnedBooks.BackColor = Color.Transparent
            BtnOverduesBooks.BackColor = Color.Transparent

            TxtBookId.Text = selected_book_id.ToString()
            TxtTitle.Text = selected_book_title
            TxtType.Text = borrow_type
        End If

        TxtExistingBorrowerId.Focus()
    End Sub

    'CONFIRM BORROWING
    Private Sub BtnConfirm_Click(sender As Object, e As EventArgs) Handles BtnConfirm.Click

    End Sub
End Class