using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAddress.Models {
    class FiatCurrency : Currency {
        public FiatCurrency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacter) : base(symbolCode, fullName, unitNames, symbolCharacter) {
        }

        public FiatCurrency() {

        }
    }
}
