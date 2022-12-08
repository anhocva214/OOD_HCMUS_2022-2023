using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace financial_management_service.Core.Entities
{
    [Table("transaction")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        public string? Id { get; set; }
        [Column("user_id")]
        public string? UserId { get; set; }
        [Column("wallet_id")]
        public string? WalletId { get; set; }
        [Column("category_id")]
        public string? CategoryId { get; set; }
        [Column("amount")]
        public decimal? Amount { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("note")]
        public string? Note { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        public Transaction()
        {
            Id = Guid.NewGuid().ToString();
            WalletId = String.Empty;
        }

    }
}
