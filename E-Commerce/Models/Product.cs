using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column("Name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("Brand", TypeName = "varchar(100)")]
        public string Brand { get; set; }

        [Column("Category", TypeName = "varchar(100)")]
        public string Category { get; set; }

        [Column("Price", TypeName = "varchar(100)")]
        public string Price { get; set; }
        public string Image { get; set; }
        
    }
}
