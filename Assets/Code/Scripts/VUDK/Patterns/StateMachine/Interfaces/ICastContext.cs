namespace VUDK.Patterns.StateMachine.Interfaces
{
    public interface ICastContext<T> where T : StateMachineContext
    {
        public T Context { get; }
    }
}