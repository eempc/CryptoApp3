using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CryptoAddress.Data;
using CryptoAddress.Models;
using System.Linq;
using Xamarin.Essentials;

namespace CryptoAddress {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        // To populate the upper app elements, it should only require UserAddress and fiat currency at first
        UserAddress currentUserAddress;
        string currentUserFiat;

        // The pickers will require the lists but it goes into the init and accesses the static classes
        
        // I will also need to instantiate a PriceFeed to retrieve exchange rates

            // Constructor will be used for initialisation stuff
        public MainPage() {
            InitializeComponent();
            UserAddressDatabase.CreateDatabase(); // Initial creation

            // The following are sequestered away in a partial class 
            // that is all about the initial loadup
            SetFiatPicker();
            SetUserAddress();                       
        }

        // Populate the header, title, address and barcode of the XAML
        private void SetAddressDetails() {
            LabelHeader.Text = currentUserAddress.Name;
            LabelTitle.Text = currentUserAddress.GetCryptoFullName();
            LabelAddress.Text = currentUserAddress.Address;
            BarcodeImageView.BarcodeValue = currentUserAddress.Address;            
            Preferences.Set("last_used_id", (int)currentUserAddress.Id);
        }

        // Do you or do you not limit event handlers to a single task? Having seen the previous version, the answer is that it would be less messy this way
        private void PickerFiatCurrencySelect_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedItem = PickerFiatCurrencySelect.SelectedItem.ToString();
            Preferences.Set("user_fiat_currency", selectedItem);
            currentUserFiat = selectedItem;
            SetFiatCharacterSymbol();
        }

        private void SetFiatCharacterSymbol() {
            LabelFiatCurrencyCharacter.Text = FiatCurrencyList.GetCharacterSymbol(currentUserFiat);
        }
    }

    // Loading startup methods go in here
    public partial class MainPage : ContentPage {
        //Fiat picker stuff
        public void SetFiatPicker() {          
            PickerFiatCurrencySelect.ItemsSource = FiatCurrencyList.GetSymbolsList().OrderBy(c => c).ToList();
            
            string currentUserFiat = Preferences.Get("user_fiat_currency", null);

            if (!string.IsNullOrEmpty(currentUserFiat)) {
                int startIndex = PickerFiatCurrencySelect.ItemsSource.IndexOf(currentUserFiat);
                PickerFiatCurrencySelect.SelectedIndex = startIndex;
                SetFiatCharacterSymbol();
            }
        }

        //User Address init
        public void SetUserAddress() {
            int lastId = Preferences.Get("last_used_id", 1);
            currentUserAddress = UserAddressDatabase.GetUserAddressById(lastId);
            SetAddressDetails();
        }
    }
}
