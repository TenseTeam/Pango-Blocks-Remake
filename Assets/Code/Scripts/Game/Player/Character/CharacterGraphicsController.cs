namespace ProjectPBR.Player.Character
{
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.PathSystem;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterGraphicsController : MonoBehaviour
    {
        private Animator _anim;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _sprite);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.AddListener<Path>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, SpawnStarsVFX);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.RemoveListener<Path>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, SpawnStarsVFX);
        }

        private void Start()
        {
            _anim.SetInteger(Constants.CharacterAnimations.State, Constants.CharacterAnimations.Idle);
        }

        /// <summary>
        /// Spawns a cloud VFX at the character's position.
        /// </summary>
        public void SpawnCloudVFX() // Linked to the animation
        {
            MainManager.Ins.PoolsManager.Pools[PoolKeys.CloudVFX].Get().transform.position = transform.position;
        }

        public void IncreaseRender()
        {
            _sprite.sortingOrder++;
        }

        public void SetStartAnimation()
        {
            SetWalkAnimation();
        }

        public void ResetGraphics()
        {
            SetIdleAnimation();
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
            _anim.SetInteger(Constants.CharacterAnimations.State, Constants.CharacterAnimations.Idle);
        }

        private void SetWalkAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _anim.SetInteger(Constants.CharacterAnimations.State, Constants.CharacterAnimations.Walk);
        }

        private void SetClimbAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            _anim.SetInteger(Constants.CharacterAnimations.State, Constants.CharacterAnimations.Climb);
        }

        private void SetSlideAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            _anim.SetInteger(Constants.CharacterAnimations.State, Constants.CharacterAnimations.Slide);
        }

        private void SetEndGameAnimation(Path pathData)
        {
            if (pathData.HasReached)
            {
                _anim.SetTrigger(Constants.CharacterAnimations.GamewonAnimation);
                return;
            }

            if (pathData.IsGoingToCollide)
            {
                _anim.SetTrigger(Constants.CharacterAnimations.GameoverCollide);
                return;
            }

            if(pathData.IsGoingToFall)
            {
                _anim.SetTrigger(Constants.CharacterAnimations.GameoverFall);
                return;
            }
        }
    }
}