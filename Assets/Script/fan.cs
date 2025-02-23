using UnityEngine;

public class Fan : MonoBehaviour
{
    public float maxHeight;
    public float speed;
    public float positionTolerance = 0.01f; // �ݲ���ֵ

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
            // �������ʼλ�õĴ�ֱ����
            float distanceToInitial = initialPosition.y - rb.position.y;

            if (distanceToInitial > positionTolerance)
            {
                // δ�����ʼλ�ã������½�
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
            {
                // �����ʼλ�ã�ֹͣ������״̬
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
            isDown = false; // ȷ������ʱ�ر��½�״̬
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player1") || collision.gameObject.CompareTag("player2"))
        {
            isUp = false;
            isDown = true; // �����½�
        }
    }
}