namespace VUDK.Patterns.StateMachine.Interfaces
{
    public interface IContextConverter<T> where T : Context
    {
        public T Context { get; }
    }
}