using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record CreateResidentTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public CreateResidentTrigger() { }
        
        [SetsRequiredMembers]
        public CreateResidentTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, CreateResidentTrigger trigger)
        {
            var firstName = InteractiveComponents.ValuePrompt<string>("Enter resident's first name");
            var lastName = InteractiveComponents.ValuePrompt<string>("Enter resident's last name");
            var dateOfBirth = InteractiveComponents.ValuePrompt<DateTime>("Enter resident's date of birth");
            
            if (!InteractiveComponents.YesNoPrompt($"Are you sure you want to create resident '{firstName} {lastName}' (born on {dateOfBirth.ToShortDateString()})?"))
            {
                Console.WriteLine("Resident creation cancelled.");
                InteractiveComponents.PressAnyKeyToContinue();
                
                return new ManageHotelTrigger(trigger.Hotel);
            }

            var rooms = await context.RoomService.GetAllRoomsAsync(trigger.Hotel.Id);
            var dict = rooms
                .Where(r => !r.IsReserved)
                .ToDictionary(r => $"Room {r.Number} (Price: {r.PricePerDay:C})", r => r);
            
            var selectedRoom = InteractiveComponents.SelectPrompt("Select a room for the new resident", dict);
            
            if (!InteractiveComponents.YesNoPrompt($"Are you sure you want to assign room {selectedRoom.Number} to resident '{firstName} {lastName}'?"))
            {
                Console.WriteLine("Resident creation cancelled.");
                InteractiveComponents.PressAnyKeyToContinue();
                
                return new ManageHotelTrigger(trigger.Hotel);
            }
            
            var resident = await context.ResidentService.CreateResidentAsync(firstName, lastName, dateOfBirth, DateTime.Now);
            await context.RoomService.AddResidentToRoomAsync(selectedRoom.Id, resident.Id);
            
            Console.WriteLine($"Resident '{firstName} {lastName}' has been created and assigned to room {selectedRoom.Number}.");
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}