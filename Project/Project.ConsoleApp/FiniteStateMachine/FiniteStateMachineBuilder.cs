namespace Project.ConsoleApp.FiniteStateMachine
{
    public class FiniteStateMachineBuilder<TEnum, TContext> where TEnum : Enum
    {
        private readonly TContext _context;
        
        private readonly Dictionary<TEnum, MachineState<TEnum, TContext>> _states = new Dictionary<TEnum, MachineState<TEnum, TContext>>();
        
        public FiniteStateMachineBuilder(TContext context)
        {
            this._context = context;
        }
        
        public FiniteStateMachineBuilder<TEnum, TContext> AddState(TEnum state, Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> onEnterAsync)
        {
            var stateImpl = new MachineState<TEnum, TContext>(onEnterAsync, (_, _) => Task.CompletedTask);
            this._states[state] = stateImpl;

            return this;
        }
        
        public FiniteStateMachineBuilder<TEnum, TContext> AddState(
            TEnum state,
            Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> onEnterAsync,
            Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> onExitAsync)
        {
            var stateImpl = new MachineState<TEnum, TContext>(onEnterAsync, onExitAsync);
            this._states[state] = stateImpl;
            
            return this;
        }
        
        public FiniteStateMachine<TEnum, TContext> Build()
        {
            return new FiniteStateMachine<TEnum, TContext>(this._context, this._states);
        }
    }
}