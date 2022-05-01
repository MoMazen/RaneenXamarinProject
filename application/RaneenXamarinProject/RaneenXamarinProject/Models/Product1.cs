using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Product1
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string vendor { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime published_at { get; set; }
        public string status { get; set; }
        public string tag { get; set; }
        public string admin_id { get; set; }
        public float weight { get; set; }
        public float weight_unit { get; set; }
        public float price { get; set; }
        public int amount { get; set; }
        public List<string> images { get; set; }
    }
}
