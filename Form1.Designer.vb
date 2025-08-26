<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuStrip = New System.Windows.Forms.MenuStrip()
        Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.connectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.analyzeDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.manageMETADATAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.manageIndexesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.aiConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.statusStrip = New System.Windows.Forms.StatusStrip()
        Me.statusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.connectionStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.panelLeft = New System.Windows.Forms.Panel()
        Me.groupBoxTables = New System.Windows.Forms.GroupBox()
        Me.treeViewTables = New System.Windows.Forms.TreeView()
        Me.groupBoxConnection = New System.Windows.Forms.GroupBox()
        Me.buttonConnect = New System.Windows.Forms.Button()
        Me.labelCurrentDB = New System.Windows.Forms.Label()
        Me.comboBoxDBType = New System.Windows.Forms.ComboBox()
        Me.labelDBType = New System.Windows.Forms.Label()
        Me.listBoxSavedConnections = New System.Windows.Forms.ListBox()
        Me.labelSavedConnections = New System.Windows.Forms.Label()
        Me.buttonConnectSaved = New System.Windows.Forms.Button()
        Me.buttonDeleteConnection = New System.Windows.Forms.Button()
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.panelTop = New System.Windows.Forms.Panel()
        Me.groupBoxNaturalQuery = New System.Windows.Forms.GroupBox()
        Me.buttonGenerateSQL = New System.Windows.Forms.Button()
        Me.textBoxNaturalQuery = New System.Windows.Forms.TextBox()
        Me.labelInstruction = New System.Windows.Forms.Label()
        Me.splitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.panelSQL = New System.Windows.Forms.Panel()
        Me.groupBoxGeneratedSQL = New System.Windows.Forms.GroupBox()
        Me.panelSQLButtons = New System.Windows.Forms.Panel()
        Me.buttonCopySQL = New System.Windows.Forms.Button()
        Me.buttonSaveSQL = New System.Windows.Forms.Button()
        Me.buttonExecuteSQL = New System.Windows.Forms.Button()
        Me.textBoxGeneratedSQL = New System.Windows.Forms.TextBox()
        Me.panelResults = New System.Windows.Forms.Panel()
        Me.groupBoxResults = New System.Windows.Forms.GroupBox()
        Me.labelResultsInfo = New System.Windows.Forms.Label()
        Me.dataGridViewResults = New System.Windows.Forms.DataGridView()
        Me.menuStrip.SuspendLayout()
        Me.statusStrip.SuspendLayout()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.panelLeft.SuspendLayout()
        Me.groupBoxTables.SuspendLayout()
        Me.groupBoxConnection.SuspendLayout()
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.panelTop.SuspendLayout()
        Me.groupBoxNaturalQuery.SuspendLayout()
        CType(Me.splitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer3.Panel1.SuspendLayout()
        Me.splitContainer3.Panel2.SuspendLayout()
        Me.splitContainer3.SuspendLayout()
        Me.panelSQL.SuspendLayout()
        Me.groupBoxGeneratedSQL.SuspendLayout()
        Me.panelSQLButtons.SuspendLayout()
        Me.panelResults.SuspendLayout()
        Me.groupBoxResults.SuspendLayout()
        CType(Me.dataGridViewResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'menuStrip
        '
        Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
        Me.menuStrip.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip.Name = "menuStrip"
        Me.menuStrip.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.menuStrip.Size = New System.Drawing.Size(1400, 24)
        Me.menuStrip.TabIndex = 0
        Me.menuStrip.Text = "menuStrip"
        '
        'fileToolStripMenuItem
        '
        Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.connectionToolStripMenuItem, Me.exitToolStripMenuItem})
        Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
        Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.fileToolStripMenuItem.Text = "&File"
        '
        'connectionToolStripMenuItem
        '
        Me.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem"
        Me.connectionToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.connectionToolStripMenuItem.Text = "&Connection"
        '
        'exitToolStripMenuItem
        '
        Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
        Me.exitToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.exitToolStripMenuItem.Text = "E&xit"
        '
        'toolsToolStripMenuItem
        '
        Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.analyzeDBToolStripMenuItem, Me.manageMETADATAToolStripMenuItem, Me.manageIndexesToolStripMenuItem, Me.toolStripSeparator1, Me.aiConfigToolStripMenuItem})
        Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
        Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.toolsToolStripMenuItem.Text = "&Tools"
        '
        'analyzeDBToolStripMenuItem
        '
        Me.analyzeDBToolStripMenuItem.Name = "analyzeDBToolStripMenuItem"
        Me.analyzeDBToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.analyzeDBToolStripMenuItem.Text = "&Analyze Database"
        '
        'manageMETADATAToolStripMenuItem
        '
        Me.manageMETADATAToolStripMenuItem.Name = "manageMETADATAToolStripMenuItem"
        Me.manageMETADATAToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.manageMETADATAToolStripMenuItem.Text = "&Manage Metadata"
        '
        'manageIndexesToolStripMenuItem
        '
        Me.manageIndexesToolStripMenuItem.Name = "manageIndexesToolStripMenuItem"
        Me.manageIndexesToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.manageIndexesToolStripMenuItem.Text = "Manage &Indexes"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(167, 6)
        '
        'aiConfigToolStripMenuItem
        '
        Me.aiConfigToolStripMenuItem.Name = "aiConfigToolStripMenuItem"
        Me.aiConfigToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.aiConfigToolStripMenuItem.Text = "&AI Configuration"
        '
        'helpToolStripMenuItem
        '
        Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.aboutToolStripMenuItem})
        Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
        Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.helpToolStripMenuItem.Text = "&Help"
        '
        'aboutToolStripMenuItem
        '
        Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
        Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.aboutToolStripMenuItem.Text = "&About"
        '
        'statusStrip
        '
        Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusLabel, Me.connectionStatusLabel})
        Me.statusStrip.Location = New System.Drawing.Point(0, 843)
        Me.statusStrip.Name = "statusStrip"
        Me.statusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.statusStrip.Size = New System.Drawing.Size(1400, 22)
        Me.statusStrip.TabIndex = 1
        Me.statusStrip.Text = "statusStrip"
        '
        'statusLabel
        '
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(39, 17)
        Me.statusLabel.Text = "Ready"
        '
        'connectionStatusLabel
        '
        Me.connectionStatusLabel.Name = "connectionStatusLabel"
        Me.connectionStatusLabel.Size = New System.Drawing.Size(88, 17)
        Me.connectionStatusLabel.Text = "Not Connected"
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.splitContainer1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
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
        Me.splitContainer1.Size = New System.Drawing.Size(1400, 819)
        Me.splitContainer1.SplitterDistance = 300
        Me.splitContainer1.SplitterWidth = 5
        Me.splitContainer1.TabIndex = 2
        '
        'panelLeft
        '
        Me.panelLeft.Controls.Add(Me.groupBoxTables)
        Me.panelLeft.Controls.Add(Me.groupBoxConnection)
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelLeft.Location = New System.Drawing.Point(0, 0)
        Me.panelLeft.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Padding = New System.Windows.Forms.Padding(6)
        Me.panelLeft.Size = New System.Drawing.Size(300, 819)
        Me.panelLeft.TabIndex = 0
        '
        'groupBoxTables
        '
        Me.groupBoxTables.Controls.Add(Me.treeViewTables)
        Me.groupBoxTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxTables.Location = New System.Drawing.Point(6, 286)
        Me.groupBoxTables.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBoxTables.Name = "groupBoxTables"
        Me.groupBoxTables.Padding = New System.Windows.Forms.Padding(6)
        Me.groupBoxTables.Size = New System.Drawing.Size(288, 669)
        Me.groupBoxTables.TabIndex = 1
        Me.groupBoxTables.TabStop = False
        Me.groupBoxTables.Text = "Database Structure"
        '
        'treeViewTables
        '
        Me.treeViewTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeViewTables.Location = New System.Drawing.Point(6, 22)
        Me.treeViewTables.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.treeViewTables.Name = "treeViewTables"
        Me.treeViewTables.Size = New System.Drawing.Size(276, 641)
        Me.treeViewTables.TabIndex = 0
        '
        'groupBoxConnection
        '
        Me.groupBoxConnection.Controls.Add(Me.buttonDeleteConnection)
        Me.groupBoxConnection.Controls.Add(Me.buttonConnectSaved)
        Me.groupBoxConnection.Controls.Add(Me.listBoxSavedConnections)
        Me.groupBoxConnection.Controls.Add(Me.labelSavedConnections)
        Me.groupBoxConnection.Controls.Add(Me.buttonConnect)
        Me.groupBoxConnection.Controls.Add(Me.labelCurrentDB)
        Me.groupBoxConnection.Controls.Add(Me.comboBoxDBType)
        Me.groupBoxConnection.Controls.Add(Me.labelDBType)
        Me.groupBoxConnection.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBoxConnection.Location = New System.Drawing.Point(6, 6)
        Me.groupBoxConnection.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBoxConnection.Name = "groupBoxConnection"
        Me.groupBoxConnection.Padding = New System.Windows.Forms.Padding(12)
        Me.groupBoxConnection.Size = New System.Drawing.Size(288, 280)
        Me.groupBoxConnection.TabIndex = 0
        Me.groupBoxConnection.TabStop = False
        Me.groupBoxConnection.Text = "Database Connection"
        '
        'buttonConnect
        '
        Me.buttonConnect.Location = New System.Drawing.Point(12, 58)
        Me.buttonConnect.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonConnect.Name = "buttonConnect"
        Me.buttonConnect.Size = New System.Drawing.Size(117, 29)
        Me.buttonConnect.TabIndex = 2
        Me.buttonConnect.Text = "Connect"
        Me.buttonConnect.UseVisualStyleBackColor = True
        '
        'labelCurrentDB
        '
        Me.labelCurrentDB.AutoSize = True
        Me.labelCurrentDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.labelCurrentDB.ForeColor = System.Drawing.Color.Green
        Me.labelCurrentDB.Location = New System.Drawing.Point(12, 98)
        Me.labelCurrentDB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelCurrentDB.Name = "labelCurrentDB"
        Me.labelCurrentDB.Size = New System.Drawing.Size(92, 13)
        Me.labelCurrentDB.TabIndex = 3
        Me.labelCurrentDB.Text = "Not Connected"
        '
        'comboBoxDBType
        '
        Me.comboBoxDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxDBType.FormattingEnabled = True
        Me.comboBoxDBType.Items.AddRange(New Object() {"SQL Server", "MySQL", "PostgreSQL", "SQLite", "Oracle"})
        Me.comboBoxDBType.Location = New System.Drawing.Point(93, 27)
        Me.comboBoxDBType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.comboBoxDBType.Name = "comboBoxDBType"
        Me.comboBoxDBType.Size = New System.Drawing.Size(189, 23)
        Me.comboBoxDBType.TabIndex = 1
        '
        'labelDBType
        '
        Me.labelDBType.AutoSize = True
        Me.labelDBType.Location = New System.Drawing.Point(12, 30)
        Me.labelDBType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelDBType.Name = "labelDBType"
        Me.labelDBType.Size = New System.Drawing.Size(52, 15)
        Me.labelDBType.TabIndex = 0
        Me.labelDBType.Text = "DB Type:"
        '
        'labelSavedConnections
        '
        Me.labelSavedConnections.AutoSize = True
        Me.labelSavedConnections.Location = New System.Drawing.Point(12, 120)
        Me.labelSavedConnections.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelSavedConnections.Name = "labelSavedConnections"
        Me.labelSavedConnections.Size = New System.Drawing.Size(110, 15)
        Me.labelSavedConnections.TabIndex = 4
        Me.labelSavedConnections.Text = "Saved Connections:"
        '
        'listBoxSavedConnections
        '
        Me.listBoxSavedConnections.FormattingEnabled = True
        Me.listBoxSavedConnections.ItemHeight = 15
        Me.listBoxSavedConnections.Location = New System.Drawing.Point(12, 140)
        Me.listBoxSavedConnections.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.listBoxSavedConnections.Name = "listBoxSavedConnections"
        Me.listBoxSavedConnections.Size = New System.Drawing.Size(264, 79)
        Me.listBoxSavedConnections.TabIndex = 5
        '
        'buttonConnectSaved
        '
        Me.buttonConnectSaved.Location = New System.Drawing.Point(12, 225)
        Me.buttonConnectSaved.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonConnectSaved.Name = "buttonConnectSaved"
        Me.buttonConnectSaved.Size = New System.Drawing.Size(120, 27)
        Me.buttonConnectSaved.TabIndex = 6
        Me.buttonConnectSaved.Text = "Connect Selected"
        Me.buttonConnectSaved.UseVisualStyleBackColor = True
        '
        'buttonDeleteConnection
        '
        Me.buttonDeleteConnection.Location = New System.Drawing.Point(140, 225)
        Me.buttonDeleteConnection.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonDeleteConnection.Name = "buttonDeleteConnection"
        Me.buttonDeleteConnection.Size = New System.Drawing.Size(80, 27)
        Me.buttonDeleteConnection.TabIndex = 7
        Me.buttonDeleteConnection.Text = "Delete"
        Me.buttonDeleteConnection.UseVisualStyleBackColor = True
        '
        'splitContainer2
        '
        Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.splitContainer2.Name = "splitContainer2"
        Me.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.panelTop)
        Me.splitContainer2.Panel1MinSize = 120
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.splitContainer3)
        Me.splitContainer2.Size = New System.Drawing.Size(1095, 819)
        Me.splitContainer2.SplitterDistance = 150
        Me.splitContainer2.SplitterWidth = 5
        Me.splitContainer2.TabIndex = 0
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me.groupBoxNaturalQuery)
        Me.panelTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelTop.Location = New System.Drawing.Point(0, 0)
        Me.panelTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelTop.Name = "panelTop"
        Me.panelTop.Padding = New System.Windows.Forms.Padding(6)
        Me.panelTop.Size = New System.Drawing.Size(1095, 150)
        Me.panelTop.TabIndex = 0
        '
        'groupBoxNaturalQuery
        '
        Me.groupBoxNaturalQuery.Controls.Add(Me.buttonGenerateSQL)
        Me.groupBoxNaturalQuery.Controls.Add(Me.textBoxNaturalQuery)
        Me.groupBoxNaturalQuery.Controls.Add(Me.labelInstruction)
        Me.groupBoxNaturalQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxNaturalQuery.Location = New System.Drawing.Point(6, 6)
        Me.groupBoxNaturalQuery.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBoxNaturalQuery.Name = "groupBoxNaturalQuery"
        Me.groupBoxNaturalQuery.Padding = New System.Windows.Forms.Padding(12)
        Me.groupBoxNaturalQuery.Size = New System.Drawing.Size(1083, 138)
        Me.groupBoxNaturalQuery.TabIndex = 0
        Me.groupBoxNaturalQuery.TabStop = False
        Me.groupBoxNaturalQuery.Text = "Natural Language Query"
        '
        'buttonGenerateSQL
        '
        Me.buttonGenerateSQL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonGenerateSQL.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.buttonGenerateSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonGenerateSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.buttonGenerateSQL.ForeColor = System.Drawing.Color.White
        Me.buttonGenerateSQL.Location = New System.Drawing.Point(947, 58)
        Me.buttonGenerateSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonGenerateSQL.Name = "buttonGenerateSQL"
        Me.buttonGenerateSQL.Size = New System.Drawing.Size(117, 69)
        Me.buttonGenerateSQL.TabIndex = 2
        Me.buttonGenerateSQL.Text = "Generate SQL"
        Me.buttonGenerateSQL.UseVisualStyleBackColor = False
        '
        'textBoxNaturalQuery
        '
        Me.textBoxNaturalQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxNaturalQuery.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.textBoxNaturalQuery.Location = New System.Drawing.Point(12, 58)
        Me.textBoxNaturalQuery.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.textBoxNaturalQuery.Multiline = True
        Me.textBoxNaturalQuery.Name = "textBoxNaturalQuery"
        Me.textBoxNaturalQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxNaturalQuery.Size = New System.Drawing.Size(923, 69)
        Me.textBoxNaturalQuery.TabIndex = 1
        '
        'labelInstruction
        '
        Me.labelInstruction.AutoSize = True
        Me.labelInstruction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.labelInstruction.ForeColor = System.Drawing.Color.DarkBlue
        Me.labelInstruction.Location = New System.Drawing.Point(12, 29)
        Me.labelInstruction.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelInstruction.Name = "labelInstruction"
        Me.labelInstruction.Size = New System.Drawing.Size(204, 15)
        Me.labelInstruction.TabIndex = 0
        Me.labelInstruction.Text = "Enter your query in natural language"
        '
        'splitContainer3
        '
        Me.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.splitContainer3.Name = "splitContainer3"
        Me.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer3.Panel1
        '
        Me.splitContainer3.Panel1.Controls.Add(Me.panelSQL)
        '
        'splitContainer3.Panel2
        '
        Me.splitContainer3.Panel2.Controls.Add(Me.panelResults)
        Me.splitContainer3.Size = New System.Drawing.Size(1095, 664)
        Me.splitContainer3.SplitterDistance = 301
        Me.splitContainer3.SplitterWidth = 5
        Me.splitContainer3.TabIndex = 0
        '
        'panelSQL
        '
        Me.panelSQL.Controls.Add(Me.groupBoxGeneratedSQL)
        Me.panelSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelSQL.Location = New System.Drawing.Point(0, 0)
        Me.panelSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelSQL.Name = "panelSQL"
        Me.panelSQL.Padding = New System.Windows.Forms.Padding(6)
        Me.panelSQL.Size = New System.Drawing.Size(1095, 301)
        Me.panelSQL.TabIndex = 0
        '
        'groupBoxGeneratedSQL
        '
        Me.groupBoxGeneratedSQL.Controls.Add(Me.panelSQLButtons)
        Me.groupBoxGeneratedSQL.Controls.Add(Me.textBoxGeneratedSQL)
        Me.groupBoxGeneratedSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxGeneratedSQL.Location = New System.Drawing.Point(6, 6)
        Me.groupBoxGeneratedSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBoxGeneratedSQL.Name = "groupBoxGeneratedSQL"
        Me.groupBoxGeneratedSQL.Padding = New System.Windows.Forms.Padding(12)
        Me.groupBoxGeneratedSQL.Size = New System.Drawing.Size(1083, 289)
        Me.groupBoxGeneratedSQL.TabIndex = 0
        Me.groupBoxGeneratedSQL.TabStop = False
        Me.groupBoxGeneratedSQL.Text = "Generated SQL Query"
        '
        'panelSQLButtons
        '
        Me.panelSQLButtons.Controls.Add(Me.buttonCopySQL)
        Me.panelSQLButtons.Controls.Add(Me.buttonSaveSQL)
        Me.panelSQLButtons.Controls.Add(Me.buttonExecuteSQL)
        Me.panelSQLButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelSQLButtons.Location = New System.Drawing.Point(12, 242)
        Me.panelSQLButtons.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelSQLButtons.Name = "panelSQLButtons"
        Me.panelSQLButtons.Size = New System.Drawing.Size(1059, 35)
        Me.panelSQLButtons.TabIndex = 1
        '
        'buttonCopySQL
        '
        Me.buttonCopySQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonCopySQL.Location = New System.Drawing.Point(210, 6)
        Me.buttonCopySQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonCopySQL.Name = "buttonCopySQL"
        Me.buttonCopySQL.Size = New System.Drawing.Size(93, 29)
        Me.buttonCopySQL.TabIndex = 2
        Me.buttonCopySQL.Text = "Copy"
        Me.buttonCopySQL.UseVisualStyleBackColor = True
        '
        'buttonSaveSQL
        '
        Me.buttonSaveSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonSaveSQL.Location = New System.Drawing.Point(105, 6)
        Me.buttonSaveSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonSaveSQL.Name = "buttonSaveSQL"
        Me.buttonSaveSQL.Size = New System.Drawing.Size(93, 29)
        Me.buttonSaveSQL.TabIndex = 1
        Me.buttonSaveSQL.Text = "Save"
        Me.buttonSaveSQL.UseVisualStyleBackColor = True
        '
        'buttonExecuteSQL
        '
        Me.buttonExecuteSQL.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.buttonExecuteSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonExecuteSQL.ForeColor = System.Drawing.Color.White
        Me.buttonExecuteSQL.Location = New System.Drawing.Point(0, 6)
        Me.buttonExecuteSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.buttonExecuteSQL.Name = "buttonExecuteSQL"
        Me.buttonExecuteSQL.Size = New System.Drawing.Size(93, 29)
        Me.buttonExecuteSQL.TabIndex = 0
        Me.buttonExecuteSQL.Text = "Execute"
        Me.buttonExecuteSQL.UseVisualStyleBackColor = False
        '
        'textBoxGeneratedSQL
        '
        Me.textBoxGeneratedSQL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxGeneratedSQL.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.textBoxGeneratedSQL.Location = New System.Drawing.Point(12, 29)
        Me.textBoxGeneratedSQL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.textBoxGeneratedSQL.Multiline = True
        Me.textBoxGeneratedSQL.Name = "textBoxGeneratedSQL"
        Me.textBoxGeneratedSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.textBoxGeneratedSQL.Size = New System.Drawing.Size(1059, 208)
        Me.textBoxGeneratedSQL.TabIndex = 0
        '
        'panelResults
        '
        Me.panelResults.Controls.Add(Me.groupBoxResults)
        Me.panelResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelResults.Location = New System.Drawing.Point(0, 0)
        Me.panelResults.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelResults.Name = "panelResults"
        Me.panelResults.Padding = New System.Windows.Forms.Padding(6)
        Me.panelResults.Size = New System.Drawing.Size(1095, 358)
        Me.panelResults.TabIndex = 0
        '
        'groupBoxResults
        '
        Me.groupBoxResults.Controls.Add(Me.labelResultsInfo)
        Me.groupBoxResults.Controls.Add(Me.dataGridViewResults)
        Me.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxResults.Location = New System.Drawing.Point(6, 6)
        Me.groupBoxResults.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBoxResults.Name = "groupBoxResults"
        Me.groupBoxResults.Padding = New System.Windows.Forms.Padding(12)
        Me.groupBoxResults.Size = New System.Drawing.Size(1083, 346)
        Me.groupBoxResults.TabIndex = 0
        Me.groupBoxResults.TabStop = False
        Me.groupBoxResults.Text = "Query Results"
        '
        'labelResultsInfo
        '
        Me.labelResultsInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labelResultsInfo.AutoSize = True
        Me.labelResultsInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.labelResultsInfo.Location = New System.Drawing.Point(12, 316)
        Me.labelResultsInfo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelResultsInfo.Name = "labelResultsInfo"
        Me.labelResultsInfo.Size = New System.Drawing.Size(80, 13)
        Me.labelResultsInfo.TabIndex = 1
        Me.labelResultsInfo.Text = "0 rows returned"
        '
        'dataGridViewResults
        '
        Me.dataGridViewResults.AllowUserToAddRows = False
        Me.dataGridViewResults.AllowUserToDeleteRows = False
        Me.dataGridViewResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewResults.Location = New System.Drawing.Point(12, 29)
        Me.dataGridViewResults.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dataGridViewResults.Name = "dataGridViewResults"
        Me.dataGridViewResults.ReadOnly = True
        Me.dataGridViewResults.RowHeadersWidth = 51
        Me.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridViewResults.Size = New System.Drawing.Size(1059, 281)
        Me.dataGridViewResults.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1400, 865)
        Me.Controls.Add(Me.splitContainer1)
        Me.Controls.Add(Me.statusStrip)
        Me.Controls.Add(Me.menuStrip)
        Me.MainMenuStrip = Me.menuStrip
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MinimumSize = New System.Drawing.Size(931, 686)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AI QueryGen - Intelligent SQL Query Generator"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.menuStrip.ResumeLayout(False)
        Me.menuStrip.PerformLayout()
        Me.statusStrip.ResumeLayout(False)
        Me.statusStrip.PerformLayout()
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.ResumeLayout(False)
        Me.panelLeft.ResumeLayout(False)
        Me.groupBoxTables.ResumeLayout(False)
        Me.groupBoxConnection.ResumeLayout(False)
        Me.groupBoxConnection.PerformLayout()
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel2.ResumeLayout(False)
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer2.ResumeLayout(False)
        Me.panelTop.ResumeLayout(False)
        Me.groupBoxNaturalQuery.ResumeLayout(False)
        Me.groupBoxNaturalQuery.PerformLayout()
        Me.splitContainer3.Panel1.ResumeLayout(False)
        Me.splitContainer3.Panel2.ResumeLayout(False)
        CType(Me.splitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer3.ResumeLayout(False)
        Me.panelSQL.ResumeLayout(False)
        Me.groupBoxGeneratedSQL.ResumeLayout(False)
        Me.groupBoxGeneratedSQL.PerformLayout()
        Me.panelSQLButtons.ResumeLayout(False)
        Me.panelResults.ResumeLayout(False)
        Me.groupBoxResults.ResumeLayout(False)
        Me.groupBoxResults.PerformLayout()
        CType(Me.dataGridViewResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents menuStrip As MenuStrip
    Friend WithEvents fileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents connectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents exitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents analyzeDBToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents manageMETADATAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents manageIndexesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents helpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents aboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents statusStrip As StatusStrip
    Friend WithEvents statusLabel As ToolStripStatusLabel
    Friend WithEvents connectionStatusLabel As ToolStripStatusLabel
    Friend WithEvents splitContainer1 As SplitContainer
    Friend WithEvents panelLeft As Panel
    Friend WithEvents groupBoxConnection As GroupBox
    Friend WithEvents labelCurrentDB As Label
    Friend WithEvents buttonConnect As Button
    Friend WithEvents comboBoxDBType As ComboBox
    Friend WithEvents labelDBType As Label
    Friend WithEvents listBoxSavedConnections As ListBox
    Friend WithEvents labelSavedConnections As Label
    Friend WithEvents buttonConnectSaved As Button
    Friend WithEvents buttonDeleteConnection As Button
    Friend WithEvents groupBoxTables As GroupBox
    Friend WithEvents treeViewTables As TreeView
    Friend WithEvents splitContainer2 As SplitContainer
    Friend WithEvents panelTop As Panel
    Friend WithEvents groupBoxNaturalQuery As GroupBox
    Friend WithEvents buttonGenerateSQL As Button
    Friend WithEvents textBoxNaturalQuery As TextBox
    Friend WithEvents labelInstruction As Label
    Friend WithEvents splitContainer3 As SplitContainer
    Friend WithEvents panelSQL As Panel
    Friend WithEvents groupBoxGeneratedSQL As GroupBox
    Friend WithEvents panelSQLButtons As Panel
    Friend WithEvents buttonCopySQL As Button
    Friend WithEvents buttonSaveSQL As Button
    Friend WithEvents buttonExecuteSQL As Button
    Friend WithEvents textBoxGeneratedSQL As TextBox
    Friend WithEvents panelResults As Panel
    Friend WithEvents groupBoxResults As GroupBox
    Friend WithEvents labelResultsInfo As Label
    Friend WithEvents dataGridViewResults As DataGridView
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents aiConfigToolStripMenuItem As ToolStripMenuItem

End Class
