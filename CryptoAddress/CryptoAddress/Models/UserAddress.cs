using SQLite;
using CryptoAddress.Data;

namespace CryptoAddress.Models {
    [Table("UserAddress")]
    public class UserAddress : IUserAddress {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; } // Column("_id") is optional
        [MaxLength(64), Unique]
        public string Name { get; set; } // E.g. "My Ethereum Address"
        [MaxLength(256)]
        public string Address { get; set; } // E.g. "0x667273242..."
        [MaxLength(6)]
        public string CryptoSymbol { get; set; } // E.g. "ETH"

        public string GetCryptoImageFileName(string fileType = ".png") {
            // Image file check could go in here before return
            return CryptoSymbol + fileType;
        }

        public string GetCryptoFullName() {
            return CryptocurrencyList.GetSingleFullName(CryptoSymbol);
        }
    }
}
