using System.Runtime.InteropServices;
using financial_management_service.Infrastructure.Repository;

namespace financial_management_service.Infrastructure.DBContext
{
    public interface IUnitOfWork : IDisposable  
    {
        Task<int> CompleteAsync();
		ICategoriesRepository Categories { get; }
		ITransactionRepository Transaction { get; }
		IWalletRepository Wallet { get; }
        IUsersRepository Users { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
			Categories = new CategoriesRepository(_context);
			Transaction = new TransactionRepository(_context);
			Wallet = new WalletRepository(_context);

            Users = new UsersRepository(_context);
        }

        public IUsersRepository Users { get; private set; }

        public IWalletRepository Wallet {get; private set; }
		public ITransactionRepository Transaction {get; private set; }
		public ICategoriesRepository Categories {get; private set; }
		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
        }
    }
}


