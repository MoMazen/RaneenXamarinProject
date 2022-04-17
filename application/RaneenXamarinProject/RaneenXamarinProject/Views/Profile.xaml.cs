using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //root.BindingContext = this.CurrentUser;
            //updateFields();
            //setImagesSource();
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