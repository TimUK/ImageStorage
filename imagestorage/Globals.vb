Imports MySql.Data.MySqlClient
Public Class Globals

    Public Shared ConnectionBase As String = "Server={0};Database={1};User Id={2};Password={3}"
    Public Shared ConnectionString As String = String.Format(ConnectionBase, Constants.ServerName, Constants.Database, Constants.Username, Constants.Password)
    Public Shared conn As New MySqlConnection(Globals.ConnectionString)

    Public Shared currentViewing As Int32 = 0
    Public Shared currentTitle As String = ""
End Class
