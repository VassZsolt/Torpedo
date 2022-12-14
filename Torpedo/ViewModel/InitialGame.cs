using System;
using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.ViewModel
{
    /// <summary>
    /// This class responsible for creating players and their ships.
    /// </summary>
    internal class InitialGame
    {
        private const int _numberOfShips = 5;
        private const int _sizeOfBoard = 10;
        private Random _random = new Random();

        public Player Player1 = new Player(string.Empty, -1, -1);
        public Player Player2 = new Player(string.Empty, -1, -1);
        public GameMode GameMode;

        public InitialGame(GameMode gameMode)
        {
            GameMode = gameMode;
            CreatePlayers();
            PlaceShips();
        }

        private void CreatePlayers()
        {
            if (GameMode == GameMode.TwoPlayerMode)
            {
                Player1 = new Player(SetPlayerName(), _numberOfShips, _sizeOfBoard);
                Player2 = new Player(SetPlayerName(), _numberOfShips, _sizeOfBoard);
            }
            else
            {
                _random.Next(2); // véletlenszerűen kiválasztjuk ki kezdjen..
                Player1 = new Player(SetPlayerName(), _numberOfShips, _sizeOfBoard);
                // TODO __Dede Zsolt__
            }
        }

        private string SetPlayerName()
        {
            // TODO __Molnár Nikolett__
            return "Játékos1";
        }

        private void PlaceShips()
        {
            if (GameMode == GameMode.TwoPlayerMode)
            {
                ShipPlacement(Player1);
                ShipPlacement(Player2);
            }
            else
            {
                // TODO __Dede Zsolt__
            }
        }

        private void ShipPlacement(Player player)
        {
            for (int i = 0; i < _numberOfShips; i++)
            {
                player.Ships[i].ShipSize = i + 1;
                do
                {
                    player.Ships[i].ShipAlignment = SetShipAlignment();
                    player.Ships[i].StartPosition = SetShipPosition();
                }
                while (!IsPossiblePlacement(player.Ships[i].ShipAlignment, player.Ships[i].StartPosition, player.Ships[i].ShipSize));
                GenerateShipPositions(player.Ships[i]);
            }
        }

        private Coordinate SetShipPosition()
        {
            // TODO __Molnár Nikolett__
            return new Coordinate(Column.A, 1);
        }

        private Alignment SetShipAlignment()
        {
            // TODO __Molnár Nikolett__
            return Alignment.Horizontal;
        }

        private bool IsPossiblePlacement(Alignment shipAlignment, Coordinate startPosition, int shipSize)
        {
            return shipAlignment == Alignment.Horizontal && (int)startPosition.Column + shipSize - 1 <= _sizeOfBoard ||
                        shipAlignment == Alignment.Vertical && startPosition.Row + shipSize - 1 <= _sizeOfBoard;
        }

        private void GenerateShipPositions(Ship ship)
        {
            if (ship.ShipAlignment == Alignment.Horizontal)
            {
                for (int i = 0; i < ship.ShipSize; i++)
                {
                    ship.Positions.Add(new Coordinate(
                        (Column)(int)ship.StartPosition.Column + i,
                        ship.StartPosition.Row));
                }
            }
            else
            {
                for (int i = 0; i < ship.ShipSize; i++)
                {
                    ship.Positions.Add(new Coordinate(
                        ship.StartPosition.Column,
                        ship.StartPosition.Row + i));
                }
            }
        }
    }
}
