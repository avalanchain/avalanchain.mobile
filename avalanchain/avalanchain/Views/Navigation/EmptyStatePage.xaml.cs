using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace avalanchain
{
	public partial class EmptyStatePage : ContentPage
	{
		public EmptyStatePage()
		{
			InitializeComponent();
			BindingContext = this;
		}

		public async void OnCloseButtonClicked (object sender, EventArgs args)
		{
			await Navigation.PopAsync ();
		}
	}
}

