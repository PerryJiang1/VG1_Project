using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed; 
    Vector3 targetPos;
    private Rigidbody2D platformRb;

    private void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        if (platformRb == null)
        {
            platformRb = gameObject.AddComponent<Rigidbody2D>();
            platformRb.isKinematic = true;
        }

        targetPos = posA.position;
    }

    private void FixedUpdate()
    {
        // Move towards the target position
        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            // If near position A, set target to position B
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            // If near position B, set target to position A
            targetPos = posA.position;
        }

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            Debug.Log("Player Rigidbody2D: " + playerRb);

            if (playerRb != null)
            {
                Debug.Log("Player Rigidbody2D: " + playerRb);
                playerRb.gravityScale = 0f;
                Debug.Log("Player's current gravity scale: " + playerRb.gravityScale);

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.transform.SetParent(null);
            
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            Debug.Log("Exit: Player Rigidbody2D: " + playerRb);
            if (playerRb != null)
            {
                playerRb.gravityScale = 1.4f;
            }
        }
    }
}