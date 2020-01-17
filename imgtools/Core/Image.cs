using imgtools.Core;
using imgtools.Core.ImageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Core
{
    public class Image : IDisposable
    {
        private ImageHandler handler;
        private MemoryStream imageStream;
        
        public int Height { get { return handler.GetTotalSize().H; } }
        public int Width { get { return handler.GetTotalSize().W; } }

        /// <summary>
        /// Creates a new image, auto detecting the handler.
        /// </summary>
        /// <param name="path">The path of the image to load.</param>
        public Image(string path)
        {
            string fileExtension = Path.GetExtension(path); //TODO use image magic to find type
            switch(fileExtension)
            {
                default:
                    handler = new FallbackImageHandler();
                    break;
                case ".png":
                    handler = new PngImageHandler();
                    handler.Init(imageStream = new MemoryStream(File.ReadAllBytes(path), true));
                    break;
            }
        }

        public void SetPixel(int x, int y, Color c)
        {
            handler.SetPixel(x, y, c);
        }

        public Color GetPixel(int x, int y)
        {
            return handler.GetPixel(x, y);
        }

        public void Dispose()
        {
            try
            {
                imageStream.Dispose();
            }
            catch (Exception) { }
        }

        public void Save(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            MemoryStream newStream = handler.Flush();
            newStream.WriteTo(fs);
            newStream.Close();
            fs.Flush();
            fs.Close();
        }
    }
}
