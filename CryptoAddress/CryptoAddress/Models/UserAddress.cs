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
       
        public string ImageFileUrl { get; set; } // This should be automatically derived from the symbol, e.g. ImageFileUrl = cryptoSymbol + ".png" but it doesn't work

        public UserAddress() {

        }

        public UserAddress(string name, string address, string cryptoSymbol, string imageFileUrl) {            
            Name = name;
            Address = address;
            CryptoSymbol = cryptoSymbol;
            ImageFileUrl = imageFileUrl;
        }

        public string GetCryptoFullName() {
            return CryptocurrencyList.GetSingleFullName(CryptoSymbol);
        }
    }
}
