using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de bedroom klasse die over erft van de Abstractroom klasse
    /// </summary>
    public class Bedroom : AbstractRoom
    {
        //properties van de bedroom klasse
        public bool Taken { get; set; }
        public Dictionary<int, string> ClassificationDictionary { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="classification">geef mee hoeveel sterren hij heeft</param>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="pos">Geef de positie mee</param>
        /// <param name="dim">geef de dimensie mee</param>
        /// <param name="id">Geef een Id mee</param>
        public Bedroom(string classification, string areatype, string pos, string dim, int id) : base()
        {
            //stel alle properties in
            Classification = classification;
            AreaType = areatype;
            Taken = false;

            Id = id;

            //maak de dictionary en vul het
            ClassificationDictionary = new Dictionary<int, string>();

            ClassificationDictionary.Add(1, "1 Star");
            ClassificationDictionary.Add(2, "2 stars");
            ClassificationDictionary.Add(3, "3 stars");
            ClassificationDictionary.Add(4, "4 stars");
            ClassificationDictionary.Add(5, "5 stars");

            DimensionX = Int32.Parse(dim.Split(',').First());
            DimensionY = Int32.Parse(dim.Split(',').Last());

            PositionX = Int32.Parse(pos.Split(',').First());
            PositionY = Int32.Parse(pos.Split(',').Last());

            //check welke sprite er nodig en stel die in
            if(Classification == "1 Star")
            {
                sprite = "Room1";
            }
            else if(Classification == "2 stars")
            {
                sprite = "Room2";
            }
            else if (Classification == "3 stars")
            {
                sprite = "Room3";
            }
            else if (Classification == "4 stars")
            {
                sprite = "Room4";
            }
            else if (Classification == "5 stars")
            {
                sprite = "Room5";
            }

        }
    }
}
