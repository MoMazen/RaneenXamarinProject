using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Order
    {
        public string id { get; set; }
        public string customer_id { get; set; }
        public List<CartItem> products { get; set; }
        public float total_price { get; set; }
    }
}
