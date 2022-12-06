using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;

namespace financial_management_service.Infrastructure.Repository
{
    public interface IUsersRepository : IRepository<Users>
    {
    }

    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
