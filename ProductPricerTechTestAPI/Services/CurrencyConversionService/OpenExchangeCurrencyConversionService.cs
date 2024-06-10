using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProductPricerTechTestAPI.Models;
using ProductPricerTechTestAPI.Models.Currency;
using System.IO;

namespace ProductPricerTechTestAPI.Services.CurrencyConversionService
{
    public class OpenExchangeCurrencyConversionService : ICurrencyConversionService
    {
        public CurrencyRates Rates { get; set; } = new CurrencyRates();

        readonly HttpClient client = new HttpClient();

        private readonly string appId;
        private const string OpenExchangeAppId = "OpenExchangeAppId";

        private const string apiURL = "https://openexchangerates.org/api/latest.json?app_id={0}";

        public OpenExchangeCurrencyConversionService()
        {
            appId = System.Configuration.ConfigurationManager.AppSettings[OpenExchangeAppId];
        }

        public async Task<decimal> ConvertCurrency(decimal value, string fromCurrencyCode, string toCurrencyCode)
        {
            if (Rates.Rates.IsNullOrEmpty())
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(apiURL, appId));
                if (response.IsSuccessStatusCode)
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    Rates = JsonConvert.DeserializeObject<CurrencyRates>(contentString);
                }
            }

            if (fromCurrencyCode == "USD")
            {
                //API base currency type
                var rate = Rates.Rates.First(r => r.Key == toCurrencyCode).Value;
                return value * rate;
            }

            if(toCurrencyCode == "USD")
            {
                //API base currency type
                var rate = Rates.Rates.First(r => r.Key == fromCurrencyCode).Value;
                return value / rate;
            }

            var intermediateRate = Rates.Rates.First(r => r.Key == fromCurrencyCode).Value;
            var intermediateValue = value / intermediateRate;
            var targetRate= Rates.Rates.First(r => r.Key == toCurrencyCode).Value;
            return intermediateValue * targetRate;

        }
    }
}
