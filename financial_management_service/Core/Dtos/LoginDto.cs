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

    public class LoginResDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int? PhoneNumber { get; set; }

    }
}
