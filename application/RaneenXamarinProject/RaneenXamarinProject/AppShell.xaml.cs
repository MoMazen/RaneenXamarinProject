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
        }

        private async void TabBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.Navigation.PopToRootAsync(false);
            }
        }
    }
}
