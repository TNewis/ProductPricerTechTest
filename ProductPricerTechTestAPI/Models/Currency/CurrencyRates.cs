namespace ProductPricerTechTestAPI.Models.Currency
{
    public class CurrencyRates
    {
        public string Disclaimer { get; set; }
        public string Licence { get; set; }
        public int Timestamp { get; set; }
        public string Base {  get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}
