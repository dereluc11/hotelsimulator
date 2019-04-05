using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de hallway klasse die overerft van de Abstractroom klasse
    /// </summary>
    class Hallway : AbstractRoom
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="dimX">geef de x dimensie mee</param>
        /// <param name="dimY">geef de y dimensie mee</param>
        /// <param name="posX">geef de x positie mee</param>
        /// <param name="posY">geef de y positie mee</param>
        public Hallway(string areatype, int dimX, int dimY, int posX, int posY) : base()
        {
            //stel alle properties in
            AreaType = areatype;

            DimensionX = dimX;
            DimensionY = dimY;

            PositionX = posX;
            PositionY = posY;

            sprite = "Hallway";

        }
    }
}
