using Project.FSM.Triggers;

namespace Project.FSM.States
{
    public interface IFiniteState<TContext>
    {
        public FiniteStateMachine<TContext>.StateAction<IFiniteTrigger> OnEnterAction { get; }
        
        public bool CanBeTriggeredBy<TTrigger>()
            where TTrigger : IFiniteTrigger;
        
        public bool CanBeTriggeredBy(Type anotherTriggerType);
    }
}