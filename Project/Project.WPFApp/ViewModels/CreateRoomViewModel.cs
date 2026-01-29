using System.Windows.Input;
using Project.Model;
using Project.WPFApp.Common;

namespace Project.WPFApp.ViewModels
{
    public class CreateRoomViewModel : BaseViewModel
    {
        public WpfServices Services { get; init; } = new WpfServices();
        
        public Hotel? ChosenHotel { get; set; } = null;
        
        public string Number { get; set; } = string.Empty;
        
        public string Floor { get; set; } = string.Empty;
        
        public string PricePerDay { get; set; } = string.Empty;

        public ICommand CreateRoomCommand => RelayCommandFactory.Create(this.CreateRoom);
        
        private async Task CreateRoom()
        {
            var number = int.Parse(this.Number);
            var floor = int.Parse(this.Floor);
            var pricePerDay = decimal.Parse(this.PricePerDay);
            
            await this.Services.RoomService!.CreateRoomAsync(ChosenHotel!.Id, number, floor, pricePerDay);
            
            this.CloseRequest();
        }
    }
}