namespace ProductPricerTechTestAPI.Services.CurrencyConversionService
{
    public class DummyCurrencyConversionService : ICurrencyConversionService
    {

        public async Task<decimal> ConvertCurrency(decimal value, string fromCurrencyCode, string toCurrencyCode)
        {
            if (toCurrencyCode == "USD")
            {
                return value * 1.2m;
            }

            if (toCurrencyCode == "GBP")
            {
                return value / 1.2m;
            }

            throw new Exception("Currency code not recognised");
        }
    }
}
