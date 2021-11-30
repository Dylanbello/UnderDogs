using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GameUI;
    public GameObject OptionsMenuUI;
    public GameObject CardSlotsUI;

    void Update ()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                GameUI.SetActive(true);
                OptionsMenuUI.SetActive(false);
                

            } else
            {
                Pause();
                GameUI.SetActive(false);
                CardSlotsUI.SetActive(false);
            }

        }
    }

    public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameUI.SetActive(true);
    }

    void Pause ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitGame()
    {
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
