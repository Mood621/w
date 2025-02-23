using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "player")
        {
           //Debug.Log("¼ñµ½éÙ×Ó");
            gameObject.SetActive(false);
        }
    }
}
