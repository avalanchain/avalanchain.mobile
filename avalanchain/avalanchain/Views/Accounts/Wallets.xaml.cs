using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wallets : ContentPage
    {
        private WalletsViewModel ViewModel => BindingContext as WalletsViewModel;
        public Wallets()
        {
            InitializeComponent();
            BindingContext = new WalletsViewModel();
            //SetupEventHandlers();
        }


        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var gridSender = (Grid)sender;
            NavigateToEventPage(gridSender.ClassId);
        }
        private async void OnWalletTapped(Object sender, ItemTappedEventArgs e)
        {
            var account = (CryptocurrencyWallet)((ListView)sender).SelectedItem;

            var accountView = new AccountDetail(account);

            await Navigation.PushAsync(accountView);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteFetchWalletsCommand();
        }

        //private async void CryptocurrencyPricing(Object sender, ItemTappedEventArgs e)
        //{
        //    var account = (CryptocurrencyWallet)((ListView)sender).SelectedItem;

        //    var accountView = new AccountDetail(account);

        //    await Navigation.PushAsync(accountView);
        //}

        public async void NavigateToEventPage(string type)
        {
            await Navigation.PushAsync(new Transfer());
        }
    }
}