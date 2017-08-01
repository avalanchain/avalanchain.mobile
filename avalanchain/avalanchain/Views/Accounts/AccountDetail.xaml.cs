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
    public partial class AccountDetail : ContentPage
    {
        private AccountDetailViewModel ViewModel => BindingContext as AccountDetailViewModel;
        public AccountDetail(Account account)
        {
            InitializeComponent();
            BindingContext = new AccountDetailViewModel(account);//SampleData.Accounts[0];
        }

        private async void OnTransactionTapped(Object sender, ItemTappedEventArgs e)
        {
            var transaction = (Transaction)((ListView)sender).SelectedItem;

            var transactionView = new TransactionDetail(transaction);

            await Navigation.PushAsync(transactionView);
        }

        public async void TransferTapped(Object sender, EventArgs eventArgs)
        {
            var transferView = new Transfer();

            if (ViewModel.Account != null)
            {
                transferView = new Transfer(ViewModel.Account);
            }
            
            await Navigation.PushAsync(transferView);
        }

    }
    
}