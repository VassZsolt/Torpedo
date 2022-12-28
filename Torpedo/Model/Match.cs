using System.Collections.Generic;

namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// This class collect all of the important informations about a Match.
    /// </summary>
    internal class Match
    {
        private string _playerName1;
        private string _playerName2;
        public string _firstPlayerName = string.Empty;
        public void SetFirstPlayerName(string name) => _firstPlayerName = name;

        public int NumberOfRounds { get; set; }
        public List<Coordinate> Player1Shoots = new List<Coordinate>();
        public List<Coordinate> Player2Shoots = new List<Coordinate>();
        private string _winner = string.Empty;
        public List<Coordinate> __matchHistory = new List<Coordinate>();
        public List<int> WinnerSurvivedShips = new List<int>();

        public Match(Player player1, Player player2)
        {
            _playerName1 = player1.Name;
            _playerName2 = player2.Name;
            NumberOfRounds = 1;
        }

        public Match(string playerName1, string playerName2, string firstPlayerName, int numberOfRounds, string winner, List<int> winnerSurvivedShips)
        {
            _playerName1 = playerName1;
            _playerName2 = playerName2;
            _firstPlayerName = firstPlayerName;
            NumberOfRounds = numberOfRounds;
            _winner = winner;
            WinnerSurvivedShips = winnerSurvivedShips;
        }

        public void AddWinner(string winner) { _winner = winner; }

        public void AddShoot(Player player, Coordinate coordinate)
        {
            if (player.Name == _playerName1)
            {
                Player1Shoots.Add(coordinate);
            }
            else
            {
                Player2Shoots.Add(coordinate);
            }
        }
        public void AddWinnerShip(Player player, int size)
        {
            WinnerSurvivedShips.Add(size);
        }
    }
}
