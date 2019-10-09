using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAddress.Models {
    interface ICurrency {
        string SymbolCode { get; set; }
        string FullName { get; set; }
        Dictionary<int, string> UnitNames { get; set; }
        char SymbolCharacter { get; set; }

        string GetMainUnits();
        string GetImageFileName(string fileType);
    }
}
