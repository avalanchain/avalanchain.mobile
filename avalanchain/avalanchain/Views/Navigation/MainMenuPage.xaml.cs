using System;
using avalanchain;
using Xamarin.Forms;

namespace avalanchain
{
	public partial class MainMenuPage : ContentPage
	{
		private readonly INavigation _navigation;

		public MainMenuPage (INavigation navigation)
		{
			InitializeComponent ();

			_navigation = navigation;

			BindingContext = new ACViewModel(navigation);
		}

		public async void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			var sample = sampleListView.SelectedItem as Sample;

			if (sample != null) {
				if (sample.PageType == typeof(RootPage)) {
					await DisplayAlert ("Hey", string.Format ("You are already here, on sample {0}.", sample.Name), "OK");
				} else {
					await sample.NavigateToSample (_navigation);
				}

				sampleListView.SelectedItem = null;
			}
		}
			
        private async void OnCloseButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

		public void OnBtnCustomClicked()
		{
			var uri = "http://avalanchain.com/";
			Device.OpenUri(new Uri(uri));
		}
	}
}