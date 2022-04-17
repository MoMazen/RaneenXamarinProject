using RaneenXamarinProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaneenXamarinProject.Services
{
    public class EventsService
    {
        List<Events> events;

        public EventsService()
        {
            events = new List<Events>()
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

        #region Methods
        public List<Events> GetEvents()
        {
            return events;
        }
        #endregion
    }
}
