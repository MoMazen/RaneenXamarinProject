using RaneenXamarinProject.ViewModels;
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
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            this.BindingContext = new EmptyCartPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (this.BindingContext as EmptyCartPageViewModel).refreshCart();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            (this.BindingContext as EmptyCartPageViewModel).SaveCart();
        }
    }
}