namespace ProjectPBR.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/BlockCollider")]
    public class BlockColliderData : ScriptableObject
    {
        public Vector2[] Points;
    }
}