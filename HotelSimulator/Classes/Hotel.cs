using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulator.Classes;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Timers;

namespace HotelSimulator
{
    public class Hotel
    {
        //initialiseer variable
        List<JsonObject> Jsonobjectlist;
        public List<AbstractRoom> Roomlist { get; set; }
        public List<Guest> GuestList { get; set; }
        public List<Maid> Employees { get; set; }
        private Queue<AbstractRoom> _dirtyroom;
        AbstractRoom reception;

        //create the factory for all the rooms
        RoomFactory roomFactory { get; set; }

        //create the factory for all the humans
        HumanFactory humanFactory { get; set; }

        //maximum X and Y value
        int maxX;
        int maxY;
        List<Point> positions = new List<Point>();

        //update hotel timer
        private Timer _hotelTimer;

        /// <summary>
        /// Constructor voor Hotel
        /// </summary>
        public Hotel()
        {
            //maak nieuwe lijsten aan, maak een queue en zet de timer aan
            Roomlist = new List<AbstractRoom>();
            Employees = new List<Maid>();
            GuestList = new List<Guest>();
            _dirtyroom = new Queue<AbstractRoom>();
            roomFactory = new RoomFactory();
            humanFactory = new HumanFactory();
            SetTimer();

            
            //open de streamreader
            using (StreamReader r =
                new StreamReader(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "HotelLayout/Hotel.layout")))
            {
                //String met alle data uit de layout file
                string json = r.ReadToEnd();

                //desirialiseer alle string json informatie naar een jsonObject die toegevoegd kunnen worden aan roomlist
                Jsonobjectlist = JsonConvert.DeserializeObject<List<JsonObject>>(json);

                //zet elke json object om naar de juiste kamer en voeg hem toe aan roomlist
                foreach (JsonObject room in Jsonobjectlist)
                {
                    switch (room.AreaType)
                    {
                        case "Cinema":
                            Roomlist.Add(roomFactory.CreateCinema(room.AreaType, room.Dimension, room.Position, room.Id));
                            break;
                        case "Restaurant":
                            Roomlist.Add(roomFactory.CreateRestaurant(room.Capacity, room.AreaType, room.Position, room.Dimension, room.Id));
                            break;
                        case "Room":
                            Roomlist.Add(roomFactory.CreateBedroom(room.Classification, room.AreaType, room.Position, room.Dimension, room.Id));
                            break;
                        case "Fitness":
                            Roomlist.Add(roomFactory.CreateGym(room.AreaType, room.Dimension, room.Position, room.Id));
                            break;

                    }
                }
            }

            //maak een nieuwe receptie en voeg hem toe aan roomlist
            reception = roomFactory.CreateReception(Roomlist);
            Roomlist.Add(reception);

            //voeg 2 schoonmaker toe aan de Employees lijst
            Employees.Add((Maid)humanFactory.CreateMaid(reception));
            Employees.Add((Maid)humanFactory.CreateMaid(reception));

            //een for each om te bepalen welke posities al in genomen zijn
            foreach (AbstractRoom r in Roomlist)
            {
                //hou max X bij
                if (r.PositionX > maxX)
                {
                    maxX = r.PositionX;
                }

                //hou max Y bij
                if (r.PositionY > maxY)
                {
                    maxY = r.PositionY;
                }


                //als de dimensie meer dan 1 is voeg dan alle posities van die kamer toe aan positions
                if (r.DimensionX > 1 || r.DimensionY > 1)
                {
                    for (int x = 0; x < r.DimensionX; x++)
                    {
                        for (int y = 0; y < r.DimensionY; y++)
                        {
                            positions.Add(new Point(r.PositionX + x, r.PositionY + y));
                        }
                    }
                }
                else // als een kamer 1 X 1 is voeg hem dan toe aan positions lijst
                {
                    positions.Add(new Point(r.PositionX, r.PositionY));
                }
            }
            
            //maak een lift aan
           Elevator elevator = (Elevator)roomFactory.CreateElevator("Elevator", 1, 1, maxX + 1, 0);

            //voeg lift en trap toe aan roomlist en zijn positie aan de positions lijst
            for (int i = 0; i <= maxY; i++)
            {
                //voeg de trap en zijn positie toe
                Roomlist.Add(roomFactory.CreateStairwell("Stairwell", 1, 1, 0, i));
                positions.Add(new Point(0, i));

                //maak een nieuwe shaft geef hem een refference naar de lift en voeg hem toe aan room list
                ElevatorShaft tempShaft = (ElevatorShaft)roomFactory.CreateElevatorshaft("Elevatorshaft", 1, 1, maxX + 1, i);
                tempShaft.setElevator(ref elevator);
                Roomlist.Add(tempShaft);

                //zet de lift op de positie van lift schaft
                if (i == 0)
                {
                    elevator.currentPosition = tempShaft;
                }

                //voeg de positie aan positions lijst
                positions.Add(new Point(maxX + 1, i));                
            }


            
            //voeg hallway toe om lege ruimtes op te vullen
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    //als de positie niet gevuld is maak dan een hallway met die positie
                    if (!positions.Contains(new Point(x, y)))
                    {
                        Roomlist.Add(roomFactory.CreateHallway("Hallway", 1, 1, x, y));
                    }
                }
            }

            //buren toe voegen
            foreach (AbstractRoom r in Roomlist)
            {
                    //als het een trap of een lift schaft is
                    if (r.AreaType == "Stairwell" || r.AreaType == "Elevatorshaft")
                    {
                        //kijk of er boven buren zijn
                        if (Roomlist.Any(x => x.PositionY == r.PositionY + 1 && x.PositionX == r.PositionX))
                        {
                            
                            //selecteer de boven buur
                            var linq = (from a in Roomlist
                                        where (r.PositionY + 1) == a.PositionY && r.PositionX == a.PositionX
                                        select a).ToList();

                            //maak er een Abstractroom van en voeg hem toe aan buren
                            AbstractRoom result = linq.First();
                            r.AddNeighbour(ref result, 1);

                        }

                        //kijk of er onder buren zijn
                        if (Roomlist.Any(x => x.PositionY == r.PositionY - 1 && x.PositionX == r.PositionX))
                        {
                                                    
                            //selecteer de onder buur
                            var linq = (from a in Roomlist
                                        where (r.PositionY - 1) == a.PositionY && r.PositionX == a.PositionX
                                        select a).ToList();

                            //maak er een Abstractroom van en voeg hem toe aan buren
                            AbstractRoom result = linq.First();
                            r.AddNeighbour(ref result, 1);
                        }
                    }

                    //kijk of er rechter buren zijn
                    if (Roomlist.Any(x => x.PositionY == r.PositionY && x.PositionX == r.PositionX + r.DimensionX))
                    {

                        //selecteer rechter buur zolang het geen lift is
                        var linq = (from a in Roomlist
                                    where r.PositionY == a.PositionY && (r.PositionX + r.DimensionX) == a.PositionX
                                     && a.AreaType != "Elevator" select a).ToList();

                        //maak er een Abstractroom van en voeg hem toe aan buren
                        AbstractRoom result = linq.First();
                        r.AddNeighbour(ref result, r.DimensionX);

                    }
                    else //als er geen rechter buur is
                    {
                        //kijk naar rechts tot je iets vindt of de maxX is bereikt
                        for (int i = r.PositionX + 1; i <= maxX + 1; i++)
                        {
                            //als er een kamer is gevonden die deze positie heeft
                            if (Roomlist.Any(x => x.PositionY == r.PositionY && x.PositionX == i))
                            {
                                //selecteer rechter buur
                                var linq = (from a in Roomlist
                                            where r.PositionY == a.PositionY && i == a.PositionX
                                            select a).ToList();

                                //maak er een Abstractroom van en voeg hem toe aan buren
                                AbstractRoom result = linq.First();
                                r.AddNeighbour(ref result, 1);

                                break;
                            }
                        }
                    }


                    //kijk of er linker buren zijn
                    if (Roomlist.Any(x => x.PositionY == r.PositionY && x.PositionX == r.PositionX - 1))
                    {
                        //selecteer linker buur
                        var linq = (from a in Roomlist
                                    where r.PositionY == a.PositionY && (r.PositionX - 1) == a.PositionX
                                    select a).ToList();

                        //maak er een Abstractroom van en voeg hem toe aan buren
                        AbstractRoom result = linq.First();
                        r.AddNeighbour(ref result, 1);
                    }
                    else //als er geen linker buur is
                    {
                        //kijk naar links tot je iets vindt of het begin is bereikt
                        for (int i = r.PositionX - 1; i >= 0; i--)
                        {
                            //als er een kamer is gevonden die deze positie heeft
                            if (Roomlist.Any(x => x.PositionY == r.PositionY && x.PositionX == i))
                            {
                                //selecteer linker buur
                                var linq = (from a in Roomlist
                                            where r.PositionY == a.PositionY && i == a.PositionX
                                            select a).ToList();

                                //maak er een Abstractroom van en voeg hem toe aan buren
                                AbstractRoom result = linq.First();
                                r.AddNeighbour(ref result, 1);

                                break;
                            }
                        }
                    }
                }             
            
            //voeg lift toe aan roomlist
            Roomlist.Add(elevator);
        }

        /// <summary>
        /// check out listener
        /// </summary>
        /// <param name="data">een Dictionary met een wens en een Id</param>
        public void CheckoutGuest(Dictionary<string, string> data)
        {
            //maak variable
            string value = data.Values.ToList().Last().ToString();
            int guestId = Int32.Parse(value);
            Guest guest = null;

            //voor elke gast in GuestList kijk of de Id gelijk is aan guestId
            foreach(Guest g in GuestList)
            {
                if(g.GuestId == guestId)
                {
                    guest = g;
                    break;
                }
            }

            //zet de kamer van guest in _dirtyroom list en verwijder de gast uit guest list (check out)
            _dirtyroom.Enqueue(guest.MyRoom);
            GuestList.Remove(guest);                     
        }

        /// <summary>
        /// als een gast inchecked bij het hotel
        /// </summary>
        /// <param name="data"> een Dictionary met een wens en een Id</param>
        public void WelcomeGuest(Dictionary<string, string> data)
        {
            //zet de value en key van de gegeven dictonary om naar variable
            string value = data.Values.ToList().Last().ToString();
            string key = data.Keys.ToList().Last().ToString();

            //zet de value string om naar een wens
            int wish = Int32.Parse(Regex.Match(value, @"\d+").Value);

            //geef de gast een kamer
            AbstractRoom GivenRoom = ((Reception)reception).giveRoom(wish);

            //als de kamer niet null is voeg een nieuwe gast toe aan GuestList
            if(GivenRoom != null)
            {
                GuestList.Add((Guest)humanFactory.CreateGuest(reception, value.Split(' ').Last(), GivenRoom, key));
            }
            else //laat een bericht zien dat de kamer niet beschikbaar is
            {
                Debug.WriteLine("No Room Available");
            }
           
        }

        /// <summary>
        /// zet de gegeven voor de timer en start hem
        /// </summary>
        private void SetTimer()
        {
            _hotelTimer = new Timer(1000);
            _hotelTimer.Elapsed += UpdateHotel;
            _hotelTimer.AutoReset = true;
            _hotelTimer.Enabled = true;
        }

        /// <summary>
        /// stop de timer
        /// </summary>
        public void StopTimer()
        {
            _hotelTimer.Dispose();
            _hotelTimer.Stop();
        }
      

        /// <summary>
        /// Deze functie wordt elke seconden aangeroepen om gasten en medewerkes te laten lopen.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void UpdateHotel(Object source, ElapsedEventArgs e)
        {
            //for loop voor het update van de lift
            for(int i = 0; i < Roomlist.Count; i++)
            {
                //als de kamer gelijk is aan Elevator
                if(Roomlist[i].AreaType == "Elevator")
                {
                    //maak een tijdelijke lift update de lift en voeg hem weer toe
                    Elevator tempElevator = (Elevator)Roomlist[i];
                    tempElevator.updateElevator();
                    Roomlist[i] = tempElevator;

                }
            }

            //update de gasten
            foreach(Guest guest in GuestList.ToList())
            {
                guest.Move();
            }

           
            //update de schoonmakers en kijk of er een kamer moet worden schoon gemaakt
            foreach (Maid maid in Employees)
            {
               maid.Move();

               if (!maid.working && _dirtyroom.Count > 0)
               {
                    maid.working = true;
                    maid.CleanRoom(_dirtyroom.Dequeue());
               }
               else if (_dirtyroom.Count == 0 && !maid.working && maid.CurrentPosition.AreaType != "Reception")
                {
                    maid.ReturnToBase();
                }
            }
           
            
        }
    }
}
