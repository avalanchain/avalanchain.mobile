using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    class TranasactionsViewModel
    {
        //public List<SampleGroup> TransactionsGroupedByCategory { get; set; }
        public TranasactionsViewModel()
        {
            //TransactionsGroupedByCategory = ACItemsDefinition.SamplesGroupedByCategory;
        }

        public List<Transaction> Tranasactions
        {
            get
            {
                return SampleData.Transactions;
            }
        }

       



       
    }
}
