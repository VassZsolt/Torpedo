using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Torpedo.Model
{
    //Represents a Coordinate object like A5 or C8.

    internal class Coordinate
    {
        public Column Column { get; private set; } //A-J
        public int Row { get; private set; }


        public Coordinate(Column column, int row)
        {
            Column = column;
            Row = row;
        }

        public override string ToString()
        {
            return $"{Column}{Row}";
        }
    }
}