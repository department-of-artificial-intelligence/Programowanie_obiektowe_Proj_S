using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record ChangeHotelNameTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public ChangeHotelNameTrigger() { }
        
        [SetsRequiredMembers]
        public ChangeHotelNameTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, ChangeHotelNameTrigger trigger)
        {
            var newName = InteractiveComponents.ValuePrompt<string>($"Enter new name for hotel (current: '{trigger.Hotel.Name}')");
         
            if (InteractiveComponents.YesNoPrompt("Are you sure you want to change the hotel name?"))
            {
                await context.HotelService.ChangeHotelNameAsync(trigger.Hotel, newName);
                Console.WriteLine($"Hotel name changed to '{trigger.Hotel.Name}'");
            }
            else
            {
                Console.WriteLine("Hotel name change cancelled.");
            }
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}