# AI QueryGen 🤖💾

**The Ultimate AI-Powered SQL Query Generator Desktop Application**

Transform your natural language questions into precise SQL queries instantly! AI QueryGen is a powerful Windows desktop application that revolutionizes database interaction by combining artificial intelligence with comprehensive database management tools.

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET-5.0-purple.svg)
![AI Powered](https://img.shields.io/badge/AI-Powered-orange.svg)

## 🌟 Why AI QueryGen is Essential for You

### 🚀 **Save Hours of Development Time**
- **No more syntax struggles**: Convert plain English to SQL in seconds
- **Eliminate learning curves**: Non-technical users can query databases instantly
- **Reduce development overhead**: Focus on business logic, not SQL syntax

### 🎯 **Universal Database Support**
Works seamlessly with all major database systems:
- **SQL Server** - Enterprise-grade Microsoft databases
- **MySQL** - Popular open-source database
- **PostgreSQL** - Advanced open-source database
- **SQLite** - Lightweight embedded databases

### 🧠 **AI-Powered Intelligence**
- **Multiple AI Providers**: OpenAI, Google Gemini, Ollama, and custom endpoints
- **Context-Aware**: Understands your database schema for accurate queries
- **Multi-Language Support**: English and Vietnamese natural language processing

### 📊 **Professional Database Management**
- **Advanced Schema Analysis**: Deep database structure inspection
- **Metadata Management**: Comprehensive table and column documentation
- **Index Management**: Optimize database performance
- **Connection Management**: Save and reuse database connections

## 🔥 Key Features

### 🎤 **Natural Language to SQL Conversion**
```
You say: "Find all customers from New York who placed orders last month"
AI QueryGen generates:
SELECT c.customer_name, c.city, o.order_date, o.total_amount
FROM customers c
INNER JOIN orders o ON c.customer_id = o.customer_id
WHERE c.city = 'New York' 
    AND o.order_date >= DATEADD(month, -1, GETDATE())
ORDER BY o.order_date DESC;
```

### 🔧 **Advanced Database Analysis**
- **Automatic Schema Detection**: Discovers tables, columns, relationships, and constraints
- **Incremental Analysis**: Detects database changes since last analysis
- **Foreign Key Mapping**: Automatically identifies table relationships
- **Data Type Recognition**: Understands column types and constraints

### 🎨 **Intelligent Query Generation**
- **Context-Aware AI**: Leverages your database schema for precise queries
- **Multi-Provider Support**: Choose from OpenAI, Gemini, Ollama, or custom AI endpoints
- **Syntax Optimization**: Generates database-specific SQL syntax
- **Error Handling**: Provides fallback mechanisms for reliable operation

### 💼 **Enterprise-Ready Features**
- **Connection History**: Save and manage multiple database connections
- **Metadata Caching**: Fast query generation with cached schema information
- **Query Execution**: Test generated queries directly in the application
- **Export Capabilities**: Save queries as SQL files or copy to clipboard

### 🌐 **Multi-Language Support**
- **English**: Full natural language processing
- **Vietnamese**: Native Vietnamese language support
- **Extensible**: Architecture supports additional languages

## 🏗️ **Perfect For These Use Cases**

### 👨‍💼 **Business Analysts**
- Generate reports without SQL knowledge
- Explore data using natural language
- Create ad-hoc queries for business insights

### 👩‍💻 **Developers**
- Rapid prototyping of complex queries
- Learning SQL syntax for different databases
- Accelerating development workflows

### 🎓 **Students & Educators**
- Learning database concepts interactively
- Understanding SQL through natural language
- Academic research and projects

### 🏢 **Database Administrators**
- Quick schema exploration
- Documentation generation
- Performance analysis and optimization

## 🛠️ **System Requirements**

- **Operating System**: Windows 10/11
- **.NET Framework**: .NET 5.0 or higher
- **Memory**: 4GB RAM minimum (8GB recommended)
- **Storage**: 100MB free disk space
- **Network**: Internet connection for AI features

## 🚀 **Quick Start**

### 1. **Download & Install**
```bash
# Download the latest release
git clone https://github.com/coffeecms/aiquerygen.git
cd aiquerygen
```

### 2. **Configure AI Provider**
- Launch AI QueryGen
- Navigate to **Tools** → **AI Configuration**
- Choose your preferred AI provider:
  - **OpenAI**: Enter API key and model
  - **Google Gemini**: Configure endpoint and credentials
  - **Ollama**: Set local server endpoint
  - **Custom**: Configure any OpenAI-compatible API

### 3. **Connect to Database**
- Click **Connect to Database**
- Select your database type
- Enter connection details
- Test connection and save for future use

### 4. **Start Querying**
- Type your question in natural language
- Click **Generate SQL**
- Review and execute the generated query
- View results in the integrated data grid

## 📖 **Usage Examples**

### **Basic Data Retrieval**
```
Input: "Show me all products with price greater than 100"
Output: SELECT * FROM products WHERE price > 100;
```

### **Complex Joins**
```
Input: "Find customers who haven't placed any orders"
Output: SELECT c.* FROM customers c 
        LEFT JOIN orders o ON c.customer_id = o.customer_id 
        WHERE o.customer_id IS NULL;
```

### **Aggregation Queries**
```
Input: "What's the total sales by category this year?"
Output: SELECT c.category_name, SUM(oi.quantity * oi.unit_price) as total_sales
        FROM categories c
        JOIN products p ON c.category_id = p.category_id
        JOIN order_items oi ON p.product_id = oi.product_id
        JOIN orders o ON oi.order_id = o.order_id
        WHERE YEAR(o.order_date) = YEAR(GETDATE())
        GROUP BY c.category_name;
```

## 🔧 **Advanced Configuration**

### **AI Provider Settings**
- **Temperature**: Control AI creativity (0.0 - 1.0)
- **Max Tokens**: Limit response length
- **Timeout**: Set request timeout duration
- **Custom Endpoints**: Use your own AI infrastructure

### **Database Optimization**
- **Index Analysis**: Identify missing indexes
- **Performance Monitoring**: Track query execution times
- **Schema Validation**: Ensure data integrity

## 🤝 **Contributing**

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

### **Development Setup**
1. Clone the repository
2. Open in Visual Studio 2019/2022
3. Restore NuGet packages
4. Build and run

### **Areas for Contribution**
- Additional AI provider integrations
- Enhanced natural language processing
- New database system support
- UI/UX improvements
- Documentation and tutorials

## 📄 **License**

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 **Support**

- **Documentation**: [Wiki](https://github.com/coffeecms/aiquerygen/wiki)
- **Issues**: [GitHub Issues](https://github.com/coffeecms/aiquerygen/issues)
- **Discussions**: [GitHub Discussions](https://github.com/coffeecms/aiquerygen/discussions)

## 🎯 **Roadmap**

- [ ] **Cloud Integration**: Azure, AWS, GCP database support
- [ ] **API Version**: REST API for programmatic access
- [ ] **Web Interface**: Browser-based version
- [ ] **Mobile App**: iOS and Android applications
- [ ] **Advanced Analytics**: Machine learning insights
- [ ] **Team Collaboration**: Shared query libraries

## ⭐ **Why Choose AI QueryGen?**

### 🚀 **Productivity Boost**
- **10x faster** query development
- **Zero learning curve** for non-technical users
- **Consistent results** across different databases

### 💰 **Cost Effective**
- **Reduce training costs** for SQL syntax
- **Minimize development time** on database queries
- **Eliminate consultant fees** for simple reports

### 🛡️ **Reliable & Secure**
- **Local processing** option with Ollama
- **No data transmission** in offline mode
- **Enterprise security** compliance ready

### 🌍 **Future-Proof**
- **Regular updates** with latest AI models
- **Extensible architecture** for new features
- **Community-driven** development

---

**Ready to revolutionize your database workflows?** 

[⬇️ Download AI QueryGen](https://github.com/coffeecms/aiquerygen/releases) | [📚 View Documentation](https://github.com/coffeecms/aiquerygen/wiki) | [💬 Join Community](https://github.com/coffeecms/aiquerygen/discussions)

---

*Built with ❤️ for developers, analysts, and data enthusiasts worldwide*
