Imports System.IO
Imports MySql.Data.MySqlClient

Public Class ResearchRepoManager

    'VARIABLES DECLARATION
    Dim selected_research As Integer = 0

    Dim sw_edit_id As Integer

    'MAIN FORM LOAD
    Private Sub ResearchRepoManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PnlFilter.Width = 0
        PnlFilter.Height = 0

        LoadScholarlyWorks("default")

        'setting the height of rows in datagrid
        For i = 0 To DgvSwData.Rows.Count - 1
            DgvSwData.Rows(i).Height = 35
        Next

        DgvSwData.ClearSelection()
        BtnDelete.Enabled = False
    End Sub

    'LOADING ALL DATA FROM SCHOLARLY WORKS IN DATAGRIDVIEW FROM DATABASE 
    Public Sub LoadScholarlyWorks(to_load_data)
        If to_load_data = "default" Then
            Try
                ConOpen()
                Dim query As String = "
                SELECT 
                    scholarly_works.*, 
                    sw_abstract.display_text,
                    sw_whole_file.display_text AS whole_file_text,
            CONCAT('Author: ', '\n', authors.authors_name, '\n','\n','Co-Author(s):','\n',
                (SELECT GROUP_CONCAT(co_authors.co_authors_name SEPARATOR'\n')
                     FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id )) AS authors_and_co_authors,
            CONCAT('Author: ', '\n', authors.degree_program, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.degree_program SEPARATOR'\n')
                    FROM co_authors   
                     WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_deg_prog,
            CONCAT('Author: ', '\n',authors.role, '\n','\n','Co-Author(s): ','\n',
                (SELECT GROUP_CONCAT(co_authors.role SEPARATOR'\n')
                    FROM co_authors
                    WHERE co_authors.co_authors_id = scholarly_works.sw_id)) AS auth_and_co_auth_role
                FROM scholarly_works
                INNER JOIN authors 
                    ON authors.authors_id = scholarly_works.sw_id
                INNER JOIN sw_abstract 
                    ON sw_abstract.abstract_id = scholarly_works.sw_id
                INNER JOIN sw_whole_file 
                    ON sw_whole_file.whole_file_id = scholarly_works.sw_id"

                Using cmd As New MySqlCommand(query, con)
                    Using adptr As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adptr.Fill(dt)

                        DgvSwData.DataSource = dt
                        DgvSwData.Refresh()
                        For i = 0 To DgvSwData.Rows.Count - 1
                            DgvSwData.Rows(i).Height = 35
                        Next
                    End Using
                End Using
                DgvSwData.Refresh()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ERROR OCCURRED: Failed in Loading Scholarly Works")
                Console.WriteLine(ex.Message)
            Finally
                DgvSwData.ClearSelection()
            End Try
        End If

    End Sub

    ' HANDLE CELL CLICK FUNCTION
    Dim whl_abs As String
    Private Sub DgvSwData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            If e.ColumnIndex = 9 Then
                Dim open_file As DialogResult
                open_file = MessageBox.Show("Open this abstract file?", "Click Yes to proceed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If open_file = DialogResult.Yes Then
                    Dim i As Integer = DgvSwData.CurrentRow.Index
                    selected_research = DgvSwData.Item(1, i).Value
                    whl_abs = "abstract"
                    OpenFile(selected_research)

                End If
            ElseIf e.ColumnIndex = 10 Then
                Dim open_file As DialogResult
                open_file = MessageBox.Show("Open this whole file?", "Click Yes to proceed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If open_file = DialogResult.Yes Then
                    Dim i As Integer = DgvSwData.CurrentRow.Index
                    selected_research = DgvSwData.Item(1, i).Value
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
            DgvSwData.Rows(i).Height = 35
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
        If e.ColumnIndex = 9 Then
            DgvSwData.Cursor = Cursors.Hand
        ElseIf e.ColumnIndex = 10 Then
            DgvSwData.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub DgvSwData_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellMouseLeave
        If e.ColumnIndex = 9 Then
            DgvSwData.Cursor = Cursors.Default
        ElseIf e.ColumnIndex = 10 Then
            DgvSwData.Cursor = Cursors.Default
        End If
    End Sub

    'OPENING AND CLOSING OF FILTER PANEL 
    Dim open_close_filter As Integer = 0
    Private Sub BtnFilter_Click(sender As Object, e As EventArgs) Handles BtnFilter.Click
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
        If PnlFilter.Width >= 500 Then
            TmOpenFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width + 500
            PnlFilter.Height = PnlFilter.Height + 400
        End If
    End Sub
    Private Sub TmCloseFilter_Tick(sender As Object, e As EventArgs) Handles TmCloseFilter.Tick
        If PnlFilter.Width <= 0 Then
            TmCloseFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width - 500
            PnlFilter.Height = PnlFilter.Height - 400
        End If
    End Sub
    Private Sub BtnCloseFilter_Click(sender As Object, e As EventArgs) Handles BtnCloseFilter.Click
        TmCloseFilter.Enabled = True
        open_close_filter = 0
    End Sub

    'CHECKING ALL CHECK BOX AT ONCE
    Private Sub CbxAllRole_CheckedChanged(sender As Object, e As EventArgs) Handles CbxAllRole.CheckedChanged
        If CbxAllRole.Checked Then
            CbxFaculty.Checked = True
            CbxAdmin.Checked = True
            CbxStud.Checked = True
            CbxStaff.Checked = True
        Else
            CbxFaculty.Checked = False
            CbxAdmin.Checked = False
            CbxStud.Checked = False
            CbxStaff.Checked = False
        End If
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
                        "DELETE FROM sw_whole_file WHERE whole_file_id = @to_delete_id"
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
                            LoadScholarlyWorks("default")
                            BtnRemoveSelection.PerformClick()
                            BtnRemoveSelection.Visible = False
                            BtnDelete.Enabled = False
                            MessageBox.Show("Successfuly Deleted Item.", "Deleted Successfully.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            transaction.Rollback()
                            MessageBox.Show("Error Occurred: " & ex.Message, "Failed to delete selected item.", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    'SEARCHING FUNCTION
    Private Sub Search()

    End Sub


End Class