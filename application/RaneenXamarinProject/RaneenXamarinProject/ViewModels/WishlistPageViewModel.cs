using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for wishlist page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class WishlistPageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Product> wishlistItems;

        private ObservableCollection<CartItem> cartItems;

        private bool hasItems;

        private int? cartItemCount;

        private Command cardItemCommand;

        private Command addToCartCommand;

        private Command deleteCommand;

        private Command itemTappedCommand;

        #endregion

        #region Constructor
        public WishlistPageViewModel()
        {
            initData();
        }
        #endregion

        #region Public properties

        public bool HasItems
        {
            get { return hasItems; }
            set 
            { 
                this.SetProperty(ref hasItems, value);
            }
        }


        /// <summary>
        /// Gets the property that has been bound with a list view, which displays the wishlist details.
        /// </summary>
        public ObservableCollection<Product> WishListItems
        {
            get
            {
                return this.wishlistItems;
            }

            private set
            {
                if (this.wishlistItems == value)
                {
                    return;
                }

                this.SetProperty(ref this.wishlistItems, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int? CartItemCount
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


        public ObservableCollection<CartItem> CartItems
        {
            get { return this.cartItems; }

            set
            {
                if (this.cartItems == value)
                {
                    return;
                }

                this.SetProperty(ref this.cartItems, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command will be executed when the cart button has been clicked.
        /// </summary>
        public Command CardItemCommand
        {
            get
            {
                return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked));
            }
        }

        /// <summary>
        /// Gets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command AddToCartCommand
        {
            get
            {
                return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked));
            }
        }

        /// <summary>
        /// Gets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new Command(this.DeleteClicked));
            }
        }

        /// <summary>
        /// Gets the command that is executed when the item is selected.
        /// </summary>
        public Command ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command(this.ItemSelected));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void CartClicked(object obj)
        {
            // Do something
            Shell.Current.GoToAsync("//cart");
        }

        /// <summary>
        /// Invoked when add to cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddToCartClicked(object obj)
        {
            var product = obj as Product;

            foreach (var item in CartItems)
            {
                if (item.item._id == product._id)
                {
                    await Shell.Current.DisplayAlert("Alert", "This product already exists in cart.", "OK");
                    return;
                }
            }

            var cartItem = new CartItem()
            {
                item = product,
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

        /// <summary>
        /// Invoked when an delete button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void DeleteClicked(object obj)
        {
            IsLoading = true;

            WishListItems.Remove(obj as Product);

            var wishListItemsString = "";
            if(wishlistItems.Count > 0)
            {
                wishListItemsString = JsonConvert.SerializeObject(WishListItems);
            }

            var clientData = new ClientData()
            {
                Token = Preferences.Get("UserToken", ""),
                wishList = wishListItemsString
            };

            App.Database.SaveClientDataAsync(clientData);

            HasItems = wishlistItems.Count > 0;

            IsLoading = false;
        }

        /// <summary>
        /// Invoked when item is clicked.
        /// </summary>
        private void ItemSelected(object obj)
        {
            // Do something
            Shell.Current.Navigation.PushAsync(new DetailPage((obj as Product)._id));
        }

        private async void initData()
        {
            GetWishList();
            GetCartItems();
        }

        /// <summary>
        /// This method is used to get the products from json.
        /// </summary>
        /// <param name="products">The products</param>
        private async void GetWishList()
        {
            this.WishListItems = new ObservableCollection<Product>();

            var clientData = await getCurrentUserData();
            if (clientData != null)
            {
                if (clientData.wishList != null && clientData.wishList != "")
                {
                    string WishListItemsString = clientData.wishList;
                    WishListItems = JsonConvert.DeserializeObject<ObservableCollection<Product>>(WishListItemsString);
                }
            }

            HasItems = WishListItems.Count > 0;
        }

        private async void GetCartItems()
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

        #endregion
    }
}
