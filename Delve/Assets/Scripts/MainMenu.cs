using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private LevelLoader levelLoader;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        levelLoader.LoadIntro();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        levelLoader.LoadMain();
    }

    public void ToControls()
    {
        levelLoader.LoadControls();
    }
}
