using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static event Action<bool> OnPauseChanged; 

    [SerializeField] private GameObject pauseMenuPanel;
    private bool isPaused = false;

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
        OnPauseChanged?.Invoke(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        OnPauseChanged?.Invoke(false);
    }
}