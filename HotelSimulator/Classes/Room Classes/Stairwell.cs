using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// de stairwell klasse die overerft van Abstractroom
    /// </summary>
    class Stairwell : AbstractRoom
    {
        /// <summary>
        /// construtor
        /// </summary>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="dimX">geef de x dimensie mee</param>
        /// <param name="dimY">geef de y dimensie mee</param>
        /// <param name="posX">geef de x positie mee</param>
        /// <param name="posY">geef de y positie mee</param>
        public Stairwell(string areatype, int dimX, int dimY, int posX, int posY) : base()
        {
            //stel je properties in
            AreaType = areatype;

            DimensionX = dimX;
            DimensionY = dimY;

            PositionX = posX;
            PositionY = posY;

            sprite = "Stairwell";

        }
    }
}
