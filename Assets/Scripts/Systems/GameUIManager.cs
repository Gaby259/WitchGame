using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject creditsPanel;
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
        
        mainMenuPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        creditsPanel.SetActive(false);

        switch (state)
        {
            case GameState.MainMenu:
                mainMenuPanel.SetActive(true);
                break;
            case GameState.Win:
                winPanel.SetActive(true);
                break;
            case GameState.Lose:
                losePanel.SetActive(true);
                break;
            case GameState.Credits:
                creditsPanel.SetActive(true);
                break;
        }
    }
    
    public void PlayButton() => GameManager.Instance.StartGame();
    public void MainMenuButton() => GameManager.Instance.LoadMenu();
    public void ExitButton() => GameManager.Instance.ExitGame();
    public void CreditsButton() => GameManager.Instance.CreditsGame();
}