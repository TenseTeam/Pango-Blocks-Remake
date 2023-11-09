namespace ProjectPBR.Player.Character
{
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.PathSystem;
    using ProjectPBR.Managers.Main.GameStats;
    using UnityEngine;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterGraphicsController : MonoBehaviour, ICastGameStats<GameStats>
    {
        private Animator _anim;
        private SpriteRenderer _sprite;

        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        private void Awake()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _sprite);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<BlockType>(GameConstants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.AddListener<Path>(GameConstants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, SpawnStarsVFX);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<BlockType>(GameConstants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.RemoveListener<Path>(GameConstants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, SpawnStarsVFX);
        }

        private void Start()
        {
            ResetGraphics();
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Idle);
        }

        /// <summary>
        /// Triggers the character footstep.
        /// </summary>
        [CalledByAnimationEvent]
        public void Footstep()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnCharacterFootstep);
            SpawnCloudVFX();
        }

        /// <summary>
        /// Sets the start character animation.
        /// </summary>
        public void SetStartAnimation()
        {
            SetWalkAnimation();
        }

        /// <summary>
        /// Increases the character render priority.
        /// </summary>
        public void IncreaseRender()
        {
            _sprite.sortingOrder++;
        }

        /// <summary>
        /// Decreases the character render priority.
        /// </summary>
        public void ResetGraphics()
        {
            _sprite.sortingOrder = GameStats.CharacterLayer;
            SetIdleAnimation();
        }

        /// <summary>
        /// Spawns a cloud VFX at the character's position.
        /// </summary>
        private void SpawnCloudVFX()
        {
            if (MainManager.Ins.PoolsManager.Pools[PoolKeys.CloudVFX].TryGet(out GameObject vfx))
                vfx.transform.position = transform.position;
        }

        /// <summary>
        /// Spawns a stars VFX at the character's position.
        /// </summary>
        private void SpawnStarsVFX()
        {
            MainManager.Ins.PoolsManager.Pools[PoolKeys.StarsVFX].Get().transform.position = transform.position;
        }

        /// <summary>
        /// Sets the character animation based on the block type.
        /// </summary>
        /// <param name="blockType"><see cref="BlockType"/> to check.</param>
        private void SetAnimation(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.Flat:
                    SetWalkAnimation();
                    break;

                case BlockType.Climbable:
                    SetClimbAnimation();
                    break;

                case BlockType.Slideable:
                    SetSlideAnimation();
                    break;
            }
        }

        /// <summary>
        /// Sets the character animation to idle.
        /// </summary>
        private void SetIdleAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Idle);
        }

        /// <summary>
        /// Sets the character animation to walk.
        /// </summary>
        private void SetWalkAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Walk);
        }

        /// <summary>
        /// Sets the character animation to climb.
        /// </summary>
        private void SetClimbAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Climb);
        }

        /// <summary>
        /// Sets the character animation to slide.
        /// </summary>
        private void SetSlideAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Slide);
        }

        /// <summary>
        /// Sets the end game animation based on the path data.
        /// </summary>
        /// <param name="pathData"><see cref="Path"/> to check.</param>
        private void SetEndGameAnimation(Path pathData)
        {
            if (pathData.HasReached)
            {
                _anim.SetTrigger(GameConstants.CharacterAnimations.GamewonAnimation);
                return;
            }

            if (pathData.IsGoingToCollide)
            {
                _anim.SetTrigger(GameConstants.CharacterAnimations.GameoverCollide);
                return;
            }

            if(pathData.IsGoingToFall)
            {
                _anim.SetTrigger(GameConstants.CharacterAnimations.GameoverFall);
                return;
            }
        }
    }
}