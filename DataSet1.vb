

Partial Class DataSet1
    Partial Public Class DataTable1DataTable
        Private Sub DataTable1DataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.co_authorsColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class
End Class
