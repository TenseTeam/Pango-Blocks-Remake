namespace VUDK.Features.Main.InputSystem
{
    using UnityEngine;

    public class InputsManager
    {
        public static InputsMap Inputs;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void LoadInputs()
        {
            Inputs = new InputsMap();
            Inputs.Enable();
        }
    }
}
