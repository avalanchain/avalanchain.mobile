using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace avalanchain
{
	public partial class SocialGalleryImage : ContentView
	{
		public static BindableProperty ImageProperty =
			BindableProperty.Create (
				nameof( Image ), 
				typeof ( ImageSource ),
				typeof ( SocialGalleryImage ),
				null,
				defaultBindingMode	: BindingMode.OneWay
			);

		public ImageSource Image {
			get { return ( ImageSource )GetValue( ImageProperty ); }
			set { SetValue ( ImageProperty, value ); }
		}

		public SocialGalleryImage ()
		{			
			InitializeComponent ();
		}

		private async void OnImageTapped(Object sender, EventArgs e){
			var selectedItem = (SocialGalleryImage) sender;
			var socialGalleryImagePreview =  new SocialGalleryImagePreviewPage ( selectedItem.Image );

			await Navigation.PushModalAsync(NavigationPageHelper.Create(socialGalleryImagePreview));
		}
	}
}

