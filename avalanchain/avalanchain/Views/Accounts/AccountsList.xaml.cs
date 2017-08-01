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
    public partial class AccountsList : ContentPage
    {
        private AccountsViewModel ViewModel => BindingContext as AccountsViewModel;
        public Account SelectedAaccount = new Account();
        public bool IsClosed = false;
        public AccountsList()
        {
            InitializeComponent();
            BindingContext = new AccountsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteFetchAccountsCommand();
        }

        private async void OnAccountTapped(Object sender, ItemTappedEventArgs e)
        {
            SelectedAaccount = (Account)((ListView)sender).SelectedItem;

            await Navigation.PopModalAsync();
        }

        private async void OnCloseTapped(Object sender, ItemTappedEventArgs e)
        {
            IsClosed = true;

            await Navigation.PopModalAsync();
        }
    }
}