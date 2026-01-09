using Project.WPFApp.Common;
using System.Windows;

namespace Project.WPFApp.ViewModels
{
    public class DatabaseConnectionViewModel : BaseViewModel
    {
        public string ConnectionString { get; set; } = string.Empty;

        public WpfCommandAdapter<Window> ConnectCommand { get; set; } = WpfCommandAdapterFactory.Create((Window? w) => w?.Close());
    }
}
