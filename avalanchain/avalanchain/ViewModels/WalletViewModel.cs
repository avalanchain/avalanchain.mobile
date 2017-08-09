using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace avalanchain
{
    class WalletViewModel :BaseViewModel
    {
        private ZXingBarcodeImageView _barcode;
        public WalletViewModel(CryptocurrencyWallet wallet)
        {
            Wallet = wallet;
            InitData(wallet.AccountNumber);
        }
        public CryptocurrencyWallet Wallet { get; }
        public ZXingBarcodeImageView Barcode {

            get => _barcode;
            set
            {
                _barcode = value;
                OnPropertyChanged("Barcode");
            }
        }
        public void InitData(string walletNumber)
        {
            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView",
                BarcodeFormat = ZXing.BarcodeFormat.QR_CODE,
                BarcodeOptions =
                {
                    Width = 200,
                    Height = 200,
                },
                BarcodeValue = walletNumber,
            };

            Barcode = barcode;
        }

        public List<TransactionGroup> GroupedTransactions
        {
            get
            {
                var transactions = SampleData.Transactions.Where(acc => acc.From == Wallet.AccountNumber).ToList();
                var transactionsTo = SampleData.Transactions.Where(acc => acc.To == Wallet.AccountNumber).Select(AccountsService.TransformReceiverTransaction).ToList();
                transactions.AddRange(transactionsTo);
                //transactions = transactions
                var groups = transactions.GroupBy(t => t.DateTime.DayOfYear).Select(g => new { date = g.First().DateTime, dayOfYear = g.Key, transactions = g.ToList() }).ToList();


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
    }
}
