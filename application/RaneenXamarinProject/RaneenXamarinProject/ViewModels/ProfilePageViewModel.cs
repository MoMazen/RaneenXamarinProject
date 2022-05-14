using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfilePageViewModel:BaseViewModel
    {
        #region Fields
        Customer customer;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class
        /// </summary>
        public ProfilePageViewModel()
        {
            getCurrentUserData();

            this.ProfileInfoCommand = new Command(this.ProfileInfoClicked);
            this.AddressCommand = new Command(this.AddressOptionClicked);
            this.CartCommand = new Command(this.CartOptionClicked);
            this.WishListCommand = new Command(this.WishListOptionClicked);
            this.LogoutCommand = new Command(this.LogoutOptionClicked);
        }

        #endregion

        #region Properties

        public Customer CustomerData
        {
            get { return customer; }
            set 
            {
                if (this.customer == value)
                {
                    return;
                }

                this.SetProperty(ref this.customer, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the profileInfo option is clicked.
        /// </summary>
        public Command ProfileInfoCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the address option is clicked.
        /// </summary>
        public Command AddressCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the cart option is clicked.
        /// </summary>
        public Command CartCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the wishList option is clicked.
        /// </summary>
        public Command WishListCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the logout option is clicked.
        /// </summary>
        public Command LogoutCommand { get; set; }

        #endregion

        #region Methods

        private async void getCurrentUserData()
        {
            var customer = await getCurrentUser();

            CustomerData = customer;
        }


        /// <summary>
        /// Invoked when the profileInfo option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void ProfileInfoClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            // TODO: Navigate to ProfileInfo
            await Shell.Current.Navigation.PushAsync(new Views.ProfileInfo());
        }

        /// <summary>
        /// Invoked when the address option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void AddressOptionClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            // TODO: Navigate to Address
            await Shell.Current.Navigation.PushAsync(new MyAddressPage());
        }

        /// <summary>
        /// Invoked when the cart option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void CartOptionClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            // TODO: Navigate to cart

            emptyingNavigationStack();

            await Shell.Current.GoToAsync("//cart");
        }

        /// <summary>
        /// Invoked when the wishList option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void WishListOptionClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            // TODO: Navigate to WishList
            await Shell.Current.Navigation.PushAsync(new WishlistPage());
        }

        /// <summary>
        /// Invoked when the logout option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void LogoutOptionClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;

            // TODO: logOut
            Preferences.Remove("UserToken");
            await Shell.Current.Navigation.PopToRootAsync();
        }

        #endregion

    }
}
