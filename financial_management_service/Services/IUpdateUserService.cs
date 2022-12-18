using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
    public interface IUpdateUserService
    {
        Task<LoginResDto> Execute(UpdateUserReqDto dto);
    }

    public class UpdateUserService : BaseService, IUpdateUserService
    {
        private readonly IUnitOfWork _uok;

        public UpdateUserService(IUnitOfWork uok) => _uok = uok;

        public async Task<LoginResDto> Execute(UpdateUserReqDto dto)
        {
            var user = await Validate(dto);

            InitUser(dto, user);

            _uok.Users.Update(user);

            await _uok.CompleteAsync();

            return new LoginResDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                Birthday = user.Birthday,
                PhoneNumber = user.PhoneNumber
            };
        }

        private async Task<Users> Validate(UpdateUserReqDto dto)
        {
            If.IsTrue(dto.Id.IsNullOrEmpty() || !dto.Id.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

            if (dto.Password != null)
            {
                If.IsNull(dto.PasswordComfirm).ThrBiz(ErrorCode._400_02, "Mật khẩu xác nhận không được để trống.");

                dto.Password = dto.Password.Trim();
                dto.PasswordComfirm = dto.PasswordComfirm.Trim();

                If.IsTrue(dto.Password != dto.PasswordComfirm).ThrBiz(ErrorCode._400_03, "Mật khẩu xác nhận không khớp.");
            }

            var user = await _uok.Users.GetByIdAsync(dto.Id);

            If.IsTrue(user == null).ThrBiz(ErrorCode._400_04, "Không tìm thấy dữ liệu hợp lệ.");

            return user;
        }

        private static void InitUser(UpdateUserReqDto dto, Users user)
        {
            user.FullName = dto.FullName.IsNullOrEmpty() ? user.FullName : dto.FullName;
            user.Gender = dto.Gender.IsNullOrEmpty() ? user.Gender : dto.Gender;
            user.Birthday= !dto.Birthday.HasValue ? user.Birthday : dto.Birthday;
            user.PhoneNumber= !dto.PhoneNumber.HasValue ? user.PhoneNumber : dto.PhoneNumber;
            user.Password = dto.Password.IsNullOrEmpty() ? user.Password : dto.Password;
        }
    }
}
