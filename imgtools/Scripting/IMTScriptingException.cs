using System;
using System.Runtime.Serialization;

namespace imgtools.Scripting
{
    [Serializable]
    internal class IMTScriptingException : Exception
    {
        public IMTScriptingException()
        {

        }

        public IMTScriptingException(string message) : base(message)
        {

        }

        public IMTScriptingException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected IMTScriptingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}