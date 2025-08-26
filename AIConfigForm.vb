Imports System.ComponentModel
Imports System.IO
Imports Newtonsoft.Json

Public Class AIConfigForm
    Private aiConfig As AIConfiguration
    
    Public ReadOnly Property Configuration As AIConfiguration
        Get
            Return aiConfig
        End Get
    End Property

    Public Sub New()
        InitializeComponent()
        LoadConfiguration()
    End Sub

    Private Sub LoadConfiguration()
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            System.Diagnostics.Debug.WriteLine($"📂 Loading configuration from: {configPath}")

            If File.Exists(configPath) Then
                Dim json As String = File.ReadAllText(configPath)
                System.Diagnostics.Debug.WriteLine($"📄 Config file content: {json}")

                If Not String.IsNullOrWhiteSpace(json) Then
                    aiConfig = JsonConvert.DeserializeObject(Of AIConfiguration)(json)

                    ' Validate loaded config
                    If aiConfig Is Nothing Then
                        System.Diagnostics.Debug.WriteLine($"⚠️ Deserialization returned null, creating new config")
                        aiConfig = New AIConfiguration()
                    Else
                        System.Diagnostics.Debug.WriteLine($"✅ Configuration loaded successfully")
                        System.Diagnostics.Debug.WriteLine($"  Model: '{aiConfig.ModelName}'")
                        System.Diagnostics.Debug.WriteLine($"  Endpoint: '{aiConfig.CustomEndpoint}'")
                        System.Diagnostics.Debug.WriteLine($"  API Key: {If(String.IsNullOrEmpty(aiConfig.ApiKey), "(empty)", $"(present, {aiConfig.ApiKey.Length} chars)")}")
                    End If
                Else
                    System.Diagnostics.Debug.WriteLine($"⚠️ Config file is empty, using defaults")
                    aiConfig = New AIConfiguration()
                End If
            Else
                System.Diagnostics.Debug.WriteLine($"⚠️ Config file not found, creating new configuration")
                aiConfig = New AIConfiguration()
            End If

            PopulateFormFromConfig()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error loading configuration: {ex.Message}")
            MessageBox.Show($"Error loading configuration: {ex.Message}{Environment.NewLine}Using default settings.", "Configuration Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            aiConfig = New AIConfiguration()
            PopulateFormFromConfig()
        End Try
    End Sub

    Private Sub PopulateFormFromConfig()
        Try
            If aiConfig Is Nothing Then
                System.Diagnostics.Debug.WriteLine("⚠️ aiConfig is null, creating new instance")
                aiConfig = New AIConfiguration()
            End If

            System.Diagnostics.Debug.WriteLine($"📝 Populating form with config:")
            System.Diagnostics.Debug.WriteLine($"  Model: '{aiConfig.ModelName}'")
            System.Diagnostics.Debug.WriteLine($"  Endpoint: '{aiConfig.CustomEndpoint}'")

            ' Set configuration values with null checks
            txtApiKey.Text = If(aiConfig.ApiKey, "")
            txtModel.Text = If(aiConfig.ModelName, "")
            txtCustomEndpoint.Text = If(aiConfig.CustomEndpoint, "")

            ' Handle numeric values with bounds checking
            numericTimeout.Value = Math.Max(Math.Min(aiConfig.TimeoutSeconds, CInt(numericTimeout.Maximum)), CInt(numericTimeout.Minimum))
            numericMaxTokens.Value = Math.Max(Math.Min(aiConfig.MaxTokens, CInt(numericMaxTokens.Maximum)), CInt(numericMaxTokens.Minimum))
            numericTemperature.Value = Math.Max(Math.Min(CDec(aiConfig.Temperature), numericTemperature.Maximum), numericTemperature.Minimum)

            chkStreamingResponse.Checked = aiConfig.EnableStreaming

            System.Diagnostics.Debug.WriteLine("✅ Form populated successfully")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error populating form: {ex.Message}")
            MessageBox.Show($"Error populating form: {ex.Message}", "Form Population Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Auto-save when text fields change
    Private Sub txtApiKey_TextChanged(sender As Object, e As EventArgs) Handles txtApiKey.TextChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub txtModel_TextChanged(sender As Object, e As EventArgs) Handles txtModel.TextChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub txtCustomEndpoint_TextChanged(sender As Object, e As EventArgs) Handles txtCustomEndpoint.TextChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    ' Auto-save when numeric values change
    Private Sub numericTimeout_ValueChanged(sender As Object, e As EventArgs) Handles numericTimeout.ValueChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub numericMaxTokens_ValueChanged(sender As Object, e As EventArgs) Handles numericMaxTokens.ValueChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub numericTemperature_ValueChanged(sender As Object, e As EventArgs) Handles numericTemperature.ValueChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub chkStreamingResponse_CheckedChanged(sender As Object, e As EventArgs) Handles chkStreamingResponse.CheckedChanged
        If Me.Visible Then SaveFormToConfig()
    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        Try
            btnTestConnection.Enabled = False
            lblTestResult.Text = "Testing connection..."
            lblTestResult.ForeColor = Color.Orange
            Application.DoEvents()

            ' Save current form data to config
            SaveFormToConfig()

            ' Log configuration for debugging
            System.Diagnostics.Debug.WriteLine($"🔍 Testing connection with config:")
            System.Diagnostics.Debug.WriteLine($"  Endpoint: {aiConfig.CustomEndpoint}")
            System.Diagnostics.Debug.WriteLine($"  Model: {aiConfig.ModelName}")
            System.Diagnostics.Debug.WriteLine($"  API Key: {If(String.IsNullOrEmpty(aiConfig.ApiKey), "(empty)", "(present)")}")

            ' Test the connection
            Dim aiClient As New AIClient(aiConfig)
            Dim testResult As Boolean = aiClient.TestConnection()

            If testResult Then
                lblTestResult.Text = "✓ Connection successful!"
                lblTestResult.ForeColor = Color.Green
                System.Diagnostics.Debug.WriteLine("✅ Connection test passed")
            Else
                lblTestResult.Text = "✗ Connection failed!"
                lblTestResult.ForeColor = Color.Red
                System.Diagnostics.Debug.WriteLine("❌ Connection test failed")
            End If

        Catch ex As Exception
            lblTestResult.Text = $"✗ Error: {ex.Message}"
            lblTestResult.ForeColor = Color.Red
            System.Diagnostics.Debug.WriteLine($"❌ Connection test error: {ex.Message}")
        Finally
            btnTestConnection.Enabled = True
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            System.Diagnostics.Debug.WriteLine("💾 Save button clicked - saving configuration...")

            ' Update config from form data
            UpdateConfigFromForm()

            ' Save to file
            SaveConfiguration()

            ' Verify the save by reloading and checking
            VerifyConfigurationSaved()

            ' Reload configuration to ensure form shows exactly what was saved
            ReloadConfigurationFromFile()

            System.Diagnostics.Debug.WriteLine("✅ Configuration saved and verified successfully")

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error in btnSave_Click: {ex.Message}")
            MessageBox.Show($"Error saving configuration: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to reload configuration from file (used after save to verify)
    Private Sub ReloadConfigurationFromFile()
        Try
            System.Diagnostics.Debug.WriteLine("🔄 Reloading configuration from file to verify display...")

            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            If File.Exists(configPath) Then
                Dim json As String = File.ReadAllText(configPath)
                Dim reloadedConfig As AIConfiguration = JsonConvert.DeserializeObject(Of AIConfiguration)(json)

                ' Replace current config with reloaded one
                aiConfig = reloadedConfig

                ' Repopulate form with reloaded data
                PopulateFormFromConfig()

                System.Diagnostics.Debug.WriteLine("✅ Configuration reloaded and form updated")
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"⚠️ Error reloading configuration: {ex.Message}")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Method to update configuration from form data (without auto-save)
    Private Sub UpdateConfigFromForm()
        System.Diagnostics.Debug.WriteLine("📝 Updating config from form data...")

        ' Save settings with validation
        aiConfig.ApiKey = txtApiKey.Text.Trim()
        aiConfig.ModelName = txtModel.Text.Trim()
        aiConfig.CustomEndpoint = txtCustomEndpoint.Text.Trim()

        aiConfig.TimeoutSeconds = CInt(numericTimeout.Value)
        aiConfig.MaxTokens = CInt(numericMaxTokens.Value)
        aiConfig.Temperature = CDbl(numericTemperature.Value)
        aiConfig.EnableStreaming = chkStreamingResponse.Checked

        System.Diagnostics.Debug.WriteLine($"✅ Config updated:")
        System.Diagnostics.Debug.WriteLine($"  Endpoint: {aiConfig.CustomEndpoint}")
        System.Diagnostics.Debug.WriteLine($"  Model: {aiConfig.ModelName}")
        System.Diagnostics.Debug.WriteLine($"  API Key: {If(String.IsNullOrEmpty(aiConfig.ApiKey), "(empty)", $"({aiConfig.ApiKey.Length} chars)")}")
    End Sub    ' Method to verify configuration was saved correctly
    Private Sub VerifyConfigurationSaved()
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            If File.Exists(configPath) Then
                Dim json As String = File.ReadAllText(configPath)
                Dim loadedConfig As AIConfiguration = JsonConvert.DeserializeObject(Of AIConfiguration)(json)

                System.Diagnostics.Debug.WriteLine("🔍 Verifying saved configuration:")
                System.Diagnostics.Debug.WriteLine($"  Endpoint: '{loadedConfig.CustomEndpoint}' | Expected: '{aiConfig.CustomEndpoint}'")
                System.Diagnostics.Debug.WriteLine($"  Model: '{loadedConfig.ModelName}' | Expected: '{aiConfig.ModelName}'")
                System.Diagnostics.Debug.WriteLine($"  API Key: {If(String.IsNullOrEmpty(loadedConfig.ApiKey), "(empty)", $"({loadedConfig.ApiKey.Length} chars)")} | Expected: {If(String.IsNullOrEmpty(aiConfig.ApiKey), "(empty)", $"({aiConfig.ApiKey.Length} chars)")}")

                ' Check each important field
                Dim errors As New List(Of String)

                If loadedConfig.CustomEndpoint <> aiConfig.CustomEndpoint Then
                    errors.Add($"Endpoint mismatch: saved='{loadedConfig.CustomEndpoint}', expected='{aiConfig.CustomEndpoint}'")
                End If

                If loadedConfig.ModelName <> aiConfig.ModelName Then
                    errors.Add($"Model mismatch: saved='{loadedConfig.ModelName}', expected='{aiConfig.ModelName}'")
                End If

                If errors.Count > 0 Then
                    Dim errorMessage As String = String.Join(Environment.NewLine, errors)
                    System.Diagnostics.Debug.WriteLine($"❌ Configuration verification failed:")
                    For Each Err As String In errors
                        System.Diagnostics.Debug.WriteLine($"  • {Err}")
                    Next
                    Throw New Exception($"Configuration verification failed: {errorMessage}")
                End If

                System.Diagnostics.Debug.WriteLine("✅ Configuration verification passed")
            Else
                Throw New Exception("Configuration file was not created")
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Configuration verification failed: {ex.Message}")
            Throw
        End Try
    End Sub



    Private Sub SaveFormToConfig()
        Try
            ' Update config from form data
            UpdateConfigFromForm()

            ' Auto-save configuration immediately when any setting changes
            SaveConfiguration()

            System.Diagnostics.Debug.WriteLine($"📝 Auto-save completed: {aiConfig.CustomEndpoint}")
        Catch ex As Exception
            ' Log error but don't interrupt the user
            System.Diagnostics.Debug.WriteLine($"❌ Auto-save error: {ex.Message}")
            Console.WriteLine($"Auto-save error: {ex.Message}")
        End Try
    End Sub

    Private Sub SaveConfiguration()
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "ai_config.json")
            System.Diagnostics.Debug.WriteLine($"💾 Saving configuration to: {configPath}")

            ' Ensure directory exists
            Dim configDir As String = Path.GetDirectoryName(configPath)
            If Not Directory.Exists(configDir) Then
                Directory.CreateDirectory(configDir)
            End If

            ' Serialize with indented formatting for readability
            Dim json As String = JsonConvert.SerializeObject(aiConfig, Formatting.Indented)
            System.Diagnostics.Debug.WriteLine($"📄 Saving config JSON: {json}")

            ' Write to file with UTF-8 encoding
            File.WriteAllText(configPath, json, System.Text.Encoding.UTF8)

            System.Diagnostics.Debug.WriteLine("✅ Configuration saved successfully")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error saving configuration: {ex.Message}")
            Throw New Exception($"Failed to save configuration: {ex.Message}")
        End Try
    End Sub

    Private Sub linkLabelOpenAIHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            Process.Start("https://platform.openai.com/api-keys")
        Catch
            ' Ignore errors when opening browser
        End Try
    End Sub

    Private Sub linkLabelOpenRouterHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            Process.Start("https://openrouter.ai/keys")
        Catch
            ' Ignore errors when opening browser
        End Try
    End Sub

    Private Sub linkLabelGeminiHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            Process.Start("https://aistudio.google.com/app/apikey")
        Catch
            ' Ignore errors when opening browser
        End Try
    End Sub

    Private Sub AIConfigForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

' Configuration classes
Public Class AIConfiguration
    Public Property ApiKey As String = ""
    Public Property ModelName As String = "gpt-3.5-turbo"
    Public Property CustomEndpoint As String = "https://api.openai.com/v1"
    Public Property TimeoutSeconds As Integer = 60
    Public Property MaxTokens As Integer = 2048
    Public Property Temperature As Double = 0.7
    Public Property EnableStreaming As Boolean = False
End Class
