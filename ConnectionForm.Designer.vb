<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectionForm
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
        Me.labelTitle = New System.Windows.Forms.Label()
        Me.groupBoxConnection = New System.Windows.Forms.GroupBox()
        Me.buttonBrowse = New System.Windows.Forms.Button()
        Me.checkBoxWindowsAuth = New System.Windows.Forms.CheckBox()
        Me.textBoxPassword = New System.Windows.Forms.TextBox()
        Me.labelPassword = New System.Windows.Forms.Label()
        Me.textBoxUsername = New System.Windows.Forms.TextBox()
        Me.labelUsername = New System.Windows.Forms.Label()
        Me.textBoxDatabase = New System.Windows.Forms.TextBox()
        Me.labelDatabase = New System.Windows.Forms.Label()
        Me.textBoxPort = New System.Windows.Forms.TextBox()
        Me.labelPort = New System.Windows.Forms.Label()
        Me.textBoxServer = New System.Windows.Forms.TextBox()
        Me.labelServer = New System.Windows.Forms.Label()
        Me.buttonTest = New System.Windows.Forms.Button()
        Me.buttonConnect = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.groupBoxConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'labelTitle
        '
        Me.labelTitle.AutoSize = True
        Me.labelTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.labelTitle.ForeColor = System.Drawing.Color.DarkBlue
        Me.labelTitle.Location = New System.Drawing.Point(20, 20)
        Me.labelTitle.Name = "labelTitle"
        Me.labelTitle.Size = New System.Drawing.Size(178, 20)
        Me.labelTitle.TabIndex = 0
        Me.labelTitle.Text = "Database Connection"
        '
        'groupBoxConnection
        '
        Me.groupBoxConnection.Controls.Add(Me.buttonBrowse)
        Me.groupBoxConnection.Controls.Add(Me.checkBoxWindowsAuth)
        Me.groupBoxConnection.Controls.Add(Me.textBoxPassword)
        Me.groupBoxConnection.Controls.Add(Me.labelPassword)
        Me.groupBoxConnection.Controls.Add(Me.textBoxUsername)
        Me.groupBoxConnection.Controls.Add(Me.labelUsername)
        Me.groupBoxConnection.Controls.Add(Me.textBoxDatabase)
        Me.groupBoxConnection.Controls.Add(Me.labelDatabase)
        Me.groupBoxConnection.Controls.Add(Me.textBoxPort)
        Me.groupBoxConnection.Controls.Add(Me.labelPort)
        Me.groupBoxConnection.Controls.Add(Me.textBoxServer)
        Me.groupBoxConnection.Controls.Add(Me.labelServer)
        Me.groupBoxConnection.Location = New System.Drawing.Point(20, 50)
        Me.groupBoxConnection.Name = "groupBoxConnection"
        Me.groupBoxConnection.Padding = New System.Windows.Forms.Padding(15)
        Me.groupBoxConnection.Size = New System.Drawing.Size(420, 280)
        Me.groupBoxConnection.TabIndex = 1
        Me.groupBoxConnection.TabStop = False
        Me.groupBoxConnection.Text = "Connection Details"
        '
        'buttonBrowse
        '
        Me.buttonBrowse.Location = New System.Drawing.Point(330, 120)
        Me.buttonBrowse.Name = "buttonBrowse"
        Me.buttonBrowse.Size = New System.Drawing.Size(75, 23)
        Me.buttonBrowse.TabIndex = 11
        Me.buttonBrowse.Text = "Browse..."
        Me.buttonBrowse.UseVisualStyleBackColor = True
        Me.buttonBrowse.Visible = False
        '
        'checkBoxWindowsAuth
        '
        Me.checkBoxWindowsAuth.AutoSize = True
        Me.checkBoxWindowsAuth.Location = New System.Drawing.Point(120, 150)
        Me.checkBoxWindowsAuth.Name = "checkBoxWindowsAuth"
        Me.checkBoxWindowsAuth.Size = New System.Drawing.Size(162, 17)
        Me.checkBoxWindowsAuth.TabIndex = 6
        Me.checkBoxWindowsAuth.Text = "Use Windows Authentication"
        Me.checkBoxWindowsAuth.UseVisualStyleBackColor = True
        '
        'textBoxPassword
        '
        Me.textBoxPassword.Location = New System.Drawing.Point(120, 230)
        Me.textBoxPassword.Name = "textBoxPassword"
        Me.textBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textBoxPassword.Size = New System.Drawing.Size(200, 20)
        Me.textBoxPassword.TabIndex = 9
        '
        'labelPassword
        '
        Me.labelPassword.AutoSize = True
        Me.labelPassword.Location = New System.Drawing.Point(15, 233)
        Me.labelPassword.Name = "labelPassword"
        Me.labelPassword.Size = New System.Drawing.Size(56, 13)
        Me.labelPassword.TabIndex = 8
        Me.labelPassword.Text = "Password:"
        '
        'textBoxUsername
        '
        Me.textBoxUsername.Location = New System.Drawing.Point(120, 200)
        Me.textBoxUsername.Name = "textBoxUsername"
        Me.textBoxUsername.Size = New System.Drawing.Size(200, 20)
        Me.textBoxUsername.TabIndex = 7
        '
        'labelUsername
        '
        Me.labelUsername.AutoSize = True
        Me.labelUsername.Location = New System.Drawing.Point(15, 203)
        Me.labelUsername.Name = "labelUsername"
        Me.labelUsername.Size = New System.Drawing.Size(58, 13)
        Me.labelUsername.TabIndex = 6
        Me.labelUsername.Text = "Username:"
        '
        'textBoxDatabase
        '
        Me.textBoxDatabase.Location = New System.Drawing.Point(120, 120)
        Me.textBoxDatabase.Name = "textBoxDatabase"
        Me.textBoxDatabase.Size = New System.Drawing.Size(200, 20)
        Me.textBoxDatabase.TabIndex = 5
        '
        'labelDatabase
        '
        Me.labelDatabase.AutoSize = True
        Me.labelDatabase.Location = New System.Drawing.Point(15, 123)
        Me.labelDatabase.Name = "labelDatabase"
        Me.labelDatabase.Size = New System.Drawing.Size(56, 13)
        Me.labelDatabase.TabIndex = 4
        Me.labelDatabase.Text = "Database:"
        '
        'textBoxPort
        '
        Me.textBoxPort.Location = New System.Drawing.Point(120, 80)
        Me.textBoxPort.Name = "textBoxPort"
        Me.textBoxPort.Size = New System.Drawing.Size(80, 20)
        Me.textBoxPort.TabIndex = 3
        '
        'labelPort
        '
        Me.labelPort.AutoSize = True
        Me.labelPort.Location = New System.Drawing.Point(15, 83)
        Me.labelPort.Name = "labelPort"
        Me.labelPort.Size = New System.Drawing.Size(29, 13)
        Me.labelPort.TabIndex = 2
        Me.labelPort.Text = "Port:"
        '
        'textBoxServer
        '
        Me.textBoxServer.Location = New System.Drawing.Point(120, 40)
        Me.textBoxServer.Name = "textBoxServer"
        Me.textBoxServer.Size = New System.Drawing.Size(200, 20)
        Me.textBoxServer.TabIndex = 1
        '
        'labelServer
        '
        Me.labelServer.AutoSize = True
        Me.labelServer.Location = New System.Drawing.Point(15, 43)
        Me.labelServer.Name = "labelServer"
        Me.labelServer.Size = New System.Drawing.Size(41, 13)
        Me.labelServer.TabIndex = 0
        Me.labelServer.Text = "Server:"
        '
        'buttonTest
        '
        Me.buttonTest.Location = New System.Drawing.Point(200, 350)
        Me.buttonTest.Name = "buttonTest"
        Me.buttonTest.Size = New System.Drawing.Size(100, 30)
        Me.buttonTest.TabIndex = 2
        Me.buttonTest.Text = "Test Connection"
        Me.buttonTest.UseVisualStyleBackColor = True
        '
        'buttonConnect
        '
        Me.buttonConnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.buttonConnect.ForeColor = System.Drawing.Color.White
        Me.buttonConnect.Location = New System.Drawing.Point(320, 350)
        Me.buttonConnect.Name = "buttonConnect"
        Me.buttonConnect.Size = New System.Drawing.Size(100, 30)
        Me.buttonConnect.TabIndex = 3
        Me.buttonConnect.Text = "Connect"
        Me.buttonConnect.UseVisualStyleBackColor = False
        '
        'buttonCancel
        '
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(80, 350)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(100, 30)
        Me.buttonCancel.TabIndex = 4
        Me.buttonCancel.Text = "Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'ConnectionForm
        '
        Me.AcceptButton = Me.buttonConnect
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(460, 400)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonConnect)
        Me.Controls.Add(Me.buttonTest)
        Me.Controls.Add(Me.groupBoxConnection)
        Me.Controls.Add(Me.labelTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConnectionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Database Connection"
        Me.groupBoxConnection.ResumeLayout(False)
        Me.groupBoxConnection.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelTitle As Label
    Friend WithEvents groupBoxConnection As GroupBox
    Friend WithEvents buttonBrowse As Button
    Friend WithEvents checkBoxWindowsAuth As CheckBox
    Friend WithEvents textBoxPassword As TextBox
    Friend WithEvents labelPassword As Label
    Friend WithEvents textBoxUsername As TextBox
    Friend WithEvents labelUsername As Label
    Friend WithEvents textBoxDatabase As TextBox
    Friend WithEvents labelDatabase As Label
    Friend WithEvents textBoxPort As TextBox
    Friend WithEvents labelPort As Label
    Friend WithEvents textBoxServer As TextBox
    Friend WithEvents labelServer As Label
    Friend WithEvents buttonTest As Button
    Friend WithEvents buttonConnect As Button
    Friend WithEvents buttonCancel As Button
End Class
