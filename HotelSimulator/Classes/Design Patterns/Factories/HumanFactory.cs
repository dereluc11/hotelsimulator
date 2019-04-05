using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// een factory voor alle objecten die overerven van de AbstractHuman class
    /// </summary>
    public class HumanFactory
    {
        /// <summary>
        /// deze functie geeft een nieuwe gast terug
        /// </summary>
        /// <param name="_current">Geef de room mee waar hij nu is</param>
        /// <param name="_wish">Geef de wish mee die die heeft(dat is nu hoeveel sterren kamer hij wilt die uit de dll komt)</param>
        /// <param name="_checkIn">Geef de room mee die hij krijgt van de receptie</param>
        /// <param name="_id">Geef een id mee(deze wordt gegeven door de dll)</param>
        /// <returns>een new guest object met al de gegeven parameters</returns>
        public AbstractHuman CreateGuest(AbstractRoom _current, string _wish, AbstractRoom _checkIn, string _id)
        {
            //return een guest object
            return new Guest(_current, _wish, _checkIn, _id);
        }
        
        /// <summary>
        /// deze functie geeft een nieuwe maid terug
        /// </summary>
        /// <param name="_room">Geef de room mee waar de maid gemaakt moet worden</param>
        /// <returns>een new Maid object met de gegeven parameter</returns>
        public AbstractHuman CreateMaid(AbstractRoom _room)
        {
            //return een maid object
            return new Maid(_room);
        }
    }
}
