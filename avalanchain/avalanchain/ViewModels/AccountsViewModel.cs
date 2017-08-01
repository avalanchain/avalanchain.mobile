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
    class AccountsViewModel : BaseViewModel
    {
        private ObservableCollection<Account> _accounts = new ObservableCollection<Account>();
        private Command _fetchAccountsCommand;
        public AccountsViewModel()
        {
            //Accounts.AddRange(SampleData.Accounts);
        }

        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set { _accounts = value; OnPropertyChanged("Accounts"); }
            //get
            //{
            //    return SampleData.Accounts;
            //}
        }

        public Command FetchAccountsCommand
        {
            get { return _fetchAccountsCommand ?? (_fetchAccountsCommand = new Command(async () => await ExecuteFetchAccountsCommand())); }
        }

        public async Task ExecuteFetchAccountsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                //if (await ConnectivityService.IsConnected())
                //{
                Accounts.Clear();//MomentService.Instance.GetMoments();

                await Task.Run(() => { Accounts = new ObservableCollection<Account>(SampleData.Accounts); });
                //}
                //else
                //{
                //    DialogService.ShowError(Strings.NoInternetConnection);
                //}
            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report(ex);
            }

            IsBusy = false;
        }

    }
}
