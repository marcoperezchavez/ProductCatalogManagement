using Microsoft.EntityFrameworkCore;
using ProductCatalogManagement.Server.Models;

namespace ProductCatalogManagement.Server.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
