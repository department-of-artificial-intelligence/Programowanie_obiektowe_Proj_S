using Project.WPFApp.ViewModels;
using Project.WPFApp.Windows;
using System.Windows;

namespace Project.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            new MainWindow()
            {
                DataContext = new MainWindowViewModel()
                {
                    Title = "Meow~!"
                }
            }.Show();
        }
    }
}
