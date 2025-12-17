using Project.ConsoleApp.FiniteStateMachine;

namespace Project.ConsoleApp.ApplicationModes.Interactive
{
    public class InteractiveMode : IApplicationMode
    {
        private readonly FiniteStateMachine<State, int> _stateMachine = new FiniteStateMachineBuilder<State, int>(0)
            .AddState(State.Hello, async (machine, _) =>
            {
                Console.WriteLine("Hello");
                Console.ReadKey(intercept: true);

                await machine.TransitToStateAsync(State.World);
            })
            .AddState(State.World, async (machine, _) =>
            {
                Console.WriteLine("World");
                Console.ReadKey(intercept: true);
                
                await machine.TransitToStateAsync(State.Hello);
            })
            .Build();
        
        public async Task Run(ApplicationContext context)
        {
            await this._stateMachine.TransitToStateAsync(State.Hello);
        }
        
        private enum State
        {
            Hello,
            World
        }
    }
}