using _Pr2.Scripts.Core;
using _Pr2.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Pr2.Scripts.Player
{
    public abstract class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference upAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference healAction;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float healTickInterval = 1f;
        [SerializeField] private LayerMask groundLayers = ~0;
        [SerializeField] private float groundCheckDistance = 0.05f;

        private float healTickTimer;

        protected Rigidbody2D Body => rb;
        protected InputActionReference UpAction => upAction;
        protected InputActionReference JumpAction => jumpAction;
        protected InputActionReference HealAction => healAction;
        protected bool IsGrounded { get; private set; }

        protected virtual void Awake()
        {
            if (!rb)
            {
                rb = GetComponent<Rigidbody2D>();
            }

            if (!playerCollider)
            {
                playerCollider = GetComponent<Collider2D>();
            }
        }

        private void OnEnable()
        {
            moveAction?.action?.Enable();
            upAction?.action?.Enable();
            jumpAction?.action?.Enable();
            healAction?.action?.Enable();
        }

        private void OnDisable()
        {
            moveAction?.action?.Disable();
            upAction?.action?.Disable();
            jumpAction?.action?.Disable();
            healAction?.action?.Disable();
        }

        private void Update()
        {
            bool isHealing = IsHealPressed();

            if (isHealing)
            {
                ClearPrimaryAction();

                if (healTickTimer <= 0f)
                {
                    healTickTimer = healTickInterval;
                }
                else
                {
                    healTickTimer -= Time.deltaTime;

                    if (healTickTimer <= 0f && GameManager.Instance)
                    {
                        GameManager.Instance.TryHealTick();
                        healTickTimer = healTickInterval;
                    }
                }

                return;
            }

            healTickTimer = 0f;
            HandlePrimaryActionInput();
        }

        private void FixedUpdate()
        {
            IsGrounded = CheckGrounded(GetGroundCheckDirection());

            float horizontalInput = moveAction ? moveAction.action.ReadValue<float>() : 0f;
            Body.velocity = new Vector2(horizontalInput * moveSpeed, Body.velocity.y);

            HandlePrimaryActionPhysics();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var spike = other.GetComponent<Spike>();
            if (spike != null)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ApplyDamage(spike.Damage);
                }

                Destroy(spike.gameObject);
                return;
            }

            var coin = other.GetComponent<Coin>();
            if (coin == null)
            {
                return;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(coin.ScoreValue);
            }

            Destroy(coin.gameObject);
        }

        protected virtual bool IsHealPressed()
        {
            return HealAction && HealAction.action.IsPressed();
        }

        protected virtual float GetGroundCheckDirection()
        {
            return 1f;
        }

        protected abstract void HandlePrimaryActionInput();
        protected abstract void HandlePrimaryActionPhysics();
        protected abstract void ClearPrimaryAction();

        private bool CheckGrounded(float groundCheckDirection)
        {
            var bounds = playerCollider.bounds;
            float edgeY = groundCheckDirection > 0f ? bounds.min.y : bounds.max.y;
            var origin = new Vector2(bounds.center.x, edgeY);
            var size = new Vector2(bounds.size.x * 0.95f, groundCheckDistance);
            var offset = Vector2.down * (groundCheckDistance * 0.5f * groundCheckDirection);
            var hit = Physics2D.OverlapBox(origin + offset, size, 0f, groundLayers);
            return hit && hit != playerCollider;
        }
    }
}
