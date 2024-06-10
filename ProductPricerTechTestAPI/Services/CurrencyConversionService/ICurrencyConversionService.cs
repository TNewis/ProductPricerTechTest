namespace ProductPricerTechTestAPI.Services.CurrencyConversionService
{
    public interface ICurrencyConversionService
    {
        public Task<decimal> ConvertCurrency(decimal value, string fromCurrencyCode, string toCurrencyCode);
    }
}
