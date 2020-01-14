using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Scripting
{
    public class IMTParser
    {
        public List<IMTFunction> RegisteredFunctions = new List<IMTFunction>();

        public IMTParser()
        {

        }

        public void RegisterBuiltInFunctions()
        {
            RegisteredFunctions.Add(new IMTFunction("print", new Dictionary<string, IMTPrimitiveDataType>()
            {
                { "text", IMTPrimitiveDataType.String }
            })
            {
                IsLinked = true,
                OnCalled = new IMTLinkedFunctionCallback((arguments) =>
                {
                    if (!arguments.ContainsKey("name")) return;
                    Console.WriteLine(arguments["name"]);
                })
            });
        }
    }
}
