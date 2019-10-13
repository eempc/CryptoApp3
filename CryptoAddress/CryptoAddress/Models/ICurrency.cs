using System.Collections.Generic;

namespace CryptoAddress.Models {
    interface ICurrency {
        string SymbolCode { get; set; }
        string FullName { get; set; }
        Dictionary<int, string> UnitNames { get; set; }
        char SymbolCharacterMajor { get; set; }

        string GetMainUnits();
        string ImageFileUrl { get; }
    }
}
