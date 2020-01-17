using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core
{
    public struct Color
    {
        public Color(int r, int g, int b, int a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public override string ToString()
        {
            return string.Format("A = {0}, R = {1}, G = {2}, B = {3}", A, R, G, B);
        }
    }
}
