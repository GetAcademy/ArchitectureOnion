using System;
using System.IO;
using System.Text;

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
            var code = new StringBuilder();
            code.AppendLine("public partial class " + className + " { ");
            var files = Directory.GetFileSystemEntries(directory);
            foreach (var file in files)
            {
                GenerateQueryMethod(code, file);
            }
            code.AppendLine("}");
        }

        private static void GenerateQueryMethod(StringBuilder code, string file)
        {
            var content = File.ReadAllText(file);
            //code.AppendLine(readAllText);
        }

        private static string GetClassName(string directory)
        {
            var lastBackslashIndex = directory.LastIndexOf("\\", StringComparison.Ordinal);
            return directory.Substring(lastBackslashIndex + 1);
        }
    }
}
