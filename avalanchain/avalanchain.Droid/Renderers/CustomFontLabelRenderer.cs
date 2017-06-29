using System;
using avalanchain.Droid;
using Android.Graphics;
using Android.Widget;
using UXDivers.Artina.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


//[assembly: ExportRenderer(typeof(Label), typeof(AwesomeLabelRenderer))]
namespace avalanchain.Droid
{
	public class CustomFontLabelRenderer : ArtinaCustomFontLabelRenderer
	{
        //TODO: Delete grialfonts 
		private static readonly string[] CustomFontFamily = new [] { "Lato","grialshapes", "FontAwesome", "Ionicons" };//"grialshapes", "FontAwesome", "Ionicons" 
		private static readonly Tuple<FontAttributes, string>[][] CustomFontFamilyData = new [] {
            new [] {
                new Tuple<FontAttributes, string>(FontAttributes.None, "LatoLatin-Regular.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Bold, "LatoLatin-Semibold.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Italic, "LatoLatin-Regular.ttf")
            },

            new [] {
                new Tuple<FontAttributes, string>(FontAttributes.None, "grialshapes.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Bold, "grialshapes.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Italic, "grialshapes.ttf")
            },

            new [] {
                new Tuple<FontAttributes, string>(FontAttributes.None, "fontawesome-webfont.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Bold, "fontawesome-webfont.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Italic, "fontawesome-webfont.ttf")
            },



            new [] {
                new Tuple<FontAttributes, string>(FontAttributes.None, "ionicons.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Bold, "ionicons.ttf"),
                new Tuple<FontAttributes, string>(FontAttributes.Italic, "ionicons.ttf")
            }

        };
			
		protected override bool CheckIfCustomFont (string fontFamily, FontAttributes attributes, out string fontFileName)
		{
			for (int i = 0; i < CustomFontFamily.Length; i++) {
				if (string.Equals(fontFamily, CustomFontFamily[i], StringComparison.InvariantCulture)){
					var fontFamilyData = CustomFontFamilyData[i];

					for (int j = 0; j < fontFamilyData.Length; j++) {
						var data = fontFamilyData[j];
						if (data.Item1 == attributes){
							fontFileName = data.Item2;

							return true;
						}
					}

					break;
				}
			}

			fontFileName = null;
			return false;
		}
	}

    //public class AwesomeLabelRenderer : LabelRenderer
    //{
    //    protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
    //    {
    //        base.OnElementChanged(e);

    //        AwesomeUtil.CheckAndSetTypeFace(Control);
    //    }
    //}
    //internal static class AwesomeUtil
    //{
    //    public static void CheckAndSetTypeFace(TextView view)
    //    {
    //        if (view.Text.Length == 0) return;
    //        var text = view.Text;
    //        if (text.Length > 1 || text[0] < 0xf000)
    //        {
    //            return;
    //        }

    //        var font = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fontawesome-webfont.ttf");
    //        //var  font = Typeface.CreateFromAsset(getContext().getAssets(), "fontawesome-webfont.ttf");
    //        view.Typeface = font;
    //    }
    //}
}

