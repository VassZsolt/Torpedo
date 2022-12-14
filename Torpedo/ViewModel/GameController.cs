using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.ViewModel
{
    /// <summary>
    /// This class responsible for the game play (next player, shooting, game over)
    /// </summary>
    internal class GameController
    {
        private Player _player1;
        private Player _player2;

        public GameController(GameMode gameMode)
        {
            InitialGame game = new InitialGame(gameMode);
            _player1 = game.Player1;
            _player2 = game.Player2;
            PlayGame();
        }

        private void PlayGame()
        {
            Player actualPlayer = _player2;
            while (!IsGameOver(actualPlayer))
            {
                actualPlayer = NextPlayer(actualPlayer);
                MakeShoot(actualPlayer);
            }
        }

        private Player NextPlayer(Player player)
        {
            if (player == _player1)
            {
                return _player2;
            }
            else
            {
                return _player1;
            }
        }
        private void MakeShoot(Player actualPlayer)
        {
            Coordinate target = SetTargetCoordinate();
            Shoot(actualPlayer, target);
        }

        private Coordinate SetTargetCoordinate()
        {
            // TODO __Molnár Niki__
            return new Coordinate(Column.A, 1);
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
