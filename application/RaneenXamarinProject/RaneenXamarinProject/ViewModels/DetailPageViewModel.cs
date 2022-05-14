using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.CommunityToolkit.Extensions;
using System.Linq;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class DetailPageViewModel : BaseViewModel
    {
        #region Fields

  
        Product selectedProduct;
   
        private ObservableCollection<CartItem> cartItems= new ObservableCollection<CartItem>();

        private ObservableCollection<Product> wishListItems= new ObservableCollection<Product>();

        //private int? cartItemCount;

        private Command addToCartCommand;

        private Command favouriteCommand;

        private Command cardItemCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DetailPageViewModel" /> class.
        /// </summary>
        public DetailPageViewModel(string productId)
        {
            getWishListItems();
            getCartItems();
            getSelectedProduct(productId);
        }

        #endregion

        #region Public properties

        public ObservableCollection<CartItem> CartItems
        {
            get
            {
                return this.cartItems;
            }

            set
            {
                if (this.cartItems == value)
                {
                    return;
                }

                this.SetProperty(ref this.cartItems, value);
            }
        }

        public ObservableCollection<Product> WishListItems
        {
            get
            {
                return this.wishListItems;
            }

           set
            {
                if (this.wishListItems == value)
                {
                    return;
                }

                this.SetProperty(ref this.wishListItems, value);
            }
        }

        public Product SelectedProduct
        {
            get => selectedProduct;

            set
            {
                if (selectedProduct == value) return;

                this.SetProperty(ref this.selectedProduct, value);
            }
        }

        #endregion

        #region Commands

        public Command AddToCartCommand
        {
            get
            {
                return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked));
            }
        }

        public Command FavouriteCommand
        {
            get
            {
                return this.favouriteCommand ?? (this.favouriteCommand = new Command(this.FavouriteClicked));
            }
        }

        public Command CardItemCommand
        {
            get
            {
                return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked));
            }
        }

        #endregion

        #region Methods

        private async void AddToCartClicked(object obj)
        {
            if (Preferences.ContainsKey("UserToken"))
            {
                foreach (var item in CartItems)
                {
                    if (item.item._id == selectedProduct._id)
                    {
                        await Shell.Current.DisplayAlert("Alert", "This product already exists in cart.", "OK");
                        return;
                    }
                }

                var cartItem = new CartItem()
                {
                    item = selectedProduct,
                    amount = 1,
                };

                CartItems.Add(cartItem);
                var cartItemsString = JsonConvert.SerializeObject(CartItems);

                var clientData = new ClientData()
                {
                    Token = Preferences.Get("UserToken", ""),
                    cartItems = cartItemsString,
                };

                await App.Database.SaveClientDataAsync(clientData);

                await PageExtension.DisplayToastAsync(Shell.Current, "Product added to cart");
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

        private async void getSelectedProduct(string productId)
        {
            IsLoading = true;
            string jsonResponse = await httpClient.GetStringAsync("https://raneen-app.herokuapp.com/app/api/v1/products");
            var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            if (response.success == true)
            {
                var allProducts = JsonConvert.DeserializeObject<List<Product>>(response.data.ToString());
                try
                {
                    foreach (var p in allProducts)
                    {

                        if (String.Equals(p._id, productId))
                        {
                            foreach (var item in WishListItems)
                            {
                                if(item._id == p._id)
                                {
                                    p.IsFavourite = true;
                                }
                            }
                            SelectedProduct = p;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }

            }

            IsLoading = false;
        }

        private async void getWishListItems()
        {
            WishListItems = new ObservableCollection<Product>();

            var clientData = await getCurrentUserData();
            if(clientData != null)
            {
                if(clientData.wishList != null && clientData.wishList != "")
                {
                    string WishListItemsString = clientData.wishList;
                    WishListItems = JsonConvert.DeserializeObject<ObservableCollection<Product>>(WishListItemsString);
                }
            }
        }

        private async void getCartItems()
        {
            CartItems = new ObservableCollection<CartItem>();

            var clientData = await getCurrentUserData();
            if (clientData != null)
            {
                if (clientData.cartItems != null && clientData.cartItems != "")
                {
                    string cartItemsString = clientData.cartItems;
                    CartItems = JsonConvert.DeserializeObject<ObservableCollection<CartItem>>(cartItemsString);
                }
            }
        }

        private async void CartClicked(object obj)
        {
            await Shell.Current.GoToAsync("//cart");
        }

        private async void FavouriteClicked(object obj)
        {
            if (Preferences.ContainsKey("UserToken"))
            {
                var wishListItemsString = "";
                if (SelectedProduct.IsFavourite)
                {
                    // remove from wishlist

                    var matchedItem = wishListItems.Where(i=> i._id == SelectedProduct._id);
                    if (matchedItem.Any())
                    {
                        WishListItems.Remove(matchedItem.FirstOrDefault());
                    }

                    SelectedProduct.IsFavourite = false;

                    if (WishListItems.Count > 0)
                    {
                        wishListItemsString = JsonConvert.SerializeObject(WishListItems);
                    }

                    ClientData clientData = new ClientData()
                    {
                        Token = Preferences.Get("UserToken", ""),
                        wishList = wishListItemsString,
                    };

                    await App.Database.SaveClientDataAsync(clientData);

                    await PageExtension.DisplayToastAsync(Shell.Current, "Product removed from wishList");

                }
                else
                {
                    // add to wishlist

                    SelectedProduct.IsFavourite = true;
                    WishListItems.Add(SelectedProduct);

                    wishListItemsString = JsonConvert.SerializeObject(WishListItems);

                    ClientData clientData = new ClientData()
                    {
                        Token = Preferences.Get("UserToken", ""),
                        wishList = wishListItemsString,
                    };

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

        #endregion
    }
}
