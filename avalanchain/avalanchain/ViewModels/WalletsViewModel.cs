using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain;
using Xamarin.Forms;

namespace avalanchain
{
    class WalletsViewModel : BaseViewModel// INotifyPropertyChanged//:
    {
        readonly CurrencyPricing _cryptocurrencyPrices = new CurrencyPricing();
        private ObservableCollection<CryptocurrencyWallet> _wallets = new ObservableCollection<CryptocurrencyWallet>();
        private Command _fetchWalletsCommand;
        //public event PropertyChangedEventHandler PropertyChanged;
        public WalletsViewModel()
        {
            LoadData();
            //Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            //{
            //    LoadData();
            //    return true;
            //});
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
        public string EthToUsd
        {
            set
            {
                if (_cryptocurrencyPrices.ETH != value)
                {
                    _cryptocurrencyPrices.ETH = value;
                    OnPropertyChanged("EthToUsd");
                }
            }
            get => _cryptocurrencyPrices.ETH;
        }
        public string EosToUsd
        {
            set
            {
                if (_cryptocurrencyPrices.EOS != value)
                {
                    _cryptocurrencyPrices.EOS = value;
                    OnPropertyChanged("EosToUsd");
                }
            }
            get => _cryptocurrencyPrices.EOS;
        }

        //private async Task<CurrencyPricing> LoadData()
        //{
        //    return await SampleData.CryptocurrencyPrices;
        //}

        private async void LoadData()
        {
            EthToUsd = "ETH \n " + SampleData.StaticCryptocurrencyPrices.ETH;
            EosToUsd = "EOS \n " + SampleData.StaticCryptocurrencyPrices.EOS;
            BtcToUsd = "BTC \n " + SampleData.StaticCryptocurrencyPrices.USD;
            var cp = await SampleData.CryptocurrencyPrices;
            //var ee = decimal.Parse(cp.USD.Replace(".", ","));
            var eth= (decimal.Parse(cp.USD) / decimal.Parse(cp.ETH));
            var eos = (decimal.Parse(cp.USD) / decimal.Parse(cp.EOS));
            eth = Math.Round(eth, 2);
            eos = Math.Round(eos, 2);
            EthToUsd = "ETH \n " + eth.ToString();
            EosToUsd = "EOS \n " + eos.ToString();
            BtcToUsd = "BTC \n " + cp.USD;
            //EthToUsd = eth.ToString();
            //EosToUsd = eos.ToString();
            //BtcToUsd = cp.USD;
        }
    }
}
