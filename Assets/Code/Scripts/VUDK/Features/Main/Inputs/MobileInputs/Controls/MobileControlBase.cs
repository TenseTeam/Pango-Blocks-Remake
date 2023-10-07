namespace VUDK.Features.Main.InputSystem.MobileInputs.Controls
{
    using System;
    using UnityEngine;

    public abstract class MobileControlBase : MonoBehaviour
    {
        public Vector2 InputDirection { get; protected set; }

        public Action<Vector2> OnInputDirection;

        protected abstract void CalculateInputDirection(Vector2 startingInputPosition);
    }
}
