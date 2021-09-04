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
    }

    public void Dialogue(TextAsset file)
    {
        player.Freeze();
        dialogueManager.loadInkFile(file);
        dialogueManager.startDialogue();
    }
}
