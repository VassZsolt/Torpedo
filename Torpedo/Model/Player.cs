using System.Collections.Generic;

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
        public int HitCount = 0;
        public int NumberOfDeadShips = 0;
        private List<char> _bannedChars = new List<char>(new char[] { '!', '?', '_', '-', ':', ';', '#', ' ' });
        public List<int> LivingShips = new List<int> { 1, 2, 3, 4, 5 };
        public List<int> DeadShips = new List<int>();

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
