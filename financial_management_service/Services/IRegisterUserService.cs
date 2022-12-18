using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IRegisterUserService
	{
		Task Execute(UserReqDto dto);
	}

	public class RegisterUserService : BaseService, IRegisterUserService
	{
		private readonly IUnitOfWork _uok;

		public RegisterUserService(IUnitOfWork uok) => _uok = uok;

		public async Task Execute(UserReqDto dto)
		{
			await Validate(dto);

			_uok.Users.Add(InitUser(dto));

			await _uok.CompleteAsync();
		}

		private async Task Validate(UserReqDto dto)
		{
			If.IsNull(dto.Email).ThrBiz(ErrorCode._400_01, "Email không được để trống!");

			If.IsTrue(!RegexUtils.RegexEmail(dto.Email)).ThrBiz(ErrorCode._400_02, "Email không đúng định dạng!");

			If.IsNull(dto.Password).ThrBiz(ErrorCode._400_03, "Mật khẩu không được để trống.");

            If.IsNull(dto.FullName).ThrBiz(ErrorCode._400_04, "Họ và tên không được để trống.");

            HandleData(dto);

			If.IsTrue(await _uok.Users.GetFirstOrDefaultAsync(x => x.Email == dto.Email) != null).ThrBiz(ErrorCode._400_04, "Email đã được đăng ký tài khoản.");
		}

		private static void HandleData(UserReqDto dto)
		{
			dto.Email = dto.Email.Trim();
			dto.Password = dto.Password.Trim();
			dto.FullName = dto.FullName.Trim();
		}

		private static Users InitUser(UserReqDto dto)
		{
			return new Users()
			{
				Email = dto.Email,
				Password = dto.Password,
				FullName = dto.FullName
			};
		}
	}
}