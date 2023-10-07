namespace VUDK.UI
{
    using UnityEngine;

    public class UISetCursor : MonoBehaviour
    {
        [SerializeField, Header("Cursor Visibility")]
        public bool _isEnableOnAwake;

        private void Awake()
        {
            Cursor.visible = _isEnableOnAwake;
        }
    }
}
