Imports System.Data.SqlClient
Imports System.IO
Public Class Order
    Sub aturDGV()
        sql = "Select * from MsMenu"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "MsMenu")
        DataGridView1.DataSource = ds.Tables("MsMenu")

        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(3).Visible = False
    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value
        TextBox4.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value
        TextBox5.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value
        PictureBox1.ImageLocation = TextBox4.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        TextBox2.Focus()
    End Sub

    Private Sub Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturDGV()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox6.Text = Val(TextBox2.Text) * (TextBox5.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sum As Integer
        DataGridView2.Rows.Add(TextBox1.Text, TextBox2.Text, TextBox5.Text, TextBox6.Text)
        For baris1 As Integer = 0 To DataGridView2.Rows.Count - 1
            sum = sum + DataGridView2.Rows(baris1).Cells(3).Value
        Next
        Label5.Text = sum

        PictureBox1.ImageLocation = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView2.CurrentRow.Index <> DataGridView2.NewRowIndex Then
            DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For baris As Integer = 0 To DataGridView2.Rows.Count - 2
            connect()
            sql = "Select * from OrderDetail where Id in (Select max(Id) from OrderDetail)"
            cmd = New SqlCommand(sql, con)

            Dim kode, urutankode As String
            Dim hitung As Integer
            Dim hit As Long
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                kode = "1"
                urutankode = Format(Now, "yyyyMMdd") + "0001"
            Else
                hitung = Microsoft.VisualBasic.Right(rd.GetInt32(0), 1) + 1
                kode = Microsoft.VisualBasic.Right(hitung, 1)
                hit = Microsoft.VisualBasic.Right(rd.GetInt32(0), 11) + 1
                urutankode = Format(Now, "yyyyMMdd") + Microsoft.VisualBasic.Right("000" & hit, 4)
            End If
            rd.Close()

            Try
                sql = "INSERT INTO OrderDetail(Id,Orderid,Menuid,Qty,Status,TotalDetail) VALUES ('" & kode & "','" & urutankode & "','" & TextBox3.Text & "','" & DataGridView2.Rows(baris).Cells(1).Value & "','Bill','" & DataGridView2.Rows(baris).Cells(3).Value & "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MsgBox("Sukses Tambah Data")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub
End Class