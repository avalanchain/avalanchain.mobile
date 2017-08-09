using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    class WalletsViewModel : BaseViewModel// INotifyPropertyChanged//:
    {
        readonly CurrencyPricing _cryptocurrencyPrices = new CurrencyPricing();
        private ObservableCollection<CryptocurrencyWallet> _wallets = new ObservableCollection<CryptocurrencyWallet>();
        private Command _fetchWalletsCommand;
        private DateTime _dateTime;
        //public event PropertyChangedEventHandler PropertyChanged;
        public WalletsViewModel()
        {
            var initPricing = SampleData.StaticCryptocurrencyPrices;
            BtcToEur = initPricing.EUR;
            BtcToGbp = initPricing.GBP;
            BtcToUsd = initPricing.USD;
            DateTime = DateTime.UtcNow.AddDays(-1); 
            LoadData();
            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                LoadData();
                return true;
            });
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set { _dateTime = value; OnPropertyChanged("DateTime"); }
        }
        public ObservableCollection<CryptocurrencyWallet> Wallets
        {
            get => _wallets;
            set { _wallets = value; OnPropertyChanged("Wallets"); }
        }
        public Command FetchAccountsCommand
        {
            get { return _fetchWalletsCommand ?? (_fetchWalletsCommand = new Command(async () => await ExecuteFetchWalletsCommand())); }
        }

        public async Task ExecuteFetchWalletsCommand()
        {
            if (IsBusy)
                return;
            

            IsBusy = true;

            try
            {
                Wallets.Clear();

                await Task.Run(() => { Wallets = new ObservableCollection<CryptocurrencyWallet>(SampleData.Wallets); });
            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report(ex);
            }

            IsBusy = false;
        }

        public string BtcToUsd
        {
            set
            {
                if (_cryptocurrencyPrices.BTC != value)
                {
                    _cryptocurrencyPrices.BTC = value;
                    OnPropertyChanged("BtcToUsd");
                }
            }
            get => _cryptocurrencyPrices.BTC;
        }
        public string BtcToEur
        {
            set
            {
                if (_cryptocurrencyPrices.EUR != value)
                {
                    _cryptocurrencyPrices.EUR = value;
                    OnPropertyChanged("BtcToEur");
                }
            }
            get => _cryptocurrencyPrices.EUR;
        }
        public string BtcToGbp
        {
            set
            {
                if (_cryptocurrencyPrices.GBP != value)
                {
                    _cryptocurrencyPrices.GBP = value;
                    OnPropertyChanged("BtcToGbp");
                }
            }
            get => _cryptocurrencyPrices.GBP;
        }

        //private async Task<CurrencyPricing> LoadData()
        //{
        //    return await SampleData.CryptocurrencyPrices;
        //}

        private async void LoadData()
        {
            var cp = await SampleData.CryptocurrencyPrices;
            BtcToEur = cp.EUR;
            BtcToGbp = cp.GBP;
            BtcToUsd = cp.USD;
            DateTime = DateTime.UtcNow;
        }
    }
}
