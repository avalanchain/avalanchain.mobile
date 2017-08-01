//using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    public class BottomTabPage : TabbedPage
    {
        public BottomTabPage()
        {
            ////BarTheme = BarThemeTypes.Light;
            //string[] tabTitles = { "Accounts", "Dashboard", "Welcome" };
            //string[] tabColors = {null,
            //    "#c7413e", "#5D4037", "#7B1FA2", "#FF5252", "#FF9800" };
            ////int[] tabBadgeCounts = { 0, 1, 5, 3, 4 };
            ////string[] tabBadgeColors = { "#000000", "#FF0000", "#000000", "#000000", "#000000" };

            //for (int i = 0; i < tabTitles.Length; ++i)// tabTitles.Length
            //{
            //    string title = tabTitles[i];
            //    string tabColor = tabColors[i];
            //    //int tabBadgeCount = tabBadgeCounts[i];
            //    //string tabBadgeColor = tabBadgeColors[i];

            //    //FileImageSource icon = (FileImageSource)FileImageSource.FromFile(string.Format("ic_{0}.png", title.ToLowerInvariant()));

            //    // create tab page
            //    var tabPage = new NavigationPage();
            //    var tabPage1 = new ContentPage();
            //    if (i == 0)
            //    {
            //        var page = new Accounts();
            //        //NavigationPage.SetHasNavigationBar(page, false);
            //        tabPage = new NavigationPage(page)
            //        {
            //            Title = title,
            //            Icon = "tabs0_icon.png"//icon
            //        };
                    
            //    }
            //    else if (i == 1)
            //    {
            //        var page = new DashboardFlatPage();
            //        //NavigationPage.SetHasNavigationBar(page, false);
            //        tabPage = new NavigationPage(page)
            //        {
            //            Title = title,
            //            Icon = "tabs0_icon.png"//icon
            //        };
                    
            //    }
            //    else if (i == 2)
            //    {
            //        var page = new WelcomePage();
            //        //NavigationPage.SetHasNavigationBar(page, false);
            //        tabPage = new NavigationPage(page)
            //        {
            //            Title = title,
            //            Icon = "tabs0_icon.png"//icon
            //        };
            //    }

            //    // set tab color
            //    if (tabColor != null)
            //    {
            //        BottomBarPageExtensions.SetTabColor(tabPage, Color.FromHex(tabColor));
            //    }
            //    //var navigationPage = new NavigationPage(tabPage);
            //    // Set badges
            //    //BottomBarPageExtensions.SetBadgeCount(tabPage, tabBadgeCount);
            //    //BottomBarPageExtensions.SetBadgeColor(tabPage, Color.FromHex(tabBadgeColor));

            //    // set label based on title
            //    //tabPage.UpdateLabel();

            //    // add tab pag to tab control
            //    //if (i == 1)
            //    //{
            //    //    Children.Add(tabPage1);
            //    //}
            //    //else { Children.Add(tabPage); }
            //    Children.Add(tabPage);
            //}
        }
    }
}
