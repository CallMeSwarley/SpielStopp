//adaptet from https://www.youtube.com/watch?v=JivuXdrIHK0
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string menu;
    public static bool isPaused = false;
    public GameObject PauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        isPaused = true;
        PauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0.0001f;//freezes time in game       
    }

    public void resumeGame()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void gotoMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(menu);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
