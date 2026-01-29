using Project.WPFApp.Windows;
using System.Windows;
using Project.WPFApp.Common;

namespace Project.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            new MainWindow(new WpfServices()).Show();
        }
    }
}
