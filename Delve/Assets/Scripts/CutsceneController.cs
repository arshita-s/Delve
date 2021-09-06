using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public TMP_Text message;
    public bool ready = false;
    public TextAsset inkfile;
    private static Story story;
    private FadeText fade;
    public TextAsset[] cutscenes;
    private bool sceneComplete = false;
    private LevelLoader level;
    private int v;

    // Start is called before the first frame update
    void Start()
    {
        v = 0;
        fade = GetComponent<FadeText>();
        message.text = "";
        inkfile = cutscenes[v];
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && story != null)
        {
            ready = false;
            StartCoroutine(AdvanceDialogue());
        }
        
        if (ready && story == null)
        {
            story = new Story(inkfile.text);
            ready = false;
            StartCoroutine(AdvanceDialogue());
        }

        if (sceneComplete)
        {
            if (cutscenes.Length > 1)
            {
                cutscenes = cutscenes.Skip(1).ToArray();
                v += 1;
                inkfile = cutscenes[v];
            }
            sceneComplete = false;
            message.text = "";
            level.LoadGame();
        }
    }
    
    IEnumerator AdvanceDialogue()
    {
        ChangeColor("white");
        string sentence = story.Continue();
        ParseTags();
        message.text = sentence;
        StartCoroutine(fade.FadeTextToFullAlpha(2f, message));
        yield return new WaitForSeconds(5f);
        StartCoroutine(fade.FadeTextToZeroAlpha(2f, message));
        yield return new WaitForSeconds(3f);
        if (story.canContinue)
        {
            ready = true;
        }
        else
        {
            sceneComplete = true;
        }
    }

    private IEnumerator Pause(int p)
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
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
                case "color":
                    ChangeColor(param);
                    break;
            }
        }
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
