Imports System.Data.SqlClient
Public Class Payment
    Sub aturDGV()
        sql = "Select Name, Qty, Price, TotalDetail from MsMenu, OrderDetail WHERE MsMenu.Id=OrderDetail.Menuid AND OrderDetail.Orderid LIKE '%" & ComboBox1.Text & "%' AND OrderDetail.Status='Bill'"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "OrderDetail")
        DataGridView1.DataSource = ds.Tables("OrderDetail")

        Dim sum As Integer
        For baris1 As Integer = 0 To DataGridView1.Rows.Count - 1
            sum = sum + DataGridView1.Rows(baris1).Cells(3).Value
        Next
        Label9.Text = sum
    End Sub
    Sub aturCombo1()
        sql = "Select * from OrderDetail"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(1))
        Loop
        rd.Close()
    End Sub
    Sub aturComboemploy()
        sql = "Select * from MsEmployee"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox4.Items.Add(rd.Item(0))
        Loop
        rd.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        connect()
        aturDGV()
    End Sub

    Private Sub Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturCombo1()
        aturComboemploy()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Cash" Then
            TextBox2.Visible = True
            TextBox3.Visible = True
            TextBox1.Text = "0"
        Else
            If ComboBox2.Text = "Credit" Then
                TextBox2.Visible = False
                TextBox3.Visible = False
            End If
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox3.Text = Val(TextBox2.Text) - Val(Label9.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        sql = "Select * from OrderHeader where Id in(Select max(Id) from OrderHeader)"
        cmd = New SqlCommand(sql, con)

        Dim kode As String
        Dim hitung As Integer

        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            kode = "1"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetInt32(0), 1) + 1
            kode = Microsoft.VisualBasic.Right(hitung, 1)
        End If
        rd.Close()
        Try
            sql = "INSERT INTO OrderHeader(Id,Employeeid,Date,PaymentType,CardNumber,Bank,Orderid,Total) VALUES ('" & kode & "','" & ComboBox4.Text & "','" & Format(DateTimePicker1.Value, "yyyyMMdd") & "','" & ComboBox2.Text & "','" & TextBox1.Text & "','" & ComboBox3.Text & "','" & ComboBox1.Text & "','" & Label9.Text & "')"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        MsgBox("Sukses Tambah Data")
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub
End Class