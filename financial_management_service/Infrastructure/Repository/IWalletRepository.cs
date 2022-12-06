using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Core.Entities;

namespace financial_management_service.Infrastructure.Repository
{
    public interface IWalletRepository : IRepository<Wallet>
    {
		
    }

    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationContext context) : base(context)
        {
			
        }
    }
}
