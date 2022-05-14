using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Product: INotifyPropertyChanged
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string vendor { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime published_at { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public List<string> tags { get; set; }
        public string admin_id { get; set; }
        public float weight { get; set; }
        public float weight_unit { get; set; }
        public float price { get; set; }
        public int amount { get; set; }
        public List<string> images { get; set; }

        private bool isFavourite;

        public bool IsFavourite
        {
            get
            {
                return isFavourite;
            }
            set
            {
                isFavourite = value;
                this.NotifyPropertyChanged("IsFavourite");
            }
        }

        public double DiscountPrice 
        { 
            get
            {
                return this.price - (this.price * (this.DiscountPercent/100));
            }
        }
        public double DiscountPercent { get; } = 12;

        public double OverallRating { get; } = 4;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }


    }

    public class CartItem
    {
        public string id { get; set; }

        public int amount { get; set; } = 1;

        public Product item { get; set; }

    }
}
