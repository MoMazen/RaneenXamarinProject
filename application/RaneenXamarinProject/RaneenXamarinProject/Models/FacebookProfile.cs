using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class FacebookProfile
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string id { get; set; }
    }

    public class JsonFacebookProfile
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userId { get; set; }
    }
}
