namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    
    [RequireComponent(typeof(Collider2D))]
    public class InvalidBlockTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out SinglePlaceableBlock block))
            {
                block.SetIsInvalid(true);
            }
        }
    }
}
