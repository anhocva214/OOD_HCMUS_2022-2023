namespace financial_management_service.Core.Dtos
{
    public class SearchTransactionDto
    {
    }

    public class SearchTransactionReqDto
    {
        public string? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class SearchTransactionResDto
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
    }
}
