﻿namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a Coordinate object like A5 or C8.
    /// </summary>
    internal class Coordinate
    {
        private Column _column; // A-J
        private int _row;

        public void SetColumn(Column column) => _column = column;
        public Column Column => _column;
        public void SetRow(Column column) => _column = column;
        public int Row => _row;

        public Coordinate(Column column, int row)
        {
            _column = column;
            _row = row;
        }

        public override string ToString()
        {
            return $"{Column}{_row}";
        }
    }
}