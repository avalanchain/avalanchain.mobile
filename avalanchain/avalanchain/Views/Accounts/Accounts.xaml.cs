using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Accounts : ContentPage
    {
        private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        private AccountsViewModel ViewModel => BindingContext as AccountsViewModel;
        public Accounts()
        {
            InitializeComponent();
            //GetAccountsLists(SampleData.Accounts);
            BindingContext = new AccountsViewModel();//new SamplesViewModel(Navigation);
        }
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //    tapCount++;
            var gridSender = (Grid)sender;
            NavigateToEventPage(gridSender.ClassId);
            //}
        }

        public async void NavigateToEventPage(string type)
        {
            await Navigation.PushAsync(new Transfer());
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteFetchAccountsCommand();

            //tapGestureRecognizer.Tapped += OnBannerTapped;
            EcommerceProductGridBanner.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //tapGestureRecognizer.Tapped -= OnBannerTapped;
            EcommerceProductGridBanner.GestureRecognizers.Remove(tapGestureRecognizer);
        }

        private async void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem;
            var sampleCategory = (SampleCategory)selectedItem;

            await SamplesListFromCategoryPage.NavigateToCategory(sampleCategory, Navigation);
        }
        private async void OnAccountTapped(Object sender, ItemTappedEventArgs e)
        {
            var account = (Account)((ListView)sender).SelectedItem;
            if (account.Currency == CurrencyType.BTC)
            {
                var wallet = (CryptocurrencyWallet)account;
                var accountView = new WalletDetail(wallet);

                await Navigation.PushAsync(accountView);
            }
            else
            {
                var accountView = new AccountDetail(account);

                await Navigation.PushAsync(accountView);
            }

        }

    }
}