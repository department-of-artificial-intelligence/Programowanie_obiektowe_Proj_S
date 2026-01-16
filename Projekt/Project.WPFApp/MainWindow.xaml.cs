using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.WPFApp
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

        /*/

        private void ButtPokaz(object sender, RoutedEventArgs e)
        {
            var okno = new OknoListaPrac();
            okno.ShowDialog();
        }

        private void ButtZatrudnij(object sender, RoutedEventArgs e)
        {
            var okno = new OknoZatrudnij();
            okno.ShowDialog();
        }

        private void ButtWyplaty(object sender, RoutedEventArgs e)
        {
            var okno = new OknoWyplaty();
            okno.ShowDialog();
        }

        private void ButtNajlepszy(object sender, RoutedEventArgs e)
        {
            var okno = new OknoNajlepszy();
            okno.ShowDialog();
        }

        private void ButtZwolnij(object sender, RoutedEventArgs e)
        {
            var okno = new OknoZwolnij();
            okno.ShowDialog();
        }

        private void ButtWyjscie(object sender, RoutedEventArgs e)
        {

        }
       /*/
    }
}