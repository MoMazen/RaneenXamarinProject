using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RaneenXamarinProject.Models
{
    public abstract class SharedData
    {
        public static Customer CurrentUser { get; set; }
        public static Page currentPage { get; set; } = null;
    }
}
