using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameObject playerInstance;
    public int Score { get; private set; }
    
    [SerializeField] private GameObject _player;
    
    //1. Will call GameOver when player is killed 
    
    //2. Should pause game when "ESC" is pressed
    
    //3. Should keep track of score for the player
    
    //

    public void PlayerWin()
    {
        Debug.Log("Player Win");
        SceneManager.LoadScene("WinScreen");
    }

    public void PlayerLose()
    {
        Debug.Log("Player Lost");
        SceneManager.LoadScene("LoseScreen");
    }
    
}
