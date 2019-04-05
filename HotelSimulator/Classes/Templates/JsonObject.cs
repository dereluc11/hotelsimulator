using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Classes
{
    /// <summary>
    /// deze klasse is nodig om de layout file uit te lezen
    /// </summary>
    class JsonObject
    {
        //alle nodige properties
        public string AreaType { get; set; }
        public string Position { get; set; }
        public string Dimension { get; set; }
        public int Capacity { get; set; }
        public string Classification { get; set; }
        public int Id { get; set; }
    }
}
