using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image textBox;
    public TextAsset inkFile;
    private static Story story;
    private TMP_Text name;
    private TMP_Text message;
    private PlayerController player;
    public bool dialogueFinished = false;
    public bool ready = false;
    public TMP_Text X;
    public bool triggerFadeOut = false;
    public bool triggerFadeIn = false;
    public TextAsset intro;
    private GameManager gm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        name = textBox.transform.GetChild(0).GetComponent<TMP_Text>();
        message = textBox.transform.GetChild(1).GetComponent<TMP_Text>();
        textBox.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        X.enabled = false;
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            story = new Story(inkFile.text); 
            GetName();
            ready = false;
            textBox.enabled = true;
            AdvanceDialogue();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (story.canContinue)
            {
                AdvanceDialogue();
            }
            else
            {
                FinishDialogue();
            }
        }

        if (inkFile == intro)
        {
            gm.liftCalBarrier(); 
        }
    }

    void AdvanceDialogue()
    {
        X.enabled = false;
        string sentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void FinishDialogue()
    {
        Debug.Log("End of dialogue.");
        message.text = "";
        name.text = "";
        dialogueFinished = true;
        player.Unfreeze();
        X.enabled = false;
        textBox.enabled = false;
        ChangeColor("white");
        
        if (triggerFadeOut)
        {
            StartCoroutine(gm.fadeOut(false));
            triggerFadeOut = false;
        }
    }

    IEnumerator TypeSentence(string s)
    {
        message.text = "";
        char[] sentence = s.ToCharArray();
        for (int i = 0; i < sentence.Length;i++)
        {
            char l = sentence[i];
            message.text += l;
            if (i == sentence.Length - 1)
            {
                X.enabled = true;
            }
            yield return null;
        }
    }

    void GetName()
    {
        List<string> tags = story.globalTags;
        foreach (string s in tags)
        {
            string prefix = s.Split(' ')[0];
            string param = s.Split(' ')[1];
            switch (prefix)
            {
                case "name":
                    name.text = param;
                    break;
            }
        }
    }

    void ParseTags()
    {
        List<string> tags = story.currentTags;
        foreach (string s in tags)
        {
            string prefix = s.Split(' ')[0];
            string param = s.Split(' ')[1];
            switch (prefix)
            {
                case "anim":
                    if (param == "flip")
                    {
                        player.Flip();
                    }
                    else if (param == "flipr")
                    {
                        player.FlipRight();
                    }
                    else if (param == "flipl")
                    {
                        player.FlipLeft();
                    }
                    break;
                case "color":
                    ChangeColor(param);
                    break;
                case "fade":
                    if (param == "out")
                    {
                        triggerFadeOut = true;
                    }
                    else if (param == "in")
                    {
                        triggerFadeIn = true;
                    }
                    break;
            }
        }
    }

    public void LoadInkFile(TextAsset file)
    {
        inkFile = file;
    }

    public void StartDialogue()
    {
        Debug.Log("start dialogue");
        ready = true;
        name.enabled = true;
        message.enabled = true;
    }

    void ChangeColor(string color)
    {
        switch(color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            case "yellow":
                message.color = Color.yellow;
                break;
            default:
                Debug.Log($"{color} is not available as a text color");
                break;
        }
    }
}
