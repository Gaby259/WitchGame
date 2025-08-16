using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        //Show the panels for each state 
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(state == GameState.MainMenu);
        }

        if (winPanel != null)
        {
            winPanel.SetActive(state == GameState.Win);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(state == GameState.Lose);
        }
        
    }
    
    public void PlayButton() => GameManager.Instance.StartGame();
    public void MainMenuButton() => GameManager.Instance.LoadMenu();
    public void ExitButton() => GameManager.Instance.ExitGame();
}