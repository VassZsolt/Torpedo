﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Torpedo.Model
{
    //Represents a Board for the game.
    internal class Board
    {
        public const int BoardSize = 10;
        public Coordinate[,] GameBoard { get; private set; }

        public Board()
        {
             GameBoard = new Coordinate[BoardSize, BoardSize];
        }
    }
}
