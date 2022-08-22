// adapted from https://www.youtube.com/watch?v=76WOa6IU_s8&t=71s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLvl;
    public Canvas tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (tutorialCanvas != null)
        {
            tutorialCanvas.gameObject.SetActive(false);
        } 
    }


    public void startGame()
    {
        SceneManager.LoadScene(firstLvl);
    }
   
    public void openTutorial()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }

    public void closeTutorial()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
