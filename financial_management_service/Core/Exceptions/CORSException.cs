
using System.Runtime.Serialization;

namespace financial_management_service.Core.Exceptions
{
    [Serializable]
    public class CorsException : Exception
    {
        public string ErrorMessage { get; set; }

        public CorsException(string origin) => ErrorMessage = "Cross-Origin Request Blocked: The Same Origin Policy disallows reading the remote resource at " + origin;

        protected CorsException(SerializationInfo info, StreamingContext context) : base(info, context) => ErrorMessage = string.Empty;
    }
}



