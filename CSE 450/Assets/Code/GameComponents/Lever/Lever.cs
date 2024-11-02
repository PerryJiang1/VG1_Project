using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public RemoteControlMirror remoteMirror;  
    private bool isFlipped = false;  
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float playerDirection = other.transform.position.x - transform.position.x;

            if (playerDirection > 0) 
            {
                if (isFlipped && remoteMirror != null && !remoteMirror.IsRotating()) // 添加检查旋转状态
                {
                    Unflip();
                }
            }
            else if (playerDirection < 0)
            {
                if (!isFlipped && remoteMirror != null && !remoteMirror.IsRotating()) // 添加检查旋转状态
                {
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        if (!isFlipped)
        {
            isFlipped = true;
            spriteRenderer.flipX = true;
            if (remoteMirror != null)
            {
                remoteMirror.Activate();
            }
        }
    }

    private void Unflip()
    {
        if (isFlipped)
        {
            isFlipped = false;
            spriteRenderer.flipX = false;
            if (remoteMirror != null)
            {
                remoteMirror.Deactivate();
            }
        }
    }
}
