using Microsoft.AspNetCore.Mvc;
using ProductPricerTechTestAPI.Models;
using ProductPricerTechTestAPI.Requests;
using ProductPricerTechTestAPI.Services;

namespace ProductPricerTechTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductData _productData;

        public ProductController(ILogger<ProductController> logger, IProductData productData)
        {
            _logger = logger;
            _productData = productData;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _productData.GetProductsAsync();
        }

        [HttpPut(Name = "EditProductPrice")]
        public async Task<Product> PutAsync(EditProductPriceRequest request)
        {
            if (request.Price < 0.00m)
            {
                _logger.LogError("Bad EditProductPriceRequest: Price negative");
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            }
            return await _productData.UpdatePriceAsync(request.ProductGuid, request.Price);
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<Product> PostAsync(AddProductRequest request)
        {
            if (request.Price < 0.00m)
            {
                _logger.LogError("Bad AddProductRequest: Price negative");
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            }
            return await _productData.AddProductAsync(request.ProductName, request.Price);
        }
    }
}