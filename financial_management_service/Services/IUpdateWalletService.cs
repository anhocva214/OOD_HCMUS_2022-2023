using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Core.Object;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IUpdateWalletService
	{
		Task<Wallet> Execute(UpdateWalletReqDto dto);
	}

	public class UpdateWalletService : BaseService, IUpdateWalletService
	{
		private readonly IUnitOfWork _uok;

		public UpdateWalletService(IUnitOfWork uok) => _uok = uok;

		public async Task<Wallet> Execute(UpdateWalletReqDto dto)
		{
			var wallet = await Validate(dto);

			InitWallet(dto, wallet);

			_uok.Wallet.Update(wallet);

			await _uok.CompleteAsync();

			return wallet;
		}

		private async Task<Wallet> Validate(UpdateWalletReqDto dto)
		{
			If.IsTrue(dto.WalletId.IsNullOrEmpty() || !dto.WalletId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsNull(dto.WalletName).ThrBiz(ErrorCode._400_02, "Tên ví không được để trống.");

			var wallet = await _uok.Wallet.GetByIdAsync(dto.WalletId);

			If.IsTrue(wallet == null).ThrBiz(ErrorCode._400_03, "Không tìm thấy dữ liệu hợp lệ.");

			return wallet;
		}

		private static void InitWallet(UpdateWalletReqDto dto, Wallet wallet)
        {
			wallet.WalletName = dto.WalletName;
			wallet.WalletBalance = dto.WalletBalance;
        }
    }
}