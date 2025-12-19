using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record DatabasePreparationTrigger : IFiniteTrigger
    {
        private const string SeedDataPrompt = "There is no hotels in the database. Do you want to seed sample data?";
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, DatabasePreparationTrigger trigger)
        {
            if (!await context.HotelService.IsAnyHotelExistsAsync() && InteractiveComponents.YesNoPrompt(SeedDataPrompt))
            {
                return new SeedTestHotel();
            }

            return new StartMenuTrigger();
        }
    }
}