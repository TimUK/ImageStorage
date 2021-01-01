Imports System.IO
Imports MySql.Data.MySqlClient
Public Class View
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub View_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Globals.conn.Open()

            Me.Text = Globals.currentTitle


            Dim cmd As MySqlCommand = New MySqlCommand("select * from files where id=" + Globals.currentViewing.ToString, Globals.conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                ' ListBox1.Items.Add(reader.GetInt32(0).ToString + " - " + reader.GetString(1))
                Dim b64b As Byte() = Convert.FromBase64String(reader.GetString(2))
                Dim ms As MemoryStream = New MemoryStream(b64b)
                Dim image = Drawing.Image.FromStream(ms)
                PictureBox1.Image = image

            End While
            reader.Close()

            Me.Width = PictureBox1.Image.Width + 20
            PictureBox1.Width = Me.Width

            Me.Height = PictureBox1.Image.Height + 50
            PictureBox1.Height = Me.Height


        Catch ex As MySqlException
            'TextBox1.Text = TextBox1.Text + "Error: " + ex.ToString()
        Finally
            '   Globals.conn.Close()
        End Try
    End Sub
End Class