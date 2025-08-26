Imports System.Data
Imports Newtonsoft.Json

Public Class DatabaseMetadata
    Public Property TableName As String
    Public Property SchemaName As String
    Public Property TableType As String
    Public Property Description As String
    Public Property Comments As String
    Public Property Columns As List(Of ColumnMetadata)
    Public Property LastAnalyzed As DateTime
    Public Property TableHash As String ' Hash để detect thay đổi

    Public Sub New()
        Columns = New List(Of ColumnMetadata)
        SchemaName = "dbo"
        TableType = "TABLE"
        Description = ""
        Comments = ""
        LastAnalyzed = DateTime.Now
        TableHash = ""
    End Sub
    
    ' For backward compatibility
    Public Property Schema As String
        Get
            Return SchemaName
        End Get
        Set(value As String)
            SchemaName = value
        End Set
    End Property
    
    ''' <summary>
    ''' Tính hash của table structure để detect thay đổi
    ''' </summary>
    Public Function CalculateStructureHash() As String
        Dim hashInput As String = $"{TableName}|{SchemaName}|{TableType}|"
        For Each col In Columns.OrderBy(Function(c) c.ColumnName)
            hashInput += $"{col.ColumnName}|{col.DataType}|{col.IsNullable}|{col.IsPrimaryKey}|{col.IsForeignKey}|"
        Next
        
        Using md5 = System.Security.Cryptography.MD5.Create()
            Dim inputBytes = System.Text.Encoding.UTF8.GetBytes(hashInput)
            Dim hashBytes = md5.ComputeHash(inputBytes)
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function
End Class

Public Class ColumnMetadata
    Public Property ColumnName As String
    Public Property DataType As String
    Public Property IsNullable As Boolean
    Public Property IsPrimaryKey As Boolean
    Public Property IsForeignKey As Boolean
    Public Property ForeignKeyTable As String
    Public Property ForeignKeyColumn As String
    Public Property Description As String
    Public Property MaxLength As Integer?
    Public Property DefaultValue As String
    Public Property NumericPrecision As Integer?
    Public Property NumericScale As Integer?
    Public Property IsAutoIncrement As Boolean
    Public Property ReferencedTable As String
    Public Property ReferencedColumn As String

    Public Sub New()
        ColumnName = ""
        DataType = ""
        IsNullable = True
        IsPrimaryKey = False
        IsForeignKey = False
        ForeignKeyTable = ""
        ForeignKeyColumn = ""
        Description = ""
        MaxLength = Nothing
        DefaultValue = ""
        NumericPrecision = Nothing
        NumericScale = Nothing
        IsAutoIncrement = False
        ReferencedTable = ""
        ReferencedColumn = ""
    End Sub
End Class

Public Class DatabaseAnalyzer
    Private connection As IDbConnection
    Private dbType As String
    
    Public Sub New(conn As IDbConnection, databaseType As String)
        connection = conn
        dbType = databaseType.ToLower()
    End Sub
    
    ''' <summary>
    ''' Phân tích database với chế độ incremental - chỉ cập nhật thay đổi
    ''' </summary>
    Public Function AnalyzeDatabaseIncremental(existingMetadata As List(Of DatabaseMetadata)) As IncrementalAnalysisResult
        Dim result As New IncrementalAnalysisResult()
        
        Try
            ' Lấy metadata hiện tại từ database
            Dim currentTables = AnalyzeDatabase()
            
            ' So sánh với metadata cũ
            result = CompareMetadata(existingMetadata, currentTables)
            
        Catch ex As Exception
            Throw New Exception($"Error in incremental analysis: {ex.Message}", ex)
        End Try
        
        Return result
    End Function
    
    ''' <summary>
    ''' So sánh metadata cũ và mới để tìm thay đổi
    ''' </summary>
    Private Function CompareMetadata(oldMetadata As List(Of DatabaseMetadata), newMetadata As List(Of DatabaseMetadata)) As IncrementalAnalysisResult
        Dim result As New IncrementalAnalysisResult()
        
        ' Tạo dictionary để tra cứu nhanh
        Dim oldTables = oldMetadata.ToDictionary(Function(t) $"{t.SchemaName}.{t.TableName}")
        Dim newTables = newMetadata.ToDictionary(Function(t) $"{t.SchemaName}.{t.TableName}")
        
        ' Tìm bảng mới
        For Each kvp In newTables
            If Not oldTables.ContainsKey(kvp.Key) Then
                result.AddedTables.Add(kvp.Value)
            End If
        Next
        
        ' Tìm bảng bị xóa
        For Each kvp In oldTables
            If Not newTables.ContainsKey(kvp.Key) Then
                result.DeletedTables.Add(kvp.Value)
            End If
        Next
        
        ' Tìm bảng thay đổi
        For Each kvp In newTables
            If oldTables.ContainsKey(kvp.Key) Then
                Dim oldTable = oldTables(kvp.Key)
                Dim newTable = kvp.Value
                
                ' So sánh hash structure
                Dim oldHash = oldTable.CalculateStructureHash()
                Dim newHash = newTable.CalculateStructureHash()
                
                If oldHash <> newHash Then
                    Dim changes = CompareTableStructure(oldTable, newTable)
                    If changes.HasChanges Then
                        result.ModifiedTables.Add(changes)
                    End If
                End If
            End If
        Next
        
        ' Cập nhật merged metadata
        result.MergedMetadata = CreateMergedMetadata(oldMetadata, result)
        
        Return result
    End Function
    
    ''' <summary>
    ''' So sánh cấu trúc 2 bảng
    ''' </summary>
    Private Function CompareTableStructure(oldTable As DatabaseMetadata, newTable As DatabaseMetadata) As TableChanges
        Dim changes As New TableChanges() With {
            .TableName = newTable.TableName,
            .SchemaName = newTable.SchemaName,
            .OldTable = oldTable,
            .NewTable = newTable
        }
        
        ' Tạo dictionary columns để so sánh
        Dim oldColumns = oldTable.Columns.ToDictionary(Function(c) c.ColumnName)
        Dim newColumns = newTable.Columns.ToDictionary(Function(c) c.ColumnName)
        
        ' Tìm cột mới
        For Each kvp In newColumns
            If Not oldColumns.ContainsKey(kvp.Key) Then
                changes.AddedColumns.Add(kvp.Value)
            End If
        Next
        
        ' Tìm cột bị xóa
        For Each kvp In oldColumns
            If Not newColumns.ContainsKey(kvp.Key) Then
                changes.DeletedColumns.Add(kvp.Value)
            End If
        Next
        
        ' Tìm cột thay đổi
        For Each kvp In newColumns
            If oldColumns.ContainsKey(kvp.Key) Then
                Dim oldColumn = oldColumns(kvp.Key)
                Dim newColumn = kvp.Value
                
                If Not ColumnsAreEqual(oldColumn, newColumn) Then
                    changes.ModifiedColumns.Add(New ColumnChange With {
                        .OldColumn = oldColumn,
                        .NewColumn = newColumn
                    })
                End If
            End If
        Next
        
        Return changes
    End Function
    
    ''' <summary>
    ''' So sánh 2 cột xem có giống nhau không
    ''' </summary>
    Private Function ColumnsAreEqual(col1 As ColumnMetadata, col2 As ColumnMetadata) As Boolean
        Return col1.ColumnName = col2.ColumnName AndAlso
               col1.DataType = col2.DataType AndAlso
               col1.IsNullable = col2.IsNullable AndAlso
               col1.IsPrimaryKey = col2.IsPrimaryKey AndAlso
               col1.IsForeignKey = col2.IsForeignKey AndAlso
               col1.MaxLength = col2.MaxLength
    End Function
    
    ''' <summary>
    ''' Tạo merged metadata từ old metadata và changes
    ''' </summary>
    Private Function CreateMergedMetadata(oldMetadata As List(Of DatabaseMetadata), result As IncrementalAnalysisResult) As List(Of DatabaseMetadata)
        Dim merged As New List(Of DatabaseMetadata)
        
        ' Copy old metadata, loại trừ những bảng bị xóa
        For Each table In oldMetadata
            Dim tableKey = $"{table.SchemaName}.{table.TableName}"
            If Not result.DeletedTables.Any(Function(t) $"{t.SchemaName}.{t.TableName}" = tableKey) Then
                merged.Add(table)
            End If
        Next
        
        ' Thêm bảng mới
        merged.AddRange(result.AddedTables)
        
        ' Cập nhật bảng thay đổi
        For Each change In result.ModifiedTables
            Dim existingTable = merged.FirstOrDefault(Function(t) t.SchemaName = change.SchemaName AndAlso t.TableName = change.TableName)

            If existingTable IsNot Nothing Then
                Dim index = merged.IndexOf(existingTable)
                merged(index) = change.NewTable
            End If
        Next
        
        Return merged
    End Function
    
    Public Function AnalyzeDatabase() As List(Of DatabaseMetadata)
        Dim tables As New List(Of DatabaseMetadata)
        
        Try
            Select Case dbType
                Case "sql server"
                    tables = AnalyzeSqlServer()
                Case "mysql"
                    tables = AnalyzeMySql()
                Case "postgresql"
                    tables = AnalyzePostgreSQL()
                Case "sqlite"
                    tables = AnalyzeSQLite()
                Case Else
                    Throw New NotSupportedException($"Database type '{dbType}' is not supported for analysis")
            End Select
            
        Catch ex As Exception
            Throw New Exception($"Error analyzing database: {ex.Message}", ex)
        End Try
        
        Return tables
    End Function
    
    Private Function AnalyzeSqlServer() As List(Of DatabaseMetadata)
        Dim tables As New List(Of DatabaseMetadata)
        
        ' Get all tables
        Dim tableQuery As String = "
            SELECT 
                t.TABLE_SCHEMA,
                t.TABLE_NAME,
                ISNULL(ep.value, '') as TABLE_DESCRIPTION
            FROM INFORMATION_SCHEMA.TABLES t
            LEFT JOIN sys.tables st ON st.name = t.TABLE_NAME
            LEFT JOIN sys.extended_properties ep ON ep.major_id = st.object_id AND ep.minor_id = 0 AND ep.name = 'MS_Description'
            WHERE t.TABLE_TYPE = 'BASE TABLE'
            ORDER BY t.TABLE_SCHEMA, t.TABLE_NAME"
        
        Using cmd As IDbCommand = connection.CreateCommand()
            cmd.CommandText = tableQuery
            Using reader As IDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim table As New DatabaseMetadata()
                    table.SchemaName = reader("TABLE_SCHEMA").ToString()
                    table.TableName = reader("TABLE_NAME").ToString()
                    table.Description = reader("TABLE_DESCRIPTION").ToString()
                    table.TableType = "TABLE"
                    tables.Add(table)
                End While
            End Using
        End Using
        
        ' Get columns for each table
        For Each table In tables
            table.Columns = GetSqlServerColumns(table.SchemaName, table.TableName)
        Next
        
        Return tables
    End Function
    
    Private Function GetSqlServerColumns(schema As String, tableName As String) As List(Of ColumnMetadata)
        Dim columns As New List(Of ColumnMetadata)
        
        Dim columnQuery As String = $"
            SELECT 
                c.COLUMN_NAME,
                c.DATA_TYPE,
                c.IS_NULLABLE,
                c.CHARACTER_MAXIMUM_LENGTH,
                CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END as IS_PRIMARY_KEY,
                CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END as IS_FOREIGN_KEY,
                fk.REFERENCED_TABLE_NAME as FK_TABLE,
                fk.REFERENCED_COLUMN_NAME as FK_COLUMN,
                ISNULL(ep.value, '') as COLUMN_DESCRIPTION
            FROM INFORMATION_SCHEMA.COLUMNS c
            LEFT JOIN (
                SELECT ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku ON tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
            ) pk ON c.TABLE_SCHEMA = pk.TABLE_SCHEMA AND c.TABLE_NAME = pk.TABLE_NAME AND c.COLUMN_NAME = pk.COLUMN_NAME
            LEFT JOIN (
                SELECT 
                    ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME,
                    rc.UNIQUE_CONSTRAINT_SCHEMA as REFERENCED_TABLE_SCHEMA,
                    ku2.TABLE_NAME as REFERENCED_TABLE_NAME,
                    ku2.COLUMN_NAME as REFERENCED_COLUMN_NAME
                FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku ON rc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku2 ON rc.UNIQUE_CONSTRAINT_NAME = ku2.CONSTRAINT_NAME
            ) fk ON c.TABLE_SCHEMA = fk.TABLE_SCHEMA AND c.TABLE_NAME = fk.TABLE_NAME AND c.COLUMN_NAME = fk.COLUMN_NAME
            LEFT JOIN sys.tables st ON st.name = c.TABLE_NAME
            LEFT JOIN sys.columns sc ON sc.object_id = st.object_id AND sc.name = c.COLUMN_NAME
            LEFT JOIN sys.extended_properties ep ON ep.major_id = st.object_id AND ep.minor_id = sc.column_id AND ep.name = 'MS_Description'
            WHERE c.TABLE_SCHEMA = '{schema}' AND c.TABLE_NAME = '{tableName}'
            ORDER BY c.ORDINAL_POSITION"
        
        Using cmd As IDbCommand = connection.CreateCommand()
            cmd.CommandText = columnQuery
            Using reader As IDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim column As New ColumnMetadata()
                    column.ColumnName = reader("COLUMN_NAME").ToString()
                    column.DataType = reader("DATA_TYPE").ToString()
                    column.IsNullable = reader("IS_NULLABLE").ToString().ToUpper() = "YES"
                    column.IsPrimaryKey = Convert.ToBoolean(reader("IS_PRIMARY_KEY"))
                    column.IsForeignKey = Convert.ToBoolean(reader("IS_FOREIGN_KEY"))
                    column.ForeignKeyTable = reader("FK_TABLE").ToString()
                    column.ForeignKeyColumn = reader("FK_COLUMN").ToString()
                    column.Description = reader("COLUMN_DESCRIPTION").ToString()
                    
                    If Not IsDBNull(reader("CHARACTER_MAXIMUM_LENGTH")) Then
                        column.MaxLength = Convert.ToInt32(reader("CHARACTER_MAXIMUM_LENGTH"))
                    End If
                    
                    columns.Add(column)
                End While
            End Using
        End Using
        
        Return columns
    End Function
    
    Private Function AnalyzeMySql() As List(Of DatabaseMetadata)
        Dim tables As New List(Of DatabaseMetadata)
        
        ' Get all tables
        Dim tableQuery As String = "
            SELECT 
                TABLE_SCHEMA,
                TABLE_NAME,
                TABLE_TYPE,
                TABLE_COMMENT
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = DATABASE()
            ORDER BY TABLE_NAME"
            
        Using cmd As IDbCommand = connection.CreateCommand()
            cmd.CommandText = tableQuery
            Using reader As IDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim table As New DatabaseMetadata()
                    table.SchemaName = reader("TABLE_SCHEMA").ToString()
                    table.TableName = reader("TABLE_NAME").ToString()
                    table.TableType = reader("TABLE_TYPE").ToString()
                    table.Description = reader("TABLE_COMMENT").ToString()
                    table.LastAnalyzed = DateTime.Now
                    tables.Add(table)
                End While
            End Using
        End Using
        
        ' Get columns for each table
        For Each table In tables
            Dim columnQuery As String = "
                SELECT 
                    COLUMN_NAME,
                    DATA_TYPE,
                    IS_NULLABLE,
                    COLUMN_DEFAULT,
                    CHARACTER_MAXIMUM_LENGTH,
                    NUMERIC_PRECISION,
                    NUMERIC_SCALE,
                    COLUMN_COMMENT,
                    COLUMN_KEY,
                    EXTRA
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tableName
                ORDER BY ORDINAL_POSITION"
                
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = columnQuery
                Dim param As IDbDataParameter = cmd.CreateParameter()
                param.ParameterName = "@tableName"
                param.Value = table.TableName
                cmd.Parameters.Add(param)
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim column As New ColumnMetadata()
                        column.ColumnName = reader("COLUMN_NAME").ToString()
                        column.DataType = reader("DATA_TYPE").ToString()
                        column.IsNullable = reader("IS_NULLABLE").ToString() = "YES"
                        column.DefaultValue = If(reader("COLUMN_DEFAULT") Is DBNull.Value, "", reader("COLUMN_DEFAULT").ToString())
                        column.MaxLength = If(reader("CHARACTER_MAXIMUM_LENGTH") Is DBNull.Value, 0, Convert.ToInt32(reader("CHARACTER_MAXIMUM_LENGTH")))
                        column.NumericPrecision = If(reader("NUMERIC_PRECISION") Is DBNull.Value, 0, Convert.ToInt32(reader("NUMERIC_PRECISION")))
                        column.NumericScale = If(reader("NUMERIC_SCALE") Is DBNull.Value, 0, Convert.ToInt32(reader("NUMERIC_SCALE")))
                        column.Description = reader("COLUMN_COMMENT").ToString()
                        column.IsPrimaryKey = reader("COLUMN_KEY").ToString() = "PRI"
                        column.IsAutoIncrement = reader("EXTRA").ToString().Contains("auto_increment")
                        
                        table.Columns.Add(column)
                    End While
                End Using
            End Using
            
            ' Get foreign key information
            Dim fkQuery As String = "
                SELECT 
                    COLUMN_NAME,
                    REFERENCED_TABLE_NAME,
                    REFERENCED_COLUMN_NAME
                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
                WHERE TABLE_SCHEMA = DATABASE() 
                    AND TABLE_NAME = @tableName 
                    AND REFERENCED_TABLE_NAME IS NOT NULL"
                    
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = fkQuery
                Dim param As IDbDataParameter = cmd.CreateParameter()
                param.ParameterName = "@tableName"
                param.Value = table.TableName
                cmd.Parameters.Add(param)
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim columnName As String = reader("COLUMN_NAME").ToString()
                        Dim refTable As String = reader("REFERENCED_TABLE_NAME").ToString()
                        Dim refColumn As String = reader("REFERENCED_COLUMN_NAME").ToString()
                        
                        Dim column = table.Columns.FirstOrDefault(Function(c) c.ColumnName = columnName)
                        If column IsNot Nothing Then
                            column.IsForeignKey = True
                            column.ReferencedTable = refTable
                            column.ReferencedColumn = refColumn
                        End If
                    End While
                End Using
            End Using
            
            table.TableHash = table.CalculateStructureHash()
        Next
        
        Return tables
    End Function
    
    Private Function AnalyzePostgreSQL() As List(Of DatabaseMetadata)
        Dim tables As New List(Of DatabaseMetadata)
        
        ' Get all tables
        Dim tableQuery As String = "
            SELECT 
                schemaname as table_schema,
                tablename as table_name,
                'TABLE' as table_type
            FROM pg_tables 
            WHERE schemaname = 'public'
            ORDER BY tablename"
            
        Using cmd As IDbCommand = connection.CreateCommand()
            cmd.CommandText = tableQuery
            Using reader As IDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim table As New DatabaseMetadata()
                    table.SchemaName = reader("table_schema").ToString()
                    table.TableName = reader("table_name").ToString()
                    table.TableType = reader("table_type").ToString()
                    table.Description = ""
                    table.LastAnalyzed = DateTime.Now
                    tables.Add(table)
                End While
            End Using
        End Using
        
        ' Get columns for each table
        For Each table In tables
            Dim columnQuery As String = "
                SELECT 
                    c.column_name,
                    c.data_type,
                    c.is_nullable,
                    c.column_default,
                    c.character_maximum_length,
                    c.numeric_precision,
                    c.numeric_scale,
                    CASE 
                        WHEN pk.column_name IS NOT NULL THEN true 
                        ELSE false 
                    END as is_primary_key
                FROM information_schema.columns c
                LEFT JOIN (
                    SELECT ku.column_name
                    FROM information_schema.table_constraints tc
                    INNER JOIN information_schema.key_column_usage ku
                        ON tc.constraint_name = ku.constraint_name
                    WHERE tc.constraint_type = 'PRIMARY KEY'
                        AND tc.table_schema = 'public'
                        AND tc.table_name = @tableName
                ) pk ON c.column_name = pk.column_name
                WHERE c.table_schema = 'public' AND c.table_name = @tableName
                ORDER BY c.ordinal_position"
                
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = columnQuery
                Dim param As IDbDataParameter = cmd.CreateParameter()
                param.ParameterName = "@tableName"
                param.Value = table.TableName
                cmd.Parameters.Add(param)
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim column As New ColumnMetadata()
                        column.ColumnName = reader("column_name").ToString()
                        column.DataType = reader("data_type").ToString()
                        column.IsNullable = reader("is_nullable").ToString() = "YES"
                        column.DefaultValue = If(reader("column_default") Is DBNull.Value, "", reader("column_default").ToString())
                        column.MaxLength = If(reader("character_maximum_length") Is DBNull.Value, 0, Convert.ToInt32(reader("character_maximum_length")))
                        column.NumericPrecision = If(reader("numeric_precision") Is DBNull.Value, 0, Convert.ToInt32(reader("numeric_precision")))
                        column.NumericScale = If(reader("numeric_scale") Is DBNull.Value, 0, Convert.ToInt32(reader("numeric_scale")))
                        column.Description = ""
                        column.IsPrimaryKey = Convert.ToBoolean(reader("is_primary_key"))
                        column.IsAutoIncrement = column.DefaultValue.Contains("nextval")
                        
                        table.Columns.Add(column)
                    End While
                End Using
            End Using
            
            ' Get foreign key information
            Dim fkQuery As String = "
                SELECT 
                    kcu.column_name,
                    ccu.table_name AS referenced_table_name,
                    ccu.column_name AS referenced_column_name
                FROM information_schema.table_constraints tc
                JOIN information_schema.key_column_usage kcu
                    ON tc.constraint_name = kcu.constraint_name
                JOIN information_schema.constraint_column_usage ccu
                    ON ccu.constraint_name = tc.constraint_name
                WHERE tc.constraint_type = 'FOREIGN KEY'
                    AND tc.table_schema = 'public'
                    AND tc.table_name = @tableName"
                    
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = fkQuery
                Dim param As IDbDataParameter = cmd.CreateParameter()
                param.ParameterName = "@tableName"
                param.Value = table.TableName
                cmd.Parameters.Add(param)
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim columnName As String = reader("column_name").ToString()
                        Dim refTable As String = reader("referenced_table_name").ToString()
                        Dim refColumn As String = reader("referenced_column_name").ToString()
                        
                        Dim column = table.Columns.FirstOrDefault(Function(c) c.ColumnName = columnName)
                        If column IsNot Nothing Then
                            column.IsForeignKey = True
                            column.ReferencedTable = refTable
                            column.ReferencedColumn = refColumn
                        End If
                    End While
                End Using
            End Using
            
            table.TableHash = table.CalculateStructureHash()
        Next
        
        Return tables
    End Function
    
    Private Function AnalyzeSQLite() As List(Of DatabaseMetadata)
        Dim tables As New List(Of DatabaseMetadata)
        
        ' Get all tables
        Dim tableQuery As String = "
            SELECT name, type
            FROM sqlite_master 
            WHERE type IN ('table', 'view') 
                AND name NOT LIKE 'sqlite_%'
            ORDER BY name"
            
        Using cmd As IDbCommand = connection.CreateCommand()
            cmd.CommandText = tableQuery
            Using reader As IDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim table As New DatabaseMetadata()
                    table.SchemaName = "main"
                    table.TableName = reader("name").ToString()
                    table.TableType = reader("type").ToString().ToUpper()
                    table.Description = ""
                    table.LastAnalyzed = DateTime.Now
                    tables.Add(table)
                End While
            End Using
        End Using
        
        ' Get columns for each table
        For Each table In tables
            ' Use PRAGMA table_info to get column information
            Dim columnQuery As String = $"PRAGMA table_info('{table.TableName}')"
                
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = columnQuery
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim column As New ColumnMetadata()
                        column.ColumnName = reader("name").ToString()
                        column.DataType = reader("type").ToString()
                        column.IsNullable = Convert.ToInt32(reader("notnull")) = 0
                        column.DefaultValue = If(reader("dflt_value") Is DBNull.Value, "", reader("dflt_value").ToString())
                        column.IsPrimaryKey = Convert.ToInt32(reader("pk")) > 0
                        column.Description = ""
                        
                        ' Check for auto increment (INTEGER PRIMARY KEY)
                        column.IsAutoIncrement = column.IsPrimaryKey AndAlso 
                                                column.DataType.ToUpper() = "INTEGER"
                        
                        table.Columns.Add(column)
                    End While
                End Using
            End Using
            
            ' Get foreign key information
            Dim fkQuery As String = $"PRAGMA foreign_key_list('{table.TableName}')"
                    
            Using cmd As IDbCommand = connection.CreateCommand()
                cmd.CommandText = fkQuery
                
                Using reader As IDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim columnName As String = reader("from").ToString()
                        Dim refTable As String = reader("table").ToString()
                        Dim refColumn As String = reader("to").ToString()
                        
                        Dim column = table.Columns.FirstOrDefault(Function(c) c.ColumnName = columnName)
                        If column IsNot Nothing Then
                            column.IsForeignKey = True
                            column.ReferencedTable = refTable
                            column.ReferencedColumn = refColumn
                        End If
                    End While
                End Using
            End Using
            
            table.TableHash = table.CalculateStructureHash()
        Next
        
        Return tables
    End Function
    
    Public Shared Sub SaveMetadata(metadata As List(Of DatabaseMetadata), filePath As String)
        Try
            Dim json As String = JsonConvert.SerializeObject(metadata, Formatting.Indented)
            IO.File.WriteAllText(filePath, json)
        Catch ex As Exception
            Throw New Exception($"Error saving metadata: {ex.Message}", ex)
        End Try
    End Sub
    
    Public Shared Function LoadMetadata(filePath As String) As List(Of DatabaseMetadata)
        Try
            If Not IO.File.Exists(filePath) Then
                Return New List(Of DatabaseMetadata)
            End If
            
            Dim json As String = IO.File.ReadAllText(filePath)
            Return JsonConvert.DeserializeObject(Of List(Of DatabaseMetadata))(json)
        Catch ex As Exception
            Throw New Exception($"Error loading metadata: {ex.Message}", ex)
        End Try
    End Function
End Class

''' <summary>
''' Kết quả phân tích incremental
''' </summary>
Public Class IncrementalAnalysisResult
    Public Property AddedTables As List(Of DatabaseMetadata)
    Public Property DeletedTables As List(Of DatabaseMetadata)
    Public Property ModifiedTables As List(Of TableChanges)
    Public Property MergedMetadata As List(Of DatabaseMetadata)
    
    Public Sub New()
        AddedTables = New List(Of DatabaseMetadata)
        DeletedTables = New List(Of DatabaseMetadata)
        ModifiedTables = New List(Of TableChanges)
        MergedMetadata = New List(Of DatabaseMetadata)
    End Sub
    
    Public ReadOnly Property HasChanges As Boolean
        Get
            Return AddedTables.Count > 0 OrElse DeletedTables.Count > 0 OrElse ModifiedTables.Count > 0
        End Get
    End Property
    
    Public Function GetSummary() As String
        Dim summary As New Text.StringBuilder()
        summary.AppendLine("Database Analysis Summary:")
        summary.AppendLine($"- Added Tables: {AddedTables.Count}")
        summary.AppendLine($"- Deleted Tables: {DeletedTables.Count}")
        summary.AppendLine($"- Modified Tables: {ModifiedTables.Count}")
        
        If AddedTables.Count > 0 Then
            summary.AppendLine()
            summary.AppendLine("Added Tables:")
            For Each table In AddedTables
                summary.AppendLine($"  + {table.SchemaName}.{table.TableName}")
            Next
        End If
        
        If DeletedTables.Count > 0 Then
            summary.AppendLine()
            summary.AppendLine("Deleted Tables:")
            For Each table In DeletedTables
                summary.AppendLine($"  - {table.SchemaName}.{table.TableName}")
            Next
        End If
        
        If ModifiedTables.Count > 0 Then
            summary.AppendLine()
            summary.AppendLine("Modified Tables:")
            For Each change In ModifiedTables
                summary.AppendLine($"  * {change.SchemaName}.{change.TableName}")
                summary.AppendLine($"    - Added Columns: {change.AddedColumns.Count}")
                summary.AppendLine($"    - Deleted Columns: {change.DeletedColumns.Count}")
                summary.AppendLine($"    - Modified Columns: {change.ModifiedColumns.Count}")
            Next
        End If
        
        Return summary.ToString()
    End Function
End Class

''' <summary>
''' Thay đổi của một bảng
''' </summary>
Public Class TableChanges
    Public Property TableName As String
    Public Property SchemaName As String
    Public Property OldTable As DatabaseMetadata
    Public Property NewTable As DatabaseMetadata
    Public Property AddedColumns As List(Of ColumnMetadata)
    Public Property DeletedColumns As List(Of ColumnMetadata)
    Public Property ModifiedColumns As List(Of ColumnChange)
    
    Public Sub New()
        AddedColumns = New List(Of ColumnMetadata)
        DeletedColumns = New List(Of ColumnMetadata)
        ModifiedColumns = New List(Of ColumnChange)
    End Sub
    
    Public ReadOnly Property HasChanges As Boolean
        Get
            Return AddedColumns.Count > 0 OrElse DeletedColumns.Count > 0 OrElse ModifiedColumns.Count > 0
        End Get
    End Property
End Class

''' <summary>
''' Thay đổi của một cột
''' </summary>
Public Class ColumnChange
    Public Property OldColumn As ColumnMetadata
    Public Property NewColumn As ColumnMetadata
End Class
