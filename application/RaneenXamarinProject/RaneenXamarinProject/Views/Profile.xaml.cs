using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            //this.CurrentUser = currentUser;
            //this.userService = userService;
            InitializeComponent();

            if(SharedData.Navigation != null && SharedData.loginPage != null)
            {
                SharedData.Navigation.RemovePage(SharedData.loginPage);
            }

            //root.BindingContext = this.CurrentUser;
            //updateFields();
            //setImagesSource();
        }

        protected override async void OnAppearing()
        {
            //if (!Preferences.ContainsKey("UserToken"))
            //{
                base.OnAppearing();
            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, Color = Color.Orange};
            HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("x-auth-token", Preferences.Get("UserToken", ""));
                var requestResult = await httpClient.GetAsync("https://raneen-app.herokuapp.com/app/api/v1/Profile/me");

                if (requestResult.IsSuccessStatusCode)
                {
                    var responseContent = await requestResult.Content.ReadAsStringAsync();
                    Debug.WriteLine("Response: " + responseContent);

                    var response = JsonConvert.DeserializeObject<Response>(responseContent);

                    if (response.success)
                    {

                        Customer customer = JsonConvert.DeserializeObject<Customer>(response.data.ToString());
                        Debug.WriteLine("customer : " + customer.email);
                    }
                    activityIndicator.IsRunning = false;
                }
            //}

        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            Preferences.Remove("UserToken");
            await Navigation.PopToRootAsync();
        }

        //private void updateFields()
        //{
        //    lblUserName.Text = currentUser.UserName;
        //    txtUniversity.Text = currentUser.University;
        //    txtAddress.Text = currentUser.Address;
        //    txtMobile.Text = currentUser.Mobile;

        //    profileImg.Source = ImageSource.FromFile(currentUser.ImageName);
        //}

        //private void setImagesSource()
        //{
        //    if(Device.RuntimePlatform == Device.Android)
        //    {
        //        profileImg.Source = ImageSource.FromFile("android.png");
        //        coverImg.Source = ImageSource.FromFile("androidCover.png");
        //    }
        //    else if(Device.RuntimePlatform == Device.UWP)
        //    {
        //        profileImg.Source = ImageSource.FromFile("assets/windows.png");
        //        coverImg.Source = ImageSource.FromFile("assets/windowsCover.png");
        //    }
        //}

        //private async void btnEdit_Click(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new EditData(CurrentUser, userService));
        //}

        //private void btnLogout_Click(object sender, EventArgs e)
        //{
        //    Application.Current.MainPage = new Login(userService);
        //}
    }
}