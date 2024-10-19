using UnityEngine;

public class MirrorCube : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation (if needed for future rotation control)
    public GameObject player;
    public float maxDistance = 5f;
    public float mirrorMoveSpeed = 2f;  // Speed at which the mirror moves

    private Rigidbody2D playerRb;
    private bool isPlayerColliding = false;  // 用于跟踪玩家是否与镜子碰撞
    private Vector2 moveDirection;  // 用于记录镜子的移动方向

    void Start()
    {
        // 获取玩家的 Rigidbody2D 组件以访问速度
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 如果玩家正在与镜子碰撞，镜子将持续移动
        if (isPlayerColliding)
        {
            MoveMirror(moveDirection);
        }
    }

    // 当玩家碰撞时，触发持续移动
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Vector2 playerVelocity = playerRb.velocity;  // 获取玩家的速度

            // 根据玩家的速度确定移动方向
            if (playerVelocity.x > 0)  // 玩家向右移动
            {
                moveDirection = Vector2.right;  // 镜子向右移动
            }
            else if (playerVelocity.x < 0)  // 玩家向左移动
            {
                moveDirection = Vector2.left;  // 镜子向左移动
            }

            isPlayerColliding = true;  // 标记玩家正在与镜子碰撞
        }
    }

    // 当玩家离开碰撞区域时停止移动
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerColliding = false;  // 玩家离开，停止镜子的移动
        }
    }

    // 持续移动镜子
    private void MoveMirror(Vector2 direction)
    {
        // 使用世界坐标系，确保移动始终沿水平轴（X轴）
        transform.Translate(direction * mirrorMoveSpeed * Time.deltaTime, Space.World);
    }

    // Method to rotate the mirror if needed
    private void RotateMirror(float speed)
    {
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
