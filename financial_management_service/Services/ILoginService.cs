using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Object;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface ILoginService
	{
		Task<LoginResDto> Execute(LoginReqDto dto);
	}

	public class LoginService : BaseService, ILoginService
	{
		private readonly IUnitOfWork _uok;

		public LoginService(IUnitOfWork uok) => _uok = uok;

		public LoginService Set(CurrentUserObj obj)
        {
            CurrentUser = obj;
            return this;
        }

		public async Task<LoginResDto> Execute(LoginReqDto dto) => await Validate(dto);

		private async Task<LoginResDto> Validate(LoginReqDto dto)
        {
			If.IsNull(dto.Email).ThrBiz(ErrorCode._400_01, "Email không được để trống.");

			If.IsNull(dto.Password).ThrBiz(ErrorCode._400_02, "Mật khẩu không được để trống.");

			HandleData(dto);

			var user = await _uok.Users.GetFirstOrDefaultAsync(x =>x.Email == dto.Email && x.Password == dto.Password);

			If.IsTrue(user == null).ThrBiz(ErrorCode._400_03, "Tài khoản hoặc mật khẩu không đúng.");

			return new LoginResDto()
			{
				Id = user.Id,
				Email = user.Email,
				Gender = user.Gender,
				Birthday = user.Birthday,
				PhoneNumber = user.PhoneNumber
			};
		}

		private static void HandleData(LoginReqDto dto)
		{
			dto.Email = dto.Email.Trim();
			dto.Password = dto.Password.Trim();
		}
	}
}