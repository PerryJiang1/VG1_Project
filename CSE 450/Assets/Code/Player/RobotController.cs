using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class RobotController : MonoBehaviour
    {
        Rigidbody2D rb;

        [SerializeField] int speed;
        [SerializeField] float jumpForce = 8.3f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform groundCheck;
        [SerializeField] float groundCheckRadius = 0.1f;
        [SerializeField] float delta = 0.1f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float riseMultiplier = 2f;

        float speedMultiplier;
        bool buttonPressed;
        bool onGround;

        private SpriteRenderer spriteRenderer;

        private float lastLeftPressorLeaseTime = 0f;
        private float lastRightPressorLeaseTime = 0f;

        private bool isControlEnabled = true;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (isControlEnabled)
            {
                float targetSpeed = speed * speedMultiplier;
                rb.velocity = new Vector2(targetSpeed, rb.velocity.y);

                onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
                }
                else if (rb.velocity.y > 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
                }

                if (speedMultiplier == 1 && Time.time - lastLeftPressorLeaseTime < delta)
                {
                    speedMultiplier = 1;
                    spriteRenderer.flipX = false;
                }
                else if (speedMultiplier == -1 && Time.time - lastRightPressorLeaseTime < delta)
                {
                    speedMultiplier = -1;
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        public void MoveRight(InputAction.CallbackContext value)
        {
            if (!isControlEnabled) return;

            if (value.started)
            {
                buttonPressed = true;
                lastRightPressorLeaseTime = Time.time;
                speedMultiplier = 1;
                spriteRenderer.flipX = false;
            }
            else if (value.canceled)
            {
                buttonPressed = false;
                lastRightPressorLeaseTime = Time.time;
                if (Time.time - lastLeftPressorLeaseTime > delta)
                {
                    speedMultiplier = 0;
                }
            }
        }

        public void MoveLeft(InputAction.CallbackContext value)
        {
            if (!isControlEnabled) return;

            if (value.started)
            {
                buttonPressed = true;
                lastLeftPressorLeaseTime = Time.time;
                speedMultiplier = -1;
                spriteRenderer.flipX = true;
            }
            else if (value.canceled)
            {
                buttonPressed = false;
                lastLeftPressorLeaseTime = Time.time;
                if (Time.time - lastRightPressorLeaseTime > delta)
                {
                    speedMultiplier = 0;
                }
            }
        }

        public void Jump(InputAction.CallbackContext value)
        {
            if (value.started && onGround)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        // 添加新功能：按下B键增大角色，按下N键缩小角色
        private void Update()
        {
            // 仅在控制启用时检测按键
            if (isControlEnabled)
            {
                if (Keyboard.current.bKey.wasPressedThisFrame)
                {
                    transform.localScale *= 2; // 大小增加两倍
                    jumpForce *= 2; // 跳跃高度增加两倍
                }
                else if (Keyboard.current.nKey.wasPressedThisFrame)
                {
                    transform.localScale *= 0.5f; // 大小缩小两倍
                    jumpForce *= 0.5f; // 跳跃高度减少两倍
                }
            }
        }

        // Disable the player's controls after the level is complete
        public void DisableControl()
        {
            isControlEnabled = false;
        }

        public void EnableControl()
        {
            isControlEnabled = true;
        }
    }
}
