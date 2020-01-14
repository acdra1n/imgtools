using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Util
{
    public static class ImageAdjustments
    {
        public static void Grayscale(this Bitmap bmp)
        {
            for(var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);
                    if ((oc.R == 255) && (oc.G == 255) && (oc.B == 255)) continue;
                    int l = (oc.R + oc.G + oc.B) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(oc.A, l, l, l));
                }
            }
        }

        public static void Brightness(this Bitmap bmp, float percentage)
        {
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);

                    int newR = (int)(oc.R * (percentage / 100));
                    int newG = (int)(oc.G * (percentage / 100));
                    int newB = (int)(oc.B * (percentage / 100));

                    if (newR > 255) newR = 255;
                    if (newG > 255) newG = 255;
                    if (newB > 255) newB = 255;

                    bmp.SetPixel(x, y, Color.FromArgb(oc.A, newR, newG, newB));
                }
            }
        }

        public static void InvertColors(this Bitmap bmp)
        {
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);

                    int newR = 255 - oc.R;
                    int newG = 255 - oc.G;
                    int newB = 255 - oc.B;

                    bmp.SetPixel(x, y, Color.FromArgb(oc.A, newR, newG, newB));
                }
            }
        }

        public static void ReplaceColor(this Bitmap bmp, Color a, Color b, bool ignoreAlpha = false)
        {
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);
                    if((oc.R == a.R) && (oc.G == a.G) && (oc.B == a.B))
                    {
                        if ((!ignoreAlpha) && (oc.A != a.A)) continue;
                        bmp.SetPixel(x, y, b);
                    }
                }
            }
        }

        public static void RemoveAlpha(this Bitmap bmp)
        {
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, oc.R, oc.G, oc.B));
                }
            }
        }

        public static void AlphaBlend_Multiply(Color a, Color b)
        {

        }
    }
}
