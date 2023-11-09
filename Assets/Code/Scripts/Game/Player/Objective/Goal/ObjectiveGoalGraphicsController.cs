namespace ProjectPBR.Player.Objective.Goal
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(Animator))]
    public class ObjectiveGoalGraphicsController : MonoBehaviour
    {
        private Animator _anim;

        private void Awake()
        {
            TryGetComponent(out _anim);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, AnimateWon);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnObjectiveGoalTouched, AnimateTouch);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, AnimateWon);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnObjectiveGoalTouched, AnimateTouch);
        }

        /// <summary>
        /// Starts the objective won animation.
        /// </summary>
        private void AnimateWon()
        {
            _anim.SetTrigger(GameConstants.ObjectiveAnimations.ObjectiveWon);
        }

        /// <summary>
        /// Starts the objective touched animation.
        /// </summary>
        private void AnimateTouch()
        {
            _anim.SetTrigger(GameConstants.ObjectiveAnimations.ObjectiveTouched);
        }
    }
}
