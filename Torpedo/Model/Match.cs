using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// This class collect all of the important informations about a Match.
    /// </summary>
    internal class Match
    {
        private string _playerName1;
        private string _playerName2;
        private string _firstPlayerName;
        public void SetFirstPlayerName(string name) => _firstPlayerName = name;

        public int NumberOfRounds { get; set; }
        private List<Coordinate> _player1Shoots = new List<Coordinate>();
        private List<Coordinate> _player2Shoots = new List<Coordinate>();
        private string _winner = string.Empty;
        public List<Coordinate> __matchHistory = new List<Coordinate>();

        public Match(Player player1, Player player2)
        {
            _playerName1 = player1.Name;
            _playerName2 = player2.Name;
            NumberOfRounds = 0;
        }

        public void AddWinner(string winner) { _winner = winner; }

        public void AddShoot(Player player, Coordinate coordinate)
        {
            if (player.Name == _playerName1)
            {
                _player1Shoots.Add(coordinate);
            }
            else
            {
                _player2Shoots.Add(coordinate);
            }
        }
    }
}
