using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAddress.Models {
    class Cryptocurrency : Currency {
        public string ExternalUrl { get; set; }
        public Cryptocurrency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacter, string externalUrl) : base(symbolCode, fullName, unitNames, symbolCharacter) {
            ExternalUrl = externalUrl;
        }

        public Cryptocurrency() {
        }    
    }
}
