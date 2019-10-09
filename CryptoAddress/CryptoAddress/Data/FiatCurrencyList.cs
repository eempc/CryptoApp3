using CryptoAddress.Models;
using System.Collections.Generic;

namespace CryptoAddress.Data {
    class FiatCurrencyList {
        public static Dictionary<string, ICurrency> currencyList = new Dictionary<string, ICurrency>();

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
            currencyList.Add("AUD",
                new FiatCurrency(
                    "AUD", "Australian dollar",
                    new Dictionary<int, string>() { { 0, "euro" }, { 2, "cent" }, { 4, "pip" } }, 
                    '$', '¢'
                    ));
            currencyList.Add("CAD",
                new FiatCurrency(
                    "CAD", "Canadian dollar",
                    new Dictionary<int, string>() { { 0, "euro" }, { 2, "cent" }, { 4, "pip" } }, 
                    '$', '¢'
                    ));
        }

        public static List<string> GetFullNamesList() {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, ICurrency> entry in currencyList) {
                list.Add(entry.Value.FullName);
            }
            return list;
        }

        public static List<string> GetSymbolsList() {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, ICurrency> entry in currencyList) {
                list.Add(entry.Key);
            }
            return list;
        }
    }
}
