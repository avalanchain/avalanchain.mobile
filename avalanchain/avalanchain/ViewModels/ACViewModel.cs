using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avalanchain;
using Xamarin.Forms;

namespace avalanchain
{
    public class ACViewModel : BaseViewModel//INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        private Sample _selectedSample;
        private readonly User _currentUser = new User("Bill1", "user_profile_1.jpg");

        public ACViewModel(INavigation navigation)
        {
            SamplesCategories = new List<SampleCategory>(ACItemsDefinition.SamplesCategories.Values);
            AllSamples = ACItemsDefinition.AllSamples;
            SamplesGroupedByCategory = ACItemsDefinition.SamplesGroupedByCategory;
            LoadData();
        }

        public List<SampleCategory> SamplesCategories { get; set; }

        public List<Sample> AllSamples { get; set; }

        public List<SampleGroup> SamplesGroupedByCategory { get; set; }
        public string Avatar
        {
            set
            {
                if (_currentUser.Avatar != value)
                {
                    _currentUser.Avatar = value;
                    OnPropertyChanged("Avatar");
                }
            }
            get => _currentUser.Avatar;
        }
        public string Name
        {
            set
            {
                if (_currentUser.Name != value)
                {
                    _currentUser.Name = value;
                    OnPropertyChanged("Name");
                }
            }
            get => _currentUser.Name;
        }

        public Sample SelectedSample
        {
            get
            {
                return _selectedSample;
            }

            set
            {
                if (value != _selectedSample)
                {
                    _selectedSample = value;

                    OnPropertyChanged("SelectedSample");
                }
            }
        }

        private void LoadData()
        {
            var currentUser = SampleData.CurrentUser;
            Avatar = currentUser.Avatar;
            Name = currentUser.Name;
        }

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}