using Microsoft.EntityFrameworkCore;

namespace ProductPricerTechTestAPI.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public Product ToProduct()
        {
            return new Product {  Guid = this.Guid, Name = this.Name, Price = this.Price };
        }
    }

    public class ProductDBContext : DbContext
    {
        private const string ConnectionString = "ProductDBContext";

        public DbSet<ProductEntity> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
        }
    }
}
