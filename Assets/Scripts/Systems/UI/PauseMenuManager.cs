using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static event Action<bool> OnPauseChanged; 

    [SerializeField] private GameObject pauseMenuPanel;
    private bool isPaused = false;
    private PlayerData playerData;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        pauseMenuPanel.SetActive(false);//Pause menu will no longer appear at the start of the game
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
        OnPauseChanged?.Invoke(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        pauseMenuPanel.SetActive(false);
        OnPauseChanged?.Invoke(false);
    }

    public void OnSaveButtonPressed()
    {
        if (playerData != null)
        {
            playerData.SavePlayerData();
            Debug.Log("Juego guardado manualmente");
        }
    }

    public void MainMenuButton() => GameManager.Instance.LoadMenu();
    public void ExitButton() => GameManager.Instance.ExitGame();
}