namespace ProjectPBR.Data.ScriptableObjects.Blocks
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/Blocks/Complex Block")]
    public class ComplexBlockData : BlockDataBase
    {
        public List<SingleBlockData> ComposedBlocks;
    }
}