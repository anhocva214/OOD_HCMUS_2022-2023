using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IGetTransactionsService
	{
		Task<List<SearchTransactionResDto>> Execute(SearchTransactionReqDto dto);
	}

	public class GetTransactionsService : BaseService, IGetTransactionsService
	{
		private readonly static string _typeDate = "dd/MM/yyyy";

		private readonly IUnitOfWork _uok;

		public GetTransactionsService(IUnitOfWork uok) => _uok = uok;

		public async Task<List<SearchTransactionResDto>> Execute(SearchTransactionReqDto dto)
		{
			await Validate(dto);

			return await _uok.Transaction.GetTransactionsBy(dto);
		}

		private async Task Validate(SearchTransactionReqDto dto)
		{
			If.IsTrue(dto.UserId.IsNullOrEmpty() || !dto.UserId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(dto.WalletId.IsNullOrEmpty() || !dto.WalletId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(await _uok.Users.GetByIdAsync(dto.UserId) == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy tài khoản đăng nhập.");

			If.IsTrue(await _uok.Wallet.GetByIdAsync(dto.WalletId) == null).ThrBiz(ErrorCode._400_03, "Không tìm thấy dữ liệu ví.");

			if (!dto.FromDate.IsNullOrEmpty() && !dto.ToDate.IsNullOrEmpty())
			{
				DateTime? fromDate = DatetimeUtils.ToDate(dto.FromDate, _typeDate);
				DateTime? toDate = DatetimeUtils.ToDate(dto.ToDate, _typeDate);

				If.IsTrue(fromDate > toDate).ThrBiz(ErrorCode._400_04, "Ngày bắt đầu phải nhỏ hoặc bằng ngày kết thúc.");

				dto.FromDate = fromDate?.ToString("yyyy-MM-dd 00:00:00");
				dto.ToDate = toDate?.ToString("yyyy-MM-dd 23:59:59");
			}
		}
	}
}