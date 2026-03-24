using UnityEngine;

namespace _Pr2.Scripts.Player
{
    public sealed class PlayerControllerLvl2 : PlayerController
    {
        [SerializeField] private float gravityScale = 1f;

        private bool queuedFlip;
        private bool canFlip = true;
        private bool isGravityUpsideDown;

        protected override void Awake()
        {
            base.Awake();
            Body.gravityScale = Mathf.Abs(gravityScale);
        }

        protected override void HandlePrimaryActionInput()
        {
            if (ShouldTriggerFlip() && canFlip)
            {
                queuedFlip = true;
            }
        }

        protected override void HandlePrimaryActionPhysics()
        {
            if (IsGrounded)
            {
                canFlip = true;
            }

            if (!queuedFlip || !canFlip)
            {
                return;
            }

            isGravityUpsideDown = !isGravityUpsideDown;
            Body.gravityScale = isGravityUpsideDown ? -Mathf.Abs(gravityScale) : Mathf.Abs(gravityScale);
            Body.velocity = new Vector2(Body.velocity.x, 0f);
            canFlip = false;
            queuedFlip = false;
        }

        protected override void ClearPrimaryAction()
        {
            queuedFlip = false;
        }

        protected override bool IsHealPressed()
        {
            if (!isGravityUpsideDown)
            {
                return base.IsHealPressed();
            }

            return UpAction && UpAction.action.IsPressed();
        }

        protected override float GetGroundCheckDirection()
        {
            return isGravityUpsideDown ? -1f : 1f;
        }

        private bool ShouldTriggerFlip()
        {
            if (JumpAction && JumpAction.action.triggered)
            {
                return true;
            }

            if (!isGravityUpsideDown)
            {
                return UpAction && UpAction.action.triggered;
            }

            return HealAction && HealAction.action.triggered;
        }
    }
}
