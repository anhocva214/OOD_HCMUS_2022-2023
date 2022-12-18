using financial_management_service.Core.Constant;
using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
    public interface IGetUserService
    {
        Task<Users> Execute(string userId);
    }

    public class GetUserService : BaseService, IGetUserService
    {
        private readonly IUnitOfWork _uok;

        public GetUserService(IUnitOfWork uok) => _uok = uok;

        public async Task<Users> Execute(string userId)
        {
            var user = await _uok.Users.GetByIdAsync(userId);

            If.IsTrue(user == null).ThrBiz(ErrorCode._400_04, "Không tìm thấy dữ liệu tài khoản.");

            return user;
        }
    }

}
