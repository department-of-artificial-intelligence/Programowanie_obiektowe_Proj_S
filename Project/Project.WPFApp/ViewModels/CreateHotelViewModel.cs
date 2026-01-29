using System.Windows.Input;
using Project.Model;
using Project.WPFApp.Common;

namespace Project.WPFApp.ViewModels
{
    public class CreateHotelViewModel : BaseViewModel
    {
        public WpfServices Services { get; set; } = new WpfServices();
        
        public bool IsCanBeCreated { get; set; } = true;
        
        public string Name { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;

        public Manager? Manager { get; set; } = null;

        public Hotel? AddedHotel { get; set; } = null; // . . .
        
        public List<Manager> AvailableManagers { get; set; } = new List<Manager>();
        
        public ICommand CreateHotelCommand { get; set; }

        public CreateHotelViewModel()
        {
            this.CreateHotelCommand = RelayCommandFactory.Create(CreateHotel);
        }

        private async Task CreateHotel()
        {
            if (this.Manager is null)
                return;
            
            this.IsCanBeCreated = false;

            var hotel = await this.Services.HotelService?.CreateHotelAsync(this.Name, this.Address, this.Manager)!;
            this.AddedHotel = hotel;
            
            this.CloseRequest();
        }
    }
}