using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAddress.Models {
    public abstract class Currency : ICurrency {
        public Currency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacter) {
            SymbolCode = symbolCode;
            FullName = fullName;
            UnitNames = unitNames;
            SymbolCharacter = symbolCharacter;
        }

        public Currency() {
        }

        public string SymbolCode { get; set; } // E.g. ETH
        public string FullName { get; set; } // E.g. Ethereum
        public Dictionary<int, string> UnitNames { get; set; }
        public char SymbolCharacter { get; set; }

        public string GetImageFileName(string fileType = ".png") {
            return SymbolCode + fileType;
        }

        public string GetMainUnits() {
            if (UnitNames[0] != null) {
                return UnitNames[0];
            }
            throw new NullReferenceException();
        }
    }
}
