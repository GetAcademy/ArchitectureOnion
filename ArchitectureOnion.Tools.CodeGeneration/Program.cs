using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ArchitectureOnion.Tools.CodeGeneration
{
    class Program
    {
        private const string QueriesDir = @"..\..\..\Queries";
        const string OutputDir = @"..\..\..\ArchitectureOnion.DataAccess\GeneratedQueries";
        const string ConnStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ArchitectureDemo;Integrated Security=True;";

        static void Main(string[] args)
        {
            var directories = Directory.GetDirectories(QueriesDir);
            foreach (var directory in directories)
            {
                GenerateRepositoryClass(directory);
            }
        }

        private static void GenerateRepositoryClass(string directory)
        {
            var className = GetClassName(directory);
            var repositoryCode = new StringBuilder();
            var typesCode = new StringBuilder();
            repositoryCode.AppendLine("public partial class " + className + " { ");
            var files = Directory.GetFileSystemEntries(directory);
            foreach (var file in files)
            {
                GenerateQueryMethod(repositoryCode, typesCode, file);
            }
            repositoryCode.AppendLine("}");
        }

        private static void GenerateQueryMethod(StringBuilder repositoryCode, 
            StringBuilder typesCode, string file)
        {
            var content = File.ReadAllText(file);
            var serializer = new XmlSerializer(typeof(Query));
            var query = (Query)serializer.Deserialize(new StringReader(content));
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query.SQL;
            foreach (var p in query.Parameters)
            {
                command.Parameters.AddWithValue(p.Name, DefaultValueFromType(p.CSType));
            }
            var reader = command.ExecuteReader();
            var schemaTable = reader.GetColumnSchema();
        }

        private static object DefaultValueFromType(string csType)
        {
            if (csType == "int") return 1;
            return null;
        }

        private static string GetClassName(string directory)
        {
            var lastBackslashIndex = directory.LastIndexOf("\\", StringComparison.Ordinal);
            return directory.Substring(lastBackslashIndex + 1);
        }
    }
}
