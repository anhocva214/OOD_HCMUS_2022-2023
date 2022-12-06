namespace financial_management_service.Core.Dtos
{
    public class ForgotPasswordDto
    {
    }

    public class ForgotPasswordReqDto
    {
        public string? Email { get; set; }

        public ForgotPasswordReqDto()
        {
            Email = String.Empty;
        }
    }
}
