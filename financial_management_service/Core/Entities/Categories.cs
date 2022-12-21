using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace financial_management_service.Core.Entities
{
    [Table("categories")]
    public class Categories
    {
        [Key]
        [Column("id")]
        public string? Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        public Categories()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
