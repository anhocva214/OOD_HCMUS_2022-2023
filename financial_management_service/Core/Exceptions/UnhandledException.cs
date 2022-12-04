using System.Runtime.Serialization;

namespace financial_management_service.Core.Exceptions
{
    [Serializable]
    public class UnhandledException : Exception
    {
        public string ErrorMessage { get; set; }

        public UnhandledException() => ErrorMessage = string.Empty;

        public UnhandledException(string mes) => ErrorMessage = mes;

        protected UnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorMessage = string.Empty;
        }
    }
}



