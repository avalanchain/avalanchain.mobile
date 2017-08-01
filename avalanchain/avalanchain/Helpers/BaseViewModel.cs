using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string title = string.Empty;
        private string subTitle = string.Empty;
        private string icon = null;
        private bool isBusy;

        public const string TitlePropertyName = "Title";
        public const string SubtitlePropertyName = "Subtitle";
        public const string IconPropertyName = "Icon";
        public const string IsBusyPropertyName = "IsBusy";

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {

        }

        //public string Title
        //{
        //    get { return title; }
        //    set { SetProperty(ref title, value, TitlePropertyName); }
        //}

        //public string Subtitle
        //{
        //    get { return subTitle; }
        //    set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
        //}

        //public string Icon
        //{
        //    get { return icon; }
        //    set { SetProperty(ref icon, value, IconPropertyName); }
        //}

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        protected void SetProperty<T>(ref T backingStore, T value, string propertyName, Action onChanged = null, Action<T> onChanging = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            OnPropertyChanging(propertyName);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging == null)
                return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetObservableProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}
