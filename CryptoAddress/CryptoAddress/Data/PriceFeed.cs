using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace CryptoAddress.Data {
    public class PriceFeed {
        private string apiKey = "";
        private readonly string converterUrl = "https://pro-api.coinmarketcap.com/v1/tools/price-conversion";


        private string MakeApiCall(string firstCurrency, string secondCurrency, double amount = 1) {
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
        

    }
}
