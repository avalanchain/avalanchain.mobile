using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    class Common
    {
    }
    public class CurrencyPricing
    {
        public string BTC { get; set; }
        public string ETH { get; set; }
        public string EUR { get; set; }
        public string GBP { get; set; }
        public string USD { get; set; }
    }
    public enum Currency
    {
        USD, GBP, EUR
    }

    public class Send
    {
        public string FromAccountNumber { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public decimal FromAmount { get; set; }
        public decimal ReceiveAmount { get; set; }
        public CurrencyType CurrencyReceive { get; set; }
    }
}
