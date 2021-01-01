Imports MySql.Data.MySqlClient
Public Class Globals

    Public Shared ConnectionString As String = "Server=192.168.8.201;Database=imagestorage;User Id=root;Password=tornado12"

    Public Shared conn As New MySqlConnection(Globals.ConnectionString)

    Public Shared currentViewing As Int32 = 0
    Public Shared currentTitle As String = ""
End Class
