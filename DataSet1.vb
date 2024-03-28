

Partial Class DataSet1
    Partial Public Class scholarly_worksDataTable
        Private Sub scholarly_worksDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.co_authorsColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub scholarly_worksDataTable_scholarly_worksRowChanging(sender As Object, e As scholarly_worksRowChangeEvent) Handles Me.scholarly_worksRowChanging

        End Sub

    End Class
End Class
