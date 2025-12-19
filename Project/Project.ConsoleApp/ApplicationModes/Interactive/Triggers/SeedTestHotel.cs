using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record SeedTestHotel : IFiniteTrigger
    {
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, SeedTestHotel trigger)
        {
            await context.HotelService.SeedSampleHotelAsync();
            return new StartMenuTrigger();
        }
    }
}