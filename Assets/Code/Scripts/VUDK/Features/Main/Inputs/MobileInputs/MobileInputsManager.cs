namespace VUDK.Features.Main.InputSysten.MobileInputs
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;
    using VUDK.Features.Main.Inputs.MobileInputs.Keys;

    [DefaultExecutionOrder(-500)]
    public sealed class MobileInputsManager : MonoBehaviour
    {
        [field: SerializeField, Header("Mobile Inputs")]
        public SerializableDictionary<MobileInputActionKeys, InputTouchBase> MobileInputsActions { get; private set; }
    }
}
