using Project.ConsoleApp.ApplicationModes.Interactive.Triggers;
using Project.ConsoleApp.ApplicationModes.Interactive.Triggers.Hotel;
using Project.FSM;

namespace Project.ConsoleApp.ApplicationModes.Interactive
{
    public static class FiniteInteractive
    {
        public static FiniteStateMachine<ApplicationContext> StateMachine =>
            new FiniteStateMachineBuilder<ApplicationContext>()
                .AddState<DatabasePreparationTrigger>(DatabasePreparationTrigger.FiniteAction)
                .AddState<SeedTestHotel>(SeedTestHotel.FiniteAction)
                .AddState<StartMenuTrigger>(StartMenuTrigger.FiniteAction)
                .AddState<CreateManagerTrigger>(CreateManagerTrigger.FiniteAction)
                .AddState<FireManagerTrigger>(FireManagerTrigger.FiniteAction)
                .AddState<CreateHotelTrigger>(CreateHotelTrigger.FiniteAction)
                .AddState<ManageHotelTrigger>(ManageHotelTrigger.FiniteAction)
                .Build();
    }
}