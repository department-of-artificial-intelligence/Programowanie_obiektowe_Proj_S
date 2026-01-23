using System.Windows;
using Project.UI.ViewModel;
namespace Project.UI.Views;
public partial class AuthorWindow : Window
{
    public AuthorWindow(AuthorViewModel viewModel)
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
