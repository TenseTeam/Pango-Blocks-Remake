namespace ProjectPBR.Config.Constants
{
    public static class Constants
    {
        public static class Events
        {
            public const string OnBlockReset = "OnBlockReset";
            public const string OnObjectiveTouched = "OnObjeciveTouched";

            public const string OnBeginObjectivePhase = "OnBeginObjectivePhase";
            public const string OnBeginGameWonPhase = "OnGamewon";
            public const string OnBeginGameoverPhase = "OnGameover";

            public const string OnCharacterStartWalking = "OnCharacterStartWalking";
            public const string OnCharacterChangedTile = "OnCharacterMoved";
            public const string OnCharacterReachedDestination = "OnCharacterReachedDestination";
        }

        public static class CharacterAnimations
        {
            public const int Idle = 0;
            public const int Walk = 1;
            public const int Slide = 2;
            public const int Climb = 3;
            public const string State = "State";
            public const string GamewonAnimation = "Gamewon";
            public const string GameoverFall = "GameoverFall";
            public const string GameoverHit = "GameoverHit";
        }

        public static class GridAnimations
        {
            public const string FadeOut = "FadeOut";
        }

        public static class UIAnimations
        {
            public const int Horizontal = 0;
            public const int Vertical = 1;
            public const int Pyramid = 2;
            public const int MaxAnimations = 3;
            public const string OpenScreen = "Open";
            public const string CloseScreen = "Close";
            public const string ScreenState = "Screen";
        }
    }
}