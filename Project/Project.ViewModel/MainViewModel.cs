using Project.Common;
using Project.Model;
using Project.View.Abstractions;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel
{
    public class MainViewModel
    {
        public List<Driver> Drivers { get; set; }

        private readonly IMainWindow _mainWindow;
        private readonly IServiceProvider _serviceProvider;

        public ICommand LoadCommand { get; set; }
        public ICommand ShowAddOrderWindowCommand { get; set; }
        public ICommand ShowAddDriverWindowCommand { get; set; }
        public ICommand DeleteDriverCommand { get; set; }

        public MainViewModel(IMainWindow mainWindow, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mainWindow = mainWindow;
            Drivers = new List<Driver>();
            Drivers.AddRange(new List<Driver>() {
                new Driver() {FirstName = "John", LastName="Taylor", LicenseNumber="ABC 12345"},
                new Driver() {FirstName = "Anna", LastName="Smith", LicenseNumber="ABC 12346"},
                new Driver() {FirstName = "Julia", LastName="Williams", LicenseNumber="ABC 12345"}
            });
            LoadCommand = new RelayCommand(LoadCommand_Loaded);
            ShowAddOrderWindowCommand = new RelayCommand(ShowAddOrderWindow_Click);
            ShowAddDriverWindowCommand = new RelayCommand(ShowAddDriverWindow_Click);
            DeleteDriverCommand = new RelayCommand(DeleteDriverCommand_Click);
        }

        private void ShowAddOrderWindow_Click(object sender)
        {
            if (_mainWindow != null &&
                _mainWindow.DataGridDrivers.SelectedItem != null &&
                _mainWindow.DataGridDrivers.SelectedItem is Driver selectedDriver)
            {
                var addOrderWindow = _serviceProvider.GetRequiedService<IAddOrderWindow>();
                addOrderWindow.DataContext = new AddOrderViewModel(addOrderWindow, selectedDriver, _serviceProvider);
                if(addOrderWindow.ShowDiaglog() == true)
                    _mainWindow.DataGridDrivers.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Select driver.");
            }
        }

        private void ShowAddDriverWindow_Click(object sender)
        {
            var addDriverWindow = _serviceProvider.GetRequiredService<IAddDriverWindow>();
            addDriverWindow.DataContext = new AddDriverViewModel(addDriverWindow, _serviceProvider);
            if(addDriverWindow.ShowDiaglog() == true)
            {
                Drivers.Add(addDriverWindow.Driver);
                _mainWindow.DataGridDrivers.Items.Refresh();
            }
        }

        private void DeleteDriver_Click(object sender)
        {
            if (_mainWindow != null &&
                _mainWindow.DataGridDrivers.SelectedItem != null &&
                _mainWindow.DataGridDrivers.SelectedItems is Driver selectedDriver)
            {
                if (Drivers.Contains(selectedDriver))
                {
                    Drivers.Remove(selectedDriver);
                    _mainWindow.DataGridDrivers.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Select driver.")
            }
        }

        private void LoadCommand_Click(object obj)
        {

        }
    }
}
