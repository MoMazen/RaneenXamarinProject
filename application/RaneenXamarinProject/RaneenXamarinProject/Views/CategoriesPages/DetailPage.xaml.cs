using RaneenXamarinProject.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RaneenXamarinProject.Views
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage
    {

        string productId;
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPage" /> class.
        /// </summary>
        public DetailPage(string productId)
        {
            this.productId = productId;
            this.InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = new DetailPageViewModel(productId);
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                this.Rotator.ItemTemplate = (DataTemplate)this.Resources["LandscapeTemplate"];
            }
            else
            {
                this.Rotator.ItemTemplate = (DataTemplate)this.Resources["PortraitTemplate"];
            }
        }
    }
}