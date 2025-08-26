Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json
Imports System.String
Imports System.Text
Imports MySql.Data.MySqlClient
Imports Npgsql
Imports Microsoft.Data.Sqlite

Public Class Form1
    Private currentConnection As IDbConnection
    Private currentDbType As String = ""
    Private metadata As New Dictionary(Of String, Object)
    Private aiConfig As AIConfiguration
    Private aiClient As AIClient

    ' Database connections management
    Private savedConnections As New List(Of DatabaseConnectionInfo)
    Private Const DB_LOGS_FILE As String = "db_logs.json"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
        LoadAIConfiguration()
        LoadSavedConnections()
    End Sub

    Private Sub InitializeForm()
        ' Set default values
        comboBoxDBType.SelectedIndex = 0
        textBoxNaturalQuery.Text = ""
        textBoxNaturalQuery.ForeColor = Color.Gray

        ' Initialize result info
        labelResultsInfo.Text = "No query executed yet"

        ' Set status
        statusLabel.Text = "Ready to connect to database"
        connectionStatusLabel.Text = "Not Connected"
        connectionStatusLabel.ForeColor = Color.Red

        ' Disable buttons initially
        buttonGenerateSQL.Enabled = False
        buttonExecuteSQL.Enabled = False
        buttonSaveSQL.Enabled = False
        buttonCopySQL.Enabled = False
        analyzeDBToolStripMenuItem.Enabled = False
        manageMETADATAToolStripMenuItem.Enabled = False
        manageIndexesToolStripMenuItem.Enabled = False
    End Sub

    Private Sub LoadAIConfiguration()
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            If File.Exists(configPath) Then
                Dim json As String = File.ReadAllText(configPath)
                aiConfig = JsonConvert.DeserializeObject(Of AIConfiguration)(json)
            Else
                aiConfig = New AIConfiguration()
            End If

            ' Initialize AI client
            aiClient = New AIClient(aiConfig)

            ' Update status
            statusLabel.Text = $"AI Endpoint: {aiConfig.CustomEndpoint} | Model: {aiConfig.ModelName}"

        Catch ex As Exception
            aiConfig = New AIConfiguration()
            aiClient = New AIClient(aiConfig)
            statusLabel.Text = "AI configuration not found - using defaults"
        End Try
    End Sub

    Private Sub textBoxNaturalQuery_Enter(sender As Object, e As EventArgs) Handles textBoxNaturalQuery.Enter
        If textBoxNaturalQuery.ForeColor = Color.Gray Then
            textBoxNaturalQuery.Text = ""
            textBoxNaturalQuery.ForeColor = Color.Black
        End If
    End Sub

    Private Sub textBoxNaturalQuery_Leave(sender As Object, e As EventArgs) Handles textBoxNaturalQuery.Leave
        If String.IsNullOrWhiteSpace(textBoxNaturalQuery.Text) Then

            textBoxNaturalQuery.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub buttonConnect_Click(sender As Object, e As EventArgs) Handles buttonConnect.Click
        Try
            ShowConnectionDialog()
        Catch ex As Exception
            MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowConnectionDialog()
        Dim connectionForm As New ConnectionForm(comboBoxDBType.SelectedItem.ToString())
        If connectionForm.ShowDialog() = DialogResult.OK Then
            Try
                currentConnection = connectionForm.GetConnection()
                currentDbType = comboBoxDBType.SelectedItem.ToString()

                ' Update UI
                labelCurrentDB.Text = $"Connected to {currentDbType}"
                labelCurrentDB.ForeColor = Color.Green
                connectionStatusLabel.Text = "Connected"
                connectionStatusLabel.ForeColor = Color.Green
                statusLabel.Text = "Database connected successfully"

                ' Enable buttons
                buttonGenerateSQL.Enabled = True
                analyzeDBToolStripMenuItem.Enabled = True
                manageMETADATAToolStripMenuItem.Enabled = True
                manageIndexesToolStripMenuItem.Enabled = True

                ' Save connection to history
                Dim connectionName As String = GenerateConnectionName(connectionForm.GetConnectionString())
                AddConnectionToSaved(connectionName, currentDbType, connectionForm.GetConnectionString())

                ' Load database structure
                LoadDatabaseStructure()

            Catch ex As Exception
                MessageBox.Show($"Failed to connect: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub LoadDatabaseStructure()
        Try
            If currentConnection Is Nothing OrElse currentConnection.State <> ConnectionState.Open Then
                Return
            End If

            treeViewTables.Nodes.Clear()
            statusLabel.Text = "Loading database structure..."

            ' Use DatabaseAnalyzer to get real structure
            Dim analyzer As New DatabaseAnalyzer(currentConnection, currentDbType)
            Dim tables = analyzer.AnalyzeDatabase()

            If tables.Count > 0 Then
                ' Update metadata cache
                metadata("tables") = tables

                ' Update tree view
                UpdateTreeViewWithMetadata(tables)
            Else
                ' Fallback to simple structure if analysis fails
                Dim rootNode As New TreeNode($"{currentDbType} Database")
                rootNode.Nodes.Add("No tables found or analysis failed")
                treeViewTables.Nodes.Add(rootNode)
                rootNode.Expand()
            End If

            statusLabel.Text = "Database structure loaded"

        Catch ex As Exception
            MessageBox.Show($"Error loading database structure: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            statusLabel.Text = "Error loading database structure"

            ' Fallback to simple display
            Try
                Dim rootNode As New TreeNode($"{currentDbType} Database")
                rootNode.Nodes.Add("Error loading structure")
                treeViewTables.Nodes.Add(rootNode)
                rootNode.Expand()
            Catch
                ' Ignore fallback errors
            End Try
        End Try
    End Sub

    Private Sub buttonGenerateSQL_Click(sender As Object, e As EventArgs) Handles buttonGenerateSQL.Click
        If String.IsNullOrWhiteSpace(textBoxNaturalQuery.Text) OrElse
           textBoxNaturalQuery.ForeColor = Color.Gray Then
            MessageBox.Show("Please enter a natural language query.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            textBoxNaturalQuery.Focus()
            Return
        End If

        Try
            statusLabel.Text = "Generating SQL query using AI..."
            buttonGenerateSQL.Enabled = False

            ' Generate SQL using AI or fallback
            GenerateSQLWithAI()

        Catch ex As Exception
            MessageBox.Show($"Error generating SQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            statusLabel.Text = "Error generating SQL query"
        Finally
            buttonGenerateSQL.Enabled = True
        End Try
    End Sub

    Private Async Sub GenerateSQLWithAI()
        Try
            Dim generatedSQL As String = ""

            ' Check if we have AI configuration and database schema
            If aiClient IsNot Nothing AndAlso currentConnection IsNot Nothing Then
                ' Get database schema for AI context
                Dim schemaInfo As String = GetDatabaseSchemaInfo()

                If Not String.IsNullOrEmpty(schemaInfo) Then
                    ' Use AI to generate SQL
                    generatedSQL = Await aiClient.GenerateSQLAsync(textBoxNaturalQuery.Text, schemaInfo)
                    statusLabel.Text = "SQL query generated using AI"
                Else
                    ' Fallback to basic generation
                    generatedSQL = GenerateBasicSQL(textBoxNaturalQuery.Text)
                    statusLabel.Text = "SQL query generated using basic rules (no schema available)"
                End If
            Else
                ' Fallback to basic generation
                ''generatedSQL = GenerateBasicSQL(textBoxNaturalQuery.Text)
                statusLabel.Text = "SQL query generated using basic rules"
            End If

            textBoxGeneratedSQL.Text = generatedSQL
            buttonExecuteSQL.Enabled = True
            buttonSaveSQL.Enabled = True
            buttonCopySQL.Enabled = True

        Catch ex As Exception
            ' Fallback to basic generation on AI error
            Dim generatedSQL As String = GenerateBasicSQL(textBoxNaturalQuery.Text)
            textBoxGeneratedSQL.Text = generatedSQL

            buttonExecuteSQL.Enabled = True
            buttonSaveSQL.Enabled = True
            buttonCopySQL.Enabled = True

            statusLabel.Text = "AI generation failed - used basic rules"
        End Try
    End Sub

    Private Function GetDatabaseSchemaInfo() As String
        Try
            If metadata.ContainsKey("tables") Then
                Dim tables As List(Of DatabaseMetadata) = DirectCast(metadata("tables"), List(Of DatabaseMetadata))
                Dim schema As New StringBuilder()

                ' Add database type information at the beginning
                schema.AppendLine($"Database Type: {currentDbType}")
                schema.AppendLine("IMPORTANT: Please generate SQL syntax that is compatible with this specific database type.")

                ' Add database-specific syntax notes
                Select Case currentDbType.ToLower()
                    Case "sql server"
                        schema.AppendLine("- Use SQL Server syntax (T-SQL)")
                        schema.AppendLine("- Use square brackets [table_name] for identifiers if needed")
                        schema.AppendLine("- Use TOP N for limiting results")
                        schema.AppendLine("- Use GETDATE() for current date/time")
                    Case "mysql"
                        schema.AppendLine("- Use MySQL syntax")
                        schema.AppendLine("- Use backticks `table_name` for identifiers if needed")
                        schema.AppendLine("- Use LIMIT N for limiting results")
                        schema.AppendLine("- Use NOW() for current date/time")
                    Case "postgresql"
                        schema.AppendLine("- Use PostgreSQL syntax")
                        schema.AppendLine("- Use double quotes ""table_name"" for identifiers if needed")
                        schema.AppendLine("- Use LIMIT N for limiting results")
                        schema.AppendLine("- Use NOW() for current date/time")
                    Case "sqlite"
                        schema.AppendLine("- Use SQLite syntax")
                        schema.AppendLine("- Use square brackets [table_name] or double quotes for identifiers if needed")
                        schema.AppendLine("- Use LIMIT N for limiting results")
                        schema.AppendLine("- Use datetime('now') for current date/time")
                End Select

                schema.AppendLine()
                schema.AppendLine("Database Schema:")
                schema.AppendLine()

                For Each table In tables
                    schema.AppendLine($"Table: {table.TableName}")
                    If Not String.IsNullOrEmpty(table.Description) Then
                        schema.AppendLine($"Description: {table.Description}")
                    End If

                    schema.AppendLine("Columns:")
                    For Each column In table.Columns
                        Dim columnInfo As String = $"  - {column.ColumnName} ({column.DataType})"
                        If column.IsPrimaryKey Then columnInfo += " [PRIMARY KEY]"
                        If column.IsForeignKey Then columnInfo += " [FOREIGN KEY]"
                        If Not String.IsNullOrEmpty(column.Description) Then
                            columnInfo += $" - {column.Description}"
                        End If
                        schema.AppendLine(columnInfo)
                    Next
                    schema.AppendLine()
                Next

                Return schema.ToString()
            End If

            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GenerateBasicSQL(naturalQuery As String) As String
        Try
            ' If we have metadata, use the advanced NLP processor
            If metadata.Count > 0 AndAlso metadata.ContainsKey("tables") Then
                Dim tableMetadata As List(Of DatabaseMetadata) = DirectCast(metadata("tables"), List(Of DatabaseMetadata))
                Dim nlp As New NaturalLanguageProcessor(tableMetadata, currentDbType)
                Return nlp.GenerateSQL(naturalQuery)
            End If
        Catch ex As Exception
            ' Fall back to basic generation if advanced fails
        End Try

        ' Fallback to basic rule-based SQL generation
        Dim query As String = naturalQuery.ToLower().Trim()
        Dim sql As New System.Text.StringBuilder()

        ' Simple keyword detection
        If query.Contains("tìm") OrElse query.Contains("find") OrElse query.Contains("select") Then
            sql.AppendLine("SELECT ")

            If query.Contains("tất cả") OrElse query.Contains("all") Then
                sql.AppendLine("    *")
            Else
                sql.AppendLine("    column1, column2, column3")
            End If

            sql.AppendLine("FROM table_name")

            If query.Contains("where") OrElse query.Contains("ở") OrElse query.Contains("tại") Then
                sql.AppendLine("WHERE condition = 'value'")
            End If

            If query.Contains("đếm") OrElse query.Contains("count") OrElse query.Contains("số lượng") Then
                sql.AppendLine("GROUP BY column_name")
            End If

        ElseIf query.Contains("thêm") OrElse query.Contains("insert") Then
            sql.AppendLine("INSERT INTO table_name (column1, column2, column3)")
            sql.AppendLine("VALUES (value1, value2, value3)")

        ElseIf query.Contains("cập nhật") OrElse query.Contains("update") Then
            sql.AppendLine("UPDATE table_name")
            sql.AppendLine("SET column1 = value1, column2 = value2")
            sql.AppendLine("WHERE condition = 'value'")

        ElseIf query.Contains("xóa") OrElse query.Contains("delete") Then
            sql.AppendLine("DELETE FROM table_name")
            sql.AppendLine("WHERE condition = 'value'")

        Else
            sql.AppendLine("-- Unable to interpret the natural language query")
            sql.AppendLine("-- Please try rephrasing your request")
            sql.AppendLine("-- Example: 'Find all customers in Hanoi'")
        End If

        Return sql.ToString()
    End Function

    Private Sub buttonExecuteSQL_Click(sender As Object, e As EventArgs) Handles buttonExecuteSQL.Click
        If String.IsNullOrWhiteSpace(textBoxGeneratedSQL.Text) Then
            MessageBox.Show("No SQL query to execute.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            statusLabel.Text = "Executing SQL query..."
            ExecuteQuery(textBoxGeneratedSQL.Text)

        Catch ex As Exception
            MessageBox.Show($"Error executing query: {ex.Message}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            statusLabel.Text = "Query execution failed"
        End Try
    End Sub

    Private Sub ExecuteQuery(sqlQuery As String)
        Try
            ' Ensure connection is open
            If currentConnection.State <> ConnectionState.Open Then
                currentConnection.Open()
            End If

            Using cmd As IDbCommand = currentConnection.CreateCommand()
                cmd.CommandText = sqlQuery

                ' Check if it's a SELECT query
                If sqlQuery.Trim().ToUpper().StartsWith("SELECT") Then
                    ' Use appropriate data adapter based on database type
                    Dim dataTable As New DataTable()

                    Select Case currentDbType.ToLower()
                        Case "sql server"
                            Using adapter As New SqlDataAdapter(DirectCast(cmd, SqlCommand))
                                adapter.Fill(dataTable)
                            End Using
                        Case "mysql"
                            Using adapter As New MySqlDataAdapter(DirectCast(cmd, MySqlCommand))
                                adapter.Fill(dataTable)
                            End Using
                        Case "postgresql"
                            Using adapter As New NpgsqlDataAdapter(DirectCast(cmd, NpgsqlCommand))
                                adapter.Fill(dataTable)
                            End Using
                        Case "sqlite"
                            ' SQLite doesn't have a DataAdapter, use DataReader instead
                            Using reader As IDataReader = cmd.ExecuteReader()
                                dataTable.Load(reader)
                            End Using
                        Case Else
                            ' Generic approach using DataReader
                            Using reader As IDataReader = cmd.ExecuteReader()
                                dataTable.Load(reader)
                            End Using
                    End Select

                    dataGridViewResults.DataSource = dataTable
                    labelResultsInfo.Text = $"{dataTable.Rows.Count} rows returned"
                    statusLabel.Text = "Query executed successfully"
                Else
                    ' For non-SELECT queries
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    dataGridViewResults.DataSource = Nothing
                    labelResultsInfo.Text = $"{rowsAffected} rows affected"
                    statusLabel.Text = "Command executed successfully"
                End If
            End Using

        Catch ex As Exception
            Throw New Exception($"Query execution failed: {ex.Message}")
        End Try
    End Sub

    Private Sub buttonCopySQL_Click(sender As Object, e As EventArgs) Handles buttonCopySQL.Click
        If Not String.IsNullOrWhiteSpace(textBoxGeneratedSQL.Text) Then
            Clipboard.SetText(textBoxGeneratedSQL.Text)
            statusLabel.Text = "SQL query copied to clipboard"
        End If
    End Sub

    Private Sub buttonSaveSQL_Click(sender As Object, e As EventArgs) Handles buttonSaveSQL.Click
        If String.IsNullOrWhiteSpace(textBoxGeneratedSQL.Text) Then
            MessageBox.Show("No SQL query to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            saveDialog.DefaultExt = "sql"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Try
                    File.WriteAllText(saveDialog.FileName, textBoxGeneratedSQL.Text)
                    statusLabel.Text = $"SQL query saved to {saveDialog.FileName}"
                Catch ex As Exception
                    MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub connectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles connectionToolStripMenuItem.Click
        buttonConnect_Click(sender, e)
    End Sub

    Private Sub exitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles exitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub analyzeDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles analyzeDBToolStripMenuItem.Click
        System.Diagnostics.Debug.WriteLine($"🔍 analyzeDBToolStripMenuItem_Click - currentConnection: {currentConnection?.GetType()?.Name}, State: {currentConnection?.State}")

        If currentConnection Is Nothing OrElse currentConnection.State <> ConnectionState.Open Then
            System.Diagnostics.Debug.WriteLine($"❌ Connection check failed - currentConnection is Nothing: {currentConnection Is Nothing}, State: {currentConnection?.State}")
            MessageBox.Show("Please connect to a database first.", "Connection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            statusLabel.Text = "Analyzing database structure..."

            Dim analyzer As New DatabaseAnalyzer(currentConnection, currentDbType)

            ' Load existing metadata for incremental analysis
            Dim metadataPath As String = Path.Combine(Application.StartupPath, "metadata.json")
            Dim existingMetadata As List(Of DatabaseMetadata) = DatabaseAnalyzer.LoadMetadata(metadataPath)

            Dim tables As List(Of DatabaseMetadata)
            Dim analysisMessage As String = ""

            If existingMetadata.Count > 0 Then
                ' Perform incremental analysis
                Dim incrementalResult = analyzer.AnalyzeDatabaseIncremental(existingMetadata)

                If incrementalResult.HasChanges Then
                    ' Show changes summary
                    Dim summaryForm As New Form()
                    summaryForm.Text = "Database Changes Detected"
                    summaryForm.Size = New Size(600, 400)
                    summaryForm.StartPosition = FormStartPosition.CenterParent

                    Dim txtSummary As New TextBox()
                    txtSummary.Dock = DockStyle.Fill
                    txtSummary.Multiline = True
                    txtSummary.ScrollBars = ScrollBars.Both
                    txtSummary.ReadOnly = True
                    txtSummary.Font = New Font("Consolas", 9)
                    txtSummary.Text = incrementalResult.GetSummary()

                    summaryForm.Controls.Add(txtSummary)
                    summaryForm.ShowDialog()

                    tables = incrementalResult.MergedMetadata
                    analysisMessage = $"Incremental analysis complete. {incrementalResult.AddedTables.Count} added, {incrementalResult.DeletedTables.Count} deleted, {incrementalResult.ModifiedTables.Count} modified tables."
                Else
                    tables = existingMetadata
                    analysisMessage = "No database changes detected since last analysis."
                End If
            Else
                ' Full analysis for first time
                tables = analyzer.AnalyzeDatabase()
                analysisMessage = $"Full database analysis complete. Found {tables.Count} tables."
            End If

            ' Update hashes for future incremental analysis
            For Each table In tables
                table.TableHash = table.CalculateStructureHash()
                table.LastAnalyzed = DateTime.Now
            Next

            ' Store metadata
            metadata("tables") = tables

            ' Update tree view with detailed structure
            UpdateTreeViewWithMetadata(tables)

            ' Save updated metadata to file
            DatabaseAnalyzer.SaveMetadata(tables, metadataPath)

            statusLabel.Text = analysisMessage
            MessageBox.Show(analysisMessage, "Analysis Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error analyzing database: {ex.Message}", "Analysis Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            statusLabel.Text = "Database analysis failed"
        End Try
    End Sub

    Private Sub UpdateTreeViewWithMetadata(tables As List(Of DatabaseMetadata))
        treeViewTables.Nodes.Clear()

        Dim rootNode As New TreeNode($"{currentDbType} Database")
        treeViewTables.Nodes.Add(rootNode)

        For Each table In tables
            Dim tableNode As New TreeNode($"{table.TableName}")
            If Not String.IsNullOrEmpty(table.Description) Then
                tableNode.ToolTipText = table.Description
            End If

            For Each column In table.Columns
                Dim columnText As String = $"{column.ColumnName} ({column.DataType})"
                If column.IsPrimaryKey Then columnText += " PK"
                If column.IsForeignKey Then columnText += " FK"

                Dim columnNode As New TreeNode(columnText)
                If Not String.IsNullOrEmpty(column.Description) Then
                    columnNode.ToolTipText = column.Description
                End If

                tableNode.Nodes.Add(columnNode)
            Next

            rootNode.Nodes.Add(tableNode)
        Next

        rootNode.Expand()
    End Sub

    Private Sub manageMETADATAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles manageMETADATAToolStripMenuItem.Click
        System.Diagnostics.Debug.WriteLine($"📋 manageMETADATAToolStripMenuItem_Click - currentConnection: {currentConnection?.GetType()?.Name}, State: {currentConnection?.State}")

        If currentConnection Is Nothing OrElse currentConnection.State <> ConnectionState.Open Then
            System.Diagnostics.Debug.WriteLine($"❌ Connection check failed - currentConnection is Nothing: {currentConnection Is Nothing}, State: {currentConnection?.State}")
            MessageBox.Show("Please connect to a database first.", "Connection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' Load existing metadata if available
            Dim existingMetadata As List(Of DatabaseMetadata) = Nothing
            If metadata.ContainsKey("tables") Then
                existingMetadata = DirectCast(metadata("tables"), List(Of DatabaseMetadata))
            End If

            ' If no metadata exists, offer to analyze database first
            If existingMetadata Is Nothing OrElse existingMetadata.Count = 0 Then
                Dim result As DialogResult = MessageBox.Show(
                    "No metadata found. Would you like to analyze the database structure first?",
                    "No Metadata",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    ' Analyze database first
                    analyzeDBToolStripMenuItem_Click(sender, e)

                    ' Get the metadata after analysis
                    If metadata.ContainsKey("tables") Then
                        existingMetadata = DirectCast(metadata("tables"), List(Of DatabaseMetadata))
                    End If
                ElseIf result = DialogResult.Cancel Then
                    Return
                Else
                    ' Create empty metadata
                    existingMetadata = New List(Of DatabaseMetadata)()
                End If
            End If

            ' Open metadata management form
            Dim metadataForm As New MetadataManagementForm(existingMetadata)
            If metadataForm.ShowDialog() = DialogResult.OK Then
                ' Update metadata with changes
                metadata("tables") = metadataForm.ModifiedMetadata

                ' Update tree view
                UpdateTreeViewWithMetadata(metadataForm.ModifiedMetadata)

                ' Save metadata
                Dim metadataPath As String = Path.Combine(Application.StartupPath, "metadata.json")
                DatabaseAnalyzer.SaveMetadata(metadataForm.ModifiedMetadata, metadataPath)

                statusLabel.Text = "Metadata updated successfully"
            End If

        Catch ex As Exception
            MessageBox.Show($"Error managing metadata: {ex.Message}", "Metadata Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            statusLabel.Text = "Error managing metadata"
        End Try
    End Sub

    Private Sub manageIndexesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles manageIndexesToolStripMenuItem.Click
        System.Diagnostics.Debug.WriteLine($"🏗️ manageIndexesToolStripMenuItem_Click - currentConnection: {currentConnection?.GetType()?.Name}, State: {currentConnection?.State}")

        If currentConnection Is Nothing OrElse currentConnection.State <> ConnectionState.Open Then
            System.Diagnostics.Debug.WriteLine($"❌ Connection check failed - currentConnection is Nothing: {currentConnection Is Nothing}, State: {currentConnection?.State}")
            MessageBox.Show("Please connect to a database first.", "Connection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' Load current metadata
            Dim metadataPath As String = Path.Combine(Application.StartupPath, "metadata.json")
            Dim tables As List(Of DatabaseMetadata) = DatabaseAnalyzer.LoadMetadata(metadataPath)

            If tables.Count = 0 Then
                Dim result = MessageBox.Show("No metadata found. Would you like to analyze the database first?",
                                            "No Metadata", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    analyzeDBToolStripMenuItem_Click(sender, e)
                    tables = DatabaseAnalyzer.LoadMetadata(metadataPath)
                Else
                    Return
                End If
            End If

            If tables.Count > 0 Then
                Dim indexForm As New IndexManagementForm(tables, currentConnection, currentDbType)
                indexForm.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show($"Error managing indexes: {ex.Message}", "Index Management Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub aiConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles aiConfigToolStripMenuItem.Click
        Try
            Dim aiConfigForm As New AIConfigForm()
            If aiConfigForm.ShowDialog() = DialogResult.OK Then
                ' Reload AI configuration
                aiConfig = aiConfigForm.Configuration

                ' Recreate AI client with new configuration
                If aiClient IsNot Nothing Then
                    aiClient.Dispose()
                End If
                aiClient = New AIClient(aiConfig)

                ' Update status
                statusLabel.Text = $"AI Endpoint: {aiConfig.CustomEndpoint} | Model: {aiConfig.ModelName}"

                MessageBox.Show("AI configuration updated successfully!", "Configuration Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error updating AI configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub aboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles aboutToolStripMenuItem.Click
        MessageBox.Show("AI QueryGen v1.0" & vbCrLf & vbCrLf &
                       "Intelligent SQL Query Generator" & vbCrLf &
                       "Powered by Natural Language Processing" & vbCrLf & vbCrLf &
                       "© 2025 AI QueryGen Team",
                       "About AI QueryGen", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Clean up database connection
        If currentConnection IsNot Nothing AndAlso currentConnection.State = ConnectionState.Open Then
            currentConnection.Close()
            currentConnection.Dispose()
        End If

        ' Clean up AI client
        If aiClient IsNot Nothing Then
            aiClient.Dispose()
        End If

        MyBase.OnFormClosing(e)
    End Sub

    ' ===============================
    ' DATABASE CONNECTIONS MANAGEMENT
    ' ===============================

    Private Sub LoadSavedConnections()
        Try
            Dim logFilePath As String = Path.Combine(Application.StartupPath, DB_LOGS_FILE)
            If File.Exists(logFilePath) Then
                Dim jsonContent As String = File.ReadAllText(logFilePath)
                savedConnections = JsonConvert.DeserializeObject(Of List(Of DatabaseConnectionInfo))(jsonContent)

                If savedConnections Is Nothing Then
                    savedConnections = New List(Of DatabaseConnectionInfo)
                End If

                System.Diagnostics.Debug.WriteLine($"📂 Loaded {savedConnections.Count} saved connections")
            Else
                savedConnections = New List(Of DatabaseConnectionInfo)
                System.Diagnostics.Debug.WriteLine("📂 No saved connections file found, starting fresh")
            End If

            UpdateConnectionsList()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error loading saved connections: {ex.Message}")
            savedConnections = New List(Of DatabaseConnectionInfo)
        End Try
    End Sub

    Private Sub SaveConnectionsToFile()
        Try
            Dim logFilePath As String = Path.Combine(Application.StartupPath, DB_LOGS_FILE)
            Dim jsonContent As String = JsonConvert.SerializeObject(savedConnections, Formatting.Indented)
            File.WriteAllText(logFilePath, jsonContent)

            System.Diagnostics.Debug.WriteLine($"💾 Saved {savedConnections.Count} connections to file")

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error saving connections: {ex.Message}")
        End Try
    End Sub

    Private Sub UpdateConnectionsList()
        ' This method will populate the connections list in the UI
        System.Diagnostics.Debug.WriteLine($"🔄 Updating connections list with {savedConnections.Count} items")

        Try
            listBoxSavedConnections.Items.Clear()

            For Each conn In savedConnections
                listBoxSavedConnections.Items.Add(conn)
            Next

            ' Enable/disable buttons based on list content
            buttonConnectSaved.Enabled = listBoxSavedConnections.Items.Count > 0
            buttonDeleteConnection.Enabled = listBoxSavedConnections.Items.Count > 0

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error updating connections list: {ex.Message}")
        End Try
    End Sub

    Private Sub AddConnectionToSaved(name As String, dbType As String, connectionString As String)
        Try
            ' Check if connection already exists (by name)
            Dim existingConnection = savedConnections.FirstOrDefault(Function(c) c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))

            If existingConnection IsNot Nothing Then
                ' Update existing connection
                existingConnection.DbType = dbType
                existingConnection.ConnectionString = connectionString
                existingConnection.LastConnected = DateTime.Now
                System.Diagnostics.Debug.WriteLine($"📝 Updated existing connection: {name}")
            Else
                ' Add new connection
                Dim newConnection As New DatabaseConnectionInfo(name, dbType, connectionString)
                savedConnections.Add(newConnection)
                System.Diagnostics.Debug.WriteLine($"➕ Added new connection: {name}")
            End If

            ' Sort by last connected (most recent first)
            savedConnections = savedConnections.OrderByDescending(Function(c) c.LastConnected).ToList()

            ' Save to file
            SaveConnectionsToFile()

            ' Update UI
            UpdateConnectionsList()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error adding connection: {ex.Message}")
        End Try
    End Sub

    Private Sub ConnectToSavedConnection(connectionInfo As DatabaseConnectionInfo)
        Try
            System.Diagnostics.Debug.WriteLine($"🔗 Connecting to saved connection: {connectionInfo.Name}")

            ' Create connection based on database type
            Select Case connectionInfo.DbType.ToLower()
                Case "sql server"
                    currentConnection = New SqlConnection(connectionInfo.ConnectionString)
                Case "mysql"
                    currentConnection = New MySqlConnection(connectionInfo.ConnectionString)
                Case "postgresql"
                    currentConnection = New NpgsqlConnection(connectionInfo.ConnectionString)
                Case "sqlite"
                    currentConnection = New SqliteConnection(connectionInfo.ConnectionString)
                Case Else
                    Throw New NotSupportedException($"Database type '{connectionInfo.DbType}' is not supported")
            End Select

            ' Test the connection
            currentConnection.Open()
            System.Diagnostics.Debug.WriteLine($"✅ Connection test successful for {connectionInfo.Name}")
            System.Diagnostics.Debug.WriteLine($"🔗 Connection State: {currentConnection.State}")
            ' Keep connection open for use

            ' Update the current connection details
            currentDbType = connectionInfo.DbType

            ' Update UI to reflect the database type
            For i As Integer = 0 To comboBoxDBType.Items.Count - 1
                If comboBoxDBType.Items(i).ToString().Equals(connectionInfo.DbType, StringComparison.OrdinalIgnoreCase) Then
                    comboBoxDBType.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Update UI
            labelCurrentDB.Text = $"Connected to {connectionInfo.Name}"
            labelCurrentDB.ForeColor = Color.Green
            connectionStatusLabel.Text = "Connected"
            connectionStatusLabel.ForeColor = Color.Green
            statusLabel.Text = $"Connected to saved database: {connectionInfo.Name}"

            ' Enable buttons
            buttonGenerateSQL.Enabled = True
            analyzeDBToolStripMenuItem.Enabled = True
            manageMETADATAToolStripMenuItem.Enabled = True
            manageIndexesToolStripMenuItem.Enabled = True

            ' Update LastConnected time
            connectionInfo.LastConnected = DateTime.Now
            SaveConnectionsToFile()
            UpdateConnectionsList()

            ' Load database structure
            LoadDatabaseStructure()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error connecting to saved connection: {ex.Message}")

            ' Reset connection state on error
            currentConnection = Nothing
            currentDbType = ""

            ' Update UI to show disconnected state
            labelCurrentDB.Text = "Not Connected"
            labelCurrentDB.ForeColor = Color.Red
            connectionStatusLabel.Text = "Connection Failed"
            connectionStatusLabel.ForeColor = Color.Red
            statusLabel.Text = "Connection failed"

            ' Disable buttons
            buttonGenerateSQL.Enabled = False
            analyzeDBToolStripMenuItem.Enabled = False
            manageMETADATAToolStripMenuItem.Enabled = False
            manageIndexesToolStripMenuItem.Enabled = False

            MessageBox.Show($"Error connecting to {connectionInfo.Name}: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GenerateConnectionName(connectionString As String) As String
        Try
            ' Extract meaningful information from connection string to create a readable name
            Dim name As String = ""

            ' Common patterns in connection strings
            If connectionString.ToLower().Contains("server=") Then
                Dim serverMatch = System.Text.RegularExpressions.Regex.Match(connectionString, "server=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If serverMatch.Success Then
                    name = serverMatch.Groups(1).Value
                End If
            ElseIf connectionString.ToLower().Contains("host=") Then
                Dim hostMatch = System.Text.RegularExpressions.Regex.Match(connectionString, "host=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If hostMatch.Success Then
                    name = hostMatch.Groups(1).Value
                End If
            ElseIf connectionString.ToLower().Contains("data source=") Then
                Dim dsMatch = System.Text.RegularExpressions.Regex.Match(connectionString, "data source=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If dsMatch.Success Then
                    name = dsMatch.Groups(1).Value
                    ' For file paths, just get the filename
                    If name.Contains("\") OrElse name.Contains("/") Then
                        name = Path.GetFileName(name)
                    End If
                End If
            End If

            ' Extract database name if available
            Dim databaseName As String = ""
            If connectionString.ToLower().Contains("database=") Then
                Dim dbMatch = System.Text.RegularExpressions.Regex.Match(connectionString, "database=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If dbMatch.Success Then
                    databaseName = dbMatch.Groups(1).Value
                End If
            ElseIf connectionString.ToLower().Contains("initial catalog=") Then
                Dim icMatch = System.Text.RegularExpressions.Regex.Match(connectionString, "initial catalog=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If icMatch.Success Then
                    databaseName = icMatch.Groups(1).Value
                End If
            End If

            ' Combine server and database name
            If Not String.IsNullOrEmpty(name) AndAlso Not String.IsNullOrEmpty(databaseName) Then
                Return $"{name}/{databaseName}"
            ElseIf Not String.IsNullOrEmpty(databaseName) Then
                Return databaseName
            ElseIf Not String.IsNullOrEmpty(name) Then
                Return name
            Else
                ' Fallback to a generic name with timestamp
                Return $"Database_{DateTime.Now:yyyyMMdd_HHmmss}"
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error generating connection name: {ex.Message}")
            Return $"Database_{DateTime.Now:yyyyMMdd_HHmmss}"
        End Try
    End Function

    ' ===========================
    ' EVENT HANDLERS FOR SAVED CONNECTIONS
    ' ===========================

    Private Sub buttonConnectSaved_Click(sender As Object, e As EventArgs) Handles buttonConnectSaved.Click
        Try
            If listBoxSavedConnections.SelectedItem IsNot Nothing Then
                Dim selectedConnection As DatabaseConnectionInfo = DirectCast(listBoxSavedConnections.SelectedItem, DatabaseConnectionInfo)
                ConnectToSavedConnection(selectedConnection)
            Else
                MessageBox.Show("Please select a connection from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error connecting to saved connection: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonDeleteConnection_Click(sender As Object, e As EventArgs) Handles buttonDeleteConnection.Click
        Try
            If listBoxSavedConnections.SelectedItem IsNot Nothing Then
                Dim selectedConnection As DatabaseConnectionInfo = DirectCast(listBoxSavedConnections.SelectedItem, DatabaseConnectionInfo)

                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to delete the connection '{selectedConnection.Name}'?",
                                                           "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    savedConnections.Remove(selectedConnection)
                    SaveConnectionsToFile()
                    UpdateConnectionsList()
                    System.Diagnostics.Debug.WriteLine($"🗑️ Deleted connection: {selectedConnection.Name}")
                End If
            Else
                MessageBox.Show("Please select a connection to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error deleting connection: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub listBoxSavedConnections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listBoxSavedConnections.SelectedIndexChanged
        ' Enable/disable buttons based on selection
        Dim hasSelection As Boolean = listBoxSavedConnections.SelectedItem IsNot Nothing
        buttonConnectSaved.Enabled = hasSelection
        buttonDeleteConnection.Enabled = hasSelection
    End Sub

    Private Sub listBoxSavedConnections_DoubleClick(sender As Object, e As EventArgs) Handles listBoxSavedConnections.DoubleClick
        ' Double-click to connect
        buttonConnectSaved_Click(sender, e)
    End Sub

End Class

' Class to store database connection information
Public Class DatabaseConnectionInfo
    Public Property Name As String
    Public Property DbType As String
    Public Property ConnectionString As String
    Public Property LastConnected As DateTime

    Public Sub New()
    End Sub

    Public Sub New(name As String, dbType As String, connectionString As String)
        Me.Name = name
        Me.DbType = dbType
        Me.ConnectionString = connectionString
        Me.LastConnected = DateTime.Now
    End Sub

    Public Overrides Function ToString() As String
        Return $"{Name} ({DbType}) - {LastConnected:dd/MM/yyyy HH:mm}"
    End Function
End Class
