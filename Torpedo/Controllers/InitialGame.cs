using System.Collections.Generic;
using NationalInstruments.Torpedo.Controllers;
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

        public Player Player1 = new Player(-1, -1);
        public Player Player2 = new Player(-1, -1);
        private GameMode _gameMode;
        private ShipPlacementController _shipPlacementController = new ShipPlacementController(_sizeOfBoard);
        public Match Match;

        public InitialGame(GameMode gameMode)
        {
            _gameMode = gameMode;
            CreatePlayers();
            Match = new Match(Player1, Player2);
        }

        private void CreatePlayers()
        {
            if (_gameMode == GameMode.TwoPlayerMode)
            {
                Player1 = new Player(_numberOfShips, _sizeOfBoard);
                // Player1.Name=
                Player2 = new Player(_numberOfShips, _sizeOfBoard);
                // Player2.Name=
            }
            else
            {
                Player1 = new Player(_numberOfShips, _sizeOfBoard);
                // Player1.Name=
                // TODO __Dede Zsolt__
            }
        }

        public void PlaceShip(int shipSize, Alignment align, Coordinate startPosition, Player player)
        {
            _shipPlacementController.SizeOfShip = shipSize;
            _shipPlacementController.SetAlignment(align);
            _shipPlacementController.SetStartPosition(startPosition);

            if (_shipPlacementController.IsPossiblePlacement())
            {
                List<Coordinate> positions = _shipPlacementController.GenerateShipPositions;

                // A hajókat szigorúan növekvő sorrendben helyezzük el!
                player.Ships[shipSize - 1].ShipSize = shipSize;
                player.Ships[shipSize - 1].ShipAlignment = align;
                player.Ships[shipSize - 1].StartPosition = startPosition;
                player.Ships[shipSize - 1].Positions.AddRange(positions);
                player.Ships[shipSize - 1].Status = true;
            }
        }
    }
}