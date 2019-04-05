using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de gast klasse die overeft van de abstracthuman klasse
    /// </summary>
    public class Guest : AbstractHuman
    {
        //properties van de gast
        public string Wish { get; set; }
        public AbstractRoom MyRoom { get; set; }
        public int GuestId { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="current">geef de room mee wat de huidige positie is van de gast</param>
        /// <param name="wish">Geef een wens mee(deze komt uit de dll)</param>
        /// <param name="checkin">Geef de kamer mee waar de gast naar toe moet(deze kamer komt van de receptie)</param>
        /// <param name="Id">Geef een id mee(komt uit de dll)</param>
        public Guest(AbstractRoom current, string wish, AbstractRoom checkin, string Id) : base()
        {
            //zet alle parameters die je mee krijgt in je properties
            Wish = wish;
            MyRoom = checkin;
            sprite = "Sim";
            CurrentPosition = current;
            Destination = checkin;
            inElevator = false;
            waiting = false;
            GuestId = Int32.Parse(Regex.Match(Id, @"\d+").Value);
            //maak een pad
            this.SetPath();
        }
    }
}
