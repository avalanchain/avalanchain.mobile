namespace avalanchain
{
    public class Card : Account
    {
        public bool RequireExpiry { get; set; } = true;
        public bool RequireCvv { get; set; } = true;
        public bool RequirePostalCode { get; set; } = false;
        public bool ShowPaypalLogo { get; set; } = false;
        public bool HideCardIOLogo { get; set; } = true;
        public bool CollectCardholderName { get; set; } = true;
        public string ScanInstructions { get; set; }
        public string Localization { get; set; }

        public Card()
        {
            Type = AccountType.Card;
        }

    }
}
