using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Image demo;
    public TMP_Text demotxt;
    public Button demobutton;
    public TMP_Text buttontxt;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = GetComponent<Health>();
        demo.enabled = false;
        demotxt.enabled = false;
        demobutton.enabled = false;
        buttontxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getHealth() == 0)
        {
            gameOver = true;
        }
        
        healthBar.updateHearts(player);

        
    }

    public void Dialogue(TextAsset file)
    {
        player.Freeze();
        dialogueManager.LoadInkFile(file);
        dialogueManager.StartDialogue();
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    public void DemoFinished()
    {
        demo.enabled = true;
        demotxt.enabled = true;
        demobutton.enabled = true;
        buttontxt.enabled = true;
        player.Freeze();
    }
}
