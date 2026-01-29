using System.Windows;
using Project.WPFApp.ViewModels;

namespace Project.WPFApp.Windows
{
    /// <summary>
    /// Interaction logic for DatabaseConnectionWindow.xaml
    /// </summary>
    public partial class DatabaseConnectionWindow : Window
    {
        public DatabaseConnectionWindow(DatabaseConnectionViewModel viewModel)
        {
            InitializeComponent();
            
            this.DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }
    }
}
