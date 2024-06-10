using Microsoft.Data.SqlClient;
using ProductPricerTechTestAPI.Controllers;
using ProductPricerTechTestAPI.Models;
using ProductPricerTechTestAPI.Services.CurrencyConversionService;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace ProductPricerTechTestAPI.Services
{
    public class ProductData : IProductData
    {
        private const string ProductDbConnectionString = "ProductDBContext";

        private const string StoredProcedureAddNewProduct = "AddNewProduct";
        private const string AddNewProductProductName = "@ProductName";
        private const string AddNewProductPrice = "@Price";

        private ProductDBContext _dbContext = new();

        private ICurrencyConversionService _currencyConversionService;
        private ILogger _logger;

        public ProductData(ILogger<ProductData> logger, ICurrencyConversionService currencyConversionService)
        {
            _logger = logger;
            _currencyConversionService = currencyConversionService;
        }

        public async Task<Product> AddProductAsync(string productName, decimal price)
        {
            Product newProduct = new();

            SqlConnection connection;
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ProductDbConnectionString].ConnectionString;

            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(StoredProcedureAddNewProduct, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter(AddNewProductProductName, productName));
            command.Parameters.Add(new SqlParameter(AddNewProductPrice, price));

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    newProduct.Name = reader["Name"].ToString();
                    newProduct.Price = await _currencyConversionService.ConvertCurrency(Convert.ToDecimal(reader["Price"]),"USD","GBP");
                    newProduct.Guid = Guid.Parse(reader["Guid"].ToString());                 
                }
            }

            return newProduct;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var productEntities= _dbContext.Product.ToList();
            var products = new List<Product>();
            foreach (var productEntity in productEntities)
            {
                var product = productEntity.ToProduct();

                product.Price= await _currencyConversionService.ConvertCurrency(product.Price, "GBP", "USD");
                product.Currency = "USD";

                products.Add(product);

            }

            return products;
        }

        public Product UpdatePrice(Guid productGuid, decimal price)
        {
            var product = _dbContext.Product.SingleOrDefault(p => p.Guid == productGuid);

            if (product != null) 
            { 
                product.Price = price;
                _dbContext.SaveChanges();

                return product.ToProduct();
            }

            _logger.LogError("Guid " + productGuid + " does not exist in database.");
            throw new KeyNotFoundException("Guid " + productGuid + " does not exist in database.");
        }
    }
}
