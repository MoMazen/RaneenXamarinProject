using RaneenXamarinProject.Models;
using RaneenXamarinProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    /// <summary>
    /// Page to add business details such as name, email address, and phone number.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressPage" /> class.
        /// </summary>
        public AddAddressPage()
        {
            this.InitializeComponent();
        }

        private void CountryPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            (this.BindingContext as AddAddressPageViewModel).Country.Value = (CountryPicker.SelectedItem as CountryModel).Country;
        }
    }
}