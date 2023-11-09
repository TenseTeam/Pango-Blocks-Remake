namespace ProjectPBR.Audio
{
    using UnityEngine;
    using VUDK.Features.Main.AudioSFX;
    using VUDK.Generic.Managers.Main;
    using VUDK.Config;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Level.Blocks;

    public class GameAudioController : AudioEventsControllerBase
    {
        [SerializeField, Header("UI")]
        private AudioClip _defaultButton;
        [SerializeField]
        private AudioClip _playButton;
        [SerializeField]
        private AudioClip _infoButton;
        [SerializeField]
        private AudioClip _levelButton;
        [SerializeField]
        private AudioClip _transitionMenu;
        [SerializeField]
        private AudioClip _closeButton;

        [SerializeField, Header("Game")]
        private AudioClip _transitionLevel;
        [SerializeField]
        private AudioClip _levelClear;

        [SerializeField, Header("Character")]
        private AudioClip _characterFootstep;
        [SerializeField]
        private AudioClip _characterSlide;

        [SerializeField, Header("Block")]
        private AudioClip _touchBlock;
        [SerializeField]
        private AudioClip _cantPlaceBlock;

        private bool _hasAlreadyPlayedSlide;

        protected override void RegisterAudioEvents()
        {
            // Register audio events here

            // UI
            EventManager.AddListener(GameConstants.UIEvents.OnPlayButtonPressed, PlayPlayClip);
            EventManager.AddListener(GameConstants.UIEvents.OnInfoButtonPressed, PlayInfoClip);
            EventManager.AddListener(GameConstants.UIEvents.OnLevelButtonPressed, PlayLevelAndTransitionMenuClip);
            EventManager.AddListener(GameConstants.UIEvents.OnCloseButtonPressed, PlayCloseClip);
            EventManager.AddListener(EventKeys.UIEvents.OnButtonPressed, PlayDefaultButtonClip);

            // Level
            EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, PlayLevelClearClip);
            EventManager.AddListener(GameConstants.UIEvents.OnLoadingScreenClose, PlayTransitionLevelClip);

            // Character
            EventManager.AddListener(GameConstants.Events.OnCharacterFootstep, PlayCharacterFootstepClip);
            EventManager.AddListener<BlockType>(GameConstants.Events.OnCharacterChangedTile, PlayCharacterTileClip);

            // Block
            EventManager.AddListener(GameConstants.Events.OnBlockPlaced, PlayTouchBlockClip);
            EventManager.AddListener(GameConstants.Events.OnBlockStartDrag, PlayTouchBlockClip);
            EventManager.AddListener(GameConstants.Events.OnCantPlaceBlock, PlayCantPlaceBlockClip);
        }

        protected override void UnregisterAudioEvents()
        {
            // Unregister audio events here

            // UI
            EventManager.RemoveListener(GameConstants.UIEvents.OnPlayButtonPressed, PlayPlayClip);
            EventManager.RemoveListener(GameConstants.UIEvents.OnInfoButtonPressed, PlayInfoClip);
            EventManager.RemoveListener(GameConstants.UIEvents.OnLevelButtonPressed, PlayLevelAndTransitionMenuClip);
            EventManager.RemoveListener(GameConstants.UIEvents.OnCloseButtonPressed, PlayCloseClip);
            EventManager.RemoveListener(EventKeys.UIEvents.OnButtonPressed, PlayDefaultButtonClip);

            // Level
            EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, PlayLevelClearClip);
            EventManager.RemoveListener(GameConstants.UIEvents.OnLoadingScreenClose, PlayTransitionLevelClip);

            // Character
            EventManager.RemoveListener(GameConstants.Events.OnCharacterFootstep, PlayCharacterFootstepClip);
            EventManager.RemoveListener<BlockType>(GameConstants.Events.OnCharacterChangedTile, PlayCharacterTileClip);

            // Block
            EventManager.RemoveListener(GameConstants.Events.OnBlockPlaced, PlayTouchBlockClip);
            EventManager.RemoveListener(GameConstants.Events.OnBlockStartDrag, PlayTouchBlockClip);
            EventManager.RemoveListener(GameConstants.Events.OnCantPlaceBlock, PlayCantPlaceBlockClip);
        }

        /// <summary>
        /// Plays the default button clip.
        /// </summary>
        private void PlayDefaultButtonClip()
        {
            AudioManager.PlayStereoAudio(_defaultButton);
        }

        /// <summary>
        /// Plays the play button clip.
        /// </summary>
        private void PlayPlayClip()
        {
            AudioManager.PlayStereoAudio(_playButton);
        }

        /// <summary>
        /// Plays the info button clip.
        /// </summary>
        private void PlayInfoClip()
        {
            AudioManager.PlayStereoAudio(_infoButton);
        }

        /// <summary>
        /// Plays the level transition clip and level button clip.
        /// </summary>
        private void PlayLevelAndTransitionMenuClip()
        {
            AudioManager.PlayStereoAudio(_levelButton, false);
            AudioManager.PlayStereoAudio(_transitionMenu, true);
        }

        /// <summary>
        /// Plays the close button clip.
        /// </summary>
        private void PlayCloseClip()
        {
            AudioManager.PlayStereoAudio(_closeButton);
        }

        /// <summary>
        /// Plays the transition level clip.
        /// </summary>
        private void PlayTransitionLevelClip()
        {
            AudioManager.PlayStereoAudio(_transitionLevel);
        }

        /// <summary>
        /// Plays the level clear clip.
        /// </summary>
        private void PlayLevelClearClip()
        {
            AudioManager.PlayStereoAudio(_levelClear);
        }

        /// <summary>
        /// Plays the character footstep clip.
        /// </summary>
        private void PlayCharacterFootstepClip()
        {
            _hasAlreadyPlayedSlide = false;
            AudioManager.PlayStereoAudio(_characterFootstep, true);
        }

        /// <summary>
        /// Plays a character tile clip based on the block type.
        /// </summary>
        /// <param name="blockType"><see cref="BlockType"/></param>
        private void PlayCharacterTileClip(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.Climbable:
                    _hasAlreadyPlayedSlide = false;
                    break;
                case BlockType.Slideable:
                    if (!_hasAlreadyPlayedSlide)
                    {
                        _hasAlreadyPlayedSlide = true;
                        AudioManager.PlayStereoAudio(_characterSlide, true);
                    }
                    break;
            }
        }

        /// <summary>
        /// Plays the touch block clip.
        /// </summary>
        private void PlayTouchBlockClip()
        {
            AudioManager.PlayStereoAudio(_touchBlock, true);
        }

        /// <summary>
        /// Plays the cant place block clip.
        /// </summary>
        private void PlayCantPlaceBlockClip()
        {
            AudioManager.PlayStereoAudio(_cantPlaceBlock, true);
        }
    }
}
