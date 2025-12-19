using Project.FSM.States;
using Project.FSM.Triggers;

namespace Project.FSM
{
    public class FiniteStateMachine<TContext>
    {
        public delegate Task<IFiniteTrigger> StateAction<in TTrigger>(TContext context, TTrigger trigger)
            where TTrigger : IFiniteTrigger;
        
        private readonly List<IFiniteState<TContext>> _states 
            = new List<IFiniteState<TContext>>();
        
        public FiniteStateMachine() { }

        public FiniteStateMachine(List<IFiniteState<TContext>> states)
        {
            this._states = states;
        }
        
        public IFiniteState<TContext> LookupStateByTrigger<TTrigger>()
            where TTrigger : IFiniteTrigger
            => this.LookupStateByTrigger(typeof(TTrigger));
        
        public IFiniteState<TContext> LookupStateByTrigger(Type triggerType)
        {
            foreach (var state in this._states)
            {
                if (state.CanBeTriggeredBy(triggerType))
                {
                    return state;
                }
            }

            throw new KeyNotFoundException($"State for trigger type {triggerType.Name} not found");
        }

        public void AddState<TTrigger>(IFiniteState<TContext> state)
        {
            this._states.Add(state);
        }
        
        public async Task RunAsync(TContext context)
        {
            await this.RunAsync(context, new FiniteEnterTrigger());
        }
        
        public async Task RunAsync(TContext context, IFiniteTrigger startTrigger)
        {
            var trigger = startTrigger;

            while (!trigger.GetType().IsAssignableFrom(typeof(FiniteExitTrigger)))
            {
                trigger = await this
                    .LookupStateByTrigger(trigger.GetType())
                    .OnEnterAction(context, trigger);
            }
        }
    }
}