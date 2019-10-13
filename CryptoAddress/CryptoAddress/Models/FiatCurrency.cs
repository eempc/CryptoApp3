using System.Collections.Generic;

namespace CryptoAddress.Models {
    public class FiatCurrency : Currency {
        public char SymbolCharacterMinor { get; set; } //E.g. c for cent
        public FiatCurrency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacterMajor, char symbolCharacterMinor) : 
            base(symbolCode, fullName, unitNames, symbolCharacterMajor) {
            SymbolCharacterMinor = symbolCharacterMinor;
        }

        public FiatCurrency() {
        }
    }
}
