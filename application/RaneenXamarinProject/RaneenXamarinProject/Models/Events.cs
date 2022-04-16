using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Events
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<Product> product_list { get; set; }
    }
}
