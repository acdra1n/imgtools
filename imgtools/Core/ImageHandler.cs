using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core
{
    public abstract class ImageHandler
    {
        public abstract void Init(MemoryStream imageStream);
        public abstract Color GetPixel(int x, int y);
        public abstract void SetPixel(int x, int y, Color color);
        public abstract Size GetTotalSize();
        public abstract MemoryStream Flush();
    }
}
