
Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class BorrowingAndReturning
    Dim selected_book_id As Integer = 0
    Dim selected_book_stat As String = ""
    Dim selected_book_title As String = ""
    Dim selected_book_type As String = ""
    Shared date_time As DateTime = DateTime.Now
    Dim current_time As TimeSpan = DateTime.Now.TimeOfDay
    ReadOnly current_year As Integer = date_time.Year
    Private Shared ReadOnly rnd As New Random()
    Dim isAddingPanelVisible As Boolean = False

    'GENERATING ID'S 
    Dim initial_bor_id As Integer
    Dim borrower_id As Integer
    Dim initial_bor_trans_id As Integer
    Dim borrow_trans_id As Integer

    Private Sub BorrowingAndReturning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        LoadBooksList()
        LoadBorrowersList()
        LoadBorrowedBooksList()
        LoadReturnedBooksList()
        LoadOverDues()
        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        TxtTime.Text = TimeOfDay.ToString("h:mm:ss tt")
    End Sub



    '==============BOOKS==================
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



    '
    Private Sub BtnBooks_Click(sender As Object, e As EventArgs) Handles BtnBooks.Click
        TabControls.SelectedIndex = 0
        TabControls.SelectedTab = TabPage1
        BtnBooks.BackColor = Color.PowderBlue

        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
        LoadBooksList()
    End Sub


    'LOADING TO BORROW BOOKS TEMP STORAGE
    Private Sub LoadToBorrowTemp()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrow_books_temp ORDER BY books_count ASC"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DataGridView1.DataSource = dt
                        DataGridView1.Refresh()

                        DgvToBorrow.DataSource = dt
                        DgvToBorrow.Refresh()
                        For i = 0 To DataGridView1.Rows.Count - 1
                            DataGridView1.Rows(i).Height = 27
                        Next
                        DataGridView1.ClearSelection()
                        For i = 0 To DgvToBorrow.Rows.Count - 1
                            DgvToBorrow.Rows(i).Height = 70
                        Next

                        DgvToBorrow.ClearSelection()
                    Else
                        DataGridView1.DataSource = dt
                        DataGridView1.Refresh()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading to borrow Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'ADDING BOOKS TO BORROW INTO TEMPORARY STORAGE
    Dim isThereInternalBorrowSelected As Boolean = False
    Dim isThereAvailableBorrowSelected As Boolean = False
    Dim books_count As Integer = 1
    Private Sub AddToBorrow_Click(sender As Object, e As EventArgs) Handles AddToBorrow.Click

        If selected_book_id = 0 Then
            MessageBox.Show("Please Select Book to Add", "No Selected Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf selected_book_stat = "Unvailable" Then
            MessageBox.Show("This book is Unavailable right now", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf isThereInternalBorrowSelected And selected_book_stat = "Available" And books_count > 1 Then
            MessageBox.Show("Book with 'Internal Borrow Only' status is cannot be combined with other book 'Available'. | You can make another transaction instead.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf isThereAvailableBorrowSelected And selected_book_stat = "Internal Borrow Only" And books_count > 1 Then
            MessageBox.Show("Book with 'Internal Borrow Only' status is cannot be combined with other book 'Available'. | You can make another transaction instead.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            con.Close()
            Try
                con.Open()
                Dim check_query As String = "SELECT * FROM borrow_books_temp WHERE books_id=@id"
                Using cmd_check_query As New MySqlCommand(check_query, con)
                    cmd_check_query.Parameters.AddWithValue("@id", TxtBookId.Text.Trim)
                    Dim reader As MySqlDataReader = cmd_check_query.ExecuteReader()
                    If reader.HasRows Then
                        MessageBox.Show("You've Already selected this book", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        reader.Close()
                        Dim query As String = "
                            INSERT INTO borrow_books_temp
                                (`books_count`,`books_id`,`title`)
                            VALUES
                                (@books_count, @books_id, @title)
                            "
                        Using cmd As New MySqlCommand(query, con)
                            cmd.Parameters.AddWithValue("@books_count", books_count)
                            cmd.Parameters.AddWithValue("@books_id", TxtBookId.Text.Trim)
                            cmd.Parameters.AddWithValue("@title", TxtTitle.Text.Trim)
                            cmd.ExecuteNonQuery()
                        End Using
                        LoadToBorrowTemp()
                        books_count += 1
                        If selected_book_stat = "Internal Borrow Only" Then
                            isThereInternalBorrowSelected = True
                        End If
                        If selected_book_stat = "Available" Then
                            isThereAvailableBorrowSelected = True
                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred on Adding Books to Borrow", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If
    End Sub

    Private Sub DgvBooks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBooks.CellDoubleClick

        If selected_book_stat = "Unavailable" Then
            MessageBox.Show("This Book is unvailable right now", "Unavaiable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            PnlAddingToBorrow.Visible = True
            isAddingPanelVisible = True
            TxtBookId.Text = selected_book_id.ToString()
            TxtTitle.Text = selected_book_title
            If selected_book_stat = "Internal Borrow Only" Then
                TxtToBorrowType.Text = "Internal Borrow Only"
            Else
                TxtToBorrowType.Text = "Any"
            End If
        End If
    End Sub

    'HANDLES BOOKS TAB DATA GRID CLICK
    Private Sub DgvBooks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBooks.CellClick
        Dim i As Integer = DgvBooks.CurrentRow.Index
        selected_book_id = DgvBooks.Item(0, i).Value
        selected_book_title = DgvBooks.Item(1, i).Value
        selected_book_stat = DgvBooks.Item(5, i).Value
        BtnNext.Enabled = True
        If isAddingPanelVisible Then
            If selected_book_stat = "Unavailable" Then
                MessageBox.Show("This Book is unvailable right now", "Unavaiable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                PnlAddingToBorrow.Visible = True
                TxtBookId.Text = selected_book_id.ToString()
                TxtTitle.Text = selected_book_title
                If selected_book_stat = "Internal Borrow Only" Then
                    TxtToBorrowType.Text = "Internal Borrow Only"
                Else
                    TxtToBorrowType.Text = "Any"
                End If
            End If
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        GenerateBorrowerID()
        GenerateBorrowTransID()
        If selected_book_stat = "Internal Borrow Only" Then
            TxtType.ForeColor = Color.Maroon
            TabControls.SelectedTab = TabPage6
            BtnBooks.BackColor = Color.Transparent
            BtnBorrower.BackColor = Color.Transparent
            BtnBorrowedBooks.BackColor = Color.Transparent
            BtnReturnedBooks.BackColor = Color.Transparent
            BtnOverduesBooks.BackColor = Color.Transparent
            TxtType.Text = selected_book_stat
        ElseIf selected_book_stat = "Available" Then
            TxtType.ForeColor = Color.Green
            TabControls.SelectedTab = TabPage6
            BtnBooks.BackColor = Color.Transparent
            BtnBorrower.BackColor = Color.Transparent
            BtnBorrowedBooks.BackColor = Color.Transparent
            BtnReturnedBooks.BackColor = Color.Transparent
            BtnOverduesBooks.BackColor = Color.Transparent
            TxtType.Text = selected_book_stat
        End If
        TxtExistingBorrowerId.Focus()
    End Sub

    'TEXTBOX ENTERED BORROWER'S ID HANDLE TEXTCHANGED
    Private Sub TxtExistingBorrowerId_TextChanged(sender As Object, e As EventArgs) Handles TxtExistingBorrowerId.TextChanged
        If TxtExistingBorrowerId.Text <> "" Then
            If IsNumeric(TxtExistingBorrowerId.Text.Trim) Then
                Check_borrower_record()
            Else
                MessageBox.Show("Please enter 9-digit Borrower's ID. Ex.202402573", "Input Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Lbl1.Text = ""
                TxtExistingBorrowerId.Text = ""
            End If
        Else
            Lbl1.Text = ""
        End If

    End Sub

    'IF USER LEAVE THE BORROWER'S INPUT FIELD BALNKED
    Private Sub TxtExistingBorrowerId_Leave(sender As Object, e As EventArgs) Handles TxtExistingBorrowerId.Leave
        If Lbl1.Text = "No record found!" Then
            TxtExistingBorrowerId.Text = ""
            Lbl1.Text = ""
            TxtName.Text = ""
            TxtEmail.Text = ""
            TxtPhoneNo.Text = ""
            TxtAddress.Text = ""
        End If
    End Sub

    'CONFIRM BORROWING BUTTON
    Private Sub BtnConfirm_Click(sender As Object, e As EventArgs) Handles BtnConfirm.Click
        Save_borrowing_info()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PnlAddingToBorrow.Visible = False
        isAddingPanelVisible = False
    End Sub

    'HANDLE CELL CLICK ON TO BORROW TEMP DATAGRID
    Dim to_delete_temp_info As Integer = 0
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer = DataGridView1.CurrentRow.Index
        to_delete_temp_info = DataGridView1.Item(1, i).Value
        BtnRemoveTempInfo.Visible = True
    End Sub

    Private Sub BtnRemoveTempInfo_Click(sender As Object, e As EventArgs) Handles BtnRemoveTempInfo.Click

        Dim max_no As Integer = 0
        con.Close()
        Try
            con.Open()
            'delete 
            Using cmd1 As New MySqlCommand("DELETE FROM borrow_books_temp WHERE books_count=@count", con)
                cmd1.Parameters.AddWithValue("@count", to_delete_temp_info)
                cmd1.ExecuteNonQuery()
            End Using

            Using cmd2 As New MySqlCommand("SELECT ifNull(MAX(books_count),0) FROM borrow_books_temp", con)
                Dim reader As MySqlDataReader = cmd2.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        max_no = reader(0)
                    End If
                End If
                reader.Close()
            End Using

            Dim cntr As Integer = to_delete_temp_info
            Dim count_to_place As Integer = to_delete_temp_info
            While cntr <= max_no
                cntr += 1
                Using cmd3 As New MySqlCommand("UPDATE borrow_books_temp SET books_count=@count WHERE books_count=@cntr", con)
                    cmd3.Parameters.AddWithValue("@count", count_to_place)
                    cmd3.Parameters.AddWithValue("@cntr", cntr)
                    cmd3.ExecuteNonQuery()
                End Using
                count_to_place += 1
            End While

            books_count -= 1

            BtnRemoveTempInfo.Visible = False
            LoadToBorrowTemp()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on BtnRemoveTempInfo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        End Try
    End Sub





    '==============BORROWERS==================
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
                        For i = 0 To DgvBorrowers.Rows.Count - 1
                            DgvBorrowers.Rows(i).Height = 50
                        Next
                        DgvBorrowers.ClearSelection()
                    End If

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Borrowers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub BtnBorrower_Click(sender As Object, e As EventArgs) Handles BtnBorrower.Click
        TabControls.SelectedIndex = 1
        TabControls.SelectedTab = TabPage2
        BtnBorrower.BackColor = Color.PowderBlue
        BtnBooks.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
        LoadBorrowersList()
    End Sub

    'GENERATING BORROWER ID AND CHECKING UNIQUENESS
    Private Sub GenerateBorrowerID()
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
                    Dim query2 As String = "SELECT borrow_id FROM borrowed_books WHERE borrow_id=@id"
                    Using cmd2 As New MySqlCommand(query2, con)
                        cmd2.Parameters.AddWithValue("@id", current_year.ToString & initial_bor_id)
                        Dim count2 As Integer = Convert.ToInt32(cmd2.ExecuteScalar())
                        If count2 > 0 Then
                            GenerateBorrowerID()
                        Else
                            borrower_id = initial_bor_id
                            TxtGenaratedId.Text = current_year.ToString & borrower_id.ToString
                        End If
                    End Using
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 001: Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub

    'CHECKING BORROWER'S RECORD BASED ON ENTERED ID
    Private Sub Check_borrower_record()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowers WHERE borrower_id = @id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(TxtExistingBorrowerId.Text.Trim))
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        TxtName.Text = reader("name").ToString()
                        TxtEmail.Text = reader("email").ToString()
                        TxtPhoneNo.Text = reader("phone").ToString()
                        TxtAddress.Text = reader("address").ToString()
                        Lbl1.ForeColor = Color.Green
                        Lbl1.Text = "Record found"
                    Else
                        Lbl1.ForeColor = Color.Maroon
                        Lbl1.Text = "No record found!"
                        TxtName.Text = ""
                        TxtEmail.Text = ""
                        TxtPhoneNo.Text = ""
                        TxtAddress.Text = ""
                    End If
                Else

                    Lbl1.ForeColor = Color.Maroon
                    Lbl1.Text = "No record found!"
                    Lbl1.ForeColor = Color.Maroon
                    Lbl1.Text = "No record found!"
                    TxtName.Text = ""
                    TxtEmail.Text = ""
                    TxtPhoneNo.Text = ""
                    TxtAddress.Text = ""
                End If
                reader.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrower Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'SAVING BORROWER'S INFORMATION
    Private Sub Save_borrower_info()
        con.Close()

        Try
            con.Open()
            Dim query As String = "
                            INSERT INTO borrowers
                                (`borrower_id`,`name`,`email`,`phone`,`address`)
                            VALUES
                                (@id, @name, @email, @phone, @address)
                            "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(TxtGenaratedId.Text.Trim))
                cmd.Parameters.AddWithValue("@name", TxtName.Text.Trim)
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text.Trim)
                cmd.Parameters.AddWithValue("@phone", TxtPhoneNo.Text.Trim)
                cmd.Parameters.AddWithValue("@address", TxtAddress.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrower Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub





    '==============BORROWED BOOKS ============================
    Private Sub LoadBorrowedBooksList()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT 
                                        borrowed_books.*, scholarly_works.title
                                FROM borrowed_books 
                                LEFT JOIN scholarly_works 
                                    ON scholarly_works.sw_id=borrowed_books.book_id"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvBorrowed.DataSource = dt
                        DgvBorrowed.Refresh()
                        For i = 0 To DgvBorrowed.Rows.Count - 1
                            DgvBorrowed.Rows(i).Height = 50
                        Next
                        DgvBorrowed.ClearSelection()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Borrowers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnBorrowedBooks_Click(sender As Object, e As EventArgs) Handles BtnBorrowedBooks.Click
        TabControls.SelectedIndex = 2
        TabControls.SelectedTab = TabPage3
        BtnBorrowedBooks.BackColor = Color.PowderBlue
        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
        LoadBorrowedBooksList()
    End Sub

    'GENERATING BORROW TRANSACTION ID AND CHECKING UNIQUENESS
    Private Sub GenerateBorrowTransID()
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
                    GenerateBorrowTransID()
                Else
                    Dim query2 As String = "SELECT borrower_id FROM borrowers WHERE borrower_id=@id"
                    Using cmd2 As New MySqlCommand(query2, con)
                        cmd2.Parameters.AddWithValue("@id", current_year.ToString & initial_bor_trans_id)
                        Dim count2 As Integer = Convert.ToInt32(cmd2.ExecuteScalar())
                        If count2 > 0 Then
                            GenerateBorrowTransID()
                        Else
                            borrow_trans_id = initial_bor_trans_id
                            TxtBorrowTransId.Text = current_year.ToString & borrow_trans_id.ToString
                        End If
                    End Using
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 002: Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub

    'SAVING BORROW INFORMATION
    Private Sub Save_borrowing_info()
        con.Close()

        Try
            con.Open()
            Dim query As String = "
                            INSERT INTO borrowed_books
                                (`borrow_id`,`book_id`,`borrower_id`,`borrow_date`,`due_date`,`type`, `time`)
                            VALUES
                                (@borrow_trans_id, @book_id, @borrower_id, @date, @to_return_date, @type, @time)
                            "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@borrow_trans_id", Convert.ToInt64(TxtBorrowTransId.Text.Trim))
                cmd.Parameters.AddWithValue("@book_id", TxtBookId.Text.Trim)
                cmd.Parameters.AddWithValue("@date", TxtDate.Text.Trim)
                cmd.Parameters.AddWithValue("@to_return_date", DtDueDate.Value.Date.ToString("MM-dd-yyyy"))
                cmd.Parameters.AddWithValue("@type", TxtType.Text.Trim)
                cmd.Parameters.AddWithValue("@time", TxtTime.Text.Trim)

                If TxtExistingBorrowerId.Text = "" Then
                    cmd.Parameters.AddWithValue("@borrower_id", TxtGenaratedId.Text.Trim)
                Else
                    cmd.Parameters.AddWithValue("@borrower_id", TxtExistingBorrowerId.Text.Trim)
                End If
                cmd.ExecuteNonQuery()
            End Using

            If TxtExistingBorrowerId.Text = "" And Lbl1.Text <> "Record found" Then
                Save_borrower_info()
            End If

            MessageBox.Show("Successfully Saved Borrowing transaction at Borrowed Books", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBorrowedBooksList()
            LoadBorrowersList()
            LoadReturnedBooksList()
            LoadOverDues()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrower Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub





    '==============RETURNED BOOKS ==============================
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
                        For i = 0 To DgvReturned.Rows.Count - 1
                            DgvReturned.Rows(i).Height = 50
                        Next
                        DgvReturned.ClearSelection()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Returned Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnReturnedBooks_Click(sender As Object, e As EventArgs) Handles BtnReturnedBooks.Click
        TabControls.SelectedIndex = 3
        TabControls.SelectedTab = TabPage4
        BtnReturnedBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
        LoadReturnedBooksList()
    End Sub



    '==============OVERDUES===============================
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
                        For i = 0 To DgvOverdues.Rows.Count - 1
                            DgvOverdues.Rows(i).Height = 50
                        Next
                        DgvOverdues.ClearSelection()
                    End If

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading List of Overdues Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnOverduesBooks_Click(sender As Object, e As EventArgs) Handles BtnOverduesBooks.Click
        TabControls.SelectedIndex = 4
        TabControls.SelectedTab = TabPage5
        BtnOverduesBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        LoadOverDues()
    End Sub



End Class