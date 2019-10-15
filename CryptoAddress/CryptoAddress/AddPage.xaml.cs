using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CryptoAddress.Data;
using CryptoAddress.Models;

namespace CryptoAddress {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage {
        public AddPage() {
            InitializeComponent();
            PickerCryptocurrency.ItemsSource = CryptocurrencyList.GetSymbolsList();
        }

        private async void ButtonOkay_Clicked(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(EntryName.Text) && !string.IsNullOrEmpty(EntryAddress.Text) && PickerCryptocurrency.SelectedItem != null) {
                UserAddress newUserAddress = new UserAddress();
                newUserAddress.Name = EntryName.Text;
                newUserAddress.Address = EntryAddress.Text;
                newUserAddress.CryptoSymbol = PickerCryptocurrency.SelectedItem.ToString();
                UserAddressDatabase.Save(newUserAddress);
                await Navigation.PopModalAsync();
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();
        }
    }
}