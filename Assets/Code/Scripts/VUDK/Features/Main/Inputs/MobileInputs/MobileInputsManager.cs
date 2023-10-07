namespace VUDK.Features.Main.InputSysten.MobileInputs
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem.MobileInputs.Controls;

    public class MobileInputsManager : MonoBehaviour
    {
        // Adds more controls here if necessary, the default controls must be the controls most used tho
        [field: SerializeField, Header("Default Controls")]
        public MobileControlBase MobileDefaultControls { get; private set; }
    }
}
