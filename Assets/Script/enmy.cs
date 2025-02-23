using Unity.VisualScripting;
using UnityEngine;

public class ChaseAndJumpEnemy : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed;      // 移动速度
    public float jumpForce;      // 跳跃力度
    public float detectionDistance = 1f; // 墙壁检测距离

    [Header("组件引用")]
    public Transform player;         // 主角的Transform
    public LayerMask wallLayer;      // 墙壁所在的层级
    private Animator animator;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private float raycastOffset = 0.2f; // 射线垂直偏移量


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

        // 追击逻辑
        ChasePlayer();

        // 墙壁检测
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
        // 计算移动方向
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // 控制朝向
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }

        // 水平移动
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void CheckWalls()
    {
        // 计算射线起点（底部和顶部两个点）
        Vector2 rayOriginBottom = new Vector2(transform.position.x, transform.position.y - raycastOffset);
        Vector2 rayOriginTop = new Vector2(transform.position.x, transform.position.y + raycastOffset);

        // 射线方向（根据朝向）
        Vector2 rayDirection = isFacingRight ? Vector2.right : Vector2.left;

        // 发射射线
        RaycastHit2D hitBottom = Physics2D.Raycast(rayOriginBottom, rayDirection, detectionDistance, wallLayer);
        RaycastHit2D hitTop = Physics2D.Raycast(rayOriginTop, rayDirection, detectionDistance, wallLayer);

        // 如果检测到墙壁且在地面时跳跃
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

    // 可视化检测射线（调试用）
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