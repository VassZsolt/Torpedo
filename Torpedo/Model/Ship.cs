using System.Collections.Generic;

namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a general ship object.
    /// </summary>
    public class Ship
    {
        public int ShipSize { get; set; }
        public Alignment ShipAlignment { get; set; }

        public Coordinate StartPosition { get; set; } = new Coordinate(Column.A, -1);
        public bool Status { get; set; } // false if sunk
        public List<Coordinate> Positions { get; set; } = new List<Coordinate>();

        public Ship() { }
    }
}
