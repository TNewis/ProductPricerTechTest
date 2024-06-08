namespace ProductPricerTechTestAPI.Requests
{
    public class AddProductRequest
    {
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
