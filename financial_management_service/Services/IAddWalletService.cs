using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Core.Object;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IAddWalletService
	{
		Task<Wallet> Execute(WalletReqDto dto);
	}

	public class AddWalletService : BaseService, IAddWalletService
	{
		private readonly IUnitOfWork _uok;

		public AddWalletService(IUnitOfWork uok) => _uok = uok;

		public async Task<Wallet> Execute(WalletReqDto dto)
		{
			Validate(dto);

			var wallet = new Wallet()
			{
				UserId = dto.UserId,
				WalletName = dto.WalletName,
				WalletBalance = dto.WalletBalance
			};

			_uok.Wallet.Add(wallet);

			await _uok.CompleteAsync();

			return wallet;
		}

		private  async void Validate(WalletReqDto dto)
		{
			If.IsTrue(dto.UserId.IsNullOrEmpty() || !dto.UserId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(await _uok.Users.GetByIdAsync(dto.UserId) == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy ví của bạn.");

			If.IsNull(dto.WalletName).ThrBiz(ErrorCode._400_03, "Tên ví không được để trống.");
		}
	}
}