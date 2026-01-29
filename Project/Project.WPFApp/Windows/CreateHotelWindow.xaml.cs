using System.Windows;
using Project.WPFApp.ViewModels;

namespace Project.WPFApp.Windows
{
    public partial class CreateHotelWindow : Window
    {
        public CreateHotelWindow(CreateHotelViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
            vm.RequestClose += this.Close;
        }
    }
}