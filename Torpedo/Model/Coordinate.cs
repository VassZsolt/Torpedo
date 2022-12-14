namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a Coordinate object like A5 or C8.
    /// </summary>
    internal class Coordinate
    {
        public Column Column { get; private set; } // A-J
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