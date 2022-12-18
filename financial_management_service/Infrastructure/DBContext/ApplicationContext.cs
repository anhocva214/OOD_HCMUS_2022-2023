
using financial_management_service.Core.Entities;
using financial_management_service.Infrastructure.Extenstions;
using Microsoft.EntityFrameworkCore;

namespace financial_management_service.Infrastructure.DBContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public DbSet<Users>? Users { get; set; }
        public DbSet<Wallet>? Wallet { get; set; }
        public DbSet<Transaction>? Transaction { get; set; }
        public DbSet<Categories>? Categories { get; set; }
    }
}
