using System;
using System.Runtime.Serialization;

namespace TurtleZilla
{
    [Serializable]
    internal class ParameterParseException : Exception
    {
        private const string DEFAULT_MESSAGE = "An error occurred parsing parameters.";

        public ParameterParseException() 
            : this(null) {}

        public ParameterParseException(string message) 
            : base(message ?? DEFAULT_MESSAGE, null) {}

        public ParameterParseException(string message, Exception innerException) 
            : base(message ?? DEFAULT_MESSAGE, innerException) {}

        protected ParameterParseException(SerializationInfo info, StreamingContext context) 
            : base(info, context) {}
    }
}
