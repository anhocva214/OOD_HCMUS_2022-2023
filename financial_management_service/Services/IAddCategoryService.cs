using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
    public interface IAddCategoryService
    {
        Task<Categories> Execute(AddCategoryReqDto dto);
    }

    public class AddCategoryService : BaseService, IAddCategoryService
    {
        private readonly IUnitOfWork _uok;

        public AddCategoryService(IUnitOfWork uok) => _uok = uok;

        public async Task<Categories> Execute(AddCategoryReqDto dto)
        {
            Validate(dto);

            var category = InitCategory(dto);

            _uok.Categories.Add(category);

            await _uok.CompleteAsync();

            return category;
        }

        private void Validate(AddCategoryReqDto dto)
        {
            If.IsNull(dto.Name).ThrBiz(ErrorCode._400_01, "Tên danh mục không được đê trống.");
        }

        private static Categories InitCategory(AddCategoryReqDto dto)
        {
            return new Categories()
            {
                Name = dto.Name,
            };
        }
    }

}
