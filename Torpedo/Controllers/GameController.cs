using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NationalInstruments.Torpedo.Controllers;
using NationalInstruments.Torpedo.Model;
using NationalInstruments.Torpedo.View;

namespace NationalInstruments.Torpedo.ViewModel
{
    /// <summary>
    /// This class responsible for the game play (next player, shooting, game over)
    /// </summary>
    internal class GameController
    {
        public Player FirstPlayer;
        public Player SecondPlayer;
        private Random _random = new Random();

        private const int _numberOfShips = 5;
        private const int _sizeOfBoard = 10;

        private Player _player1 = new Player(0, 0);
        private Player _player2 = new Player(0, 0);
        private GameMode _gameMode;
        private ShipPlacementController _shipPlacementController = new ShipPlacementController(_sizeOfBoard);
        public Match Match;

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GameController(GameMode gameMode, string playerOneName, string? playerTwoName)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _gameMode = gameMode;
            FirstPlayer = _player1;
            SecondPlayer = _player2;
            CreatePlayers(playerOneName, playerTwoName);
        }
        private void CreatePlayers(string playerOneName, string? playerTwoName)
        {
            _player1 = new Player(_numberOfShips, _sizeOfBoard);
            _player1.Name = playerOneName;

            if (_gameMode == GameMode.TwoPlayerMode)
            {
                _player2 = new Player(_numberOfShips, _sizeOfBoard);
                #pragma warning disable CS8601 // Possible null reference assignment.
                _player2.Name = playerTwoName;
                #pragma warning restore CS8601 // Possible null reference assignment.
            }
            else
            {
                // TODO __Dede Zsolt__ Create an AI
            }
            Match = new Match(_player1, _player2);
            ChooseFirstPlayer();
        }
        public void ChooseFirstPlayer()
        {
            if (_random.Next(2) == 0)
            {
                FirstPlayer = _player1;
                SecondPlayer = _player2;
                Match.SetFirstPlayerName(_player1.Name);
            }
            else
            {
                FirstPlayer = _player2;
                SecondPlayer = _player1;
                Match.SetFirstPlayerName(_player2.Name);
            }
        }
        public Player NextPlayer(Player player)
        {
            if (player == FirstPlayer)
            {
                return SecondPlayer;
            }
            else
            {
                return FirstPlayer;
            }
        }
        public bool IsHit(Player player, Coordinate target)
        {
            Player enemy = NextPlayer(player);
            for (int i = 0; i < enemy.Ships.Count; i++)
            {
                Ship ship = enemy.Ships[i];
                if (ship.Status == false)
                {
                    continue;
                }
                foreach (Coordinate coordinate in ship.Positions)
                {
                    if (coordinate.Column == target.Column && coordinate.Row == target.Row)
                    {
                        ship.Positions.Remove(coordinate);
                        if (ship.Positions.Count == 0)
                        {
                            ship.Status = false;
                            enemy.NumberOfDeadShips++;
                            enemy.LivingShips.Remove(ship.ShipSize);
                            enemy.DeadShips.Add(ship.ShipSize);
                        }
                        return true;
                    }
                }

                /*
                if (ship.Positions.Contains(target))
                {
                    ship.Positions.Remove(target);
                    return true;
                }*/
            }
            return false;
        }

        public bool IsGameOver(Player player)
        {
            for (int i = 0; i < player.Ships.Count; i++)
            {
                if (player.Ships[i].Status == true)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsAlreadyUsed(Player player, List<Coordinate> coordinates)
        {
            foreach (Ship ship in player.Ships)
            {
                foreach (Coordinate shipCoordinate in ship.Positions)
                {
                    foreach (Coordinate coordinate in coordinates)
                    {
                        if (shipCoordinate.Column == coordinate.Column && shipCoordinate.Row == coordinate.Row)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsAnUniqueSize(int shipSize, Player player)
        {
            foreach (Ship ship in player.Ships)
            {
                if (ship.ShipSize == shipSize)
                {
                    return false;
                }
            }
            return true;
        }

        public void PlaceShip(int shipSize, Alignment align, Coordinate startPosition, Player player)
        {
            if (shipSize == 0)
            {
                return;
            }
            _shipPlacementController.SizeOfShip = shipSize;
            _shipPlacementController.SetAlignment(align);
            _shipPlacementController.SetStartPosition(startPosition);

            if (_shipPlacementController.IsPossiblePlacement())
            {
                List<Coordinate> positions = _shipPlacementController.GenerateShipPositions;
                if (!IsAlreadyUsed(player, positions) && IsAnUniqueSize(shipSize, player))
                {
                    Ship ship = new Ship();
                    ship.ShipSize = shipSize;
                    ship.ShipAlignment = align;
                    ship.StartPosition = startPosition;
                    foreach (Coordinate coordinate in positions)
                    {
                        ship.Positions.Add(coordinate);
                    }
                    ship.Status = true;

                    player.Ships.Add(ship);
                }
            }
        }

        public Coordinate GetCoordinate(string buttonName)
        {
            Column column;
            if (buttonName.Substring(1, 1) == "A")
            {
                column = Column.A;
            }
            else if (buttonName.Substring(1, 1) == "B")
            {
                column = Column.B;
            }
            else if (buttonName.Substring(1, 1) == "C")
            {
                column = Column.C;
            }
            else if (buttonName.Substring(1, 1) == "D")
            {
                column = Column.D;
            }
            else if (buttonName.Substring(1, 1) == "E")
            {
                column = Column.E;
            }
            else if (buttonName.Substring(1, 1) == "F")
            {
                column = Column.F;
            }
            else if (buttonName.Substring(1, 1) == "G")
            {
                column = Column.G;
            }
            else if (buttonName.Substring(1, 1) == "H")
            {
                column = Column.H;
            }
            else if (buttonName.Substring(1, 1) == "I")
            {
                column = Column.I;
            }
            else
            {
                column = Column.J;
            }
            string row = buttonName.Substring(2);
            #pragma warning disable CA1305 // Specify IFormatProvider
            int x = int.Parse(row);
            #pragma warning restore CA1305 // Specify IFormatProvider
            return new Coordinate(column, x);
        }
    }
}
