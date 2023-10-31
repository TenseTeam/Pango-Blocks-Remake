namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Generic.Managers.Main.BaseManagers;

    public interface ICastSceneManager<T> where T : SceneManagerBase
    {
        public T SceneManager { get; }
    }
}
