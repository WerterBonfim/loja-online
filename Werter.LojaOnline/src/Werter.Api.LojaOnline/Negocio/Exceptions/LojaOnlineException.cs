using System.Runtime.Serialization;

namespace Werter.Api.LojaOnline.Negocio.Exceptions
{
    [Serializable]
    public class LojaOnlineException : Exception
    {
        public LojaOnlineException() {}

        public LojaOnlineException(string? message) : base(message) { }

        public LojaOnlineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LojaOnlineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class InfraestruturaException : Exception
    {
        public InfraestruturaException()
        {
        }

        public InfraestruturaException(string? message) : base(message)
        {
        }

        public InfraestruturaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InfraestruturaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
