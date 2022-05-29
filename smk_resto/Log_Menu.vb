Imports System.Data.SqlClient
Imports System.IO
Public Class Log_Menu
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Manager_Navigation.Show()
    End Sub

    Sub aturDGV()
        sql = "Select * from LogMenu"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "LogMenu")
        DataGridView1.DataSource = ds.Tables("LogMenu")
    End Sub

    Private Sub Log_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturDGV()
    End Sub
End Class