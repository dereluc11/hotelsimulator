using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulator.Classes
{
    /// <summary>
    ///  de abstracte klasse voor alle kamers voor het hotel
    /// </summary>
    public abstract class AbstractRoom
    {
        //properties van de abstracthuman class
        public string AreaType { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public string Classification { get; set; }
        public int Capacity { get; set; }
        public string sprite { get; set; }
        public int Id { get; set; }

        public Dictionary<AbstractRoom, int> Neighbours;
        public int Weight { get; set; }
        public AbstractRoom Last { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public AbstractRoom()
        {
            //zet je last op null
            Last = null;
            //zet de weight heel hoog
            Weight = Int32.MaxValue / 2;
            //maak de dictionary aan
            Neighbours = new Dictionary<AbstractRoom, int>();
        }

        /// <summary>
        /// roep deze functie aan als je een neighbour wilt toevoegen
        /// </summary>
        /// <param name="neighbour">geef de daad werkelijke neighbour mee(de room die je wilt koppelen)</param>
        /// <param name="distance">geef mee hoe groot de afstand is voor het dijktra algortime</param>
        public void AddNeighbour(ref AbstractRoom neighbour, int distance)
        {
            //voor de neighbour toe aan de dictionary
            Neighbours.Add(neighbour, distance);
        }
        

    }
}
