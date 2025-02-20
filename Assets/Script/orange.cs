using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orange : MonoBehaviour
{
    private int orange_num = 0;
    [SerializeField] private Text OText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("orange"))
        {
            Destroy(collision.gameObject);
            orange_num++;
            //Debug.Log("éÙ×Ó£º"+orange_num);
            OText.text = ":" + orange_num;


        }
    }
}
