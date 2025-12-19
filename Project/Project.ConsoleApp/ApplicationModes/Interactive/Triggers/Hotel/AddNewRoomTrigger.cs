using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record AddNewRoomTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public AddNewRoomTrigger() { }
        
        [SetsRequiredMembers]
        public AddNewRoomTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, AddNewRoomTrigger trigger)
        {
            var number = InteractiveComponents.ValuePrompt<int>($"Enter room number to add to hotel '{trigger.Hotel.Name}'");
            var floor = InteractiveComponents.ValuePrompt<int>($"Enter floor for room number {number}");
            var price = InteractiveComponents.ValuePrompt<decimal>($"Enter price per night for room number {number}");
            
            if (InteractiveComponents.YesNoPrompt($"Are you sure you want to add room number {number} to hotel '{trigger.Hotel.Name}'?"))
            {
                await context.RoomService.CreateRoomAsync(trigger.Hotel.Id, number, floor, price);
                Console.WriteLine($"Room number {number} added to hotel '{trigger.Hotel.Name}'");
            }
            else
            {
                Console.WriteLine("Add new room cancelled.");
            }
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}