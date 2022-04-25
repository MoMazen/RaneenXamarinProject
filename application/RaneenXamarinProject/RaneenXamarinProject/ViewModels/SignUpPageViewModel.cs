using RaneenXamarinProject.Models;
using RaneenXamarinProject.Validators;
using RaneenXamarinProject.Validators.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Essentials;
using System;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private HttpClient client;

        private ValidatableObject<string> firstName;

        private ValidatableObject<string> lastName;

        private ValidatablePair<string> password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(4);
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClickedAsync);
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName == value)
                {
                    return;
                }

                this.SetProperty(ref this.firstName, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (this.lastName == value)
                {
                    return;
                }

                this.SetProperty(ref this.lastName, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatablePair<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isEmail = this.Email.Validate();
            bool isFisrtNameValid = this.FirstName.Validate();
            bool isLastNameValid = this.LastName.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isPasswordValid && isFisrtNameValid && isLastNameValid && isEmail;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.FirstName = new ValidatableObject<string>();
            this.LastName = new ValidatableObject<string>();
            this.Password = new ValidatablePair<string>();
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.FirstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
            this.LastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
            this.Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            this.Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Re-enter Password" });
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            // Do something
            SharedData.Navigation.PopAsync();

        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClickedAsync(object obj)
        {
            IsLoading = true;

            if (this.AreFieldsValid())
            {
                // Do something
                Customer signUpData = new Customer();
                signUpData.firstName = this.FirstName.Value;
                signUpData.lastName = this.LastName.Value;
                signUpData.email = this.Email.Value;
                signUpData.password = this.Password.Item1.Value;
                signUpData.phone = "00111111111";
                signUpData.company = "CompanyName";


                string jsonSignUpData = JsonConvert.SerializeObject(signUpData);
                Debug.WriteLine("Json: " + jsonSignUpData);
                try
                {
                    var httpResponseMessage = await client.PostAsync("https://raneen-app.herokuapp.com/app/api/v1/auth/register", new StringContent(jsonSignUpData, Encoding.UTF8, "application/json"));

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        Debug.WriteLine("Response: " + responseContent);

                        var response = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

                        if (response.success)
                        {
                            Preferences.Set("UserToken", response.jwt);
                            await SharedData.Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await SharedData.currentPage.DisplayAlert("Error", "There is an error", "OK");
                        }
                    }
                    else
                    {
                        string error = await httpResponseMessage.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<ErrorResponse>(error);
                        Debug.WriteLine("Request Error: "+ error);
                        await SharedData.currentPage.DisplayAlert("Error", response.error.ToString(), "OK");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine("Exception: "+ e);
                    await SharedData.currentPage.DisplayAlert("ALert","Request time-out please try again.","OK");
                }

            }

            IsLoading = false;
        }

        #endregion
    }
}