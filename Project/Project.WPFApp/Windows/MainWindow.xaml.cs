using System.Windows;
using Project.WPFApp.Common;

namespace Project.WPFApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(WpfServices services)
        {
            InitializeComponent();
            
            this.DataContext = new ViewModels.MainWindowViewModel()
            {
                Services = services
            };
        }
    }
}