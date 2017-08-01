using System;

using System.Configuration;
using System.Collections.Specialized;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Threading.Tasks;

using UXDivers.Artina.Shared;
using UXDivers.Artina.Shared.Droid;

using FFImageLoading.Forms.Droid;
using Syncfusion.SfChart.XForms.Droid;
using Com.Testfairy;
using Card.IO;

namespace avalanchain.Droid
{
    //https://developer.android.com/guide/topics/manifest/activity-element.html
    [Activity(
        Label = "Avalanchain",
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
         MainLauncher = true,
        LaunchMode = LaunchMode.SingleTask,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
        )
    ]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // Changing to App's theme since we are OnCreate and we are ready to 
            // "hide" the splash
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.AppTheme);


            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabs;

            base.OnCreate(bundle);
#if (!DEBUG)
            TestFairy.Begin(this, "885a88cfad8a6e2d4f3c8a09d8b4a9f1d8a48522");
#endif

            //Initializing FFImageLoading
            CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            UXDivers.Artina.Shared.GrialKit.Init(this, "avalanchain.Droid.GrialLicense");



            FormsHelper.ForceLoadingAssemblyContainingType(typeof(UXDivers.Effects.Effects));

            //string sAttr;

            // Read a particular key from the config file            
            //sAttr = Configuration.ConfigurationManager.AppSettings.Get("Debug");
            //sAttr = Configuration.ConfigurationManager.AppSettings.Get("Deploy");

#if DEBUG
            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
            //this,
            //new UXDivers.Gorilla.Config("Good Gorilla").RegisterAssemblyFromType<UXDivers.Artina.Shared.CircleImage>()));

            LoadApplication(new App());
#endif

#if (!DEBUG)
            LoadApplication(new App());
#endif
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            DeviceOrientationLocator.NotifyOrientationChanged();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);


            // Feel free to extend the CreditCard_PCL object to include more than what's here.
            CreditCard_PCL ccPCL = new CreditCard_PCL();

            if (data != null)
            {

                // Be sure to JavaCast to a CreditCard (normal cast won't work)		 
                var card = data.GetParcelableExtra(CardIOActivity.ExtraScanResult).JavaCast<CreditCard>();

                Console.WriteLine("Scanned: " + card.RedactedCardNumber);

                ccPCL.cardNumber = card.CardNumber;
                ccPCL.ccv = card.Cvv;
                ccPCL.expr = card.ExpiryMonth.ToString() + card.ExpiryYear.ToString();
                ccPCL.redactedCardNumber = card.RedactedCardNumber;
                ccPCL.cardholderName = card.CardholderName;

                Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanSuccess");

            }
            else
            {
                Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");
            }

        }

    }

}

