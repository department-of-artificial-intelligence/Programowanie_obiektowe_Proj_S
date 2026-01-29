using System.Collections.ObjectModel;
using System.Windows.Input;
using Project.Model;
using Project.WPFApp.Common;
using Project.WPFApp.Windows;

namespace Project.WPFApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Hotel? _chosenHotel;
        
        public bool IsReady => this.Services?.IsReady ?? false;
        
        public bool IsCanBeConnected => !this.Services?.IsReady ?? false;
        
        public bool IsHotelChosen => this.ChosenHotel != null;
        
        public WpfServices? Services { get; init; }

        public List<HotelRoom> Rooms => this.ChosenHotel?.Rooms ?? new List<HotelRoom>();
        
        public List<Resident> Residents => this.ChosenHotel?.Residents ?? new List<Resident>();
        
        public Hotel? ChosenHotel
        {
            get => this._chosenHotel;
            set
            {
                this._chosenHotel = value;
                
                this.OnPropertyChanged(nameof(this.ChosenHotel));
                this.OnPropertyChanged(nameof(this.IsHotelChosen));
                
                this.UpdateRoomsAndResidents();
            }
        }
        
        public ObservableCollection<Hotel> Hotels { get; private set; } = new ObservableCollection<Hotel>();
        
        public ICommand CloseDatabaseCommand => RelayCommandFactory.Create(this.CloseDatabaseConnection);
        
        public ICommand OpenDatabaseCommand => RelayCommandFactory.Create(this.OpenDatabaseConnection);
        
        public ICommand CreateHotelCommand => RelayCommandFactory.Create(this.CreateHotel);

        public ICommand CreateManagerCommand => RelayCommandFactory.Create(this.CreateManager);
        
        public ICommand ExitCommand => RelayCommandFactory.Create(this.CloseRequest);

        public ICommand AddResidentCommand => RelayCommandFactory.Create(this.AddResident);
        
        public ICommand CreateRoomCommand => RelayCommandFactory.Create(this.CreateRoom);
        
        public ICommand EvictResidentCommand => RelayCommandFactory.Create((Resident? resident) => _ = this.RemoveResident(resident));

        private void UpdateRoomsAndResidents()
        {
            this.OnPropertyChanged(nameof(this.Rooms));
            this.OnPropertyChanged(nameof(this.Residents));
        }
        
        private async Task UpdateHotelList()
        {
            this.Hotels.Clear();
            (await this.Services!.HotelService!.GetAllHotelsAsync()).ForEach(x => this.Hotels.Add(x));

            this.OnPropertyChanged(nameof(this.Hotels));
        }
        
        private void UpdateDatabaseConnectionStatus()
        {
            this.OnPropertyChanged(nameof(this.IsReady));
            this.OnPropertyChanged(nameof(this.IsCanBeConnected));
            
            _ = this.UpdateHotelList();
        }
        
        private void CloseDatabaseConnection()
        {
            this.Services?.CloseDatabaseConnectionAsync();
            this.UpdateDatabaseConnectionStatus();
        }
        
        private void OpenDatabaseConnection()
        {
            var vm = new DatabaseConnectionViewModel()
            {
                Services = this.Services!
            };
            
            new DatabaseConnectionWindow(vm).ShowDialog();
            
            this.UpdateDatabaseConnectionStatus();
        }

        private async Task CreateHotel()
        {
            if (this.Services?.IsReady != true)
                return;
            
            var vm = new CreateHotelViewModel
            {
                Services = this.Services,
                AvailableManagers = await this.Services.ManagerService!.GetAllManagersAsync()
            };
            
            var window = new CreateHotelWindow(vm);
            window.ShowDialog();

            this.Hotels.Add(vm.AddedHotel!);
        }
        
        private void CreateManager()
        {
            if (this.Services?.IsReady != true)
                return;
            
            var vm = new CreateManagerViewModel
            {
                Services = this.Services
            };
            
            var window = new CreateManagerWindow(vm);
            window.ShowDialog();
        }

        private void AddResident()
        {
            if (this.Services?.IsReady != true || this.ChosenHotel == null)
                return;
            
            var vm = new CreateResidentViewModel
            {
                Services = this.Services
            };
            
            var window = new CreateResidentWindow(vm);
            window.ShowDialog();
            
            this.UpdateRoomsAndResidents();
        }
        
        private async Task CreateRoom()
        {
            if (this.Services?.IsReady != true || this.ChosenHotel == null)
                return;
            
            var vm = new CreateRoomViewModel
            {
                Services = this.Services,
                ChosenHotel = this.ChosenHotel
            };
            
            var window = new CreateRoomWindow(vm);
            window.ShowDialog();

            await this.UpdateHotelList();
            
            this.OnPropertyChanged(nameof(this.Rooms));
        }
        
        private async Task RemoveResident(Resident? resident)
        {
            if (this.Services?.IsReady != true || resident == null)
                return;
            
            await this.Services.ResidentService!.EvictResidentAsync(resident);
            
            await this.UpdateHotelList();
            
            this.OnPropertyChanged(nameof(this.Residents));
        }
    }
}
