using financial_management_service.Core.Constant;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IDeleteWalletService
	{
		Task Execute(string walletId);
	}

	public class DeleteWalletService : BaseService, IDeleteWalletService
	{
		private readonly IUnitOfWork _uok;

		public DeleteWalletService(IUnitOfWork uok) => _uok = uok;

		public async Task Execute(string walletId)
		{
			var wallet = await Validate(walletId);

			_uok.Wallet.Remove(wallet);

			await _uok.CompleteAsync();
		}

		private async Task<Wallet> Validate(string walletId)
		{
			If.IsTrue(walletId.IsNullOrEmpty() || !walletId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			var wallet = await _uok.Wallet.GetByIdAsync(walletId);

			If.IsTrue(wallet == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy dữ liệu hợp lệ.");

			If.IsTrue((await _uok.Transaction.GetListAsync(x => x.WalletId == walletId)).ToList().Count > 0).ThrBiz(ErrorCode._400_03, "Ví đang được sử dụng, không thể xoá.");

			return wallet;
		}
	}
}