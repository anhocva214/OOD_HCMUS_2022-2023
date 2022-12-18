using financial_management_service.Core.Constant;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
    public interface IDeleteCategoryService
    {
        Task Execute(string categoryId);
    }

    public class DeleteCategoryService : BaseService, IDeleteCategoryService
    {
        private readonly IUnitOfWork _uok;

        public DeleteCategoryService(IUnitOfWork uok) => _uok = uok;

        public async Task Execute(string categoryId)
        {
            var category = await Validate(categoryId);

            _uok.Categories.Remove(category);

            await _uok.CompleteAsync();
        }

        private async Task<Categories> Validate(string categoryId)
        {
            If.IsTrue(categoryId.IsNullOrEmpty() || !categoryId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

            var category = await _uok.Categories.GetByIdAsync(categoryId);

            If.IsTrue(category == null).ThrBiz(ErrorCode._400_02, "Không tìm thấy dữ liệu phù hợp.");

            return category;
        }
    }

}
