using System.Windows;
using System.Windows.Controls;
using NationalInstruments.Torpedo.Model;


namespace NationalInstruments.Torpedo.View
{
    /// <summary>
    /// Interaction logic for OnePlayer.xaml
    /// </summary>

    public partial class OnePlayer : Window
    {
        public string PlayerNameOne = string.Empty;
        public string PlayerNameTwo = ".Bot";
        private Player player = new Player(0, 0);
        public GameMode GameMode;
        public OnePlayer()
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

        private void PlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (player.IsCorrectName(playerName.Text) && !string.IsNullOrEmpty(playerName.Text))
            {
                wrongName.Content = "Megfelelő a név";
                startButton.Visibility = Visibility.Visible;
            }
            else
            {
                wrongName.Content = "Nem megfelelő a név";
                startButton.Visibility = Visibility.Hidden;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerNameOne = playerName.Text;
            GameMode = GameMode.SinglePlayerMode;
            var gamePlay = new GamePlay(GameMode, PlayerNameOne, PlayerNameTwo);
            gamePlay.Show();
            Close();
        }
    }
}
