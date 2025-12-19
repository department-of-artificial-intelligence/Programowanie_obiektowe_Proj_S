using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
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
        
        public static Task<IFiniteTrigger> FiniteAction(ApplicationContext context, ManageHotelTrigger trigger)
        {
            Console.WriteLine($"Managing Hotel: {trigger.Hotel.Name}");

            var dict = new Dictionary<string, IFiniteTrigger>()
            {
                ["Change Hotel Manager"] = new ChangeHotelManagerTrigger(trigger.Hotel),
                ["Change Hotel Name"] = new ChangeHotelNameTrigger(trigger.Hotel),
                ["Generate Report"] = new GenerateReportTrigger(trigger.Hotel),
                ["Add New Room"] = new AddNewRoomTrigger(trigger.Hotel),
                ["Add New Resident"] = new CreateResidentTrigger(trigger.Hotel),
                ["Remove Resident"] = new RemoveResidentTrigger(trigger.Hotel),
                ["Return to Main Menu"] = new StartMenuTrigger()
            };
            
            return Task.FromResult(InteractiveComponents.SelectPrompt("Select an action to perform:", dict));
        }
    }
}