using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Win,
    Lose,
    Credits
}

public class GameManager : Singleton<GameManager>
{
    public static GameObject playerInstance;
    [SerializeField] private GameObject _player; 
    public GameState State { get; private set; }
    
    public event Action<GameState> OnGameStateChanged; //Is in charge of notify changes of the state 

    private void Awake()
    {
        base.Awake();
        playerInstance = _player;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }
    
    
    public void UpdateGameState(GameState newState) //Is in charge of changing the state 
    {
        State = newState;
        OnGameStateChanged?.Invoke(newState);

        switch(newState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
            case GameState.Playing:
                SceneManager.LoadScene("FirstLevel");
                break;
            case GameState.Lose:
                Debug.Log("Game Over");
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }


    //Events that triggers the state of the game 
    public void StartGame() => UpdateGameState(GameState.Playing);
    public void LoadMenu() => UpdateGameState(GameState.MainMenu);
    public void WinGame() => UpdateGameState(GameState.Win);
    public void LoseGame() => UpdateGameState(GameState.Lose);
    public void CreditsGame() => UpdateGameState(GameState.Credits);
    
    public void ExitGame() => Application.Quit();
}