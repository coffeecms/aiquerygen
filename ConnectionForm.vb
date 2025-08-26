Imports System.Data
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports Npgsql
Imports Microsoft.Data.Sqlite

Public Class ConnectionForm
    Private dbType As String
    Private connection As IDbConnection

    Public Sub New(selectedDbType As String)
        InitializeComponent()
        dbType = selectedDbType
        ConfigureForDbType()
    End Sub

    Private Sub ConfigureForDbType()
        Me.Text = $"Connect to {dbType}"
        labelTitle.Text = $"Connect to {dbType} Database"
        
        Select Case dbType.ToLower()
            Case "sqlite"
                ' SQLite only needs file path
                labelServer.Visible = False
                textBoxServer.Visible = False
                labelPort.Visible = False
                textBoxPort.Visible = False
                labelUsername.Visible = False
                textBoxUsername.Visible = False
                labelPassword.Visible = False
                textBoxPassword.Visible = False
                
                labelDatabase.Text = "Database File:"
                buttonBrowse.Visible = True
                
            Case "sql server"
                textBoxPort.Text = "1433"
                
            Case "mysql"
                textBoxPort.Text = "3306"
                
            Case "postgresql"
                textBoxPort.Text = "5432"
                
            Case "oracle"
                textBoxPort.Text = "1521"
        End Select
    End Sub

    Private Sub buttonBrowse_Click(sender As Object, e As EventArgs) Handles buttonBrowse.Click
        Using openDialog As New OpenFileDialog()
            openDialog.Filter = "SQLite Database Files (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3|All Files (*.*)|*.*"
            openDialog.Title = "Select SQLite Database File"
            
            If openDialog.ShowDialog() = DialogResult.OK Then
                textBoxDatabase.Text = openDialog.FileName
            End If
        End Using
    End Sub

    Private Sub buttonTest_Click(sender As Object, e As EventArgs) Handles buttonTest.Click
        Try
            Dim testConnection As IDbConnection = CreateConnection()
            testConnection.Open()
            testConnection.Close()
            testConnection.Dispose()
            
            MessageBox.Show("Connection successful!", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            
        Catch ex As Exception
            MessageBox.Show($"Connection failed: {ex.Message}", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonConnect_Click(sender As Object, e As EventArgs) Handles buttonConnect.Click
        Try
            connection = CreateConnection()
            connection.Open()
            Me.DialogResult = DialogResult.OK
            
        Catch ex As Exception
            MessageBox.Show($"Connection failed: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function CreateConnection() As IDbConnection
        Dim connectionString As String = BuildConnectionString()
        
        Select Case dbType.ToLower()
            Case "sql server"
                Return New SqlConnection(connectionString)
                
            Case "mysql"
                Return New MySqlConnection(connectionString)
                
            Case "postgresql"
                Return New NpgsqlConnection(connectionString)
                
            Case "sqlite"
                Return New SqliteConnection(connectionString)
                
            Case "oracle"
                ' Oracle connection would need Oracle.ManagedDataAccess.Client
                Throw New NotImplementedException("Oracle connection not yet implemented")
                
            Case Else
                Throw New ArgumentException($"Unsupported database type: {dbType}")
        End Select
    End Function

    Private Function BuildConnectionString() As String
        Select Case dbType.ToLower()
            Case "sql server"
                Dim builder As New SqlConnectionStringBuilder()
                builder.DataSource = If(String.IsNullOrEmpty(textBoxPort.Text), 
                                      textBoxServer.Text, 
                                      $"{textBoxServer.Text},{textBoxPort.Text}")
                builder.InitialCatalog = textBoxDatabase.Text
                
                If checkBoxWindowsAuth.Checked Then
                    builder.IntegratedSecurity = True
                Else
                    builder.UserID = textBoxUsername.Text
                    builder.Password = textBoxPassword.Text
                End If
                
                builder.ConnectTimeout = 30
                Return builder.ConnectionString
                
            Case "mysql"
                Dim builder As New MySqlConnectionStringBuilder()
                builder.Server = textBoxServer.Text
                builder.Port = If(String.IsNullOrEmpty(textBoxPort.Text), 3306, Convert.ToUInt32(textBoxPort.Text))
                builder.Database = textBoxDatabase.Text
                builder.UserID = textBoxUsername.Text
                builder.Password = textBoxPassword.Text
                builder.ConnectionTimeout = 30
                Return builder.ConnectionString
                
            Case "postgresql"
                Dim builder As New NpgsqlConnectionStringBuilder()
                builder.Host = textBoxServer.Text
                builder.Port = If(String.IsNullOrEmpty(textBoxPort.Text), 5432, Convert.ToInt32(textBoxPort.Text))
                builder.Database = textBoxDatabase.Text
                builder.Username = textBoxUsername.Text
                builder.Password = textBoxPassword.Text
                builder.Timeout = 30
                Return builder.ConnectionString
                
            Case "sqlite"
                Dim builder As New SqliteConnectionStringBuilder()
                builder.DataSource = textBoxDatabase.Text
                Return builder.ConnectionString
                
            Case Else
                Throw New ArgumentException($"Unsupported database type: {dbType}")
        End Select
    End Function

    Public Function GetConnection() As IDbConnection
        Return connection
    End Function
    
    Public Function GetConnectionString() As String
        Return BuildConnectionString()
    End Function

    Private Sub checkBoxWindowsAuth_CheckedChanged(sender As Object, e As EventArgs) Handles checkBoxWindowsAuth.CheckedChanged
        Dim isEnabled As Boolean = Not checkBoxWindowsAuth.Checked
        textBoxUsername.Enabled = isEnabled
        textBoxPassword.Enabled = isEnabled
        labelUsername.Enabled = isEnabled
        labelPassword.Enabled = isEnabled
    End Sub
End Class
