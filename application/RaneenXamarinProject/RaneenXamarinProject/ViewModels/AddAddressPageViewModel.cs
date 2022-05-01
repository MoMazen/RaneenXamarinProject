using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Validators;
using RaneenXamarinProject.Validators.Rules;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for Business Registration Form page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class AddAddressPageViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressPageViewModel" /> class
        /// </summary>
        public AddAddressPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.SubmitCommand = new Command(this.SubmitClicked);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Phone Number from user.
        /// </summary>
        public ValidatableObject<string> PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Street Address from user.
        /// </summary>
        public ValidatableObject<string> StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the City from user.
        /// </summary>
        public ValidatableObject<string> City { get; set; }

        //public ValidatableObject<string> Country { get; set; }

        private ValidatableObject<string> country;

        public ValidatableObject<string> Country
        {
            get { return country; }
            set 
            {
                this.SetProperty(ref this.country, value);
            }
        }

        //public ValidatableObject<string> State { get; set; }

        private ValidatableObject<string> state;

        public ValidatableObject<string> State
        {
            get { return state; }
            set
            {
                this.SetProperty(ref this.state, value);
            }
        }


        #endregion

        #region Comments

        /// <summary>
        /// Gets or sets the command is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializzing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.StreetAddress = new ValidatableObject<string>();
            this.City = new ValidatableObject<string>();
            this.State = new ValidatableObject<string>();
            this.Country = new ValidatableObject<string>();
            this.PhoneNumber = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for name
        /// </summary>
        private void AddValidationRules()
        {
            this.StreetAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Street Required" });
            this.City.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "City Required" });
            this.State.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "State Required" });
            this.Country.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Country Required" });
            this.PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Phone Required" });
        }

        /// <summary>
        /// Check name is valid or not
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        private bool AreFieldsValid()
        {
            bool isStreetValid = this.StreetAddress.Validate();
            bool isCityValid = this.City.Validate();
            bool isStateValid = this.Country.Validate();
            bool isCountryValid = this.State.Validate();
            bool isPhoneValid = this.PhoneNumber.Validate();
            return isStreetValid && isCityValid && isStateValid && isCountryValid && isPhoneValid;
        }

        /// <summary>
        /// Invoked when the Submit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void SubmitClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                IsLoading = true;

                Address address = new Address()
                {
                    address1 = this.StreetAddress.Value,
                    address2 = this.StreetAddress.Value,
                    country = this.Country.Value,
                    province = this.state.Value,
                    city = this.City.Value,
                    countryCode = "EG",
                    province_code = "CR",
                    zip = "12122",
                };

                var requestBody = JsonConvert.SerializeObject(address);
                Debug.WriteLine("Request Body: "+ requestBody);
                try
                {
                    client.DefaultRequestHeaders.Add("x-auth-token", Preferences.Get("UserToken", ""));
                    Debug.WriteLine("Token-----------: " +Preferences.Get("UserToken", ""));
                    var requestResponse = await client.PostAsync("https://raneen-app.herokuapp.com/app/api/v1/client/address/attach", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                    
                    if (requestResponse.IsSuccessStatusCode)
                    {
                        var responseContent = await requestResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("Response: " + responseContent);

                        var response = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

                        if (response.success)
                        {
                            this.IsLoading = false;
                            await Shell.Current.DisplayAlert("Alert", "Saved !!", "OK");
                            //Preferences.Set("UserToken", response.jwt);
                            //await Shell.Current.Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Error", "There is an error", "OK");
                        }
                    }
                    else
                    {
                        string error = await requestResponse.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<ErrorResponse>(error);
                        Debug.WriteLine("Request Error: " + error);
                        await Shell.Current.DisplayAlert("Error", response.error.ToString(), "OK");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine("Exception: " + e);
                    await Shell.Current.DisplayAlert("ALert", "Request time-out please try again.", "OK");
                }

                IsLoading = false;
            }
        }

        #endregion
    }
}