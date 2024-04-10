
Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Windows.Forms

Public Class BorrowingAndReturning
    Dim selected_book_id As Integer = 0
    Dim selected_book_stat As String = ""
    Dim selected_book_title As String = ""
    ReadOnly selected_book_type As String = ""
    Shared ReadOnly date_time As DateTime = DateTime.Now
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

    'CODES TO LOAD RESEARCH WORK IN REPO MANAGER
    ReadOnly open_tab As String = ""
    Private ReadOnly frm1 As Form1
    Public Sub New(ByVal frm1 As Form1, tab_toOpen As String)
        InitializeComponent()
        Me.frm1 = frm1
        open_tab = tab_toOpen
    End Sub

    Private ReadOnly cl As CreateLoginAccount
    Public Sub New(ByVal cl As CreateLoginAccount)
        InitializeComponent()
        Me.cl = cl
    End Sub

    Private Sub BorrowingAndReturning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        LoadBooksList()
        LoadBorrowersList()
        LoadBorrowedBooksList()
        LoadCancelledBorrow()
        LoadReturnedBooksList()
        CheckOverDuesBorrowedBooks()
        LoadOverDues()
        'CheckActiveLogin()
        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        Timer1.Start()
        DgvBooks.Focus()
        If open_tab = "overdues" Then
            BtnOverduesBooks.PerformClick()
        ElseIf open_tab = "borrowed" Then
            BtnBorrowedBooks.PerformClick()
        ElseIf open_tab = "returned" Then
            BtnReturnedBooks.PerformClick()
        End If
    End Sub

    Private Sub CheckActiveLogin()
        If loggedin <= 0 Then
            Me.Close()
            Dim crt_lgn As New CreateLoginAccount
            crt_lgn.Show()
        Else
            If account_type_loggedin = "staff" Then
                LblStaffLoggedin.Text = "STAFF | " & account_loggedin.ToUpper()
            Else
                LblStaffLoggedin.Text = "ADMIN | " & account_loggedin.ToUpper()
            End If
        End If
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
    ReadOnly dt_books_list As New DataTable()
    Private Sub BtnPrintBooksList_Click(sender As Object, e As EventArgs) Handles BtnPrintBooksList.Click
        ' bbr is the form where crystal report viewer is attached
        Dim brr As New ReportBorrowingAndReturning
        brr.Show()
        'report book is my report 
        Dim print_books As New report_book_list
        print_books.Database.Tables("books").SetDataSource(dt_books_list)
        'CrBaR is the report viewer
        brr.CrvBaR.ReportSource = print_books
    End Sub
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
                            qnty_loc.location,
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
                    dt_books_list.Clear()
                    adptr.Fill(dt_books_list)
                    If dt_books_list.Rows.Count > 0 Then
                        DgvBooks.DataSource = dt_books_list
                        DgvBooks.Refresh()
                        For i = 0 To DgvBooks.Rows.Count - 1
                            DgvBooks.Rows(i).Height = 70
                        Next
                        DgvBooks.ClearSelection()
                    Else
                        DgvBooks.DataSource = dt_books_list
                        DgvBooks.ClearSelection()
                    End If

                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading Book List", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnSearchBooks_Click(sender As Object, e As EventArgs) Handles BtnSearchBooks.Click
        SearchBooks()
    End Sub

    Private Sub TxtSearchBooks_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchBooks.TextChanged
        If TxtSearchBooks.Text.Trim <> "" And TxtSearchBooks.Text.Trim <> "Search Title, Author Etc." Then
            SearchBooks()
        End If
    End Sub

    Private Sub TxtSearchBooks_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearchBooks.KeyDown
        If e.KeyCode = 13 And TxtSearchBooks.Text.Trim <> "" And TxtSearchBooks.Text.Trim <> "Search Title, Author Etc." Then
            SearchBooks()
        End If
    End Sub

    Private Sub TxtSearchBooks_Click(sender As Object, e As EventArgs) Handles TxtSearchBooks.Click
        If TxtSearchBooks.Text.Trim = "Search Title, Author Etc." Then
            TxtSearchBooks.Text = ""
        End If
    End Sub

    Private Sub TxtSearchBooks_Leave(sender As Object, e As EventArgs) Handles TxtSearchBooks.Leave
        If TxtSearchBooks.Text = "" Then
            TxtSearchBooks.Text = "Search Title, Author Etc."
            LoadBooksList()
        End If
    End Sub

    Private Sub SearchBooks()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                        SELECT 
                            scholarly_works.sw_id,
                            scholarly_works.title, 
                            authors.authors_name,
                            published_details.date_published,
                            qnty_loc.location,
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
                        WHERE 
                            scholarly_works.sw_id LIKE @to_search
                            OR scholarly_works.title LIKE @to_search
                            OR authors.authors_name LIKE @to_search
                            OR published_details.date_published LIKE @to_search
                            "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearchBooks.Text.Trim & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvBooks.DataSource = dt
                    DgvBooks.Refresh()
                    For i = 0 To DgvBooks.Rows.Count - 1
                        DgvBooks.Rows(i).Height = 70
                    Next
                    DgvBooks.ClearSelection()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub DgvBooks_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvBooks.ColumnHeaderMouseClick
        For i = 0 To DgvBooks.Rows.Count - 1
            DgvBooks.Rows(i).Height = 70
        Next
        DgvBooks.ClearSelection()
    End Sub

    'BUTTON BOOKS CLICK
    Private Sub BtnBooks_Click(sender As Object, e As EventArgs) Handles BtnBooks.Click
        TabControls.SelectedIndex = 0
        TabControls.SelectedTab = TabPage1
        BtnBooks.BackColor = Color.PowderBlue

        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        BtnOverduesBooks.BackColor = Color.Transparent
        LoadBooksList()

        BtnRemoveToBorrow.Visible = False
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
        BtnCancelBorrow.Enabled = False
        BtnReturnBooks.Enabled = False
        DgvBooks.Focus()
    End Sub

    Private Sub DgvBooks_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvBooks.KeyDown
        If e.KeyCode = 13 Then
            If isAddingPanelVisible = False Then
                PnlAddingToBorrow.Visible = True
                isAddingPanelVisible = True
            Else
                AddToBorrow.PerformClick()
            End If
            e.Handled = True
        End If
    End Sub

    'DELETE TEMPORARY ADDED BOOKS
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
            TxtExistingBorrowerId.Clear()
            TxtName.Clear()
            TxtAddress.Clear()
            TxtEmail.Clear()
            TxtPhoneNo.Clear()
            BtnRemoveToBorrow.Visible = False
            books_count = 1
            BtnRemoveTempInfo.Visible = False
            BtnRemoveToBorrow.Visible = False
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

        If selected_book_id = 0 Or TxtBookId.Text = "" Then
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
                        ElseIf selected_book_stat = "Available" Then
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
        selected_book_stat = DgvBooks.Item(6, i).Value
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
                DtDueDate.Enabled = False
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
                DtDueDate.Enabled = True

                LoadToBorrowTemp()
                TxtExistingBorrowerId.Focus()
                TxtBookId.Clear()
                TxtTitle.Clear()
                TxtToBorrowType.Clear()
                selected_book_id = 0
            Else
                TabControls.SelectedTab = TabPage6
                BtnBooks.BackColor = Color.Transparent
                BtnBorrower.BackColor = Color.Transparent
                BtnBorrowedBooks.BackColor = Color.Transparent
                BtnReturnedBooks.BackColor = Color.Transparent
                BtnOverduesBooks.BackColor = Color.Transparent
                TxtType.Text = selected_book_stat

                LoadToBorrowTemp()
                TxtExistingBorrowerId.Focus()
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
            TxtName.Clear()
            TxtEmail.Clear()
            TxtPhoneNo.Clear()
            TxtAddress.Clear()

            TxtName.Enabled = True
            TxtEmail.Enabled = True
            TxtPhoneNo.Enabled = True
            TxtAddress.Enabled = True

            Lbl1.Text = ""
        End If

    End Sub

    'KEYDOWN
    Private Sub TxtExistingBorrowerId_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtExistingBorrowerId.KeyDown
        If e.KeyCode = 13 Then
            BtnConfirm.Focus()
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
            If max_no = 0 Then
                isThereAvailableBorrowSelected = False
                isThereInternalBorrowSelected = False
            End If

            If max_no = 0 And isBrtbClicked Then
                BtnBooks.PerformClick()
                selected_book_id = 0
                isThereAvailableBorrowSelected = False
                isThereInternalBorrowSelected = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on BtnRemoveTempInfo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        End Try
    End Sub

    Private Sub BtnCancelAddedToBorrow_Click(sender As Object, e As EventArgs) Handles BtnCancelAddedToBorrow.Click
        Dim confirmation As DialogResult = MessageBox.Show("Cancel Borrowing? All books added to will be removed.", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
            EmptyTempStorage()
            Button7.PerformClick()

        End If
    End Sub
    Private Sub BtnCancelBorrowingTrans_Click(sender As Object, e As EventArgs) Handles BtnCancelBorrowingTrans.Click
        Dim confirmation As DialogResult = MessageBox.Show("Cancel Borrowing? All books added to will be removed.", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
            BtnBooks.PerformClick()
            EmptyTempStorage()
            Button7.PerformClick()
            BtnCancelAddedToBorrow.PerformClick()
        End If

    End Sub



    '==============BORROWERS==================
    ReadOnly dt_borrowers_rec As New DataTable()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim brr As New ReportBorrowingAndReturning
        brr.Show()

        Dim print_borrowers As New report_borrowers_list
        print_borrowers.Database.Tables("borrowers").SetDataSource(dt_borrowers_rec)

        brr.CrvBaR.ReportSource = print_borrowers
    End Sub
    Private Sub LoadBorrowersList()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowers ORDER BY name ASC"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    dt_borrowers_rec.Clear()
                    adptr.Fill(dt_borrowers_rec)

                    If dt.Rows.Count > 0 Then
                        DgvBorrowers.DataSource = dt
                        DgvBorrowers.Refresh()
                        For i = 0 To DgvBorrowers.Rows.Count - 1
                            DgvBorrowers.Rows(i).Height = 50
                        Next
                        DgvBorrowers.ClearSelection()
                    Else
                        DgvBorrowers.DataSource = dt
                        DgvBorrowers.Refresh()
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

    Private Sub DgvBorrowers_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvBorrowers.ColumnHeaderMouseClick
        For i = 0 To DgvBorrowers.Rows.Count - 1
            DgvBorrowers.Rows(i).Height = 70
        Next
        DgvBorrowers.ClearSelection()
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
        BtnRemoveToBorrow.Visible = False
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
        BtnCancelBorrow.Enabled = False
        BtnReturnBooks.Enabled = False
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

    'METHOD TO CEATE OR ADD NEW BORROWER
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

    'BUTTON TO EDIT BORROWER DETAIL
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

    'BUTTON TO UPDATE BORROWER DETAILS
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

    'BUTTON DELETE BORROWER
    Private Sub BtnDeleteBorrower_Click(sender As Object, e As EventArgs) Handles BtnDeleteBorrower.Click
        If selected_borrower_id = 0 Then
            MessageBox.Show("Please select a record first", "No selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            con.Close()
            Try
                con.Open()

                Using cmd As New MySqlCommand("
                    SELECT 
                        borrower_id 
                    FROM 
                        borrowed_books 
                    WHERE 
                        borrower_id = @borrower_id 
                        AND is_cancel='NO'
                        AND is_returned='NO'", con)
                    cmd.Parameters.AddWithValue("@borrower_id", selected_borrower_id)
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        MessageBox.Show("Unable to delete borrower with active borrow transaction", "Unable to delete borrower", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        reader.Close()
                        Dim confirmation As DialogResult = MessageBox.Show("Delete borrower " & "[ ID: " & selected_borrower_id & " NAME: " & selected_borrower_name & " ] permanently?", "Confirm Deleting?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        If confirmation = DialogResult.Yes Then
                            Using cmd2 As New MySqlCommand("DELETE FROM borrowers WHERE borrower_id = @borrower_id", con)
                                cmd2.Parameters.AddWithValue("@borrower_id", selected_borrower_id)
                                cmd2.ExecuteNonQuery()
                            End Using
                            LoadBorrowersList()
                            BtnEditBorrower.Enabled = False
                            BtnDeleteBorrower.Enabled = False
                            MessageBox.Show("Successfully deleted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                    reader.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred while deleting borrower record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
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

                        TxtName.Enabled = False
                        TxtEmail.Enabled = False
                        TxtPhoneNo.Enabled = False
                        TxtAddress.Enabled = False
                    Else
                        Lbl1.ForeColor = Color.Maroon
                        Lbl1.Text = "No record found!"
                        TxtName.Text = ""
                        TxtEmail.Text = ""
                        TxtPhoneNo.Text = ""
                        TxtAddress.Text = ""

                        TxtName.Enabled = True
                        TxtEmail.Enabled = True
                        TxtPhoneNo.Enabled = True
                        TxtAddress.Enabled = True
                    End If
                Else
                    TxtName.Enabled = True
                    TxtEmail.Enabled = True
                    TxtPhoneNo.Enabled = True
                    TxtAddress.Enabled = True
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


    Private Sub SearchBorrower()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                SELECT * 
                FROM 
                    borrowers 
                WHERE 
                    borrower_id LIKE @to_search 
                    OR name LIKE @to_search 
                    OR email LIKE @to_search 
                    OR phone LIKE @to_search 
                    OR address LIKE @to_search 
                    OR violations LIKE @to_search 
                ORDER BY name ASC
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearchBorrowers.Text.Trim & "%")
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
                    Else
                        DgvBorrowers.DataSource = dt
                        DgvBorrowers.Refresh()
                    End If

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrowers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnSearchBorrowers_Click(sender As Object, e As EventArgs) Handles BtnSearchBorrowers.Click
        SearchBorrower()
    End Sub

    Private Sub TxtSearchBorrowers_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchBorrowers.TextChanged
        If TxtSearchBorrowers.Text.Trim <> "" And TxtSearchBorrowers.Text.Trim <> "Search ID, Name, Email Etc." Then
            SearchBorrower()
        End If
    End Sub

    Private Sub TxtSearchBorrowers_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearchBorrowers.KeyDown
        If e.KeyCode = 13 And TxtSearchBorrowers.Text.Trim <> "" And TxtSearchBorrowers.Text.Trim <> "Search ID, Name, Email Etc." Then
            SearchBorrower()
        End If
    End Sub

    Private Sub TxtSearchBorrowers_Click(sender As Object, e As EventArgs) Handles TxtSearchBorrowers.Click
        If TxtSearchBorrowers.Text.Trim = "Search ID, Name, Email Etc." Then
            TxtSearchBorrowers.Text = ""
        End If
    End Sub

    Private Sub TxtSearchBorrowers_Leave(sender As Object, e As EventArgs) Handles TxtSearchBorrowers.Leave
        If TxtSearchBorrowers.Text = "" Then
            TxtSearchBorrowers.Text = "Search ID, Name, Email Etc."
            LoadBorrowersList()
        End If
    End Sub




    '==============BORROWED BOOKS ============================
    Private Sub LoadBorrowedBooksList()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowed_books WHERE is_cancel='NO' AND is_returned='NO' ORDER BY borrow_date DESC, time DESC"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvBorrowed.DataSource = dt
                        DgvBorrowed.Refresh()
                        For i = 0 To DgvBorrowed.Rows.Count - 1
                            DgvBorrowed.Rows(i).Height = 70
                        Next
                        DgvBorrowed.ClearSelection()
                    Else
                        DgvBorrowed.DataSource = dt
                        DgvBorrowed.Refresh()
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

    Private Sub DgvBorrowed_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvBorrowed.ColumnHeaderMouseClick
        For i = 0 To DgvBorrowed.Rows.Count - 1
            DgvBorrowed.Rows(i).Height = 70
        Next
        DgvBorrowed.ClearSelection()
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
        BtnRemoveToBorrow.Visible = False
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
        BtnCancelBorrow.Enabled = False
        BtnReturnBooks.Enabled = False
        If PanelCancelled.Visible = True Then
            PanelCancelled.Visible = False
        End If
        frm1.LoadAllDisplayData()
    End Sub

    Private Sub SearchInBorrowed()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                        SELECT 
                            * 
                        FROM 
                            borrowed_books 
                        WHERE  
                            is_cancel = 'NO' AND is_returned = 'NO' 
                            AND (           
                                borrow_id LIKE @to_search 
                                OR book_ids LIKE @to_search 
                                OR title LIKE @to_search 
                                OR borrower_id LIKE @to_search 
                                OR due_date LIKE @to_search 
                                OR borrow_date LIKE @to_search 
                                OR time LIKE @to_search
                            )
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearchBorrowed.Text.Trim & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvBorrowed.DataSource = dt
                    DgvBorrowed.Refresh()
                    For i = 0 To DgvBorrowed.Rows.Count - 1
                        DgvBorrowed.Rows(i).Height = 70
                    Next
                    DgvBorrowed.ClearSelection()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnSearchBorrowed_Click(sender As Object, e As EventArgs) Handles BtnSearchBorrowed.Click
        SearchInBorrowed()
    End Sub

    Private Sub TxtSearchBorrowed_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchBorrowed.TextChanged
        If TxtSearchBorrowed.Text.Trim <> "" And TxtSearchBorrowed.Text.Trim <> "Search Title, Author Etc." Then
            SearchInBorrowed()
        End If
    End Sub

    Private Sub TxtSearchBorrowed_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearchBorrowed.KeyDown
        If e.KeyCode = 13 And TxtSearchBorrowed.Text.Trim <> "" And TxtSearchBorrowed.Text.Trim <> "Search Title, Author Etc." Then
            SearchInBorrowed()
        End If
    End Sub

    Private Sub TxtSearchBorrowed_Click(sender As Object, e As EventArgs) Handles TxtSearchBorrowed.Click
        If TxtSearchBorrowed.Text.Trim = "Search Title, Author Etc." Then
            TxtSearchBorrowed.Text = ""
        End If
    End Sub

    Private Sub TxtSearchBorrowed_Leave(sender As Object, e As EventArgs) Handles TxtSearchBorrowed.Leave
        If TxtSearchBorrowed.Text = "" Then
            TxtSearchBorrowed.Text = "Search Title, Author Etc."
            LoadBorrowedBooksList()
        End If
    End Sub

    Private Sub SearchInCancelled()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                        SELECT 
                            borrow_id, book_ids, title, borrower_id, due_date, borrow_date, time, is_cancel
                        FROM 
                            borrowed_books 
                        WHERE  
                            is_cancel = 'YES' AND
                           (           
                            borrow_id LIKE @to_search 
                            OR book_ids LIKE @to_search 
                            OR title LIKE @to_search 
                            OR borrower_id LIKE @to_search 
                            OR due_date LIKE @to_search 
                            OR borrow_date LIKE @to_search 
                            OR time LIKE @to_search
                            )
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtsearchCancel.Text.Trim & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvCancelledBorrow.DataSource = dt
                    DgvCancelledBorrow.Refresh()
                    For i = 0 To DgvCancelledBorrow.Rows.Count - 1
                        DgvCancelledBorrow.Rows(i).Height = 70
                    Next
                    DgvCancelledBorrow.ClearSelection()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching CancelledBorrow", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnSearchCancel_Click(sender As Object, e As EventArgs) Handles BtnSearchCancel.Click
        SearchInCancelled()
    End Sub

    Private Sub TxtsearchCancel_TextChanged(sender As Object, e As EventArgs) Handles TxtsearchCancel.TextChanged
        If TxtsearchCancel.Text.Trim <> "" And TxtsearchCancel.Text.Trim <> "Search Title, Author Etc." Then
            SearchInCancelled()
        End If
    End Sub

    Private Sub TxtsearchCancel_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtsearchCancel.KeyDown
        If e.KeyCode = 13 And TxtsearchCancel.Text.Trim <> "" And TxtsearchCancel.Text.Trim <> "Search Title, Author Etc." Then
            SearchInCancelled()
        End If
    End Sub

    Private Sub TxtsearchCancel_Click(sender As Object, e As EventArgs) Handles TxtsearchCancel.Click
        If TxtsearchCancel.Text.Trim = "Search Title, Author Etc." Then
            TxtsearchCancel.Text = ""
        End If
    End Sub

    Private Sub TxtsearchCancel_Leave(sender As Object, e As EventArgs) Handles TxtsearchCancel.Leave
        If TxtsearchCancel.Text = "" Then
            TxtsearchCancel.Text = "Search Title, Author Etc."
            LoadCancelledBorrow()
        End If
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
            Dim confirmation As DialogResult = MessageBox.Show("Save this borrowing transaction?.", "Confirm Saving?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

        Try
            con.Open()
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
                Dim count As String = DgvToBorrow.Item(1, row_cntr).Value.ToString
                Dim id As String = DgvToBorrow.Item(2, row_cntr).Value.ToString
                book_ids &= nmbrng & ".) " & id & Environment.NewLine
                Dim title As String = DgvToBorrow.Item(3, row_cntr).Value.ToString
                books_title &= nmbrng & ".) " & title & Environment.NewLine

                Dim qry As String = "
                    INSERT INTO borrowed_books_id(`borrow_id`,`books_count`,`book_id`,`title`)
                    VALUES(@borrow_trans_id, @count, @book_id, @title)
                    "
                Using cmd3 As New MySqlCommand(qry, con)
                    cmd3.Parameters.AddWithValue("@borrow_trans_id", TxtBorrowTransId.Text.Trim)
                    cmd3.Parameters.AddWithValue("@count", count)
                    cmd3.Parameters.AddWithValue("@book_id", id)
                    cmd3.Parameters.AddWithValue("@title", title)
                    cmd3.ExecuteNonQuery()
                End Using

                Dim current_book_qnty As Integer = 0
                Using cmd_get_book_qnty As New MySqlCommand("SELECT MAX(quantity) FROM `qnty_loc` WHERE `sw_id`=@id", con)
                    cmd_get_book_qnty.Parameters.AddWithValue("@id", id)
                    Dim reader As MySqlDataReader = cmd_get_book_qnty.ExecuteReader()
                    If reader.HasRows Then
                        If reader.Read() Then
                            current_book_qnty = reader(0)
                        End If
                    End If
                    reader.Close()
                End Using

                Dim new_qnty As Integer = current_book_qnty - 1
                Using cmd_update_book_qnty As New MySqlCommand("UPDATE `qnty_loc` SET `quantity`=@new_qnty WHERE sw_id=@id", con)
                    cmd_update_book_qnty.Parameters.AddWithValue("@id", id)
                    cmd_update_book_qnty.Parameters.AddWithValue("@new_qnty", new_qnty)
                    cmd_update_book_qnty.ExecuteNonQuery()
                End Using

                row_cntr += 1
                nmbrng += 1

            End While
            isThereAvailableBorrowSelected = False
            isThereInternalBorrowSelected = False
            con.Close()
            frm1.LoadAllDisplayData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on getting book ids and titles from datagrid", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

        Try
            con.Open()
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

            'save new borrowers record if do not have record yet
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
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Saving Borrowing Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnReturn_Click(sender As Object, e As EventArgs) Handles BtnReturn.Click
        PanelCancelled.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PanelCancelled.Visible = True
        LoadCancelledBorrow()
    End Sub

    Dim selected_borrowed_book As Integer = 0
    Private Sub DgvBorrowed_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBorrowed.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim i As Integer = DgvBorrowed.CurrentRow.Index
            selected_borrowed_book = DgvBorrowed.Item(1, i).Value
            BtnCancelBorrow.Enabled = True
            BtnReturnedBooks.Enabled = True
            BtnReturnBooks.Enabled = True
            ' MsgBox(selected_borrowed_book)
        End If

    End Sub

    Private Sub BtnCancelBorrow_Click(sender As Object, e As EventArgs) Handles BtnCancelBorrow.Click
        If selected_borrowed_book = 0 Then
            MessageBox.Show("Please select a record first", "No selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            con.Close()
            'checking if book is overdue, if yes, so cancelling is not applicable
            Try
                con.Open()
                Using check_qcmd As New MySqlCommand("SELECT * FROM borrowed_books WHERE borrow_id=@borrow_id AND is_overdue='YES' ", con)
                    check_qcmd.Parameters.AddWithValue("@borrow_id", selected_borrowed_book)
                    Dim reader0 As MySqlDataReader = check_qcmd.ExecuteReader()

                    If reader0.HasRows AndAlso reader0.Read() Then
                        MessageBox.Show("This Book was Overdue", "Invalid cancelling overdue books", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        Dim confirmation As DialogResult = MessageBox.Show("Cancel Borrowed Book?", "Confirm Cancelling?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If confirmation = DialogResult.Yes Then

                            reader0.Close()
                            Try
                                'get total count of books borrowed that has the same id
                                Dim total As Integer = 0
                                Dim cntr As Integer = 1
                                Using cmd_books_count_total As New MySqlCommand("SELECT COUNT(borrow_id) FROM borrowed_books_id WHERE borrow_id=@borrow_id", con)
                                    cmd_books_count_total.Parameters.AddWithValue("@borrow_id", selected_borrowed_book)
                                    Dim reader As MySqlDataReader = cmd_books_count_total.ExecuteReader()
                                    If reader.HasRows AndAlso reader.Read() Then
                                        total = Convert.ToInt32(reader(0))
                                    End If
                                    reader.Close()
                                End Using

                                While cntr <= total

                                    'select book_id 1 by 1
                                    Dim book_id As Integer = 0
                                    Using cmd_books_id As New MySqlCommand("SELECT book_id FROM borrowed_books_id WHERE books_count=@books_count AND borrow_id=@borrow_id", con)
                                        cmd_books_id.Parameters.AddWithValue("@books_count", cntr)
                                        cmd_books_id.Parameters.AddWithValue("@borrow_id", selected_borrowed_book)

                                        Dim reader As MySqlDataReader = cmd_books_id.ExecuteReader()
                                        If reader.HasRows AndAlso reader.Read() Then
                                            book_id = Convert.ToInt32(reader(0))
                                        End If
                                        reader.Close()
                                    End Using

                                    Dim remaining_qnty As Integer = 0
                                    Dim new_qnty As Integer = 0

                                    'get remaining qnty of books
                                    Using cmd_remaining_book_qnty As New MySqlCommand("SELECT `quantity` FROM `qnty_loc` WHERE sw_id=@book_id", con)
                                        cmd_remaining_book_qnty.Parameters.AddWithValue("@book_id", book_id)
                                        Dim reader As MySqlDataReader = cmd_remaining_book_qnty.ExecuteReader()
                                        If reader.HasRows AndAlso reader.Read() Then
                                            remaining_qnty = Convert.ToInt32(reader(0))
                                        End If
                                        reader.Close()
                                    End Using

                                    new_qnty = remaining_qnty + 1
                                    'Add cancelled book's qnty to the repo
                                    Using cmd_update_book_qnty As New MySqlCommand("UPDATE `qnty_loc` SET `quantity`=@new_qnty WHERE sw_id=@book_id", con)
                                        cmd_update_book_qnty.Parameters.AddWithValue("@book_id", book_id)
                                        cmd_update_book_qnty.Parameters.AddWithValue("@new_qnty", new_qnty)
                                        cmd_update_book_qnty.ExecuteNonQuery()
                                    End Using

                                    cntr += 1
                                End While

                                Using cmd As New MySqlCommand("UPDATE borrowed_books SET is_cancel='YES' WHERE borrow_id=@id ", con)
                                    cmd.Parameters.AddWithValue("@id", selected_borrowed_book)
                                    cmd.ExecuteNonQuery()
                                End Using
                                con.Close()
                                LoadCancelledBorrow()
                                LoadBorrowedBooksList()
                                selected_borrowed_book = 0
                                BtnCancelBorrow.Enabled = True
                                BtnReturnedBooks.Enabled = True
                                DgvCancelledBorrow.ClearSelection()
                                frm1.LoadAllDisplayData()
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "Error Occurred on Cancelling", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                con.Close()
                            Finally
                                con.Close()
                            End Try

                        End If
                    End If
                    reader0.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred on Cancelling", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If
    End Sub

    Private Sub LoadCancelledBorrow()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM borrowed_books WHERE is_cancel='YES' ORDER BY borrow_date DESC, time DESC"
            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvCancelledBorrow.DataSource = dt
                        DgvCancelledBorrow.Refresh()
                        For i = 0 To DgvCancelledBorrow.Rows.Count - 1
                            DgvCancelledBorrow.Rows(i).Height = 50
                        Next
                        DgvCancelledBorrow.ClearSelection()
                    Else
                        DgvCancelledBorrow.DataSource = dt
                        DgvCancelledBorrow.Refresh()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading Cancelled Book", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub
    Private Sub DgvCancelledBorrow_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvCancelledBorrow.ColumnHeaderMouseClick
        For i = 0 To DgvCancelledBorrow.Rows.Count - 1
            DgvCancelledBorrow.Rows(i).Height = 70
        Next
        DgvCancelledBorrow.ClearSelection()
    End Sub

    Dim initial_ret_id As Integer = 0
    Dim generated_returned_id As Integer = 0
    Private Sub GenerateRetunedID()
        initial_ret_id = rnd.Next(10000, 99999)
        Isinitial_ret_idUnique()
    End Sub
    Private Sub Isinitial_ret_idUnique()

        Try

            Dim query As String = "SELECT returned_id FROM returned_books WHERE returned_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & initial_ret_id)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateRetunedID()
                Else
                    Dim query2 As String = "SELECT borrow_id FROM borrowed_books WHERE borrow_id=@id"
                    Using cmd2 As New MySqlCommand(query2, con)
                        cmd2.Parameters.AddWithValue("@id", current_year.ToString & initial_ret_id)
                        Dim count2 As Integer = Convert.ToInt32(cmd2.ExecuteScalar())
                        If count2 > 0 Then
                            GenerateRetunedID()
                        Else
                            generated_returned_id = current_year.ToString & initial_ret_id.ToString
                        End If
                    End Using
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to check the uniqueness of generated returned id number")

        End Try
    End Sub

    Private Sub BtnReturnBooks_Click(sender As Object, e As EventArgs) Handles BtnReturnBooks.Click
        If selected_borrowed_book = 0 Then
            MessageBox.Show("Please select a record first", "No selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim confirmation As DialogResult = MessageBox.Show("Return Book?", "Confirm Return?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmation = DialogResult.Yes Then
                con.Close()

                Try
                    con.Open()
                    'get total count of books borrowed that has the same id
                    Dim total As Integer = 0
                    Dim cntr As Integer = 1
                    Using cmd_books_count_total As New MySqlCommand("SELECT COUNT(borrow_id) FROM borrowed_books_id WHERE borrow_id=@borrow_id", con)
                        cmd_books_count_total.Parameters.AddWithValue("@borrow_id", selected_borrowed_book)
                        Dim reader As MySqlDataReader = cmd_books_count_total.ExecuteReader()
                        If reader.HasRows AndAlso reader.Read() Then
                            total = Convert.ToInt32(reader(0))
                        End If
                        reader.Close()
                    End Using

                    While cntr <= total

                        'select book_id 1 by 1 based on count of boooks 
                        Dim book_id As Integer = 0
                        Using cmd_books_id As New MySqlCommand("SELECT book_id FROM borrowed_books_id WHERE books_count=@books_count AND borrow_id=@borrow_id", con)
                            cmd_books_id.Parameters.AddWithValue("@books_count", cntr)
                            cmd_books_id.Parameters.AddWithValue("@borrow_id", selected_borrowed_book)

                            Dim reader As MySqlDataReader = cmd_books_id.ExecuteReader()
                            If reader.HasRows AndAlso reader.Read() Then
                                book_id = Convert.ToInt32(reader(0))
                            End If
                            reader.Close()
                        End Using

                        Dim remaining_qnty As Integer = 0
                        Dim new_qnty As Integer = 0

                        'get remaining qnty of books
                        Using cmd_remaining_book_qnty As New MySqlCommand("SELECT `quantity` FROM `qnty_loc` WHERE sw_id=@book_id", con)
                            cmd_remaining_book_qnty.Parameters.AddWithValue("@book_id", book_id)
                            Dim reader As MySqlDataReader = cmd_remaining_book_qnty.ExecuteReader()
                            If reader.HasRows AndAlso reader.Read() Then
                                remaining_qnty = Convert.ToInt32(reader(0))
                            End If
                            reader.Close()
                        End Using

                        new_qnty = remaining_qnty + 1
                        'Adding back cancelled book's qnty to the repo
                        Using cmd_update_book_qnty As New MySqlCommand("UPDATE `qnty_loc` SET `quantity`=@new_qnty WHERE sw_id=@book_id", con)
                            cmd_update_book_qnty.Parameters.AddWithValue("@book_id", book_id)
                            cmd_update_book_qnty.Parameters.AddWithValue("@new_qnty", new_qnty)
                            cmd_update_book_qnty.ExecuteNonQuery()
                        End Using

                        cntr += 1
                    End While

                    Using cmd As New MySqlCommand("UPDATE borrowed_books SET is_returned='YES' WHERE borrow_id=@id ", con)
                        cmd.Parameters.AddWithValue("@id", selected_borrowed_book)
                        cmd.ExecuteNonQuery()
                    End Using

                    Using cmd As New MySqlCommand("UPDATE overdues SET status='RETURNED' WHERE borrow_id=@id ", con)
                        cmd.Parameters.AddWithValue("@id", selected_borrowed_book)
                        cmd.ExecuteNonQuery()
                    End Using

                    Dim return_stat As String = ""
                    Using cmd_return_late As New MySqlCommand("SELECT is_overdue FROM borrowed_books WHERE borrow_id=@id", con)
                        cmd_return_late.Parameters.AddWithValue("@id", selected_borrowed_book)
                        Dim reader As MySqlDataReader = cmd_return_late.ExecuteReader()
                        If reader.HasRows Then
                            If reader.Read() Then
                                return_stat = reader("is_overdue").ToString
                            End If
                        End If
                        reader.Close()
                    End Using

                    Dim to_save_retrn_stat As String = ""
                    If return_stat = "NO" Then
                        to_save_retrn_stat = "ON TIME"
                    Else
                        to_save_retrn_stat = "RETURNED LATE"
                    End If

                    GenerateRetunedID()
                    Using cmd_save_return As New MySqlCommand("
                        INSERT INTO returned_books 
                            (`returned_id`, `borrow_id`, `returned_date`, `returned_time`, `return_stat`)
                        VALUES
                            (@rtrnd_id, @brrw_id, @rtrnd_date, @rtrnd_time, @rtrn_stat)
                        ", con)
                        cmd_save_return.Parameters.AddWithValue("@rtrnd_id", generated_returned_id)
                        cmd_save_return.Parameters.AddWithValue("@brrw_id", selected_borrowed_book)
                        cmd_save_return.Parameters.AddWithValue("@rtrnd_date", TxtDate.Text.Trim)
                        cmd_save_return.Parameters.AddWithValue("@rtrnd_time", TxtTime.Text.Trim)
                        cmd_save_return.Parameters.AddWithValue("@rtrn_stat", to_save_retrn_stat)
                        cmd_save_return.ExecuteNonQuery()
                    End Using

                    con.Close()
                    LoadCancelledBorrow()
                    LoadBorrowedBooksList()
                    LoadBooksList()
                    LoadOverDues()
                    selected_borrowed_book = 0
                    BtnCancelBorrow.Enabled = True
                    BtnReturnedBooks.Enabled = True
                    DgvCancelledBorrow.ClearSelection()
                    frm1.LoadAllDisplayData()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error Occurred on Returning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try

            End If
        End If
    End Sub




    '==============RETURNED BOOKS ==============================
    Private Sub LoadReturnedBooksList()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                SELECT 
                    returned_books.returned_id, 
                    returned_books.borrow_id AS returned_borrow_id, 
                    returned_books.returned_date, 
                    returned_books.returned_time, 
                    returned_books.return_stat, 
                    borrowed_books.count, 
                    borrowed_books.borrow_id, 
                    borrowed_books.book_ids, 
                    borrowed_books.title, 
                    borrowed_books.total_no_book, 
                    borrowed_books.borrower_id, 
                    borrowed_books.type, 
                    borrowed_books.due_date, 
                    borrowed_books.borrow_date, 
                    borrowed_books.time, 
                    borrowed_books.is_cancel, 
                    borrowed_books.is_returned, 
                    borrowed_books.is_overdue 
                FROM returned_books 
                INNER JOIN borrowed_books 
                    ON borrowed_books.borrow_id = returned_books.borrow_id AND borrowed_books.is_returned = 'YES'
                ORDER BY returned_date DESC, returned_time DESC
"
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
                    Else
                        DgvReturned.DataSource = dt
                        DgvReturned.Refresh()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Loading LoadReturnedBooksList", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub DgvReturned_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvReturned.ColumnHeaderMouseClick
        For i = 0 To DgvReturned.Rows.Count - 1
            DgvReturned.Rows(i).Height = 70
        Next
        DgvReturned.ClearSelection()
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
        BtnRemoveToBorrow.Visible = False
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
        BtnCancelBorrow.Enabled = False
        BtnReturnBooks.Enabled = False
    End Sub

    Private Sub SearchInReturned()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                    SELECT
                        returned_books.returned_id, 
                        returned_books.borrow_id AS returned_borrow_id, 
                        returned_books.returned_date, 
                        returned_books.returned_time, 
                        returned_books.return_stat, 
                        borrowed_books.count, 
                        borrowed_books.borrow_id, 
                        borrowed_books.book_ids, 
                        borrowed_books.title, 
                        borrowed_books.total_no_book, 
                        borrowed_books.borrower_id, 
                        borrowed_books.type, 
                        borrowed_books.due_date, 
                        borrowed_books.borrow_date, 
                        borrowed_books.time, 
                        borrowed_books.is_cancel, 
                        borrowed_books.is_returned, 
                        borrowed_books.is_overdue 
                    FROM returned_books 
                    INNER JOIN borrowed_books 
                    ON borrowed_books.borrow_id = returned_books.borrow_id AND borrowed_books.is_returned = 'YES'
               
                    WHERE
                        is_returned = 'YES' 
                        AND 
                        (          
                            returned_books.returned_id LIKE @to_search 
                            OR returned_books.borrow_id LIKE @to_search 
                            OR returned_books.returned_date LIKE @to_search 
                            OR returned_books.returned_time LIKE @to_search 
                            OR returned_books.return_stat LIKE @to_search 

                            OR borrowed_books.borrow_id LIKE @to_search 
                            OR borrowed_books.book_ids LIKE @to_search 
                            OR borrowed_books.title LIKE @to_search 
                            OR borrowed_books.borrower_id LIKE @to_search 
                            OR borrowed_books.due_date LIKE @to_search 
                            OR borrowed_books.borrow_date LIKE @to_search 
                            OR borrowed_books.time LIKE @to_search
                        )
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearchReturned.Text.Trim & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvReturned.DataSource = dt
                    DgvReturned.Refresh()
                    For i = 0 To DgvReturned.Rows.Count - 1
                        DgvReturned.Rows(i).Height = 70
                    Next
                    DgvReturned.ClearSelection()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Searching Borrowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnSearchReturned_Click(sender As Object, e As EventArgs) Handles BtnSearchReturned.Click
        SearchInReturned()
    End Sub

    Private Sub TxtSearchReturned_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchReturned.TextChanged
        If TxtSearchReturned.Text.Trim <> "" And TxtSearchReturned.Text.Trim <> "Search Title, Author Etc." Then
            SearchInReturned()
        End If
    End Sub

    Private Sub TxtSearchReturned_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearchReturned.KeyDown
        If e.KeyCode = 13 And TxtSearchReturned.Text.Trim <> "" And TxtSearchReturned.Text.Trim <> "Search Title, Author Etc." Then
            SearchInReturned()
        End If
    End Sub

    Private Sub TxtSearchReturned_Click(sender As Object, e As EventArgs) Handles TxtSearchReturned.Click
        If TxtSearchReturned.Text.Trim = "Search Title, Author Etc." Then
            TxtSearchReturned.Text = ""
        End If
    End Sub

    Private Sub TxtSearchReturned_Leave(sender As Object, e As EventArgs) Handles TxtSearchReturned.Leave
        If TxtSearchReturned.Text = "" Then
            TxtSearchReturned.Text = "Search Title, Author Etc."
            LoadReturnedBooksList()
        End If
    End Sub

    '==============OVERDUES===============================
    Private Sub LoadOverDues()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT 
                                    overdues.borrow_id, 
                                    overdues.borrower_id, 
                                    overdues.due_date, 
                                    overdues.overdue_days,
                                    overdues.status,
                                    borrowed_books.book_ids, 
                                    borrowed_books.title, 
                                    borrowed_books.total_no_book, 
                                    borrowed_books.type, 
                                    borrowed_books.borrow_date,
                                    borrowed_books.time
                                FROM overdues
                                INNER JOIN borrowed_books
                                    ON borrowed_books.borrow_id = overdues.borrow_id
                                WHERE overdues.status='NOT RETURNED'
                                ORDER BY overdues.due_date ASC
                "
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
                    Else
                        DgvOverdues.DataSource = dt
                        DgvOverdues.Refresh()
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

    Private Sub DgvOverdues_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvOverdues.ColumnHeaderMouseClick
        For i = 0 To DgvOverdues.Rows.Count - 1
            DgvOverdues.Rows(i).Height = 70
        Next
        DgvOverdues.ClearSelection()
    End Sub

    Private Sub BtnOverduesBooks_Click(sender As Object, e As EventArgs) Handles BtnOverduesBooks.Click
        TabControls.SelectedIndex = 4
        TabControls.SelectedTab = TabPage5
        BtnOverduesBooks.BackColor = Color.PowderBlue

        BtnBooks.BackColor = Color.Transparent
        BtnBorrower.BackColor = Color.Transparent
        BtnBorrowedBooks.BackColor = Color.Transparent
        BtnReturnedBooks.BackColor = Color.Transparent
        CheckOverDuesBorrowedBooks()
        LoadOverDues()
        frm1.LoadAllDisplayData()
        BtnRemoveToBorrow.Visible = False
        BtnEditBorrower.Enabled = False
        BtnDeleteBorrower.Enabled = False
        BtnCancelBorrow.Enabled = False
        BtnReturnBooks.Enabled = False
    End Sub

    Private Sub CheckOverDuesBorrowedBooks()
        con.Close()
        Try
            Dim due_date As String = ""
            Dim book_due_date As DateTime
            Dim current_date As Date = Date.Today
            Dim borrow_id As Integer = 0
            Dim borrower_id As Integer = 0
            Dim isThereOverdue As Boolean = False
            Dim overdue_books_count As Integer = 0

            con.Open()
            Using cmdSelect As New MySqlCommand("SELECT borrow_id, borrower_id, due_date FROM borrowed_books WHERE is_cancel='NO' AND is_returned='NO' AND is_overdue='NO'", con)
                Dim reader As MySqlDataReader = cmdSelect.ExecuteReader()

                Dim SubCon As New MySqlConnection("server=localhost;user=root;password=;database=cpri_cdsga_db")
                Try
                    SubCon.Open()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error Occurred on Checking Overdues Borrowed Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    SubCon.Close()
                End Try


                While reader.Read()
                    due_date = reader("due_date").ToString()
                    book_due_date = DateTime.ParseExact(reader("due_date").ToString(), "MM-dd-yyyy", CultureInfo.InvariantCulture).Date

                    borrower_id = Convert.ToInt32(reader("borrower_id"))
                    borrow_id = Convert.ToInt32(reader("borrow_id"))

                    'knowing if books is already overdue
                    If book_due_date < current_date Then
                        'MsgBox(book_due_date.ToString)
                        overdue_books_count += 1
                        isThereOverdue = True


                        Using cmd3 As New MySqlCommand("UPDATE borrowed_books SET is_overdue='YES' WHERE borrow_id=@id ", SubCon)
                            cmd3.Parameters.AddWithValue("@id", borrow_id)
                            cmd3.ExecuteNonQuery()
                        End Using

                        Using cmd4 As New MySqlCommand("UPDATE borrowers SET violations='LATE RETURNER' WHERE borrower_id=@id ", SubCon)
                            cmd4.Parameters.AddWithValue("@id", borrower_id)
                            cmd4.ExecuteNonQuery()
                        End Using

                        Dim overdue_days As Integer = (current_date - book_due_date).Days

                        Using cmd_insert As New MySqlCommand("
                            INSERT INTO overdues
                                (`borrow_id`, `borrower_id`, `due_date`, `overdue_days`)
                            VALUES
                                (@borrowID, @borrowerID, @dueDate, @days)", SubCon)

                            cmd_insert.Parameters.AddWithValue("@borrowID", borrow_id)
                            cmd_insert.Parameters.AddWithValue("@borrowerID", borrower_id)
                            cmd_insert.Parameters.AddWithValue("@dueDate", due_date)
                            cmd_insert.Parameters.AddWithValue("@days", overdue_days)
                            cmd_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
                reader.Close()
                SubCon.Close()
            End Using

            If isThereOverdue Then
                If overdue_books_count > 1 Then
                    MessageBox.Show("There are Overdue Books detected! Check details on overdue tab.", "Overdue Books Detected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("There is an Overdue Book detected! Check details on overdue tab.", "Overdue Book  Detected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                isThereOverdue = False
                overdue_books_count = 0
            End If

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred on Checking Overdues Borrowed Books", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub LogOut_Click(sender As Object, e As EventArgs) Handles LogOut.Click
        loggedin = 0
        account_loggedin = ""
        account_type_loggedin = ""
        CheckActiveLogin()
    End Sub

    Private Sub PanelCancelled_Paint(sender As Object, e As PaintEventArgs) Handles PanelCancelled.Paint

    End Sub
End Class