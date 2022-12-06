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
        [Column("category_name")]
        public string? CategoryName { get; set; }
        [Column("category_group")]
        public string? CategoryGroup { get; set; }

    }
}
