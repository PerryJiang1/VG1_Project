using UnityEngine;

public class MirrorCube : MonoBehaviour
{
    public float rotationSpeed = 30f;  
    public GameObject player;
    public float maxDistance = 5f;
    public float mirrorMoveSpeed = 2f;  

    private Rigidbody2D playerRb;
    private bool isPlayerColliding = false;  
    private Vector2 moveDirection;  

    void Start()
    {
        
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (isPlayerColliding)
        {
            MoveMirror(moveDirection);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject == player)
        {
            
            Vector2 playerPosition = player.transform.position;
            Vector2 mirrorPosition = transform.position;
            
            
            if (playerPosition.y > mirrorPosition.y + 0.5f)  
            {
                return;
            }

            Vector2 playerVelocity = playerRb.velocity;  

            
            if (playerVelocity.x > 0)  
            {
                moveDirection = Vector2.right;  
            }
            else if (playerVelocity.x < 0)  
            {
                moveDirection = Vector2.left;  
            }

            isPlayerColliding = true;  
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject == player)
        {
            isPlayerColliding = false;  
        }
    }

    
    private void MoveMirror(Vector2 direction)
    {
        
        transform.Translate(direction * mirrorMoveSpeed * Time.deltaTime, Space.World);
    }

    
    private void RotateMirror(float speed)
    {
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
