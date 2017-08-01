using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    class ConfirmationViewModel : BaseViewModel
    {
        //private string _toCurrencyIcon;
        private Send _send;

        public ConfirmationViewModel(Send send)
        {
            Send = send;
            CurrencyIcon = AccountsService.GetCurrencyIcon(send.CurrencyReceive);
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
        public string CurrencyIcon { get; set; }
        //public Command SendCommentCommand
        //{
        //    get { return sendCommentCommand ?? (sendCommentCommand = new Command(async (parameter) => await ExecuteSendCommentCommand(parameter))); }
        //}
        public void ExecuteSend()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {

                var isOk = AccountsService.SendMoney(Send);


                //if (await ConnectivityService.IsConnected())
                //{

                //    await CommentService.Instance.AddComment(Moment, text);

                //    //Comments.AddRange(refreshedComments);
                //}
                //else
                //{
                //    //DialogService.ShowError(Strings.NoInternetConnection);
                //}

            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report(ex);
            }

            IsBusy = false;
            //await ExecuteFetchCommentsCommand();
        }
    }
}
