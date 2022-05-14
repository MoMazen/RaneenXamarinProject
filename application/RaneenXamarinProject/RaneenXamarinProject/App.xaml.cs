//using RaneenXamarinProject.Services;
using RaneenXamarinProject.Data;
using RaneenXamarinProject.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace RaneenXamarinProject
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        static ClientDatabase database;

        // Create the database connection as a singleton.
        public static ClientDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ClientDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Clients.db3"));
                }
                return database;
            }
        }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjE4NTI0QDMyMzAyZTMxMmUzMFhKY1dyU01NV0l3TEVzaVBpVjhHOFI2REM3eng5aExSZTlteU5BeWZZUGc9");

            InitializeComponent();

            //DependencyService.Register<MockDataStore>();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NoInternetConnectionPage();
            }
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                // Connection to internet is available
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NoInternetConnectionPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
