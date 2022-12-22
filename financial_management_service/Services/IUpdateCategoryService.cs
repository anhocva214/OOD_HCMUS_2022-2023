using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
    public interface IUpdateCategoryService
    {
        Task<string> Execute(UpdateCategoryReqDto dto);
    }

    public class UpdateCategoryService : BaseService, IUpdateCategoryService
    {
        private readonly IUnitOfWork _uok;

        public UpdateCategoryService(IUnitOfWork uok) => _uok = uok;

        public async Task<string> Execute(UpdateCategoryReqDto dto)
        {
            var category = await Validate(dto);

            InitCategory(dto, category);

            _uok.Categories.Update(category);

            await _uok.CompleteAsync();

            return category.Name;
        }

        private async Task<Categories> Validate(UpdateCategoryReqDto dto)
        {
            If.IsTrue(dto.Id.IsNullOrEmpty() || !dto.Id.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

            If.IsNull(dto.Name).ThrBiz(ErrorCode._400_02, "Tên danh mục không được để trống.");

            var category = await _uok.Categories.GetByIdAsync(dto.Id);

            If.IsTrue(category == null).ThrBiz(ErrorCode._400_04, "Không tìm thấy dữ danh mục.");

            return category;
        }

        private static void InitCategory(UpdateCategoryReqDto dto, Categories category)
        {
            category.Name = dto.Name;
        }
    }

}
