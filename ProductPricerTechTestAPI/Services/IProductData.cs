using ProductPricerTechTestAPI.Models;

namespace ProductPricerTechTestAPI.Services
{
    public interface IProductData
    {
        List<Product> GetProducts();
        Product? UpdatePrice(Guid productGuid, decimal price);
        Product AddProduct(string productName, decimal price);
    }
}