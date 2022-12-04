namespace financial_management_service.Core.Exceptions
{
    public class ApiExceptionResDto
    {
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public ApiExceptionResDto(string code, string messages)
        {
            this.ErrorCode = code;
            this.ErrorMessage = messages;
        }
    }
}



