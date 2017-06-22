using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace avalanchain
{
	public partial class DocumentTimelinePage : ContentPage
	{
		public DocumentTimelinePage()
		{
			InitializeComponent ();
			BindingContext = new DocumentTimelineViewModel();
		}
	}
}

