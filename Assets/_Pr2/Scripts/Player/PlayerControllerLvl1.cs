using UnityEngine;

namespace _Pr2.Scripts.Player
{
    public sealed class PlayerControllerLvl1 : PlayerController
    {
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private float jumpBufferTime = 0.1f;

        private float jumpBufferCounter;

        protected override void HandlePrimaryActionInput()
        {
            if (IsJumpTriggered())
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else if (jumpBufferCounter > 0f)
            {
                jumpBufferCounter -= Time.deltaTime;
            }
        }

        protected override void HandlePrimaryActionPhysics()
        {
            if (jumpBufferCounter > 0f && IsGrounded)
            {
                Body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpBufferCounter = 0f;
            }
        }

        protected override void ClearPrimaryAction()
        {
            jumpBufferCounter = 0f;
        }

        private bool IsJumpTriggered()
        {
            return UpAction && UpAction.action.triggered || JumpAction && JumpAction.action.triggered;
        }
    }
}
