using CryptoAddress.Models;
using System.Collections.Generic;

namespace CryptoAddress.Data {
    class CryptocurrencyList {
        public static Dictionary<string, ICurrency> currencyList = new Dictionary<string, ICurrency>();

        public static void InitiateCurrencies() {
            currencyList.Add("ETH",
                new Cryptocurrency(
                    "ETH", "Ethereum",
                    new Dictionary<int, string>() { { 0, "ether" }, { 18, "wei" } },
                    'Ξ',
                    @"https://etherscan.io/address/"
                    ));
            currencyList.Add("BTC",
                new Cryptocurrency(
                    "BTC", "Bitcoin",
                    new Dictionary<int, string>() { { 0, "bitcoin" }, { 8, "satoshi" } },
                    '₿',
                    @"https://etherscan.io/address/"
                    ));
            currencyList.Add("LTC",
                new Cryptocurrency(
                    "LTC", "Litecoin",
                    new Dictionary<int, string>() { { 0, "litecoin" }, { 8, "litoshi" } },
                    'Ł',
                    @"https://etherscan.io/address/"
                    ));
            currencyList.Add("XMR",
                new Cryptocurrency(
                    "XMR", "Monero",
                    new Dictionary<int, string>() { { 0, "monero" }, { 12, "piconero" } },
                    'ɱ',
                    @"https://etherscan.io/address/"
                    ));
            currencyList.Add("XRP",
                new Cryptocurrency(
                    "XRP", "Ripple",
                    new Dictionary<int, string>() { { 0, "ripple" }, { 6, "drop" } },
                    'x',
                    @"https://etherscan.io/address/"
                    ));
        }

        public static List<string> GetFullNamesList() {
            if (currencyList.Count <= 0) InitiateCurrencies();

            List<string> list = new List<string>();
            foreach (KeyValuePair<string, ICurrency> entry in currencyList) {
                list.Add(entry.Value.FullName);
            }
            return list;
        }

        public static List<string> GetSymbolsList() {
            if (currencyList.Count <= 0) InitiateCurrencies();

            List<string> list = new List<string>();
            foreach (KeyValuePair<string, ICurrency> entry in currencyList) {
                list.Add(entry.Key);
            }
            return list;
        }

        public static string GetSingleFullName(string cryptoSymbol) {
            if (currencyList.Count <= 0) InitiateCurrencies();
            return currencyList[cryptoSymbol].FullName;
        }
    }
}
