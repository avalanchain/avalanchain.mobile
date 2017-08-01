using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    public class CreditCardEntryPage : ContentPage
    {
        public delegate void ScanSucceededEventHandler(object sender, CreditCard_PCL ccPCL);

        public event ScanSucceededEventHandler ScanSucceeded;
        public event EventHandler ScanCancelled;

        public Card cardIOConfig;

        public CreditCardEntryPage(Card config)
        {
            cardIOConfig = config;
        }

        public void OnScanSucceeded(CreditCard_PCL ccPCL)
        {
            if (ScanSucceeded != null)
            {
                ScanSucceeded(this, ccPCL);
            }
        }

        public void OnScanCancelled()
        {
            if (ScanCancelled != null)
            {
                ScanCancelled(this, EventArgs.Empty);
            }
        }
    }
}
