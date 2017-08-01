using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace avalanchain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderCard : ContentPage
    {
        private string _type = "Order Virtual Card";
        public OrderCard(string type)
        {
            InitializeComponent();
            //BindingContext = this;
            if (!String.IsNullOrEmpty(type))
            {
                _type = $"Order {type} Card ";
            }
        }


        protected override void OnAppearing()
        {
            Title = _type;
        }
    }
}