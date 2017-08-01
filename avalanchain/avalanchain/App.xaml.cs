using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
	//[XamlCompilation (XamlCompilationOptions.Skip)]
	public partial class App : Application
	{
		public static MasterDetailPage MasterDetailPage;

		public App()
		{
			InitializeComponent();

            //MainPage = GetPage();//GetMainPage();
            MainPage = GetMainPage();

            //MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
        }

        //public Page GetPage()
        //{
        //    string[] tabTitles = { "Accounts", "Analytics", "Transfers", "Cards", "Settings" };//"Transfers",
        //    string[] tabColors = { "#c7413e", "#5D4037", "#7B1FA2", "#FF5252", "#FF9800" };
        //    string[] tabIcons = { "th_list.png", "pie_chart.png", "exchange.png", "credit_card.png", "sliders.png", };// "exchange.png",
        //    //string[] tabBadgeColors = { "#000000", "#FF0000", "#000000", "#000000", "#000000" };
        //    //var tabbedPage = new MainPage();
        //    var tabbedPage = new BottomTabPage();
        //    for (int i = 0; i < tabTitles.Length; ++i)
        //    {
        //        string title = tabTitles[i];
        //        string tabColor = tabColors[i];
        //        string icon = tabIcons[i];
        //        tabbedPage.Children.Add(CreateTabPage(i, title, icon, tabColor));

        //    }
        //    var page = new NavigationPage(tabbedPage);
        //    NavigationPage.SetHasNavigationBar(page, false);
        //    return page;
        //}

        //private Page CreateTabPage(int index, string title, string icon, string color)
        //{
        //    var pages = new List<Page>()
        //    {
        //        new Accounts(), new Dashboard(), new Buy(), new DashboardFlatPage(), new SettingsPage()//, new Buy()
        //    };
        //    //var button = new Button { Text = "Push" };
        //    //{ Content = button2 });
        //    //button2.Clicked += (s1, e2) => navigationPage.PopAsync()
        //    //var label = new Label
        //    //{
        //    //    Text = "Page " + index,
        //    //    HorizontalOptions = LayoutOptions.CenterAndExpand,
        //    //    VerticalOptions = LayoutOptions.CenterAndExpand
        //    //};

        //    //var page = new BaseContentPage
        //    //{
        //    //    Content = button,
        //    //};
        //    //var isshow = false;
        //    //var page = index==0 ? pages[index] : new ContentPage();

        //    //page.Icon = icon;
        //    //page.Title = title;

        //    NavigationPage.SetHasNavigationBar(pages[index], false);
        //    var navigationPage = new NavigationPage(pages[index])
        //    {
        //        Icon = icon,
        //        //Title = title,
        //    };

        //    //Page pag = index==0 ? page : navigationPage;
        //    //button.Clicked += (s, e) => navigationPage.PushAsync(new ContentPage());
        //    //BottomBarPageExtensions.SetTabColor(navigationPage, Color.FromHex(color));
        //    return navigationPage;
        //}

        public static Page GetMainPage()
		{
			//return new MainPage();
			return new RootPage(true);
		}
	}
}
