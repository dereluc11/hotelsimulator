using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{

    /// <summary>
    /// de abstracte klasse voor de gast en schoonmaker
    /// </summary>
    public class AbstractHuman
    {

        //properties van de abstracthuman class
        public AbstractRoom CurrentPosition { get; set; }
        public AbstractRoom Destination { get; set; }
        public Dijkstra SearchPath { get; set; }
        public string sprite { get; set; }
        public List<AbstractRoom> Path { get; set; }
        public bool inElevator { get; set; }
        public bool waiting { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public AbstractHuman()
        {
            //maak het dijkstra object aan
            SearchPath = new Dijkstra();
        }

        /// <summary>
        /// Deze functie roep je aan als je het object wilt laten bewegen
        /// </summary>
        public void Move()
        {
            //als zijn huideige positie niet zijn bestemming is moet hij bewegen
            if (CurrentPosition != Destination)
            {
                //als hij nu bij een elevetarshaft staat en zijn volgende stap ook een elevator shaft is en hij staat nog niet in de lift
                if (CurrentPosition.AreaType == "Elevatorshaft" && Path.First().AreaType == "Elevatorshaft" && inElevator == false)
                {
                    //en hij is nog niet aan het wachten of de lift
                    if (waiting == false)
                    {
                        //maak een tijdelijke elevatorshaft object van de huidige positie
                        ElevatorShaft temp = (ElevatorShaft)CurrentPosition;

                        //voeg jezelf to aan de wacht rij van de elevatorshaft
                        temp.guestWaiting.Add(this);

                        //roep een functie aan om een call doortegeven aan de elevator
                        temp.CallElevator(new ElevatorCallTemplate(this, temp, (ElevatorShaft)Path.Find(x => x.AreaType == "Elevatorshaft" && x.PositionY == Destination.PositionY)));

                        //en geef aan dat hij nu moet wachten
                        waiting = true;

                    }
                }
                //anders moet hij zelf bewegen
                else
                {
                    //als hij nit in de lift staat
                    if (inElevator == false)
                    {
                        //verandrd de huidige positie
                        CurrentPosition = Path.First();
                        //en haal de stap uit de path list
                        Path.Remove(Path.First());
                    }
                }
            }

        }

        /// <summary>
        /// deze functie roep je aan als het object een currentposition heeft en een destination om een pad te maken
        /// </summary>
        public void SetPath()
        {
            //zet de weight van de huidige positie op 0(dit is je begin punt)
            this.CurrentPosition.Weight = 0;
            //maak het daad werkelijke pad door het dijkstra object aan te roepen
            Path = new List<AbstractRoom>(SearchPath.DijkstraFunction(CurrentPosition, Destination));
        }
    }
}
