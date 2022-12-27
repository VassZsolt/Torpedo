using NationalInstruments.Torpedo.View;
using System.Windows;

namespace NationalInstruments.Torpedo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onePlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var playerName = new PlayerName();
            playerName.secondPlayerLabel.Visibility = Visibility.Hidden;
            playerName.secondPlayerName.Visibility = Visibility.Hidden;
            playerName.Show();
            Close();
        }

        private void twoPlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var playerName = new PlayerName();
            playerName.Show();
            Close();
        }
    }
}
