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
    public partial class Cards : ContentPage
    {
        private CardsViewModel ViewModel => BindingContext as CardsViewModel;
        public Cards()
        {
            InitializeComponent();
            BindingContext = new CardsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteFetchCardsCommand();
        }
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var gridSender = (Grid)sender;
            NavigateToEventPage(gridSender.ClassId);
        }

        public async void NavigateToEventPage(string type)
        {
            var topUp = "TopUp";
            if (type == topUp)
            {
                await Navigation.PushAsync(new AddCard());
            }
            else
            {
                await Navigation.PushAsync(new Transfer());
            }

        }

        private async void OnCardTapped(Object sender, ItemTappedEventArgs e)
        {
            var account = (Card)((ListView)sender).SelectedItem;

            var accountView = new AccountDetail(account);

            await Navigation.PushAsync(accountView);
        }
    }
}