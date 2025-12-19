using Project.FSM;
using Project.FSM.Triggers;

namespace Project.ConsoleApp.ApplicationModes.Interactive
{
    public class InteractiveMode : IApplicationMode
    {
        private readonly FiniteStateMachine<int> _fsm = new FiniteStateMachineBuilder<int>()
            .AddState<HelloTrigger>((context, trigger) =>
            {
                Console.Write("Hello, ");
                Console.ReadKey(true);
                
                return Task.FromResult<IFiniteTrigger>(new WorldTrigger());
            })
            .AddState<WorldTrigger>((context, trigger) =>
            {
                Console.WriteLine("World!");
                Console.ReadKey(true);
                
                return Task.FromResult<IFiniteTrigger>(new HelloTrigger());
            })
            .Build();
        
        public async Task Run(ApplicationContext context)
        {
            await this._fsm.RunAsync(0, new HelloTrigger());
        }

        private record HelloTrigger : IFiniteTrigger;
        
        private record WorldTrigger : IFiniteTrigger;
    }
}