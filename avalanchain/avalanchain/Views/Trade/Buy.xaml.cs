using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    public partial class Buy : ContentPage
    {
        private string _bitcoins;
        public Buy()
        {
            InitializeComponent();
        }

        
        public string Bitcoins
        {
            get
            {
                return _bitcoins;
            }
            set
            {
                _bitcoins = value;
                OnPropertyChanged();
            }
        }
    }
}