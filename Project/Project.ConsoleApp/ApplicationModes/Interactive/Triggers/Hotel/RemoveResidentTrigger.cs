using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record RemoveResidentTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public RemoveResidentTrigger() { }
        
        [SetsRequiredMembers]
        public RemoveResidentTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, RemoveResidentTrigger trigger)
        {
            var residents = await context.ResidentService.GetAllResidents(trigger.Hotel.Id);
            
            var dict = residents
                .ToDictionary(x => x.Person.FullName, x => x);
            
            var residentToRemove = InteractiveComponents.SelectPrompt("Select a resident to remove", dict);
            
            if (InteractiveComponents.YesNoPrompt($"Are you sure you want to remove resident '{residentToRemove.Person.FullName}'?"))
            {
                await context.ResidentService.EvictResidentAsync(residentToRemove);
                Console.WriteLine($"Resident '{residentToRemove.Person.FullName}' has been removed.");
            }
            else
            {
                Console.WriteLine("Resident removal cancelled.");
            }
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}