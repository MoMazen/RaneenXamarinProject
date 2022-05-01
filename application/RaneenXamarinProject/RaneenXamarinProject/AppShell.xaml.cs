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
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("categories", typeof(CategoriesPage));
            Routing.RegisterRoute("deals", typeof(DealsPage));
            Routing.RegisterRoute("cart", typeof(CartPage));
            //Routing.RegisterRoute("account", typeof(AccountPage));
        }

    }
}
