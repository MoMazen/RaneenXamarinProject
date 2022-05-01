using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class MyAddressPageViewModel : BaseViewModel
    {
        #region Fields

        private bool hasAddress;

        private Customer customer;

        private Address customerAddress;

        private Command editCommand;

        private Command deleteCommand;

        private Command addCardCommand;

        #endregion

        #region Constructor

        public MyAddressPageViewModel()
        {
            getCurrentUserData();
        }

        #endregion

        #region Properties

        public bool HasAddress
        {
            get { return hasAddress; }
            set
            {
                this.SetProperty(ref this.hasAddress, value);
            }
        }


        public Customer CustomerData
        {
            get { return customer; }
            set 
            {
                this.SetProperty(ref this.customer, value); 
            }
        }

        public Address CustomerAddress
        {
            get { return customerAddress; }
            set
            {
                this.SetProperty(ref this.customerAddress, value);
            }
        }

        /// <summary>
        /// Gets or sets the value of my address page view model.
        /// </summary>
        //public static MyAddressPageViewModel BindingContext =>
        //    myAddressViewModel = PopulateData<MyAddressPageViewModel>("detail.json");

        [DataMember(Name = "addressDetails")]
        public Address AddressDetails { get; set; }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new Command(this.EditButtonClicked));
            }
        }

        /// <summary>
        /// Gets the command is executed when the delete button is clicked.
        /// </summary>
        public Command DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new Command(this.DeleteButtonClicked));
            }
        }

        /// <summary>
        /// Gets the command is executed when the add card button is clicked.
        /// </summary>
        public Command AddCardCommand
        {
            get
            {
                return this.addCardCommand ?? (this.addCardCommand = new Command(this.AddCardButtonClicked));
            }
        }

        #endregion

        #region Methods

        private async void getCurrentUserData()
        {
            var customer = await getCurrentUser();
            CustomerData = customer;
            HasAddress = CustomerData.address == null? false:true;
            //if (!HasAddress)
            //{
            //    CustomerData.address = new Address();
            //}
        }

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "RaneenXamarinProject.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when the edit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the delete button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void DeleteButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the add card button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void AddCardButtonClicked(object obj)
        {
            // Do something
            await Shell.Current.Navigation.PushAsync(new AddAddressPage());
        }

        #endregion
    }
}
