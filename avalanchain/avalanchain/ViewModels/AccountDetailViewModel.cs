using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    class AccountDetailViewModel
    {
        public AccountDetailViewModel(Account account)
        {
            Account = account;
        }
        public Account Account { get; }

        public List<TransactionGroup> GroupedTransactions
        {
            get
            {
                var transactions = SampleData.Transactions.Where(acc => acc.From == Account.AccountNumber).ToList();
                var transactionsTo = SampleData.Transactions.Where(acc => acc.To == Account.AccountNumber).Select(AccountsService.TransformReceiverTransaction).ToList();
                transactions.AddRange(transactionsTo);
                //transactions = transactions
                var groups = transactions.GroupBy(t => t.DateTime.DayOfYear).Select(g => new {date = g.First().DateTime ,dayOfYear = g.Key, transactions = g.ToList() }).ToList();


               var groupedTransactions = new List<TransactionGroup>();

                foreach (var group in groups)
                {
                    var sampleItem = new TransactionGroup(group.date);

                    sampleItem.AddRange(group.transactions);

                    groupedTransactions.Add(sampleItem);
                }
                groupedTransactions = groupedTransactions.OrderBy(t => t.DateTime).ToList();
                return groupedTransactions;
            }
        }

        //internal static Dictionary<string, TransactionGroup> CreateTransaction()
        //{
        //    var tt = new Dictionary<string, TransactionGroup>();
        //    var transactions = Transactions();

        //    return tt;
        //}
    }
}
