Imports System.Windows.Forms

Public Class ColumnEditForm
    Public Property ColumnData As ColumnMetadata

    Private Sub ColumnEditForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize data types
        comboBoxDataType.Items.AddRange(New String() {
            "varchar", "nvarchar", "char", "nchar", "text", "ntext",
            "int", "bigint", "smallint", "tinyint", "bit",
            "decimal", "numeric", "float", "real", "money", "smallmoney",
            "datetime", "datetime2", "date", "time", "timestamp",
            "uniqueidentifier", "binary", "varbinary", "image",
            "xml", "sql_variant"
        })
        
        comboBoxDataType.SelectedIndex = 0
        
        If ColumnData IsNot Nothing Then
            ' Edit mode
            textBoxColumnName.Text = ColumnData.ColumnName
            comboBoxDataType.Text = ColumnData.DataType
            checkBoxIsNullable.Checked = ColumnData.IsNullable
            checkBoxIsPrimaryKey.Checked = ColumnData.IsPrimaryKey
            checkBoxIsForeignKey.Checked = ColumnData.IsForeignKey
            textBoxDescription.Text = ColumnData.Description
            Me.Text = "Edit Column"
        Else
            ' Add mode
            ColumnData = New ColumnMetadata()
            Me.Text = "Add Column"
        End If
    End Sub

    Private Sub buttonOK_Click(sender As Object, e As EventArgs) Handles buttonOK.Click
        If String.IsNullOrWhiteSpace(textBoxColumnName.Text) Then
            MessageBox.Show("Column name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            textBoxColumnName.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(comboBoxDataType.Text) Then
            MessageBox.Show("Data type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            comboBoxDataType.Focus()
            Return
        End If

        ' Update column data
        ColumnData.ColumnName = textBoxColumnName.Text.Trim()
        ColumnData.DataType = comboBoxDataType.Text.Trim()
        ColumnData.IsNullable = checkBoxIsNullable.Checked
        ColumnData.IsPrimaryKey = checkBoxIsPrimaryKey.Checked
        ColumnData.IsForeignKey = checkBoxIsForeignKey.Checked
        ColumnData.Description = textBoxDescription.Text.Trim()

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
