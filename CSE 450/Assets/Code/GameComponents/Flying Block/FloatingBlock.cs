using System.Collections;
using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isActivated = false;

    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        startPosition = transform.position;
    }

    // 激活并移动方块
    public void Move(Vector3 direction, float distance)
    {
        if (isMoving)
        {
            StopAllCoroutines();  // 中止之前的协程
        }

        targetPosition = startPosition + direction.normalized * distance;
        isActivated = true;
        StartCoroutine(MoveToPosition(targetPosition));  // 启动新的协程
    }

    // 协程实现缓慢移动到目标位置
    private IEnumerator MoveToPosition(Vector3 destination)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }

    void Update()
    {
        // 如果方块没有被激活，返回起始位置
        if (!isActivated && !isMoving)
        {
            StartCoroutine(MoveToPosition(startPosition));
        }
    }

    // 停止激活，返回初始状态
    public void Deactivate()
    {
        if (isMoving)
        {
            StopAllCoroutines();  // 中止之前的协程
        }

        isActivated = false;
        StartCoroutine(MoveToPosition(startPosition));  // 返回到起始位置
    }
}
