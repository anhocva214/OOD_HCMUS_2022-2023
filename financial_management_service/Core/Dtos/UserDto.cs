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
        public string? FullName { get; set; }

        public UserReqDto()
        {
            Email = String.Empty;
            Password = String.Empty;
            FullName = String.Empty;
        }
    }

    public class UpdateUserReqDto
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? PasswordComfirm { get; set; }

        public UpdateUserReqDto()
        {
            Id = String.Empty;
            FullName = String.Empty;
            Password = String.Empty;
            Birthday = null;
            PhoneNumber = null;
            Password = String.Empty;
            PasswordComfirm = String.Empty;
        }
    }
}
