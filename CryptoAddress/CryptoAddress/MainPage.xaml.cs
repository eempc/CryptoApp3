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
using System.Threading;

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
                OnPropertyChanged(nameof(CurrentUserAddress)); // Required for data binding
                if (CurrentFiat != null) SetExchangeRate();
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
                if (CurrentUserAddress != null) SetExchangeRate();
            }
        }
        
        private Tuple<double, DateTime> exchangeRate;
        public Tuple<double, DateTime> ExchangeRate {
            get { return exchangeRate; }
            set {
                exchangeRate = value;
                OnPropertyChanged(nameof(ExchangeRate));
            }
        }

        List<UserAddress> addresses; //For wallet area
        double fiatAmount;

        // Constructor will be used for initialisation stuff
        public MainPage() {
            InitializeComponent();
            UserAddressDatabase.CreateDatabase(); // Initial creation, I don't like this here, should I sequester it away in the database class?
            //Initial Load up sequence                          
            SetFiatAmount();
            SetUserAddress();
            SetFiatPicker();
            SetExchangeRate();
            SetWalletArea();
            SetTimer();
            UpdateCryptoAmountCalculation();
            BindingContext = this;
        }




        // Do you or do you not limit event handlers to a single task? Having seen the previous version, the answer is that it would be less messy this way
        private void PickerFiatCurrencySelect_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedItem = PickerFiatCurrencySelect.SelectedItem.ToString();
            Preferences.Set("user_fiat_currency", selectedItem);
            CurrentFiat = FiatCurrencyList.currencyList[selectedItem];
            UpdateCryptoAmountCalculation();
        }

        private async void ButtonAddAddress_Clicked(object sender, EventArgs e) {
            AddPage addPage = new AddPage();
            await Navigation.PushModalAsync(addPage);
        }

        private void ImageButton_Clicked(object sender, EventArgs e) {
            string senderId = ((ImageButton)sender).ClassId;
            if (int.TryParse(senderId, out int number)) {
                CurrentUserAddress = UserAddressDatabase.GetUserAddressById(number);
                Preferences.Set("last_used_id", number);
                UpdateCryptoAmountCalculation();
            }
        }

        private void EntryFiatCurrencyAmount_TextChanged(object sender, TextChangedEventArgs e) {
            if (double.TryParse(EntryFiatCurrencyAmount.Text, out double amount) && !Double.IsNaN(amount) && amount > 0) {
                Preferences.Set("last_fiat_amount", amount);
                fiatAmount = amount;
                UpdateCryptoAmountCalculation();
            }
        }

        // This will not be data bound because it is the final calculation so it is quicker to use .Text = ...
        private void UpdateCryptoAmountCalculation() {
            LabelCryptocurrencyAmount.Text = (fiatAmount / ExchangeRate.Item1).ToString("0.####");
        }
    }

    // Loading startup methods go in here
    public partial class MainPage : ContentPage {
        // Fiat amount
        private void SetFiatAmount() {
            fiatAmount = Preferences.Get("last_fiat_amount", 100.00);
        }

        //Fiat picker stuff
        private void SetFiatPicker() {          
            PickerFiatCurrencySelect.ItemsSource = FiatCurrencyList.GetSymbolsList().OrderBy(c => c).ToList();
            
            string currentUserFiat = Preferences.Get("user_fiat_currency", "USD");

            if (!string.IsNullOrEmpty(currentUserFiat)) {
                int startIndex = PickerFiatCurrencySelect.ItemsSource.IndexOf(currentUserFiat);
                PickerFiatCurrencySelect.SelectedIndex = startIndex;
                CurrentFiat = FiatCurrencyList.currencyList[currentUserFiat];
            }
        }

        //User Address init
        private void SetUserAddress() {
            int lastId = Preferences.Get("last_used_id", 1);
            CurrentUserAddress = UserAddressDatabase.GetUserAddressById(lastId);            
        }

        protected override void OnAppearing() {
            SetWalletArea();
        }

        // Init wallet area       
        private void SetWalletArea() {
            addresses = UserAddressDatabase.GetAllUserAddresses();
            BindableLayout.SetItemsSource(WalletArea, addresses);
        }
        private void SetTimer() {
            Device.StartTimer(TimeSpan.FromSeconds(30), () => {
                SetExchangeRate();
                UpdateCryptoAmountCalculation();
                return true;
            });
        }

        private void SetExchangeRate() {
            ExchangeRate = PriceFeed.GetSingleExchangeRate(CurrentUserAddress.CryptoSymbol, CurrentFiat.SymbolCode);
        }
    }
}
