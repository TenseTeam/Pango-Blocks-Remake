namespace VUDK.Patterns.Factory.Interfaces
{
    public interface IAbstractFactory<T>
    {
        public T Create();
    }
}