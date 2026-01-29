using System.Windows;
using Project.WPFApp.ViewModels;

namespace Project.WPFApp.Windows
{
    public partial class CreateResidentWindow : Window
    {
        public CreateResidentWindow(CreateResidentViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }
    }
}