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
        public Ship[] Ships;        
        public Board Board;

        public Player(string name, int numberOfShips, int boardSize) {
            _name = name;
            Board = new Board(boardSize);
            Ships= new Ship[numberOfShips];
        }
    }
}
