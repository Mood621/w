using UnityEngine;

public class Fan : MonoBehaviour
{
    public float maxHeight;
    public float speed;
    public float positionTolerance = 0.01f; // 容差阈值

    private Vector2 initialPosition;
    private bool isUp;
    private bool isDown;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            float currentHeight = rb.position.y - initialPosition.y;
            if (currentHeight < maxHeight)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isUp = false;
            }
        }
        else if (isDown)
        {
            // 计算与初始位置的垂直距离
            float distanceToInitial = initialPosition.y - rb.position.y;

            if (distanceToInitial > positionTolerance)
            {
                // 未到达初始位置，继续下降
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
            {
                // 到达初始位置，停止并重置状态
                rb.velocity = Vector2.zero;
                isDown = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player1") || collision.gameObject.CompareTag("player2"))
        {
            isUp = true;
            isDown = false; // 确保上升时关闭下降状态
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player1") || collision.gameObject.CompareTag("player2"))
        {
            isUp = false;
            isDown = true; // 触发下降
        }
    }
}