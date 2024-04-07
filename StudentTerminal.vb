Imports System.IO
Imports MySql.Data.MySqlClient

Public Class StudentTerminal

    'VARIABLES DECLARATION
    Dim selected_research As Integer = 0

    Dim sw_edit_id As Integer
    Dim isSearchButtonUsed As Boolean = False
    Dim isDataAlreadyLoaded As Boolean = False

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
        If PnlFilter.Width >= 400 Then
            TmOpenFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width + 400
            PnlFilter.Height = PnlFilter.Height + 300
        End If
    End Sub
    Private Sub TmCloseFilter_Tick(sender As Object, e As EventArgs) Handles TmCloseFilter.Tick
        If PnlFilter.Width <= 0 Then
            TmCloseFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width - 400
            PnlFilter.Height = PnlFilter.Height - 300
        End If
    End Sub
    Private Sub BtnCloseFilter_Click(sender As Object, e As EventArgs) Handles BtnCloseFilter.Click
        TmCloseFilter.Enabled = True
        open_close_filter = 0
    End Sub

    Private Sub BtnRemoveSelection_Click(sender As Object, e As EventArgs) Handles BtnRemoveSelection.Click
        selected_research = 0
        on_edit_mode = 0
        BtnRemoveSelection.Visible = False
        DgvSwData.ClearSelection()
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
    Dim isDateFromSet, isDateToSet As Boolean
    Dim stat_to_query As String
    Dim pub_to_query As String
    Dim pres_to_query As String


    Private Sub BtnApplyFilter_Click(sender As Object, e As EventArgs) Handles BtnApplyFilter.Click

        If dateToQry = "" And stat_to_query = "" And pub_to_query = "" And pres_to_query = "" Then
            MessageBox.Show("Please select a filter to apply.", "No Filter Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ApplyFilterSearch()
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
            DtTo.Enabled = True
            dateToQry = " date_completed = @start_date "
            'MsgBox(dateToQry)
        ElseIf isDateToSet Then
            end_date = DtTo.Value.ToString("MM-dd-yyyy")

            dateToQry = " date_completed = @end_date "
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
        If isDateFromSet Or isDateToSet Then
            If isDateFromSet And isDateToSet Then
                start_date = DtFrom.Value.ToString("MM-dd-yyyy")
                end_date = DtTo.Value.ToString("MM-dd-yyyy")

                dateToQry = " STR_TO_DATE(date_completed, '%m-%d-%Y') >= STR_TO_DATE(@start_date, '%m-%d-%Y')
                            AND STR_TO_DATE(date_completed, '%m-%d-%Y') <= STR_TO_DATE(@end_date, '%m-%d-%Y') "

                LblFilteredDate.Text = "Filter date : FROM " & start_date & " TO " & end_date
            Else
                LblFilteredDate.Text = "Filter date : " & start_date & end_date
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

    '//
    Private Sub DtFrom_ValueChanged(sender As Object, e As EventArgs) Handles DtFrom.ValueChanged
        isDateFromSet = True
        SetQuery()
        BtnClearDate.Visible = True
    End Sub

    Private Sub DtFrom_CloseUp(sender As Object, e As EventArgs) Handles DtFrom.CloseUp
        isDateFromSet = True
        SetQuery()
        BtnClearDate.Visible = True
    End Sub

    Private Sub DtTo_ValueChanged(sender As Object, e As EventArgs) Handles DtTo.ValueChanged
        isDateToSet = True
        SetQuery()
        BtnClearDate.Visible = True
    End Sub

    Private Sub DtTo_CloseUp(sender As Object, e As EventArgs) Handles DtTo.CloseUp
        isDateToSet = True
        SetQuery()
        BtnClearDate.Visible = True
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
        dateToQry = ""
        LblFilteredDate.Text = ""
        start_date = ""
        end_date = ""
        isDateFromSet = False
        isDateToSet = False
        BtnClearDate.Visible = False
        DtTo.Enabled = False
        DtFrom.Value = DateTime.Now
        DtTo.Value = DateTime.Now
        SetQuery()
    End Sub

    'RESET FILTER
    Private Sub BtnResetFilter_Click(sender As Object, e As EventArgs) Handles BtnResetFilter.Click
        DtFrom.Value = DateTime.Now
        DtTo.Value = DateTime.Now

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
        start_date = ""
        end_date = ""
        isDateFromSet = False
        isDateToSet = False
        DtTo.Enabled = False

        BtnClearDate.Visible = False
        BtnClearStatus.Visible = False
        BtnClearPublished.Visible = False
        BtnClearPresented.Visible = False
        LoadScholarlyWorks()
    End Sub
End Class