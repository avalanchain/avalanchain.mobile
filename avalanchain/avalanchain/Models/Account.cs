using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    public class Account
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumberSubName => AccountNumber.Substring(Math.Max(0, AccountNumber.Length - 4)).ToUpper();

        public CurrencyType Currency { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyIcon { get; set; }
        public string TypeIcon { get; set; }

        public Account()
        {
            Type = AccountType.Account;
        }
    }

    public enum AccountType
    {
        Card, VirtulaCard, Account, Wallet
    }

    public enum CurrencyType
    {
        BTC, USD, GBP, EUR
    }
}
