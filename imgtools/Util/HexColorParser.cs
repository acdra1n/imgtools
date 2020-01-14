using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Util
{
    public class HexColorParser
    {
        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public HexColorParser(string hex)
        {
            string formattedHexString = hex.Trim().Replace("#", "");
            if (formattedHexString.Length < 6)
            {
                throw new FormatException("Hex string is not in the correct format");
            }

            A = 255;
            R = int.Parse(formattedHexString.Substring(0, 2), NumberStyles.HexNumber);
            G = int.Parse(formattedHexString.Substring(2, 2), NumberStyles.HexNumber);
            B = int.Parse(formattedHexString.Substring(4, 2), NumberStyles.HexNumber);   

            if(formattedHexString.Length == 8)
                A = int.Parse(formattedHexString.Substring(6, 2), NumberStyles.HexNumber);
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}
