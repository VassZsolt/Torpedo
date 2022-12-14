using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Torpedo.Model;

namespace Torpedo.ViewModel
{
    //This class responsible for creating players and their ships.
    internal class InitGame
    {
        private const int _numberOfShips = 5;
        private const int _sizeOfBoard = 10;
        private Random _random= new Random();
        
        public Player Player1;
        public Player Player2;
        public GameMode GameMode;

        public InitGame (GameMode gameMode) {
            GameMode= gameMode;
            createPlayers();
            placeShips();
        }
        
        private void createPlayers() {
            if (GameMode==GameMode.TwoPlayerMode) {
                Player1 = new Player(setPlayerName(),_numberOfShips, _sizeOfBoard);
                Player2 = new Player(setPlayerName(), _numberOfShips, _sizeOfBoard);
            }
            else {
                _random.Next(2); //véletlenszerűen kiválasztjuk ki kezdjen..
                //TODO __Dede Zsolt__
            }
        }

        private string setPlayerName() {
            //TODO __Molnár Nikolett__
            return "Játékos1"; 
        }

        private void placeShips() {
            if (GameMode == GameMode.TwoPlayerMode) {
                shipPlacement(Player1);
                shipPlacement(Player2);
            }
            else {

                //TODO __Dede Zsolt__
            }
        }

        private void shipPlacement(Player player) {
            for (int i=0; i<_numberOfShips; i++) {
                player.Ships[i].ShipSize = i + 1;
                do {
                    player.Ships[i].ShipAlignment = setShipAlignment();
                    player.Ships[i].StartPosition = setShipPosition();
                }
                while (!isPossiblePlacement(player.Ships[i].ShipAlignment, player.Ships[i].StartPosition, player.Ships[i].ShipSize));
                generateShipPositions(player.Ships[i]);
            }
        }

        private Coordinate setShipPosition() {
            //TODO __Molnár Nikolett__
            return new Coordinate(Column.A, 1); 
        }

        private Alignment setShipAlignment()
        {
            //TODO __Molnár Nikolett__
            return Alignment.Horizontal;
        }

        private bool isPossiblePlacement(Alignment shipAlignment, Coordinate startPosition, int shipSize) {
            return shipAlignment== Alignment.Horizontal && (int) startPosition.Column + shipSize-1<=_sizeOfBoard ||
                        shipAlignment == Alignment.Vertical && startPosition.Row + shipSize - 1 <= _sizeOfBoard;
        }

        private void generateShipPositions(Ship ship){
            if (ship.ShipAlignment == Alignment.Horizontal) {
                for (int i=0; i<ship.ShipSize; i++) {
                    ship.Positions.Add(new Coordinate(
                        (Column) (int)ship.StartPosition.Column + i, 
                        ship.StartPosition.Row));
                }
            }
            else {
                for (int i = 0; i < ship.ShipSize; i++)
                {
                    ship.Positions.Add(new Coordinate(
                        ship.StartPosition.Column,
                        ship.StartPosition.Row+i));
                }
            }
        }
    }
}
