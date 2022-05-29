Imports System.Data.SqlClient
Imports System.IO
Public Class Manage_Menu
    Sub aturDGV()
        sql = "Select * from MsMenu"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "MsMenu")
        DataGridView1.DataSource = ds.Tables("MsMenu")
    End Sub
    Sub kosong()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox5.Text = ""
        PictureBox1.ImageLocation = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        sql = "SELECT * FROM MsMenu WHERE (Id LIKE '%" & TextBox1.Text & "%' or Name LIKE '%" & TextBox1.Text & "%' or Price LIKE '%" & TextBox1.Text & "%')"
        da = New SqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "MsMenu")
        DataGridView1.DataSource = ds.Tables("MsMenu")
    End Sub
    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value
        TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(1).Value
        TextBox4.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value
        TextBox5.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value
        PictureBox1.ImageLocation = TextBox5.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim a As FileDialog = New OpenFileDialog
        OpenFileDialog1.Filter = "*.jpg|"
        OpenFileDialog1.ShowDialog()
        TextBox5.Text = OpenFileDialog1.FileName
        PictureBox1.ImageLocation = TextBox5.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Manage_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        aturDGV()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Insert" Then
            Button3.Text = "Save"
            Button4.Text = "Cancel"

            connect()
            sql = "Select * from MsMenu where Id in (Select max(Id) from MsMenu)"
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
            TextBox2.Text = kode
            TextBox3.Focus()
        Else
            If Button3.Text = "Save" Then
                If TextBox3.Text = "" And TextBox4.Text = "" And TextBox5.Text = "" Then
                    MsgBox("Pastikan Field Terisi Semua")
                Else
                    Try
                        connect()
                        sql = "INSERT INTO MsMenu(Id,Name,Price,Photo) VALUES ('" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Sukses tambah data")
                        aturDGV()
                        kosong()
                        Button3.Text = "Insert"
                        Button4.Text = "Update"
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            Else
                kosong()
                Button3.Text = "Insert"
                Button4.Text = "Update"
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Update" Then
            Button4.Text = "Save"
            Button5.Text = "Cancel"
        Else
            If Button4.Text = "Save" Then
                If TextBox3.Text = "" Then
                    MsgBox("Silahkan Pilih data yg ingin diubah")
                Else
                    Try
                        connect()
                        sql = "UPDATE MsMenu set Name='" & TextBox3.Text & "', Price = '" & TextBox4.Text & "', Photo = '" & TextBox5.Text & "' FROM MsMenu WHERE Id='" & TextBox2.Text & "'"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Sukses edit data")
                        aturDGV()
                        kosong()
                        Button5.Text = "Delete"
                        Button4.Text = "Update"
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If

            Else
                kosong()
                Button3.Text = "Insert"
                Button4.Text = "Update"
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Button5.Text = "Cancel" Then
            kosong()
            Button5.Text = "Delete"
            Button4.Text = "Update"
        Else

            If TextBox2.Text = "" Then
                MsgBox("silahkan pilih data yang akan di hapus")
            Else
                Try
                    connect()
                    sql = "DELETE MsMenu WHERE Id ='" & TextBox2.Text & "'"
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
        Manager_Navigation.Show()
    End Sub


    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub
End Class