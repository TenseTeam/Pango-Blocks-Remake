using ProjectPBR.Config.Constants;
namespace ProjectPBR.Player.Objective.Goal
{
    using VUDK.Generic.Managers.Main;

    public class ObjectiveGoal : ObjectiveTrigger
    {
        private void Start()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveGoalSendPosition, transform.position);
        }

        public override void Trigger()
        {
            base.Trigger();
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveGoalTouched);
        }
    }
}
