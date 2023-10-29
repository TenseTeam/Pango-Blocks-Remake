namespace ProjectPBR.Managers.UIManager.LoadingScreen.AnimationBehaviours
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    public class LoadingScreenIdleBehaviour : StateMachineBehaviour, ICastUIManager<GameUIManager>
    {
        public GameUIManager UIManager => MainManager.Ins.UIManager as GameUIManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UIManager.LoadingScreenManager.DisableScreen();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UIManager.LoadingScreenManager.EnableScreen();
        }
    }
}