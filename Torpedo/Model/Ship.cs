using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo.Model
{
    //Represents a general ship object.

    internal class Ship
    {
        public int ShipSize { get; set; }
        public Alignment ShipAlignment { get; set; }
        public Coordinate StartPosition { get; set; }
        public bool Status { get;  set; } //false if sunk
        public List<Coordinate> Positions { get; private set; }


        public Ship(int length, Alignment alignment)
        {
            ShipSize= length;
            ShipAlignment = alignment;
            Status = true;
        }
    }
}
