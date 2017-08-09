using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string FromSubName => From.Substring(Math.Max(0, From.Length - 4));
        public string To { get; set; }
        public string ToSubName => To.Substring(Math.Max(0, To.Length - 4));
        public CurrencyType Currency { get; set; }
        public CurrencyType CurrencyReceive { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal AmountReceive { get; set; }
        //public decimal AccountBalanceAfterTransaction { get; set; }
        public string AmountFullDetails => Type != TransactionType.Receive ? "-" + Amount : Amount.ToString();
        public string CurrencyIcon { get; set; }
        public string CurrencyReceiveIcon { get; set; }
        public string TypeIcon { get; set; }
        public TransactionType Type { get; set; }
        public DateTime DateTime { get; set; }
    }
    
    public enum TransactionType
    {
        Sent, Receive, Pay
    }

    public class TransactionGroup : List<Transaction>
    {
        public TransactionGroup(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
    }
}
