using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Customer
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        //public int order_count { get; set; }
        public string phone { get; set; }
        //public float total_spent { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public string email { get; set; }
        //public string verified_email { get; set; }
        public string password { get; set; }
        public string company { get; set; }
        public string role { get; set; }
        public Address address { get; set; }

    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string customerId { get; set; }
        public string province { get; set; }
        public string province_code { get; set; }
        public string zip { get; set; }
    }
}
