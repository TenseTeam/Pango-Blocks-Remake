namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.PathSystem.Data;
    using VUDK.Patterns.Pooling;
    using System;

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
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, SetRender);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, SetWalkAnimation); // Because it never starts on a climbable or slideable block
            MainManager.Ins.EventManager.AddListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.AddListener<PathData>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, SpawnStarsVFX);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, SetRender);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, SetWalkAnimation);
            MainManager.Ins.EventManager.RemoveListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.RemoveListener<PathData>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
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

        private void SetRender()
        {
            _sprite.sortingOrder++;
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

        private void SetEndGameAnimation(PathData pathData)
        {
            _anim.SetBool(Constants.CharacterAnimations.GamewonAnimation, pathData.HasReached);
            _anim.SetBool(Constants.CharacterAnimations.GameoverHit, pathData.IsGoingToCollide);
            _anim.SetBool(Constants.CharacterAnimations.GameoverFall, pathData.IsGoingToFall);
        }
    }
}
