<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ColumnEditForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.labelColumnName = New System.Windows.Forms.Label()
        Me.textBoxColumnName = New System.Windows.Forms.TextBox()
        Me.labelDataType = New System.Windows.Forms.Label()
        Me.comboBoxDataType = New System.Windows.Forms.ComboBox()
        Me.checkBoxIsNullable = New System.Windows.Forms.CheckBox()
        Me.checkBoxIsPrimaryKey = New System.Windows.Forms.CheckBox()
        Me.checkBoxIsForeignKey = New System.Windows.Forms.CheckBox()
        Me.labelDescription = New System.Windows.Forms.Label()
        Me.textBoxDescription = New System.Windows.Forms.TextBox()
        Me.buttonOK = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'labelColumnName
        '
        Me.labelColumnName.AutoSize = True
        Me.labelColumnName.Location = New System.Drawing.Point(12, 15)
        Me.labelColumnName.Name = "labelColumnName"
        Me.labelColumnName.Size = New System.Drawing.Size(87, 15)
        Me.labelColumnName.TabIndex = 0
        Me.labelColumnName.Text = "Column Name:"
        '
        'textBoxColumnName
        '
        Me.textBoxColumnName.Location = New System.Drawing.Point(105, 12)
        Me.textBoxColumnName.Name = "textBoxColumnName"
        Me.textBoxColumnName.Size = New System.Drawing.Size(200, 23)
        Me.textBoxColumnName.TabIndex = 1
        '
        'labelDataType
        '
        Me.labelDataType.AutoSize = True
        Me.labelDataType.Location = New System.Drawing.Point(12, 44)
        Me.labelDataType.Name = "labelDataType"
        Me.labelDataType.Size = New System.Drawing.Size(65, 15)
        Me.labelDataType.TabIndex = 2
        Me.labelDataType.Text = "Data Type:"
        '
        'comboBoxDataType
        '
        Me.comboBoxDataType.FormattingEnabled = True
        Me.comboBoxDataType.Location = New System.Drawing.Point(105, 41)
        Me.comboBoxDataType.Name = "comboBoxDataType"
        Me.comboBoxDataType.Size = New System.Drawing.Size(200, 23)
        Me.comboBoxDataType.TabIndex = 3
        '
        'checkBoxIsNullable
        '
        Me.checkBoxIsNullable.AutoSize = True
        Me.checkBoxIsNullable.Location = New System.Drawing.Point(12, 80)
        Me.checkBoxIsNullable.Name = "checkBoxIsNullable"
        Me.checkBoxIsNullable.Size = New System.Drawing.Size(69, 19)
        Me.checkBoxIsNullable.TabIndex = 4
        Me.checkBoxIsNullable.Text = "Nullable"
        Me.checkBoxIsNullable.UseVisualStyleBackColor = True
        '
        'checkBoxIsPrimaryKey
        '
        Me.checkBoxIsPrimaryKey.AutoSize = True
        Me.checkBoxIsPrimaryKey.Location = New System.Drawing.Point(12, 105)
        Me.checkBoxIsPrimaryKey.Name = "checkBoxIsPrimaryKey"
        Me.checkBoxIsPrimaryKey.Size = New System.Drawing.Size(87, 19)
        Me.checkBoxIsPrimaryKey.TabIndex = 5
        Me.checkBoxIsPrimaryKey.Text = "Primary Key"
        Me.checkBoxIsPrimaryKey.UseVisualStyleBackColor = True
        '
        'checkBoxIsForeignKey
        '
        Me.checkBoxIsForeignKey.AutoSize = True
        Me.checkBoxIsForeignKey.Location = New System.Drawing.Point(12, 130)
        Me.checkBoxIsForeignKey.Name = "checkBoxIsForeignKey"
        Me.checkBoxIsForeignKey.Size = New System.Drawing.Size(86, 19)
        Me.checkBoxIsForeignKey.TabIndex = 6
        Me.checkBoxIsForeignKey.Text = "Foreign Key"
        Me.checkBoxIsForeignKey.UseVisualStyleBackColor = True
        '
        'labelDescription
        '
        Me.labelDescription.AutoSize = True
        Me.labelDescription.Location = New System.Drawing.Point(12, 165)
        Me.labelDescription.Name = "labelDescription"
        Me.labelDescription.Size = New System.Drawing.Size(70, 15)
        Me.labelDescription.TabIndex = 7
        Me.labelDescription.Text = "Description:"
        '
        'textBoxDescription
        '
        Me.textBoxDescription.Location = New System.Drawing.Point(12, 183)
        Me.textBoxDescription.Multiline = True
        Me.textBoxDescription.Name = "textBoxDescription"
        Me.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxDescription.Size = New System.Drawing.Size(293, 80)
        Me.textBoxDescription.TabIndex = 8
        '
        'buttonOK
        '
        Me.buttonOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonOK.ForeColor = System.Drawing.Color.White
        Me.buttonOK.Location = New System.Drawing.Point(149, 278)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.Size = New System.Drawing.Size(75, 29)
        Me.buttonOK.TabIndex = 9
        Me.buttonOK.Text = "OK"
        Me.buttonOK.UseVisualStyleBackColor = False
        '
        'buttonCancel
        '
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonCancel.Location = New System.Drawing.Point(230, 278)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(75, 29)
        Me.buttonCancel.TabIndex = 10
        Me.buttonCancel.Text = "Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'ColumnEditForm
        '
        Me.AcceptButton = Me.buttonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(317, 319)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOK)
        Me.Controls.Add(Me.textBoxDescription)
        Me.Controls.Add(Me.labelDescription)
        Me.Controls.Add(Me.checkBoxIsForeignKey)
        Me.Controls.Add(Me.checkBoxIsPrimaryKey)
        Me.Controls.Add(Me.checkBoxIsNullable)
        Me.Controls.Add(Me.comboBoxDataType)
        Me.Controls.Add(Me.labelDataType)
        Me.Controls.Add(Me.textBoxColumnName)
        Me.Controls.Add(Me.labelColumnName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColumnEditForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Column Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelColumnName As Label
    Friend WithEvents textBoxColumnName As TextBox
    Friend WithEvents labelDataType As Label
    Friend WithEvents comboBoxDataType As ComboBox
    Friend WithEvents checkBoxIsNullable As CheckBox
    Friend WithEvents checkBoxIsPrimaryKey As CheckBox
    Friend WithEvents checkBoxIsForeignKey As CheckBox
    Friend WithEvents labelDescription As Label
    Friend WithEvents textBoxDescription As TextBox
    Friend WithEvents buttonOK As Button
    Friend WithEvents buttonCancel As Button
End Class
