using System.Collections.Generic;
using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.Controllers
{
    /// <summary>
    /// This class handle the ship placement.
    /// </summary>
    public class ShipPlacementController
    {
        private Coordinate _startPosition = new Coordinate();
        private Alignment _alignment;

        public void SetAlignment(Alignment alignment) => _alignment = alignment;
        public Alignment Alignment => _alignment;

        public void SetStartPosition(Coordinate coordinate)
        {
            _startPosition.SetColumn(coordinate.Column);
            _startPosition.SetRow(coordinate.Row);
        }
        public Coordinate StartPosition => _startPosition;
        public int SizeOfShip { get; set; }
        private int _sizeOfBoard;

        public ShipPlacementController(int sizeOfBoard)
        {
            _sizeOfBoard = sizeOfBoard;
        }

        public bool IsPossiblePlacement()
        {
            return _alignment == Alignment.Horizontal && (int)_startPosition.Column + SizeOfShip - 1 <= _sizeOfBoard ||
                        _alignment == Alignment.Vertical && _startPosition.Row + SizeOfShip - 1 <= _sizeOfBoard;
        }

        public List<Coordinate> GenerateShipPositions
        {
            get
            {
                List<Coordinate> positions = new List<Coordinate>();
                positions.Add(_startPosition);
                if (_alignment == Alignment.Horizontal)
                {
                    for (int i = 0; i < SizeOfShip; i++)
                    {
                        positions.Add(new Coordinate((Column)(int)StartPosition.Column + i, StartPosition.Row));
                    }
                }
                else
                {
                    for (int i = 0; i < SizeOfShip; i++)
                    {
                        positions.Add(new Coordinate(StartPosition.Column, StartPosition.Row + i));
                    }
                }
                return positions;
            }
        }
    }
}