using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record CreateHotelTrigger : IFiniteTrigger
    {
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, CreateHotelTrigger trigger)
        {
            if (!await context.ManagerService.IsAnyManagerExistsAsync())
            {
                Console.WriteLine("No managers to assign.");
                InteractiveComponents.PressAnyKeyToContinue();

                return new StartMenuTrigger();
            }

            var name = InteractiveComponents.ValuePrompt<string>("Enter hotel name");
            var address = InteractiveComponents.ValuePrompt<string>("Enter hotel address");

            var managers = await context.ManagerService.GetAllManagersAsync()
                .ContinueWith(x => x.Result.ToDictionary(y => y.Person.FullName, y => y));

            var manager = InteractiveComponents.SelectPrompt("Choose a manager for the hotel", managers);
            
            await context.HotelService.CreateHotelAsync(name, address, manager);

            return new StartMenuTrigger();
        }
    }
}