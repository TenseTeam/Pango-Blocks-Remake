namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class ObjectiveFailTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CharacterRunner character))
            {
                Debug.Log("Objective failed!");
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCantReachObjective);
            }
        }
    }
}
