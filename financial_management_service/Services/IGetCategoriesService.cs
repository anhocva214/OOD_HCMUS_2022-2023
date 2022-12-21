using financial_management_service.Core.Dtos;
using financial_management_service.Infrastructure.DBContext;

namespace financial_management_service.Services
{
	public interface IGetCategoriesService
	{
		Task<List<CategoryResDto>> Execute();
	}

	public class GetCategoriesService : BaseService, IGetCategoriesService
	{
		private readonly IUnitOfWork _uok;

		public GetCategoriesService(IUnitOfWork uok) => _uok = uok;

		public async Task<List<CategoryResDto>> Execute() 
		{
			var categories = new List<CategoryResDto>();

            var result = await _uok.Categories.GetListAsync();

			foreach (var category in result )
			{
				categories.Add(new CategoryResDto()
				{
					Name= category.Name
				});

            }

			return categories;
        } 
	}
}