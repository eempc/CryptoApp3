﻿using System.Collections.Generic;

namespace CryptoAddress.Models {
    class Cryptocurrency : Currency {
        public string ExternalUrl { get; set; }
        public Cryptocurrency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacterMajor, string externalUrl) : 
            base(symbolCode, fullName, unitNames, symbolCharacterMajor) {
            ExternalUrl = externalUrl;
        }

        public Cryptocurrency() {
        }    
    }
}
