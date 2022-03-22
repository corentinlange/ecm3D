using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public CanvasGroup pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            { 
                Resume(); 
            }
            else
            {
                Pause();
            };
        }
    }

    public void Resume()
    {   
        pauseMenuUI.alpha = 0;
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Pause()
    {
        pauseMenuUI.alpha = 1;
        GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void HelpMenu()
    {
        Debug.Log("Help menu...");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game...");
        Application.Quit();
    }
}
