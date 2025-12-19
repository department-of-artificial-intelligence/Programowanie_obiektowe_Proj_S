using Project.ConsoleApp.ApplicationModes.Interactive.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive
{
    public class InteractiveMode : IApplicationMode
    {
        public async Task Run(ApplicationContext context)
        {
            await FiniteInteractive.StateMachine.RunAsync(context, new DatabasePreparationTrigger());
        }
    }
}