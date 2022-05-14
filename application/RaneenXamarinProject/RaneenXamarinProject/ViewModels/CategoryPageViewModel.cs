using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for Category page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly HttpClient httpClient = new HttpClient();

        private ObservableCollection<Category> categories;

        private Command categorySelectedCommand;

        private INavigation navigation;
        
        #endregion

        #region Constructor
        public CategoryPageViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            categorySelectedCommand = new Command(CategorySelected);
            getCategories();
        }
        public CategoryPageViewModel()
        {
            getCategories();
        }
        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        [DataMember(Name = "categories")]
        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }

            set
            {
                if (this.categories == value)
                {
                    return;
                }
             

                this.SetProperty(ref this.categories, value);
            }
        }

        #endregion

        #region Command

      
        public Command CategorySelectedCommand
        {
            get { return this.categorySelectedCommand ?? (this.categorySelectedCommand = new Command(this.CategorySelected)); }
        }

        
        #endregion

        #region Methods

        public INavigation Navigation
        {
            get { return navigation; }
            set { navigation = value; }
        }
        /// <summary>
        /// Invoked when the Category is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CategorySelected(object obj)
        {
            IsLoading = true;
            await Shell.Current.Navigation.PushAsync(new CategoryProductsPage((obj as Category).name));
            IsLoading = false;
        }

        private async void getCategories() {
            IsLoading = true;
            string jsonResponse =  await httpClient.GetStringAsync("https://raneen-app.herokuapp.com/app/api/v1/categories");  
            try {
              
                
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                if (response.success == true) {
                    Categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(response.data.ToString());
                }

              
            }
            catch (Exception ex) {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
           
            IsLoading = false;
        }

        #endregion
    }
}