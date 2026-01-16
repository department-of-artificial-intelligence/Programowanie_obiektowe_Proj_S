using System.Windows.Input;
using VehicleRentalSystem.Common;
using VehicleRentalSystem.View.Abstractions;
using VehicleRentalSystem.Model;
using System.Text.RegularExpressions;
using System.Windows;

namespace VehicleRentalSystem.ViewModel
{
    public class AddDepartmentViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        public ICommand AddDepartmentCommand { get; set; }
        public IAddDepartmentWindow AddDepartmentWindow { get; }
        public AddDepartmentViewModel(IAddDepartmentWindow addDepartmentWindow, IServiceProvider serviceProvider)
        {
            AddDepartmentWindow = addDepartmentWindow;
            AddDepartmentWindow.Department = new Department();
            _serviceProvider = serviceProvider;
            AddDepartmentCommand = new RelayCommand(AddVehicle_Click);
        }

        private void AddVehicle_Click(object sender)
        {
            if (AddDepartmentWindow.Department.Name is null || !Regex.IsMatch(AddDepartmentWindow.Department.Name, @"^\p{L}{1,12}$") ||
                AddDepartmentWindow.Department.City is null || !Regex.IsMatch(AddDepartmentWindow.Department.City, @"^\p{L}{1,12}$") ||
                Regex.IsMatch(AddDepartmentWindow.Department.PhoneNumber.ToString(), @"^$"))
            {
                MessageBox.Show("Nieprawidłowe dane");
                return;
            }
            AddDepartmentWindow.DialogResult = true;
        }
    }

}
