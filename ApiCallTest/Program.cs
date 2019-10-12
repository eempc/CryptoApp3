using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Web;
using System.IO;

namespace ApiCallTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            string str = File.ReadAllText(@"D:\Temp\CMCconverterTestJSON.txt");
            //Console.WriteLine(str);

            JsonDocumentOptions options = new JsonDocumentOptions { AllowTrailingCommas = true };
            string price = "";
            using (JsonDocument document = JsonDocument.Parse(str, options)) {
               
                price = document.RootElement
                    .GetProperty("data")
                    .GetProperty("quote")
                    .GetProperty("USD")
                    .GetProperty("price")
                    .ToString();
            }
            double x = double.Parse(price);

            Console.WriteLine(x);

            Console.ReadKey();
        }

        

        static string ApiConverterTool(string firstCurrency = "BTC", string secondCurrency = "USD", double amount = 1) {
            string converterUrl = "https://pro-api.coinmarketcap.com/v1/tools/price-conversion";
            UriBuilder url = new UriBuilder(converterUrl);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = firstCurrency;
            queryString["amount"] = amount.ToString();
            queryString["convert"] = secondCurrency;

            url.Query = queryString.ToString();

            WebClient client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", "2977f70f-3ac7-4a5e-833c-7ea88278f25c");
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(url.ToString());
        }
    }
}
