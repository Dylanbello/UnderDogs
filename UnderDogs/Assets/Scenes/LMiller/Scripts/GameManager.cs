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
    [SerializeField] GameObject tutorialMenuUI;
    [SerializeField] GameObject cogUIHints;
    [SerializeField] GameObject bridgeUIHints;
    [SerializeField] GameObject elevatorUIHints;
    [SerializeField] GameObject robitsUIHints;
    [SerializeField] GameObject tutorialOverview;
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

    public void HintsMenu()
    {
        pauseMenuUI.SetActive(false);
        tutorialOverview.SetActive(true);
        tutorialMenuUI.SetActive(true);

    }

    // The Next Button Script Will Look like a mess but I promise you it's not <3
    public void HintsNext()
    {
        tutorialMenuUI.SetActive(false);
        cogUIHints.SetActive(true);

    }
    public void HintsNext1()
    {
        cogUIHints.SetActive(false);
        bridgeUIHints.SetActive(true);

    }
    public void HintsNext2()
    {
        bridgeUIHints.SetActive(false);
        elevatorUIHints.SetActive(true);

    }

    public void HintsNext3()
    {
        bridgeUIHints.SetActive(false);
        robitsUIHints.SetActive(true);

    }

    public void Return()
    {
        robitsUIHints.SetActive(false);
        pauseMenuUI.SetActive(true);
        tutorialOverview.SetActive(false);


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
