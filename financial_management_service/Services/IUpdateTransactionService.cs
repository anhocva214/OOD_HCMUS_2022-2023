using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IUpdateTransactionService
	{
		Task<Transaction> Execute(UpdateTransactionReqDto dto);
	}

	public class UpdateTransactionService : BaseService, IUpdateTransactionService
	{
		private readonly IUnitOfWork _uok;

		public UpdateTransactionService(IUnitOfWork uok) => _uok = uok;

		public async Task<Transaction> Execute(UpdateTransactionReqDto dto)
		{
			var transaction = await Validate(dto);

			InitTransaction(dto, transaction);

			_uok.Transaction.Update(transaction);

			await _uok.CompleteAsync();

			return transaction;
		}

		private async Task<Transaction> Validate(UpdateTransactionReqDto dto)
		{
			If.IsTrue(dto.TransactionId.IsNullOrEmpty() || !dto.TransactionId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(dto.CategoryId.IsNullOrEmpty() || !dto.CategoryId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(!dto.Amount.HasValue).ThrBiz(ErrorCode._400_02, "Vui lòng điền số tiền.");

			If.IsTrue(dto.Date == null).ThrBiz(ErrorCode._400_03, "Vui lòng chọn ngày.");

			var transaction = await _uok.Transaction.GetByIdAsync(dto.TransactionId);

			If.IsTrue(transaction == null).ThrBiz(ErrorCode._400_04, "Không tìm thấy dữ giao dịch.");

			If.IsTrue(await _uok.Categories.GetByIdAsync(dto.CategoryId) == null).ThrBiz(ErrorCode._400_05, "Không tìm thấy dữ liệu danh mục.");

			return transaction;
		}

		private static void InitTransaction(UpdateTransactionReqDto dto, Transaction transaction)
		{
			transaction.CategoryId = dto.CategoryId;
			transaction.Amount = dto.Amount;
			transaction.Date = dto.Date;
			transaction.Note = dto.Note;
			transaction.ModifiedAt = DateTime.Now;
		}
	}
}