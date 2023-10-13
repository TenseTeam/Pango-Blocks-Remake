namespace VUDK.Patterns.StateMachine.Interfaces
{
    public interface ICastContext<T> where T : Context
    {
        public T Context { get; }
    }
}