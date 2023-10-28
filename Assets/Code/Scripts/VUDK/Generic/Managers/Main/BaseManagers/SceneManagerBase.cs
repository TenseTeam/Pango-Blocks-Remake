namespace VUDK.Generic.Managers.Main.BaseManagers
{
    using UnityEngine.SceneManagement;
    using VUDK.Features.Main.SceneManagement;

    public abstract class SceneManagerBase : SceneSwitcher
    {
        public virtual void LoadNextScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public virtual void ReloadScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
