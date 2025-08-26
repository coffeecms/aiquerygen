Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class MetadataManagementForm
    Private metadata As List(Of DatabaseMetadata)
    Private currentTable As DatabaseMetadata
    Private isModified As Boolean = False

    Public Sub New(existingMetadata As List(Of DatabaseMetadata))
        InitializeComponent()
        metadata = If(existingMetadata, New List(Of DatabaseMetadata)())
    End Sub

    Private Sub MetadataManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupDataGridViews()
        LoadTableList()
        EnableControls(False)
    End Sub

    Private Sub LoadTableList()
        listBoxTables.Items.Clear()
        For Each table In metadata
            listBoxTables.Items.Add(table.TableName)
        Next

        If metadata.Count > 0 Then
            listBoxTables.SelectedIndex = 0
        End If
    End Sub

    Private Sub SetupDataGridViews()
        ' Setup table properties grid
        dataGridViewTableProperties.Columns.Clear()
        dataGridViewTableProperties.Columns.Add("Property", "Property")
        dataGridViewTableProperties.Columns.Add("Value", "Value")
        dataGridViewTableProperties.Columns(0).ReadOnly = True
        dataGridViewTableProperties.Columns(0).Width = 120
        dataGridViewTableProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Setup columns grid
        dataGridViewColumns.Columns.Clear()

        ' Add text columns
        dataGridViewColumns.Columns.Add("ColumnName", "Column Name")
        dataGridViewColumns.Columns.Add("DataType", "Data Type")

        ' Add checkbox columns
        Dim chkNullableColumn As New DataGridViewCheckBoxColumn()
        chkNullableColumn.Name = "IsNullable"
        chkNullableColumn.HeaderText = "Nullable"
        chkNullableColumn.ReadOnly = True
        dataGridViewColumns.Columns.Add(chkNullableColumn)

        Dim chkPKColumn As New DataGridViewCheckBoxColumn()
        chkPKColumn.Name = "IsPrimaryKey"
        chkPKColumn.HeaderText = "Primary Key"
        chkPKColumn.ReadOnly = True
        dataGridViewColumns.Columns.Add(chkPKColumn)

        Dim chkFKColumn As New DataGridViewCheckBoxColumn()
        chkFKColumn.Name = "IsForeignKey"
        chkFKColumn.HeaderText = "Foreign Key"
        chkFKColumn.ReadOnly = True
        dataGridViewColumns.Columns.Add(chkFKColumn)

        ' Add description column
        dataGridViewColumns.Columns.Add("Description", "Description")

        ' Make text columns read-only
        dataGridViewColumns.Columns("ColumnName").ReadOnly = True
        dataGridViewColumns.Columns("DataType").ReadOnly = True

        dataGridViewColumns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub listBoxTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listBoxTables.SelectedIndexChanged
        If listBoxTables.SelectedIndex >= 0 Then
            currentTable = metadata(listBoxTables.SelectedIndex)
            LoadTableData()
            EnableControls(True)
        Else
            EnableControls(False)
        End If
    End Sub

    Private Sub LoadTableData()
        If currentTable Is Nothing Then Return
        
        ' Load table properties
        dataGridViewTableProperties.Rows.Clear()
        dataGridViewTableProperties.Rows.Add("Table Name", currentTable.TableName)
        dataGridViewTableProperties.Rows.Add("Schema", currentTable.SchemaName)
        dataGridViewTableProperties.Rows.Add("Table Type", currentTable.TableType)
        dataGridViewTableProperties.Rows.Add("Description", If(currentTable.Description, ""))
        dataGridViewTableProperties.Rows.Add("Comments", If(currentTable.Comments, ""))
        
        ' Load columns
        dataGridViewColumns.Rows.Clear()
        For Each column In currentTable.Columns
            Dim rowIndex As Integer = dataGridViewColumns.Rows.Add(
                column.ColumnName,
                column.DataType,
                column.IsNullable,
                column.IsPrimaryKey,
                column.IsForeignKey,
                If(column.Description, "")
            )
        Next
    End Sub

    Private Sub EnableControls(enabled As Boolean)
        dataGridViewTableProperties.Enabled = enabled
        dataGridViewColumns.Enabled = enabled
        buttonSave.Enabled = enabled
        buttonExport.Enabled = enabled
        buttonImport.Enabled = True ' Always enabled
    End Sub

    Private Sub dataGridViewTableProperties_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridViewTableProperties.CellValueChanged
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 1 AndAlso currentTable IsNot Nothing Then
            Dim propertyName As String = dataGridViewTableProperties.Item(0, e.RowIndex).Value?.ToString()
            Dim value As String = dataGridViewTableProperties.Item(1, e.RowIndex).Value?.ToString()
            
            Select Case propertyName
                Case "Description"
                    currentTable.Description = value
                    isModified = True
                Case "Comments"
                    currentTable.Comments = value
                    isModified = True
            End Select
            
            UpdateFormTitle()
        End If
    End Sub

    Private Sub dataGridViewColumns_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridViewColumns.CellValueChanged
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 5 AndAlso currentTable IsNot Nothing Then ' Description column
            Dim columnName As String = dataGridViewColumns.Item(0, e.RowIndex).Value?.ToString()
            Dim description As String = dataGridViewColumns.Item(5, e.RowIndex).Value?.ToString()
            
            Dim column = currentTable.Columns.FirstOrDefault(Function(c) c.ColumnName = columnName)
            If column IsNot Nothing Then
                column.Description = description
                isModified = True
                UpdateFormTitle()
            End If
        End If
    End Sub

    Private Sub UpdateFormTitle()
        Me.Text = If(isModified, "Metadata Management*", "Metadata Management")
    End Sub

    Private Sub buttonSave_Click(sender As Object, e As EventArgs) Handles buttonSave.Click
        Try
            Dim metadataPath As String = Path.Combine(Application.StartupPath, "metadata.json")
            DatabaseAnalyzer.SaveMetadata(metadata, metadataPath)
            
            isModified = False
            UpdateFormTitle()
            MessageBox.Show("Metadata saved successfully!", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            
        Catch ex As Exception
            MessageBox.Show($"Error saving metadata: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonExport_Click(sender As Object, e As EventArgs) Handles buttonExport.Click
        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
            saveDialog.DefaultExt = "json"
            saveDialog.FileName = "metadata_export.json"
            
            If saveDialog.ShowDialog() = DialogResult.OK Then
                Try
                    DatabaseAnalyzer.SaveMetadata(metadata, saveDialog.FileName)
                    MessageBox.Show("Metadata exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Error exporting metadata: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub buttonImport_Click(sender As Object, e As EventArgs) Handles buttonImport.Click
        Using openDialog As New OpenFileDialog()
            openDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
            openDialog.Title = "Import Metadata"
            
            If openDialog.ShowDialog() = DialogResult.OK Then
                Try
                    If File.Exists(openDialog.FileName) Then
                        Dim importedMetadata As List(Of DatabaseMetadata) = DatabaseAnalyzer.LoadMetadata(openDialog.FileName)
                        
                        Dim result As DialogResult = MessageBox.Show(
                            $"Found {importedMetadata.Count} tables in import file. This will replace current metadata. Continue?",
                            "Import Confirmation",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question)
                        
                        If result = DialogResult.Yes Then
                            metadata = importedMetadata
                            LoadTableList()
                            isModified = True
                            UpdateFormTitle()
                            MessageBox.Show("Metadata imported successfully!", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Error importing metadata: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub buttonAddTable_Click(sender As Object, e As EventArgs) Handles buttonAddTable.Click
        Dim tableName As String = InputBox("Enter table name:", "Add New Table", "")
        If Not String.IsNullOrWhiteSpace(tableName) Then
            ' Check if table already exists
            If metadata.Any(Function(t) t.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase)) Then
                MessageBox.Show("Table with this name already exists!", "Duplicate Table", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            
            ' Create new table
            Dim newTable As New DatabaseMetadata() With {
                .TableName = tableName,
                .SchemaName = "dbo",
                .TableType = "TABLE",
                .Description = "",
                .Comments = "",
                .Columns = New List(Of ColumnMetadata)()
            }
            
            metadata.Add(newTable)
            LoadTableList()
            listBoxTables.SelectedItem = tableName
            isModified = True
            UpdateFormTitle()
        End If
    End Sub

    Private Sub buttonDeleteTable_Click(sender As Object, e As EventArgs) Handles buttonDeleteTable.Click
        If currentTable IsNot Nothing Then
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete table '{currentTable.TableName}' and all its metadata?",
                "Delete Table",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                metadata.Remove(currentTable)
                LoadTableList()
                isModified = True
                UpdateFormTitle()
            End If
        End If
    End Sub

    Private Sub buttonAddColumn_Click(sender As Object, e As EventArgs) Handles buttonAddColumn.Click
        If currentTable Is Nothing Then Return
        
        Dim columnForm As New ColumnEditForm()
        If columnForm.ShowDialog() = DialogResult.OK Then
            Dim newColumn As ColumnMetadata = columnForm.ColumnData
            
            ' Check if column already exists
            If currentTable.Columns.Any(Function(c) c.ColumnName.Equals(newColumn.ColumnName, StringComparison.OrdinalIgnoreCase)) Then
                MessageBox.Show("Column with this name already exists!", "Duplicate Column", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            
            currentTable.Columns.Add(newColumn)
            LoadTableData()
            isModified = True
            UpdateFormTitle()
        End If
    End Sub

    Private Sub buttonDeleteColumn_Click(sender As Object, e As EventArgs) Handles buttonDeleteColumn.Click
        If dataGridViewColumns.SelectedRows.Count > 0 Then
            Dim selectedIndex As Integer = dataGridViewColumns.SelectedRows(0).Index
            Dim columnName As String = dataGridViewColumns.Item(0, selectedIndex).Value?.ToString()
            
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete column '{columnName}'?",
                "Delete Column",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                currentTable.Columns.RemoveAt(selectedIndex)
                LoadTableData()
                isModified = True
                UpdateFormTitle()
            End If
        End If
    End Sub

    Private Sub MetadataManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isModified Then
            Dim result As DialogResult = MessageBox.Show(
                "You have unsaved changes. Do you want to save before closing?",
                "Unsaved Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                buttonSave_Click(sender, EventArgs.Empty)
            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Public ReadOnly Property ModifiedMetadata As List(Of DatabaseMetadata)
        Get
            Return metadata
        End Get
    End Property
End Class
