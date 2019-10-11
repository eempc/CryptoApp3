namespace CryptoAddress.Models {
    // For use with the UserAddress model
    interface IUserAddress {
        int? Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        string CryptoSymbol { get; set; }
        string ImageFileUrl { get; set; }
    }
}
