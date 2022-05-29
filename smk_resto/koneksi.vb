Imports System.Data.SqlClient
Module koneksi
    Public con As SqlConnection
    Public ds As DataSet
    Public rd As SqlDataReader
    Public da As SqlDataAdapter
    Public cmd As SqlCommand
    Public sql As String

    Public Sub connect()
        sql = "Data source=localhost\MSSQLSERVER01; Initial Catalog=restoran; Integrated Security=True"
        con = New SqlConnection(sql)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Module
