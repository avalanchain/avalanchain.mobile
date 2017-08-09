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

        public async void TransferTapped(object sender, EventArgs eventArgs)
        {
            if (ViewModel.Account != null)
            {
                var transferView = new Transfer(ViewModel.Account, true);
                await Navigation.PushAsync(transferView);
            }
            else
            {
                var transferView = new Transfer();
                await Navigation.PushAsync(transferView);
            }
            
            
        }

    }
    
}