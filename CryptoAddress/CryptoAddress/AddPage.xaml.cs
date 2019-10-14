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

        private void ButtonOkay_Clicked(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(EntryName.Text)) {
                EntryName.Placeholder = "*** A name is required ***";
            }

            if (string.IsNullOrEmpty(EntryAddress.Text)) {
                EntryAddress.Placeholder = "*** An address is required ***";
            }

            if (PickerCryptocurrency.SelectedItem == null) {
                PickerCryptocurrency.Title = "*** Required ***";
            }

            //UserAddress newUserAddress = new UserAddress();
            //newUserAddress.Name = EntryName.Text;
            //newUserAddress.Address = EntryAddress.Text;
            //newUserAddress.CryptoSymbol = PickerCryptocurrency.SelectedItem.ToString();



        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();
        }
    }
}