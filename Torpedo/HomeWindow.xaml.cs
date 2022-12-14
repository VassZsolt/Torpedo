using NationalInstruments.Torpedo.ViewModel;
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
            GameController game = new GameController(GameMode.TwoPlayerMode);
        }
    }
}
