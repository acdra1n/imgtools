using imgtools.Properties;
using imgtools.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using imgtools.IPC;
using imgtools.Scripting;
using imgtools.Plugin;

namespace imgtools
{
    class ImgTools
    {
        public static void Error(string text, params object[] o)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text, o);
            Console.ResetColor();
        }

        static bool CheckCmdLine(string[] args)
        {
            if (args.Length < 2)
            {
                Error("Error: no file specified.");
                return false;
            }

            if (!File.Exists(args[1]))
            {
                Error("Error: cannot find file '{0}'.", args[1]);
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ImageTools v1.0. (C) vreman.me 2020. All rights reserved.\n");
            Console.ResetColor();

            if(args.Length < 1)
            {
                Console.WriteLine(Resources.help);
                return;
            }

            Bitmap bmp;

            switch(args[0])
            {
                case "grayscale":
                    if (!CheckCmdLine(args)) return;
                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    bmp.Grayscale();
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "brightness":
                    if (!CheckCmdLine(args)) return;
                    if(args.Length < 3)
                    {
                        Error("Error: no brightness value specified.");
                        return;
                    }

                    int brightness = 0;
                    if(!int.TryParse(args[2].Replace("%", "").Trim(), out brightness))
                    {
                        Error("Error: no valid brightness value was specified.");
                        return;
                    }

                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    bmp.Brightness(brightness);
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "resize":
                    if (!CheckCmdLine(args)) return;
                    if(args.Length < 4)
                    {
                        Error("Error: no valid dimensions specified.");
                        break;
                    }

                    int height = 0;
                    int width = 0;

                    if((!int.TryParse(args[2], out width)) || (width < 1))
                    {
                        Error("Error: invalid value specified for dimension 'width'.");
                        return;
                    }

                    if ((!int.TryParse(args[3], out height)) || (height < 1))
                    {
                        Error("Error: invalid value specified for dimension 'height'.");
                        return;
                    }
                    
                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]), new Size(width, height));
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "invert":
                    if (!CheckCmdLine(args)) return;
                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    bmp.InvertColors();
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "parsehex":
                    if (args.Length < 2)
                    {
                        Error("Error: no color specified.");
                        return;
                    }
                    try
                    {
                        HexColorParser hcp = new HexColorParser(args[1]);
                        var c = hcp.ToColor();
                        Console.WriteLine("[A: {0}, R: {1}, G: {2}, B: {3}]", c.A, c.R, c.G, c.B);
                    }
                    catch (Exception)
                    {
                        Error("Cannot parse hex string.");
                    }
                    break;
                case "ls-algorithms":
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\algorithms"))
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\algorithms");
                    IPAlgorithmManager.LoadAllAlgorithms(Environment.CurrentDirectory + "\\algorithms");
                    Console.WriteLine("Installed Algorithms:");
                    if (IPAlgorithmManager.algorithms.Count < 1)
                        Console.WriteLine("  (none found)");
                    else
                    {
                        foreach (var algorithm in IPAlgorithmManager.algorithms)
                        {
                            Console.WriteLine("  {0}", algorithm.Value.GetName());
                        }
                    }
                    break;
                case "build-algorithm":
                    if (!CheckCmdLine(args)) return;
                    Console.WriteLine("Compiling...");
                    IMTAlgorithmCompiler.CompileAlgorithm(args[1]);
                    Console.WriteLine("Compilation finished.");
                    break;
                case "run-algorithm":
                    if (!CheckCmdLine(args)) return;
                    string algorithmName = args[2];
                    IPAlgorithmManager.LoadAllAlgorithms(Environment.CurrentDirectory + "\\algorithms");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    if(!bmp.ExecuteAlgorithm(algorithmName, args)) return;
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "removealpha":
                    if (!CheckCmdLine(args)) return;
                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    bmp.RemoveAlpha();
                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "replacecolor":
                    if (!CheckCmdLine(args)) return;

                    Console.WriteLine("Transforming image...");
                    sw.Start();
                    bmp = new Bitmap(Image.FromFile(args[1]));

                    Color a;
                    Color b;
                    bool ignoreAlpha = false;

                    try
                    {
                        a = new HexColorParser(args[2]).ToColor();
                        b = new HexColorParser(args[3]).ToColor();
                    }
                    catch (Exception)
                    {
                        Error("Cannot parse hex string.");
                        return;
                    }

                    if ((args.Length > 4) && (args[4] == "--ignoreAlpha"))
                        ignoreAlpha = true;

                    bmp.ReplaceColor(a, b, ignoreAlpha);

                    bmp.Save("output.png");
                    Console.WriteLine("Operation completed in {0}ms", sw.ElapsedMilliseconds);
                    sw.Stop();
                    break;
                case "ipc":
                    IPCServer ipcs = new IPCServer();
                    ipcs.StartThreadedProc();
                    break;
                case "runscript":
                    if(args.Length < 2)
                    {
                        Error("No script file specified.");
                        return;
                    }
                    else if(!File.Exists(args[1]))
                    {
                        Error("Cannot locate file.");
                        return;
                    }
                    IMTParser ip = new IMTParser(args[1]);
                    
                    break;
                case "info":
                    if (!CheckCmdLine(args)) return;
                    bmp = new Bitmap(Image.FromFile(args[1]));
                    Console.WriteLine(@"Image information for [{0}]
Dimensions (w*h): {1}x{2} pixels
Pixel Format: {3}", args[1], bmp.Width, bmp.Height, bmp.PixelFormat);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command specified.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
