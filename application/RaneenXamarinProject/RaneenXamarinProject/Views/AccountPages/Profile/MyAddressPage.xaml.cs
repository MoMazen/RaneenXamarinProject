using RaneenXamarinProject.ViewModels;
using Syncfusion.ListView.XForms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    /// <summary>
    /// Page to show my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAddressPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAddressPage" /> class.
        /// </summary>
        public MyAddressPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MyAddressPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = new MyAddressPageViewModel();
        }

        private void addButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("alert", "Heeey", "ok");
        }
    }
}