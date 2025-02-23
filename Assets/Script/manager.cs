using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int life1;
    public int life2;
    public  Image first1,second1,third1;
    public Image first2,second2,third2;
    public GameObject gameoverpanel;
    public bool isPlaying;

    private void Start()
    {
        isPlaying = true;
        gameoverpanel.SetActive(false);
    }
    private void Update()
    {
        gameover();
        if (isPlaying == false)
            return;
    }
    public void Takedamage(string name)
    {
        if (name == "player1")
        {
            life1--;
            switch (life1)
            {

                case 0:
                    first1.enabled = false;
                    second1.enabled = false;
                    third1.enabled = false;
                    break;
                case 1:
                    first1.enabled = false;
                    second1.enabled = false;
                    third1.enabled = true;
                    break;
                case 2:
                    first1.enabled = false;
                    second1.enabled = true;
                    third1.enabled = true;
                    break;
                case 3:
                    first1.enabled = true;
                    second1.enabled = true;
                    third1.enabled = true;
                    break;

            }
        }
        else
        {
            life2--;
            switch (life2)
            {

                case 0:
                    first2.enabled = false;
                    second2.enabled = false;
                    third2.enabled = false;
                   
                    break;
                case 1:
                    first2.enabled = false;
                    second2.enabled = false;
                    third2.enabled = true;
                    break;
                case 2:
                    first2.enabled = false;
                    second2.enabled = true;
                    third2.enabled = true;
                    break;
                case 3:
                    first2.enabled = true;
                    second2.enabled = true;
                    third2.enabled = true;
                    break;

            }

        }
    } 
    public void gameover()
    {
        if (life1 == 0 && life2 == 0)
        { 
            gameoverpanel.SetActive(true);
            isPlaying = false;
        }
    }

}
