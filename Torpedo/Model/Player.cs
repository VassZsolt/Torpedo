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
        private string _name;
        public Ship[] Ships;
        public Board Board;
        public List<Coordinate> Shoots = new List<Coordinate>();

        private List<char> _bannedChars = new List<char>(new char[] { '!', '?', '_', '-', ':', ';', '#', ' ' });

        public Player(int numberOfShips, int boardSize)
        {
            _name = string.Empty;
            Board = new Board(boardSize);
            Ships = new Ship[numberOfShips];
        }

        public void SetName(string name)
        {
            if (IsCorrectName(name))
            {
                _name = name;
            }
        }

        public string GetName()
        {
            return _name;
        }
        private bool IsCorrectName([NotNull] string name)
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
