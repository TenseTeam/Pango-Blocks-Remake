namespace ProjectPBR.Player.Objective.Goal
{
    using ProjectPBR.Config.Constants;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

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
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, AnimateWon);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjectiveGoalTouched, AnimateTouch);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, AnimateWon);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjectiveGoalTouched, AnimateTouch);
        }

        private void AnimateWon()
        {
            _anim.SetTrigger(Constants.ObjectiveAnimations.ObjectiveWon);
        }

        private void AnimateTouch()
        {
            _anim.SetTrigger(Constants.ObjectiveAnimations.ObjectiveTouched);
        }
    }
}
