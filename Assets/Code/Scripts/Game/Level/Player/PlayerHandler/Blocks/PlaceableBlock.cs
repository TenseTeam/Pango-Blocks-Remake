namespace ProjectPBR.Player.PlayerHandler.Blocks
{
    using UnityEngine;
    using ProjectPBR.Level.Player.PlayerHandler.Blocks;
    using ProjectPBR.ScriptableObjects;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using VUDK.Generic.Managers.Main;

    public class PlaceableBlock : BlockBase, ICastGameManager<PBRGameManager>
    {
        //private PlayerHand _playerHand;
        //private int _overlappingCount;
     
        public Vector2 OriginalPosition { get; private set; }
        public PBRGameManager GameManager => MainManager.Ins.GameManager as PBRGameManager;

        //public void Init(BlockData data, PlayerHand hand)
        //{
        //    base.Init(data);
        //    _playerHand = hand;
        //}

        public void OnMouseDown()
        {
            GameManager.GridBlocksManager.StartDrag(this);
        }

        public void SetOriginalPosition(Vector2 originalPosition)
        {
            OriginalPosition = originalPosition;
        }

        //[ContextMenu("IsMultipleOverlapping")]
        //public bool IsMultipleOverlapping()
        //{
        //    Collider2D[] overlappedColliders = new Collider2D[10];
        //    ContactFilter2D contactFilter = new ContactFilter2D();
        //    int numOverlaps = Collider.OverlapCollider(contactFilter, overlappedColliders);
        //    int blockCount = 0;

        //    for (int i = 0; i < numOverlaps; i++)
        //    {
        //        if (overlappedColliders[i].GetComponent<BlockBase>() != null)
        //        {
        //            blockCount++;
        //        }
        //    }

        //    Debug.Log("Block Count: " + blockCount + " bool: " + (blockCount >= 1));
        //    return blockCount >= 1;
        //}
    }
}
