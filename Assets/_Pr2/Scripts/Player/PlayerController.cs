using _Pr2.Scripts.Core;
using _Pr2.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Pr2.Scripts.Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private LayerMask groundLayers = ~0;
        [SerializeField] private float groundCheckDistance = 0.05f;
        [SerializeField] private float jumpBufferTime = 0.1f;

        private float jumpBufferCounter;
        private bool isGrounded;

        private void Awake()
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
            jumpAction?.action?.Enable();
        }

        private void OnDisable()
        {
            moveAction?.action?.Disable();
            jumpAction?.action?.Disable();
        }

        private void Update()
        {
            if (jumpAction && jumpAction.action.triggered)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else if (jumpBufferCounter > 0f)
            {
                jumpBufferCounter -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            isGrounded = CheckGrounded();

            var horizontalInput = moveAction ? moveAction.action.ReadValue<float>() : 0f;
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (jumpBufferCounter > 0f && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpBufferCounter = 0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var spike = other.GetComponent<Spike>();
            if (spike == null)
            {
                return;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ApplyDamage(spike.Damage);
            }

            Destroy(spike.gameObject);
        }

        private bool CheckGrounded()
        {

            var bounds = playerCollider.bounds;
            var origin = new Vector2(bounds.center.x, bounds.min.y);
            var size = new Vector2(bounds.size.x * 0.95f, groundCheckDistance);
            var hit = Physics2D.OverlapBox(origin + Vector2.down * (groundCheckDistance * 0.5f), size, 0f, groundLayers);
            return hit && hit != playerCollider;
        }
    }
}
