namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.InputSysten.MobileInputs;
    using VUDK.Generic.Managers.Main;

    public abstract class InputTouchBase : MonoBehaviour
    {
        //[SerializeField, Header("Input Location")]
        //private ScreenInputLocation _screenInputLocation;
        //[SerializeField, Range(0f, 1f)]
        //private float _maxDistanceFromLocation;

        //protected MobileInputsManager MobileInputs => (MainManager.Ins.GameManager).MobileInputsManager;
        protected Vector2 ReferenceResolution => MainManager.Ins.GameConfig.ReferenceResolution;

        public Action OnInputPerformed;
        public Action OnInputCancelled;
        public Action OnInputInvalid;

        //protected bool IsFingerInScreenLocation(Vector2 fingerPosition)
        //{
        //    if(_screenInputLocation == ScreenInputLocation.Any)
        //        return true;

        //    Vector2 referencePosition = _screenInputLocation.ScreenPosition();

        //    Vector2 normalizedFingerPosition = new Vector2(fingerPosition.x / Screen.width, fingerPosition.y / Screen.height);
        //    Vector2 normalizedReferencePosition = new Vector2(referencePosition.x / ReferenceResolution.x, referencePosition.y / ReferenceResolution.y);

        //    float distance = Vector2.Distance(normalizedFingerPosition, normalizedReferencePosition);
        //    return distance <= _maxDistanceFromLocation;
        //}
    }
}
