using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feetPosition;

    // This could probably be an enumeration
    private bool isOnGround = false;
    private bool isJumping = false;
    private bool jumpHeld = false;
    private bool isDead = false;

    private float jumpTimer = 0f;

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(feetPosition.position, groundDistance, ground);

        // Handle jump holding
        if (isJumping && jumpHeld)
        {
            if (jumpTimer < jumpTime)
            {
                rigidBody.velocity = Vector2.up * jumpPower;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (!isDead)
        {
            // Poll the animator every frame so state lockouts are unlikely
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isFalling", rigidBody.velocity.y < 0 && !isOnGround);
            animator.SetBool("isGrounded", isOnGround);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Jump initiated
        if (context.started && isOnGround && !isDead)
        {
            
            isJumping = true;
            jumpHeld = true;
            jumpTimer = 0f;
            rigidBody.velocity = Vector2.up * jumpPower;
        }

        // Jump button released
        if (context.canceled && !isDead)
        {
            jumpHeld = false;
            isJumping = false;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        // Implement shooting 
    }

    public void DamageIncoming()
    {
        isDead = true;

        animator.SetBool("gameOver", true);

        GameManager.GameInstance.GameOver();

    }
}
