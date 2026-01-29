using System.Windows;
using Project.WPFApp.ViewModels;

namespace Project.WPFApp.Windows
{
    public partial class CreateManagerWindow : Window
    {
        public CreateManagerWindow(CreateManagerViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
            vm.RequestClose += this.Close;
        }
    }
}