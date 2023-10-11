namespace VUDK.Generic.Managers.Main.Interfaces
{
    public interface ICastEventManager<T> where T : EventManager
    {
        public T EventManager { get; }
    }
}
