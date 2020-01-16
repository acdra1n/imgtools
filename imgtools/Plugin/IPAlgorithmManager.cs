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

        public static int LoadAlgorithmsFromFile(string binaryPath, bool silent = false)
        {
            int count = 0;
            try
            {
                foreach (var type in Assembly.LoadFile(binaryPath).GetTypes())
                {
                    if ((!type.IsClass) || type.IsNotPublic) continue;
                    if (typeof(ProcessingAlgorithm).IsAssignableFrom(type))
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
                            if (!silent) ImgTools.Error("Error: failed to instantiate algorithm '{0}' in file '{1}'!", type.Name, binaryPath);
                            continue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (!silent) ImgTools.Error("Error: cannot load algorithms from file '{0}'!", binaryPath);
            }
            return count;
        }

        public static void LoadAllAlgorithms(string binsPath, bool silent = false)
        {
            foreach(var algorithmFPath in Directory.GetFiles(binsPath, "*.dll", SearchOption.AllDirectories))
            {
                if(!silent) Console.WriteLine("Loading algorithms from binary '{0}'...", algorithmFPath);
                int c = LoadAlgorithmsFromFile(algorithmFPath, silent);
                if (!silent) Console.WriteLine("Successfully loaded {0} algorithms.", c);
            }
        }
    }
}
