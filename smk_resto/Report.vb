Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Report
    Sub aturDGV()
        sql = "Select Id, EmployeeId,Date,PaymentType, CardNumber, Bank ,Total from OrderHeader where Date between '" & DateTimePicker1.Value.Date.ToString("MM/dd/yyyy") & "' and '" & DateTimePicker2.Value.Date.ToString("MM/dd/yyyy") & "' "
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "OrderHeader")
        DataGridView1.DataSource = ds.Tables("OrderHeader")
    End Sub
    Sub aturChart()
        sql = "Select Date,Total from OrderHeader where Date between '" & DateTimePicker1.Value.Date.ToString("MM/dd/yyyy") & "' and '" & DateTimePicker2.Value.Date.ToString("MM/dd/yyyy") & "' "
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            Chart1.Series("Income").Points.AddXY(Microsoft.VisualBasic.Left(rd("Date").ToString, 10), rd("Total").ToString)
        Loop
        rd.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        aturDGV()
        aturChart()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Manager_Navigation.Show()
    End Sub

End Class