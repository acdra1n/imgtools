using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Plugin
{
    public static class Stdlib
    {
        public static void Print(string text, params object[] args)
        {
            Console.WriteLine(text, args);
        }
    }
}
