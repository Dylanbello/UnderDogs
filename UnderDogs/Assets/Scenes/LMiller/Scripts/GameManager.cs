using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int collectablesCounter = 0;

    private void Awake()
    {
        if(Instance == null)_instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

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

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void AddToCollection()
    {
        collectablesCounter++;
        PlayerPrefs.SetInt("Collectables", collectablesCounter);
    }
}
