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
    private PlayerData playerData; //Access to load game 
    private void Awake()
    {
        base.Awake();
        playerInstance = _player;
        DontDestroyOnLoad(gameObject);
        playerData = FindObjectOfType<PlayerData>();
    }
    
    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
        LoadPlayerData();
        
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("FirstLevel");
        UpdateGameState(GameState.Playing);
    }
    public void LoadPlayerData()
    {
        if (playerData != null)
        {
            GameData loadedData = SaveManager.Instance.LoadGame();
            if (loadedData != null)
            {
                playerData.SetPlayerData(loadedData);
            }
        }
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        OnGameStateChanged?.Invoke(newState);
        
        if (playerInstance != null)
        {
            playerInstance.SetActive(newState == GameState.Playing);
        }

        switch(newState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                Cursor.visible = true; 
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Win:
                SceneManager.LoadScene("WinScene");
                SoundManager.Play("");
                Cursor.visible = true; 
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Lose:
                SceneManager.LoadScene("GameOverScene");
                SoundManager.Play("GameOver");
                Cursor.visible = true; 
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Credits:
                SceneManager.LoadScene("CreditsScene");
                Cursor.visible = true; 
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }


    //Events that triggers the state of the game 
    public void LoadMenu() => UpdateGameState(GameState.MainMenu);
    public void WinGame() => UpdateGameState(GameState.Win);
    public void LoseGame() => UpdateGameState(GameState.Lose);
    public void CreditsGame() => UpdateGameState(GameState.Credits);
    
    public void ExitGame() => Application.Quit();
}