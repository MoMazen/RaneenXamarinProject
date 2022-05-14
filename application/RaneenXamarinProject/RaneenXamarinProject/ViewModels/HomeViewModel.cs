using RaneenXamarinProject.Models;
using RaneenXamarinProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace RaneenXamarinProject.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        private Command itemSelectedCommand;

        private ObservableCollection<Events> allEvents;

        public ObservableCollection<Events> AllEvents
        {
            get { return allEvents; }
            set 
            { 
                this.SetProperty(ref allEvents, value); 
            }
        }

        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        public HomeViewModel()
        {
            AllEvents = new ObservableCollection<Events>()
            {
                new Events(){ id="1",name="Cardify",image="offers1.png"},
                new Events(){ id="2",name="Lotstring",image="offers2.png"},
                new Events(){ id="3",name="Span",image="offers3.png"},
                new Events(){ id="4",name="Tin",image="offers4.png"},
                new Events(){ id="5",name="Fixflex",image="offers5.png"},
                //new Events(){ id="6",name="Cardguard",image="offers/offers1.png"},
                //new Events(){ id="7",name="Zathin",image="offers/offers1.png"},
                //new Events(){ id="8",name="Viva",image="offers/offers1.png"},
                //new Events(){ id="9",name="Subin",image="offers/offers1.png"},
            };

            getUser();
        }

        private void ItemSelected(object obj)
        {
            Shell.Current.GoToAsync("//categories");
        }

        private async void getUser()
        {
            SharedData.CurrentUser = await getCurrentUser();
        }
    }
}
