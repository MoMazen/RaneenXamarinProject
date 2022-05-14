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
    public partial class CategoryProductsPage : ContentPage
    {
        string categoryName;
        public CategoryProductsPage(string categoryName)
        {
            this.categoryName = categoryName;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = new CategoryProductsPageViewModel(categoryName);
        }
    }
}