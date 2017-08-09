using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain;
using Xamarin.Forms;

namespace avalanchain.ViewModels
{
    class TransferViewModel : BaseViewModel
    {
        Account _from = new Account();
        Account _to = new Account();
        private Command sendCommentCommand;
        private bool _isOwnResiever;
        private bool _isAnotherResiever;
        private string _toCurrencyIcon;
        private decimal _rate;
        private decimal _receiveAmount;
        private Send _send;
        private CurrencyPricing _prices;

        public TransferViewModel()
        {
            InitViewModel();
        }

        public TransferViewModel(Account fromAccount)
        {
            InitViewModel();
            From = fromAccount;
        }

        public void InitViewModel()
        {
            ChoosIcon = FontAwesome.FAAngleRight;
            IsOwnResiever = false;
            IsAnotherResiever = true;
            From = SampleData.Accounts[1];
            To = SampleData.Accounts[1];
            ToCurrencyIcon = FontAwesome.FAMoney;
            Rate = 1;
            Send = new Send();

            Prices = new CurrencyPricing
            {
                EUR = "2183.22",
                GBP = "2183.22",
                USD = "2515.21",
                BTC = "1"

        };

            LoadData();
        }

        public string ChoosIcon { get; set; }

        public Account From
        {
            get => _from;
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public Account To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }
        public Send Send
        {
            get => _send;
            set
            {
                _send = value;
                OnPropertyChanged("Send");
            }
        }
        public CurrencyPricing Prices
        {
            get => _prices;
            set
            {
                _prices = value;
                OnPropertyChanged("Prices");
            }
        }
        public string ToCurrencyIcon
        {
            get => _toCurrencyIcon;
            set
            {
                _toCurrencyIcon = value;
                OnPropertyChanged("ToCurrencyIcon");
            }
        }
        public decimal Rate
        {
            get => _rate;
            set
            {
                _rate = value;
                OnPropertyChanged("Rate");
            }
        }
        public void ChangeRate()
        {
            CurrencyType from = From.Currency;
            CurrencyType receive = To.Currency;
            if (from == receive)
            {
                Rate = 1;
                return;
            }
            var fromPrice = decimal.Parse(AccountsService.GetCurrencyPrice(from, Prices));
            var receivePrice = decimal.Parse(AccountsService.GetCurrencyPrice(receive, Prices));


            if (fromPrice != 0 && receivePrice != 0)
            {
                if (from == CurrencyType.BTC || receive == CurrencyType.BTC)
                {
                    var rate = receivePrice / fromPrice;
                    Rate = decimal.Parse(rate.ToString("0.##########")) ;
                }
                else
                {
                    var rate = fromPrice / receivePrice;
                    Rate = decimal.Parse(rate.ToString("0.##"));
                }
            }


            //IsOwnResiever = !IsOwnResiever;
            //IsAnotherResiever = !IsAnotherResiever;

            //ToCurrencyIcon = IsOwnResiever ? FontAwesome.FAMoney : To.CurrencyIcon;
        }

        public bool IsOwnResiever
        {
            get => _isOwnResiever;
            set
            {
                _isOwnResiever = value;
                OnPropertyChanged("IsOwnResiever");
            }
        }

        public bool IsAnotherResiever
        {
            get => _isAnotherResiever;
            set
            {
                _isAnotherResiever = value;
                OnPropertyChanged("IsAnotherResiever");
            }
        }

        public void ChangeReseiver()
        {
            ToCurrencyIcon = IsOwnResiever ? FontAwesome.FAMoney : To.CurrencyIcon;
            IsOwnResiever = !IsOwnResiever;
            IsAnotherResiever = !IsAnotherResiever;
        }

        public void ChangeReseiverAccount(Account account)
        {
            if (IsAnotherResiever)
            {
                ChangeReseiver();
                Rate = 1;
            }
            
            if (account != null)
                To = account;

            if(!IsAnotherResiever)
            {
                ToCurrencyIcon = To.CurrencyIcon;
            }
            Send.ReceiverAccountNumber = IsOwnResiever ? To.AccountNumber : Send.ReceiverAccountNumber;
            UpdateAll();
        }
        public void ChangeFromAccount(Account account)
        {
            if (account != null)
            {
                From = account;
                UpdateAll();
            }
                
        }

        public void ChangeReceiveAmount(decimal number)
        {
            ChangeRate();
            _receiveAmount = number;
            Send.ReceiveAmount = ParseData(_receiveAmount * Rate);
            UpdateAll();
        }

        private async void LoadData()
        {
            var prices = await SampleData.CryptocurrencyPrices;
            if (prices != null)
            {
                Prices = prices;
                UpdateAll();
            }
        }

        private void UpdateAll()
        {
            OnPropertyChanged("Prices");
            ChangeRate();
            RefreshReceiveAmount();
            OnPropertyChanged("Send");
        }
        private void RefreshReceiveAmount()
        {
            Send.ReceiveAmount = ParseData(_receiveAmount * Rate);
        }

        private decimal ParseData(decimal value)
        {
            CurrencyType from = From.Currency;
            CurrencyType receive = To.Currency;
            var fromPrice = decimal.Parse(AccountsService.GetCurrencyPrice(from, Prices));
            var receivePrice = decimal.Parse(AccountsService.GetCurrencyPrice(receive, Prices));


            if (fromPrice != 0 && receivePrice != 0)
            {
                if (receive == CurrencyType.BTC)
                {
                    value = decimal.Parse(value.ToString("0.##########"));
                }
                else
                {
                    value = decimal.Parse(value.ToString("0.##"));
                }
            }
            return value;
        }
    }


}
