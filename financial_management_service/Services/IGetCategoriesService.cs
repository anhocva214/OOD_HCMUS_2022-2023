using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;

namespace financial_management_service.Services
{
	public interface IGetCategoriesService
	{
		Task<List<Categories>> Execute();
	}

	public class GetCategoriesService : BaseService, IGetCategoriesService
	{
		private readonly IUnitOfWork _uok;

		public GetCategoriesService(IUnitOfWork uok) => _uok = uok;

		public async Task<List<Categories>> Execute() => (await _uok.Categories.GetListAsync()).OrderBy(y => y.CategoryName).ToList();
	}
}