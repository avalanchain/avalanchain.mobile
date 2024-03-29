using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace avalanchain
{
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{			
			InitializeComponent();

			BindingContext = new DashboardViewModel ();
		}

		public async void NavigateToGrialSettingsPage() { 
			await Navigation.PushAsync(new GrialDemoSettings() );
		}
	}
}