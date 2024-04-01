
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

    'Borrower variables
    Dim selected_borrower_id As Integer = 0
    Dim selected_borrower_name As String = ""
    Private Sub BorrowingAndReturning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        LoadBooksList()
        LoadBorrowersList()
        LoadBorrowedBooksList()
        LoadReturnedBooksList()
        LoadOverDues()
        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        Timer1.Start()
        BtnBooks.Focus()
    End Sub

    Private Sub BorrowingAndReturning_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        EmptyTempStorage()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TxtTime.Text = TimeOfDay.ToString("h:mm:ss tt")
    End Sub
    Private Sub DtDueDate_ValueChanged(sender As Object, e As EventArgs) Handles DtDueDate.ValueChanged
        If DtDueDate.Value < DateTime.Today Then
            DtDueDate.Value = DateTime.Today
        End If
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


    Private Sub EmptyTempStorage()
        'clearing data in temporary storage
        con.Close()
        Try
            con.Open()
            'delete 
            Using cmd As New MySqlCommand("DELETE FROM borrow_books_temp", con)
                cmd.Parameters.AddWithValue("@count", to_delete_temp_info)
                cmd.ExecuteNonQuery()
            End Using
            LoadToBorrowTemp()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred while deleting temp data | form closing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
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
                        For i = 0 To DgvToBorrow.Rows.Count - 1
                            DgvToBorrow.Rows(i).Height = 50
                        Next

                        DataGridView1.ClearSelection()
                        DgvToBorrow.ClearSelection()
                    Else
                        DataGridView1.DataSource = dt
                        DataGridView1.Refresh()
                        DgvToBorrow.DataSource = dt
                        DgvToBorrow.Refresh()
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
                selected_book_stat = ""
                selected_book_id = 0
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
        If books_count <> 1 Then

            If TxtToBorrowType.Text = "Internal Borrow Only" Then
                GenerateBorrowerID()
                GenerateBorrowTransID()

                TxtType.ForeColor = Color.Maroon
                TabControls.SelectedTab = TabPage6
                BtnBooks.BackColor = Color.Transparent
                BtnBorrower.BackColor = Color.Transparent
                BtnBorrowedBooks.BackColor = Color.Transparent
                BtnReturnedBooks.BackColor = Color.Transparent
                BtnOverduesBooks.BackColor = Color.Transparent
                TxtType.Text = selected_book_stat

                LoadToBorrowTemp()
                TxtExistingBorrowerId.Focus()
                TxtBookId.Clear()
                TxtTitle.Clear()
                TxtToBorrowType.Clear()
                selected_book_id = 0
            ElseIf TxtToBorrowType.Text = "Any" Then
                GenerateBorrowerID()
                GenerateBorrowTransID()

                TxtType.ForeColor = Color.Green
                TabControls.SelectedTab = TabPage6
                BtnBooks.BackColor = Color.Transparent
                BtnBorrower.BackColor = Color.Transparent
                BtnBorrowedBooks.BackColor = Color.Transparent
                BtnReturnedBooks.BackColor = Color.Transparent
                BtnOverduesBooks.BackColor = Color.Transparent
                TxtType.Text = selected_book_stat

                LoadToBorrowTemp()
                TxtExistingBorrowerId.Focus()
                TxtBookId.Clear()
                TxtTitle.Clear()
                TxtToBorrowType.Clear()
                selected_book_id = 0
            Else
                selected_book_stat = ""
                selected_book_id = 0
            End If

        End If

    End Sub

    'TEXTBOX ENTERED BORROWER'S ID HANDLE TEXTCHANGED
    Private Sub TxtExistingBorrowerId_TextChanged(sender As Object, e As EventArgs) Handles TxtExistingBorrowerId.TextChanged
        If TxtExistingBorrowerId.Text.Trim <> "" Then
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
        If Lbl1.Text.Trim = "No record found!" Then
            TxtExistingBorrowerId.Text = ""
            Lbl1.Text = ""
            TxtName.Text = ""
            TxtEmail.Text = ""
            TxtPhoneNo.Text = ""
            TxtAddress.Text = ""
        End If
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

    'HANDLE CELL CLICK ON BORROWING FORM DATAGRID TAB
    Private Sub DgvToBorrow_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvToBorrow.CellClick
        Dim i As Integer = DgvToBorrow.CurrentRow.Index
        to_delete_temp_info = DgvToBorrow.Item(1, i).Value
        BtnRemoveToBorrow.Visible = True
    End Sub

    'BUTTON REMOVE CLICK ON BORROWING FORM TAB
    Dim isBrtbClicked As Boolean = False
    Private Sub BtnRemoveToBorrow_Click(sender As Object, e As EventArgs) Handles BtnRemoveToBorrow.Click
        isBrtbClicked = True
        RemoveToBorrow()
    End Sub

    'BUTTON REMOVE CLICK ON BOOK TAB
    Private Sub BtnRemoveTempInfo_Click(sender As Object, e As EventArgs) Handles BtnRemoveTempInfo.Click
        RemoveToBorrow()
    End Sub

    'CODE TO REMOVE TEMP DATA SELECTED
    Private Sub RemoveToBorrow()
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
            BtnRemoveToBorrow.Visible = False
            LoadToBorrowTemp()

            If max_no = 0 And isBrtbClicked Then
                BtnBooks.PerformClick()
                selected_book_id = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on BtnRemoveTempInfo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        End Try
    End Sub

    Private Sub BtnCancelAddedToBorrow_Click(sender As Object, e As EventArgs) Handles BtnCancelAddedToBorrow.Click
        Dim confirmation As DialogResult = MessageBox.Show("Cancel Borrowing? All books added to will be removed.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
            EmptyTempStorage()
            Button7.PerformClick()
            books_count = 1
        End If
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
                            TxtAddBorId.Text = current_year.ToString & borrower_id.ToString
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

    'ADDING BORROWERS
    Private Sub BtnGoToAddingBorrower_Click(sender As Object, e As EventArgs) Handles BtnGoToAddingBorrower.Click
        GenerateBorrowerID()
        TabControls.SelectedIndex = 7
        TabControls.SelectedTab = TabPage8

        TxtEditId.Visible = False
        TxtEditName.Visible = False
        TxtEditEmail.Visible = False
        TxtEditAddress.Visible = False
        TxtEditPhone.Visible = False
        BtnUpdateBorDetails.Visible = False
        Label19.Text = "ADD BORROWER"
    End Sub
    Private Sub BtnCancelAddingBorrower_Click(sender As Object, e As EventArgs) Handles BtnCancelAddingBorrower.Click
        BtnBorrower.PerformClick()
    End Sub

    Private Sub BtnAddBorrowerInfo_Click(sender As Object, e As EventArgs) Handles BtnAddBorrowerInfo.Click
        If TxtAddBorName.Text.Trim = "" Or TxtAddBorEmail.Text.Trim = "" Or TxtAddBorAddress.Text.Trim = "" Or TxtAddBorPhone.Text.Trim = "" Or TxtAddBorId.Text.Trim = "" Then
            MessageBox.Show("Fill in the blank(s)", "No Input(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            AddBorrower()
        End If
    End Sub

    Private Sub AddBorrower()
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
                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(TxtAddBorId.Text.Trim))
                cmd.Parameters.AddWithValue("@name", TxtAddBorName.Text.Trim)
                cmd.Parameters.AddWithValue("@email", TxtAddBorEmail.Text.Trim)
                cmd.Parameters.AddWithValue("@phone", TxtAddBorPhone.Text.Trim)
                cmd.Parameters.AddWithValue("@address", TxtAddBorAddress.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
            BtnBorrower.PerformClick()
            MessageBox.Show("Successfully added borrower details", "Successfully Added", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Adding Borrower Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'CLICKING ON BORROWERS RECORD
    Dim selected_borrower_email As String = ""
    Dim selected_borrower_address As String = ""
    Dim selected_borrower_phone As String = ""
    Private Sub DgvBorrowers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBorrowers.CellClick
        Dim i As Integer = DgvBorrowers.CurrentRow.Index
        selected_borrower_id = DgvBorrowers.Item(0, i).Value
        selected_borrower_name = DgvBorrowers.Item(1, i).Value
        selected_borrower_email = DgvBorrowers.Item(2, i).Value
        selected_borrower_phone = DgvBorrowers.Item(3, i).Value
        selected_borrower_address = DgvBorrowers.Item(4, i).Value

        'MsgBox(selected_borrower_id)
        BtnEditBorrower.Enabled = True
        BtnDeleteBorrower.Enabled = True
    End Sub

    Private Sub BtnEditBorrower_Click(sender As Object, e As EventArgs) Handles BtnEditBorrower.Click
        If selected_borrower_id = 0 Then
            MessageBox.Show("Please select a record first", "No selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            TabControls.SelectedIndex = 7
            TabControls.SelectedTab = TabPage8

            TxtEditId.Text = selected_borrower_id
            TxtEditName.Text = selected_borrower_name
            TxtEditEmail.Text = selected_borrower_email
            TxtEditAddress.Text = selected_borrower_phone
            TxtEditPhone.Text = selected_borrower_address
            TxtEditId.Visible = True
            TxtEditName.Visible = True
            TxtEditEmail.Visible = True
            TxtEditAddress.Visible = True
            TxtEditPhone.Visible = True
            BtnUpdateBorDetails.Visible = True
            Label19.Text = "EDIT BORROWER DETAILS"
        End If

        'for edit
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
    End Sub

    'UPDATING BORROWER'S DETAILS
    Private Sub BtnUpdateBorDetails_Click(sender As Object, e As EventArgs) Handles BtnUpdateBorDetails.Click
        If TxtEditName.Text.Trim = "" Or TxtEditEmail.Text.Trim = "" Or TxtEditAddress.Text.Trim = "" Or TxtEditPhone.Text.Trim = "" Then
            MessageBox.Show("Fill in the blank(s)", "No Input(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            UpdateEditedBorrowerDetails()
        End If

    End Sub

    Private Sub UpdateEditedBorrowerDetails()
        con.Close()
        Try
            con.Open()
            Using cmd As New MySqlCommand("
                    UPDATE borrowers 
                    SET 
                        name=@name, 
                        email=@email, 
                        phone=@phone, 
                        address=@address 
                    WHERE 
                        borrower_id=@id", con)
                cmd.Parameters.AddWithValue("@id", TxtEditId.Text.Trim)
                cmd.Parameters.AddWithValue("@name", TxtEditName.Text.Trim)
                cmd.Parameters.AddWithValue("@email", TxtEditEmail.Text.Trim)
                cmd.Parameters.AddWithValue("@phone", TxtEditPhone.Text.Trim)
                cmd.Parameters.AddWithValue("@address", TxtEditAddress.Text.Trim)
                cmd.ExecuteNonQuery()
            End Using
            LoadBorrowersList()
            TabControls.SelectedIndex = 1
            TabControls.SelectedTab = TabPage2
            TxtEditId.Visible = False
            TxtEditName.Visible = False
            TxtEditEmail.Visible = False
            TxtEditAddress.Visible = False
            TxtEditPhone.Visible = False
            BtnUpdateBorDetails.Visible = False
            TxtEditId.Clear()
            TxtEditName.Clear()
            TxtEditEmail.Clear()
            TxtEditAddress.Clear()
            TxtEditPhone.Clear()
            Label19.Text = "ADD BORROWER"
            MessageBox.Show("Successfully updated", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred while updating borrower record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnDeleteBorrower_Click(sender As Object, e As EventArgs) Handles BtnDeleteBorrower.Click
        If selected_borrower_id = 0 Then
            MessageBox.Show("Please select a record first", "No selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim confirmation As DialogResult = MessageBox.Show("Delete borrower " & "[ ID: " & selected_borrower_id & " NAME: " & selected_borrower_name & " ] permanently?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirmation = DialogResult.Yes Then
                con.Close()
                Try
                    con.Open()

                    Using cmd As New MySqlCommand("DELETE FROM borrowers WHERE borrower_id = @borrower_id", con)
                        cmd.Parameters.AddWithValue("@borrower_id", selected_borrower_id)
                        cmd.ExecuteNonQuery()
                    End Using
                    LoadBorrowersList()
                    BtnEditBorrower.Enabled = False
                    BtnDeleteBorrower.Enabled = False
                    MessageBox.Show("Successfully deleted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error Occurred while deleting borrower record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try
            End If

        End If
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
            Dim query As String = "SELECT * FROM borrowed_books ORDER BY borrow_date DESC"
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

    'CONFIRM BORROWING BUTTON
    Private Sub BtnConfirm_Click(sender As Object, e As EventArgs) Handles BtnConfirm.Click

        If TxtName.Text.Trim = "" Or TxtPhoneNo.Text.Trim = "" Or TxtEmail.Text.Trim = "" Or TxtPhoneNo.Text.Trim = "" Then
            MessageBox.Show("Please fill in the blank(s)", "No Input(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim confirmation As DialogResult = MessageBox.Show("Save this transaction?.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmation = DialogResult.Yes Then
                Save_borrowing_info()
                isAddingPanelVisible = False
            End If
        End If

    End Sub

    'SAVING BORROW INFORMATION
    Private Sub Save_borrowing_info()
        con.Close()

        Dim book_ids As String = ""
        Dim books_title As String = ""
        Dim max_no_books_to_bor As Integer
        Dim books_total As Integer = 0
        con.Open()
        Try
            Using cmd2 As New MySqlCommand("SELECT ifNull(MAX(books_count),0) FROM borrow_books_temp", con)
                Dim reader As MySqlDataReader = cmd2.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        max_no_books_to_bor = reader(0) - 1
                        books_total = reader(0)
                    End If
                End If
                reader.Close()
            End Using

            Dim row_cntr As Integer = 0
            Dim nmbrng As Integer = 1
            While row_cntr <= max_no_books_to_bor
                Dim id As String = DgvToBorrow.Item(2, row_cntr).Value.ToString
                book_ids &= nmbrng & ".) " & id & Environment.NewLine
                Dim title As String = DgvToBorrow.Item(3, row_cntr).Value.ToString
                books_title &= nmbrng & ".) " & title & Environment.NewLine
                row_cntr += 1
                nmbrng += 1
            End While
            'MsgBox(book_ids & Environment.NewLine & books_title)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on getting book ids and titles from datagrid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        End Try

        Try
            Dim query As String = "
            INSERT INTO borrowed_books
                (`borrow_id`,`book_ids`,`title`,`total_no_book`,`borrower_id`,`type`,`due_date`,`borrow_date`, `time`)
            VALUES
                (@borrow_trans_id, @book_id, @title, @total_books, @borrower_id,  @type, @to_return_date, @date, @time)
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@borrow_trans_id", Convert.ToInt64(TxtBorrowTransId.Text.Trim))
                cmd.Parameters.AddWithValue("@book_id", book_ids)
                cmd.Parameters.AddWithValue("@title", books_title)
                cmd.Parameters.AddWithValue("@total_books", books_total)
                cmd.Parameters.AddWithValue("@date", TxtDate.Text.Trim)
                cmd.Parameters.AddWithValue("@to_return_date", DtDueDate.Value.Date.ToString("MM-dd-yyyy"))
                cmd.Parameters.AddWithValue("@type", TxtType.Text.Trim)
                cmd.Parameters.AddWithValue("@time", TxtTime.Text.Trim)

                If TxtExistingBorrowerId.Text.Trim = "" Then
                    cmd.Parameters.AddWithValue("@borrower_id", TxtGenaratedId.Text.Trim)
                Else
                    cmd.Parameters.AddWithValue("@borrower_id", TxtExistingBorrowerId.Text.Trim)
                End If
                cmd.ExecuteNonQuery()
            End Using

            If TxtExistingBorrowerId.Text.Trim = "" And Lbl1.Text.Trim <> "Record found" Then
                Save_borrower_info()
            End If

            LoadBorrowedBooksList()
            LoadBorrowersList()
            LoadReturnedBooksList()
            LoadOverDues()
            EmptyTempStorage()

            TxtName.Clear()
            TxtEmail.Clear()
            TxtAddress.Clear()
            TxtPhoneNo.Clear()
            TxtType.Clear()
            TxtExistingBorrowerId.Clear()
            BtnBorrowedBooks.PerformClick()
            PnlAddingToBorrow.Visible = False
            books_count = 1
            selected_book_id = 0
            MessageBox.Show("Successfully Saved Borrowing transaction at Borrowed Books", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub BtnReturn_Click(sender As Object, e As EventArgs) Handles BtnReturn.Click
        PanelCancelled.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PanelCancelled.Visible = True
    End Sub
End Class