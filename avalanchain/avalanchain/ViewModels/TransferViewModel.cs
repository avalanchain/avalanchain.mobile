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
        private Account _from;
        private Account _to;
        private Command sendCommentCommand;
        private bool _isOwnResiever;
        private string _toCurrencyIcon;
        private decimal _rate;
        private decimal _receiveAmount;
        private Send _send = new Send();
        private CurrencyPricing _prices;

        public TransferViewModel()
        {
            Send = new Send();
            InitViewModel(string.Empty, false);
        }

        public TransferViewModel(Account account, bool isFrom)
        {
            if (isFrom)
            {
                InitViewModel(account.AccountNumber, isFrom);
            }
            else
            {
                Send = new Send()
                {
                    ReceiverAccountNumber = account.AccountNumber
                };
                InitViewModel(string.Empty, isFrom);
            }


            //From = fromAccount;
        }

        public void InitViewModel(string accNnumber, bool isFrom)
        {
            var accountNumber = String.IsNullOrEmpty(accNnumber) ? SampleData.Accounts[1].AccountNumber : accNnumber;

            ChoosIcon = FontAwesome.FAAngleRight;
            IsOwnResiever = false;
            From = AccountsService.GetAccount(accountNumber);
            To = AccountsService.GetAccount(accountNumber);
            ToCurrencyIcon = FontAwesome.FAMoney;
            Rate = 1;
            if (isFrom)
            {
                Send = new Send();
            }
            OnPropertyChanged("Send");
            Prices = SampleData.StaticCryptocurrencyPrices;


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

        public bool IsOwnResiever
        {
            get => _isOwnResiever;
            set
            {
                _isOwnResiever = value;
                OnPropertyChanged("IsOwnResiever");
                OnPropertyChanged("IsAnotherResiever");
            }
        }

        public bool IsAnotherResiever => !IsOwnResiever;


        public void ChangeRate()
        {

            try
            {
                CurrencyType from = From.Currency;
                CurrencyType receive = IsOwnResiever ? To.Currency : From.Currency;

                if (from == receive || !IsOwnResiever)
                {
                    Rate = 1;
                    return;
                }
                var fPrice = AccountsService.GetCurrencyPrice(from, Prices);
                var fromPrice = decimal.Parse(fPrice);
                var receivePrice = decimal.Parse(AccountsService.GetCurrencyPrice(receive, Prices));


                if (fromPrice != 0 && receivePrice != 0)
                {
                    var rate = receivePrice / fromPrice;
                    if (from == CurrencyType.BTC || receive == CurrencyType.BTC)
                    {
                        Rate = decimal.Parse(rate.ToString("0.##########"));
                    }
                    else
                    {
                        Rate = decimal.Parse(rate.ToString("0.##"));
                    }
                }
            }
            catch (Exception ex)
            {
                var er = ex;
            }
            
        }

        private decimal ParseData(string value)
        {
            decimal result = 0;
            if (value != null && decimal.TryParse(value, out result))
            {
                result = decimal.Parse(value);
            }
            return result;
        }

        public void ChangeReseiver()
        {
            ToCurrencyIcon = IsOwnResiever ? FontAwesome.FAMoney : To.CurrencyIcon;
            IsOwnResiever = !IsOwnResiever;
        }

        public void ChangeReseiverAccount(Account account)
        {
            if (account == null)
            {
                ChangeReseiver();
                Rate = 1;
            }
            else
            {
                To = account;
                if (!IsOwnResiever)
                {
                    ChangeReseiver();
                }
            }

            if (IsOwnResiever)
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
