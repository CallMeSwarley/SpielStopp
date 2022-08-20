// adapted from https://www.youtube.com/watch?v=76WOa6IU_s8&t=71s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLvl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(firstLvl);
    }
   
    public void openOptions()// here you can select the game mode
    {

    }

    public void closeOptions()
    {

    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
