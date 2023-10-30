namespace VUDK.SaveSystem
{
    using UnityEngine;
    using VUDK.SaveSystem.Interfaces;

    public abstract class SaverControllerBase : MonoBehaviour, ISaver
    {
        public abstract void RefillLoadedData();

        public abstract void SaveAllData();
    }
}
