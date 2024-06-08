namespace ProductPricerTechTestAPI.Requests
{
    public class EditProductPriceRequest
    {
        public Guid ProductGuid { get; set; }
        public decimal Price { get; set; }
    }
}
