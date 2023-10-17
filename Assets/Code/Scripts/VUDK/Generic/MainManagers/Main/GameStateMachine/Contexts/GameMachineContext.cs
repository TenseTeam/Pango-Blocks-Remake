﻿namespace VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts
{
    using VUDK.Patterns.StateMachine;

    public abstract class GameMachineContext : InputContext
    {
        public GameMachineContext(InputsMap inputs) : base(inputs)
        {
        }
    }
}
