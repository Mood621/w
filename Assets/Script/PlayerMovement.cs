using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;

    private bool isRight1;
    private bool isRight2;
    //private bool facingRight;

    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        rb2 = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveX1 = Input.GetAxis("Horizontal_1");
        float moveZ1 = Input.GetAxis("Vertical_1");
        Vector3 moveDirection1 = new Vector3(moveX1, 0, moveZ1).normalized;
        player1.position += moveDirection1 * moveSpeed * Time.deltaTime;
        if (isRight1 == false && moveX1 < 0)
        {
            Flip1();
        }
        else if (isRight1 == true && moveX1 > 0)
        {
            Flip1();
        }


        float moveX2 = Input.GetAxis("Horizontal_2");
        float moveZ2 = Input.GetAxis("Vertical_2");
        Vector3 moveDirection2 = new Vector3(moveX2, 0, moveZ2).normalized;
        player2.position += moveDirection2 * moveSpeed * Time.deltaTime;
        if (isRight2 == false && moveX2 < 0)
        {
            Flip2();
        }
        else if (isRight2 == true && moveX2 > 0)
        {
            Flip2();
        }

    }

    private void Flip1()
    {
        isRight1 = !isRight1;
        Vector3 PlayerScale = player1.localScale;
        PlayerScale.x *= -1;
        player1.localScale = PlayerScale;
        Debug.Log("1");

    }
    private void Flip2()
    {
        isRight2 = !isRight2;
        Vector3 PlayerScale = player2.localScale;
        PlayerScale.x *= -1;
        player2.localScale = PlayerScale;
        Debug.Log("2");
    }

   
}
