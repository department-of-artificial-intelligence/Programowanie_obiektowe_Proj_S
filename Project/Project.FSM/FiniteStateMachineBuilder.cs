using Project.FSM.States;
using Project.FSM.Triggers;

namespace Project.FSM
{
    public class FiniteStateMachineBuilder<TContext>
    {
        private readonly List<IFiniteState<TContext>> _states 
            = new List<IFiniteState<TContext>>();
        
        public FiniteStateMachineBuilder<TContext> AddState<TTrigger>(
            FiniteStateMachine<TContext>.StateAction<TTrigger> onEnterAction)
            where TTrigger : IFiniteTrigger
        {
            var state = new FiniteState<TContext, TTrigger>((context, trigger) =>
                onEnterAction(context, (TTrigger) trigger));
            
            this._states.Add(state);
            
            return this;
        }
        
        public FiniteStateMachine<TContext> Build()
        {
            return new FiniteStateMachine<TContext>(this._states);
        }
    }
}