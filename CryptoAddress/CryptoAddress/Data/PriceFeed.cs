using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Web;
using CryptoAddress.Protected;

namespace CryptoAddress.Data {
    public class PriceFeed {
        private static string apiKey = Keys.apiKey;
        
        private static string ApiConverterTool(string firstCurrency, string secondCurrency, double amount = 1) {
            string converterUrl = "https://pro-api.coinmarketcap.com/v1/tools/price-conversion";
            UriBuilder url = new UriBuilder(converterUrl);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = firstCurrency;
            queryString["amount"] = amount.ToString();
            queryString["convert"] = secondCurrency;

            url.Query = queryString.ToString();

            WebClient client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(url.ToString());
        }

        public static Tuple<double, DateTime> GetSingleExchangeRate(string firstCurrency, string secondCurrency, double amount = 1) {
            string fullJson = ApiConverterTool(firstCurrency, secondCurrency);
            if (string.IsNullOrEmpty(fullJson)) {
                throw new ArgumentNullException();
            }
            JsonDocumentOptions options = new JsonDocumentOptions { AllowTrailingCommas = true };

            string extractedPrice = "";
            string extractedDate = "";
            using (JsonDocument document = JsonDocument.Parse(fullJson, options)) {
                extractedPrice = document.RootElement
                    .GetProperty("data")
                    .GetProperty("quote")
                    .GetProperty(secondCurrency)
                    .GetProperty("price")
                    .ToString();

                extractedDate = document.RootElement
                    .GetProperty("data")
                    .GetProperty("quote")
                    .GetProperty(secondCurrency)
                    .GetProperty("last_updated")
                    .ToString();
            }

            Tuple<double, DateTime> result = new Tuple<double, DateTime>(double.Parse(extractedPrice), DateTime.Parse(extractedDate));

            return result;
        }

        private static string ApiFullCall() {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }
        
        

    }
}
