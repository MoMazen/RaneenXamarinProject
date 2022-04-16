using RaneenXamarinProject.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RaneenXamarinProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
            Routing.RegisterRoute(nameof(DealsPage), typeof(DealsPage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
            Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
            // 
        }

    }
}
