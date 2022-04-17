using Newtonsoft.Json;
using Plugin.FacebookClient;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Validators;
using RaneenXamarinProject.Validators.Rules;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        HttpClient client;
        IFacebookClient facebookClient;     // for Oauth
        private ValidatableObject<string> password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigation navigation)
        {
            this.InitializeProperties();
            this.AddValidationRules();
            
            this.Navigation = navigation;

            this.client = new HttpClient();
            this.facebookClient = CrossFacebookClient.Current;  // for Oauth

            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        public INavigation Navigation { get; set; }

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
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

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isEmailValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                var loginData = new Customer() { email = Email.Value, password = Password.Value };

                var loginDataJson = JsonConvert.SerializeObject(loginData);

                var resultString = await client.PostAsync("API URL", new StringContent(loginDataJson));

                //var result = JsonConvert.DeserializeObject<Response>(resultString);
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do Something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// Oauth method
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SocialLoggedIn(object obj)
        {

            Debug.WriteLine("HERE!!");
            try
            {
                if (facebookClient.IsLoggedIn)
                {
                    facebookClient.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null) return;

                    switch (e.Status)
                    {

                        case FacebookActionStatus.Completed:
                            Debug.WriteLine("Data:" + e.Data);
                            Debug.WriteLine("Message:" + e.Message);
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));

                            //Application.Current.Properties.Add("userLogin", facebookProfile);
                            Preferences.Set("userLogin", e.Data);
                            //await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    //Xamarin essential shared pref

                    facebookClient.OnUserData -= userDataDelegate;
                };

                facebookClient.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await facebookClient.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}