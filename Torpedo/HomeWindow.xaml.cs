using System.Windows;
using NationalInstruments.Torpedo.View;

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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnePlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var playerName = new PlayerName();
            playerName.secondPlayerLabel.Visibility = Visibility.Hidden;
            playerName.secondPlayerName.Visibility = Visibility.Hidden;
            playerName.Show();
            Close();
        }

        private void TwoPlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var playerName = new PlayerName();
            playerName.Show();
            Close();
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            var results = new Results();
            results.Show();
            Close();
        }
    }
}
