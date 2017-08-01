using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCard : ContentPage
    {
        //Label txtCreditCardNumber;
        public AddCard()
        {
            InitializeComponent();
            BindingContext = this;
            MessagingCenter.Subscribe<CreditCard_PCL>(this, "CreditCardScanSuccess", (sender) => {
                // Do something whenever the "iOSCreditCardReceived" message is sent.
                // We could fill in CCV and expiration date things here, whatever else we need.
                // This is enough to show capability, however.
                txtCreditCardNumber.Text = sender.redactedCardNumber;

                Navigation.PopModalAsync();

            });

            MessagingCenter.Subscribe<CreditCard_PCL>(this, "CreditCardScanCancelled", (sender) => {
                // Do something whenever the "CreditCardCancelled" message is sent.
                Navigation.PopModalAsync();

            });
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //    tapCount++;
            var labelSender = (Label)sender;
            NavigateToOrderCardPage(labelSender.ClassId);
            //// watch the monkey go from color to black&white!
            //if (tapCount % 2 == 0) {
            //    imageSender.Source = "tapped.jpg";
            //} else {
            //    imageSender.Source = "tapped_bw.jpg";
            //}
        }

        public async void NavigateToOrderCardPage(string type)
        {
            await Navigation.PushAsync(new OrderCard(type));
        }

        private void AddCardButton_Clicked(object sender, EventArgs e)
        {
            Card card = new Card();
            var ccPage = new CreditCardEntryPage(card);

            // Not really called - Can't get reference to the CreditCardEntryPage right in Android.
            ccPage.ScanCancelled += HandleScanCancelled;
            ccPage.ScanSucceeded += HandleScanSucceeded;

            Navigation.PushModalAsync(ccPage);

        }
        private async void HandleScanSucceeded(object sender, CreditCard_PCL ccPCL)
        {
            //txtCreditCardNumber.Text = ccPCL.cardNumber;
            //await Navigation.PopModalAsync();
        }

        // not implemented, as I'm not sure how to finish this out in Android. 
        private async void HandleScanCancelled(object sender, EventArgs e)
        {
            //await Navigation.PopModalAsync();
        }
       
    }
   
}