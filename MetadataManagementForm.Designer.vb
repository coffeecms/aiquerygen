<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MetadataManagementForm
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
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.panelLeft = New System.Windows.Forms.Panel()
        Me.groupBoxTables = New System.Windows.Forms.GroupBox()
        Me.panelTableButtons = New System.Windows.Forms.Panel()
        Me.buttonDeleteTable = New System.Windows.Forms.Button()
        Me.buttonAddTable = New System.Windows.Forms.Button()
        Me.listBoxTables = New System.Windows.Forms.ListBox()
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.panelTableInfo = New System.Windows.Forms.Panel()
        Me.groupBoxTableProperties = New System.Windows.Forms.GroupBox()
        Me.dataGridViewTableProperties = New System.Windows.Forms.DataGridView()
        Me.panelColumns = New System.Windows.Forms.Panel()
        Me.groupBoxColumns = New System.Windows.Forms.GroupBox()
        Me.panelColumnButtons = New System.Windows.Forms.Panel()
        Me.buttonDeleteColumn = New System.Windows.Forms.Button()
        Me.buttonAddColumn = New System.Windows.Forms.Button()
        Me.dataGridViewColumns = New System.Windows.Forms.DataGridView()
        Me.panelBottom = New System.Windows.Forms.Panel()
        Me.buttonImport = New System.Windows.Forms.Button()
        Me.buttonExport = New System.Windows.Forms.Button()
        Me.buttonSave = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.panelLeft.SuspendLayout()
        Me.groupBoxTables.SuspendLayout()
        Me.panelTableButtons.SuspendLayout()
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.panelTableInfo.SuspendLayout()
        Me.groupBoxTableProperties.SuspendLayout()
        CType(Me.dataGridViewTableProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelColumns.SuspendLayout()
        Me.groupBoxColumns.SuspendLayout()
        Me.panelColumnButtons.SuspendLayout()
        CType(Me.dataGridViewColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.panelLeft)
        Me.splitContainer1.Panel1MinSize = 250
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
        Me.splitContainer1.Panel2.Controls.Add(Me.panelBottom)
        Me.splitContainer1.Size = New System.Drawing.Size(1200, 700)
        Me.splitContainer1.SplitterDistance = 300
        Me.splitContainer1.TabIndex = 0
        '
        'panelLeft
        '
        Me.panelLeft.Controls.Add(Me.groupBoxTables)
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelLeft.Location = New System.Drawing.Point(0, 0)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Padding = New System.Windows.Forms.Padding(8)
        Me.panelLeft.Size = New System.Drawing.Size(300, 700)
        Me.panelLeft.TabIndex = 0
        '
        'groupBoxTables
        '
        Me.groupBoxTables.Controls.Add(Me.panelTableButtons)
        Me.groupBoxTables.Controls.Add(Me.listBoxTables)
        Me.groupBoxTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxTables.Location = New System.Drawing.Point(8, 8)
        Me.groupBoxTables.Name = "groupBoxTables"
        Me.groupBoxTables.Padding = New System.Windows.Forms.Padding(8)
        Me.groupBoxTables.Size = New System.Drawing.Size(284, 684)
        Me.groupBoxTables.TabIndex = 0
        Me.groupBoxTables.TabStop = False
        Me.groupBoxTables.Text = "Tables"
        '
        'panelTableButtons
        '
        Me.panelTableButtons.Controls.Add(Me.buttonDeleteTable)
        Me.panelTableButtons.Controls.Add(Me.buttonAddTable)
        Me.panelTableButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelTableButtons.Location = New System.Drawing.Point(8, 641)
        Me.panelTableButtons.Name = "panelTableButtons"
        Me.panelTableButtons.Size = New System.Drawing.Size(268, 35)
        Me.panelTableButtons.TabIndex = 1
        '
        'buttonDeleteTable
        '
        Me.buttonDeleteTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonDeleteTable.ForeColor = System.Drawing.Color.Red
        Me.buttonDeleteTable.Location = New System.Drawing.Point(70, 3)
        Me.buttonDeleteTable.Name = "buttonDeleteTable"
        Me.buttonDeleteTable.Size = New System.Drawing.Size(60, 29)
        Me.buttonDeleteTable.TabIndex = 1
        Me.buttonDeleteTable.Text = "Delete"
        Me.buttonDeleteTable.UseVisualStyleBackColor = True
        '
        'buttonAddTable
        '
        Me.buttonAddTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonAddTable.ForeColor = System.Drawing.Color.Green
        Me.buttonAddTable.Location = New System.Drawing.Point(3, 3)
        Me.buttonAddTable.Name = "buttonAddTable"
        Me.buttonAddTable.Size = New System.Drawing.Size(60, 29)
        Me.buttonAddTable.TabIndex = 0
        Me.buttonAddTable.Text = "Add"
        Me.buttonAddTable.UseVisualStyleBackColor = True
        '
        'listBoxTables
        '
        Me.listBoxTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listBoxTables.FormattingEnabled = True
        Me.listBoxTables.ItemHeight = 15
        Me.listBoxTables.Location = New System.Drawing.Point(8, 24)
        Me.listBoxTables.Name = "listBoxTables"
        Me.listBoxTables.Size = New System.Drawing.Size(268, 652)
        Me.listBoxTables.TabIndex = 0
        '
        'splitContainer2
        '
        Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer2.Name = "splitContainer2"
        Me.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.panelTableInfo)
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.panelColumns)
        Me.splitContainer2.Size = New System.Drawing.Size(896, 650)
        Me.splitContainer2.SplitterDistance = 250
        Me.splitContainer2.TabIndex = 1
        '
        'panelTableInfo
        '
        Me.panelTableInfo.Controls.Add(Me.groupBoxTableProperties)
        Me.panelTableInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelTableInfo.Location = New System.Drawing.Point(0, 0)
        Me.panelTableInfo.Name = "panelTableInfo"
        Me.panelTableInfo.Padding = New System.Windows.Forms.Padding(8)
        Me.panelTableInfo.Size = New System.Drawing.Size(896, 250)
        Me.panelTableInfo.TabIndex = 0
        '
        'groupBoxTableProperties
        '
        Me.groupBoxTableProperties.Controls.Add(Me.dataGridViewTableProperties)
        Me.groupBoxTableProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxTableProperties.Location = New System.Drawing.Point(8, 8)
        Me.groupBoxTableProperties.Name = "groupBoxTableProperties"
        Me.groupBoxTableProperties.Padding = New System.Windows.Forms.Padding(8)
        Me.groupBoxTableProperties.Size = New System.Drawing.Size(880, 234)
        Me.groupBoxTableProperties.TabIndex = 0
        Me.groupBoxTableProperties.TabStop = False
        Me.groupBoxTableProperties.Text = "Table Properties (Edit Description and Comments)"
        '
        'dataGridViewTableProperties
        '
        Me.dataGridViewTableProperties.AllowUserToAddRows = False
        Me.dataGridViewTableProperties.AllowUserToDeleteRows = False
        Me.dataGridViewTableProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewTableProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGridViewTableProperties.Location = New System.Drawing.Point(8, 24)
        Me.dataGridViewTableProperties.MultiSelect = False
        Me.dataGridViewTableProperties.Name = "dataGridViewTableProperties"
        Me.dataGridViewTableProperties.RowHeadersWidth = 51
        Me.dataGridViewTableProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dataGridViewTableProperties.Size = New System.Drawing.Size(864, 202)
        Me.dataGridViewTableProperties.TabIndex = 0
        '
        'panelColumns
        '
        Me.panelColumns.Controls.Add(Me.groupBoxColumns)
        Me.panelColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelColumns.Location = New System.Drawing.Point(0, 0)
        Me.panelColumns.Name = "panelColumns"
        Me.panelColumns.Padding = New System.Windows.Forms.Padding(8)
        Me.panelColumns.Size = New System.Drawing.Size(896, 396)
        Me.panelColumns.TabIndex = 0
        '
        'groupBoxColumns
        '
        Me.groupBoxColumns.Controls.Add(Me.panelColumnButtons)
        Me.groupBoxColumns.Controls.Add(Me.dataGridViewColumns)
        Me.groupBoxColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxColumns.Location = New System.Drawing.Point(8, 8)
        Me.groupBoxColumns.Name = "groupBoxColumns"
        Me.groupBoxColumns.Padding = New System.Windows.Forms.Padding(8)
        Me.groupBoxColumns.Size = New System.Drawing.Size(880, 380)
        Me.groupBoxColumns.TabIndex = 0
        Me.groupBoxColumns.TabStop = False
        Me.groupBoxColumns.Text = "Columns (Edit Description for each column)"
        '
        'panelColumnButtons
        '
        Me.panelColumnButtons.Controls.Add(Me.buttonDeleteColumn)
        Me.panelColumnButtons.Controls.Add(Me.buttonAddColumn)
        Me.panelColumnButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelColumnButtons.Location = New System.Drawing.Point(8, 337)
        Me.panelColumnButtons.Name = "panelColumnButtons"
        Me.panelColumnButtons.Size = New System.Drawing.Size(864, 35)
        Me.panelColumnButtons.TabIndex = 1
        '
        'buttonDeleteColumn
        '
        Me.buttonDeleteColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonDeleteColumn.ForeColor = System.Drawing.Color.Red
        Me.buttonDeleteColumn.Location = New System.Drawing.Point(70, 3)
        Me.buttonDeleteColumn.Name = "buttonDeleteColumn"
        Me.buttonDeleteColumn.Size = New System.Drawing.Size(60, 29)
        Me.buttonDeleteColumn.TabIndex = 1
        Me.buttonDeleteColumn.Text = "Delete"
        Me.buttonDeleteColumn.UseVisualStyleBackColor = True
        '
        'buttonAddColumn
        '
        Me.buttonAddColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonAddColumn.ForeColor = System.Drawing.Color.Green
        Me.buttonAddColumn.Location = New System.Drawing.Point(3, 3)
        Me.buttonAddColumn.Name = "buttonAddColumn"
        Me.buttonAddColumn.Size = New System.Drawing.Size(60, 29)
        Me.buttonAddColumn.TabIndex = 0
        Me.buttonAddColumn.Text = "Add"
        Me.buttonAddColumn.UseVisualStyleBackColor = True
        '
        'dataGridViewColumns
        '
        Me.dataGridViewColumns.AllowUserToAddRows = False
        Me.dataGridViewColumns.AllowUserToDeleteRows = False
        Me.dataGridViewColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dataGridViewColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGridViewColumns.Location = New System.Drawing.Point(8, 24)
        Me.dataGridViewColumns.MultiSelect = False
        Me.dataGridViewColumns.Name = "dataGridViewColumns"
        Me.dataGridViewColumns.RowHeadersWidth = 51
        Me.dataGridViewColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridViewColumns.Size = New System.Drawing.Size(864, 348)
        Me.dataGridViewColumns.TabIndex = 0
        '
        'panelBottom
        '
        Me.panelBottom.Controls.Add(Me.buttonImport)
        Me.panelBottom.Controls.Add(Me.buttonExport)
        Me.panelBottom.Controls.Add(Me.buttonSave)
        Me.panelBottom.Controls.Add(Me.buttonCancel)
        Me.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottom.Location = New System.Drawing.Point(0, 650)
        Me.panelBottom.Name = "panelBottom"
        Me.panelBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.panelBottom.Size = New System.Drawing.Size(896, 50)
        Me.panelBottom.TabIndex = 0
        '
        'buttonImport
        '
        Me.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonImport.Location = New System.Drawing.Point(198, 11)
        Me.buttonImport.Name = "buttonImport"
        Me.buttonImport.Size = New System.Drawing.Size(85, 29)
        Me.buttonImport.TabIndex = 3
        Me.buttonImport.Text = "Import"
        Me.buttonImport.UseVisualStyleBackColor = True
        '
        'buttonExport
        '
        Me.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonExport.Location = New System.Drawing.Point(107, 11)
        Me.buttonExport.Name = "buttonExport"
        Me.buttonExport.Size = New System.Drawing.Size(85, 29)
        Me.buttonExport.TabIndex = 2
        Me.buttonExport.Text = "Export"
        Me.buttonExport.UseVisualStyleBackColor = True
        '
        'buttonSave
        '
        Me.buttonSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonSave.ForeColor = System.Drawing.Color.White
        Me.buttonSave.Location = New System.Drawing.Point(11, 11)
        Me.buttonSave.Name = "buttonSave"
        Me.buttonSave.Size = New System.Drawing.Size(90, 29)
        Me.buttonSave.TabIndex = 1
        Me.buttonSave.Text = "Save"
        Me.buttonSave.UseVisualStyleBackColor = False
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonCancel.Location = New System.Drawing.Point(798, 11)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(85, 29)
        Me.buttonCancel.TabIndex = 0
        Me.buttonCancel.Text = "Close"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'MetadataManagementForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.splitContainer1)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "MetadataManagementForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Metadata Management"
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.panelLeft.ResumeLayout(False)
        Me.groupBoxTables.ResumeLayout(False)
        Me.panelTableButtons.ResumeLayout(False)
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel2.ResumeLayout(False)
        Me.splitContainer2.ResumeLayout(False)
        Me.panelTableInfo.ResumeLayout(False)
        Me.groupBoxTableProperties.ResumeLayout(False)
        CType(Me.dataGridViewTableProperties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelColumns.ResumeLayout(False)
        Me.groupBoxColumns.ResumeLayout(False)
        Me.panelColumnButtons.ResumeLayout(False)
        CType(Me.dataGridViewColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents splitContainer1 As SplitContainer
    Friend WithEvents panelLeft As Panel
    Friend WithEvents groupBoxTables As GroupBox
    Friend WithEvents listBoxTables As ListBox
    Friend WithEvents splitContainer2 As SplitContainer
    Friend WithEvents panelTableInfo As Panel
    Friend WithEvents groupBoxTableProperties As GroupBox
    Friend WithEvents dataGridViewTableProperties As DataGridView
    Friend WithEvents panelColumns As Panel
    Friend WithEvents groupBoxColumns As GroupBox
    Friend WithEvents dataGridViewColumns As DataGridView
    Friend WithEvents panelBottom As Panel
    Friend WithEvents buttonSave As Button
    Friend WithEvents buttonCancel As Button
    Friend WithEvents panelTableButtons As Panel
    Friend WithEvents buttonDeleteTable As Button
    Friend WithEvents buttonAddTable As Button
    Friend WithEvents panelColumnButtons As Panel
    Friend WithEvents buttonDeleteColumn As Button
    Friend WithEvents buttonAddColumn As Button
    Friend WithEvents buttonExport As Button
    Friend WithEvents buttonImport As Button
End Class
