namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using System;
    using ProjectPBR.Level.Blocks;

    public class CharacterGraphicsController : MonoBehaviour
    {
        private Animator _anim;

        private void Awake()
        {
            TryGetComponent(out _anim);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<BlockType>(Constants.Events.OnCharacterChangedTile, SetAnimation);
        }

        private void OnDisable()
        {
            
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

        private void SetWalkAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void SetClimbAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }

        private void SetSlideAnimation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        }
    }
}
