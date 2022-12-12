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
        private int _length;
        private Alignment _alignment;
        private Coordinate _startPosition;

        public bool Status { get; private set; } //false if sunk
        public List<Coordinate> Positions { get; private set; }


        public Ship(int length, Alignment alignment)
        {
            _length= length;
            _alignment = alignment;
            Status = true;
        }
    }
}
