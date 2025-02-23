using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player1"))
        {
            FindObjectOfType<PlayerMovement>().hit("player1");
            FindObjectOfType<manager>().Takedamage("player1");
            
        }


        else if (collision.gameObject.CompareTag("player2"))
        { 
            FindObjectOfType<PlayerMovement>().hit("player2");
            FindObjectOfType<manager>().Takedamage("player2");
           
        }
    }
}
