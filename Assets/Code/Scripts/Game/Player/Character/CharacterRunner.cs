namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using ProjectPBR.Config.Constants;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.CharacterController;

    public class CharacterRunner : CharacterController2D
    {
        private bool _isWalking;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartWalk);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjectiveAchieved, StopWalk); // TO DO: For now objective achieved, but it should be OnGameWon
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartWalk);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjectiveAchieved, StopWalk);
        }

        protected override void Update()
        {
            base.Update();

            if (_isWalking)
                MoveCharacter(Vector2.right);
        }

        private void StartWalk()
        {
            _isWalking = true;
        }

        private void StopWalk()
        {
            _isWalking = false;
            StopCharacter();
        }
    }
}
