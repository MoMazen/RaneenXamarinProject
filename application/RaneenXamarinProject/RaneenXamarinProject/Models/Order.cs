using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Order
    {
        public ObservableCollection<CartItem> products { get; set; }
    }
}
