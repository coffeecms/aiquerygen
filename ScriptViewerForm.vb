Public Class ScriptViewerForm
    Private scriptContent As String
    
    Public Sub New(script As String, title As String)
        InitializeComponent()
        scriptContent = script
        Me.Text = title
    End Sub
    
    Private Sub ScriptViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtScript.Text = scriptContent
        txtScript.SelectionStart = 0
        txtScript.SelectionLength = 0
    End Sub
    
    Private Sub btnCopyToClipboard_Click(sender As Object, e As EventArgs) Handles btnCopyToClipboard.Click
        Try
            Clipboard.SetText(txtScript.Text)
            lblStatus.Text = "Script copied to clipboard!"
            lblStatus.ForeColor = Color.Green
        Catch ex As Exception
            lblStatus.Text = $"Error copying to clipboard: {ex.Message}"
            lblStatus.ForeColor = Color.Red
        End Try
    End Sub
    
    Private Sub btnSaveToFile_Click(sender As Object, e As EventArgs) Handles btnSaveToFile.Click
        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            saveDialog.DefaultExt = "sql"
            saveDialog.FileName = "index_script.sql"
            
            If saveDialog.ShowDialog() = DialogResult.OK Then
                Try
                    IO.File.WriteAllText(saveDialog.FileName, txtScript.Text)
                    lblStatus.Text = $"Script saved to: {saveDialog.FileName}"
                    lblStatus.ForeColor = Color.Green
                Catch ex As Exception
                    lblStatus.Text = $"Error saving file: {ex.Message}"
                    lblStatus.ForeColor = Color.Red
                End Try
            End If
        End Using
    End Sub
    
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
