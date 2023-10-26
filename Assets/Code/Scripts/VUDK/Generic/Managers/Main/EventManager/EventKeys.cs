namespace VUDK.Generic.Managers.Main
{
    public static class EventKeys
    {
        public static class CountdownEvents
        {
            public const string OnCountdownTimesUp = "OnCountdownTimesUp";
            public const string OnCountdownCount = "OnCountDownCount";
        }

        public static class InteractEvents
        {
            public const string OnPickup = "OnPickup";
        }

        public static class ScoreEvents
        {
            public const string OnScoreChange = "OnScoreChange";
            public const string OnHighScoreChange = "OnHighScoreChange";
        }

        public static class DialogueEvents
        {
            public const string OnDialougeTypedLetter = "OnDialougeTypedLetter";
            public const string OnTriggeredDialouge = "OnTriggeredDialouge";
            public const string OnStartDialogue = "OnStartDialogue";
            public const string OnEndDialogue = "OnEndDialogue";
        }

        public static class PauseEvents
        {
            public const string OnPauseEnter = "OnPauseEnter";
            public const string OnPauseExit = "OnPuaseExit";
        }

        public static class GameEvents
        {
            public const string OnGameMachineStart = "OnGameMachineStart";
        }

        public static class SceneEvents
        {
            public const string OnMainMenuLoaded = "OnMainMenuLoaded";
            public const string OnBeforeChangeScene = "OnBeforeChangeScene";
        }

        public static class EntityEvents
        {
            public const string OnEntityInit = "OnEntityInit";
            public const string OnEntityTakeDamage = "OnEntityTakeDamage";
            public const string OnEntityHeal = "OnEntityHeal";
            public const string OnEntityDeath = "OnEntityDeath";
        }

        public static class UIEvents 
        {
            public const string OnButtonClick = "OnButtonClick";
        }
    }
}