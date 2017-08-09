using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transfer : ContentPage
    {
        public Transfer(Account account, bool isFrom)
        {
            InitializeComponent();
            BindingContext = new TransferViewModel(account, isFrom);
        }
        public Transfer()
        {
            InitializeComponent();
            BindingContext = new TransferViewModel();
        }
        private TransferViewModel ViewModel => BindingContext as TransferViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimateIn();
        }

        private void Send(object sender, EventArgs eventArgs)
        {
            if (ViewModel.From.AccountNumber == ViewModel.Send.ReceiverAccountNumber && ViewModel.IsOwnResiever)
            {
                //var mes = "You can't send money to the same Account"
            }
            else if(string.IsNullOrEmpty(ViewModel.Send.ReceiverAccountNumber) && ViewModel.IsAnotherResiever)
            {
                //var mes = "Please, enter number account in field 'To'"
            }
            else
            {
                var send = GetFullSend();
                NavigateToEventPage(send);
            }
        }

        public async void NavigateToEventPage(Send send)
        {
            await Navigation.PushAsync(new Confirmation(send));
        }
        private Send GetFullSend()
        {
            ViewModel.Send.ReceiverId = ViewModel.IsOwnResiever ? ViewModel.To.Id.ToString() : "";
            ViewModel.Send.ReceiverAccountNumber = ViewModel.IsOwnResiever ? ViewModel.To.AccountNumber : ViewModel.Send.ReceiverAccountNumber;
            ViewModel.Send.FromAccountNumber = ViewModel.From.AccountNumber;
            ViewModel.Send.CurrencyReceive = ViewModel.IsOwnResiever ? ViewModel.To.Currency : ViewModel.From.Currency;
            return ViewModel.Send;
        }

        void OnStackLayoutTapped(object sender, EventArgs args)
        {
            ViewModel.ChangeReseiverAccount(null);
        }
        void OnLabelTapped(object sender, EventArgs args)
        {
            GetAccounslist(false);
        }
        void OnLabelFromTapped(object sender, EventArgs args)
        {
            GetAccounslist(true);
        }
        private async void GetAccounslist(bool isFrom)
        {
            var accountsList = new AccountsList();
            if (isFrom)
            {
                accountsList.Disappearing += OnAccountsListFromDisappearing;
            }
            else
            {
                accountsList.Disappearing += OnAccountsListToDisappearing;
            }
            await Navigation.PushModalAsync(accountsList);
        }

        public void TextChanged(object sender, TextChangedEventArgs e)
        {
            //var oldText = e.OldTextValue;
            decimal newText;
            if (e.NewTextValue != null)
            {
                try
                {
                    newText = Decimal.Parse(e.NewTextValue);
                    ViewModel.Send.FromAmount = newText;
                }
                catch (Exception ex)
                {
                    var err = ex;
                    newText = ViewModel.Send.FromAmount;
                }
                ViewModel.ChangeReceiveAmount(newText);
            }

        }

        private void OnAccountsListToDisappearing(object sender, EventArgs eventArgs)
        {
            ChangeAccount(sender, false);
        }
        private void OnAccountsListFromDisappearing(object sender, EventArgs eventArgs)
        {
            ChangeAccount(sender, true);
        }

        private void ChangeAccount(object sender, bool isFrom)
        {
            var accountsList = (AccountsList)sender;
            if (!accountsList.IsClosed)
            {
                var account = accountsList.SelectedAaccount;
                if (isFrom)
                {
                    ViewModel.ChangeFromAccount(account);
                    ((AccountsList)sender).Disappearing -= OnAccountsListFromDisappearing;
                }
                else
                {
                    ViewModel.ChangeReseiverAccount(account);
                    ((AccountsList)sender).Disappearing -= OnAccountsListToDisappearing;
                }

            }
        }

        public void ShowMessages(bool isShow, string message)
        {
            
        }


        public async Task AnimateIn()
        {
            await Task.WhenAll(new[] {
                AnimationService.AnimateItem (CircleImage1, 500),
                AnimationService.AnimateItem (CircleImage2, 500),
                AnimationService.AnimateItem (FromCurrencyIcon, 700),
                AnimationService.AnimateItem (ToCurrencyIcon, 700)
            });
        }
    }
}