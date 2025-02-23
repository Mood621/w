using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public string SceneName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision .gameObject.GetComponent<SpriteRenderer>().sortingLayerName=="player")
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Debug.Log("玩家与物体发生碰撞！");
        }
    }
}
