using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de cimena klasse die overerft van de abstractroom klasse
    /// </summary>
    class Cinema : AbstractRoom
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="dim">geef de dimensie mee</param>
        /// <param name="pos">Geef de positie mee</param>
        /// <param name="id">Geef een Id mee</param>
        public Cinema(string areatype, string dim, string pos, int id) : base()
        {
            //stel alle properties in
            AreaType = areatype;

            DimensionX = Int32.Parse(dim.Split(',').First());
            DimensionY = Int32.Parse(dim.Split(',').Last());

            PositionX = Int32.Parse(pos.Split(',').First());
            PositionY = Int32.Parse(pos.Split(',').Last());

            Id = id;

            sprite = "Cinema";

        }

    }
}
