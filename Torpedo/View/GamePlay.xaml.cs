using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NationalInstruments.Torpedo.Model;
using NationalInstruments.Torpedo.ViewModel;

namespace NationalInstruments.Torpedo.View
{
    /// <summary>
    /// Interaction logic for GamePlay.xaml
    /// </summary>
    public partial class GamePlay : Window
    {
        private Player _actualPlayer;
        private GameController _controller;
        private GameMode _gameMode;
        private string _clickedButtonName = string.Empty;
        private Ship _ship = new Ship();
        private Coordinate _coordinate;
        private bool _isFirstPlayerShipsPlanted = false;
        private bool _isSecondPlayerShipsPlanted = false;
        private bool _isGameScreenChanged = false;
        private string _firstPlayerLivingShips;
        private string _secondPlayerLivingShips;
        private string _firstPlayerDeadShips;
        private string _secondPlayerDeadShips;

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GamePlay(GameMode gameMode, string playerOneName, string? playerTwoName)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();
            _controller = new GameController(gameMode, playerOneName, playerTwoName);
            _actualPlayer = _controller.FirstPlayer;
            _gameMode = gameMode;
            if (!_isFirstPlayerShipsPlanted)
            {
                ShipPlacement(_controller.FirstPlayer);
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _clickedButtonName = button.Name;
            _coordinate = _controller.GetCoordinate(_clickedButtonName);
            if (!_isFirstPlayerShipsPlanted)
            {
                if (_controller.FirstPlayer.Ships.Count < 5)
                {
                    _controller.PlaceShip(_ship.ShipSize, _ship.ShipAlignment, _coordinate, _controller.FirstPlayer);
                    DrawShips(_controller.FirstPlayer);
                }
                if (_controller.FirstPlayer.Ships.Count == 5)
                {
                    _isFirstPlayerShipsPlanted = true;
                    ShipPlacement(_controller.SecondPlayer);
                    if (_gameMode == GameMode.SinglePlayerMode)
                    {
                        gameStartButton.Visibility = Visibility.Visible;
                        oneLong.Visibility = Visibility.Hidden;
                        twoLong.Visibility = Visibility.Hidden;
                        threeLong.Visibility = Visibility.Hidden;
                        fourLong.Visibility = Visibility.Hidden;
                        fiveLong.Visibility = Visibility.Hidden;
                        alignmentButton.Visibility = Visibility.Hidden;
                        DisableBoard(PlayerOneBoard);
                        title.Content = "Induljon a játék!";
                    }
                    return;
                }
            }
            else if (!_isSecondPlayerShipsPlanted && _isFirstPlayerShipsPlanted)
            {
                if (_gameMode == GameMode.TwoPlayerMode)
                {
                    if (_controller.SecondPlayer.Ships.Count < 5)
                    {
                        _controller.PlaceShip(_ship.ShipSize, _ship.ShipAlignment, _coordinate, _controller.SecondPlayer);
                        DrawShips(_controller.SecondPlayer);
                    }
                    if (_controller.SecondPlayer.Ships.Count == 5)
                    {
                        _isSecondPlayerShipsPlanted = true;
                        PlayGame();
                    }
                }
            }
            else
            {
                foreach (Coordinate coordinate in _actualPlayer.Shoots)
                {
                    if (coordinate.Column == _coordinate.Column && coordinate.Row == _coordinate.Row)
                    {
                        return;
                    }
                }

                if (!_controller.IsGameOver(_controller.NextPlayer(_actualPlayer)))
                {
                    MakeShoot(_actualPlayer);
                    _actualPlayer = _controller.NextPlayer(_actualPlayer);
                    if (_gameMode == GameMode.TwoPlayerMode)
                    {
                        title.Content = "Te következel " + _actualPlayer.Name + "!";
                    }
                    else
                    {
                        if (_actualPlayer.Name == "Bot")
                        {
                            title.Content = "A Bot következik!";
                        }
                        else
                        {
                            title.Content = "Te következel " + _actualPlayer.Name + "!";
                        }
                    }
                }
            }
        }

        private void DrawShips(Player player)
        {
            foreach (Button button in PlayerOneBoard.Children)
            {
                foreach (Ship ship in player.Ships)
                {
                    foreach (Coordinate coordinate in ship.Positions)
                    {
                        if (button.Name.Substring(1) == coordinate.ToString())
                        {
                            button.Background = new SolidColorBrush(Colors.Green);
                        }
                    }
                }
            }
        }

        private void HideShips(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                button.Background = new SolidColorBrush(Colors.Wheat);
            }
        }

        private void ShipPlacement(Player player)
        {
            if (_gameMode == GameMode.TwoPlayerMode)
            {
                MessageBox.Show("Kérlek add át az egeret " + player.Name + " játékosnak!", "Hajólehelyezés", MessageBoxButton.OK, MessageBoxImage.Information);
                HideShips(PlayerOneBoard);
                firstPlayerName.Visibility = Visibility.Hidden;
                secondPlayerName.Visibility = Visibility.Hidden;
                EnemyBoard.Visibility = Visibility.Hidden;
                PlayerOneBoard.Margin = new Thickness(0, 0, 0, 0);
                title.Content = "Kérlek " + player.Name + "  rakd le a hajókat!";
            }
            else
            {
                HideShips(PlayerOneBoard);
                firstPlayerName.Visibility = Visibility.Hidden;
                secondPlayerName.Visibility = Visibility.Hidden;
                EnemyBoard.Visibility = Visibility.Hidden;
                PlayerOneBoard.Margin = new Thickness(0, 0, 0, 0);
                title.Content = "Kérlek " + player.Name + "  rakd le a hajókat!";
            }
        }
        private void PlayGame()
        {
            if (!_isGameScreenChanged)
            {
                HideShips(PlayerOneBoard);
                HideShips(EnemyBoard);
                EnemyBoard.Visibility = Visibility.Visible;
                PlayerOneBoard.Margin = new Thickness(-600, 70, 0, 0);
                _isGameScreenChanged = true;
                gameStartButton.Visibility = Visibility.Hidden;
                oneLong.Visibility = Visibility.Hidden;
                twoLong.Visibility = Visibility.Hidden;
                threeLong.Visibility = Visibility.Hidden;
                fourLong.Visibility = Visibility.Hidden;
                fiveLong.Visibility = Visibility.Hidden;
                alignmentButton.Visibility = Visibility.Hidden;
                firstPlayerName.Visibility = Visibility.Visible;
                secondPlayerName.Visibility = Visibility.Visible;
                firstPlayerName.Content = _controller.FirstPlayer.Name + " táblája";
                secondPlayerName.Content = _controller.SecondPlayer.Name + " táblája";
                roundCount.Content = "Körök száma: " + _controller.Match.NumberOfRounds;
                title.Content = "Te következel " + _controller.FirstPlayer.Name + "!";
                hitPlayerOne.Content = _controller.FirstPlayer.Name + " találatainak száma: " + _controller.FirstPlayer.HitCount;
                hitPlayerTwo.Content = _controller.SecondPlayer.Name + " találatainak száma: " + _controller.SecondPlayer.HitCount;
                SetStatisticVariable();
                livingShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} meglévő hajói: {_firstPlayerLivingShips}";
                livingShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} meglévő hajói: {_secondPlayerLivingShips}";
                deadShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} elsüllyedt hajói: {_firstPlayerDeadShips}";
                deadShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} elsüllyedt hajói: {_secondPlayerDeadShips}";
                DisableBoard(PlayerOneBoard);
            }
        }

        private void DisableBoard(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                button.IsEnabled = false;
            }
        }

        private void ActivateBoard(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                button.IsEnabled = true;
            }
        }

        private void SetFieldBackGroundToHit(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                if (_coordinate.ToString() == button.Name.Substring(1))
                {
                    button.Background = new SolidColorBrush(Colors.Red);
                }
            }
            if (_controller.IsGameOver(_actualPlayer))
            {
                MessageBox.Show("Vége a játéknak, " + _controller.NextPlayer(_actualPlayer).Name + " a nyertes!", "Játék vége", MessageBoxButton.OK, MessageBoxImage.Information);
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

                _controller.Match.AddWinner(_controller.NextPlayer(_actualPlayer).Name);
                foreach (int size in _controller.NextPlayer(_actualPlayer).LivingShips)
                {
                    _controller.Match.AddWinnerShip(_controller.NextPlayer(_actualPlayer), size);
                }
                MyFileHandler jsonFile = new MyFileHandler();
                List<Result> result() => new()
                {
                    new(_controller.FirstPlayer.Name, _controller.SecondPlayer.Name, _controller.NextPlayer(_actualPlayer).Name, _controller.Match.NumberOfRounds)
                };
                var data = result();
                jsonFile.PrettyWrite(data);
            }
        }

        private void SetFieldBackGroundToTaken(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                if (_coordinate.ToString() == button.Name.Substring(1))
                {
                    button.Background = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        private void MakeShoot(Player player)
        {
            if (_gameMode == GameMode.TwoPlayerMode)
            {
                if (!player.Shoots.Contains(_coordinate))
                {
                    player.Shoots.Add(_coordinate);
                    if (_controller.IsHit(player, _coordinate))
                    {
                        player.HitCount++;
                        _actualPlayer = _controller.NextPlayer(_actualPlayer);
                        if (_clickedButtonName[0] == 'A')
                        {
                            SetFieldBackGroundToHit(PlayerOneBoard);
                        }
                        else
                        {
                            SetFieldBackGroundToHit(EnemyBoard);
                        }
                        hitPlayerOne.Content = _controller.FirstPlayer.Name + " találatainak száma: " + _controller.FirstPlayer.HitCount;
                        hitPlayerTwo.Content = _controller.SecondPlayer.Name + " találatainak száma: " + _controller.SecondPlayer.HitCount;
                        SetStatisticVariable();
                        livingShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} meglévő hajói: {_firstPlayerLivingShips}";
                        livingShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} meglévő hajói: {_secondPlayerLivingShips}";
                        deadShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} elsüllyedt hajói: {_firstPlayerDeadShips}";
                        deadShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} elsüllyedt hajói: {_secondPlayerDeadShips}";
                    }
                    else
                    {
                        if (_actualPlayer.Name == _controller.SecondPlayer.Name)
                        {
                            _controller.Match.NumberOfRounds++;
                            roundCount.Content = "Körök száma: " + _controller.Match.NumberOfRounds;
                        }
                        if (_clickedButtonName[0] == 'A')
                        {
                            SetFieldBackGroundToTaken(PlayerOneBoard);
                            DisableBoard(PlayerOneBoard);
                            ActivateBoard(EnemyBoard);
                        }
                        else
                        {
                            SetFieldBackGroundToTaken(EnemyBoard);
                            DisableBoard(EnemyBoard);
                            ActivateBoard(PlayerOneBoard);
                        }
                        MessageBox.Show("Kérlek add át az egeret " + _controller.NextPlayer(_actualPlayer).Name + " játékosnak!", "Játékos váltás", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                if (!player.Shoots.Contains(_coordinate))
                {
                    player.Shoots.Add(_coordinate);
                    if (_controller.IsHit(player, _coordinate))
                    {
                        player.HitCount++;
                        _actualPlayer = _controller.NextPlayer(_actualPlayer);
                        if (_clickedButtonName[0] == 'A')
                        {
                            SetFieldBackGroundToHit(PlayerOneBoard);
                        }
                        else
                        {
                            SetFieldBackGroundToHit(EnemyBoard);
                        }
                        hitPlayerOne.Content = _controller.FirstPlayer.Name + " találatainak száma: " + _controller.FirstPlayer.HitCount;
                        hitPlayerTwo.Content = _controller.SecondPlayer.Name + " találatainak száma: " + _controller.SecondPlayer.HitCount;
                        SetStatisticVariable();
                        livingShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} meglévő hajói: {_firstPlayerLivingShips}";
                        livingShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} meglévő hajói: {_secondPlayerLivingShips}";
                        deadShipPlayerOne.Content = $"{_controller.FirstPlayer.Name} elsüllyedt hajói: {_firstPlayerDeadShips}";
                        deadShipPlayerTwo.Content = $"{_controller.SecondPlayer.Name} elsüllyedt hajói: {_secondPlayerDeadShips}";
                    }
                    else
                    {
                        if (_actualPlayer.Name == _controller.SecondPlayer.Name)
                        {
                            _controller.Match.NumberOfRounds++;
                            roundCount.Content = "Körök száma: " + _controller.Match.NumberOfRounds;
                        }
                        if (_clickedButtonName[0] == 'A')
                        {
                            SetFieldBackGroundToTaken(PlayerOneBoard);
                            DisableBoard(PlayerOneBoard);
                            ActivateBoard(EnemyBoard);
                            MessageBox.Show(_controller.NextPlayer(_actualPlayer).Name + " következik!", "Játékos váltás", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            SetFieldBackGroundToTaken(EnemyBoard);
                            DisableBoard(EnemyBoard);
                            ActivateBoard(PlayerOneBoard);
                            MessageBox.Show("A Bot következik!", "Játékos váltás", MessageBoxButton.OK, MessageBoxImage.Information);
                            _controller.AiTurn();
                        }
                    }
                }
            }
        }

        private void SetSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "fiveLong")
            {
                _ship.ShipSize = 5;
            }
            else if (button.Name == "fourLong")
            {
                _ship.ShipSize = 4;
            }
            else if (button.Name == "threeLong")
            {
                _ship.ShipSize = 3;
            }
            else if (button.Name == "twoLong")
            {
                _ship.ShipSize = 2;
            }
            else
            {
                _ship.ShipSize = 1;
            }
        }

        private void AlignmentChange(object sender, RoutedEventArgs e)
        {
            if (_ship.ShipAlignment == Alignment.Horizontal)
            {
                _ship.ShipAlignment = Alignment.Vertical;
                alignmentButton.Content = "Vertikális";
            }
            else
            {
                _ship.ShipAlignment = Alignment.Horizontal;
                alignmentButton.Content = "Horizontális";
            }
        }
        private void SetStatisticVariable()
        {
            _firstPlayerLivingShips = " ";
            _firstPlayerDeadShips = " ";
            _secondPlayerLivingShips = " ";
            _secondPlayerDeadShips = " ";
            foreach (var ship in _controller.FirstPlayer.LivingShips)
            {
                _firstPlayerLivingShips += $" {ship}, ";
            }
            foreach (var ship in _controller.SecondPlayer.LivingShips)
            {
                _secondPlayerLivingShips += $" {ship}, ";
            }
            foreach (var ship in _controller.FirstPlayer.DeadShips)
            {
                _firstPlayerDeadShips += $" {ship}, ";
            }
            foreach (var ship in _controller.SecondPlayer.DeadShips)
            {
                _secondPlayerDeadShips += $" {ship}, ";
            }
        }

        private void StartAiGame(object sender, RoutedEventArgs e)
        {
            while (_controller.SecondPlayer.Ships.Count < 5)
            {
                _controller.AiPlaceShip(5 - _controller.SecondPlayer.Ships.Count, _controller.SecondPlayer);
            }
            if (_controller.SecondPlayer.Ships.Count == 5)
            {
                _isSecondPlayerShipsPlanted = true;
                PlayGame();
            }
        }
    }
}
