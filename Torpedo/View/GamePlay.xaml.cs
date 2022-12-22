using System.Numerics;
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
        private GameController _controller;
        private GameMode _gameMode;
        private string _clickedButtonName = string.Empty;
        private Ship _ship = new Ship();
        private Coordinate _coordinate;
        private bool _isFirstPlayerShipsPlanted = false;
        private bool _isSecondPlayerShipsPlanted = false;
        private bool _isGameScreenChanged = false;
        public GamePlay(GameMode gameMode, string playerOneName, string? playerTwoName)
        {
            InitializeComponent();
            _controller = new GameController(gameMode, playerOneName, playerTwoName);
            _gameMode = gameMode;
            if (!_isFirstPlayerShipsPlanted)
            {
                ShipPlacement(_controller.Firstplayer);
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _clickedButtonName = button.Name;
            _coordinate = _controller.GetCoordinate(_clickedButtonName);
            if (!_isFirstPlayerShipsPlanted)
            {
                if (_controller.Firstplayer.Ships.Count < 5)
                {
                    _controller.PlaceShip(_ship.ShipSize, _ship.ShipAlignment, _coordinate, _controller.Firstplayer);
                    DrawShips(_controller.Firstplayer);
                }
                if (_controller.Firstplayer.Ships.Count == 5)
                {
                    _isFirstPlayerShipsPlanted = true;
                    ShipPlacement(_controller.SecondPlayer);
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
                MakeShoot(_controller.Firstplayer);
            }

            /*
            Player actualPlayer = _controller.Firstplayer;
            if (!_controller.IsGameOver(_controller.NextPlayer(actualPlayer)))
            {
                MakeShoot(actualPlayer);
                actualPlayer = _controller.NextPlayer(_controller.Firstplayer);
            }*/
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
                        { button.Background = new SolidColorBrush(Colors.Green); }
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
            MessageBox.Show("Kérlek add át az egeret "+player.Name+" játékosnak!", "Hajólehelyezés", MessageBoxButton.OK, MessageBoxImage.Information);
            HideShips(PlayerOneBoard);
            EnemyBoard.Visibility = Visibility.Hidden;
            PlayerOneBoard.Margin = new Thickness(0, 0, 0, 0);
            title.Content = "Kérlek " + player.Name + "  rakd le a hajókat!";
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
                oneLong.Visibility = Visibility.Hidden;
                twoLong.Visibility = Visibility.Hidden;
                threeLong.Visibility = Visibility.Hidden;
                fourLong.Visibility = Visibility.Hidden;
                fiveLong.Visibility = Visibility.Hidden;
                alignmentButton.Visibility = Visibility.Hidden;

                title.Content = "Te következel " + _controller.Firstplayer.Name + "!";
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
                button.IsEnabled = false;
            }
        }

        private void SetFielBackGroundToHit(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                if (_coordinate.ToString() == button.Name.Substring(1))
                {
                    button.Background = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void SetFielBackGroundToTaken(Grid grid)
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
            //szerintem ez a szűrő nem lesz jó
            if (!player.Shoots.Contains(_coordinate))
            {
                player.Shoots.Add(_coordinate);
                if (_controller.IsHit(player, _coordinate))
                {
                    if (_clickedButtonName[0] == 'A')
                    {
                        SetFielBackGroundToHit(PlayerOneBoard);
                    }
                    else { SetFielBackGroundToHit(EnemyBoard); }
                }
                else
                {
                    if (_clickedButtonName[0] == 'A')
                    {
                        SetFielBackGroundToTaken(PlayerOneBoard);
                    }
                    else { SetFielBackGroundToTaken(EnemyBoard); }
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
    }
}
