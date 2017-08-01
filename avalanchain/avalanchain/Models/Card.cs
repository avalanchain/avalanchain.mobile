using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    public class Card : Account
    {
        public bool RequireExpiry { get; set; }
        public bool RequireCvv { get; set; }
        public bool RequirePostalCode { get; set; }
        public bool ShowPaypalLogo { get; set; }
        public bool HideCardIOLogo { get; set; }
        public bool CollectCardholderName { get; set; }
        public string ScanInstructions { get; set; }
        public string Localization { get; set; }

        public Card()
        {
            this.Type = AccountType.Card;
            this.ShowPaypalLogo = false;
            this.RequireExpiry = true;
            this.RequireCvv = true;
            this.RequirePostalCode = false;
            this.HideCardIOLogo = true;
            this.CollectCardholderName = true;
        }

    }
}
