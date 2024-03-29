using System;
using System.Collections.Generic;
using System.Reflection;

using Xamarin.Forms;

namespace avalanchain
{
	public partial class IconsPage : ContentPage
	{

		public IconsPage()
		{
			InitializeComponent();

			BindingContext = new IconsFontDetails
			{
                IconsFontFamily = nameof(GrialShapesFont),
                //IconsFontFamily = nameof(FontAwesome),
                IconsCount = LoadIcons().Count,
				IconsList = LoadIcons()
			};
		}

		private static List<IconsFont> LoadIcons()
		{
			var result = new List<IconsFont>();
            var type = typeof(GrialShapesFont);
            //var type = typeof(FontAwesome);
            //var type = typeof(IoniciconsFont);

            foreach (var field in type.GetRuntimeFields())
			{
				if (field.IsLiteral && !field.IsInitOnly)
				{
					result.Add(new IconsFont() { 
						Name = field.Name, 
						Icon = (string)field.GetValue(type)
					});
				}
			}

			return result;
		}


		class IconsFont
		{
			public string Name
			{
				get;
				set;
			}

			public string Icon
			{
				get;
				set;
			}
		}

		class IconsFontDetails
		{
			public string IconsFontFamily
			{
				get;
				set;
			}

			public List<IconsFont> IconsList
			{
				get;
				set;
			}

			public int IconsCount
			{
				get;
				set;
			}
		}
	}
}
