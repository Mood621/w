using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float moveSpeed = 3.0f;
    public float jumpForce = 7.0f; // ͳһ����Ծ����

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;

    private bool isRight1;
    private bool isRight2;

    private bool isGround1;
    private bool isGround2;

    private BoxCollider2D Myfeet1;
    private BoxCollider2D Myfeet2;
    private int maxJumpCount = 2; // �����Ծ����
    private int jumpCount1;
    private int jumpCount2;
    private Animator animator1;
    private Animator animator2;

    void Start()
    {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
        Myfeet1 = player1.GetComponent<BoxCollider2D>();
        Myfeet2 = player2.GetComponent<BoxCollider2D>();
        jumpCount1 = maxJumpCount;
        jumpCount2 = maxJumpCount;
        animator1=player1.GetComponent<Animator>();  
        animator2=player2.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        CheckGround();
        Jump();
       
    }
    public void hit (string name)
    {
        if (name == "player1")
            animator1.SetTrigger("ishit");
        else
            animator2.SetTrigger("ishit");
    }
    void CheckGround()
    {
        bool wasGrounded1 = isGround1;
        bool wasGrounded2 = isGround2;

        isGround1 = Myfeet1.IsTouchingLayers(LayerMask.GetMask("Ground"));
        isGround2 = Myfeet2.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // ���Ӵ�����ʱ������Ծ����
        if (isGround1 && !wasGrounded1)
        {
            jumpCount1 = maxJumpCount;
        }
        if (isGround2 && !wasGrounded2)
        {
            jumpCount2 = maxJumpCount;
        }
    }

    void Jump()
    {
        // ���1��Ծ
        if (Input.GetButtonDown("Jump_1") && jumpCount1 > 0)
        {
            rb1.velocity = new Vector2(rb1.velocity.x, jumpForce);
            jumpCount1--;
            animator1.SetTrigger("jump1");
            if (jumpCount1 == 0)
             animator1.SetTrigger("jump2");

            // ������Ծʱ����ˮƽ�ٶ�
            if (!isGround1 && jumpCount1 == maxJumpCount - 1)
            {
                rb1.velocity = new Vector2(rb1.velocity.x, jumpForce);
            }
        }

        // ���2��Ծ
        if (Input.GetButtonDown("Jump_2") && jumpCount2 > 0)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, jumpForce);
            jumpCount2--;
            animator2.SetTrigger("jump1");
            if (jumpCount2 == 0)
                animator2.SetTrigger("jump2");


            // ������Ծʱ����ˮƽ�ٶ�
            if (!isGround2 && jumpCount2 == maxJumpCount - 1)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, jumpForce);
            }
        }
    }

    private void Move()
    {
        // ���1�ƶ�
        float moveX1 = Input.GetAxis("Horizontal_1");
        // ʹ��velocity����ˮƽ�ƶ�������ԭ�д�ֱ�ٶ�
        rb1.velocity = new Vector2(moveX1 * moveSpeed, rb1.velocity.y);

        // ����ת�ж�
        if ((moveX1 < 0 && !isRight1) || (moveX1 > 0 && isRight1))
        {
            Flip1();
        }

        // ���2�ƶ�
        float moveX2 = Input.GetAxis("Horizontal_2");
        rb2.velocity = new Vector2(moveX2 * moveSpeed, rb2.velocity.y);

        if ((moveX2 < 0 && !isRight2) || (moveX2 > 0 && isRight2))
        {
            Flip2();
        }

        // ���¶���������ʹ��ʵ���ƶ��ٶȣ�
        animator1.SetFloat("speed", Mathf.Abs(moveX1));
        animator2.SetFloat("speed",Mathf.Abs(moveX2));  

    }

    private void Flip1()
    {
        isRight1 = !isRight1;
        Vector3 PlayerScale = player1.localScale;
        PlayerScale.x *= -1;
        player1.localScale = PlayerScale;
    }

    private void Flip2()
    {
        isRight2 = !isRight2;
        Vector3 PlayerScale = player2.localScale;
        PlayerScale.x *= -1;
        player2.localScale = PlayerScale;
    }

}