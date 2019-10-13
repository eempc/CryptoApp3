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
    public partial class MainPage : ContentPage, INotifyPropertyChanged {
        // Global variables to populate the upper app elements, it should only require UserAddress and fiat currency at first
        private UserAddress currentUserAddress;
        public UserAddress CurrentUserAddress {
            get { 
                return currentUserAddress; 
            }
            set {
                currentUserAddress = value;
                OnPropertyChanged(nameof(CurrentUserAddress));
            }
        }

        private FiatCurrency currentFiat;
        public FiatCurrency CurrentFiat {
            get {
                return currentFiat;
            }
            set {
                currentFiat = value;
                OnPropertyChanged(nameof(CurrentFiat));
            }
        }
        Tuple<double, DateTime> exchangeRate;

        //public string testStr { get; set; }
       
        // I will also need to instantiate a PriceFeed to retrieve exchange rates

        // Constructor will be used for initialisation stuff
        public MainPage() {
            InitializeComponent();
            UserAddressDatabase.CreateDatabase(); // Initial creation, I don't like this here, should I sequester it away in the database class?
            
            // The following are sequestered away in a partial class that is all about the initial loadup
            SetFiatPicker();           
            SetUserAddress();
            SetWalletArea();
            SetExchangeRate();

            BindingContext = this;
        }

        // Populate the header, title, address and barcode of the XAML
        //private void DisplayAddressDetails() {
        //    LabelHeader.Text = currentUserAddress.Name;
        //    LabelTitle.Text = currentUserAddress.GetCryptoFullName();
        //    LabelAddress.Text = currentUserAddress.Address;
        //    BarcodeImageView.BarcodeValue = currentUserAddress.Address;            
        //    Preferences.Set("last_used_id", (int)currentUserAddress.Id);
        //}

        // Do you or do you not limit event handlers to a single task? Having seen the previous version, the answer is that it would be less messy this way
        private void PickerFiatCurrencySelect_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedItem = PickerFiatCurrencySelect.SelectedItem.ToString();
            Preferences.Set("user_fiat_currency", selectedItem);
            CurrentFiat = FiatCurrencyList.currencyList[selectedItem];
            //DisplayFiatCharacterSymbol();
        }

        private void DisplayFiatCharacterSymbol() => LabelFiatCurrencyCharacter.Text = CurrentFiat.SymbolCharacterMajor.ToString();

        private void ButtonAddAddress_Clicked(object sender, EventArgs e) {
            //testStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //LabelAddress.Text = testStr;
        }
    }

    // Loading startup methods go in here
    public partial class MainPage : ContentPage {
        //Fiat picker stuff
        private void SetFiatPicker() {          
            PickerFiatCurrencySelect.ItemsSource = FiatCurrencyList.GetSymbolsList().OrderBy(c => c).ToList();
            
            string currentUserFiat = Preferences.Get("user_fiat_currency", "USD");

            if (!string.IsNullOrEmpty(currentUserFiat)) {
                int startIndex = PickerFiatCurrencySelect.ItemsSource.IndexOf(currentUserFiat);
                PickerFiatCurrencySelect.SelectedIndex = startIndex;
                //DisplayFiatCharacterSymbol();
                CurrentFiat = FiatCurrencyList.currencyList[currentUserFiat];
            }
        }

        //User Address init
        private void SetUserAddress() {
            int lastId = Preferences.Get("last_used_id", 1);
            CurrentUserAddress = UserAddressDatabase.GetUserAddressById(lastId);
            //DisplayAddressDetails();
            
        }

        // Init wallet area       
        private void SetWalletArea() {
            List<UserAddress> addresses = UserAddressDatabase.ReadAll();
            BindableLayout.SetItemsSource(WalletArea, addresses);
        }

        // Get the first rate on start up
        private void SetExchangeRate() {
            exchangeRate = PriceFeed.GetSingleExchangeRate(CurrentUserAddress.CryptoSymbol, CurrentFiat.SymbolCode);
            LabelExchangeRate.Text = CurrentFiat.SymbolCharacterMajor + " " + exchangeRate.Item1.ToString("0.##");
            LabelUpdateDateTime.Text = exchangeRate.Item2.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
