using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Model;

namespace Torpedo.ViewModel
{
    //This class responsible for the game play (next player, shooting, game over)
    internal class GameController
    {
        private Player _player1;
        private Player _player2;

        public GameController(GameMode gameMode) {
            InitGame game = new InitGame(gameMode);
            _player1 = game.Player1;
            _player2 = game.Player2;
            playGame();
        }
        
        private void playGame() {
            Player actualPlayer=_player2;
            while(!isGameOver(actualPlayer)){
                actualPlayer=nextPlayer(actualPlayer);
                makeShoot(actualPlayer);
            };
        }

        private Player nextPlayer(Player player) {
            if (player == _player1) { 
                return _player2;
            } 
            else { 
                return _player1; 
            }
        }
        private void makeShoot(Player actualPlayer)
        {
            Coordinate target = setTargetCoordinate();
            shoot(actualPlayer, target);
        }

        private Coordinate setTargetCoordinate() {
            //TODO __Molnár Niki__
            return new Coordinate(Column.A,1);
        }

        private void shoot(Player player, Coordinate target) {
            player.Shoots.Add(target);
            while (isHit(player, target)) {
                makeShoot(player);
            }
        }

        private bool isHit(Player player, Coordinate target){
            Player enemy = nextPlayer(player);
            for (int i = 0; i < enemy.Ships.Length; i++) {
                Ship ship = enemy.Ships[i];
                if (ship.Status == false) {
                    continue;
                }
                if (ship.Positions.Count == 0){
                    ship.Status = false;
                    continue;
                }
                if (ship.Positions.Contains(target)) {
                    ship.Positions.Remove(target);
                    return true;
                }
            }
            return false;
        }

        private bool isGameOver(Player player){
            for (int i = 0; i < player.Ships.Length; i++) {
                if (player.Ships[i].Status == true){
                    return false;
                }
            }
            return true;
        }
    }
}
