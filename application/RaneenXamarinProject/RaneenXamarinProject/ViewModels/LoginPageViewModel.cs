using Newtonsoft.Json;
using Plugin.FacebookClient;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Validators;
using RaneenXamarinProject.Validators.Rules;
using RaneenXamarinProject.Views;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Profile = RaneenXamarinProject.Views.Profile;

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
        public LoginPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();

            this.client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(4);
            this.facebookClient = CrossFacebookClient.Current;  // for Oauth

            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

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
            IsLoading = true;   // activate loading
            if (this.AreFieldsValid())
            {
                Debug.WriteLine("Login clicked");
                var loginData = new Customer() { email = Email.Value, password = Password.Value };

                var jsonLoginData = JsonConvert.SerializeObject(loginData);

                try
                {
                    var httpResponseMessage = await client.PostAsync("https://raneen-app.herokuapp.com/app/api/v1/auth/login", new StringContent(jsonLoginData, Encoding.UTF8, "application/json"));

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
                    }
                    else
                    {
                        string error = await httpResponseMessage.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<ErrorResponse>(error);
                        Debug.WriteLine("Request Error: " + error);
                        await SharedData.currentPage.DisplayAlert("Error", response.error.ToString(), "OK");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await SharedData.currentPage.DisplayAlert("ALert", "Request time-out please try again.", "OK");
                }
                IsLoading = false;      // deActivate loading
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do Something
            SharedData.Navigation.PushAsync(new SignUpPage());
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
                            IsLoading = true;
                            Debug.WriteLine("Data:" + e.Data);
                            Debug.WriteLine("Message:" + e.Message);
                            
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));

                            //Application.Current.Properties.Add("userLogin", facebookProfile);
                            //await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());

                            var profile = new JsonFacebookProfile()
                            {
                                email = facebookProfile.email,
                                firstName = facebookProfile.first_name,
                                lastName = facebookProfile.last_name,
                                userId = facebookProfile.id
                            };

                            string jsonFacebookProfile = JsonConvert.SerializeObject(profile);

                            Debug.WriteLine("Request Body: " + jsonFacebookProfile);

                            var httpResponseMessage = await client.PostAsync("https://raneen-app.herokuapp.com/app/api/v1/oauth/login", new StringContent(jsonFacebookProfile, Encoding.UTF8, "application/json"));

                            if (httpResponseMessage.IsSuccessStatusCode)
                            {
                                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                                Debug.WriteLine("Response: " + responseContent);

                                var response = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

                                if (response.success)
                                {
                                    Debug.WriteLine("Iam Here!!");
                                    Preferences.Set("UserToken", response.jwt);
                                    IsLoading = false;
                                    await SharedData.Navigation?.PopToRootAsync();
                                }
                            }
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    //Xamarin essential shared pref

                    facebookClient.OnUserData -= userDataDelegate;
                };

                facebookClient.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "birthday", "last_name" };
                string[] fbPermisions = { "email" };
                FacebookResponse<string> faceResponse = await facebookClient.RequestUserDataAsync(fbRequestFields, fbPermisions);

                Debug.WriteLine("FaceResponse: " + faceResponse.Data +"||"+faceResponse.Status + "||" + faceResponse.Message);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}