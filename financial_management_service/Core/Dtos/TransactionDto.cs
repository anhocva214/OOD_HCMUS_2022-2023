namespace financial_management_service.Core.Dtos
{
    public class TransactionDto
    {
    }

    public class TransactionReqDto
    {
        public string? UserId { get; set; }
        public string? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime?  Date{ get; set; }
        public string? Note { get; set; }
        public TransactionReqDto()
        {
            UserId = String.Empty;
            CategoryId = String.Empty;
            Amount = 0;
            Date = null;
            Note = String.Empty;
        }
    }
    
    public class UpdateTransactionReqDto
    {
        public string? TransactionId { get; set; }
        public string? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Note { get; set; }
        public UpdateTransactionReqDto()
        {
            TransactionId = String.Empty;
            CategoryId = String.Empty;
            Amount = 0;
            Date = null;
            Note = String.Empty;
        }
    }

}
