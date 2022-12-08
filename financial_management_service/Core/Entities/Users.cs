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
        [Column("gender")]
        public string? Gender { get; set; }
        [Column("birthday")]
        public DateTime? Birthday { get; set; }
        [Column("phone_number")]
        public int? PhoneNumber { get; set; }
        [Column("fullname")]
        public string? FullNmae { get; set; }

        public Users()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
