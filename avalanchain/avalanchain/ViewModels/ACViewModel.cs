using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace avalanchain
{
    public class ACViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Sample _selectedSample;

        public ACViewModel(INavigation navigation)
        {
            SamplesCategories = new List<SampleCategory>(ACItemsDefinition.SamplesCategories.Values);
            AllSamples = ACItemsDefinition.AllSamples;
            SamplesGroupedByCategory = ACItemsDefinition.SamplesGroupedByCategory;
        }

        public List<SampleCategory> SamplesCategories { get; set; }

        public List<Sample> AllSamples { get; set; }

        public List<SampleGroup> SamplesGroupedByCategory { get; set; }

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

                    RaisePropertyChanged("SelectedSample");
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}