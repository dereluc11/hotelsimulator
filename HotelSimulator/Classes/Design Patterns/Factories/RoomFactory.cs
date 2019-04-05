using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// een factory voor alle objecten die overerven van de AbstractRoom class
    /// </summary>
    public class RoomFactory
    {
        /// <summary>
        /// deze functie returned een nieuwe bedroom object(alle gegevens worden geleverd vanuit een layout file)
        /// </summary>
        /// <param name="_classification">Geef de classification mee van hoeveel sterren de kamer is</param>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_pos">Geef de positie mee</param>
        /// <param name="_dim">Geef de dimensies mee</param>
        /// <param name="_id">geef de id mee</param>
        /// <returns>een nieuwe bedroom object met alle gegeven parameters</returns>
        public AbstractRoom CreateBedroom(string _classification, string _areatype, string _pos, string _dim, int _id)
        {
            //return een bedroom object
            return new Bedroom(_classification, _areatype, _pos, _dim, _id);
        }

        /// <summary>
        /// deze functie returned een nieuwe restaurant object(alle gegevens worden geleverd vanuit een layout file)
        /// </summary>
        /// <param name="_capacity">Geef de hoeveelheid aantal plekken mee die beschikbaar zijn in het restaurant</param>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_pos">Geef de positie mee</param>
        /// <param name="_dim">Geef de dimensies mee</param>
        /// <param name="_id">geef de id mee</param>
        /// <returns>een nieuwe restaurant object met alle gegeven parameters</returns>
        public AbstractRoom CreateRestaurant(int _capacity, string _areatype, string _pos, string _dim, int _id)
        {
            //return een restaurant object
            return new Restaurant(_capacity, _areatype, _pos, _dim, _id);
        }

        /// <summary>
        /// Deze functie returned een nieuwe cinema object(alle gegevens worden geleverd vanuit een layout file)
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_pos">Geef de positie mee</param>
        /// <param name="_dim">Geef de dimensies mee</param>
        /// <param name="_id">geef de id mee</param>
        /// <returns>een nieuwe cinema object met alle gegeven parameters</returns>
        public AbstractRoom CreateCinema(string _areatype, string _dim, string _pos, int _id)
        {
            //return een cinema object
            return new Cinema(_areatype, _dim, _pos, _id);
        }

        /// <summary>
        /// Deze functie returned een nieuwe gym object(alle gegevens worden geleverd vanuit een layout file)
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_pos">Geef de positie mee</param>
        /// <param name="_dim">Geef de dimensies mee</param>
        /// <param name="_id">geef de id mee</param>
        /// <returns>een nieuwe gym object met alle gegeven parameters</returns>
        public AbstractRoom CreateGym(string _areatype, string _dim, string _pos, int _id)
        {
            //return een gym object
            return new Gym(_areatype, _dim, _pos, _id);
        }

        /// <summary>
        /// Deze functie returned een nieuwe reception object
        /// </summary>
        /// <param name="_roomlist">geef de hele roomlist mee nadat die helemaal gemaakt is</param>
        /// <returns>een nieuwe reception object met de gegeven parameter</returns>
        public AbstractRoom CreateReception(List<AbstractRoom> _roomlist)
        {
            //return een reception object
            return new Reception(_roomlist);
        }

        /// <summary>
        /// Deze functie returned een nieuwe staiwell object
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_dimX">Geef een X dimensie mee</param>
        /// <param name="_dimY">Geef een Y dimensie mee</param>
        /// <param name="_posX">Geef een X positie mee</param>
        /// <param name="_posY">Geef een Y positie mee</param>
        /// <returns>een nieuwe staiwell object met alle gegeven parameters</returns>
        public AbstractRoom CreateStairwell(string _areatype, int _dimX, int _dimY, int _posX, int _posY)
        {
            //return een stairwell object
            return new Stairwell(_areatype, _dimX, _dimY, _posX, _posY);
        }

        /// <summary>
        /// Deze functie returned een nieuwe ElevatorShaft object
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_dimX">Geef een X dimensie mee</param>
        /// <param name="_dimY">Geef een Y dimensie mee</param>
        /// <param name="_posX">Geef een X positie mee</param>
        /// <param name="_posY">Geef een Y positie mee</param>
        /// <returns>een nieuwe ElevatorShaft object met alle gegeven parameters</returns>
        public AbstractRoom CreateElevatorshaft(string _areatype, int _dimX, int _dimY, int _posX, int _posY)
        {
            //return een elevatorshaft object
            return new ElevatorShaft(_areatype, _dimX, _dimY, _posX, _posY);
        }

        /// <summary>
        /// Deze functie returned een nieuwe Hallway object
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_dimX">Geef een X dimensie mee</param>
        /// <param name="_dimY">Geef een Y dimensie mee</param>
        /// <param name="_posX">Geef een X positie mee</param>
        /// <param name="_posY">Geef een Y positie mee</param>
        /// <returns>een nieuwe Hallway object met alle gegeven
        public AbstractRoom CreateHallway(string _areatype, int _dimX, int _dimY, int _posX, int _posY)
        {
            //return een hallway object
            return new Hallway(_areatype, _dimX, _dimY, _posX, _posY);
        }

        /// <summary>
        /// Deze functie returned een nieuwe Elevator object
        /// </summary>
        /// <param name="_areatype">Geef mee wat voor areatype het is</param>
        /// <param name="_dimX">Geef een X dimensie mee</param>
        /// <param name="_dimY">Geef een Y dimensie mee</param>
        /// <param name="_posX">Geef een X positie mee</param>
        /// <param name="_posY">Geef een Y positie mee</param>
        /// <returns>een nieuwe Elevator object met alle gegeven
        public AbstractRoom CreateElevator(string _areatype, int _dimX, int _dimY, int _posX, int _posY)
        {
            //return een elevator object
            return new Elevator(_areatype, _dimX, _dimY, _posX, _posY);
        }
    }
}
