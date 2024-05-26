using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.ViewModel
{
    public class ProductListResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }
        public string Category { get; set; }
        public string BrandCategory { get; set; }
        
        public string Price { get; set; }
        public decimal PriceWithVat { get; set; }
        public string Image { get; set; }
    }
}
