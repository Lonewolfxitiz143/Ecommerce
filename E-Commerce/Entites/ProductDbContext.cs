using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<ForImage> imagetable { get; set; }
    }
}
