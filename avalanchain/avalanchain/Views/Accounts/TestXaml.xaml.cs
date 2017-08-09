using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace avalanchain
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestXaml : ContentPage
	{
	    ZXingBarcodeImageView barcode;
	    private WalletViewModel ViewModel => BindingContext as WalletViewModel;
        public TestXaml ()
		{
			InitializeComponent ();
		    BindingContext = new WalletViewModel(SampleData.Wallets[0]);

            InitData();
		}

	    public void InitData()
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
	        barcode.BarcodeOptions.Margin = 10;
	        barcode.BarcodeValue = ViewModel.Barcode.BarcodeValue;
            //barcode.BarcodeValue = "Hi there!";

            try
	        {
	            StackBarcode.Children.Add(barcode);
                //BarcodeIm = barcode;
            }
	        catch (Exception e)
	        {
	            var er = e;
	        }
	    }
	}
}