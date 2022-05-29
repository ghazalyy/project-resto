Imports System.Data.SqlClient
Imports System.IO
Public Class Log_Activity_Peg
    Sub aturDGV()
        sql = "Select * from LogEmployee"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "LogEmployee")
        DataGridView1.DataSource = ds.Tables("LogEmployee")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Cashier_Navigation.Show()
    End Sub

    Private Sub Log_Activity_Peg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturDGV()
    End Sub
End Class