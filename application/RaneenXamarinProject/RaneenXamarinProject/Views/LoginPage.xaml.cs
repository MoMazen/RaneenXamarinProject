using RaneenXamarinProject.Models;
using RaneenXamarinProject.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();

            BindingContext = new LoginPageViewModel();
        }

        protected override void OnAppearing()
        {
            if (App.Current.Properties.ContainsKey("userLogin"))
            {
                Navigation.PushAsync(new Views.Profile());
            }
        }
    }
}