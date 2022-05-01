using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// This viewmodel extends in another viewmodels.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        protected HttpClient client;

        private Command<object> backButtonCommand;

        private bool isLoading;
        #endregion

        #region Constructor
        public BaseViewModel()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
        }
        #endregion

        #region Properties

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                if (this.isLoading == value)
                {
                    return;
                }

                this.SetProperty(ref this.isLoading, value);
            }
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> BackButtonCommand
        {
            get
            {
                return this.backButtonCommand ?? (this.backButtonCommand = new Command<object>(this.BackButtonClicked));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            if (Device.RuntimePlatform == Device.UWP && Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.Navigation.PopAsync();
            }
            else if (Device.RuntimePlatform != Device.UWP && Shell.Current.Navigation.NavigationStack.Count > 0)
            {
                Shell.Current.Navigation.PopAsync();
            }
        }

        public async Task<Customer> getCurrentUser()
        {
            try
            {
                IsLoading = true;
                if (Preferences.ContainsKey("UserToken"))
                {
                    client.DefaultRequestHeaders.Add("x-auth-token", Preferences.Get("UserToken", ""));

                    var requestResult = await client.GetAsync("https://raneen-app.herokuapp.com/app/api/v1/Profile/me");

                    if (requestResult.IsSuccessStatusCode)
                    {
                        var responseContent = await requestResult.Content.ReadAsStringAsync();
                        Debug.WriteLine("Response: " + responseContent);

                        var response = JsonConvert.DeserializeObject<Response>(responseContent);

                        if (response.success)
                        {

                            Customer customer = JsonConvert.DeserializeObject<Customer>(response.data.ToString());
                            Debug.WriteLine("customer : " + customer.email);
                            customer.fullName = $"{customer.firstName} {customer.lastName}";
                            IsLoading = false;
                            return customer;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Debug.WriteLine(e);
                await Shell.Current.DisplayAlert("Alert", "Connection error please try again!", "OK");
            }
            IsLoading = false;
            Debug.WriteLine("HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            return null;
        }

        #endregion
    }
}
