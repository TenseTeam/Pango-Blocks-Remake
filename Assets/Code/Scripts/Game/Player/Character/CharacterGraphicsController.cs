namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.PathSystem.Data;

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
            MainManager.Ins.EventManager.AddListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.AddListener<PathData>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, SetRender);
            MainManager.Ins.EventManager.RemoveListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
            MainManager.Ins.EventManager.RemoveListener<PathData>(Constants.Events.OnCharacterReachedDestination, SetEndGameAnimation);
        }

        private void Start()
        {
            _anim.SetInteger(Constants.Animations.State, Constants.Animations.Idle);
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
            _anim.SetInteger(Constants.Animations.State, Constants.Animations.Walk);
        }

        private void SetClimbAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            _anim.SetInteger(Constants.Animations.State, Constants.Animations.Climb);
        }

        private void SetSlideAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            _anim.SetInteger(Constants.Animations.State, Constants.Animations.Slide);
        }

        private void SetEndGameAnimation(PathData pathData)
        {
            _anim.SetBool(Constants.Animations.GamewonAnimation, pathData.HasReached);
            _anim.SetBool(Constants.Animations.GameoverHit, pathData.IsGoingToCollide);
            _anim.SetBool(Constants.Animations.GameoverFall, pathData.IsGoingToFall);
        }
    }
}
