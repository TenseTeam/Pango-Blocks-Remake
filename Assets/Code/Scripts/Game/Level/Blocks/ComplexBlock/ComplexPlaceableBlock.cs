namespace ProjectPBR.Level.Blocks
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Level.Blocks.ComplexBlock;

    [RequireComponent(typeof(CompositeCollider2D))]
    public class ComplexPlaceableBlock : PlaceableBlock
    {
        public new ComplexBlockData Data { get; private set; }
        private new CompositeCollider2D Collider;

        public List<SinglePlaceableBlock> ComposedBlocks { get; private set; } = new List<SinglePlaceableBlock>();

        protected override void Awake()
        {
            base.Awake();
            Collider = base.Collider as CompositeCollider2D;
        }

        public override void Init(BlockData data)
        {
            Data = data as ComplexBlockData;
            GenerateComposition();
        }

        public override void DisableCollider()
        {
            Collider.enabled = false;
            foreach (ComposedBlock block in ComposedBlocks)
                block.DisableCollider();
        }

        public override void EnableCollider()
        {
            Collider.enabled = true;
            foreach (ComposedBlock block in ComposedBlocks)
                block.EnableCollider();
        }

        private void GenerateComposition()
        {
            float width = 0;

            foreach(SingleBlockData single in Data.ComposedBlocks)
            {
                ComposedBlock block = BlocksFactory.Create(single, true) as ComposedBlock;
                ComposedBlocks.Add(block);
                block.transform.position = transform.position + Vector3.right * width++;
                block.transform.parent = transform;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (ComposedBlock block in ComposedBlocks)
                block.Dispose();
        }
    }
}
