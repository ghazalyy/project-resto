Imports System.Data.SqlClient
Imports System.IO
Public Class Manage_User
    Sub aturDGV()
        sql = "Select * from MsEmployee"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "MsEmployee")
        DataGridView1.DataSource = ds.Tables("MsEmployee")
    End Sub
    Sub kosong()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value
        TextBox4.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value
        TextBox5.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(4).Value
        ComboBox1.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(5).Value
    End Sub

    Private Sub Manage_User_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturDGV()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Insert" Then
            Button1.Text = "Save"
            Button2.Text = "Cancel"

        Else
            If Button1.Text = "Save" Then
                If TextBox1.Text = "" And TextBox2.Text = "" And TextBox3.Text = "" And TextBox4.Text = "" And TextBox5.Text = "" And ComboBox1.Text = "" Then
                    MsgBox("Pastikan Field Terisi Semua")
                Else
                    Try
                        connect()
                        sql = "INSERT INTO MsEmployee(Id,Name,Email,Password,Handphone,Position) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & ComboBox1.Text & "')"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Sukses tambah data")
                        aturDGV()
                        kosong()
                        Button1.Text = "Insert"
                        Button2.Text = "Update"
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            Else
                kosong()
                Button1.Text = "Insert"
                Button2.Text = "Update"
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Update" Then
            Button2.Text = "Save"
            Button3.Text = "Cancel"
        Else
            If Button2.Text = "Save" Then
                If TextBox1.Text = "" Then
                    MsgBox("Silahkan Pilih data yg ingin diubah")
                Else
                    Try
                        connect()
                        sql = "UPDATE MsEmployee set Id='" & TextBox1.Text & "', Name='" & TextBox2.Text & "', Email = '" & TextBox3.Text & "', Password = '" & TextBox4.Text & "', Position = '" & ComboBox1.Text & "' FROM MsEmployee WHERE Id='" & TextBox1.Text & "'"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Sukses edit data")
                        aturDGV()
                        kosong()
                        Button3.Text = "Delete"
                        Button2.Text = "Update"
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If

            Else
                kosong()
                Button1.Text = "Insert"
                Button2.Text = "Update"
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Cancel" Then
            kosong()
            Button3.Text = "Delete"
            Button2.Text = "Update"
        Else

            If TextBox1.Text = "" Then
                MsgBox("silahkan pilih data yang akan di hapus")
            Else
                Try
                    connect()
                    sql = "DELETE MsEmployee WHERE Id ='" & TextBox1.Text & "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Sukses hapus data")
                    aturDGV()
                    kosong()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub
End Class