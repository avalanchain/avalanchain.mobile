using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace avalanchain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletDetail : ContentPage
    {
        bool SupportsClipboard { get; }
        ZXingBarcodeImageView barcode;
        //ZXingBarcodeImageView barcode;
        private WalletViewModel ViewModel => BindingContext as WalletViewModel;
        public WalletDetail(CryptocurrencyWallet wallet)
        {
            InitializeComponent();
            BindingContext = new WalletViewModel(wallet);//SampleData.Accounts[0];
            InitData();


        }
        private void InitData()
        {
            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView",
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeValue = ViewModel.Barcode.BarcodeValue;

            try
            {
                StackBarcode.Children.Add(barcode);
            }
            catch (Exception e)
            {
                var er = e;
            }

        }
        private async void OnTransactionTapped(Object sender, ItemTappedEventArgs e)
        {
            var transaction = (Transaction)((ListView)sender).SelectedItem;

            var transactionView = new TransactionDetail(transaction);

            await Navigation.PushAsync(transactionView);
        }

        public async void TransferTapped(object sender, EventArgs eventArgs)
        {
            if (ViewModel.Wallet != null)
            {
                var transferView = new Transfer(ViewModel.Wallet, true);
                await Navigation.PushAsync(transferView);
            }
            else
            {
                var transferView = new Transfer();
                await Navigation.PushAsync(transferView);
            }


        }

        public async void CopyToClipboard(object sender, EventArgs eventArgs)
        {
            var isCopied = await CommonService.CopyToClipboard(ViewModel.Wallet.AccountNumber);
            if (isCopied)
            {
                await DisplayAlert("Success!", "Copy to clipboard successfully :)", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "You can't copy to clipbord :(", "OK");
            }
        }

    }
}