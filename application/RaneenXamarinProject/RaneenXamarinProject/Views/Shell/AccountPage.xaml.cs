using RaneenXamarinProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!Preferences.ContainsKey("UserToken"))
            {
                Shell.Current.Navigation.PushAsync(new LoginPage(),false);
            }
            else
            {
                Shell.Current.Navigation.PushAsync(new Profile(),false);
            }
        }
    }
}