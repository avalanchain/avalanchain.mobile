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
        public Confirmation (Send send)
		{
			InitializeComponent ();
		    BindingContext = new ConfirmationViewModel(send);
        }

	    private async void Confirm(object sender, EventArgs eventArgs)
	    {
	        {

	            ViewModel.ExecuteSend();

	            //await Navigation.PopAsync();
	            await Navigation.PopToRootAsync();
	        }
	    }
    }
}