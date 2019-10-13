using CryptoAddress.Models;
using System.Collections.Generic;

namespace CryptoAddress.Data {
    class FiatCurrencyList {
        public static Dictionary<string, FiatCurrency> currencyList = new Dictionary<string, FiatCurrency>();

        public static void InitiateCurrencies() {
            currencyList.Add("USD",
                new FiatCurrency(
                    "USD", "US dollar",
                    new Dictionary<int, string>() { { 0, "dollar" }, { 2, "cent" }, { 4, "pip" } },
                    '$', '¢'
                    ));
            currencyList.Add("EUR",
                new FiatCurrency(
                    "EUR", "Euro",
                    new Dictionary<int, string>() { { 0, "euro" }, { 2, "cent" }, { 4, "pip" } },
                    '€', '¢'
                    ));
            currencyList.Add("GBP",
                new FiatCurrency(
                    "GBP", "British pound",
                    new Dictionary<int, string>() { { 0, "pound" }, { 2, "pence" }, { 4, "pip" } },
                    '£', 'p'
                    ));
        }

        public static List<string> GetFullNamesList() {
            if (currencyList.Count <= 0) InitiateCurrencies();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, FiatCurrency> entry in currencyList) {
                list.Add(entry.Value.FullName);
            }
            return list;
        }

        public static List<string> GetSymbolsList() {
            if (currencyList.Count <= 0) InitiateCurrencies();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, FiatCurrency> entry in currencyList) {
                list.Add(entry.Key);
            }
            return list;
        }

        public static string GetCharacterSymbol(string symbol) {
            if (currencyList.Count <= 0) InitiateCurrencies();
            return currencyList[symbol].SymbolCharacterMajor.ToString();
        }
    }
}
