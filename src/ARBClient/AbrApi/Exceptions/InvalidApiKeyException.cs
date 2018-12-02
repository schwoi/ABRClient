using System;

namespace AbrApi.Exceptions
{
    [Serializable()]
    public class InvalidApiKeyException: System.Exception
    {
        public InvalidApiKeyException() : base() { }
        public InvalidApiKeyException(string message) : base(message) { }
        public InvalidApiKeyException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidApiKeyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    
        
    }
}

