namespace VUDK.Features.Main.SaveSystem
{
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.Interfaces;

    public abstract class SaverControllerBase : MonoBehaviour, ISaver
    {
        public abstract void RefillLoadedData();

        public abstract void SaveAllData();
    }
}
