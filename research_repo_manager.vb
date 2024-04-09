Imports System.IO
Imports MySql.Data.MySqlClient

Public Class ResearchRepoManager

    'VARIABLES DECLARATION
    Dim selected_research As Integer = 0

    Dim sw_edit_id As Integer
    Dim isSearchButtonUsed As Boolean = False
    Dim isDataAlreadyLoaded As Boolean = False

    Dim search_me As String = ""
    Private ReadOnly frm1 As Form1
    Public Sub New(ByVal frm1 As Form1, search_me As String)
        InitializeComponent()
        Me.frm1 = frm1
        search_me = search_me
        If search_me <> "" Then
            TxtSearch.Text = search_me
            BtnSearch.PerformClick()
        End If

    End Sub

    'MAIN FORM LOAD
    Private Sub ResearchRepoManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PnlFilter.Width = 0
        PnlFilter.Height = 0

        LoadScholarlyWorks()

        'setting the height of rows in datagrid
        For i = 0 To DgvSwData.Rows.Count - 1
            DgvSwData.Rows(i).Height = 70
        Next

        DgvSwData.ClearSelection()
        BtnDelete.Enabled = False

        If search_me <> "" Then
            TxtSearch.Text = search_me
            BtnSearch.PerformClick()
        End If

    End Sub

    'LOADING ALL DATA FROM SCHOLARLY WORKS IN DATAGRIDVIEW FROM DATABASE 
    Public Sub LoadScholarlyWorks()

        Try
            ConOpen()
            Dim query As String = "
                SELECT 
                    scholarly_works.*, 
                    COALESCE (published_details.date_published,'None') AS date_published,
                    COALESCE (presented_details.date_presented,'None') AS date_presented,
                    sw_abstract.display_text,
                    sw_whole_file.display_text AS whole_file_text,
                    authors.authors_name AS authors,
                    qnty_loc.quantity,
                    qnty_loc.location,
                (SELECT GROUP_CONCAT(co_authors.co_authors_name SEPARATOR', ')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id) AS co_authors,
            CONCAT('Author: ', '\n', authors.degree_program, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.degree_program SEPARATOR'\n')
                    FROM co_authors   
                     WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_deg_prog,
            CONCAT('Author: ', '\n',authors.role, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.role SEPARATOR'\n')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_role

                FROM scholarly_works
                LEFT JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id
                LEFT JOIN presented_details
                    ON presented_details.presented_id = scholarly_works.sw_id
                INNER JOIN authors 
                    ON authors.authors_id = scholarly_works.sw_id
                INNER JOIN sw_abstract 
                    ON sw_abstract.abstract_id = scholarly_works.sw_id
                INNER JOIN sw_whole_file 
                    ON sw_whole_file.whole_file_id = scholarly_works.sw_id
                INNER JOIN qnty_loc 
                    ON qnty_loc.sw_id = scholarly_works.sw_id

                "

            Using cmd As New MySqlCommand(query, con)
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    DgvSwData.DataSource = dt
                    DgvSwData.Refresh()
                    For i = 0 To DgvSwData.Rows.Count - 1
                        DgvSwData.Rows(i).Height = 70
                    Next
                End Using
            End Using
            DgvSwData.Refresh()
            isDataAlreadyLoaded = True
            LblSrchFnd.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR OCCURRED: Failed in Loading Scholarly Works")
            Console.WriteLine(ex.Message)
        Finally
            DgvSwData.ClearSelection()
        End Try
    End Sub

    ' HANDLE CELL CLICK FUNCTION
    Dim whl_abs As String
    Private Sub DgvSwData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            If e.ColumnIndex = 12 Then
                Dim i As Integer = DgvSwData.CurrentRow.Index
                selected_research = DgvSwData.Item(1, i).Value
                Dim open_file As DialogResult
                open_file = MessageBox.Show("Open this abstract file?", "Click Yes to proceed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If open_file = DialogResult.Yes Then
                    selected_research = DgvSwData.Item(1, i).Value
                    whl_abs = "abstract"
                    OpenFile(selected_research)

                End If
            ElseIf e.ColumnIndex = 13 Then
                Dim i As Integer = DgvSwData.CurrentRow.Index
                selected_research = DgvSwData.Item(1, i).Value
                Dim open_file As DialogResult
                open_file = MessageBox.Show("Open this whole file?", "Click Yes to proceed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If open_file = DialogResult.Yes Then

                    whl_abs = "whole"
                    OpenFile(selected_research)

                End If
                'MsgBox("You've selected abstract file with the ID of : " & selected_research)
            Else
                Dim i As Integer = DgvSwData.CurrentRow.Index
                selected_research = DgvSwData.Item(1, i).Value
                'MsgBox("You've selected " & selected_research)
            End If
            BtnRemoveSelection.Visible = True
            BtnDelete.Enabled = True
        End If
    End Sub

    ' JUST PREVENTING THE AUTO RESIZING OF ROWS WHEN COL HEADER IS CLICKED
    Private Sub DgvSwData_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvSwData.ColumnHeaderMouseClick
        For i = 0 To DgvSwData.Rows.Count - 1
            DgvSwData.Rows(i).Height = 70
        Next
    End Sub

    'RETRIEVING FILES IN DATABASE AND OPENING IT IN PDF
    Private Sub OpenFile(file_id)
        Dim pdfByteArray As Byte() = RetrievePdfFile(file_id)

        If pdfByteArray IsNot Nothing AndAlso pdfByteArray.Length > 0 Then
            Dim tempFilePath As String = Path.GetTempFileName()
            tempFilePath = Path.ChangeExtension(tempFilePath, ".pdf")
            Try
                File.WriteAllBytes(tempFilePath, pdfByteArray)
                If File.Exists(tempFilePath) Then
                    Process.Start(tempFilePath)
                Else
                    MessageBox.Show("File not exists")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to open PDF file", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine(ex.Message)
            End Try
        End If
    End Sub

    Function RetrievePdfFile(file_id) As Byte()

        Dim pdfByteArray As Byte() = Nothing

        If whl_abs = "abstract" Then
            con.Close()
            Try
                con.Open()
                Dim query As String = "SELECT file_data FROM sw_abstract WHERE abstract_id=@file_id"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@file_id", file_id)
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        pdfByteArray = DirectCast(reader("file_data"), Byte())
                    End If
                    reader.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to retrieve whole file from database.")
                Console.WriteLine(ex.Message)
            Finally
                con.Close()
            End Try

        ElseIf whl_abs = "whole" Then
            con.Close()
            Try
                con.Open()
                Dim query As String = "SELECT file_data FROM sw_whole_file WHERE whole_file_id=@file_id"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@file_id", file_id)
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        pdfByteArray = DirectCast(reader("file_data"), Byte())
                    End If
                    reader.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to retrieve whole file from database.")
                Console.WriteLine(ex.Message)
            Finally
                con.Close()
            End Try


        End If

        Return pdfByteArray
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim add_work As New AddWorks(Me)
        add_work.Show()
    End Sub

    ' ENTERING AND LEAVING THE SPECIFIC CELL
    Private Sub DgvSwData_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellMouseEnter
        If e.ColumnIndex = 12 Then
            DgvSwData.Cursor = Cursors.Hand
        ElseIf e.ColumnIndex = 13 Then
            DgvSwData.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub DgvSwData_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellMouseLeave
        If e.ColumnIndex = 12 Then
            DgvSwData.Cursor = Cursors.Default
        ElseIf e.ColumnIndex = 13 Then
            DgvSwData.Cursor = Cursors.Default
        End If
    End Sub

    'OPENING AND CLOSING OF FILTER PANEL 
    Dim open_close_filter As Integer = 0
    Private Sub BtnFilter_Click(sender As Object, e As EventArgs) Handles BtnFilter.Click
        If Not isDataAlreadyLoaded Then
            TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc."
            LblSrchFnd.Text = ""
            LoadScholarlyWorks()
            BtnRemoveSelection.PerformClick()
        End If

        BtnFilter.Enabled = False
        If open_close_filter = 0 Then
            TmOpenFilter.Enabled = True
            open_close_filter = 1
        Else
            TmCloseFilter.Enabled = True
            open_close_filter = 0
        End If
    End Sub
    Private Sub TmOpenFilter_Tick(sender As Object, e As EventArgs) Handles TmOpenFilter.Tick
        If PnlFilter.Width >= 414 Then
            TmOpenFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width + 414
            PnlFilter.Height = PnlFilter.Height + 413
        End If
    End Sub
    Private Sub TmCloseFilter_Tick(sender As Object, e As EventArgs) Handles TmCloseFilter.Tick
        If PnlFilter.Width <= 0 Then
            TmCloseFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width - 414
            PnlFilter.Height = PnlFilter.Height - 413
        End If
    End Sub
    Private Sub BtnCloseFilter_Click(sender As Object, e As EventArgs) Handles BtnCloseFilter.Click
        TmCloseFilter.Enabled = True
        open_close_filter = 0
    End Sub


    ' DELETE RESEARCH
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If selected_research = 0 Then
            MessageBox.Show("No Selected File to Open", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If on_edit_mode = selected_research Then
                MessageBox.Show("Can't Delete the selected item because it currently in edit form.", "Unable to delete this item.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Dim delete_confirmation As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Click Yes to Confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If delete_confirmation = DialogResult.Yes Then
                    con.Close()

                    'delete on scholarly work
                    Dim delete_queries() As String = {
                        "DELETE FROM authors WHERE authors_id = @to_delete_id",
                        "DELETE FROM co_authors WHERE co_authors_id = @to_delete_id",
                        "DELETE FROM presented_details WHERE presented_id = @to_delete_id",
                        "DELETE FROM published_details WHERE published_id = @to_delete_id",
                        "DELETE FROM scholarly_works WHERE sw_id = @to_delete_id",
                        "DELETE FROM status_completed_info WHERE stat_completed_id = @to_delete_id",
                        "DELETE FROM sw_abstract WHERE abstract_id= @to_delete_id",
                        "DELETE FROM sw_whole_file WHERE whole_file_id = @to_delete_id",
                        "DELETE FROM `qnty_loc` WHERE `sw_id`= @to_delete_id"
                    }

                    con.Open()
                    Using transaction As MySqlTransaction = con.BeginTransaction()

                        Try
                            For Each queries As String In delete_queries
                                Using cmd_multiple As New MySqlCommand(queries, con, transaction)
                                    cmd_multiple.Parameters.AddWithValue("@to_delete_id", selected_research)
                                    cmd_multiple.ExecuteNonQuery()
                                End Using
                            Next
                            transaction.Commit()

                            LoadScholarlyWorks()
                            BtnRemoveSelection.PerformClick()
                            BtnRemoveSelection.Visible = False
                            BtnDelete.Enabled = False
                            MessageBox.Show("Successfuly Deleted Item.", "Deleted Successfully.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            transaction.Rollback()
                            MessageBox.Show("Error Occurred: " & ex.Message, "Failed to delete selected item.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            con.Close()
                        End Try
                    End Using
                End If

            End If
        End If
    End Sub

    Private Sub BtnRemoveSelection_Click(sender As Object, e As EventArgs) Handles BtnRemoveSelection.Click
        selected_research = 0
        on_edit_mode = 0
        BtnRemoveSelection.Visible = False
        DgvSwData.ClearSelection()
        BtnDelete.Enabled = False
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        sw_edit_id = selected_research

        If sw_edit_id = 0 Then
            MessageBox.Show("PLease select an item first", "No Selected Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf isEditModeActive = False Then
            Dim edit_work_record As New EditWorkRecord(Me, sw_edit_id)
            edit_work_record.Show()
            isEditModeActive = True
            on_edit_mode = selected_research
        Else
            MessageBox.Show("There is currently in edit mode. Close it and try again.", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Dim isTxtSearchChangedTriggered As Boolean = False
    'SEARCHING FUNCTION
    Private Sub Search(search_term As String)
        con.Close()
        Try
            ConOpen()
            Dim query As String = "
            SELECT 
                scholarly_works.*, 
                    COALESCE (published_details.date_published,'None') AS date_published,
                    COALESCE (presented_details.date_presented,'None') AS date_presented,
                    sw_abstract.display_text,
                    sw_whole_file.display_text AS whole_file_text,
                    authors.authors_name AS authors,
                    qnty_loc.quantity,
                    qnty_loc.location,
                (SELECT GROUP_CONCAT(co_authors.co_authors_name SEPARATOR', ')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id) AS co_authors,
            CONCAT('Author: ', '\n', authors.degree_program, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.degree_program SEPARATOR'\n')
                    FROM co_authors   
                     WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_deg_prog,
            CONCAT('Author: ', '\n',authors.role, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.role SEPARATOR'\n')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_role
            FROM scholarly_works
                LEFT JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id
                LEFT JOIN presented_details
                    ON presented_details.presented_id = scholarly_works.sw_id
                INNER JOIN authors 
                    ON authors.authors_id = scholarly_works.sw_id
                INNER JOIN sw_abstract 
                    ON sw_abstract.abstract_id = scholarly_works.sw_id
                INNER JOIN sw_whole_file 
                    ON sw_whole_file.whole_file_id = scholarly_works.sw_id
                INNER JOIN qnty_loc 
                    ON qnty_loc.sw_id = scholarly_works.sw_id
            
            WHERE 
            scholarly_works.sw_id LIKE @searchTerm
            OR qnty_loc.location LIKE @searchTerm
            OR scholarly_works.title LIKE @searchTerm
            OR scholarly_works.research_agenda LIKE @searchTerm
            OR scholarly_works.semester LIKE @searchTerm
            OR scholarly_works.school_year LIKE @searchTerm
            OR scholarly_works.status_ongoing_completed LIKE @searchTerm
            OR scholarly_works.date_completed LIKE @searchTerm
            OR scholarly_works.published LIKE '" & search_term & "%'
            OR scholarly_works.presented LIKE '" & search_term & "%'
            OR published_details.date_published LIKE @searchTerm
            OR presented_details.date_presented LIKE @searchTerm
          
        "

            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@searchTerm", "%" & search_term & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        DgvSwData.DataSource = dt
                        DgvSwData.Refresh()
                        For i = 0 To DgvSwData.Rows.Count - 1
                            DgvSwData.Rows(i).Height = 70
                        Next
                        LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                        isDataAlreadyLoaded = False
                    Else
                        Search_in_Auth_CoAuth(search_term)
                    End If

                End Using
            End Using
            DgvSwData.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR OCCURRED: Failed in Loading Scholarly Works")
            Console.WriteLine(ex.Message)
        Finally
            con.Close()
            DgvSwData.ClearSelection()
        End Try
    End Sub


    'SEARCH ON AUTHOR AND CO AUTHOR TABLES 
    Private Sub Search_in_Auth_CoAuth(search_term As String)
        'MsgBox("search co auth")
        con.Close()
        Try
            ConOpen()
            Dim query As String = "
               SELECT
               scholarly_works.*, 
                    COALESCE (published_details.date_published,'None') AS date_published,
                    COALESCE (presented_details.date_presented,'None') AS date_presented,
                    sw_abstract.display_text,
                    sw_whole_file.display_text AS whole_file_text,
                    authors.authors_name AS authors,
                    qnty_loc.quantity,
                    qnty_loc.location,
                (SELECT GROUP_CONCAT(co_authors.co_authors_name SEPARATOR', ')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id) AS co_authors,
            CONCAT('Author: ', '\n', authors.degree_program, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.degree_program SEPARATOR'\n')
                    FROM co_authors   
                     WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_deg_prog,
            CONCAT('Author: ', '\n',authors.role, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.role SEPARATOR'\n')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_role
            FROM scholarly_works
                LEFT JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id
                LEFT JOIN presented_details
                    ON presented_details.presented_id = scholarly_works.sw_id
                INNER JOIN authors 
                    ON authors.authors_id = scholarly_works.sw_id
                LEFT JOIN co_authors 
                    ON co_authors.co_authors_id = scholarly_works.sw_id
                INNER JOIN sw_abstract 
                    ON sw_abstract.abstract_id = scholarly_works.sw_id
                INNER JOIN sw_whole_file 
                    ON sw_whole_file.whole_file_id = scholarly_works.sw_id
                INNER JOIN qnty_loc 
                    ON qnty_loc.sw_id = scholarly_works.sw_id    
            WHERE 
                authors.authors_name LIKE @searchTerm
                OR co_authors.co_authors_name LIKE @searchTerm

                OR authors.degree_program LIKE @searchTerm
                OR co_authors.degree_program LIKE @searchTerm

                OR authors.role LIKE @searchTerm
                OR co_authors.role LIKE @searchTerm

            GROUP BY scholarly_works.sw_id
       
        "

            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@searchTerm", "%" & search_term & "%")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        DgvSwData.DataSource = dt
                        DgvSwData.Refresh()
                        For i = 0 To DgvSwData.Rows.Count - 1
                            DgvSwData.Rows(i).Height = 70
                        Next
                        isDataAlreadyLoaded = False
                        LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                    Else
                        If Not isTxtSearchChangedTriggered Then
                            LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                            MessageBox.Show("Your search do not match to any records. Please try different keywords", "No data found.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                            DgvSwData.DataSource = dt
                            DgvSwData.Refresh()
                            isTxtSearchChangedTriggered = False
                        End If
                        isDataAlreadyLoaded = False
                    End If

                End Using
            End Using
            DgvSwData.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR OCCURRED: Failed in Searching in Author and Co Authors")
            Console.WriteLine(ex.Message)
        Finally
            con.Close()
            DgvSwData.ClearSelection()
        End Try
    End Sub


    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        Timer1.Stop()
        Timer1.Start()
    End Sub

    Dim txt_search_clicked As Boolean = False
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not isSearchButtonUsed And TxtSearch.Text.Trim <> "" And TxtSearch.Text <> "Search Title, Author, Keyword, Abstract, Etc." Then
            isTxtSearchChangedTriggered = True
            Dim search_term As String = TxtSearch.Text.Trim
            Search(search_term)
        ElseIf Not txt_search_clicked Then
            BtnSearch.PerformClick()
            txt_search_clicked = False
        End If
        Timer1.Stop()
        isSearchButtonUsed = False
    End Sub

    Private Sub TxtSearch_Click(sender As Object, e As EventArgs) Handles TxtSearch.Click
        If TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc." Then
            TxtSearch.Text = ""
            TxtSearch.ForeColor = Color.Black
            BtnRemoveSelection.PerformClick()
            txt_search_clicked = True
        End If

    End Sub

    Private Sub TxtSearch_Leave(sender As Object, e As EventArgs) Handles TxtSearch.Leave
        If TxtSearch.Text = "" And Not isDataAlreadyLoaded Then
            TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc."
            TxtSearch.ForeColor = Color.Gray
            LoadScholarlyWorks()
            LblSrchFnd.Text = ""
        ElseIf TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc." And Not isDataAlreadyLoaded Then
            TxtSearch.ForeColor = Color.Gray
            LoadScholarlyWorks()
            LblSrchFnd.Text = ""
        ElseIf isDataAlreadyLoaded And TxtSearch.Text = "" Then
            TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc."
            TxtSearch.ForeColor = Color.Gray
            LblSrchFnd.Text = ""
        End If
        BtnRemoveSelection.PerformClick()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If TxtSearch.Text <> "Search Title, Author, Keyword, Abstract, Etc." And TxtSearch.Text <> "" Then
            Dim search_term As String = TxtSearch.Text.Trim
            Search(search_term)
            isSearchButtonUsed = True
        ElseIf TxtSearch.Text <> "Search Title, Author, Keyword, Abstract, Etc." Then
            LoadScholarlyWorks()
            LblSrchFnd.Text = ""
        End If

    End Sub

    Private Sub TxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearch.KeyDown
        If e.KeyCode = 13 Then
            BtnSearch.PerformClick()
            isSearchButtonUsed = True
        End If
    End Sub

    'VIEW BUTTON
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If selected_research = 0 Then
            MessageBox.Show("PLease select an item first", "No Selected Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            to_view_work_id = selected_research
            Dim view_form As New ViewWorks
            view_form.Show()
        End If

    End Sub

    '/////////////////////////APPLY FILTER SEARCH
    Dim filter_query As String = ""
    Dim dateToQry As String = ""
    Dim start_date As String = ""
    Dim end_date As String = ""
    Dim isDateFromSet, isDateToSet, isDateFromSet2, isDateToSet2, isDateFromSet3, isDateToSet3 As Boolean
    Dim stat_to_query As String
    Dim pub_to_query As String
    Dim pres_to_query As String


    Private Sub BtnApplyFilter_Click(sender As Object, e As EventArgs) Handles BtnApplyFilter.Click

        If dateToQry = "" And stat_to_query = "" And pub_to_query = "" And pres_to_query = "" Then
            MessageBox.Show("Please select a filter to apply.", "No Filter Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ApplyFilterSearch()
            BtnCloseFilter.PerformClick()
        End If

    End Sub

    Private Sub ApplyFilterSearch()

        Try
            filter_query = "
                SELECT 
                scholarly_works.*, 
                    COALESCE (published_details.date_published,'None') AS date_published,
                    COALESCE (presented_details.date_presented,'None') AS date_presented,
                    sw_abstract.display_text,
                    sw_whole_file.display_text AS whole_file_text,
                    authors.authors_name AS authors,
                    qnty_loc.quantity,
                    qnty_loc.location,
                (SELECT GROUP_CONCAT(co_authors.co_authors_name SEPARATOR', ')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id) AS co_authors,
            CONCAT('Author: ', '\n', authors.degree_program, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.degree_program SEPARATOR'\n')
                    FROM co_authors   
                     WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_deg_prog,
            CONCAT('Author: ', '\n',authors.role, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.role SEPARATOR'\n')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_role
            FROM scholarly_works
                LEFT JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id
                LEFT JOIN presented_details
                    ON presented_details.presented_id = scholarly_works.sw_id
                INNER JOIN authors 
                    ON authors.authors_id = scholarly_works.sw_id
                INNER JOIN sw_abstract 
                    ON sw_abstract.abstract_id = scholarly_works.sw_id
                INNER JOIN sw_whole_file 
                    ON sw_whole_file.whole_file_id = scholarly_works.sw_id
                INNER JOIN qnty_loc 
                    ON qnty_loc.sw_id = scholarly_works.sw_id
            
                WHERE " & dateToQry & stat_to_query & pub_to_query & pres_to_query

            Using cmd As New MySqlCommand(filter_query, con)
                cmd.Parameters.AddWithValue("@start_date", start_date)
                cmd.Parameters.AddWithValue("@end_date", end_date)
                cmd.Parameters.AddWithValue("@comple", "Completed")
                cmd.Parameters.AddWithValue("@ongo", "Ongoing")
                cmd.Parameters.AddWithValue("@pub_yes", "Published")
                cmd.Parameters.AddWithValue("@pub_no", "Unpublished")
                cmd.Parameters.AddWithValue("@pres_yes", "Presented")
                cmd.Parameters.AddWithValue("@pres_no", "Unpresented")
                Using adptr As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adptr.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        DgvSwData.DataSource = dt
                        DgvSwData.Refresh()
                        For i = 0 To DgvSwData.Rows.Count - 1
                            DgvSwData.Rows(i).Height = 70
                        Next
                        LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                    Else
                        DgvSwData.DataSource = dt
                        DgvSwData.Refresh()
                        LblSrchFnd.Text = dt.Rows.Count.ToString & " Result(s) found"
                    End If
                End Using
            End Using
            DgvSwData.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR on Filtering Search")
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub SetQuery()
        If isDateFromSet And isDateToSet Then
            start_date = DtFrom.Value.ToString("MM-dd-yyyy")
            end_date = DtTo.Value.ToString("MM-dd-yyyy")

            dateToQry = " STR_TO_DATE(date_completed, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_completed, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
        ElseIf isDateFromSet Then
            start_date = DtFrom.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_completed = @start_date "
            DtTo.Enabled = True
            'MsgBox(dateToQry)
        ElseIf isDateToSet Then
            end_date = DtTo.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_completed = @end_date "
            'MsgBox(dateToQry)
        End If

        'published
        If isDateFromSet2 And isDateToSet2 Then
            start_date = DtFrom2.Value.ToString("MM-dd-yyyy")
            end_date = DtTo2.Value.ToString("MM-dd-yyyy")

            dateToQry = " STR_TO_DATE(date_published, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_published, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
        ElseIf isDateFromSet2 Then
            start_date = DtFrom2.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_published = @start_date "
            DtTo2.Enabled = True
            'MsgBox(dateToQry)
        ElseIf isDateToSet2 Then
            end_date = DtTo2.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_published = @end_date "
            'MsgBox(dateToQry)
        End If

        'published
        If isDateFromSet3 And isDateToSet3 Then
            start_date = DtFrom3.Value.ToString("MM-dd-yyyy")
            end_date = DtTo3.Value.ToString("MM-dd-yyyy")

            dateToQry = " STR_TO_DATE(date_presented, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_presented, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
        ElseIf isDateFromSet3 Then
            start_date = DtFrom3.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_presented = @start_date "
            DtTo3.Enabled = True
            'MsgBox(dateToQry)
        ElseIf isDateToSet3 Then
            end_date = DtTo3.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_presented = @end_date "
            'MsgBox(dateToQry)
        End If

        'status
        If RdCompleted.Checked = True Then
            stat_to_query = " status_ongoing_completed=@comple"
        ElseIf RdOngoing.Checked = True Then
            stat_to_query = " status_ongoing_completed=@ongo"
        Else
            stat_to_query = ""
        End If

        'published yes no
        If RdPubYes.Checked = True Then
            pub_to_query = " published=@pub_yes"
        ElseIf RdPubNo.Checked = True Then
            pub_to_query = " published=@pub_no"
        Else
            pub_to_query = ""
        End If

        'presented yes no
        If RdPreYes.Checked = True Then
            pres_to_query = " presented=@pres_yes"
        ElseIf RdPreNo.Checked = True Then
            pres_to_query = " presented=@pres_no"
        Else
            pres_to_query = ""
        End If


        '//
        If isDateFromSet Or isDateToSet Or isDateFromSet2 Or isDateToSet2 Or isDateFromSet3 Or isDateToSet3 Then
            If isDateFromSet And isDateToSet Then
                start_date = DtFrom.Value.ToString("MM-dd-yyyy")
                end_date = DtTo.Value.ToString("MM-dd-yyyy")

                dateToQry = " STR_TO_DATE(date_completed, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_completed, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
                LblFilteredDate.Text = "Filter date : FROM " & start_date & " TO " & end_date
            ElseIf isDateFromSet Or isDateToSet Then
                LblFilteredDate.Text = "Filter date : " & start_date & end_date
            End If

            If isDateFromSet2 And isDateToSet2 Then
                start_date = DtFrom2.Value.ToString("MM-dd-yyyy")
                end_date = DtTo2.Value.ToString("MM-dd-yyyy")

                dateToQry = " STR_TO_DATE(date_published, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_published, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
                LblFilteredDate2.Text = "Filter date : FROM " & start_date & " TO " & end_date
            ElseIf isDateFromSet2 Or isDateToSet2 Then
                LblFilteredDate2.Text = "Filter date : " & start_date & end_date
            End If

            If isDateFromSet3 And isDateToSet3 Then
                start_date = DtFrom3.Value.ToString("MM-dd-yyyy")
                end_date = DtTo3.Value.ToString("MM-dd-yyyy")

                dateToQry = " STR_TO_DATE(date_presented, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_presented, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "
                LblFilteredDate3.Text = "Filter date : FROM " & start_date & " TO " & end_date
            ElseIf isDateFromSet3 Or isDateToSet3 Then
                LblFilteredDate3.Text = "Filter date : " & start_date & end_date
            End If

            'status
            If RdCompleted.Checked = True Then
                stat_to_query = " AND status_ongoing_completed=@comple"
            ElseIf RdOngoing.Checked = True Then
                stat_to_query = " AND status_ongoing_completed=@ongo"
            Else
                stat_to_query = ""
            End If

            'published yes no
            If RdPubYes.Checked = True Then
                pub_to_query = " AND published=@pub_yes"
            ElseIf RdPubNo.Checked = True Then
                pub_to_query = " AND published=@pub_no"
            Else
                pub_to_query = ""
            End If

            'presented yes no
            If RdPreYes.Checked = True Then
                pres_to_query = " AND presented=@pres_yes"
            ElseIf RdPreNo.Checked = True Then
                pres_to_query = " AND presented=@pres_no"
            Else
                pres_to_query = ""
            End If

        ElseIf RdCompleted.Checked = True Or RdOngoing.Checked = True Then
            'published yes no
            If RdPubYes.Checked = True Then
                pub_to_query = " AND published='Published'"
            ElseIf RdPubNo.Checked = True Then
                pub_to_query = " AND published='Unpublished'"
            Else
                pub_to_query = ""
            End If

            'presented yes no
            If RdPreYes.Checked = True Then
                pres_to_query = " AND presented='Presented'"
            ElseIf RdPreNo.Checked = True Then
                pres_to_query = " AND presented='Unpresented'"
            Else
                pres_to_query = ""
            End If

        End If

    End Sub

    'COMPLETED
    Private Sub DtFrom_ValueChanged(sender As Object, e As EventArgs) Handles DtFrom.ValueChanged
        BtnClearDate2.PerformClick()
        BtnClearDate3.PerformClick()
        isDateFromSet = True
        SetQuery()
        BtnClearDate.Visible = True
        DtTo.Enabled = True

        RdCompleted.PerformClick()
        RdOngoing.Enabled = False
        RdCompleted.Enabled = False
        BtnClearStatus.Enabled = False
    End Sub

    Private Sub DtTo_ValueChanged(sender As Object, e As EventArgs) Handles DtTo.ValueChanged
        If DtTo.Value.Date < DtFrom.Value.Date Then
            isDateToSet = False
            SetQuery()
            MessageBox.Show("You can't pick date earlier than starting date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            isDateToSet = True
            SetQuery()
            BtnClearDate.Visible = True
        End If

    End Sub


    'PUBLISHED
    Private Sub DtFrom2_ValueChanged(sender As Object, e As EventArgs) Handles DtFrom2.ValueChanged
        BtnClearDate.PerformClick()
        BtnClearDate3.PerformClick()
        isDateFromSet2 = True
        SetQuery()
        DtTo2.Enabled = True
        BtnClearDate2.Visible = True
        RdPubYes.PerformClick()
        RdPubYes.Enabled = False
        RdPubNo.Enabled = False
        BtnClearPublished.Enabled = False
    End Sub

    Private Sub DtTo2_ValueChanged(sender As Object, e As EventArgs) Handles DtTo2.ValueChanged
        If DtTo2.Value.Date < DtFrom2.Value.Date Then
            MessageBox.Show("You can't pick date earlier than starting date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            isDateToSet2 = True
            SetQuery()
        End If

    End Sub

    'PRESENTED
    Private Sub DtFrom3_ValueChanged(sender As Object, e As EventArgs) Handles DtFrom3.ValueChanged
        BtnClearDate.PerformClick()
        BtnClearDate2.PerformClick()
        isDateFromSet3 = True
        SetQuery()
        DtTo3.Enabled = True
        BtnClearDate3.Visible = True
        RdPreYes.PerformClick()
        RdPreYes.Enabled = False
        RdPreNo.Enabled = False
        BtnClearPresented.Enabled = False
    End Sub

    Private Sub DtTo3_ValueChanged(sender As Object, e As EventArgs) Handles DtTo3.ValueChanged
        If DtTo3.Value.Date < DtFrom3.Value.Date Then
            MessageBox.Show("You can't pick date earlier than starting date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            isDateToSet3 = True
            SetQuery()
        End If

    End Sub


    'SETTING STATUS VARIABLE VALUE
    Private Sub RdOngoing_Click(sender As Object, e As EventArgs) Handles RdOngoing.Click
        SetQuery()
        BtnClearStatus.Visible = True
    End Sub

    Private Sub RdCompleted_Click(sender As Object, e As EventArgs) Handles RdCompleted.Click
        SetQuery()
        BtnClearStatus.Visible = True
    End Sub

    'SETTNG PUBLISHED PRESENTED VARAIBLE
    Private Sub RdPubNo_Click(sender As Object, e As EventArgs) Handles RdPubNo.Click
        RdPreNo.Checked = False
        RdPreYes.Checked = False
        SetQuery()
        BtnClearPublished.Visible = True
        BtnClearPresented.Visible = False
    End Sub

    Private Sub RdPubYes_Click(sender As Object, e As EventArgs) Handles RdPubYes.Click
        RdPreNo.Checked = False
        RdPreYes.Checked = False
        SetQuery()
        BtnClearPublished.Visible = True
        BtnClearPresented.Visible = False
    End Sub

    Private Sub RdPreNo_Click(sender As Object, e As EventArgs) Handles RdPreNo.Click
        RdPubNo.Checked = False
        RdPubYes.Checked = False
        SetQuery()
        BtnClearPresented.Visible = True
        BtnClearPublished.Visible = False
    End Sub

    Private Sub RdPreYes_Click(sender As Object, e As EventArgs) Handles RdPreYes.Click
        RdPubNo.Checked = False
        RdPubYes.Checked = False
        SetQuery()
        BtnClearPresented.Visible = True
        BtnClearPublished.Visible = False
    End Sub

    'CLEAR STAT
    Private Sub BtnClearStatus_Click(sender As Object, e As EventArgs) Handles BtnClearStatus.Click
        RdOngoing.Checked = False
        RdCompleted.Checked = False
        stat_to_query = ""
        BtnClearStatus.Visible = False
        SetQuery()
    End Sub

    'CLEAR PUBLISHED
    Private Sub BtnClearPublished_Click(sender As Object, e As EventArgs) Handles BtnClearPublished.Click
        RdPubNo.Checked = False
        RdPubYes.Checked = False
        pub_to_query = ""
        BtnClearPublished.Visible = False
        BtnClearDate.Visible = False
        BtnClearPresented.Visible = False
        SetQuery()
    End Sub

    Private Sub LblSrchFnd_Click(sender As Object, e As EventArgs) Handles LblSrchFnd.Click

    End Sub

    'CLEAR PRESENTED
    Private Sub BtnClearPresented_Click(sender As Object, e As EventArgs) Handles BtnClearPresented.Click
        BtnClearPresented.Visible = False
        BtnClearPublished.Visible = False
        RdPreNo.Checked = False
        RdPreYes.Checked = False
        pres_to_query = ""
        SetQuery()
    End Sub

    'CLEAR DATE
    Private Sub BtnClearDate_Click(sender As Object, e As EventArgs) Handles BtnClearDate.Click

        DtFrom.Value = DateTime.Now
        DtTo.Value = DateTime.Now
        dateToQry = ""
        LblFilteredDate.Text = ""
        start_date = ""
        end_date = ""
        isDateFromSet = False
        isDateToSet = False
        BtnClearDate.Visible = False
        DtTo.Enabled = False

        RdOngoing.Enabled = True
        RdCompleted.Enabled = True
        BtnClearStatus.Enabled = True
        BtnClearStatus.PerformClick()
        SetQuery()
    End Sub
    Private Sub BtnClearDate2_Click(sender As Object, e As EventArgs) Handles BtnClearDate2.Click

        DtFrom2.Value = DateTime.Now
        DtTo2.Value = DateTime.Now
        dateToQry = ""
        LblFilteredDate2.Text = ""
        start_date = ""
        end_date = ""
        isDateFromSet2 = False
        isDateToSet2 = False
        BtnClearDate2.Visible = False
        DtTo2.Enabled = False

        RdPubYes.Enabled = True
        RdPubNo.Enabled = True
        BtnClearPublished.Enabled = True
        BtnClearPublished.PerformClick()
        SetQuery()
    End Sub
    Private Sub BtnClearDate3_Click(sender As Object, e As EventArgs) Handles BtnClearDate3.Click

        DtFrom3.Value = DateTime.Now
        DtTo3.Value = DateTime.Now
        dateToQry = ""
        LblFilteredDate3.Text = ""
        start_date = ""
        end_date = ""
        isDateFromSet3 = False
        isDateToSet3 = False
        BtnClearDate3.Visible = False
        DtTo3.Enabled = False

        RdPreYes.Enabled = True
        RdPreNo.Enabled = True
        BtnClearPresented.Enabled = True
        BtnClearPresented.PerformClick()
        SetQuery()
    End Sub

    'RESET FILTER
    Private Sub BtnResetFilter_Click(sender As Object, e As EventArgs) Handles BtnResetFilter.Click
        DtFrom.Value = DateTime.Now
        DtTo.Value = DateTime.Now
        DtFrom2.Value = DateTime.Now
        DtTo2.Value = DateTime.Now

        RdOngoing.Checked = False
        RdCompleted.Checked = False
        stat_to_query = ""

        RdPubNo.Checked = False
        RdPubYes.Checked = False
        pub_to_query = ""

        RdPreNo.Checked = False
        RdPreYes.Checked = False
        pres_to_query = ""

        dateToQry = ""
        LblFilteredDate.Text = ""
        LblFilteredDate2.Text = ""
        start_date = ""
        end_date = ""
        isDateFromSet = False
        isDateToSet = False
        isDateFromSet2 = False
        isDateToSet2 = False
        DtTo.Enabled = False
        DtTo2.Enabled = False

        BtnClearDate.Visible = False
        BtnClearDate2.Visible = False
        BtnClearStatus.Visible = False
        BtnClearPublished.Visible = False
        BtnClearPresented.Visible = False


        RdPubYes.Enabled = True
        RdPubNo.Enabled = True
        BtnClearPublished.Enabled = True
        BtnClearPublished.Visible = False


        RdPreYes.Enabled = True
        RdPreNo.Enabled = True
        BtnClearPresented.Enabled = True
        BtnClearPresented.Visible = False

        LoadScholarlyWorks()

    End Sub


End Class