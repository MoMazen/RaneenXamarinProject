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
            if (SharedData.Navigation == null)
            {
                SharedData.Navigation = Navigation;
            }
            base.OnAppearing();
            if (!Preferences.ContainsKey("UserToken"))
            {
                Navigation.PushAsync(new LoginPage());
            }
            else
            {
                Navigation.PushAsync(new Profile());
            }
        }
    }
}