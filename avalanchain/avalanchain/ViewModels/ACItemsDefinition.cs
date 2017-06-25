using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using avalanchain.Views.Accounts;
using Xamarin.Forms;

namespace avalanchain
{
    public class ACItemsDefinition : ContentPage
    {
        private static List<SampleCategory> _samplesCategoryList;
        private static Dictionary<string, SampleCategory> _samplesCategories;
        private static List<Sample> _allSamples;
        private static List<SampleGroup> _samplesGroupedByCategory;

        public static string[] _categoriesColors = {
            "#921243",
            "#B31250",
            "#CD195E",
            "#56329A",
            "#6A40B9",
            "#7C4ECD",
            "#525ABB",
            "#5F7DD4",
            "#7B96E5"
        };

        public static List<SampleCategory> SamplesCategoryList
        {
            get
            {
                if (_samplesCategoryList == null)
                {
                    InitializeSamples();
                }

                return _samplesCategoryList;
            }
        }

        public static Dictionary<string, SampleCategory> SamplesCategories
        {
            get
            {
                if (_samplesCategories == null)
                {
                    InitializeSamples();
                }

                return _samplesCategories;
            }
        }

        public static List<Sample> AllSamples
        {
            get
            {
                if (_allSamples == null)
                {
                    InitializeSamples();
                }
                return _allSamples;
            }
        }

       
        public static List<SampleGroup> SamplesGroupedByCategory
        {
            get
            {
                if (_samplesGroupedByCategory == null)
                {
                    InitializeSamples();
                }

                return _samplesGroupedByCategory;
            }
        }

        internal static Dictionary<string, SampleCategory> CreateSamples()
        {
            var categories = new Dictionary<string, SampleCategory>();

            categories.Add(
               "TRADE",
               new SampleCategory
               {
                   Name = "Trade",
                   BackgroundColor = Color.FromHex(_categoriesColors[0]),
                   BackgroundImage = SampleData.DashboardImagesList[6],
                   Icon = GrialShapesFont.Person,
                   Badge = 2,
                   SamplesList = new List<Sample> {
                        new SampleT("Buy", typeof(Buy), SampleData.DashboardImagesList[6], GrialShapesFont.OutlineThinCircle, GrialShapesFont.ArrowUp, false, false),
                        new SampleT("Sell", typeof(Buy), SampleData.DashboardImagesList[6], GrialShapesFont.OutlineThinCircle, GrialShapesFont.ArrowDown, false, false),
                        new Sample("Transfer", typeof(Buy), SampleData.DashboardImagesList[6], GrialShapesFont.Loop, false, false),
                        new Sample("Dashboard", typeof(Dashboard), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard),

                   }
               }
           );

            categories.Add(
               "ACCOUNTS",
               new SampleCategory
               {
                   Name = "Accounts",
                   BackgroundColor = Color.FromHex(_categoriesColors[0]),
                   BackgroundImage = SampleData.DashboardImagesList[6],
                   Icon = GrialShapesFont.Person,
                   Badge = 2,
                   SamplesList = new List<Sample> {
                        new Sample("Cards", typeof(Cards), SampleData.DashboardImagesList[6], GrialShapesFont.CreditCard, false, false),
                        new Sample("BTCWallet", typeof(BTCWallet), SampleData.DashboardImagesList[6], GrialShapesFont.WebAsset, false, false),
                        new Sample("Add Card", typeof(ProductOrder), SampleData.DashboardImagesList[5], GrialShapesFont.CreditCard, false, true),
                   }
               }
           );

            categories.Add(
                "MESSAGES",
                new SampleCategory
                {
                    Name = "Messages",
                    BackgroundColor = Color.FromHex(_categoriesColors[7]),
                    BackgroundImage = SampleData.DashboardImagesList[8],
                    Badge = 2,
                    Icon = GrialShapesFont.Email,
                    SamplesList = new List<Sample> {

                        new Sample("Messages", typeof(MessagesListPage), SampleData.DashboardImagesList[8], GrialShapesFont.Email),
                        new Sample("Bot", typeof(ChatMessagesListPage), SampleData.DashboardImagesList[8], GrialShapesFont.Email),
                        new Sample("Chat Messages List", typeof(ChatMessagesListPage), SampleData.DashboardImagesList[8], GrialShapesFont.Forum),

                    }
                }
            );
            categories.Add(
                "SETTINGS",
                new SampleCategory
                {
                    Name = "Settings",
                    BackgroundColor = Color.FromHex(_categoriesColors[0]),
                    BackgroundImage = SampleData.DashboardImagesList[6],
                    Icon = GrialShapesFont.Person,
                    Badge = 2,
                    SamplesList = new List<Sample> {
                        new Sample("Profile", typeof(ProfilePage), SampleData.DashboardImagesList[6], GrialShapesFont.AccountCircle),
                        new Sample("Social", typeof(SocialPage), SampleData.DashboardImagesList[6], GrialShapesFont.Group),
                        new Sample("Social Variant", typeof(SocialVariantPage), SampleData.DashboardImagesList[6], GrialShapesFont.Group),

                    }
                }
            );

            //categories.Add(
            //    "ARTICLES",
            //    new SampleCategory
            //    {
            //        Name = "Articles",
            //        BackgroundColor = Color.FromHex(_categoriesColors[1]),
            //        BackgroundImage = SampleData.DashboardImagesList[4],
            //        Icon = GrialShapesFont.InsertFile,
            //        Badge = 2,
            //        SamplesList = new List<Sample> {
            //            new Sample("Articles Classic View", typeof(ArticlesClassicViewPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile, false, true),
            //            new Sample("Front Page News", typeof(FrontPageNewsPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile, false, true),

            //            new Sample("Article View", typeof(ArticleViewPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
            //            new Sample("Articles List", typeof(ArticlesListPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
            //            new Sample("Articles List Variant", typeof(ArticlesListVariantPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
            //            new Sample("Articles Feed", typeof(ArticlesFeedPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),

            //        }
            //    }
            //);

            categories.Add(
                "DASHBOARD",
                new SampleCategory
                {
                    Name = "Dashboards",
                    BackgroundColor = Color.FromHex(_categoriesColors[2]),
                    BackgroundImage = SampleData.DashboardImagesList[3],
                    Badge = 5,
                    Icon = GrialShapesFont.Dashboard,
                    SamplesList = new List<Sample> {

                        //new Sample("Icons Dashboard", typeof(DashboardPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard),
                        new Sample("Dashboard", typeof(DashboardFlatPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard),
                        //new Sample("Images Dashboard", typeof(DashboardWithImagesPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false),

                    }
                }
            );

            

            return categories;
        }

        internal static void InitializeSamples()
        {
            _samplesCategories = CreateSamples();

            _samplesCategoryList = new List<SampleCategory>();

            foreach (var sample in _samplesCategories.Values)
            {
                _samplesCategoryList.Add(sample);
            }

            _allSamples = new List<Sample>();

            _samplesGroupedByCategory = new List<SampleGroup>();

            foreach (var sampleCategory in SamplesCategories.Values)
            {

                var sampleItem = new SampleGroup(sampleCategory.Name.ToUpper());

                foreach (var sample in sampleCategory.SamplesList)
                {
                    _allSamples.Add(sample);
                    sampleItem.Add(sample);
                }

                _samplesGroupedByCategory.Add(sampleItem);
            }
        }

       private static void RootPageCustomNavigation(INavigation navigation)
        {
            SampleCoordinator.RaisePresentMainMenuOnAppearance();
            navigation.PopToRootAsync();
        }
    }
}