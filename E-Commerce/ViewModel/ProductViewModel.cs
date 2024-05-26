using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
    }
}
