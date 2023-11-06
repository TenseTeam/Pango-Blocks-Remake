﻿namespace ProjectPBR.GameConfig.Constants
{
    public static class GameConstants
    {
        public static class Events
        {
            // Blocks
            public const string OnBlockStartReset = "OnBlockReset";
            public const string OnBlockStartDrag = "OnBlockStartDrag";
            public const string OnBlockStopDrag = "OnBlockStopDrag";
            public const string OnBlockPlaced = "OnBlockPlaced";

            // Objective
            public const string OnObjectiveTriggered = "OnObjectiveTriggered";
            public const string OnObjectiveGoalTouched = "OnObjectiveGoalTouched";
            public const string OnObjectiveGoalSendPosition = "OnObjectiveGoalSendPosition";

            // Game Phases
            public const string OnBeginPlacementPhase = "OnBeginPlacementPhase";
            public const string OnBeginObjectivePhase = "OnBeginObjectivePhase";
            public const string OnBeginGameWonPhase = "OnGamewon";
            public const string OnBeginGameoverPhase = "OnGameover";

            // Level
            public const string OnResetLevel = "OnResetLevel";
            public const string OnSavedCompletedLevel = "OnSavedCompltedLevel";

            // Scenes
            public const string OnGoBackToMenu = "OnGoBackToMenu";

            // Character
            public const string OnCharacterStartWalking = "OnCharacterStartWalking";
            public const string OnCharacterChangedTile = "OnCharacterMoved";
            public const string OnCharacterReachedDestination = "OnCharacterReachedDestination";
            public const string OnCharacterSendPosition = "OnCharacterSendPosition";

            // Profile
            public const string OnProfileAlteration = "OnProfileAlteration";
            public const string OnSelectedProfile = "OnSelectedProfile";
            public const string OnModifiedProfile = "OnModifiedProfile";
            public const string OnCreatedProfile = "OnCreatedProfile";
            public const string OnDeletedProfile = "OnDeletedProfile";
        }

        public static class UIEvents
        {
            public const string OnStartGameoverLoadingScreen = "OnStartGameoverLoadingScreen";
            public const string OnGameoverLoadingScreenCovered = "OnGameoverLoadingScreenCovered";
            public const string OnSelectedDifficultyButton = "OnSelectedDifficultyButton";
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
            public const string GameoverCollide = "GameoverCollide";
        }

        public static class GridAnimations
        {
            public const string FadeOut = "FadeOut";
        }

        public static class ObjectiveAnimations
        {
            public const string ObjectiveTouched = "Touched";
            public const string ObjectiveWon = "Won";
        }

        public static class UIAnimations
        {
            public const int Horizontal = 0;
            public const int Vertical = 1;
            public const int Pyramid = 2;
            public const int MaxAnimations = 3;
            public const string OpenScreen = "Open";
            public const string CloseScreen = "Close";
            public const string ResetScreen = "Reset";
            public const string ScreenState = "Screen";
        }

        public static class ProfileSaving
        {
            public const int MaxProfileNameLength = 10;
            public const int MaxProfilesCount = 5;
            public const string ProfileExtension = ".prof";
        }
    }
}