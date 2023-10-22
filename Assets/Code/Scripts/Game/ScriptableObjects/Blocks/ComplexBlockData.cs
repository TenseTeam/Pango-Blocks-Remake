namespace ProjectPBR.ScriptableObjects
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/Blocks/Complex Block")]
    public class ComplexBlockData : BlockData
    {
        public List<SingleBlockData> ComposedBlocks;
    }
}