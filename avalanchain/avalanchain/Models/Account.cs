using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    public class Account
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Price
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public string Manufacturer
        {
            get { return "UXDIVERS"; }
            set { }
        }

        public string ThumbnailHeight
        {
            get;
            set;
        }

        public double RatingValue { get; set; }
        public double RatingMax { get; set; }

        public Account()
        {
        }
    }
}
