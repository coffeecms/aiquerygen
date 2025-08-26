Imports System.Data
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

Public Class IndexAnalyzer
    Private connection As IDbConnection
    Private dbType As String
    
    Public Sub New(conn As IDbConnection, databaseType As String)
        connection = conn
        dbType = databaseType.ToLower()
    End Sub
    
    ''' <summary>
    ''' Phân tích và đề xuất index cho các bảng
    ''' </summary>
    Public Function AnalyzeAndSuggestIndexes(tables As List(Of DatabaseMetadata)) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)
        
        For Each table In tables
            ' 1. Đề xuất index cho Foreign Keys
            suggestions.AddRange(SuggestForeignKeyIndexes(table))
            
            ' 2. Đề xuất index cho các cột thường được query
            suggestions.AddRange(SuggestFrequentlyQueriedIndexes(table))
            
            ' 3. Đề xuất composite index
            suggestions.AddRange(SuggestCompositeIndexes(table))
        Next
        
        Return suggestions
    End Function
    
    ''' <summary>
    ''' Phân tích query và đề xuất index
    ''' </summary>
    Public Function AnalyzeQueryAndSuggestIndexes(query As String, tables As List(Of DatabaseMetadata)) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)
        
        Try
            ' Parse WHERE clauses
            Dim whereColumns = ExtractWhereColumns(query)
            
            ' Parse JOIN clauses
            Dim joinColumns = ExtractJoinColumns(query)
            
            ' Parse ORDER BY clauses
            Dim orderByColumns = ExtractOrderByColumns(query)
            
            ' Parse GROUP BY clauses
            Dim groupByColumns = ExtractGroupByColumns(query)
            
            ' Tạo đề xuất index dựa trên phân tích
            For Each table In tables
                ' Index cho WHERE conditions
                For Each col In whereColumns
                    If TableHasColumn(table, col) Then
                        suggestions.Add(New IndexSuggestion With {
                            .TableName = table.TableName,
                            .SchemaName = table.SchemaName,
                            .IndexName = $"IX_{table.TableName}_{col}",
                            .Columns = New List(Of String) From {col},
                            .IndexType = IndexType.NonClustered,
                            .Reason = $"Frequently used in WHERE clause",
                            .Query = query,
                            .Priority = IndexPriority.High
                        })
                    End If
                Next
                
                ' Index cho JOIN conditions
                For Each col In joinColumns
                    If TableHasColumn(table, col) Then
                        suggestions.Add(New IndexSuggestion With {
                            .TableName = table.TableName,
                            .SchemaName = table.SchemaName,
                            .IndexName = $"IX_{table.TableName}_{col}_JOIN",
                            .Columns = New List(Of String) From {col},
                            .IndexType = IndexType.NonClustered,
                            .Reason = $"Used in JOIN condition",
                            .Query = query,
                            .Priority = IndexPriority.High
                        })
                    End If
                Next
                
                ' Composite index cho ORDER BY + WHERE
                If whereColumns.Count > 0 AndAlso orderByColumns.Count > 0 Then
                    Dim allColumns = whereColumns.Union(orderByColumns).ToList()
                    Dim tableColumns = allColumns.Where(Function(c) TableHasColumn(table, c)).ToList()
                    
                    If tableColumns.Count > 1 Then
                        suggestions.Add(New IndexSuggestion With {
                            .TableName = table.TableName,
                            .SchemaName = table.SchemaName,
                            .IndexName = $"IX_{table.TableName}_COMPOSITE_{String.Join("_", tableColumns)}",
                            .Columns = tableColumns,
                            .IndexType = IndexType.NonClustered,
                            .Reason = "Composite index for WHERE + ORDER BY optimization",
                            .Query = query,
                            .Priority = IndexPriority.Medium
                        })
                    End If
                End If
            Next
            
        Catch ex As Exception
            ' Log error but don't throw
            Console.WriteLine($"Error analyzing query: {ex.Message}")
        End Try
        
        Return suggestions.GroupBy(Function(s) $"{s.TableName}_{String.Join("_", s.Columns)}").
                          Select(Function(g) g.First()).
                          ToList()
    End Function
    
    Private Function SuggestForeignKeyIndexes(table As DatabaseMetadata) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)
        
        For Each column In table.Columns.Where(Function(c) c.IsForeignKey)
            suggestions.Add(New IndexSuggestion With {
                .TableName = table.TableName,
                .SchemaName = table.SchemaName,
                .IndexName = $"IX_{table.TableName}_{column.ColumnName}_FK",
                .Columns = New List(Of String) From {column.ColumnName},
                .IndexType = IndexType.NonClustered,
                .Reason = "Foreign key column - improves JOIN performance",
                .Priority = IndexPriority.High
            })
        Next
        
        Return suggestions
    End Function
    
    Private Function SuggestFrequentlyQueriedIndexes(table As DatabaseMetadata) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)
        
        ' Đề xuất index cho các cột có thể thường được query
        For Each column In table.Columns
            If ShouldIndexColumn(column) Then
                suggestions.Add(New IndexSuggestion With {
                    .TableName = table.TableName,
                    .SchemaName = table.SchemaName,
                    .IndexName = $"IX_{table.TableName}_{column.ColumnName}",
                    .Columns = New List(Of String) From {column.ColumnName},
                    .IndexType = IndexType.NonClustered,
                    .Reason = GetIndexReason(column),
                    .Priority = IndexPriority.Medium
                })
            End If
        Next
        
        Return suggestions
    End Function
    
    Private Function SuggestCompositeIndexes(table As DatabaseMetadata) As List(Of IndexSuggestion)
        Dim suggestions As New List(Of IndexSuggestion)

        ' Đề xuất composite index cho các cột thường được dùng cùng nhau
        Dim candidateColumns = table.Columns.Where(Function(c) Not c.IsPrimaryKey And (c.DataType.ToLower().Contains("varchar") Or c.DataType.ToLower().Contains("int") Or c.DataType.ToLower().Contains("date"))).Take(3).ToList()

        If candidateColumns.Count >= 2 Then
            suggestions.Add(New IndexSuggestion With {
                .TableName = table.TableName,
                .SchemaName = table.SchemaName,
                .IndexName = $"IX_{table.TableName}_COMPOSITE_{String.Join("_", candidateColumns.Select(Function(col) col.ColumnName))}",
                .Columns = candidateColumns.Select(Function(col) col.ColumnName).ToList(),
                .IndexType = IndexType.NonClustered,
                .Reason = "Composite index for potential multi-column queries",
                .Priority = IndexPriority.Low
            })
        End If
        
        Return suggestions
    End Function
    
    Private Function ShouldIndexColumn(column As ColumnMetadata) As Boolean
        ' Không index các cột đã có primary key
        If column.IsPrimaryKey Then Return False
        
        ' Index các cột có khả năng được query
        Dim dataType = column.DataType.ToLower()
        
        ' Cột date/datetime thường được query
        If dataType.Contains("date") Or dataType.Contains("time") Then Return True
        
        ' Cột status, type, category thường được query
        If column.ColumnName.ToLower().Contains("status") Or 
           column.ColumnName.ToLower().Contains("type") Or 
           column.ColumnName.ToLower().Contains("category") Or
           column.ColumnName.ToLower().Contains("code") Then Return True
        
        ' Cột varchar ngắn có thể là lookup
        If dataType.Contains("varchar") AndAlso column.MaxLength.HasValue AndAlso column.MaxLength.Value <= 50 Then Return True
        
        Return False
    End Function
    
    Private Function GetIndexReason(column As ColumnMetadata) As String
        Dim dataType = column.DataType.ToLower()
        Dim columnName = column.ColumnName.ToLower()
        
        If dataType.Contains("date") Or dataType.Contains("time") Then
            Return "Date/time column - often used in range queries"
        ElseIf columnName.Contains("status") Then
            Return "Status column - frequently filtered"
        ElseIf columnName.Contains("type") Or columnName.Contains("category") Then
            Return "Type/category column - used for grouping and filtering"
        ElseIf columnName.Contains("code") Then
            Return "Code column - often used for lookups"
        ElseIf dataType.Contains("varchar") Then
            Return "Short varchar column - potential lookup field"
        Else
            Return "Potentially useful for queries"
        End If
    End Function
    
    Private Function ExtractWhereColumns(query As String) As List(Of String)
        Dim columns As New List(Of String)
        
        ' Simple regex to extract column names from WHERE clauses
        Dim wherePattern As String = "WHERE\s+(.+?)(?:\s+ORDER\s+BY|\s+GROUP\s+BY|\s*$)"
        Dim whereMatch = Regex.Match(query, wherePattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        
        If whereMatch.Success Then
            Dim whereClause = whereMatch.Groups(1).Value
            
            ' Extract column names (simple approach)
            Dim columnPattern As String = "\b([a-zA-Z_][a-zA-Z0-9_]*)\s*[=<>!]"
            Dim matches = Regex.Matches(whereClause, columnPattern)
            
            For Each match As Match In matches
                Dim columnName = match.Groups(1).Value
                If Not columns.Contains(columnName, StringComparer.OrdinalIgnoreCase) Then
                    columns.Add(columnName)
                End If
            Next
        End If
        
        Return columns
    End Function
    
    Private Function ExtractJoinColumns(query As String) As List(Of String)
        Dim columns As New List(Of String)
        
        ' Extract JOIN conditions
        Dim joinPattern As String = "JOIN\s+\w+\s+ON\s+(.+?)(?:\s+WHERE|\s+ORDER|\s+GROUP|\s*$)"
        Dim matches = Regex.Matches(query, joinPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        
        For Each match As Match In matches
            Dim joinCondition = match.Groups(1).Value
            
            ' Extract column names from join condition
            Dim columnPattern As String = "\b([a-zA-Z_][a-zA-Z0-9_]*)\s*="
            Dim columnMatches = Regex.Matches(joinCondition, columnPattern)
            
            For Each colMatch As Match In columnMatches
                Dim columnName = colMatch.Groups(1).Value
                If Not columns.Contains(columnName, StringComparer.OrdinalIgnoreCase) Then
                    columns.Add(columnName)
                End If
            Next
        Next
        
        Return columns
    End Function
    
    Private Function ExtractOrderByColumns(query As String) As List(Of String)
        Dim columns As New List(Of String)
        
        Dim orderByPattern As String = "ORDER\s+BY\s+(.+?)(?:\s*$)"
        Dim match = Regex.Match(query, orderByPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        
        If match.Success Then
            Dim orderByClause = match.Groups(1).Value
            
            ' Extract column names
            Dim columnPattern As String = "\b([a-zA-Z_][a-zA-Z0-9_]*)"
            Dim matches = Regex.Matches(orderByClause, columnPattern)
            
            For Each colMatch As Match In matches
                Dim columnName = colMatch.Groups(1).Value
                If Not columnName.ToLower() = "asc" AndAlso Not columnName.ToLower() = "desc" Then
                    If Not columns.Contains(columnName, StringComparer.OrdinalIgnoreCase) Then
                        columns.Add(columnName)
                    End If
                End If
            Next
        End If
        
        Return columns
    End Function
    
    Private Function ExtractGroupByColumns(query As String) As List(Of String)
        Dim columns As New List(Of String)
        
        Dim groupByPattern As String = "GROUP\s+BY\s+(.+?)(?:\s+ORDER|\s*$)"
        Dim match = Regex.Match(query, groupByPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        
        If match.Success Then
            Dim groupByClause = match.Groups(1).Value
            
            ' Extract column names
            Dim columnPattern As String = "\b([a-zA-Z_][a-zA-Z0-9_]*)"
            Dim matches = Regex.Matches(groupByClause, columnPattern)
            
            For Each colMatch As Match In matches
                Dim columnName = colMatch.Groups(1).Value
                If Not columns.Contains(columnName, StringComparer.OrdinalIgnoreCase) Then
                    columns.Add(columnName)
                End If
            Next
        End If
        
        Return columns
    End Function
    
    Private Function TableHasColumn(table As DatabaseMetadata, columnName As String) As Boolean
        Return table.Columns.Any(Function(c) c.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase))
    End Function
    
    ''' <summary>
    ''' Tạo SQL script để tạo index
    ''' </summary>
    Public Function GenerateCreateIndexScript(suggestion As IndexSuggestion) As String
        Select Case dbType
            Case "sql server"
                Return GenerateSqlServerIndexScript(suggestion)
            Case "mysql"
                Return GenerateMySqlIndexScript(suggestion)
            Case "postgresql"
                Return GeneratePostgreSqlIndexScript(suggestion)
            Case "sqlite"
                Return GenerateSqliteIndexScript(suggestion)
            Case Else
                Return GenerateSqlServerIndexScript(suggestion) ' Default to SQL Server
        End Select
    End Function
    
    Private Function GenerateSqlServerIndexScript(suggestion As IndexSuggestion) As String
        Dim script As New Text.StringBuilder()
        
        script.AppendLine($"-- Index: {suggestion.IndexName}")
        script.AppendLine($"-- Reason: {suggestion.Reason}")
        If Not String.IsNullOrEmpty(suggestion.Query) Then
            script.AppendLine($"-- Related Query: {suggestion.Query.Replace(vbCrLf, " ").Replace(vbLf, " ")}")
        End If
        script.AppendLine()
        
        script.Append($"CREATE ")
        If suggestion.IndexType = IndexType.Unique Then
            script.Append("UNIQUE ")
        End If
        script.Append("NONCLUSTERED INDEX ")
        script.Append($"[{suggestion.IndexName}] ")
        script.Append($"ON [{suggestion.SchemaName}].[{suggestion.TableName}] ")
        script.Append($"({String.Join(", ", suggestion.Columns.Select(Function(c) $"[{c}]"))});")
        
        Return script.ToString()
    End Function
    
    Private Function GenerateMySqlIndexScript(suggestion As IndexSuggestion) As String
        Dim script As New Text.StringBuilder()
        
        script.AppendLine($"-- Index: {suggestion.IndexName}")
        script.AppendLine($"-- Reason: {suggestion.Reason}")
        script.AppendLine()
        
        script.Append($"CREATE ")
        If suggestion.IndexType = IndexType.Unique Then
            script.Append("UNIQUE ")
        End If
        script.Append("INDEX ")
        script.Append($"`{suggestion.IndexName}` ")
        script.Append($"ON `{suggestion.TableName}` ")
        script.Append($"({String.Join(", ", suggestion.Columns.Select(Function(c) $"`{c}`"))});")
        
        Return script.ToString()
    End Function
    
    Private Function GeneratePostgreSqlIndexScript(suggestion As IndexSuggestion) As String
        Dim script As New Text.StringBuilder()
        
        script.AppendLine($"-- Index: {suggestion.IndexName}")
        script.AppendLine($"-- Reason: {suggestion.Reason}")
        script.AppendLine()
        
        script.Append($"CREATE ")
        If suggestion.IndexType = IndexType.Unique Then
            script.Append("UNIQUE ")
        End If
        script.Append("INDEX ")
        script.Append($"""{suggestion.IndexName}"" ")
        script.Append($"ON ""{suggestion.SchemaName}"".""{suggestion.TableName}"" ")
        script.Append($"({String.Join(", ", suggestion.Columns.Select(Function(c) $"""{c}"""))});")
        
        Return script.ToString()
    End Function
    
    Private Function GenerateSqliteIndexScript(suggestion As IndexSuggestion) As String
        Dim script As New Text.StringBuilder()
        
        script.AppendLine($"-- Index: {suggestion.IndexName}")
        script.AppendLine($"-- Reason: {suggestion.Reason}")
        script.AppendLine()
        
        script.Append($"CREATE ")
        If suggestion.IndexType = IndexType.Unique Then
            script.Append("UNIQUE ")
        End If
        script.Append("INDEX ")
        script.Append($"[{suggestion.IndexName}] ")
        script.Append($"ON [{suggestion.TableName}] ")
        script.Append($"({String.Join(", ", suggestion.Columns.Select(Function(c) $"[{c}]"))});")
        
        Return script.ToString()
    End Function
End Class

Public Class IndexSuggestion
    Public Property TableName As String
    Public Property SchemaName As String
    Public Property IndexName As String
    Public Property Columns As List(Of String)
    Public Property IndexType As IndexType
    Public Property Reason As String
    Public Property Query As String
    Public Property Priority As IndexPriority
    
    Public Sub New()
        Columns = New List(Of String)
        SchemaName = "dbo"
        IndexType = IndexType.NonClustered
        Priority = IndexPriority.Medium
    End Sub
End Class

Public Enum IndexType
    NonClustered
    Clustered
    Unique
End Enum

Public Enum IndexPriority
    High
    Medium
    Low
End Enum
