namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    [RequireComponent(typeof(Collider2D))]
    public class InvalidBlockTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool isFallPhase = MainManager.Ins.GameStateMachine.IsState(GamePhaseKeys.FallPhase);
            if(!isFallPhase) return;

            if(collision.TryGetComponent(out SinglePlaceableBlock block))
                block.SetIsInvalid(true);
        }
    }
}
