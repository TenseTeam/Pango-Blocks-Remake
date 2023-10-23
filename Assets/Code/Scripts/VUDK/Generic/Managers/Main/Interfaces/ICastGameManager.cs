namespace VUDK.Generic.Managers.Main.Interfaces
{
    public interface ICastGameManager<T> where T : GameManagerBase
    {
        public T GameManager { get; }
    }
}
