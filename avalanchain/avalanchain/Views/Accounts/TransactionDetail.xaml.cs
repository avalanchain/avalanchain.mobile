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
	public partial class TransactionDetail : ContentPage
	{
		public TransactionDetail (Transaction transaction)
		{
			InitializeComponent ();
            BindingContext = transaction;// new TransactionDetailViewModel(transaction);
        }
	}
}