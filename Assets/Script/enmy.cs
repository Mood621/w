using Unity.VisualScripting;
using UnityEngine;

public class ChaseAndJumpEnemy : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed;      // �ƶ��ٶ�
    public float jumpForce;      // ��Ծ����
    public float detectionDistance = 1f; // ǽ�ڼ�����

    [Header("�������")]
    public Transform player;         // ���ǵ�Transform
    public LayerMask wallLayer;      // ǽ�����ڵĲ㼶
    private Animator animator;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private float raycastOffset = 0.2f; // ���ߴ�ֱƫ����


    public  bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!player) return;

        // ׷���߼�
        ChasePlayer();

        // ǽ�ڼ��
        CheckWalls();
       animator.SetBool("isGround",isGround);
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("player1"))
        {
            FindObjectOfType<manager>().Takedamage("player1");
            FindObjectOfType<PlayerMovement>().hit("player1");
        }
       else if(collision.gameObject.CompareTag("player2"))
        {
            FindObjectOfType<manager>().Takedamage("player2");
            FindObjectOfType<PlayerMovement>().hit("player2");
        }
    }
    void ChasePlayer()
    {
        // �����ƶ�����
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // ���Ƴ���
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }

        // ˮƽ�ƶ�
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void CheckWalls()
    {
        // ����������㣨�ײ��Ͷ��������㣩
        Vector2 rayOriginBottom = new Vector2(transform.position.x, transform.position.y - raycastOffset);
        Vector2 rayOriginTop = new Vector2(transform.position.x, transform.position.y + raycastOffset);

        // ���߷��򣨸��ݳ���
        Vector2 rayDirection = isFacingRight ? Vector2.right : Vector2.left;

        // ��������
        RaycastHit2D hitBottom = Physics2D.Raycast(rayOriginBottom, rayDirection, detectionDistance, wallLayer);
        RaycastHit2D hitTop = Physics2D.Raycast(rayOriginTop, rayDirection, detectionDistance, wallLayer);

        // �����⵽ǽ�����ڵ���ʱ��Ծ
        if ((hitBottom.collider != null || hitTop.collider != null) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isGround = true;
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGround = false; 

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ���ӻ�������ߣ������ã�
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 rayOriginBottom = new Vector2(transform.position.x, transform.position.y - raycastOffset);
        Vector2 rayOriginTop = new Vector2(transform.position.x, transform.position.y + raycastOffset);
        Gizmos.DrawLine(rayOriginBottom, rayOriginBottom + direction * detectionDistance);
        Gizmos.DrawLine(rayOriginTop, rayOriginTop + direction * detectionDistance);
    }
}