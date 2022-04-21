using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game States")]
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    public bool GameIsPaused = false;

    [Header("UI")]
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] TextMeshProUGUI cogCount;
    public int currentCogCount;
    public int levelCogCount;

    #region singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if(Instance == null)_instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Cutscene:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        MainMenu,
        Cutscene,
        Playing,
        Paused,
        GameOver
    }

    #region UI

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.Save();
    }

    public void AddToCollection(int value)
    {
        currentCogCount += value;
        cogCount.text = (currentCogCount + "/" + levelCogCount);
    }

    public void CountCogsInLevel()
    {
        levelCogCount++;
    }
    #endregion
}
