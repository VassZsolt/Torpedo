using System;
using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.ViewModel
{
    /// <summary>
    /// This class responsible for the game play (next player, shooting, game over)
    /// </summary>
    internal class GameController
    {
        private Player _firstPlayer;
        private Player _secondPlayer;
        private Random _random = new Random();
        private InitialGame _game;

        public GameController(GameMode gameMode)
        {
            _game = new InitialGame(gameMode);
            ChooseFirstPlayer();
            PlayGame();
        }

        private void ChooseFirstPlayer()
        {
            if (_random.Next(2) == 0)
            {
                _firstPlayer = _game.Player1;
                _secondPlayer = _game.Player2;
                _game.Match.SetFirstPlayerName(_game.Player1.Name);
            }
            else
            {
                _firstPlayer = _game.Player2;
                _secondPlayer = _game.Player1;
                _game.Match.SetFirstPlayerName(_game.Player2.Name);
            }
        }

        private void PlayGame()
        {
            Player actualPlayer = _firstPlayer;
            while (!IsGameOver(actualPlayer))
            {
                MakeShoot(actualPlayer);
                actualPlayer = NextPlayer(actualPlayer);
            }
        }

        private Player NextPlayer(Player player)
        {
            if (player == _firstPlayer)
            {
                return _secondPlayer;
            }
            else
            {
                return _firstPlayer;
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
            for (int i = 0; i < enemy.Ships.Length; i++)
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
            for (int i = 0; i < player.Ships.Length; i++)
            {
                if (player.Ships[i].Status == true)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
