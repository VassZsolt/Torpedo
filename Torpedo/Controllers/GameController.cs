﻿using System;
using NationalInstruments.Torpedo.Controllers;
using System.Collections.Generic;
using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.ViewModel
{
    /// <summary>
    /// This class responsible for the game play (next player, shooting, game over)
    /// </summary>
    internal class GameController
    {
        public Player Firstplayer;
        public Player SecondPlayer;
        private Random _random = new Random();

        private const int _numberOfShips = 5;
        private const int _sizeOfBoard = 10;

        private Player _player1 = new Player(0, 0);
        private Player _player2 = new Player(0, 0);
        private GameMode _gameMode;
        private ShipPlacementController _shipPlacementController = new ShipPlacementController(_sizeOfBoard);
        public Match Match;

        public GameController(GameMode gameMode, string playerOneName, string? playerTwoName)
        {
            _gameMode = gameMode;
            CreatePlayers(playerOneName, playerTwoName);
        }
        private void CreatePlayers(string playerOneName, string? playerTwoName)
        {
            _player1 = new Player(_numberOfShips, _sizeOfBoard);
            _player1.Name = playerOneName;

            if (_gameMode == GameMode.TwoPlayerMode)
            {
                _player2 = new Player(_numberOfShips, _sizeOfBoard);
                _player2.Name = playerTwoName;
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
                Firstplayer = _player1;
                SecondPlayer = _player2;
                Match.SetFirstPlayerName(_player1.Name);
            }
            else
            {
                Firstplayer = _player2;
                SecondPlayer = _player1;
                Match.SetFirstPlayerName(_player2.Name);
            }
        }

        private void PlayGame()
        {
            Player actualPlayer = Firstplayer;
            while (!IsGameOver(actualPlayer))
            {
                MakeShoot(actualPlayer);
                actualPlayer = NextPlayer(actualPlayer);
            }
        }

        private Player NextPlayer(Player player)
        {
            if (player == Firstplayer)
            {
                return SecondPlayer;
            }
            else
            {
                return Firstplayer;
            }
        }
        private void MakeShoot(Player actualPlayer)
        {
            /*
            do
            {
                Coordinate target = SetTarget;
                Shoot(actualPlayer, target);
            }
            while (!actualPlayer.Shoots.Contains(target));
            */
        }


        private void Shoot(Player player, Coordinate target)
        {
            player.Shoots.Add(target);
            while (IsHit(player, target))
            {
                MakeShoot(player);
            }
        }

        private bool IsHit(Player player, Coordinate target)
        {
            Player enemy = NextPlayer(player);
            for (int i = 0; i < enemy.Ships.Count; i++)
            {
                Ship ship = enemy.Ships[i];
                if (ship.Status == false)
                {
                    continue;
                }
                if (ship.Positions.Count == 0)
                {
                    ship.Status = false;
                    continue;
                }
                if (ship.Positions.Contains(target))
                {
                    ship.Positions.Remove(target);
                    return true;
                }
            }
            return false;
        }

        private bool IsGameOver(Player player)
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
        public void PlaceShip(int shipSize, Alignment align, Coordinate startPosition, Player player)
        {
            _shipPlacementController.SizeOfShip = shipSize;
            _shipPlacementController.SetAlignment(align);
            _shipPlacementController.SetStartPosition(startPosition);

            if (_shipPlacementController.IsPossiblePlacement())
            {
                List<Coordinate> positions = _shipPlacementController.GenerateShipPositions;
                Ship ship = new Ship();
                ship.ShipSize = shipSize;
                ship.ShipAlignment = align;
                ship.StartPosition = startPosition;
                foreach(Coordinate coordinate in positions)
                {
                    ship.Positions.Add(coordinate);
                }
                ship.Status = true;

                player.Ships.Add(ship);
            }
        }

        public Coordinate GetCoordinate(string buttonName)
        {
            Column column;
            if (buttonName.Substring(0, 1) == "A")
            {
                column = Column.A;
            }
            else if (buttonName.Substring(0, 1) == "B")
            {
                column = Column.B;
            }
            else if (buttonName.Substring(0, 1) == "C")
            {
                column = Column.C;
            }
            else if (buttonName.Substring(0, 1) == "D")
            {
                column = Column.D;
            }
            else if (buttonName.Substring(0, 1) == "E")
            {
                column = Column.E;
            }
            else if (buttonName.Substring(0, 1) == "F")
            {
                column = Column.F;
            }
            else if (buttonName.Substring(0, 1) == "G")
            {
                column = Column.G;
            }
            else if (buttonName.Substring(0, 1) == "H")
            {
                column = Column.H;
            }
            else if (buttonName.Substring(0, 1) == "I")
            {
                column = Column.I;
            }
            else
            {
                column = Column.J;
            }
            string row = buttonName.Substring(1);
            return new Coordinate(column, row: Convert.ToInt32(row));
        }
    }
}
