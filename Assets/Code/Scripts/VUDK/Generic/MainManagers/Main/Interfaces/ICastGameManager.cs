namespace VUDK.Generic.Managers.Main.Interfaces
{
    public interface ICastGameManager<T> where T : GameManager
    {
        public T GameManager { get; }
    }
}
