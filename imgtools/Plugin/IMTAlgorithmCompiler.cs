using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Plugin
{
    public class IMTAlgorithmCompiler
    {
        public static void CompileAlgorithm(string sourcePath)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\algorithms"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\algorithms");

            string[] source = File.ReadAllLines(sourcePath);
            string algorithmName = Path.GetFileName(sourcePath);
            float algorithmVersion = 1.0f;

            // Preprocess source
            int i = 0;
            foreach (var line in source)
            {
                string fmtLine = line.Trim();
                if (fmtLine.StartsWith("#define"))
                {
                    string defName = fmtLine.Split(' ')[1];
                    if (defName == "ALGORITHM_NAME")
                        algorithmName = fmtLine.Split('"')[1].Trim();
                    else if (defName == "ALGORITHM_VER")
                        algorithmVersion = float.Parse(fmtLine.Split(' ')[2].Trim());
                    source[i] = "";
                }
                i++;
            }

            Dictionary<string, string> varTable = new Dictionary<string, string>()
            {
                { "NAME", algorithmName },
                { "CODE", JoinStrArray(source) },
                { "VERSION", algorithmVersion.ToString() + "f" }
            };

            string combinedSource = Properties.Resources.imtalgorithm_template.ToString();
            foreach (var kvp in varTable)
                combinedSource = combinedSource.Replace("%" + kvp.Key + "%", kvp.Value);

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.OutputAssembly = Environment.CurrentDirectory + "\\algorithms\\" + algorithmName + ".dll";
            parameters.ReferencedAssemblies.Add(Environment.CurrentDirectory + "\\imgtools.exe"); //TODO find $0
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            var res = codeProvider.CompileAssemblyFromSource(parameters, combinedSource);
            if (res.Errors.Count > 0)
                foreach (var e in res.Errors)
                    Console.WriteLine(e);
        }

        private static string JoinStrArray(string[] source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var l in source)
                sb.AppendLine(l);
            return sb.ToString();
        }
    }
}
