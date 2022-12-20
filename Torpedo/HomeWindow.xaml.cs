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
            //GameController game = new GameController(GameMode.TwoPlayerMode);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onePlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var onePlayerWindow = new OnePlayer();
            onePlayerWindow.Show();
            Close();
        }

        private void twoPlayerModeButton_Click(object sender, RoutedEventArgs e)
        {
            var twoPlayerWindow = new TwoPlayer();
            twoPlayerWindow.Show();
            Close();
        }
    }
}
