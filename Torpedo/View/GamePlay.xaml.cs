using NationalInstruments.Torpedo.Model;
using NationalInstruments.Torpedo.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NationalInstruments.Torpedo.View
{
    /// <summary>
    /// Interaction logic for GamePlay.xaml
    /// </summary>
    public partial class GamePlay : Window
    {
        GameController controller;
        string clickedButtonName = string.Empty;
        private Ship _ship = new Ship();
        private Coordinate _coordinate;
        private int numberOfClick = 0;
        public GamePlay(GameMode gameMode, string playerOneName, string? playerTwoName)
        {
            InitializeComponent();
            controller = new GameController(gameMode, playerOneName, playerTwoName);
            ShipPlacement();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            clickedButtonName = button.Name;

            _coordinate = controller.GetCoordinate(clickedButtonName);
            if (numberOfClick < 5)
            {
                controller.PlaceShip(_ship.ShipSize, _ship.ShipAlignment, _coordinate, controller.Firstplayer);
            }
            if(numberOfClick<10 && numberOfClick>4)
            {
                controller.PlaceShip(_ship.ShipSize, _ship.ShipAlignment, _coordinate, controller.SecondPlayer);
            }

            numberOfClick++;
            drawShips(controller.Firstplayer);
        }

        private void drawShips(Player player)
        {
            foreach(Button button in PlayerOneBoard.Children)
            {
                foreach (Ship ship in player.Ships)
                {
                    foreach (Coordinate coordinate in ship.Positions)
                    { 
                        if(button.Name==coordinate.ToString())
                        { button.Background = new SolidColorBrush(Colors.Green); }
                    }
                }
            }
        }
        private void ShipPlacement()
        {
            EnemyBoard.Visibility = Visibility.Hidden;
            PlayerOneBoard.Margin = new Thickness(0, 0, 0, 0);
            title.Content = "Kérlek " + controller.Firstplayer.Name + "  rakd le a hajókat!";
        }

        private void SetSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _ship.ShipAlignment = Alignment.Horizontal;
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
