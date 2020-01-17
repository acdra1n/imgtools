using imgtools.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Image i = new Image(args[0]);
            for(int y = 0; y < i.Height; y++)
            {
                for(int x = 0; x < i.Width; x++)
                {
                    Color c = i.GetPixel(x, y);
                    int avg = (c.R + c.G + c.B) / 3;
                    c.R = avg;
                    c.G = avg;
                    c.B = avg;
                    i.SetPixel(x, y, c);
                }
            }
            i.Save("Test.png");
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            sw.Stop();
        }
    }
}
