using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de restaurant klasse die overerft van de Abstractroom
    /// </summary>
    class Restaurant : AbstractRoom
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="capacity">Geef de grootte mee van het restaurant</param>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="pos">Geef de positie mee</param>
        /// <param name="dim">geef de dimensie mee</param>
        /// <param name="id">Geef een Id mee</param>
        public Restaurant(int capacity, string areatype, string pos, string dim, int id) : base()
        {
            //stel de properties in
            Capacity = capacity;
            AreaType = areatype;

            DimensionX = Int32.Parse(dim.Split(',').First());
            DimensionY = Int32.Parse(dim.Split(',').Last());

            PositionX = Int32.Parse(pos.Split(',').First());
            PositionY = Int32.Parse(pos.Split(',').Last());

            Id = id;

            sprite = "Restaurant";

        }

    }
}
