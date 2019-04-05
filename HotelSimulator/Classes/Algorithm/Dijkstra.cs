using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    public class Dijkstra
    {

        //private variable voor de functies
        private List<AbstractRoom> _open = new List<AbstractRoom>();
        private List<AbstractRoom> _path = new List<AbstractRoom>();
        private List<AbstractRoom> _reset = new List<AbstractRoom>();

        /// <summary>
        /// Deze functie moet je aanroepen als je kortste pad wilt hebben
        /// </summary>
        /// <param name="start">Geef het begin punt mee</param>
        /// <param name="end">Geef het eind punt mee</param>
        /// <returns>return een list met het path erin</returns>
        public List<AbstractRoom> DijkstraFunction(AbstractRoom start, AbstractRoom end)
        {

            //leeg de _reset lijst en _path lijst
            _reset.Clear();
            _path.Clear();

            //voeg start van de dijkstra toe aan reset lijst
            _reset.Add(start);

            //maak een tijdelijke variabele van een abstractroom die je wilt "bezoeken"
            AbstractRoom thisRoom = start;


            //bekijk de abstractroom en als dit niet het einde is dat ga de while loop in
            while (!Visit(thisRoom, end))
            {
                if(_open.Any())
                {
                    //verander je tijdelijke variabele naar de volgende room die je wilt bezoeken
                    thisRoom = _open.Aggregate((l, r) => l.Weight < r.Weight ? l : r);
                }                       
            }

            //maak het pad
            MakePath(end);

            //reset de waardes van de kamers
            foreach (AbstractRoom r in _reset)
            {
                r.Last = null;
                r.Weight = Int32.MaxValue / 2;
            }

            //stuur het pad terugs
            return _path;
        }

        /// <summary>
        /// De functie bezoekt daadwerkelijk de room
        /// </summary>
        /// <param name="thisRoom">De room die je wilt bezoeken</param>
        /// <param name="end">de eindbestemmings room</param>
        /// <returns>return true als het einde bereikt is anders false</returns>
        private bool Visit(AbstractRoom thisRoom, AbstractRoom end)
        {

            //als de room die we bezoeken hetzelfde is al het einde die we willen bereiken return true
            if(thisRoom ==  end)
            {
                return true;
            }

            //als de room die je bezoekt in de _open lijst staat haal hem erdan uit
            if(_open.Contains(thisRoom))
            {
                //haal de huidige room uit de _open lijst
                _open.Remove(thisRoom);

                //zet kamer in een reset lijst
                _reset.Add(thisRoom);
            }


            //voor elke buur die de huidige room heeft bekijken
            foreach (KeyValuePair<AbstractRoom, int> x in thisRoom.Neighbours)
            {
                //een nieuwe afstand bereken voor de totalte afstand
                int newWeight = thisRoom.Weight + x.Value;

                //als de nieuwe afstand kleiner is dan die er al berekend is ga dan de if in
                if(newWeight < x.Key.Weight)
                {
                    //zet de nieuwe afstand in de room
                    x.Key.Weight = newWeight;

                    //zet in de buur dat dit de vorige room is voor het path maken
                    x.Key.Last = thisRoom;


                    //zet kamer in een reset lijst
                    _reset.Add(x.Key);

                    //zet de buur in de list van rooms die we nog moeten bezoeken
                    _open.Add(x.Key);
                }               
            }

            //als de room niet het einde is dan return false
            return false;
        }


        /// <summary>
        /// deze functie maakt de daadwerkelijke path
        /// </summary>
        /// <param name="thisRoom">de room die je wilt toevoegen aan de path</param>
        /// <returns>deze functie returnd het path</returns>
        private void MakePath(AbstractRoom thisRoom)
        {
            //als de propertie last van de room niet null is dan moeten we daar eerst naar toe
            if(thisRoom.Last != null)
            {
                MakePath(thisRoom.Last);
            }

            //zet de room in de list
            _path.Add(thisRoom);
        }
    }
}
