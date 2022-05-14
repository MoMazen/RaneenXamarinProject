using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryProductsPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly HttpClient httpClient = new HttpClient();
        private ObservableCollection<Product> products=new ObservableCollection<Product>();
      

        private string previewImage;

        private Command itemSelectedCommand;

        private Command favouriteClickCommand;

        private string cartItemCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CategoryProductsPageViewModel" /> class.
        /// </summary>
        public CategoryProductsPageViewModel(string categoryName)
        {
            getProducts(categoryName);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        [DataMember(Name = "products")]
        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }

            set
            {
               
                if (this.products == value)
                {
                    return;
                }


                this.SetProperty(ref this.products, value);
            }


        }

       
      

        public string CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }

            set
            {
                this.SetProperty(ref this.cartItemCount, value);
            }
        }

        public string PreviewImage
        {
            get { return this.previewImage; }
            set { this.previewImage = value; }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        public Command FavouriteClickCommand
        {
            get { return this.favouriteClickCommand ?? (this.favouriteClickCommand = new Command(this.FavouriteClick)); }
        }

        #endregion

        #region Methods

        private async void FavouriteClick(object attachedObject)
        {

            if (Preferences.ContainsKey("UserToken"))
            {
                var product = attachedObject as Product;
                var wishListItems = new List<Product>();
                var wishListItemsString = "";

                var clientData = await getCurrentUserData();

                if(clientData != null)
                {
                    if(clientData.wishList != null && clientData.wishList != "")
                    {
                        wishListItems = JsonConvert.DeserializeObject<List<Product>>(clientData.wishList);
                    }
                }

                if (product.IsFavourite)
                {
                    // remove from wishlist

                    var matchedItem = wishListItems.Where(i => i._id == product._id);
                    if (matchedItem.Any())
                    {
                        wishListItems.Remove(matchedItem.FirstOrDefault());
                    }

                    product.IsFavourite = false;

                    wishListItemsString = JsonConvert.SerializeObject(wishListItems);

                    clientData.wishList = wishListItemsString;

                    await App.Database.SaveClientDataAsync(clientData);

                    await PageExtension.DisplayToastAsync(Shell.Current, "Product removed from wishList");
                }
                else
                {
                    // add to wishlist

                    product.IsFavourite = true;
                    wishListItems.Add(product);

                    wishListItemsString = JsonConvert.SerializeObject(wishListItems);

                    clientData.wishList = wishListItemsString;

                    await App.Database.SaveClientDataAsync(clientData);

                    await PageExtension.DisplayToastAsync(Shell.Current, "Product added to wishList");
                }

            }
            else
            {
                var result = await Shell.Current.DisplayAlert("Alert", "You have to login first!.", "OK", "CANCEL");

                if (result)
                {
                    await Shell.Current.GoToAsync("//account");
                }
            }
        }

        private async void ItemSelected(object attachedObject)
        {   
            await Shell.Current.Navigation.PushAsync(new DetailPage((attachedObject as Product)._id));  
        }

        private async void getProducts(string categoryName)
        {
            IsLoading = true;

            try
            {
                var wishList = await getWishList();

                string jsonResponse = await httpClient.GetStringAsync("https://raneen-app.herokuapp.com/app/api/v1/products");
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                if (response.success == true)
                {
                    var allProducts = JsonConvert.DeserializeObject<ObservableCollection<Product>>(response.data.ToString());
                    if (categoryName == null)
                    {
                        foreach (var product in allProducts)
                        {
                            var matchedProduct = wishList.Where(i=>i._id == product._id).FirstOrDefault();
                            if (matchedProduct != null)
                            {
                                product.IsFavourite = true;
                            }
                        }

                        Products = allProducts;
                    }
                    else
                    {
                        try
                        {
                            foreach (var p in allProducts)
                            {

                                if (String.Equals(p.category, categoryName))
                                {
                                    foreach (var item in wishList)
                                    {
                                        if (item._id == p._id)
                                        {
                                            p.IsFavourite = true;
                                        }
                                    }

                                    Products.Add(p);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                        }
                    }

                }
            }catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            IsLoading = false;
        }

        private async Task<List<Product>> getWishList()
        {
            var wishList = new List<Product>();
            var clientData = await getCurrentUserData();
            if (clientData != null)
            {
                if (clientData.wishList != null && clientData.wishList != "")
                {
                    string WishListItemsString = clientData.wishList;
                    wishList = JsonConvert.DeserializeObject<List<Product>>(WishListItemsString);
                }
            }

            return wishList;
        }

        #endregion
    }
}