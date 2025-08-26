Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class AIClient
    Private ReadOnly config As AIConfiguration
    Private ReadOnly httpClient As HttpClient
    
    Public Sub New(configuration As AIConfiguration)
        If configuration Is Nothing Then
            Throw New ArgumentNullException(NameOf(configuration), "Configuration cannot be null")
        End If

        config = configuration
        httpClient = New HttpClient()

        ' Set timeout to 60 seconds like in Go server
        httpClient.Timeout = TimeSpan.FromSeconds(If(config.TimeoutSeconds > 0, config.TimeoutSeconds, 60))

        ' Set up headers based on provider
        SetupHeaders()
    End Sub

    Private Sub SetupHeaders()
        If httpClient Is Nothing Then
            Throw New InvalidOperationException("HttpClient is not initialized")
        End If

        httpClient.DefaultRequestHeaders.Clear()

        ' Add common headers like in Go server
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json")
        httpClient.DefaultRequestHeaders.Add("User-Agent", "AI-QueryGen/1.0")

        ' Add Authorization header if API key is provided
        If Not String.IsNullOrEmpty(config.ApiKey) Then
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.ApiKey}")
        End If
    End Sub

    Public Function TestConnection() As Boolean
        Try
            System.Diagnostics.Debug.WriteLine($"🔍 Testing connection to API endpoint: {config.CustomEndpoint}")

            ' Use a generic test that works with most OpenAI-compatible APIs
            Dim result As Boolean = TestGenericAPI()

            If result Then
                System.Diagnostics.Debug.WriteLine("✅ Connection test successful")
            Else
                System.Diagnostics.Debug.WriteLine("❌ Connection test failed")
            End If

            Return result
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Connection test error: {ex.Message}")
            Return False
        End Try
    End Function

    Private Function TestGenericAPI() As Boolean
        Try
            ' Try to access /models endpoint (most common)
            Dim url As String = $"{config.CustomEndpoint.TrimEnd("/"c)}/models"
            System.Diagnostics.Debug.WriteLine($"📡 Testing API endpoint: {url}")
            
            Dim response As HttpResponseMessage = httpClient.GetAsync(url).Result
            
            If response.IsSuccessStatusCode Then
                System.Diagnostics.Debug.WriteLine($"✅ API test successful: {response.StatusCode}")
                Return True
            Else
                System.Diagnostics.Debug.WriteLine($"⚠️ API test failed: {response.StatusCode}")
                Return False
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ API test error: {ex.Message}")
            Return False
        End Try
    End Function

    Public Async Function GenerateSQLAsync(naturalLanguageQuery As String, databaseSchema As String) As Task(Of String)
        Try
            ' Validate input parameters
            ValidateRequest(naturalLanguageQuery, databaseSchema)

            System.Diagnostics.Debug.WriteLine($"🤖 Starting SQL generation with endpoint: {config.CustomEndpoint}")

            Dim prompt As String = BuildPrompt(naturalLanguageQuery, databaseSchema)

            ' Use OpenAI-compatible format for all APIs
            Return Await GenerateWithOpenAIFormat(prompt)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ SQL generation failed: {ex.Message}")
            Throw New Exception($"Failed to generate SQL: {ex.Message}", ex)
        End Try
    End Function

    ' Method to auto-select model based on content (similar to Go server)
    Private Function SelectModelForContent(prompt As String) As String
        ' This can be extended to detect if the prompt contains image references
        ' For now, it returns the configured model
        System.Diagnostics.Debug.WriteLine($"📝 Using text model: {config.ModelName}")
        Return config.ModelName
    End Function

    ' Method to validate request parameters before sending
    Private Sub ValidateRequest(naturalLanguageQuery As String, databaseSchema As String)
        If String.IsNullOrWhiteSpace(naturalLanguageQuery) Then
            Throw New ArgumentException("Natural language query cannot be empty", NameOf(naturalLanguageQuery))
        End If

        If String.IsNullOrWhiteSpace(databaseSchema) Then
            Throw New ArgumentException("Database schema cannot be empty", NameOf(databaseSchema))
        End If
    End Sub

    Private Function BuildPrompt(naturalLanguageQuery As String, databaseSchema As String) As String
        Dim prompt As New StringBuilder()

        prompt.AppendLine("You are an expert SQL query generator. Your task is to convert natural language questions into accurate SQL queries.")
        prompt.AppendLine()
        prompt.AppendLine("CRITICAL: Pay close attention to the database type specified in the schema and use the EXACT syntax for that database type.")
        prompt.AppendLine()
        prompt.AppendLine("Database Schema:")
        prompt.AppendLine(databaseSchema)
        prompt.AppendLine()
        prompt.AppendLine("Rules:")
        prompt.AppendLine("1. Generate only valid SQL queries for the specific database type mentioned in the schema")
        prompt.AppendLine("2. Use proper table and column names from the schema")
        prompt.AppendLine("3. Include appropriate JOINs when multiple tables are needed")
        prompt.AppendLine("4. Add WHERE clauses for filtering conditions")
        prompt.AppendLine("5. Use aggregate functions (COUNT, SUM, AVG, etc.) when appropriate")
        prompt.AppendLine("6. Follow the syntax rules specified for the database type in the schema")
        prompt.AppendLine("7. Return only the SQL query without explanations")
        prompt.AppendLine()
        prompt.AppendLine($"Natural Language Query: {naturalLanguageQuery}")
        prompt.AppendLine()
        prompt.AppendLine("SQL Query:")

        Return prompt.ToString()
    End Function

    Private Function HandleHttpError(response As HttpResponseMessage, responseContent As String) As Exception
        ' Handle HTTP errors with detailed logging like Go server
        System.Diagnostics.Debug.WriteLine($"❌ AI API returned status: {response.StatusCode}")
        System.Diagnostics.Debug.WriteLine($"Response body: {responseContent}")

        ' Try to parse API error response
        Try
            If Not String.IsNullOrEmpty(responseContent) Then
                Dim errorObj As JObject = JObject.Parse(responseContent)
                If errorObj("error") IsNot Nothing Then
                    Dim errorMessage As String = errorObj("error")("message")?.ToString()
                    Dim errorType As String = errorObj("error")("type")?.ToString()
                    Dim errorCode As String = errorObj("error")("code")?.ToString()

                    If Not String.IsNullOrEmpty(errorMessage) Then
                        Return New Exception($"AI API error ({response.StatusCode}): {errorMessage}")
                    End If
                End If
            End If
        Catch ex As JsonException
            ' If can't parse as JSON, use raw response
        End Try

        Return New Exception($"AI API returned status {response.StatusCode}: {responseContent}")
    End Function

    Private Async Function GenerateWithOpenAIFormat(prompt As String) As Task(Of String)
        If String.IsNullOrEmpty(config.CustomEndpoint) Then
            Throw New ArgumentException("Custom endpoint is required for OpenAI format API")
        End If

        If String.IsNullOrEmpty(config.ModelName) Then
            Throw New ArgumentException("Model name is required for OpenAI format API")
        End If

        Dim url As String = $"{config.CustomEndpoint}/chat/completions"

        ' Set default values similar to Go server
        Dim maxTokens As Integer = If(config.MaxTokens > 0, config.MaxTokens, 2048)
        Dim temperature As Double = If(config.Temperature > 0, config.Temperature, 0.7)

        Dim requestBody As New With {
            .model = config.ModelName,
            .messages = New Object() {
                New With {
                    .role = "user",
                    .content = prompt
                }
            },
            .max_tokens = maxTokens,
            .temperature = temperature,
            .stream = config.EnableStreaming
        }

        Dim json As String = JsonConvert.SerializeObject(requestBody)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        ' Log request details (similar to Go server)
        System.Diagnostics.Debug.WriteLine($"📤 Forwarding request to AI API: {url}")
        System.Diagnostics.Debug.WriteLine($"Model: {config.ModelName}, MaxTokens: {maxTokens}, Temperature: {temperature}")

        Dim response As HttpResponseMessage = Await httpClient.PostAsync(url, content)

        Dim responseContent As String = Await response.Content.ReadAsStringAsync()

        ' Check for HTTP errors with detailed logging like Go server
        If Not response.IsSuccessStatusCode Then
            Throw HandleHttpError(response, responseContent)
        End If

        ' Add null checks and better error handling
        If String.IsNullOrEmpty(responseContent) Then
            Throw New Exception("Received empty response from AI provider")
        End If

        Dim responseObj As JObject = JObject.Parse(responseContent)

        ' Check if the response object is valid
        If responseObj Is Nothing Then
            Throw New Exception("Failed to parse response from AI provider")
        End If

        ' Check if choices array exists and has elements
        If responseObj("choices") Is Nothing OrElse Not responseObj("choices").HasValues Then
            Throw New Exception("No choices found in AI provider response")
        End If

        Dim choicesArray As JArray = CType(responseObj("choices"), JArray)
        If choicesArray.Count = 0 Then
            Throw New Exception("Empty choices array in AI provider response")
        End If

        ' Check if the first choice has message and content
        Dim firstChoice As JToken = choicesArray(0)
        If firstChoice("message") Is Nothing Then
            Throw New Exception("No message found in AI provider response")
        End If

        If firstChoice("message")("content") Is Nothing Then
            Throw New Exception("No content found in AI provider response message")
        End If

        Dim sqlQuery As String = firstChoice("message")("content").ToString().Trim()

        If String.IsNullOrEmpty(sqlQuery) Then
            Throw New Exception("AI provider returned empty SQL query")
        End If

        System.Diagnostics.Debug.WriteLine("✅ Successfully received response from AI API")

        Return CleanSQLResponse(sqlQuery)
    End Function

    Private Async Function GenerateWithGemini(prompt As String) As Task(Of String)
        If String.IsNullOrEmpty(config.CustomEndpoint) Then
            Throw New ArgumentException("Custom endpoint is required for Gemini API")
        End If

        If String.IsNullOrEmpty(config.ModelName) Then
            Throw New ArgumentException("Model name is required for Gemini API")
        End If

        If String.IsNullOrEmpty(config.ApiKey) Then
            Throw New ArgumentException("API key is required for Gemini API")
        End If

        Dim url As String = $"{config.CustomEndpoint}/models/{config.ModelName}:generateContent?key={config.ApiKey}"

        ' Set default values similar to Go server
        Dim maxTokens As Integer = If(config.MaxTokens > 0, config.MaxTokens, 2048)
        Dim temperature As Double = If(config.Temperature > 0, config.Temperature, 0.7)

        Dim requestBody As New With {
            .contents = New Object() {
                New With {
                    .parts = New Object() {
                        New With {
                            .text = prompt
                        }
                    }
                }
            },
            .generationConfig = New With {
                .maxOutputTokens = maxTokens,
                .temperature = temperature
            }
        }

        Dim json As String = JsonConvert.SerializeObject(requestBody)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        System.Diagnostics.Debug.WriteLine($"📤 Sending request to Gemini API: {url}")

        Dim response As HttpResponseMessage = Await httpClient.PostAsync(url, content)
        Dim responseContent As String = Await response.Content.ReadAsStringAsync()

        ' Check for HTTP errors
        If Not response.IsSuccessStatusCode Then
            Throw HandleHttpError(response, responseContent)
        End If

        ' Add null checks and better error handling
        If String.IsNullOrEmpty(responseContent) Then
            Throw New Exception("Received empty response from Gemini API")
        End If

        Dim responseObj As JObject = JObject.Parse(responseContent)

        ' Check if the response object is valid
        If responseObj Is Nothing Then
            Throw New Exception("Failed to parse response from Gemini API")
        End If

        ' Check if candidates array exists and has elements
        If responseObj("candidates") Is Nothing OrElse Not responseObj("candidates").HasValues Then
            Throw New Exception("No candidates found in Gemini API response")
        End If

        Dim candidatesArray As JArray = CType(responseObj("candidates"), JArray)
        If candidatesArray.Count = 0 Then
            Throw New Exception("Empty candidates array in Gemini API response")
        End If

        ' Check if the first candidate has content and parts
        Dim firstCandidate As JToken = candidatesArray(0)
        If firstCandidate("content") Is Nothing Then
            Throw New Exception("No content found in Gemini API response")
        End If

        If firstCandidate("content")("parts") Is Nothing OrElse Not firstCandidate("content")("parts").HasValues Then
            Throw New Exception("No parts found in Gemini API response content")
        End If

        Dim partsArray As JArray = CType(firstCandidate("content")("parts"), JArray)
        If partsArray.Count = 0 Then
            Throw New Exception("Empty parts array in Gemini API response")
        End If

        If partsArray(0)("text") Is Nothing Then
            Throw New Exception("No text found in Gemini API response parts")
        End If

        Dim sqlQuery As String = partsArray(0)("text").ToString().Trim()

        If String.IsNullOrEmpty(sqlQuery) Then
            Throw New Exception("Gemini API returned empty SQL query")
        End If

        System.Diagnostics.Debug.WriteLine("✅ Successfully received response from Gemini API")

        Return CleanSQLResponse(sqlQuery)
    End Function

    Private Async Function GenerateWithOllama(prompt As String) As Task(Of String)
        If String.IsNullOrEmpty(config.CustomEndpoint) Then
            Throw New ArgumentException("Custom endpoint is required for Ollama API")
        End If

        If String.IsNullOrEmpty(config.ModelName) Then
            Throw New ArgumentException("Model name is required for Ollama API")
        End If

        Dim url As String = $"{config.CustomEndpoint}/generate"

        ' Set default values similar to Go server
        Dim maxTokens As Integer = If(config.MaxTokens > 0, config.MaxTokens, 2048)
        Dim temperature As Double = If(config.Temperature > 0, config.Temperature, 0.7)

        Dim requestBody As New With {
            .model = config.ModelName,
            .prompt = prompt,
            .stream = False,
            .options = New With {
                .temperature = temperature,
                .num_predict = maxTokens
            }
        }

        Dim json As String = JsonConvert.SerializeObject(requestBody)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        System.Diagnostics.Debug.WriteLine($"📤 Sending request to Ollama API: {url}")

        Dim response As HttpResponseMessage = Await httpClient.PostAsync(url, content)
        Dim responseContent As String = Await response.Content.ReadAsStringAsync()

        ' Check for HTTP errors
        If Not response.IsSuccessStatusCode Then
            Throw HandleHttpError(response, responseContent)
        End If

        ' Add null checks and better error handling
        If String.IsNullOrEmpty(responseContent) Then
            Throw New Exception("Received empty response from Ollama API")
        End If

        Dim responseObj As JObject = JObject.Parse(responseContent)

        ' Check if the response object is valid
        If responseObj Is Nothing Then
            Throw New Exception("Failed to parse response from Ollama API")
        End If

        ' Check if response field exists
        If responseObj("response") Is Nothing Then
            Throw New Exception("No response field found in Ollama API response")
        End If

        Dim sqlQuery As String = responseObj("response").ToString().Trim()

        If String.IsNullOrEmpty(sqlQuery) Then
            Throw New Exception("Ollama API returned empty SQL query")
        End If

        System.Diagnostics.Debug.WriteLine("✅ Successfully received response from Ollama API")

        Return CleanSQLResponse(sqlQuery)
    End Function
    
    Private Function CleanSQLResponse(sqlResponse As String) As String
        ' Remove thinking tags and content (some AI models include this)
        sqlResponse = RemoveThinkingTags(sqlResponse)
        
        ' Remove common markdown formatting
        sqlResponse = sqlResponse.Replace("```sql", "").Replace("```", "")
        
        ' Remove common prefixes
        sqlResponse = sqlResponse.Replace("SQL Query:", "").Replace("Query:", "")
        
        ' Clean up whitespace
        sqlResponse = sqlResponse.Trim()
        
        ' Ensure the query ends with semicolon if it doesn't already
        If Not sqlResponse.EndsWith(";") Then
            sqlResponse += ";"
        End If
        
        Return sqlResponse
    End Function
    
    Private Function RemoveThinkingTags(text As String) As String
        ' Remove <think>...</think> blocks (including nested tags)
        Dim result As String = text
        Dim thinkStart As Integer
        Dim thinkEnd As Integer
        
        Do
            thinkStart = result.IndexOf("<think>", StringComparison.OrdinalIgnoreCase)
            If thinkStart = -1 Then Exit Do
            
            thinkEnd = result.IndexOf("</think>", thinkStart, StringComparison.OrdinalIgnoreCase)
            If thinkEnd = -1 Then Exit Do
            
            ' Remove the entire thinking block
            result = result.Remove(thinkStart, thinkEnd - thinkStart + 8)
        Loop
        
        ' Also remove any remaining thinking-related patterns
        result = System.Text.RegularExpressions.Regex.Replace(result, 
            "<think[^>]*>.*?</think>", 
            "", 
            System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Singleline)
        
        Return result.Trim()
    End Function
    
    Public Sub Dispose()
        httpClient?.Dispose()
    End Sub
End Class
