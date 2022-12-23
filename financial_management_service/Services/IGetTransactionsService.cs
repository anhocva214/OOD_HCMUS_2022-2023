using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IGetTransactionsService
	{
		Task<List<SearchTransactionResDto>> Execute(string userId);
	}

	public class GetTransactionsService : BaseService, IGetTransactionsService
	{
		private readonly IUnitOfWork _uok;

		public GetTransactionsService(IUnitOfWork uok) => _uok = uok;

		public async Task<List<SearchTransactionResDto>> Execute(string userId)
		{
			await Validate(userId);

            //return await _uok.Transaction.GetTransactionsBy(dto);

            return await GetTransactions();
        }

		private async Task<List<SearchTransactionResDto>> GetTransactions()
		{
            var transactions = new List<SearchTransactionResDto>();
            var result = await _uok.Transaction.GetListAsync();
            foreach (var item in result)
            {
                transactions.Add(new SearchTransactionResDto()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    CategoryId = item.CategoryId,
                    Amount = item.Amount,
                    Date = item.Date,
                    Note = item.Note
                });
            }

            return transactions;
        }

        private async Task Validate(string userId)
		{
			If.IsTrue(userId.IsNullOrEmpty() || !userId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(await _uok.Users.GetByIdAsync(userId) == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy tài khoản đăng nhập.");

			//if (dto.FromDate != null && dto.ToDate != null)
			//	If.IsTrue(dto.FromDate > dto.ToDate).ThrBiz(ErrorCode._400_04, "Ngày bắt đầu phải nhỏ hoặc bằng ngày kết thúc.");
		}
	}
}