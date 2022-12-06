using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Core.Object;
using financial_management_service.Infrastructure.Callout;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IForgotPasswordService
	{
		Task Execute(ForgotPasswordReqDto dto);
	}

	public class ForgotPasswordService : BaseService, IForgotPasswordService
	{
		private readonly IUnitOfWork _uok;
		private readonly IMailService _mailService;
		private readonly IWebHostEnvironment _environment;

		public ForgotPasswordService(IUnitOfWork uok, IMailService mailService, IWebHostEnvironment environment)
        {
			_uok = uok;
			_mailService = mailService;
			_environment = environment;
        }

		public async Task Execute(ForgotPasswordReqDto dto)
		{
			var user = await Validate(dto);

			var guid = Guid.NewGuid().ToString().Split("-");
			var passwordNew = guid[0].ToUpper() + guid[1].ToUpper();

			user.Password = passwordNew;

			_uok.Users.Update(user);
			await _uok.CompleteAsync();

			//gửi email chứa mật khẩu mới về mail
			_= _mailService.SendEmailAsync(await BuildEmail(dto, passwordNew));
		}

		private async Task<Users> Validate(ForgotPasswordReqDto dto)
        {
			If.IsNull(dto.Email).ThrBiz(ErrorCode._400_01, "Email không được để trống.");

			dto.Email = dto.Email.Trim();

			var user = await _uok.Users.GetFirstOrDefaultAsync(x => x.Email == dto.Email);

			If.IsTrue(user == null).ThrBiz(ErrorCode._400_02, "Email chưa được đăng ký sử dụng. Vui lòng kiểm tra lại.");

			return user;
		}

		private async Task<MailRequest> BuildEmail(ForgotPasswordReqDto dto, string password)
        {
			return new MailRequest()
			{
				ToEmail = dto.Email,
				Subject = "[FINANCIAL_MANAGEMENT] Lấy lại mật khẩu.",
				Body = (await System.IO.File.ReadAllTextAsync(FileUtils.GetFilePath(_environment, "EmailTemplate/email.html"))).Replace("{EMAIL}", dto.Email).Replace("{PASSWORD}", password),
			};
		}
	}
}