using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record StartMenuTrigger : IFiniteTrigger
    {
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, StartMenuTrigger trigger)
        {
            var hotels = await context.HotelService.GetAllHotelsAsync();
            
            var dict = hotels.ToDictionary(
                h => $"Manage hotel '{h.Name}'", 
                h => (IFiniteTrigger) new ManageHotelTrigger(h));
            
            dict.Add("Create New Hotel", new CreateHotelTrigger());
            dict.Add("Create New Manager", new CreateManagerTrigger());
            dict.Add("Fire Manager", new FireManagerTrigger());
            dict.Add("Seed Test Hotel", new SeedTestHotel());
            dict.Add("Exit", new FiniteExitTrigger());

            return InteractiveComponents.SelectPrompt("Select an action or hotel to manage", dict);
        }
    }
}