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
        public Accounts()
        {
            InitializeComponent();
            //GetAccountsLists(SampleData.Accounts);
            BindingContext = new SamplesViewModel(Navigation);
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
            //await Navigation.PushAsync(new AddCard(type));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //tapGestureRecognizer.Tapped += OnBannerTapped;
            EcommerceProductGridBanner.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //tapGestureRecognizer.Tapped -= OnBannerTapped;
            EcommerceProductGridBanner.GestureRecognizers.Remove(tapGestureRecognizer);
        }

        //private void GetAccountsLists(List<Account> accounts)
        //{
        //    var lastHeight = "100";
        //    var y = 0;
        //    var column = LeftColumn;
        //    var productTapGestureRecognizer = new TapGestureRecognizer();
        //    productTapGestureRecognizer.Tapped += OnProductTapped;

        //    for (var i = 0; i < accounts.Count; i++)
        //    {
        //        var item = new AccountGridItemTemplate();

        //        if (i % 2 == 0)
        //        {
        //            column = LeftColumn;
        //            y++;
        //        }
        //        else
        //        {

        //            column = RightColumn;
        //        }

        //        accounts[i].ThumbnailHeight = lastHeight;
        //        item.BindingContext = accounts[i];
        //        item.GestureRecognizers.Add(productTapGestureRecognizer);
        //        column.Children.Add(item);
        //    }
        //}


        private async void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem;
            var sampleCategory = (SampleCategory)selectedItem;

            await SamplesListFromCategoryPage.NavigateToCategory(sampleCategory, Navigation);
        }
        private async void OnProductTapped(Object sender, EventArgs e)
        {
            var selectedItem = (Account)((AccountGridItemTemplate)sender).BindingContext;

            var productView = new AccountDetail()
            {
                BindingContext = selectedItem
            };

            await Navigation.PushAsync(productView);
        }
    }
}