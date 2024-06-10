using ProductPricerTechTestAPI.Models;

namespace ProductPricerTechTestAPI.Services
{
    public interface IProductData
    {
        Task<List<Product>> GetProductsAsync();
        Product UpdatePrice(Guid productGuid, decimal price);
        Task<Product> AddProductAsync(string productName, decimal price);
    }
}