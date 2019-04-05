using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    public class ElevatorShaft : AbstractRoom
    {
        //extra properties voor de elevator shaft
        public Elevator elevator {get; set;}
        public List<AbstractHuman> guestWaiting { get; set; }

        /// <summary>
        /// de constructor
        /// </summary>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="dimX">geef de x dimensie mee</param>
        /// <param name="dimY">geef de y dimensie mee</param>
        /// <param name="posX">geef de x positie mee</param>
        /// <param name="posY">geef de y positie mee</param>
        public ElevatorShaft(string areatype, int dimX, int dimY, int posX, int posY) : base()
        {
            //zet de juiste areatype erin
            AreaType = areatype;

            //zet de juiste dimensies erin
            DimensionX = dimX;
            DimensionY = dimY;

            //zet de juiste posities erin
            PositionX = posX;
            PositionY = posY;

            //maak de guestWaiting list aan
            guestWaiting = new List<AbstractHuman>();

            //zet de juiste sprite erin
            sprite = "Elevator_Shaft";

        }

        /// <summary>
        /// roep deze functie om de lift in de elevator shaft te zetten
        /// </summary>
        /// <param name="inputElevator">geef een reference van de lift mee</param>
        public void setElevator(ref Elevator inputElevator)
        {
            //zet de input lift in je eigen lift variable
            this.elevator = inputElevator;
        }

        /// <summary>
        /// roep deze functie aan als je een call wilt maken voor de lift
        /// </summary>
        /// <param name="call">Geef een ElevatorCallTemplate mee met de juiste gegevens</param>
        public void CallElevator(ElevatorCallTemplate call)
        {
            //roep de functie van de lift aan om de call toe te voegen en geef de call mee
            this.elevator.NotifyCall(call);
        }


    }
}
