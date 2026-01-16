
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Project.Model;
using Project.View.Abstractions;
using Project.Common;

namespace Project.ViewModel
{
    public class AddDriverViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        public ICommand AddDriverCommand { get; set; }
        public IAddDriverWindow AddDriverWindow { get; }

        public AddDriverViewModel(IAddDriverWindow addDriverWindow, IServiceProvider serviceProvider)
        {
            AddDriverWindow = addDriverWindow;
            AddDriverWindow.Driver = new Driver();
            _serviceProvider = serviceProvider;
            AddDriverCommand = new RelayCommand(AddDriver_Click);
        }

        private void AddDriver_Click(object sender)
        {
            if (AddDriverWindow.Driver.FirstName is null || !Regex.IsMatch(AddDriverWindow.Driver.FirstName, @"^\p{Lu}{1,12}\p{Ll}{1,12}$") ||
                AddDriverWindow.Driver.LastName is null || !Regex.IsMatch(AddDriverWindow.Driver.LastName, @"^\p{Lu}{1,12}$") ||
                !Regex.IsMatch(AddDriverWindow.Driver.DriverNo.ToString(), @"^[0-9]{4,10}$"))

            {
                MessageBox.Show("Invalid data.");
                return;
            }
            AddDriverWindow.DialogResult = true;
        }
    }
}
