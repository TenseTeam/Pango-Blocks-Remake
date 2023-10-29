namespace ProjectPBR.Player.Objective.Goal
{
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Player.Objective.Interfaces;
    using VUDK.Generic.Managers.Main;

    public class ObjectiveGoal : ObjectiveTrigger, IGoal
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