using System.Windows;
using System.Windows.Input;
using Project.WPFApp.Common;

namespace Project.WPFApp.ViewModels
{
    public class CreateResidentViewModel : BaseViewModel
    {
        public WpfServices Services { get; init; } = new WpfServices();
        
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        
        public string RoomNumber { get; set; } = string.Empty;
        
        public ICommand CreateResidentCommand => RelayCommandFactory.Create(this.CreateResident);

        private async Task CreateResident()
        {
            var roomId = (int.TryParse(this.RoomNumber, out var parsedRoomId)) ? parsedRoomId : 0;
            
            var room = await this.Services!.RoomService!.GetRoomByNumberAsync(roomId);
            
            if (roomId == 0 || room is null)
            {
                MessageBox.Show("Invalid Room Number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var resident = await this.Services.ResidentService!.CreateResidentAsync(
                this.FirstName,
                this.LastName,
                this.DateOfBirth,
                DateTime.Now);

            await this.Services.RoomService.AddResidentToRoomAsync(room.Id, resident.Id);
            
            this.CloseRequest();
        }
    }
}