using System.Diagnostics.CodeAnalysis;

namespace Project.ConsoleApp.FiniteStateMachine
{
    public class FiniteStateMachine<TStateEnum, TContext> where TStateEnum : Enum
    {
        private readonly Dictionary<TStateEnum, MachineState<TStateEnum, TContext>> _states = new Dictionary<TStateEnum, MachineState<TStateEnum, TContext>>();

        private MachineState<TStateEnum, TContext>? _currentState = null;
        
        public required TContext FsmContext { get; init; }
        
        public FiniteStateMachine() { }
        
        [SetsRequiredMembers]
        public FiniteStateMachine(TContext fsmState)
        {
            this.FsmContext = fsmState;
        }
        
        [SetsRequiredMembers]
        public FiniteStateMachine(TContext fsmState, Dictionary<TStateEnum, MachineState<TStateEnum, TContext>> states)
        {
            this._states = states;
            
            this.FsmContext = fsmState;
        }
        
        public void AddState(TStateEnum state, MachineState<TStateEnum, TContext> stateImpl)
        {
            this._states.Add(state, stateImpl);
        }
        
        public async Task TransitToStateAsync(TStateEnum state, CancellationToken cancellationToken = default)
        {
            if (this._states.TryGetValue(state, out var stateImpl))
            {
                await this.LeaveCurrentStateAsync(cancellationToken);
                await stateImpl.EnterState(this, cancellationToken);
                
                this._currentState = stateImpl;
            }
            else
            {
                throw new ArgumentException($"State '{state}' does not exist in the FSM.");
            }
        }
        
        public async Task LeaveCurrentStateAsync(CancellationToken cancellationToken = default)
        {
            if (this._currentState is not null)
            {
                await this._currentState.LeaveState(this, cancellationToken);
                this._currentState = null;
            }
        }
    }
}