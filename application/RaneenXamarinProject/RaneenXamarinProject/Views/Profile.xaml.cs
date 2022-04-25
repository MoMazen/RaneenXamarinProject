using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.ViewModels;
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
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var context = new ProfilePageViewModel(null);
                BindingContext = context;
                context.IsLoading = true;

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
                        customer.fullName = $"{customer.firstName} {customer.lastName}";
                        context.CustomerData = customer;
                    }

                    context.IsLoading = false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            } 
        }
    }
}