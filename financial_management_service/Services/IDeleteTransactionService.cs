using financial_management_service.Core.Constant;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IDeleteTransactionService
	{
		Task Execute(string transactionId);
	}

	public class DeleteTransactionService : BaseService, IDeleteTransactionService
	{
		private readonly IUnitOfWork _uok;

		public DeleteTransactionService(IUnitOfWork uok) => _uok = uok;
		

		public async Task Execute(string transactionId)
		{
			var transaction = await Validate(transactionId);

			_uok.Transaction.Remove(transaction);

			await _uok.CompleteAsync();
		}

		private async Task<Transaction> Validate(string transactionId)
		{
			If.IsTrue(transactionId.IsNullOrEmpty() || !transactionId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			var transaction = await _uok.Transaction.GetByIdAsync(transactionId);

			If.IsTrue(transaction == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy dữ liệu giao dịch.");

			return transaction;
		}
	}
}