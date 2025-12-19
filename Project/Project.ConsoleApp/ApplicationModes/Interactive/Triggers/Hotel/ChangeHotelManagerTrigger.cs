using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record ChangeHotelManagerTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public ChangeHotelManagerTrigger() { }
        
        [SetsRequiredMembers]
        public ChangeHotelManagerTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, ChangeHotelManagerTrigger trigger)
        {
            var managers = await context.ManagerService.GetAllManagersAsync();
            var dict = managers.ToDictionary(m => m.Person.FullName, m => m);

            var manager = InteractiveComponents.SelectPrompt($"Select a new manager for the hotel '{trigger.Hotel.Name}'", dict);
            
            if (InteractiveComponents.YesNoPrompt("Are you sure you want to change the hotel manager?"))
            {
                await context.HotelService.ChangeHotelManagerAsync(trigger.Hotel, manager);
                Console.WriteLine($"Hotel manager changed to '{trigger.Hotel.Manager.Person.FullName}'");
            }
            else
            {
                Console.WriteLine("Hotel manager change cancelled.");
            }
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}