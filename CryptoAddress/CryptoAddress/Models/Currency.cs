using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoAddress.Models {
    public abstract class Currency : ICurrency {
        public string SymbolCode { get; set; } // E.g. ETH
        public string FullName { get; set; } // E.g. Ethereum
        public Dictionary<int, string> UnitNames { get; set; } // E.g. Key/Value { 18, "wei" }
        public char SymbolCharacterMajor { get; set; } // E.g. E

        public Currency(string symbolCode, string fullName, Dictionary<int, string> unitNames, char symbolCharacterMajor) {
            SymbolCode = symbolCode;
            FullName = fullName;
            UnitNames = unitNames;
            SymbolCharacterMajor = symbolCharacterMajor;
        }

        public Currency() {
        }

        public string GetImageFileName(string fileType = ".png") {
            return SymbolCode + fileType;
        }

        public string GetMainUnits() {
            if (UnitNames[0] != null) {
                return UnitNames[0];
            }
            throw new NullReferenceException();
        }

        public int GetDecimalPlaces() {
            if (UnitNames.Count() > 0) {
                return UnitNames.Keys.Max();
            }
            throw new NullReferenceException();
        }
    }
}
