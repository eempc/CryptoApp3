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
        string currentFiat;

        // The pickers will require the lists but it goes into the init and accesses the static classes
        
        // I will also need to instantiate a PriceFeed to retrieve exchange rates

            // Constructor will be used for initialisation stuff
        public MainPage() {
            InitializeComponent();
            UserAddressDatabase.CreateDatabase(); // Initial creation

            //Fiat Picker stuff
            PickerFiatCurrencySelect.ItemsSource = FiatCurrencyList.GetSymbolsList().OrderBy(c => c).ToList();

            //LoadAddressView();

        }

        public void LoadAddressView() {
            // Get the last user address by id
            int lastId = Preferences.Get("current_db_id", 1);
            if (lastId != 1) currentUserAddress = UserAddressDatabase.GetUserAddressById(lastId);
            else currentUserAddress = UserAddressDatabase.GetUserAddressById(1);


        }

    }

    public partial class MainPage : ContentPage {
        
    }
}
