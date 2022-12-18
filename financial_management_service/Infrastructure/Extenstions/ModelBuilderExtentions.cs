using financial_management_service.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace financial_management_service.Infrastructure.Extenstions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories() { Id = "1", Name = "Sức khỏe" },
                new Categories() { Id = "2", Name = "Chuyển tiền" },
                new Categories() { Id = "3", Name = "Ăn uống" },
                new Categories() { Id = "4", Name = "Mua sắm" },
                new Categories() { Id = "5", Name = "Kiến thức" },
                new Categories() { Id = "6", Name = "Khác" }
                );
        }
    }
}
