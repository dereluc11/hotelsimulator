using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// De checkIn listeners 
    /// </summary>
    class CheckInListener : HotelEventListener
    {
        //maak een hotel propertie
        private Hotel _hotel { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hotel">Geef het hotel mee</param>
        public CheckInListener(ref Hotel hotel)
        {
            //zet de hotel in je eigen propertie
            _hotel = hotel;
        }

        /// <summary>
        /// een geïmplenteerde functie van de interface
        /// </summary>
        /// <param name="evt">Het hotelevent object van de dll</param>
        public void Notify(HotelEvent evt)
        {
            //als het event type een check in is en test event bevat
            if(evt.EventType == HotelEventType.CHECK_IN && !evt.Message.Contains("TestEvent"))
            {
                //maak een nieuwe gast aan
                _hotel.WelcomeGuest(evt.Data);
            }
        }
    }

    /// <summary>
    /// een checkOut listener
    /// </summary>
    class CheckOutListener : HotelEventListener
    {
        //maak een hotel propertie
        private Hotel _hotel { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hotel">Geef het hotel mee</param>
        public CheckOutListener(ref Hotel hotel)
        {
            _hotel = hotel;
        }

        /// <summary>
        /// een geïmplenteerde functie van de interface
        /// </summary>
        /// <param name="evt">Het hotelevent object van de dll</param>
        public void Notify(HotelEvent evt)
        {
            //als het event type een check out event is
            if(evt.EventType == HotelEventType.CHECK_OUT)
            {
                //roep de checkout functie aan
                _hotel.CheckoutGuest(evt.Data);          
            }
        }
    }
}
