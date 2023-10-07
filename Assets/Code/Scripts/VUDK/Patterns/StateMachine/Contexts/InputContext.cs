namespace VUDK.Patterns.StateMachine
{
    public class InputContext : Context
    {
        public InputsMap Inputs { get; protected set; }

        public InputContext(InputsMap inputs) : base()
        {
            Inputs = inputs;
        }
    }
}