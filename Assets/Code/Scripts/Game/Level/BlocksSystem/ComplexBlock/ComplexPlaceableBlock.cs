namespace ProjectPBR.Level.Blocks
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Level.Blocks.ComplexBlock;
    using ProjectPBR.Data.ScriptableObjects.Blocks;

    public class ComplexPlaceableBlock : PlaceableBlock
    {
        public new ComplexBlockData Data => base.Data as ComplexBlockData;

        public List<ComposedBlock> ComposedBlocks { get; private set; } = new List<ComposedBlock>();

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Init(BlockData data)
        {
            base.Init(data);
            GenerateComposition();
        }

        public override void DisableCollider()
        {
            foreach (ComposedBlock block in ComposedBlocks)
                block.DisableCollider();
        }

        public override void EnableCollider()
        {
            foreach (ComposedBlock block in ComposedBlocks)
                block.EnableCollider();
        }

        private void GenerateComposition()
        {
            float width = 0;

            foreach(SingleBlockData single in Data.ComposedBlocks)
            {
                ComposedBlock block = GameFactory.Create(single, true) as ComposedBlock;
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

        public override void IncreaseRender()
        {
            foreach (ComposedBlock block in ComposedBlocks)
                block.IncreaseRender();
        }

        public override void DecreaseRender()
        {
            foreach (ComposedBlock block in ComposedBlocks)
                block.DecreaseRender();
        }

        public override void SetIsInvalid(bool isInvalid)
        {
            base.SetIsInvalid(isInvalid);
            foreach (ComposedBlock block in ComposedBlocks)
                block.SetIsInvalid(isInvalid);
        }

        public override bool IsInvalid()
        {
            foreach (ComposedBlock block in ComposedBlocks)
            {
                if (block.IsInvalid())
                    return true;
            }
            return false;
        }
    }
}
