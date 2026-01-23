using System.Windows;
using Project.UI.ViewModel;

namespace Project.UI.Views;

public partial class MovieWindow : Window
{
    public MovieWindow(MovieViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        
        viewModel.RequestClose += (sender, args) =>
        {
            DialogResult = args;
            Close();
        };
    }
}
