using System;
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

            Console.WriteLine(source);
        }
    }
}
