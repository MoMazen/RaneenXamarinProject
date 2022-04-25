using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Models
{
    public class Response
    {
        public bool success { get; set; }
        public object data { get; set; }
    }

    public class AuthResponse
    {
        public bool success { get; set; }
        public string jwt { get; set; }
    }

    public class ErrorResponse
    {
        public bool success { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public string message { get; set; }

        public override string ToString()
        {
            return message;
        }
    }
}
