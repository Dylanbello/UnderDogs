using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game States")]
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    public bool GameIsPaused = false;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

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
    [SerializeField] GameObject settingsMenu;

    public int currentCogCount;
    public int levelCogCount;

    public AudioMixer audioMixer;

  
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
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();


        List<string> options = new List<string>();


        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)

            {
                currentResolutionIndex = i;
            }
          
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

        }
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
        tutorialMenuUI.SetActive(false);
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

    public void SettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(true);

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
        settingsMenu.SetActive(false);


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

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    #endregion
}
