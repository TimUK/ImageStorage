Imports System.IO
Imports MySql.Data.MySqlClient

Public Class MainWindow

    Private Sub RefreshFiles()
        ListBox1.Items.Clear()
        Try
            'Globals.conn.Open()

            Dim cmd As MySqlCommand = New MySqlCommand("select * from files", Globals.conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                ListBox1.Items.Add(reader.GetInt32(0).ToString + "-" + reader.GetString(1))
            End While
            reader.Close()


        Catch ex As MySqlException

        Finally
            '   Globals.conn.Close()
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Globals.conn.Open()
        RefreshFiles()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()

    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

        Dim fs As IO.Stream = OpenFileDialog1.OpenFile()
        Dim br As New IO.BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Integer))
        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)


        Try
            Dim cmd As New MySqlCommand("insert into files (filename,data) values (@Name,@Data)", Globals.conn)
            cmd.Prepare()
            cmd.Parameters.AddWithValue("@Name", OpenFileDialog1.FileName.Split("\").Last())
            cmd.Parameters.AddWithValue("@Data", base64String)
            cmd.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show("Error! Perhaps the file is too large or the server is unavailable? " + vbCrLf + vbCrLf + vbCrLf + "Heres a stack trace of the error:" + vbCrLf + ex.ToString())
        Finally
            '   Globals.conn.Close()
        End Try


        RefreshFiles()
    End Sub

    Private Sub OpenImage()
        If ListBox1.SelectedIndex > 0 Then

            Globals.currentTitle = ListBox1.SelectedItem.ToString.Split("-").ElementAt(1)
            Globals.currentViewing = Integer.Parse(ListBox1.SelectedItem.ToString.Split("-").ElementAt(0))
            Dim ViewForm As View
            ViewForm = New View()
            ViewForm.ShowDialog()

            ViewForm = Nothing

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenImage()
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        OpenImage()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class
