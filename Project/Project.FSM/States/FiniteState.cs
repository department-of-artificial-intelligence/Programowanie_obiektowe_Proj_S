using Project.FSM.Triggers;

namespace Project.FSM.States
{
    public class FiniteState<TContext, TTrigger> : IFiniteState<TContext>
        where TTrigger : IFiniteTrigger
    {
        public FiniteStateMachine<TContext>.StateAction<IFiniteTrigger> OnEnterAction { get; init; } = (_, _)
            => Task.FromResult((IFiniteTrigger) new FiniteExitTrigger());
        
        public FiniteState() { }

        public FiniteState(FiniteStateMachine<TContext>.StateAction<IFiniteTrigger> onEnterAction)
        {
            this.OnEnterAction = onEnterAction;
        }
        
        public bool CanBeTriggeredBy<TAnotherTrigger>()
            where TAnotherTrigger : IFiniteTrigger
        {
            return this.CanBeTriggeredBy(typeof(TAnotherTrigger));
        }
        
        public bool CanBeTriggeredBy(Type anotherTriggerType)
        {
            return typeof(TTrigger).IsAssignableFrom(anotherTriggerType);
        }
    }
}