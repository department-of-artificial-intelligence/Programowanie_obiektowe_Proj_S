using Project.ConsoleApp.ApplicationModes.Interactive.Components;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive.Triggers
{
    public record CreateManagerTrigger : IFiniteTrigger
    {
        public static async Task<IFiniteTrigger> FiniteAction(ApplicationContext context, CreateManagerTrigger trigger)
        {
            var firstName = InteractiveComponents.ValuePrompt<string>("Enter manager first name");
            var lastName = InteractiveComponents.ValuePrompt<string>("Enter manager last name");
            var dateOfBirth = InteractiveComponents.ValuePrompt<DateTime>("Enter manager date of birth");
            
            Console.WriteLine($"Manager: {firstName} {lastName} born on {dateOfBirth:yyyy-MM-dd}");
            
            if (!InteractiveComponents.YesNoPrompt("Is this information correct?"))
            {
                Console.WriteLine("Creation cancelled.");
                InteractiveComponents.PressAnyKeyToContinue();
                
                return new StartMenuTrigger();
            }

            await context.ManagerService.CreateManagerAsync(firstName, lastName, dateOfBirth);
            
            return new StartMenuTrigger();
        }
    }
}