Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Public Class IndexManagementForm
    Private metadata As List(Of DatabaseMetadata)
    Private indexSuggestions As List(Of IndexSuggestion)
    Private indexAnalyzer As IndexAnalyzer
    Private currentConnection As IDbConnection
    Private dbType As String
    Private aiClient As AIClient

    Public Sub New(tables As List(Of DatabaseMetadata), conn As IDbConnection, databaseType As String)
        InitializeComponent()
        metadata = tables
        currentConnection = conn
        dbType = databaseType
        indexAnalyzer = New IndexAnalyzer(conn, databaseType)

        ' Initialize AI client
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            Dim aiConfig As AIConfiguration
            If File.Exists(configPath) Then
                Dim json As String = File.ReadAllText(configPath)
                aiConfig = JsonConvert.DeserializeObject(Of AIConfiguration)(json)
            Else
                aiConfig = New AIConfiguration()
            End If
            aiClient = New AIClient(aiConfig)
        Catch ex As Exception
            aiClient = New AIClient(New AIConfiguration())
        End Try
    End Sub

    Private Sub IndexManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadIndexSuggestions()
    End Sub

    Private Sub SetupDataGridView()
        dataGridViewIndexes.Columns.Clear()

        ' Checkbox column for selection
        Dim chkColumn As New DataGridViewCheckBoxColumn()
        chkColumn.Name = "Selected"
        chkColumn.HeaderText = "Select"
        chkColumn.Width = 60
        dataGridViewIndexes.Columns.Add(chkColumn)

        ' Other columns
        dataGridViewIndexes.Columns.Add("TableName", "Table")
        dataGridViewIndexes.Columns.Add("IndexName", "Index Name")
        dataGridViewIndexes.Columns.Add("Columns", "Columns")
        dataGridViewIndexes.Columns.Add("Reason", "Reason")
        dataGridViewIndexes.Columns.Add("Priority", "Priority")
        dataGridViewIndexes.Columns.Add("Query", "Related Query")

        ' Set column properties
        dataGridViewIndexes.Columns("TableName").Width = 150
        dataGridViewIndexes.Columns("IndexName").Width = 200
        dataGridViewIndexes.Columns("Columns").Width = 200
        dataGridViewIndexes.Columns("Reason").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dataGridViewIndexes.Columns("Priority").Width = 80
        dataGridViewIndexes.Columns("Query").Width = 300

        dataGridViewIndexes.AllowUserToAddRows = False
        dataGridViewIndexes.AllowUserToDeleteRows = False
        dataGridViewIndexes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dataGridViewIndexes.MultiSelect = True
    End Sub

    Private Sub LoadIndexSuggestions()
        Try
            lblStatus.Text = "Analyzing tables for index suggestions..."
            lblStatus.ForeColor = Color.Blue
            Application.DoEvents()

            indexSuggestions = indexAnalyzer.AnalyzeAndSuggestIndexes(metadata)
            PopulateIndexGrid()

            lblStatus.Text = $"Found {indexSuggestions.Count} index suggestions"
            lblStatus.ForeColor = Color.Green

        Catch ex As Exception
            lblStatus.Text = $"Error: {ex.Message}"
            lblStatus.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub PopulateIndexGrid()
        dataGridViewIndexes.Rows.Clear()

        For Each suggestion In indexSuggestions.OrderBy(Function(s) s.Priority).ThenBy(Function(s) s.TableName)
            Dim rowIndex = dataGridViewIndexes.Rows.Add(
                False, ' Selected
                suggestion.TableName,
                suggestion.IndexName,
                String.Join(", ", suggestion.Columns),
                suggestion.Reason,
                suggestion.Priority.ToString(),
                If(String.IsNullOrEmpty(suggestion.Query), "", suggestion.Query.Substring(0, Math.Min(100, suggestion.Query.Length)) + "...")
            )

            ' Color code by priority
            Select Case suggestion.Priority
                Case IndexPriority.High
                    dataGridViewIndexes.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                Case IndexPriority.Medium
                    dataGridViewIndexes.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow
                Case IndexPriority.Low
                    dataGridViewIndexes.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGray
            End Select

            ' Store the suggestion object in the row tag
            dataGridViewIndexes.Rows(rowIndex).Tag = suggestion
        Next
    End Sub

    Private Async Sub btnAnalyzeQuery_Click(sender As Object, e As EventArgs) Handles btnAnalyzeQuery.Click
        If String.IsNullOrWhiteSpace(txtQuery.Text) Then
            MessageBox.Show("Please enter a SQL query to analyze.", "Query Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            lblStatus.Text = "Analyzing query with AI for index optimization..."
            lblStatus.ForeColor = Color.Blue
            btnAnalyzeQuery.Enabled = False
            Application.DoEvents()

            ' Step 1: Get AI-powered index suggestions
            Dim aiIndexSuggestions As List(Of IndexSuggestion) = Await GetAIIndexSuggestions(txtQuery.Text)

            ' Step 2: Get traditional analyzer suggestions as backup
            ''Dim querySuggestions = indexAnalyzer.AnalyzeQueryAndSuggestIndexes(txtQuery.Text, metadata)
            Dim querySuggestions = New List(Of IndexSuggestion)

            ' Step 3: Combine AI suggestions with traditional suggestions
            Dim allSuggestions As New List(Of IndexSuggestion)
            allSuggestions.AddRange(aiIndexSuggestions)
            allSuggestions.AddRange(querySuggestions)

            ' Step 4: Check existing indexes and update suggestions
            Dim checkedSuggestions = CheckExistingIndexes(allSuggestions)

            ' Step 5: Add new suggestions to the list (avoid duplicates)
            For Each suggestion In checkedSuggestions
                Dim existingKey = $"{suggestion.TableName}_{String.Join("_", suggestion.Columns)}"
                If Not indexSuggestions.Any(Function(s) $"{s.TableName}_{String.Join("_", s.Columns)}" = existingKey) Then
                    indexSuggestions.Add(suggestion)
                End If
            Next

            PopulateIndexGrid()

            lblStatus.Text = $"AI Analysis complete: {aiIndexSuggestions.Count} AI suggestions, {querySuggestions.Count} traditional suggestions"
            lblStatus.ForeColor = Color.Green

        Catch ex As Exception
            lblStatus.Text = $"Error analyzing query: {ex.Message}"
            lblStatus.ForeColor = Color.Red
        Finally
            btnAnalyzeQuery.Enabled = True
        End Try
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For i As Integer = 0 To dataGridViewIndexes.Rows.Count - 1
            dataGridViewIndexes.Item(0, i).Value = True
        Next
    End Sub

    Private Sub btnSelectNone_Click(sender As Object, e As EventArgs) Handles btnSelectNone.Click
        For i As Integer = 0 To dataGridViewIndexes.Rows.Count - 1
            dataGridViewIndexes.Item(0, i).Value = False
        Next
    End Sub

    Private Sub btnSelectHighPriority_Click(sender As Object, e As EventArgs) Handles btnSelectHighPriority.Click
        For i As Integer = 0 To dataGridViewIndexes.Rows.Count - 1
            Dim suggestion As IndexSuggestion = TryCast(dataGridViewIndexes.Rows(i).Tag, IndexSuggestion)
            dataGridViewIndexes.Item(0, i).Value = (suggestion IsNot Nothing AndAlso suggestion.Priority = IndexPriority.High)
        Next
    End Sub

    Private Sub btnGenerateScript_Click(sender As Object, e As EventArgs) Handles btnGenerateScript.Click
        Dim selectedSuggestions As New List(Of IndexSuggestion)

        ' Get selected suggestions
        For i As Integer = 0 To dataGridViewIndexes.Rows.Count - 1
            Dim isSelected As Boolean = Convert.ToBoolean(dataGridViewIndexes.Item(0, i).Value)
            If isSelected Then
                Dim suggestion As IndexSuggestion = TryCast(dataGridViewIndexes.Rows(i).Tag, IndexSuggestion)
                If suggestion IsNot Nothing Then
                    selectedSuggestions.Add(suggestion)
                End If
            End If
        Next

        If selectedSuggestions.Count = 0 Then
            MessageBox.Show("Please select at least one index to generate script.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Generate script
        Dim script As New StringBuilder()
        script.AppendLine($"-- Index Creation Script")
        script.AppendLine($"-- Generated on: {DateTime.Now}")
        script.AppendLine($"-- Database Type: {dbType}")
        script.AppendLine($"-- Total Indexes: {selectedSuggestions.Count}")
        script.AppendLine()

        For Each suggestion In selectedSuggestions.OrderBy(Function(s) s.TableName)
            script.AppendLine(indexAnalyzer.GenerateCreateIndexScript(suggestion))
            script.AppendLine()
        Next

        ' Show script in a dialog
        Dim scriptForm As New ScriptViewerForm(script.ToString(), "Index Creation Script")
        scriptForm.ShowDialog()
    End Sub

    Private Sub btnExecuteSelected_Click(sender As Object, e As EventArgs) Handles btnExecuteSelected.Click
        Dim selectedSuggestions As New List(Of IndexSuggestion)

        ' Get selected suggestions
        For i As Integer = 0 To dataGridViewIndexes.Rows.Count - 1
            Dim isSelected As Boolean = Convert.ToBoolean(dataGridViewIndexes.Item(0, i).Value)
            If isSelected Then
                Dim suggestion As IndexSuggestion = TryCast(dataGridViewIndexes.Rows(i).Tag, IndexSuggestion)
                If suggestion IsNot Nothing Then
                    selectedSuggestions.Add(suggestion)
                End If
            End If
        Next

        If selectedSuggestions.Count = 0 Then
            MessageBox.Show("Please select at least one index to execute.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result = MessageBox.Show(
            $"This will create {selectedSuggestions.Count} indexes on the database. Are you sure you want to continue?",
            "Confirm Index Creation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ExecuteIndexCreation(selectedSuggestions)
        End If
    End Sub

    Private Sub ExecuteIndexCreation(suggestions As List(Of IndexSuggestion))
        Dim successCount As Integer = 0
        Dim errorCount As Integer = 0
        Dim errors As New List(Of String)

        Try
            lblStatus.Text = "Creating indexes..."
            lblStatus.ForeColor = Color.Blue
            progressBar.Visible = True
            progressBar.Maximum = suggestions.Count
            progressBar.Value = 0
            Application.DoEvents()

            For Each suggestion In suggestions
                Try
                    Dim script = indexAnalyzer.GenerateCreateIndexScript(suggestion)
                    Using cmd = currentConnection.CreateCommand()
                        cmd.CommandText = script.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).
                                         Where(Function(line) Not line.Trim().StartsWith("--")).
                                         FirstOrDefault(Function(line) line.Trim().StartsWith("CREATE"))

                        If Not String.IsNullOrEmpty(cmd.CommandText) Then
                            cmd.ExecuteNonQuery()
                            successCount += 1
                        End If
                    End Using

                Catch ex As Exception
                    errorCount += 1
                    errors.Add($"Index {suggestion.IndexName}: {ex.Message}")
                End Try

                progressBar.Value += 1
                Application.DoEvents()
            Next

            progressBar.Visible = False

            Dim message As New StringBuilder()
            message.AppendLine($"Index creation completed!")
            message.AppendLine($"Successfully created: {successCount}")
            message.AppendLine($"Errors: {errorCount}")

            If errors.Count > 0 Then
                message.AppendLine()
                message.AppendLine("Errors:")
                For Each Err As String In errors.Take(5).ToList() ' Show first 5 errors
                    message.AppendLine($"- {Err}")
                Next
                If errors.Count > 5 Then
                    message.AppendLine($"... and {errors.Count - 5} more")
                End If
            End If

            MessageBox.Show(message.ToString(), "Index Creation Results", MessageBoxButtons.OK,
                           If(errorCount = 0, MessageBoxIcon.Information, MessageBoxIcon.Warning))

            lblStatus.Text = $"Completed: {successCount} created, {errorCount} errors"
            lblStatus.ForeColor = If(errorCount = 0, Color.Green, Color.Orange)

        Catch ex As Exception
            progressBar.Visible = False
            lblStatus.Text = $"Error: {ex.Message}"
            lblStatus.ForeColor = Color.Red
            MessageBox.Show($"Error during index creation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Get AI-powered index suggestions based on SQL query
    ''' </summary>
    Private Async Function GetAIIndexSuggestions(query As String) As Task(Of List(Of IndexSuggestion))
        Dim suggestions As New List(Of IndexSuggestion)

        Try
            ' Build database-specific prompt for AI
            Dim prompt As String = BuildIndexOptimizationPrompt(query)

            ' Get AI response
            Dim aiResponse As String = Await aiClient.GenerateSQLAsync(prompt, "")

            ' Parse AI response to extract index suggestions
            suggestions = ParseAIIndexResponse(aiResponse, query)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ AI Index Analysis Error: {ex.Message}")
            ' Return empty list on error - fallback to traditional analysis
        End Try

        Return suggestions
    End Function

    ''' <summary>
    ''' Build database-specific prompt for index optimization
    ''' </summary>
    Private Function BuildIndexOptimizationPrompt(query As String) As String
        Dim prompt As New StringBuilder()

        ' Database type specific instructions
        prompt.AppendLine($"You are an expert {dbType} database performance optimizer.")
        prompt.AppendLine($"Analyze the following SQL query and suggest optimal indexes for {dbType}.")
        prompt.AppendLine()

        ' Add database-specific syntax guidelines
        Select Case dbType.ToLower()
            Case "sql server"
                prompt.AppendLine("Guidelines for SQL Server indexes:")
                prompt.AppendLine("- Use NONCLUSTERED indexes for most cases")
                prompt.AppendLine("- Consider INCLUDE columns for covering indexes")
                prompt.AppendLine("- Use proper index naming convention: IX_TableName_ColumnName(s)")
                prompt.AppendLine("- Consider filtered indexes with WHERE clauses")

            Case "mysql"
                prompt.AppendLine("Guidelines for MySQL indexes:")
                prompt.AppendLine("- Use composite indexes efficiently (leftmost prefix rule)")
                prompt.AppendLine("- Consider covering indexes to avoid table lookups")
                prompt.AppendLine("- Use proper index naming: idx_tablename_columnname")
                prompt.AppendLine("- Consider partial indexes for large text columns")

            Case "postgresql"
                prompt.AppendLine("Guidelines for PostgreSQL indexes:")
                prompt.AppendLine("- Use B-tree indexes for equality and range queries")
                prompt.AppendLine("- Consider partial indexes with WHERE conditions")
                prompt.AppendLine("- Use proper naming: idx_tablename_columnname")
                prompt.AppendLine("- Consider GIN/GiST indexes for special data types")

            Case "sqlite"
                prompt.AppendLine("Guidelines for SQLite indexes:")
                prompt.AppendLine("- Keep indexes simple and focused")
                prompt.AppendLine("- Use covering indexes where possible")
                prompt.AppendLine("- Consider partial indexes for filtered queries")
        End Select

        prompt.AppendLine()
        prompt.AppendLine("Database Schema Information:")

        ' Add table schema information
        For Each table In metadata
            prompt.AppendLine($"Table: {table.TableName}")
            For Each column In table.Columns
                Dim pkInfo As String = If(column.IsPrimaryKey, " [PK]", "")
                Dim fkInfo As String = If(column.IsForeignKey, $" [FK->{column.ReferencedTable}.{column.ReferencedColumn}]", "")
                prompt.AppendLine($"  - {column.ColumnName} ({column.DataType}){pkInfo}{fkInfo}")
            Next
            prompt.AppendLine()
        Next

        prompt.AppendLine("SQL Query to analyze:")
        prompt.AppendLine("```sql")
        prompt.AppendLine(query)
        prompt.AppendLine("```")
        prompt.AppendLine()

        prompt.AppendLine("Please provide index suggestions in the following JSON format:")
        prompt.AppendLine("[")
        prompt.AppendLine("  {")
        prompt.AppendLine("    ""tableName"": ""table_name"",")
        prompt.AppendLine("    ""indexName"": ""suggested_index_name"",")
        prompt.AppendLine("    ""columns"": [""column1"", ""column2""],")
        prompt.AppendLine("    ""reason"": ""explanation for this index"",")
        prompt.AppendLine("    ""priority"": ""High|Medium|Low"",")
        prompt.AppendLine("    ""indexType"": ""Normal|Unique"",")
        prompt.AppendLine("    ""estimatedImpact"": ""performance impact description""")
        prompt.AppendLine("  }")
        prompt.AppendLine("]")

        Return prompt.ToString()
    End Function

    ''' <summary>
    ''' Parse AI response to extract index suggestions
    ''' </summary>
    Private Function ParseAIIndexResponse(aiResponse As String, originalQuery As String) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)

        Try
            ' Extract JSON from AI response
            Dim jsonStart As Integer = aiResponse.IndexOf("[")
            Dim jsonEnd As Integer = aiResponse.LastIndexOf("]")

            If jsonStart >= 0 AndAlso jsonEnd > jsonStart Then
                Dim jsonContent As String = aiResponse.Substring(jsonStart, jsonEnd - jsonStart + 1)

                ' Parse JSON array
                Dim aiSuggestions = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonContent)

                For Each aiSuggestion In aiSuggestions
                    Try
                        Dim suggestion As New IndexSuggestion()
                        suggestion.TableName = aiSuggestion("tableName").ToString()
                        suggestion.IndexName = aiSuggestion("indexName").ToString()
                        suggestion.Reason = $"AI: {aiSuggestion("reason").ToString()}"
                        suggestion.Query = originalQuery

                        ' Get schema name safely
                        Dim tableMetadata = metadata.FirstOrDefault(Function(t) t.TableName = suggestion.TableName)
                        suggestion.SchemaName = If(tableMetadata IsNot Nothing, tableMetadata.SchemaName, "dbo")

                        ' Parse columns array
                        If aiSuggestion.ContainsKey("columns") Then
                            Dim columnsArray = JsonConvert.DeserializeObject(Of String())(aiSuggestion("columns").ToString())
                            suggestion.Columns = columnsArray.ToList()
                        End If

                        ' Parse priority
                        If aiSuggestion.ContainsKey("priority") Then
                            Select Case aiSuggestion("priority").ToString().ToLower()
                                Case "high"
                                    suggestion.Priority = IndexPriority.High
                                Case "medium"
                                    suggestion.Priority = IndexPriority.Medium
                                Case "low"
                                    suggestion.Priority = IndexPriority.Low
                                Case Else
                                    suggestion.Priority = IndexPriority.Medium
                            End Select
                        End If

                        ' Parse index type
                        If aiSuggestion.ContainsKey("indexType") Then
                            If aiSuggestion("indexType").ToString().ToLower() = "unique" Then
                                suggestion.IndexType = IndexType.Unique
                            Else
                                suggestion.IndexType = IndexType.Clustered
                            End If
                        End If

                        ' Add estimated impact to reason
                        If aiSuggestion.ContainsKey("estimatedImpact") Then
                            suggestion.Reason += $" | Impact: {aiSuggestion("estimatedImpact")}"
                        End If

                        suggestions.Add(suggestion)

                    Catch parseEx As Exception
                        System.Diagnostics.Debug.WriteLine($"❌ Error parsing individual AI suggestion: {parseEx.Message}")
                    End Try
                Next

            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error parsing AI response: {ex.Message}")
        End Try

        Return suggestions
    End Function

    ''' <summary>
    ''' Check if suggested indexes already exist in the database
    ''' </summary>
    Private Function CheckExistingIndexes(suggestions As List(Of IndexSuggestion)) As List(Of IndexSuggestion)
        Dim checkedSuggestions As New List(Of IndexSuggestion)

        Try
            ' Get existing indexes from database
            Dim existingIndexes = GetExistingIndexes()

            For Each suggestion In suggestions
                ' Check if this index already exists
                Dim indexExists As Boolean = False
                Dim existingIndexInfo As String = ""

                For Each existingIndex In existingIndexes
                    If existingIndex.TableName.Equals(suggestion.TableName, StringComparison.OrdinalIgnoreCase) Then
                        ' Check if columns match (order matters for some databases)
                        If AreColumnsSimilar(existingIndex.Columns, suggestion.Columns) Then
                            indexExists = True
                            existingIndexInfo = $"Similar index '{existingIndex.IndexName}' exists"
                            Exit For
                        End If
                    End If
                Next

                ' Update suggestion with existence check result
                If indexExists Then
                    suggestion.Reason = $"⚠️ {suggestion.Reason} | Status: {existingIndexInfo}"
                    suggestion.Priority = IndexPriority.Low ' Lower priority for existing indexes
                Else
                    suggestion.Reason = $"✅ {suggestion.Reason} | Status: New index (recommended)"
                End If

                checkedSuggestions.Add(suggestion)
            Next

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error checking existing indexes: {ex.Message}")
            ' Return original suggestions if checking fails
            Return suggestions
        End Try

        Return checkedSuggestions
    End Function

    ''' <summary>
    ''' Get existing indexes from database
    ''' </summary>
    Private Function GetExistingIndexes() As List(Of IndexSuggestion)
        Dim existingIndexes As New List(Of IndexSuggestion)

        Try
            Dim query As String = ""

            ' Database-specific queries to get existing indexes
            Select Case dbType.ToLower()
                Case "sql server"
                    query = "
                        SELECT 
                            t.name AS TableName,
                            i.name AS IndexName,
                            STRING_AGG(c.name, ',') AS Columns
                        FROM sys.indexes i
                        INNER JOIN sys.tables t ON i.object_id = t.object_id
                        INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                        WHERE i.type_desc = 'NONCLUSTERED' AND i.name IS NOT NULL
                        GROUP BY t.name, i.name"

                Case "mysql"
                    query = "
                        SELECT 
                            TABLE_NAME AS TableName,
                            INDEX_NAME AS IndexName,
                            GROUP_CONCAT(COLUMN_NAME ORDER BY SEQ_IN_INDEX) AS Columns
                        FROM INFORMATION_SCHEMA.STATISTICS 
                        WHERE TABLE_SCHEMA = DATABASE()
                            AND INDEX_NAME != 'PRIMARY'
                        GROUP BY TABLE_NAME, INDEX_NAME"

                Case "postgresql"
                    query = "
                        SELECT 
                            t.relname AS TableName,
                            i.relname AS IndexName,
                            string_agg(a.attname, ',' ORDER BY k.ordinality) AS Columns
                        FROM pg_index ix
                        JOIN pg_class i ON i.oid = ix.indexrelid
                        JOIN pg_class t ON t.oid = ix.indrelid
                        JOIN unnest(ix.indkey) WITH ORDINALITY k(attnum, ordinality) ON true
                        JOIN pg_attribute a ON a.attrelid = t.oid AND a.attnum = k.attnum
                        WHERE t.relkind = 'r' AND NOT ix.indisprimary
                        GROUP BY t.relname, i.relname"

                Case "sqlite"
                    query = "
                        SELECT 
                            m.tbl_name AS TableName,
                            m.name AS IndexName,
                            GROUP_CONCAT(ii.name) AS Columns
                        FROM sqlite_master m
                        JOIN pragma_index_info(m.name) ii
                        WHERE m.type = 'index' 
                            AND m.name NOT LIKE 'sqlite_%'
                        GROUP BY m.tbl_name, m.name"
            End Select

            If Not String.IsNullOrEmpty(query) Then
                Using cmd As IDbCommand = currentConnection.CreateCommand()
                    cmd.CommandText = query
                    Using reader As IDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim existingIndex As New IndexSuggestion()
                            existingIndex.TableName = reader("TableName").ToString()
                            existingIndex.IndexName = reader("IndexName").ToString()
                            existingIndex.Columns = reader("Columns").ToString().Split(","c).Select(Function(s) s.Trim()).ToList()
                            existingIndexes.Add(existingIndex)
                        End While
                    End Using
                End Using
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error getting existing indexes: {ex.Message}")
        End Try

        Return existingIndexes
    End Function

    ''' <summary>
    ''' Check if two column lists are similar (allowing for different order in some cases)
    ''' </summary>
    Private Function AreColumnsSimilar(existingColumns As List(Of String), suggestedColumns As List(Of String)) As Boolean
        If existingColumns.Count <> suggestedColumns.Count Then
            Return False
        End If

        ' For exact match (order matters for most databases)
        If existingColumns.SequenceEqual(suggestedColumns, StringComparer.OrdinalIgnoreCase) Then
            Return True
        End If

        ' For similar columns (same columns, different order)
        Return existingColumns.All(Function(col) suggestedColumns.Contains(col, StringComparer.OrdinalIgnoreCase)) AndAlso
               suggestedColumns.All(Function(col) existingColumns.Contains(col, StringComparer.OrdinalIgnoreCase))
    End Function

    Private Sub dataGridViewIndexes_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridViewIndexes.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim suggestion As IndexSuggestion = TryCast(dataGridViewIndexes.Rows(e.RowIndex).Tag, IndexSuggestion)
            If suggestion IsNot Nothing Then
                Dim script = indexAnalyzer.GenerateCreateIndexScript(suggestion)
                Dim scriptForm As New ScriptViewerForm(script, $"Index Script: {suggestion.IndexName}")
                scriptForm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
