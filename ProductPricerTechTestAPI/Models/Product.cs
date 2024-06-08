using Microsoft.EntityFrameworkCore;

namespace ProductPricerTechTestAPI.Models
{
    public class Product
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

}
