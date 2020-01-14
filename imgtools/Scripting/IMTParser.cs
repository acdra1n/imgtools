using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgtools.Scripting
{
    public class IMTParser
    {
        public List<IMTFunction> RegisteredFunctions = new List<IMTFunction>();
        public List<IMTVariable> RegisteredConstants = new List<IMTVariable>();
        public List<IMTVariable> RegisteredVariables = new List<IMTVariable>();

        public IMTParser(string path)
        {
            if (path == null) return;
            using(StreamReader sr = new StreamReader(path))
            {
                var script = sr.ReadToEnd();
                Parse(script);
            }
        }

        public void Parse(string code, object context = null)
        {
            IMTToken currentTok = IMTToken.Ident;
            bool begunLocatingIdent = true;
            string currentIdent = "";

            for(var i = 0; i < code.Length; i++)
            {
                if ((currentTok != IMTToken.String) && code[i] == '#')
                    if (code.IndexOf('\n', i) != -1)
                        i = code.IndexOf('\n', i);
                if(currentTok == IMTToken.Ident)
                {
                    if ((code[i] == ' ') || (code[i] == '\r') || (code[i] == '\n') && (!begunLocatingIdent)) continue;
                    
                    if(!begunLocatingIdent)
                    {
                        begunLocatingIdent = true;
                        currentIdent += code[i];
                        continue;
                    }
                    else
                    {
                        if((code[i] == ' ') || (code[i] == '\r') || (code[i] == '\n')) // Ident extracted.
                        {
                            begunLocatingIdent = false;
                            // Check if ident is keyword. If not, check if it is a variable or function name.
                            if(currentIdent[0] == '%') // This is a macro directive
                            {
                                var macroDirectiveName = currentIdent.Substring(1);
                                switch(macroDirectiveName)
                                {
                                    case "define":
                                        Console.WriteLine("Defining variable...");
                                        break;
                                }

                                i = code.IndexOf(';', i);
                            }
                            currentIdent = "";
                        }
                    }
                }
            }
        }
    }
}
