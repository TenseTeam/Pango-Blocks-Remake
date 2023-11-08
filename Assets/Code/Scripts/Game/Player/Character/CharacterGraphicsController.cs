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

        [CalledByAnimationEvent]
        public void Footstep()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnCharacterFootstep);
            SpawnCloudVFX();
        }

        public void SetStartAnimation()
        {
            SetWalkAnimation();
        }

        public void IncreaseRender()
        {
            _sprite.sortingOrder++;
        }

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

        private void SpawnStarsVFX()
        {
            MainManager.Ins.PoolsManager.Pools[PoolKeys.StarsVFX].Get().transform.position = transform.position;
        }

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

        private void SetIdleAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Idle);
        }

        private void SetWalkAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Walk);
        }

        private void SetClimbAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Climb);
        }

        private void SetSlideAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            _anim.SetInteger(GameConstants.CharacterAnimations.State, GameConstants.CharacterAnimations.Slide);
        }

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