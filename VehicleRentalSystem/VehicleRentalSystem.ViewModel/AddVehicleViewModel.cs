using System.Windows.Input;
using VehicleRentalSystem.Common;
using VehicleRentalSystem.View.Abstractions;
using VehicleRentalSystem.Model;
using System.Text.RegularExpressions;
using System.Windows;

namespace VehicleRentalSystem.ViewModel
{
    public class AddVehicleViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        public ICommand AddVehicleCommand { get; set; }
        public IAddVehicleWindow AddVehicleWindow { get; }
        public AddVehicleViewModel(IAddVehicleWindow addVehicleWindow, IServiceProvider serviceProvider)
        {
            AddVehicleWindow = addVehicleWindow;
            AddVehicleWindow.Vehicle = new Vehicle();
            _serviceProvider = serviceProvider;
            AddVehicleCommand = new RelayCommand(AddVehicle_Click);
        }

        private void AddVehicle_Click(object sender)
        {
            if (AddVehicleWindow.Vehicle.Brand is null || !Regex.IsMatch(AddVehicleWindow.Vehicle.Brand, @"^\p{L}{1,12}$") ||
                AddVehicleWindow.Vehicle.Model is null || !Regex.IsMatch(AddVehicleWindow.Vehicle.Model, @"^\p{L}{1,12}$") ||
                Regex.IsMatch(AddVehicleWindow.Vehicle.ProdYear.ToString(), @"^(19\d{2}|20[0-9]{2})$"))
            {
                MessageBox.Show("Nieprawidłowe dane");
                return;
            }
            AddVehicleWindow.DialogResult = true;
        }
    }

}
