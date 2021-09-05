using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas userInterface;
    private bool gameOver = false;
    private bool death = false;
    private float score = 0;
    private PlayerController player;
    private Health healthBar;
    public DialogueManager dialogueManager;
    public GameObject blackScreen;
    public GameObject calBarrier;
    private bool liftBarrier = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getHealth() == 0)
        {
            gameOver = true;
        }
        
        healthBar.updateHearts(player);

        if (liftBarrier)
        {
            Destroy(calBarrier);
        }
    }

    public void Dialogue(TextAsset file)
    {
        player.Freeze();
        dialogueManager.LoadInkFile(file);
        dialogueManager.StartDialogue();
    }

    public IEnumerator fadeOut(bool fadeToBlack = true, int speed = 5)
    {
        Color obj = blackScreen.GetComponent<Image>().color;
        float fadeAmt;

        if (fadeToBlack)
        {
            while (blackScreen.GetComponent<Image>().color.a < 1)
            {
                fadeAmt = obj.a + (speed * Time.deltaTime);
                obj = new Color(obj.r, obj.g, obj.b, fadeAmt);
                blackScreen.GetComponent<Image>().color = obj;
                yield return null;
            }
        }
        else
        {
            while (blackScreen.GetComponent<Image>().color.a > 0)
            {
                fadeAmt = obj.a - (speed * Time.deltaTime);
                obj = new Color(obj.r, obj.g, obj.b, fadeAmt);
                blackScreen.GetComponent<Image>().color = obj;
                yield return null;
            }
        }
    }

    public void liftCalBarrier()
    {
        liftBarrier = true;
    }
}
