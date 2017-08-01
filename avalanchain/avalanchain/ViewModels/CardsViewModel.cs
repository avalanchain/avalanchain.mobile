using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain;
using avalanchain.Models;
using Xamarin.Forms;

namespace avalanchain
{
    class CardsViewModel : BaseViewModel
    {
        private ObservableCollection<Card> _cards = new ObservableCollection<Card>();
        private Command _fetchWalletsCommand;
        public CardsViewModel()
        {
        }

        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set { _cards = value; OnPropertyChanged("Cards"); }
        }
        public Command FetchAccountsCommand
        {
            get { return _fetchWalletsCommand ?? (_fetchWalletsCommand = new Command(async () => await ExecuteFetchCardsCommand())); }
        }

        public async Task ExecuteFetchCardsCommand()
        {
            if (IsBusy)
                return;


            IsBusy = true;

            try
            {
                Cards.Clear();

                await Task.Run(() => { Cards = new ObservableCollection<Card>(SampleData.Cards); });
            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report(ex);
            }

            IsBusy = false;
        }
    }
}
