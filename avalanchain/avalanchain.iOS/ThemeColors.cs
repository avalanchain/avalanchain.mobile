using System.Collections.Generic;
using Xamarin.Forms;
using UXDivers.Artina.Shared;

namespace avalanchain.iOS
 {
	internal class ThemeColors : ThemeColorsBase
	{
		private readonly static Dictionary<string, Color> _themeColors = new Dictionary<string, Color>
		{
			{ "AccentColor", Color.FromHex("#C7413E") },
			{ "BaseTextColor", Color.FromHex("#2f2f33") },
			{ "InverseTextColor", Color.White },
			{ "BrandColor", Color.FromHex("#C7413E") },
			{ "BrandNameColor", Color.FromHex("#FFFFFF") },
			{ "BaseLightTextColor", Color.FromHex("#7b7b7b") },
			{ "OverImageTextColor", Color.FromHex("#FFFFFF") },
			{ "EcommercePromoTextColor", Color.FromHex("#FFFFFF") },
			{ "SocialHeaderTextColor", Color.FromHex("#2f2f33") },
			{ "ArticleHeaderBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "CustomNavBarTextColor", Color.FromHex("#FFFFFF") },
			{ "ListViewItemTextColor", Color.FromHex("#2f2f33") },
			{ "AboutHeaderBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "SplashColor", Color.FromHex("#FFFFFF") },
			{ "BasePageColor", Color.FromHex("#FFFFFF") },
			{ "BaseTabbedPageColor", Color.FromHex("#FFFFFF") },
			{ "MainWrapperBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "CategoriesListIconColor", Color.FromHex("#55000000") },
			{ "DashboardIconColor", Color.FromHex("#FFFFFF") },
			{ "ComplementColor", Color.FromHex("#525ABB") },
			{ "TranslucidBlack", Color.FromHex("#44000000") },
			{ "TranslucidWhite", Color.FromHex("#22ffffff") },
			{ "OkColor", Color.FromHex("#22c064") },
			{ "ErrorColor", Color.Red },
			{ "WarningColor", Color.FromHex("#ffc107") },
			{ "NotificationColor", Color.FromHex("#1274d1") },
			{ "Circle2Color", Color.FromHex("#413ec7") },
			{ "Circle3Color", Color.FromHex("#3ec741") },
			{ "CircleColor", Color.FromHex("#a3322f") },
			{ "CircleColorWhite", Color.FromHex("#ffffff") },
			{ "SaveButtonColor", Color.FromHex("#22c064") },
			{ "DeleteButtonColor", Color.FromHex("#D50000") },
			{ "LabelButtonColor", Color.FromHex("#ffffff") },
			{ "PlaceholderColor", Color.FromHex("#22ffffff") },
			{ "PlaceholderColorEntry", Color.FromHex("#FFFFFF") },
			{ "RoundedLabelBackgroundColor", Color.FromHex("#413ec7") },
			{ "MainMenuHeaderBackgroundColor", Color.FromHex("#ffffff") },
			{ "MainMenuBackgroundColor", Color.FromHex("#c7413e") },
			{ "MainMenuSeparatorColor", Color.Transparent },
			{ "MainMenuTextColor", Color.FromHex("#ffffff") },
			{ "MainMenuHeaderTextColor", Color.FromHex("#eec6c5") },
			{ "MainMenuIconColor", Color.FromHex("#ffffff") },
			{ "TransactionBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "TransactionHeaderBackgroundColor", Color.FromHex("#ffffff") },
			{ "TransactionTextColor", Color.FromHex("#2f2f33") },
			{ "ListViewSeparatorColor", Color.FromHex("#D3D3D3") },
			{ "BaseSeparatorColor", Color.FromHex("#7b7b7b") },
			{ "ChatRightBalloonBackgroundColor", Color.FromHex("#525ABB") },
			{ "ChatBalloonFooterTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatRightTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatLeftTextColor", Color.FromHex("#FFFFFF") },
			{ "CardColorEntry", Color.FromHex("#c7413e") }
		};
		public ThemeColors() : base(_themeColors) {}
	}
}
