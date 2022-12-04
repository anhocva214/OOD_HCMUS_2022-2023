namespace financial_management_service.Core.Dtos
{
    public class LoginDto
    {
    }

    public class LoginReqDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public LoginReqDto()
        {
            Email = String.Empty;
            Password = String.Empty;
        }
    }
}
