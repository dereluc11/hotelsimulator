using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de reception klasse die overerft van de abstractRoom klasse
    /// </summary>
    public class Reception : AbstractRoom
    {
        //maak de properties aan
        public List<Bedroom> AllBedrooms;
        public Queue<Guest> WaitingLine;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bedrooms">Geef de roomlist mee van het hotel</param>
        public Reception(List<AbstractRoom> bedrooms) : base()
        {
            //stel alle properties in
            AreaType = "Reception";
            PositionX = 1;
            PositionY = 0;
            DimensionX = 2;
            DimensionY = 1;

            sprite = "Reception";

            AllBedrooms = new List<Bedroom>();
            WaitingLine = new Queue<Guest>();

            //haal alle bedrooms uit de roomlist die je mee hebt gekregen
            foreach(AbstractRoom room in bedrooms)
            {
                if (room.AreaType == "Room")
                {
                    AllBedrooms.Add((Bedroom)room);
                }
            }
            
        }

        /// <summary>
        /// roep deze functie om een kamer te geven aan de gast
        /// </summary>
        /// <param name="guestWish">geef de wens van de gast mee</param>
        /// <returns></returns>
        public AbstractRoom giveRoom(int guestWish)
        {
            Bedroom room = null;

            //zoek in de lijst of er een kamer beschikbaar is
            if ((from x in AllBedrooms where x.Classification == x.ClassificationDictionary[guestWish] && x.Taken == false select x).ToList().Any())
            {
                //haal de kamer op
                room = (from x in AllBedrooms where x.Classification == x.ClassificationDictionary[guestWish] && x.Taken == false select x).ToList().Last();
            }            

            //als die er niet is return dan null
            if (room == null)
            {
                return null;
            }

            //zet de kamer op taken zodat maar 1 gast de kamer krijgt
            room.Taken = true;

            //stuur de kamer terug
            return room;
        }

    }
}
