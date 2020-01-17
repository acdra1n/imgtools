using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core.ImageHandlers
{
    public class FallbackImageHandler : ImageHandler
    {
        public override MemoryStream Flush()
        {
            throw new NotImplementedException();
        }

        public override Color GetPixel(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override Size GetTotalSize()
        {
            throw new NotImplementedException();
        }

        public override void Init(MemoryStream imageStream)
        {
            throw new NotImplementedException();
        }

        public override void SetPixel(int x, int y, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
