using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core
{
    public struct Size
    {
        public Size(int w, int h)
        {
            W = w;
            H = h;
        }

        public int H { get; set; }
        public int W { get; set; }
    }
}
