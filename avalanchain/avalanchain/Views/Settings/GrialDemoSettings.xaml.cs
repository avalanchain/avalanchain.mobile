using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace avalanchain
{
	public partial class GrialDemoSettings : ContentPage
	{
		public GrialDemoSettings()
		{
			InitializeComponent();
		}

		public void OnBtnLightClicked()
		{
			Application.Current.Resources.MergedWith = typeof(avalanchain.GrialLightTheme);
		}

		public void OnBtnDarkClicked()
		{
			Application.Current.Resources.MergedWith = typeof(avalanchain.GrialDarkTheme);
		}

		public void OnBtnEnterpriseClicked()
		{
			Application.Current.Resources.MergedWith = typeof(avalanchain.GrialEnterpriseTheme);
		}

		public void OnBtnCustomClicked()
		{
			//var uri = "mailto:hello@grialkit.com?subject=I%20want%20a%20custom%20theme%20for%20my%20Grial%20app&body=Please%20leave%20us%20your%20comments.";
			var uri = "http://grialkit.com/getquote";
			Device.OpenUri(new Uri(uri));
		}
	}
}
