using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    class TransactionDetailViewModel
    {
        private Transaction _transaction;
        public TransactionDetailViewModel(Transaction transaction)
        {
            _transaction = transaction;
        }
        public Transaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

    }
}
