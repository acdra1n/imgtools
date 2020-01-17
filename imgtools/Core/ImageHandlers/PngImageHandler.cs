using imgtools.Core.Formats.PNG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core.ImageHandlers
{
    public class PngImageHandler : ImageHandler
    {
        private Png readOnlyInstance;
        private PngBuilder writableInstance;
        private Stream imageStream;

        public override MemoryStream Flush()
        {
            imageStream.Close();
            return new MemoryStream(writableInstance.Save());   
        }

        public override Color GetPixel(int x, int y)
        {
            var p = readOnlyInstance.GetPixel(x, y);
            return new Color()
            {
                A = p.A,
                R = p.R,
                G = p.G,
                B = p.B
            };
        }

        public override Size GetTotalSize()
        {
            return new Size(readOnlyInstance.Width, readOnlyInstance.Height);
        }

        public override void Init(MemoryStream imageStream)
        {
            this.imageStream = imageStream;
            readOnlyInstance = Png.Open(this.imageStream);
            writableInstance = PngBuilder.Create(readOnlyInstance.Width, readOnlyInstance.Height, readOnlyInstance.HasAlphaChannel);
            for (var y = 0; y < readOnlyInstance.Height; y++)
                for (var x = 0; x < readOnlyInstance.Width; x++)
                    writableInstance.SetPixel(readOnlyInstance.GetPixel(x, y), x, y);
        }

        public override void SetPixel(int x, int y, Color color)
        {
            writableInstance.SetPixel(new Pixel((byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A, false), x, y);
        }
    }
}
