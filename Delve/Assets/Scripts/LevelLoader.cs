using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        StartCoroutine(Load(2));
    }

    IEnumerator Load(int level)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }

    public void LoadIntro()
    {
        StartCoroutine(Load(1));
    }

    public void LoadMain()
    {
        StartCoroutine(Load(0));
    }

    public void LoadControls()
    {
        StartCoroutine(Load(3));
    }
}
