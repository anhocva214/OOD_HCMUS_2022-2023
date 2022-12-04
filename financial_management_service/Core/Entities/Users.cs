using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace financial_management_service.Core.Entities
{
    [Table("users")]
    public class Users
    {

        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        
        public Users()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
