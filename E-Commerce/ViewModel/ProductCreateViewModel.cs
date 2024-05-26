using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        public string Price { get; set; }
        public IFormFile Image { get; set; }

    }
}

