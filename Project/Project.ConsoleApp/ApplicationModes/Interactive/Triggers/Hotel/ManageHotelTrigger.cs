using System.Diagnostics.CodeAnalysis;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record ManageHotelTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public ManageHotelTrigger() { }

        [SetsRequiredMembers]
        public ManageHotelTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, ManageHotelTrigger trigger)
        {
            Console.WriteLine($"Managing Hotel: {trigger.Hotel.Name} - Not Implemented Yet");
            
            return new StartMenuTrigger(); // TODO
        }
    }
}