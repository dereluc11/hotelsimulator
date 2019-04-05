using System;
using System.Linq;
using HotelSimulator;
using HotelSimulator.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

 
namespace HotelTests
{
    [TestClass]
    public class UnitTest1
    {
        //testen of een gast een path krijgt nadat hij is aangemaakt
        [TestMethod]
        public void GetPath()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest = new Guest(reception, "5", reception.giveRoom(5), "1");
            //act
            guest.SetPath();
            //assert
            Assert.IsNotNull(guest.Path);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void doesTheGuestGetTheRightRoom5Stars()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest;
            //act
            guest = new Guest(reception, "5", reception.giveRoom(5), "1");
            //assert
            Assert.AreEqual("5 stars", guest.MyRoom.Classification);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void doesTheGuestGetTheRightRoom4Stars()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest;
            //act
            guest = new Guest(reception, "4", reception.giveRoom(4), "1");
            //assert
            Assert.AreEqual("4 stars", guest.MyRoom.Classification);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void doesTheGuestGetTheRightRoom3Stars()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest;
            //act
            guest = new Guest(reception, "3", reception.giveRoom(3), "1");
            //assert
            Assert.AreEqual("3 stars", guest.MyRoom.Classification);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void doesTheGuestGetTheRightRoom2Stars()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest;
            //act
            guest = new Guest(reception, "2", reception.giveRoom(2), "1");
            //assert
            Assert.AreEqual("2 stars", guest.MyRoom.Classification);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void doesTheGuestGetTheRightRoom1Star()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest;
            //act
            guest = new Guest(reception, "1", reception.giveRoom(1), "1");
            //assert
            Assert.AreEqual("1 Star", guest.MyRoom.Classification);
        }

        //testen of een gast de juiste kamer krijgt bij een bepaalde wens
        [TestMethod]
        public void ElevatorCallTemplateWithRightINformation()
        {
            //arrange
            Hotel hotel = new Hotel();
            Reception reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            Guest guest = new Guest(reception, "5", reception.giveRoom(5), "1");
            ElevatorShaft shaft1 = (ElevatorShaft)hotel.Roomlist.Find(x => x.AreaType == "Elevatorshaft" && x.PositionY == 0);
            ElevatorShaft shaft2 = (ElevatorShaft)hotel.Roomlist.Find(x => x.AreaType == "Elevatorshaft" && x.PositionY == 4);
            ElevatorCallTemplate call;

            //act
            call = new ElevatorCallTemplate(guest, shaft1, shaft2);

            //assert
            Assert.AreEqual(guest, call.guest);
            Assert.AreEqual(shaft1, call.startFloor);
            Assert.AreEqual(shaft2, call.destinationFloor);


        }

        //testen of de Roomfactory de juiste room terug geeft
        [TestMethod]
        public void GetAbedroom()
        {
            //arrange
            RoomFactory factory;
            Bedroom room;

            //act
            factory = new RoomFactory();
            room = (Bedroom)factory.CreateBedroom("3 stars", "bedroom", "2,5", "1,1", 1);

            //assert
            Assert.AreEqual(1, room.DimensionX);
            Assert.AreEqual(1, room.DimensionY);
            Assert.AreEqual(2, room.PositionX);
            Assert.AreEqual(5, room.PositionY);
        }

        //testen of de HumanFactory de juiste room terug geeft
        [TestMethod]
        public void MakeGuest()
        {
            //arrange
            HumanFactory factory;
            Guest guest;
            Hotel hotel;
            Reception reception;

            //act
            hotel = new Hotel();
            reception = (Reception)(from x in hotel.Roomlist where x.AreaType == "Reception" select x).ToList().Last();
            factory = new HumanFactory();
            guest = (Guest)factory.CreateGuest(reception, "5", reception.giveRoom(5), "1");

            //assert
            Assert.AreEqual(1, guest.GuestId);
            Assert.AreEqual(reception, guest.CurrentPosition);
            Assert.AreEqual("5", guest.Wish);
        }
    }
}
