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
    public partial class Confirmation : ContentPage
    {

        private ConfirmationViewModel ViewModel => BindingContext as ConfirmationViewModel;
        public Confirmation(Send send)
        {
            InitializeComponent();
            BindingContext = new ConfirmationViewModel(send);
        }

        private async void Confirm(object sender, EventArgs eventArgs)
        {
            ViewModel.ExecuteSend();
            var isTransactionSuccess = true;
            //await Navigation.PopAsync();
            if (isTransactionSuccess)
            {
                await Navigation.PopToRootAsync();
            }
            ShowAlert(isTransactionSuccess);

        }
        public async void ShowAlert(bool isTransactionSuccess)
        {
            if (isTransactionSuccess)
            {
                await DisplayAlert("Success!", "Transaction execute successfully :)", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "Something goes wrong please try again", "OK");
            }
        }
    }
}