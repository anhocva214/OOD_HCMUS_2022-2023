using System.Runtime.Serialization;

namespace financial_management_service.Core.Exceptions
{
    [Serializable]
    public class PermissionException : Exception
    {
        public PermissionException() { }

        protected PermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}



