using financial_management_service.Core.Entities;

namespace financial_management_service.Core.Dtos
{
    public class UserDto
    {
    }

    public class UserReqDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public UserReqDto()
        {
            Email = String.Empty;
            Password = String.Empty;
        }
    }
}
