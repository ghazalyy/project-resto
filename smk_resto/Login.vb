Imports System.Data.SqlClient
Imports System.IO
Public Class Login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("Ada Data Yang Belum Diisi")
        Else
            sql = "SELECT Id,Name,Email,Password,Position FROM MsEmployee WHERE Email='" & Trim(TextBox1.Text) & "' AND Password='" & Trim(TextBox2.Text) & "' "
            cmd = New SqlCommand(sql, con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                If rd.Item("Position").Equals("admin") Then
                    Admin_Navigation.Show()
                    Admin_Navigation.Label3.Text = rd.Item("Name")
                    Me.Hide()
                ElseIf rd.Item("Position").Equals("manager") Then
                    Manager_Navigation.Show()
                    Manager_Navigation.Label3.Text = rd.Item("Name")
                    Me.Hide()
                ElseIf rd.Item("Position").Equals("cashier") Then
                    Cashier_Navigation.Show()
                    Cashier_Navigation.Label3.Text = rd.Item("Name")
                    Me.Hide()
                Else
                    MsgBox("Silahkan Coba Lagi, Data Salah!!")
                    rd.Close()
                End If
            Else
                MsgBox("Silahkan Coba Lagi, Data Salah!!")
                rd.Close()
            End If
        End If
    End Sub
End Class