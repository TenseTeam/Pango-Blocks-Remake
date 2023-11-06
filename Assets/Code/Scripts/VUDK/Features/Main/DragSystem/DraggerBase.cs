namespace VUDK.Features.Main.DragSystem
{
    using UnityEngine;
    using UnityEngine.PlayerLoop;

    public abstract class DraggerBase : MonoBehaviour
    {
        [SerializeField, Min(0f), Header("Drag Settings")]
        private float _dragSpeed = 10f;

        private Vector3 _dragOffset;

        public IDraggable DraggedObject { get; private set; }
        public bool IsDragging => DraggedObject != null;

        protected virtual void Update() => Drag();

        public virtual void StartDrag(IDraggable draggedObject, Vector3 offset = default)
        {
            DraggedObject = draggedObject;
            _dragOffset = offset;
            draggedObject.OnDragObject();
        }

        public virtual void StopDrag()
        {
            DraggedObject.OnEndDragObject();
            DraggedObject = null;
        }

        protected abstract Vector3 CalculateTargetPosition();

        private void Drag()
        {
            if(!IsDragging) return;

            Vector2 fromPosition = DraggedObject.GetDragTransform().position;
            Vector2 targetPosition = CalculateTargetPosition() - _dragOffset;
            DraggedObject.GetDragTransform().position = Vector2.Lerp(fromPosition, targetPosition, Time.deltaTime * _dragSpeed);
        }
    }
}
