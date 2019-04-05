using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    public class ElevatorCallTemplate
    {
        // de properties van de class
        public AbstractHuman guest { get; set; }
        public ElevatorShaft startFloor { get; set; }
        public ElevatorShaft destinationFloor { get; set; }

        /// <summary>
        /// de constructor van de call
        /// </summary>
        /// <param name="guest">De gast die de call maakt</param>
        /// <param name="start">de floor waar de gast staat</param>
        /// <param name="destination">de floor waar de gast naar toe moet</param>
        public ElevatorCallTemplate(AbstractHuman guest, ElevatorShaft start, ElevatorShaft destination)
        {
            //zet de juiste input in de juiste properties
            this.guest = guest;
            this.startFloor = start;
            this.destinationFloor = destination;
        }
    }
}
