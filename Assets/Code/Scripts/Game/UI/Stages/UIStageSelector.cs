namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using ProjectPBR.Managers.Static;

    public class UIStageSelector : MonoBehaviour
    {
        [SerializeField, Min(0), Header("Stage Index")]
        private int _stageIndex;

        private void OnEnable()
        {
            LevelMapper.TrySetCurrentStageIndex(_stageIndex);
        }
    }
}
