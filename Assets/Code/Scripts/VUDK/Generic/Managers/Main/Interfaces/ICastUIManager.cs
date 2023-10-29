namespace VUDK.Generic.Managers.Main.Interfaces
{
    public interface ICastUIManager<T> where T : UIManagerBase
    {
        public T UIManager { get; }
    }
}

