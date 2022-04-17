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

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Events> AllEvents
        {
            get { return allEvents; }
            set { allEvents = value; onPropertyChange("AllEvents"); }
        }

        public HomeViewModel()
        {
            eventsService = new EventsService();

            AllEvents = new ObservableCollection<Events>()
            {
                new Events(){ id="1",name="Cardify",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="2",name="Lotstring",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="3",name="Span",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="4",name="Tin",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="5",name="Fixflex",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="6",name="Cardguard",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="7",name="Zathin",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="8",name="Viva",image="http://dummyimage.com/107x206.png/dddddd/000000"},
                new Events(){ id="9",name="Subin",image="http://dummyimage.com/107x206.png/dddddd/000000"},
            };
        }

        private void onPropertyChange(string _prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
    }
}
