using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Plugin
{
    public abstract class ProcessingAlgorithm
    {
        public abstract bool Execute(Bitmap image, params object[] args);
        public abstract string GetName();
    }
}
