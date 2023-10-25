using ProjectPBR.Config.Constants;
namespace ProjectPBR.Player.Objective.Goal
{
    using VUDK.Generic.Managers.Main;

    public class ObjectiveGoal : ObjectiveTrigger
    {
        public override void Trigger()
        {
            base.Trigger();
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveGoalTouched);
        }
    }
}
