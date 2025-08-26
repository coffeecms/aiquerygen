Imports System.Text.RegularExpressions

Public Class NaturalLanguageProcessor
    Private metadata As List(Of DatabaseMetadata)
    Private dbType As String
    
    Public Sub New(databaseMetadata As List(Of DatabaseMetadata), databaseType As String)
        metadata = databaseMetadata
        dbType = databaseType
    End Sub
    
    Public Function GenerateSQL(naturalQuery As String) As String
        Try
            ' Clean and normalize the input
            Dim query As String = naturalQuery.ToLower().Trim()
            
            ' Determine query type
            Dim queryType As String = DetermineQueryType(query)
            
            Select Case queryType
                Case "SELECT"
                    Return GenerateSelectQuery(query)
                Case "INSERT"
                    Return GenerateInsertQuery(query)
                Case "UPDATE"
                    Return GenerateUpdateQuery(query)
                Case "DELETE"
                    Return GenerateDeleteQuery(query)
                Case Else
                    Return GenerateDefaultResponse(query)
            End Select
            
        Catch ex As Exception
            Return $"-- Error generating SQL: {ex.Message}" & vbCrLf & 
                   "-- Please check your query and try again."
        End Try
    End Function
    
    Private Function DetermineQueryType(query As String) As String
        ' Vietnamese keywords
        If ContainsAny(query, {"tìm", "hiển thị", "xem", "lấy", "select", "show", "display", "get", "list"}) Then
            Return "SELECT"
        ElseIf ContainsAny(query, {"thêm", "chèn", "tạo", "insert", "add", "create"}) Then
            Return "INSERT"
        ElseIf ContainsAny(query, {"cập nhật", "sửa", "thay đổi", "update", "modify", "change"}) Then
            Return "UPDATE"
        ElseIf ContainsAny(query, {"xóa", "delete", "remove"}) Then
            Return "DELETE"
        End If
        
        Return "SELECT" ' Default to SELECT
    End Function
    
    Private Function GenerateSelectQuery(query As String) As String
        Dim sql As New System.Text.StringBuilder()
        
        ' Find relevant tables
        Dim tables As List(Of DatabaseMetadata) = FindRelevantTables(query)
        Dim columns As List(Of String) = FindRelevantColumns(query, tables)
        
        ' Build SELECT clause
        sql.AppendLine("SELECT")
        If columns.Count > 0 Then
            sql.AppendLine($"    {String.Join(", ", columns)}")
        Else
            sql.AppendLine("    *")
        End If
        
        ' Build FROM clause
        If tables.Count > 0 Then
            sql.AppendLine($"FROM {tables(0).TableName}")
            
            ' Add JOINs if multiple tables
            For i As Integer = 1 To tables.Count - 1
                Dim joinCondition As String = FindJoinCondition(tables(0), tables(i))
                If Not String.IsNullOrEmpty(joinCondition) Then
                    sql.AppendLine($"    INNER JOIN {tables(i).TableName} ON {joinCondition}")
                End If
            Next
        Else
            sql.AppendLine("FROM your_table_name")
        End If
        
        ' Build WHERE clause
        Dim whereConditions As List(Of String) = GenerateWhereConditions(query, tables)
        If whereConditions.Count > 0 Then
            sql.AppendLine("WHERE")
            sql.AppendLine($"    {String.Join(" AND ", whereConditions)}")
        End If
        
        ' Add GROUP BY if aggregation is detected
        If ContainsAny(query, {"đếm", "count", "số lượng", "tổng", "sum", "trung bình", "average", "avg"}) Then
            Dim groupColumns As List(Of String) = FindGroupByColumns(query, tables)
            If groupColumns.Count > 0 Then
                sql.AppendLine($"GROUP BY {String.Join(", ", groupColumns)}")
            End If
        End If
        
        ' Add ORDER BY if sorting is mentioned
        If ContainsAny(query, {"sắp xếp", "order", "sort", "theo thứ tự"}) Then
            sql.AppendLine("ORDER BY column_name")
        End If
        
        Return sql.ToString()
    End Function
    
    Private Function FindRelevantTables(query As String) As List(Of DatabaseMetadata)
        Dim relevantTables As New List(Of DatabaseMetadata)
        
        For Each table In metadata
            ' Check table name similarity
            If query.Contains(table.TableName.ToLower()) OrElse
               LevenshteinDistance(query, table.TableName.ToLower()) < 3 Then
                relevantTables.Add(table)
                Continue For
            End If
            
            ' Check if query mentions concepts related to this table
            If Not String.IsNullOrEmpty(table.Description) AndAlso
               ContainsSimilarConcepts(query, table.Description) Then
                relevantTables.Add(table)
            End If
        Next
        
        ' If no tables found, try to guess from column names
        If relevantTables.Count = 0 Then
            For Each table In metadata
                For Each column In table.Columns
                    If query.Contains(column.ColumnName.ToLower()) Then
                        relevantTables.Add(table)
                        Exit For
                    End If
                Next
            Next
        End If
        
        Return relevantTables
    End Function
    
    Private Function FindRelevantColumns(query As String, tables As List(Of DatabaseMetadata)) As List(Of String)
        Dim columns As New List(Of String)
        
        For Each table In tables
            For Each column In table.Columns
                If query.Contains(column.ColumnName.ToLower()) OrElse
                   (Not String.IsNullOrEmpty(column.Description) AndAlso 
                    ContainsSimilarConcepts(query, column.Description)) Then
                    columns.Add($"{table.TableName}.{column.ColumnName}")
                End If
            Next
        Next
        
        Return columns
    End Function
    
    Private Function GenerateWhereConditions(query As String, tables As List(Of DatabaseMetadata)) As List(Of String)
        Dim conditions As New List(Of String)
        
        ' Look for location-based conditions
        If ContainsAny(query, {"ở", "tại", "in", "at", "from"}) Then
            Dim locationPattern As String = "(ở|tại|in|at|from)\s+([a-zA-ZÀ-ỹ\s]+)"
            Dim matches As MatchCollection = Regex.Matches(query, locationPattern, RegexOptions.IgnoreCase)
            
            For Each match As Match In matches
                Dim location As String = match.Groups(2).Value.Trim()
                ' Find columns that might contain location data
                For Each table In tables
                    For Each column In table.Columns
                        If ContainsAny(column.ColumnName.ToLower(), {"city", "địa chỉ", "address", "location", "thành phố"}) Then
                            conditions.Add($"{table.TableName}.{column.ColumnName} = '{location}'")
                        End If
                    Next
                Next
            Next
        End If
        
        ' Look for date conditions
        If ContainsAny(query, {"ngày", "date", "năm", "year", "tháng", "month"}) Then
            conditions.Add("date_column >= 'YYYY-MM-DD'")
        End If
        
        ' Look for numeric conditions
        Dim numberPattern As String = "\d+"
        Dim numberMatches As MatchCollection = Regex.Matches(query, numberPattern)
        If numberMatches.Count > 0 Then
            conditions.Add($"numeric_column = {numberMatches(0).Value}")
        End If
        
        Return conditions
    End Function
    
    Private Function FindJoinCondition(table1 As DatabaseMetadata, table2 As DatabaseMetadata) As String
        ' Look for foreign key relationships
        For Each column In table1.Columns
            If column.IsForeignKey AndAlso column.ForeignKeyTable = table2.TableName Then
                Return $"{table1.TableName}.{column.ColumnName} = {table2.TableName}.{column.ForeignKeyColumn}"
            End If
        Next
        
        For Each column In table2.Columns
            If column.IsForeignKey AndAlso column.ForeignKeyTable = table1.TableName Then
                Return $"{table2.TableName}.{column.ColumnName} = {table1.TableName}.{column.ForeignKeyColumn}"
            End If
        Next
        
        Return ""
    End Function
    
    Private Function FindGroupByColumns(query As String, tables As List(Of DatabaseMetadata)) As List(Of String)
        Dim groupColumns As New List(Of String)
        
        ' Look for columns that are not aggregated
        For Each table In tables
            For Each column In table.Columns
                If Not ContainsAny(column.DataType.ToLower(), {"int", "decimal", "float", "money"}) Then
                    groupColumns.Add($"{table.TableName}.{column.ColumnName}")
                    Exit For ' Just add one for now
                End If
            Next
        Next
        
        Return groupColumns
    End Function
    
    Private Function GenerateInsertQuery(query As String) As String
        Dim sql As New System.Text.StringBuilder()
        Dim tables As List(Of DatabaseMetadata) = FindRelevantTables(query)
        
        If tables.Count > 0 Then
            Dim table As DatabaseMetadata = tables(0)
            sql.AppendLine($"INSERT INTO {table.TableName}")
            
            Dim columnNames As New List(Of String)
            Dim values As New List(Of String)
            
            For Each column In table.Columns
                If Not column.IsPrimaryKey Then ' Assume auto-increment PK
                    columnNames.Add(column.ColumnName)
                    values.Add(GetDefaultValue(column.DataType))
                End If
            Next
            
            sql.AppendLine($"    ({String.Join(", ", columnNames)})")
            sql.AppendLine($"VALUES ({String.Join(", ", values)})")
        Else
            sql.AppendLine("INSERT INTO table_name (column1, column2, column3)")
            sql.AppendLine("VALUES (value1, value2, value3)")
        End If
        
        Return sql.ToString()
    End Function
    
    Private Function GenerateUpdateQuery(query As String) As String
        Dim sql As New System.Text.StringBuilder()
        Dim tables As List(Of DatabaseMetadata) = FindRelevantTables(query)
        
        If tables.Count > 0 Then
            sql.AppendLine($"UPDATE {tables(0).TableName}")
            sql.AppendLine("SET column_name = 'new_value'")
            sql.AppendLine("WHERE condition = 'value'")
        Else
            sql.AppendLine("UPDATE table_name")
            sql.AppendLine("SET column1 = value1, column2 = value2")
            sql.AppendLine("WHERE condition = 'value'")
        End If
        
        Return sql.ToString()
    End Function
    
    Private Function GenerateDeleteQuery(query As String) As String
        Dim sql As New System.Text.StringBuilder()
        Dim tables As List(Of DatabaseMetadata) = FindRelevantTables(query)
        
        If tables.Count > 0 Then
            sql.AppendLine($"DELETE FROM {tables(0).TableName}")
        Else
            sql.AppendLine("DELETE FROM table_name")
        End If
        
        sql.AppendLine("WHERE condition = 'value'")
        
        Return sql.ToString()
    End Function
    
    Private Function GenerateDefaultResponse(query As String) As String
        Return "-- Unable to interpret the natural language query" & vbCrLf &
               "-- Please try rephrasing your request" & vbCrLf &
               "-- Examples:" & vbCrLf &
               "--   'Find all customers in Hanoi'" & vbCrLf &
               "--   'Show total orders by customer'" & vbCrLf &
               "--   'List products with price greater than 100'"
    End Function
    
    Private Function ContainsAny(text As String, keywords As String()) As Boolean
        For Each keyword In keywords
            If text.Contains(keyword) Then
                Return True
            End If
        Next
        Return False
    End Function
    
    Private Function ContainsSimilarConcepts(text As String, description As String) As Boolean
        ' Simple similarity check - can be enhanced with more sophisticated NLP
        Dim textWords As String() = text.Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim descWords As String() = description.ToLower().Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
        
        Dim commonWords As Integer = 0
        For Each word In textWords
            If descWords.Contains(word) Then
                commonWords += 1
            End If
        Next
        
        Return commonWords > 0
    End Function
    
    Private Function LevenshteinDistance(s As String, t As String) As Integer
        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim d(n, m) As Integer
        
        If n = 0 Then Return m
        If m = 0 Then Return n
        
        For i As Integer = 0 To n
            d(i, 0) = i
        Next
        
        For j As Integer = 0 To m
            d(0, j) = j
        Next
        
        For i As Integer = 1 To n
            For j As Integer = 1 To m
                Dim cost As Integer = If(t(j - 1) = s(i - 1), 0, 1)
                d(i, j) = Math.Min(Math.Min(d(i - 1, j) + 1, d(i, j - 1) + 1), d(i - 1, j - 1) + cost)
            Next
        Next
        
        Return d(n, m)
    End Function
    
    Private Function GetDefaultValue(dataType As String) As String
        Select Case dataType.ToLower()
            Case "varchar", "nvarchar", "char", "nchar", "text", "ntext"
                Return "'sample_text'"
            Case "int", "bigint", "smallint", "tinyint"
                Return "0"
            Case "decimal", "numeric", "float", "real", "money"
                Return "0.00"
            Case "datetime", "date", "datetime2"
                Return "'2023-01-01'"
            Case "bit", "boolean"
                Return "0"
            Case Else
                Return "'value'"
        End Select
    End Function
End Class
