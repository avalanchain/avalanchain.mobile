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
               "ACCOUNTS",
               new SampleCategory
               {
                   Name = "Accounts",
                   BackgroundColor = Color.FromHex(_categoriesColors[0]),
                   BackgroundImage = SampleData.DashboardImagesList[6],
                   Icon = GrialShapesFont.Person,
                   Badge = 2,
                   SamplesList = new List<Sample> {
                       new Sample("Accounts", typeof(Accounts), SampleData.DashboardImagesList[6], FontAwesome.FAListAlt, false, false),
                       new Sample("Wallets", typeof(Wallets), SampleData.DashboardImagesList[6], FontAwesome.FABtc, false, false),
                       new Sample("Cards", typeof(Cards), SampleData.DashboardImagesList[6], FontAwesome.FACreditCard2, false, false),
                        //new Sample("BTCWallet", typeof(BTCWallet), SampleData.DashboardImagesList[6], FontAwesome.FABtc, false, false),
                        //new Sample("Add Card", typeof(AddCard), SampleData.DashboardImagesList[5], FontAwesome.FACreditCard, false, false),
                   }
               }
           );
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
                        //new Sample("Buy", typeof(Buy), SampleData.DashboardImagesList[6], FontAwesome.FAArrowCircleOUp, false, false),
                        //new Sample("Sell", typeof(Buy), SampleData.DashboardImagesList[6], FontAwesome.FAArrowCircleODown, false, false),
                        new Sample("Transfer", typeof(Transfer), SampleData.DashboardImagesList[6], FontAwesome.FAExchange, false, false),
#if DEBUG
                        new Sample("Dashboard", typeof(Dashboard), SampleData.DashboardImagesList[3], FontAwesome.FAFaChart),
#endif
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

                        new Sample("Messages", typeof(MessagesListPage), SampleData.DashboardImagesList[8], FontAwesome.FAEnvelope),
                        new Sample("Bot", typeof(ChatMessagesListPage), SampleData.DashboardImagesList[8], FontAwesome.FARocket),
                        //new Sample("Chat Messages List", typeof(ChatMessagesListPage), SampleData.DashboardImagesList[8], FontAwesome.FACommentsO),

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
                        new Sample("Settings", typeof(SettingsPage), SampleData.DashboardImagesList[6], FontAwesome.FACog),
                        //new Sample("Profile", typeof(ProfilePage), SampleData.DashboardImagesList[6], FontAwesome.FAUser),
                        new Sample("Profile", typeof(SocialVariantPage), SampleData.DashboardImagesList[6], FontAwesome.FAUser),

                    }
                }
            );

#if DEBUG

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
                        new Sample("Dashboard", typeof(DashboardFlatPage), SampleData.DashboardImagesList[3], FontAwesome.FATh),
                        //new Sample("Images Dashboard", typeof(DashboardWithImagesPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false),

                    }
                }
            );
#endif


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

    public class SampleGroup : List<Sample>
    {
        private readonly string _name;

        public SampleGroup(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}