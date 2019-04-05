using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    public class Elevator : AbstractRoom
    {
        //alle nodige properties voor de lift
        public ElevatorShaft currentPosition { get; set; }
        public List<AbstractHuman> guestInElevator { get; set; }
        public List<ElevatorCallTemplate> calls { get; set; }

        private enum State { getIn, getOut, moving, idle };
        private State _status { get; set; }

        /// <summary>
        /// de constructor
        /// </summary>
        /// <param name="areatype">geef de areatype mee</param>
        /// <param name="dimX">geef de x dimensie mee</param>
        /// <param name="dimY">geef de y dimensie mee</param>
        /// <param name="posX">geef de x positie mee</param>
        /// <param name="posY">geef de y positie mee</param>
        public Elevator(string areatype, int dimX, int dimY, int posX, int posY) : base()
        {
            //zet als de lift word aangemaakt de status op idle
            _status = State.idle;

            //maak alle properties aan
            guestInElevator = new List<AbstractHuman>();
            calls = new List<ElevatorCallTemplate>();

            //zet de juiste areatype erin
            AreaType = areatype;

            //zet de juiste dimensies erin
            DimensionX = dimX;
            DimensionY = dimY;

            //zet de juiste position erin
            PositionX = posX;
            PositionY = posY;

            //zet de naam van de jusite sprite in
            sprite = "Elevator";
        }

        /// <summary>
        /// Deze functie moet je aanroepen als je de lift wilt updaten. Deze moet je aanroepen in een timer
        /// </summary>
        public void updateElevator()
        {
            //als er een call of een gast in de lift zit doe dan de juiste actie die je status aangeeft
            if (calls.Count > 0 || guestInElevator.Count > 0)
            {
                //een switch om de juiste actie te bepalen aan de hand van de status van de lift
                switch (_status)
                {
                    //als de status "getIn" is dat betekent dan de gasten in de lift gaan
                    case State.getIn:
                        //voor alle gasten die aan het wachten zijn voor de lift
                        foreach(AbstractHuman guest in currentPosition.guestWaiting.ToList())
                        {
                            //alle guest die moeten instappen zet ze in de lijst van de lift en weg bij de elevatorshaft
                            guestInElevator.Add(guest);
                            //geef aan de gast aan dat die niet meer aan het wachten is maar wel in de lift zodat de gast niet meer gaat lopen
                            guest.waiting = false;
                            guest.inElevator = true;
                            currentPosition.guestWaiting.Remove(guest);
                        }
                        //als hij klaar is met de gasten in de lift te zetten zet dan de status op moving
                        _status = State.moving;
                        break;
                        //als de status "getOut" is dat betekend dat gasten uit de lift gaan
                    case State.getOut:
                        //chech elke gast die in de lift staat
                        foreach (AbstractHuman guest in guestInElevator.ToList())
                        {
                            //check alle calls in je lijst
                            foreach (ElevatorCallTemplate call in calls.ToList())
                            {
                                //als de gast die de call heeft gemaakt en de juiste verdieping bereikt is zet dan de gast uit de lift
                                if(call.guest == guest && call.destinationFloor == this.currentPosition)
                                {
                                    //laat de gast weten dat hij niet meer in de lift staat
                                    guest.inElevator = false;
                                    guest.waiting = false;
                                    //haal de call uit de lijst
                                    calls.Remove(call);
                                    //haal de gast uit de lift lijst
                                    guestInElevator.Remove(guest);
                                }
                            }
                        }
                        //zodra de de mensen eruit zijn gelaten zet de status op getIn
                        _status = State.getIn;
                        break;
                        //als de status "moving" is dan betekend dat dat de lift aan het bewegen is
                    case State.moving:
                        //als er gast en in de lift staan ga dan naar de verdiepeing van de eerste gast in de list
                        if(guestInElevator.Count > 0)
                        {
                            GoToFloor(calls.Find(x => x.guest == guestInElevator.First()).destinationFloor);
                        }
                        //anders ga naar de versieping waar een gast te wachten staat die als eerste een call heeft gemaakt
                        else
                        {
                            GoToFloor(calls.FirstOrDefault().startFloor);
                        }
                        break;
                        //als de status "idle" is dan doet de lift niks
                    case State.idle:
                        //als de verdieping waar de lift is het zelfde is als de begin verdieping van de call verander de status
                        if(this.currentPosition == calls.FirstOrDefault().startFloor)
                        {
                            _status = State.getIn;
                        }
                        else
                        {
                            //anders ga dan naar de eerste start verdieping van de eerste call
                            GoToFloor(calls.FirstOrDefault().startFloor);
                        }
                        break;
                    default:

                        break;

                }
            }
            else
            {
                //als er geen calls of gasten in de lift staan verander dan de status naar "idle"
                _status = State.idle;
            }
        }

        /// <summary>
        /// een private function om de lift echt the laten bewegen
        /// </summary>
        /// <param name="nextFloor">geef de elevatorshaft mee die de volgende elevatorshaft is waar de lift langs komt</param>
        private void MoveElevator(ElevatorShaft nextFloor)
        {
            //zet in de current position vande lift de nieuwe shaft
            this.currentPosition = nextFloor;
            //verander de y positie van de lift zodat je de juiste positie kan tekenen
            this.PositionY = nextFloor.PositionY;

            //als er gasten zijn in de lift
            if(guestInElevator.Count > 0)
            {
                //voor elke gast in de lift
                foreach (AbstractHuman guest in guestInElevator)
                {
                    //zet de current position van de gast naar de volgende verdieping
                    guest.CurrentPosition = nextFloor;
                    //haal een stap van de path af van de gast
                    guest.Path.Remove(guest.Path.First());
                }
            }
            
        }

        /// <summary>
        /// roep deze functie aan as je de lift wilt laten bewegen
        /// </summary>
        /// <param name="nextFloor">geef de verdieping mee waar de lift de uiteindelijk moet uitkomen</param>
        private void GoToFloor(ElevatorShaft nextFloor)
        {
            //als de Y positie van de bestemming groter is dan de huidige Y positie ga dan naar boven
            if(currentPosition.PositionY < nextFloor.PositionY)
            {
                GoUp();
            }
            //als de Y positie avnde bestemming kleiner is dan de huideige Y positie ga dan naar beneden
            else if(currentPosition.PositionY < nextFloor.PositionY)
            {
                GoDown();
            }
            //als het geen van beide is dan betekend dat dat beide Y posities gelijk zijn verander dan de status naar "getOut"
            else
            {
                _status = State.getOut;
            }
        }

        /// <summary>
        /// deze fucntie roep je aan als je omhoog moet
        /// </summary>
        private void GoUp()
        {
            //kijk in de buren van de huidige elevatorshaft. Neem dan de buur waar de Y positie +1 is van de huidige positie
            ElevatorShaft temp = (ElevatorShaft)currentPosition.Neighbours.FirstOrDefault(x => x.Key.PositionY == (currentPosition.PositionY+1)).Key;
            //roep de move functie aan
            MoveElevator(temp);
        }

        /// <summary>
        /// deze functie roep je aan als je omlaag moet
        /// </summary>
        private void GoDown()
        {
            //kijk in de buren van de huidige elevator shaft. neem dan de buur waar de Y positie -1 is van de huidige positie
            ElevatorShaft temp = (ElevatorShaft)currentPosition.Neighbours.FirstOrDefault(x => x.Key.PositionY == (currentPosition.PositionY - 1)).Key;
            //roep de move funvtie aan
            MoveElevator(temp);
        }

        /// <summary>
        /// roep deze functie aan als je een call wilt toevoegen aan de call list van de lift
        /// </summary>
        /// <param name="call">geef een ElevatorCallTemplate object mee de juiste gegevens</param>
        public void NotifyCall(ElevatorCallTemplate call)
        {
            //voeg de call toe aan de list
            calls.Add(call);
        }


    }
}
