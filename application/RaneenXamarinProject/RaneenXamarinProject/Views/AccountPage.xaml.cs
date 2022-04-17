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
            if (!Preferences.ContainsKey("userLogin"))
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