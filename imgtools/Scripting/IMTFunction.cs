using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Scripting
{
    public class IMTFunction
    {
        public string Name { get; set; }
        public Dictionary<string, IMTPrimitiveDataType> Arguments { get; set; }
        public string Body { get; set; }
        public bool IsLinked { get; set; } = false;
        public IMTLinkedFunctionCallback OnCalled { get; set; } = null;

        public IMTFunction(string name, Dictionary<string, IMTPrimitiveDataType> arguments = null)
        {
            if (arguments == null)
                Arguments = new Dictionary<string, IMTPrimitiveDataType>();
            if (name.Trim() == "") throw new IMTScriptingException("Invalid function name");
            Name = name;
            Body = "";
        }

        public void SetBody(string text)
        {
            Body = text;
        }
    }
}
