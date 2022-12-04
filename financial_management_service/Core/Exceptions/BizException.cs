using System.Runtime.Serialization;

namespace financial_management_service.Core.Exceptions
{
    [Serializable]
    public class BizException : Exception
    {
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public BizException(string code, string messages)
        {
            this.ErrorCode = code;
            this.ErrorMessage = messages;
        }

        protected BizException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = String.Empty;
            ErrorMessage = String.Empty;
        }
    }

}





