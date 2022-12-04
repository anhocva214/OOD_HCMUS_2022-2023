using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Core.Entities;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Object;
using financial_management_service.Infrastructure.Extenstions;
using financial_management_service.Extensions;

namespace financial_management_service.Infrastructure.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<SearchTransactionResDto>> GetTransactionsBy(SearchTransactionReqDto dto);
    }

    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        #region SQL
        private readonly string QuerySearchTransaction = "select count(1) as Total FROM payment_order ";

        #endregion
        public TransactionRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<SearchTransactionResDto>> GetTransactionsBy(SearchTransactionReqDto dto)
        {
            var parameters = new List<SqlParameter>();
            var sql = QuerySearchTransaction + BuildCondition(dto, parameters) + $" order by modified_at desc";

            return await _context.Database.SqlQueryAsync<SearchTransactionResDto>(sql, parameters);
        }

        private static string BuildCondition(SearchTransactionReqDto dto, List<SqlParameter> parameters)
        {
            var condition = " WHERE 1=1";

            if (!dto.UserId.IsNullOrEmpty())
            {
                condition += $" AND user_id = @UserId";
                parameters.Add(new SqlParameter("UserId", dto.UserId));
            }

            if (!dto.WalletId.IsNullOrEmpty())
            {
                condition += $" AND wallet_id = @WalletId";
                parameters.Add(new SqlParameter("WalletId", dto.WalletId));
            }

            if (!dto.FromDate.IsNullOrEmpty() && !dto.ToDate.IsNullOrEmpty())
                condition += @$" AND date BETWEEN '{dto.FromDate}' AND '{dto.ToDate}'";

            return condition;
        }
    }
}
