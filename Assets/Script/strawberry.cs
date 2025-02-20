using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class strawberry : MonoBehaviour
{
    private int strawberry_num = 0;
    [SerializeField] private Text SText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("strawberry"))
        {
            Destroy(collision.gameObject);
            strawberry_num++;
            //Debug.Log("²ÝÝ®£º"+ strawberry_num);
            SText.text = ":" + strawberry_num;
        }
    }
}
