using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float numHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;

    public void updateHearts(PlayerController player)
    {
        numHearts = player.getHealth();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numHearts)
            {
                hearts[i].enabled = true;
                if ((i + 1) % 2 == 0)
                {
                    hearts[i - 1].enabled = false;
                }
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
}
