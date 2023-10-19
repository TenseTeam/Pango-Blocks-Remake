namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using ProjectPBR.Config.Constants;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.CharacterController;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;

    public class CharacterRunner : CharacterController2D, ICastGameManager<GameManager>
    {
        private bool _isWalking;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void Start()
        {
            StopWalk();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartWalk);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, StopWalk);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, StopWalk);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartWalk);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, StopWalk);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, StopWalk);
        }

        protected override void Update()
        {
            base.Update();

            if(_isWalking)
                MoveCharacter(Vector2.right);
        }

        private void StartWalk()
        {
            _isWalking = true;
            Rigidbody.isKinematic = false;
        }

        private void StopWalk()
        {
            Rigidbody.isKinematic = true;
            
            _isWalking = false;
            StopCharacterOnPosition();
        }
    }
}
