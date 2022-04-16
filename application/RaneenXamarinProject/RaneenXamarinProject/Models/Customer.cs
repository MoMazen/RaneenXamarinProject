using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Customer
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int order_count { get; set; }
        public string phone { get; set; }
        public float total_spent { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string company { get; set; }
    }
}
