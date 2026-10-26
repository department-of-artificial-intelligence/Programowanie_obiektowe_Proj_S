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

namespace Projekt1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //klasa abstrakcyjna vs interfejs
            //dependecy injection związane z interfejsami
            //zamiast List uzywac IList
            //interface uzywany jest do tego aby nie miec w klasie zaleznosci od stringa ale od interfacu ktory implementuje rozne klasy
        }
    }
}