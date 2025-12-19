using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record FireManagerTrigger : IFiniteTrigger
    {
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, FireManagerTrigger trigger)
        {
            var managers = await context.ManagerService.GetAllManagersAsync();
            var dict = managers.ToDictionary(x => x.Person.FullName, x => x);
            
            var manager = InteractiveComponents.SelectPrompt("Select a manager to fire", dict);
            var hotels = await context.HotelService.GetHotelsByManagerAsync(manager);
            
            if (hotels.Any())
            {
                Console.WriteLine($"Cannot fire! Manager {manager.Person.FullName} is currently managing the following hotels:");
                hotels.ForEach(x => Console.WriteLine(x.Name));
                
                InteractiveComponents.PressAnyKeyToContinue();
                
                return new StartMenuTrigger();
            }
            
            await context.ManagerService.FireManagerAsync(manager);
            
            return new StartMenuTrigger();
        }
    }
}