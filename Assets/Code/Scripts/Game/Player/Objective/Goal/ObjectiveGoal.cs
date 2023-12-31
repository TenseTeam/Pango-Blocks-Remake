﻿namespace ProjectPBR.Player.Objective.Goal
{
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Player.Objective.Interfaces;
    using VUDK.Generic.Managers.Main;

    public class ObjectiveGoal : ObjectiveTrigger, IGoal
    {
        private void Start()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnObjectiveGoalSendPosition, transform.position);
        }

        /// <summary>
        /// Triggers the objective triggered and objective goal triggered.
        /// </summary>
        public override void Trigger()
        {
            base.Trigger();
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnObjectiveGoalTouched);
        }
    }
}