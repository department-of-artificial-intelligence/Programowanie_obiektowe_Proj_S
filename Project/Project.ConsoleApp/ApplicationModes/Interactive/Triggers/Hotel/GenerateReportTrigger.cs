using System.Diagnostics.CodeAnalysis;
using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;
using Project.Reports.Generators;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel
{
    public record GenerateReportTrigger : IFiniteTrigger
    {
        public required Model.Hotel Hotel { get; init; }
        
        public GenerateReportTrigger() { }
        
        [SetsRequiredMembers]
        public GenerateReportTrigger(Model.Hotel hotel)
        {
            this.Hotel = hotel;
        }
        
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, GenerateReportTrigger trigger)
        {
            var hotel = await context.HotelService.GetFullHotelByIdAsync(trigger.Hotel.Id);

            var reportGenerator = new RevenueInTimeRangeReportGenerator(DateTime.Now.AddDays(-365), DateTime.Now);
            var report = reportGenerator.GenerateReport(hotel!);
            
            Console.WriteLine($"Annual revenue for hotel '{hotel!.Name}': {report.Details.Revenue:C}");
            
            InteractiveComponents.PressAnyKeyToContinue();
            return new ManageHotelTrigger(trigger.Hotel);
        }
    }
}