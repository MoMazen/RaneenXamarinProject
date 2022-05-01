using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<Product1> product_list { get; set; }
    }
}
