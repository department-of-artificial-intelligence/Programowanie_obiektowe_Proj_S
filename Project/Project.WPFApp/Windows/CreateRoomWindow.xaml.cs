using System.Windows;
using Project.WPFApp.ViewModels;

namespace Project.WPFApp.Windows
{
    public partial class CreateRoomWindow : Window
    {
        public CreateRoomWindow(CreateRoomViewModel viewModel)
        {
            InitializeComponent();
            
            this.DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }
    }
}