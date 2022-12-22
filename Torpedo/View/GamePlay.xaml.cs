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
            if (!_isSecondPlayerShipsPlanted && _isFirstPlayerShipsPlanted)
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

        }

        private void DrawShips(Player player)
        {
            foreach (Button button in PlayerOneBoard.Children)
            {
                foreach (Ship ship in player.Ships)
                {
                    foreach (Coordinate coordinate in ship.Positions)
                    {
                        if (button.Name == coordinate.ToString())
                        { button.Background = new SolidColorBrush(Colors.Green); }
                    }
                }
            }
        }

        private void HideShips(Grid grid)
        {
            foreach (Button button in grid.Children)
            {
                button.Background = null;
            }
        }

        private void ShipPlacement(Player player)
        {
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

        private void alignmentChange(object sender, RoutedEventArgs e)
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
