using RaneenXamarinProject.Models;
using RaneenXamarinProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RaneenXamarinProject.ViewModels
{
    public class HomeViewModel:INotifyPropertyChanged
    {
        EventsService eventsService;

        private ObservableCollection<Events> allEvents;

        public ObservableCollection<Events> AllEvents
        {
            get { return allEvents; }
            set { allEvents = value; onPropertyChange("AllEvents"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel()
        {
            eventsService = new EventsService();

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
        }

        private void onPropertyChange(string _prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
    }
}
