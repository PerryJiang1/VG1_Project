using UnityEngine;

public class MirrorCube : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation (if needed for future rotation control)
    public GameObject player;
    public float maxDistance = 5f;
    public float mirrorMoveSpeed = 2f;  // Speed at which the mirror moves

    private Rigidbody2D playerRb;
    private bool isPlayerColliding = false;  // Tracks if the player is colliding with the mirror
    private Vector2 moveDirection;  // Records the direction of the mirror's movement

    void Start()
    {
        // Get the player's Rigidbody2D component to access velocity
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Only move the mirror if the player is colliding with it
        if (isPlayerColliding)
        {
            MoveMirror(moveDirection);
        }
    }

    // When the player collides, trigger continuous movement
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensure that only the player can trigger this logic
        if (collision.gameObject == player)
        {
            Vector2 playerVelocity = playerRb.velocity;  // Get the player's velocity

            // Determine the movement direction based on the player's velocity
            if (playerVelocity.x > 0)  // Player is moving right
            {
                moveDirection = Vector2.right;  // Move the mirror to the right
            }
            else if (playerVelocity.x < 0)  // Player is moving left
            {
                moveDirection = Vector2.left;  // Move the mirror to the left
            }

            isPlayerColliding = true;  // Mark that the player is colliding with the mirror
        }
    }

    // Stop moving when the player leaves the collision area
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ensure this is the player leaving
        if (collision.gameObject == player)
        {
            isPlayerColliding = false;  // Player left, stop moving the mirror
        }
    }

    // Move the mirror continuously
    private void MoveMirror(Vector2 direction)
    {
        // Move along the horizontal axis (X-axis) using world coordinates
        transform.Translate(direction * mirrorMoveSpeed * Time.deltaTime, Space.World);
    }

    // Method to rotate the mirror if needed
    private void RotateMirror(float speed)
    {
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
