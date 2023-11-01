namespace ProjectPBR.UI.Menu
{
    using UnityEngine;
    using TMPro;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(TMP_InputField))]
    public class UIProfileName : MonoBehaviour
    {
        private TMP_InputField _inputField;

        private void Awake()
        {
            TryGetComponent(out _inputField);
            _inputField.characterLimit = GameConstants.ProfileSaving.MaxProfileNameLength;
        }
    }
}
