using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace financial_management_service.Core.Entities
{
    [Table("wallet")]
    public class Wallet
    {
        [Key]
        [Column("id")]
        public string? Id { get; set; }
        [Column("user_id")]
        public string? UserId { get; set; }
        [Column("wallet_name")]
        public string? WalletName { get; set; }
        [Column("wallet_balance")]
        public decimal? WalletBalance { get; set; }

        public Wallet()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
