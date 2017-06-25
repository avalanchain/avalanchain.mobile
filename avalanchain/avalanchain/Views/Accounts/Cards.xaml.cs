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
    public partial class Cards : ContentPage
    {
        public Cards()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //    tapCount++;
            var labelSender = (Label)sender;
            NavigateToAddCardPage(labelSender.ClassId);
            //// watch the monkey go from color to black&white!
            //if (tapCount % 2 == 0) {
            //    imageSender.Source = "tapped.jpg";
            //} else {
            //    imageSender.Source = "tapped_bw.jpg";
            //}
        }

        public async void NavigateToAddCardPage(string type)
        {
            await Navigation.PushAsync(new AddCard(type));
        }
    }
}