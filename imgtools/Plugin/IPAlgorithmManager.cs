using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Plugin
{
    public static class IPAlgorithmManager
    {
        public static Dictionary<string, ProcessingAlgorithm> algorithms = new Dictionary<string, ProcessingAlgorithm>();

        public static int LoadAlgorithmsFromFile(string binaryPath)
        {
            int count = 0;
            try
            {
                foreach (var type in Assembly.LoadFile(binaryPath).GetTypes())
                {
                    if ((!type.IsClass) || type.IsNotPublic) continue;
                    if (type.IsAssignableFrom(typeof(ProcessingAlgorithm)))
                    {
                        try
                        {
                            var pa = Activator.CreateInstance(type) as ProcessingAlgorithm;
                            if (!algorithms.ContainsKey(pa.GetName()))
                            {
                                algorithms[pa.GetName()] = pa;
                                count++;
                            }
                        }
                        catch (Exception)
                        {
                            ImgTools.Error("Error: failed to instantiate algorithm '{0}' in file '{1}'!", type.Name, binaryPath);
                            continue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ImgTools.Error("Error: cannot load algorithms from file '{0}'!", binaryPath);
            }
            return count;
        }

        public static void LoadAllAlgorithms(string binsPath)
        {
            foreach(var algorithmFPath in Directory.GetFiles(binsPath))
            {
                Console.WriteLine("Loading algorithms from binary '{0}'...", algorithmFPath);
                int c = LoadAlgorithmsFromFile(algorithmFPath);
                Console.WriteLine("Successfully loaded '{0}' algorithms.", c);
            }
        }
    }
}
