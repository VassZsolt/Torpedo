using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    internal class Player
    {
        public string Name { get; set; }
        public List<Ship> Ships;
        public Board Board;
        public List<Coordinate> Shoots = new List<Coordinate>();

        private List<char> _bannedChars = new List<char>(new char[] { '!', '?', '_', '-', ':', ';', '#', ' ' });

        public Player(int numberOfShips, int boardSize)
        {
            Name = string.Empty;
            Board = new Board(boardSize);
            Ships = new List<Ship>(5);
        }

        public bool IsCorrectName(string name)
        {
            foreach (char item in _bannedChars)
            {
                if (name.Contains(char.ToString(item)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
