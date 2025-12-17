using System.Diagnostics.CodeAnalysis;

namespace Project.ConsoleApp.FiniteStateMachine
{
    public class MachineState<TEnum, TContext> where TEnum : Enum
    {
        public required Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> OnEnterAsync { get; init; }
        
        public required Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> OnLeaveAsync { get; init; }
        
        public MachineState() { }

        [SetsRequiredMembers]
        public MachineState(
            Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> onEnterAsync,
            Func<FiniteStateMachine<TEnum, TContext>, CancellationToken, Task> onLeaveAsync)
        {
            this.OnEnterAsync = onEnterAsync;
            this.OnLeaveAsync = onLeaveAsync;
        }
        
        public async Task EnterState(FiniteStateMachine<TEnum, TContext> fsm, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await this.OnEnterAsync(fsm, cancellationToken);
        }
        
        public async Task LeaveState(FiniteStateMachine<TEnum, TContext> fsm, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await this.OnLeaveAsync(fsm, cancellationToken);
        }
    }
}