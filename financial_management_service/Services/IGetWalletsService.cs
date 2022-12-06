using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;

namespace financial_management_service.Services
{
	public interface IGetWalletsService
	{
		Task<List<Wallet>> Execute(string userId);
	}

	public class GetWalletsService : BaseService, IGetWalletsService
	{
		private readonly IUnitOfWork _uok;

		public GetWalletsService(IUnitOfWork uok) => _uok = uok;

		public async Task<List<Wallet>> Execute(string userId) => (await _uok.Wallet.GetListAsync(x => x.UserId == userId)).OrderBy(x => x.WalletName).ToList();
	}
}