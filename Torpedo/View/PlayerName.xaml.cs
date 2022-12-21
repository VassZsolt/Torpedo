using System.Windows;
using System.Windows.Controls;
using NationalInstruments.Torpedo.Model;

namespace NationalInstruments.Torpedo.View
{
    public partial class PlayerName : Window
    {
        public string PlayerNameOne = string.Empty;
        public string PlayerNameTwo = string.Empty;
        private Player player = new Player(0, 0);
        public GameMode GameMode;

        public PlayerName()
        {
            InitializeComponent();
            startButton.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            Close();
        }

        private void FirstPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (player.IsCorrectName(firstPlayerName.Text) && !string.IsNullOrEmpty(firstPlayerName.Text))
            {
                wrongNameOne.Content = "Megfelelő a név";
                startButton.Visibility = Visibility.Visible;
            }
            else
            {
                wrongNameOne.Content = "Nem megfelelő a név";
                startButton.Visibility = Visibility.Hidden;
            }
        }

        private void SecondPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (player.IsCorrectName(secondPlayerName.Text) && !string.IsNullOrEmpty(secondPlayerName.Text))
            {
                wrongNameTwo.Content = "Megfelelő a név";
                startButton.Visibility = Visibility.Visible;
            }
            else
            {
                wrongNameTwo.Content = "Nem megfelelő a név";
                startButton.Visibility = Visibility.Hidden;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerNameOne = firstPlayerName.Text;
            if (secondPlayerName.Text.Length == 0)
            {
                GameMode = GameMode.SinglePlayerMode;
            }
            else
            {
                PlayerNameTwo = secondPlayerName.Text;
                GameMode = GameMode.TwoPlayerMode;
            }

            var gamePlay = new GamePlay(GameMode, PlayerNameOne, PlayerNameTwo);
            gamePlay.Show();
            Close();
        }
    }
}
