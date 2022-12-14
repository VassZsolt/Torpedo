using System.Collections.Generic;

namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    internal class Player
    {
        private string _name;
        public Ship[] Ships;
        public Board Board;
        public List<Coordinate> Shoots;

        public Player(string name, int numberOfShips, int boardSize)
        {
            _name = name;
            Board = new Board(boardSize);
            Ships = new Ship[numberOfShips];
            Shoots = new List<Coordinate>();
        }
    }
}
