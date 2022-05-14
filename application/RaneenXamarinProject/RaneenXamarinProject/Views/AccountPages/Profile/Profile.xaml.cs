using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            BindingContext = new ProfilePageViewModel();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if ((BindingContext as ProfilePageViewModel).CustomerData == null)
        //    {
        //        var customer = await (BindingContext as ProfilePageViewModel).getCurrentUser();
        //        (BindingContext as ProfilePageViewModel).CustomerData = customer;
        //    }
        //}
    }
}