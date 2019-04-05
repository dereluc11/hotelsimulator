using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de schoonmaker klasse die overeft van de abstracthuman klasse
    /// </summary>
    public class Maid : AbstractHuman
    {
        //properties van de schoonmaker
        public bool working;
        public bool ReturningToBase;
        private Timer _cleanTimer;
        public AbstractRoom Reception;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="room">geef de room mee waar de schoonmaker moet beginnen</param>
        public Maid(AbstractRoom room) : base()
        {
            SetTimer();
            sprite = "maid";
            CurrentPosition = room;
            Destination = room;
            working = false;
            ReturningToBase = false;
            Reception = room;
        }

        /// <summary>
        /// roep deze functie aan als een kamer schoongemaakt moet worden
        /// </summary>
        /// <param name="needscleaning">Geef de kamer mee die schoongemaakt moet worden</param>
        public void CleanRoom(AbstractRoom needscleaning)
        {
            //verander de properties
            this.Destination = needscleaning;
            this.CurrentPosition.Weight = 0;
            //maak een pad aan
            this.SetPath();

            
            bool reached = false;

            //zolang je nog niet bij je bestemming bent is reached false
            while (this.CurrentPosition != this.Destination)
            {
                reached = false;
            }

            
            reached = true;

            //als de bestemming is bereikt roep de timer aan
            if(reached)
            {
                _cleanTimer.Enabled = true;
            }
        }

        /// <summary>
        /// roep deze functie aan om de timer in te stellen
        /// </summary>
        private void SetTimer()
        {
            _cleanTimer = new Timer(3000);
            _cleanTimer.Elapsed += Cleaning;
        }

        /// <summary>
        /// roep deze functie aan om je timer te stoppen
        /// </summary>
        public void StopTimer()
        {
            _cleanTimer.Dispose();
            _cleanTimer.Stop();
        }

        /// <summary>
        /// Deze functie word aangeroepen door de timer als de timer klaar is
        /// </summary>
        /// <param name="source">verplicht door de timer</param>
        /// <param name="e">verplicht door de timer</param>
        private void Cleaning(Object source, ElapsedEventArgs e)
        {
            //stop de timer
            _cleanTimer.Enabled = false;
            //reset de timer
            this.SetTimer();
            //stel de taken boolean van de kamer op false zodat een nieuwe gast erin kan
            ((Bedroom)this.CurrentPosition).Taken = false;
            //zet je working propertie op false zodat je een nieuwe opdracht kan krijgen
            this.working = false;
        }

        /// <summary>
        /// Roep deze functie aan om terug naar de receptie te gaan
        /// </summary>
        public void ReturnToBase()
        {
            //als de schoonmaker nog niet onderweg is voer dit uit
            if(!ReturningToBase)
            {
                //bestemming word receptie
                Destination = Reception;
                CurrentPosition.Weight = 0;
                //maak een pad
                this.SetPath();
                //maak dit true als je onderweg naar de receptie bent
                ReturningToBase = true;
            }
            //als je bij de receptie bent maak de onderweg boolean op false
            else if(this.CurrentPosition.AreaType == "Reception")
            {
                ReturningToBase = false;
            }
        }
    }
}
