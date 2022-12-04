using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Core.Entities;

namespace financial_management_service.Infrastructure.Repository
{
    public interface ICategoriesRepository : IRepository<Categories>
    {
		
    }

    public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationContext context) : base(context)
        {
			
        }
    }
}
