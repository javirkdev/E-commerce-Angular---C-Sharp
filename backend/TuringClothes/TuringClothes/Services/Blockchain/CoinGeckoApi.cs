﻿using System.Text.Json;

namespace TuringClothes.Services.Blockchain
{
    public class CoinGeckoApi
    {
        private const string URL = "https://api.coingecko.com/api/v3/";

        public async Task<decimal> GetEthereumPriceAsync()
        {
            using HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(URL)
            };

            string json = await client.GetStringAsync("coins/ethereum");
            JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            JsonElement jsonMarketData = jsonElement.GetProperty("market_data");
            JsonElement jsonPrices = jsonMarketData.GetProperty("current_price");
            decimal euros = jsonPrices.GetProperty("eur").GetDecimal();

            return euros;
        }
    }
}
