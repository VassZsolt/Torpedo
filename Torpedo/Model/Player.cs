using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo.Model
{
    //Represents a player.
    internal class Player
    {
        private string _name;
        public const int NumberOfShips = 5;
        public Ship[] Ships;        
        public Board Board;

        public Player(string name) {
            _name = name;
            Board = new Board();
            Ships= new Ship[NumberOfShips];
        }
    }
}
