using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel; 

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
        bool shouldShow = false;
        if (panel == null)
        {
            return;
        }

        switch (state)
        {
            case GameState.Win:
                shouldShow = true;
                break;
            case GameState.Lose:
                shouldShow = true;
                break;
            case GameState.Credits:
                shouldShow = true;
                break;
            default:
                shouldShow = false;
                break;
        }

        panel.SetActive(shouldShow);
    }
    
    public void PlayButton() => GameManager.Instance.StartLevel();
    public void MainMenuButton() => GameManager.Instance.LoadMenu();
    public void ExitButton() => GameManager.Instance.ExitGame();
    public void CreditsButton() => GameManager.Instance.CreditsGame();
}