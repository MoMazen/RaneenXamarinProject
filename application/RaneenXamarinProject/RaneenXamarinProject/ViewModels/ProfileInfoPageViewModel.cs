using RaneenXamarinProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.ViewModels
{
    public class ProfileInfoPageViewModel:BaseViewModel
    {

        #region Fields
        Customer customer;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class
        /// </summary>
        public ProfileInfoPageViewModel()
        {
            getCurrentUserData();
            //this.ProfileInfoCommand = new Command(this.ProfileInfoClicked);
            //this.AddressCommand = new Command(this.AddressOptionClicked);
            //this.CartCommand = new Command(this.CartOptionClicked);
            //this.WishListCommand = new Command(this.WishListOptionClicked);
            //this.LogoutCommand = new Command(this.LogoutOptionClicked);
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

        #region Methods

        private async void getCurrentUserData()
        {
            CustomerData = await getCurrentUser();
        }

        #endregion
    }
}
