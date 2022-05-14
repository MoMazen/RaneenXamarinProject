using Newtonsoft.Json;
using RaneenXamarinProject.Models;
using RaneenXamarinProject.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaneenXamarinProject.ViewModels
{
    /// <summary>
    /// ViewModel for empty cart page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class EmptyCartPageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<CartItem> cartItems;

        private bool hasItems;

        private double totalPrice;

        private double discountPrice;

        private Command continueCommand;

        private Command removeCommand;

        private Command quantitySelectedCommand;

        private Command placeOrderCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="EmptyCartPageViewModel" /> class.
        /// </summary>
        public EmptyCartPageViewModel()
        {
            refreshCart();
        }

        #endregion

        #region Properties

        public ObservableCollection<CartItem> CartItems
        {
            get { return cartItems; }
            private set
            {
                if (this.cartItems == value)
                {
                    return;
                }

                this.SetProperty(ref this.cartItems, value);
            }
        }

        public bool HasItems
        {
            get { return hasItems; }
            set 
            {
                this.SetProperty(ref this.hasItems, value);
            }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set 
            {
                this.SetProperty(ref this.totalPrice, value);
            }
        }

        public double DiscountPrice
        {
            get { return discountPrice; }
            set
            {
                this.SetProperty(ref this.discountPrice, value);
            }
        }

        public List<object> Quantities { get; set; } = new List<object> { 1, 2, 3, 4, 5 };

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command that is executed when the Go back button is clicked.
        /// </summary>
        public Command ContinueCommand
        {
            get
            {
                return this.continueCommand ?? (this.continueCommand = new Command(this.ContinueShopping));
            }
        }

        public Command RemoveCommand
        {
            get
            {
                return this.removeCommand ?? (this.removeCommand = new Command(this.RemoveItem));
            }
        }

        public Command QuantitySelectedCommand
        {
            get { return this.quantitySelectedCommand ?? (this.quantitySelectedCommand = new Command(this.QuantitySelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command PlaceOrderCommand
        {
            get { return this.placeOrderCommand ?? (this.placeOrderCommand = new Command(this.PlaceOrderClicked)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Go back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ContinueShopping(object obj)
        {
            // Do something
            emptyingNavigationStack();

            await Shell.Current.GoToAsync("//home");
        }

        private void RemoveItem(object obj)
        {
            IsLoading = true;

            var cartItem = obj as CartItem;
            CartItems.Remove(cartItem);

            TotalPrice = totalPrice - (cartItem.item.DiscountPrice * cartItem.amount);

            DiscountPrice = totalPrice - (totalPrice * 0.07);

            HasItems = CartItems.Count > 0;

            IsLoading = false;
        }


        private void QuantitySelected(object obj)
        {
            calcTotalPrice();
        }

        public void calcTotalPrice()
        {
            totalPrice = 0;

            foreach (var item in CartItems)
            {
                TotalPrice += (item.item.DiscountPrice * item.amount);
            }

            DiscountPrice = totalPrice - (totalPrice * 0.07);
        }

        private async Task getCartItems()
        {
            IsLoading = true;
            TotalPrice = 0;

            CartItems = new ObservableCollection<CartItem>();

            ClientData clientData = await getCurrentUserData();
            if (clientData != null)
            {
                if (clientData.cartItems != null && clientData.cartItems != "")
                {
                    string cartItemsString = clientData.cartItems;
                    CartItems = JsonConvert.DeserializeObject<ObservableCollection<CartItem>>(cartItemsString);

                    CartItems.ForEach(item => item.id = item.item._id);
                }
            }

            HasItems = CartItems.Count > 0 ? true : false;

            if (HasItems)
            {
                foreach (var item in CartItems)
                {
                    TotalPrice += item.item.DiscountPrice;
                }

                DiscountPrice = totalPrice - (totalPrice * 0.07);
            }
            
            IsLoading = false;
        }

        public async void refreshCart()
        {
            await getCartItems();
        }

        public async void SaveCart()
        {
            if (Preferences.ContainsKey("UserToken"))
            {
                var cartItemsString = "";
                if(CartItems.Count > 0)
                {
                    cartItemsString = JsonConvert.SerializeObject(CartItems);
                }

                var clientData = new ClientData()
                {
                    Token = Preferences.Get("UserToken", ""),
                    cartItems = cartItemsString
                };

                await App.Database.SaveClientDataAsync(clientData);
            }
        }

        private async void PlaceOrderClicked(object obj)
        {
            var client = SharedData.CurrentUser;

            if(client != null)
            {
                if (client.address == null)
                {
                    await Shell.Current.DisplayAlert("Alert", "You have to add an address first!", "OK");
                }
                else
                {

                    //await Shell.Current.Navigation.PushAsync(new CardPaymentPage());

                    try
                    {

                        var order = new Order()
                        {
                            products = cartItems
                        };

                        var orderString = JsonConvert.SerializeObject(order);

                        Debug.WriteLine("Body:   " + orderString);

                        httpClient.DefaultRequestHeaders.Add("x-auth-token", Preferences.Get("UserToken", ""));

                        var httpResponseMessage = await httpClient.PostAsync("https://raneen-app.herokuapp.com/app/api/v1/order/add", new StringContent(orderString, Encoding.UTF8, "application/json"));

                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                            Debug.WriteLine("Response: " + responseContent);

                            var response = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

                            if (response.success)
                            {

                                var clientData = new ClientData()
                                {
                                    Token = Preferences.Get("UserToken", ""),
                                    cartItems = ""
                                };

                                await App.Database.SaveClientDataAsync(clientData);

                                await getCartItems();

                                await PageExtension.DisplayToastAsync(Shell.Current, "Your order has been placed successfully!", 1500);
                            }
                        }
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }

                }
            }
        }

        #endregion
    }
}
