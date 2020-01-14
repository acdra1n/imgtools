using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Scripting
{
    public class IMTVariable
    {
        public string Name { get; set; }
        public IMTPrimitiveDataType DataType { get; set; }
        public object Value { get; set; }
    }
}
