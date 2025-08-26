<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AIConfigForm
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
        Me.groupBoxConfiguration = New System.Windows.Forms.GroupBox()
        Me.chkStreamingResponse = New System.Windows.Forms.CheckBox()
        Me.numericTemperature = New System.Windows.Forms.NumericUpDown()
        Me.lblTemperature = New System.Windows.Forms.Label()
        Me.numericMaxTokens = New System.Windows.Forms.NumericUpDown()
        Me.lblMaxTokens = New System.Windows.Forms.Label()
        Me.numericTimeout = New System.Windows.Forms.NumericUpDown()
        Me.lblTimeout = New System.Windows.Forms.Label()
        Me.txtCustomEndpoint = New System.Windows.Forms.TextBox()
        Me.lblCustomEndpoint = New System.Windows.Forms.Label()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.lblTestResult = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.groupBoxConfiguration.SuspendLayout()
        CType(Me.numericTemperature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericMaxTokens, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupBoxConfiguration
        '
        Me.groupBoxConfiguration.Controls.Add(Me.chkStreamingResponse)
        Me.groupBoxConfiguration.Controls.Add(Me.numericTemperature)
        Me.groupBoxConfiguration.Controls.Add(Me.lblTemperature)
        Me.groupBoxConfiguration.Controls.Add(Me.numericMaxTokens)
        Me.groupBoxConfiguration.Controls.Add(Me.lblMaxTokens)
        Me.groupBoxConfiguration.Controls.Add(Me.numericTimeout)
        Me.groupBoxConfiguration.Controls.Add(Me.lblTimeout)
        Me.groupBoxConfiguration.Controls.Add(Me.txtCustomEndpoint)
        Me.groupBoxConfiguration.Controls.Add(Me.lblCustomEndpoint)
        Me.groupBoxConfiguration.Controls.Add(Me.txtModel)
        Me.groupBoxConfiguration.Controls.Add(Me.lblModel)
        Me.groupBoxConfiguration.Controls.Add(Me.txtApiKey)
        Me.groupBoxConfiguration.Controls.Add(Me.lblApiKey)
        Me.groupBoxConfiguration.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.groupBoxConfiguration.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.groupBoxConfiguration.Location = New System.Drawing.Point(12, 52)
        Me.groupBoxConfiguration.Name = "groupBoxConfiguration"
        Me.groupBoxConfiguration.Size = New System.Drawing.Size(748, 200)
        Me.groupBoxConfiguration.TabIndex = 3
        Me.groupBoxConfiguration.TabStop = False
        Me.groupBoxConfiguration.Text = "🔧 Configuration Settings"
        '
        'chkStreamingResponse
        '
        Me.chkStreamingResponse.AutoSize = True
        Me.chkStreamingResponse.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.chkStreamingResponse.ForeColor = System.Drawing.Color.Black
        Me.chkStreamingResponse.Location = New System.Drawing.Point(580, 160)
        Me.chkStreamingResponse.Name = "chkStreamingResponse"
        Me.chkStreamingResponse.Size = New System.Drawing.Size(133, 19)
        Me.chkStreamingResponse.TabIndex = 12
        Me.chkStreamingResponse.Text = "Streaming Response"
        Me.chkStreamingResponse.UseVisualStyleBackColor = True
        '
        'numericTemperature
        '
        Me.numericTemperature.DecimalPlaces = 1
        Me.numericTemperature.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.numericTemperature.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.numericTemperature.Location = New System.Drawing.Point(580, 130)
        Me.numericTemperature.Maximum = New Decimal(New Integer() {20, 0, 0, 65536})
        Me.numericTemperature.Name = "numericTemperature"
        Me.numericTemperature.Size = New System.Drawing.Size(150, 23)
        Me.numericTemperature.TabIndex = 11
        Me.numericTemperature.Value = New Decimal(New Integer() {7, 0, 0, 65536})
        '
        'lblTemperature
        '
        Me.lblTemperature.AutoSize = True
        Me.lblTemperature.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTemperature.ForeColor = System.Drawing.Color.Black
        Me.lblTemperature.Location = New System.Drawing.Point(440, 132)
        Me.lblTemperature.Name = "lblTemperature"
        Me.lblTemperature.Size = New System.Drawing.Size(76, 15)
        Me.lblTemperature.TabIndex = 10
        Me.lblTemperature.Text = "Temperature:"
        '
        'numericMaxTokens
        '
        Me.numericMaxTokens.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.numericMaxTokens.Location = New System.Drawing.Point(580, 100)
        Me.numericMaxTokens.Maximum = New Decimal(New Integer() {8000, 0, 0, 0})
        Me.numericMaxTokens.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numericMaxTokens.Name = "numericMaxTokens"
        Me.numericMaxTokens.Size = New System.Drawing.Size(150, 23)
        Me.numericMaxTokens.TabIndex = 9
        Me.numericMaxTokens.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        'lblMaxTokens
        '
        Me.lblMaxTokens.AutoSize = True
        Me.lblMaxTokens.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblMaxTokens.ForeColor = System.Drawing.Color.Black
        Me.lblMaxTokens.Location = New System.Drawing.Point(440, 102)
        Me.lblMaxTokens.Name = "lblMaxTokens"
        Me.lblMaxTokens.Size = New System.Drawing.Size(72, 15)
        Me.lblMaxTokens.TabIndex = 8
        Me.lblMaxTokens.Text = "Max Tokens:"
        '
        'numericTimeout
        '
        Me.numericTimeout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.numericTimeout.Location = New System.Drawing.Point(580, 70)
        Me.numericTimeout.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numericTimeout.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numericTimeout.Name = "numericTimeout"
        Me.numericTimeout.Size = New System.Drawing.Size(150, 23)
        Me.numericTimeout.TabIndex = 7
        Me.numericTimeout.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'lblTimeout
        '
        Me.lblTimeout.AutoSize = True
        Me.lblTimeout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTimeout.ForeColor = System.Drawing.Color.Black
        Me.lblTimeout.Location = New System.Drawing.Point(440, 72)
        Me.lblTimeout.Name = "lblTimeout"
        Me.lblTimeout.Size = New System.Drawing.Size(87, 15)
        Me.lblTimeout.TabIndex = 6
        Me.lblTimeout.Text = "Timeout (secs):"
        '
        'txtCustomEndpoint
        '
        Me.txtCustomEndpoint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtCustomEndpoint.Location = New System.Drawing.Point(120, 100)
        Me.txtCustomEndpoint.Name = "txtCustomEndpoint"
        Me.txtCustomEndpoint.Size = New System.Drawing.Size(300, 23)
        Me.txtCustomEndpoint.TabIndex = 5
        '
        'lblCustomEndpoint
        '
        Me.lblCustomEndpoint.AutoSize = True
        Me.lblCustomEndpoint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCustomEndpoint.ForeColor = System.Drawing.Color.Black
        Me.lblCustomEndpoint.Location = New System.Drawing.Point(15, 103)
        Me.lblCustomEndpoint.Name = "lblCustomEndpoint"
        Me.lblCustomEndpoint.Size = New System.Drawing.Size(79, 15)
        Me.lblCustomEndpoint.TabIndex = 4
        Me.lblCustomEndpoint.Text = "API Endpoint:"
        '
        'txtModel
        '
        Me.txtModel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtModel.Location = New System.Drawing.Point(120, 70)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Size = New System.Drawing.Size(300, 23)
        Me.txtModel.TabIndex = 3
        '
        'lblModel
        '
        Me.lblModel.AutoSize = True
        Me.lblModel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblModel.ForeColor = System.Drawing.Color.Black
        Me.lblModel.Location = New System.Drawing.Point(15, 73)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(79, 15)
        Me.lblModel.TabIndex = 2
        Me.lblModel.Text = "Model Name:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtApiKey.Location = New System.Drawing.Point(120, 40)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtApiKey.Size = New System.Drawing.Size(300, 23)
        Me.txtApiKey.TabIndex = 1
        '
        'lblApiKey
        '
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblApiKey.ForeColor = System.Drawing.Color.Black
        Me.lblApiKey.Location = New System.Drawing.Point(15, 43)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(50, 15)
        Me.lblApiKey.TabIndex = 0
        Me.lblApiKey.Text = "API Key:"
        '
        'btnTestConnection
        '
        Me.btnTestConnection.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnTestConnection.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnTestConnection.Location = New System.Drawing.Point(12, 272)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(120, 35)
        Me.btnTestConnection.TabIndex = 4
        Me.btnTestConnection.Text = "🔍 Test Connection"
        Me.btnTestConnection.UseVisualStyleBackColor = False
        '
        'lblTestResult
        '
        Me.lblTestResult.AutoSize = True
        Me.lblTestResult.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTestResult.Location = New System.Drawing.Point(150, 282)
        Me.lblTestResult.Name = "lblTestResult"
        Me.lblTestResult.Size = New System.Drawing.Size(143, 15)
        Me.lblTestResult.TabIndex = 5
        Me.lblTestResult.Text = "Click test to verify setup"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.LightGreen
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnSave.Location = New System.Drawing.Point(580, 272)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 35)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "💾 Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LightCoral
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnCancel.Location = New System.Drawing.Point(680, 272)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 35)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "❌ Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.lblTitle.Location = New System.Drawing.Point(12, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(230, 21)
        Me.lblTitle.TabIndex = 8
        Me.lblTitle.Text = "🤖 AI Configuration Settings"
        '
        'AIConfigForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(780, 319)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblTestResult)
        Me.Controls.Add(Me.btnTestConnection)
        Me.Controls.Add(Me.groupBoxConfiguration)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AIConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AI Configuration - AI QueryGen"
        Me.groupBoxConfiguration.ResumeLayout(False)
        Me.groupBoxConfiguration.PerformLayout()
        CType(Me.numericTemperature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericMaxTokens, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents groupBoxConfiguration As GroupBox
    Friend WithEvents txtApiKey As TextBox
    Friend WithEvents lblApiKey As Label
    Friend WithEvents txtModel As TextBox
    Friend WithEvents lblModel As Label
    Friend WithEvents txtCustomEndpoint As TextBox
    Friend WithEvents lblCustomEndpoint As Label
    Friend WithEvents numericTimeout As NumericUpDown
    Friend WithEvents lblTimeout As Label
    Friend WithEvents numericMaxTokens As NumericUpDown
    Friend WithEvents lblMaxTokens As Label
    Friend WithEvents numericTemperature As NumericUpDown
    Friend WithEvents lblTemperature As Label
    Friend WithEvents chkStreamingResponse As CheckBox
    Friend WithEvents btnTestConnection As Button
    Friend WithEvents lblTestResult As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblTitle As Label
End Class
