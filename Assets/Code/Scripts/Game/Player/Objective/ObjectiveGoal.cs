namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class ObjectiveGoal : MonoBehaviour
    {
        private Collider2D _collider;

        private void Awake()
        {
            TryGetComponent(out _collider);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjeciveTouched, EnableTrigger);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjeciveTouched, EnableTrigger);
        }

        private void EnableTrigger()
        {
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CharacterRunner character))
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveAchieved);
        }
    }
}
