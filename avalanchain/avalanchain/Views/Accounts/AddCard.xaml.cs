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
    public partial class AddCard : ContentPage
    {
        private string _type = "Add Card";
        public AddCard(string type)
        {
            InitializeComponent();
            //BindingContext = this;
            if (!String.IsNullOrEmpty(type))
            {
                _type = $"Add {type} Card ";
            }
        }


        protected override void OnAppearing()
        {
            Title = _type;
        }
    }
}