using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameObject playerInstance;
    public int Score { get; private set; }
    
    [SerializeField] private GameObject _player;
    private IState _currentState;
    
    
    //1. Will call GameOver when player is killed 
    
    //2. Should pause game when "ESC" is pressed
    
    //3. Should keep track of score for the player
    private void Start()
    {
        ChangeState(new PlayingState());
    }
    public override void Awake()
    {
        base.Awake();
        playerInstance = _player;
    }
    
    private void Update()
    {
        _currentState?.Update();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState is PauseState)
                ChangeState(new PlayingState());
            else
                ChangeState(new PauseState());
        }
    }
    
    public void ChangeState(IState newState)
    {
        _currentState?.Exit(); // Sale del estado actual si existe
        _currentState = newState;
        _currentState.Enter();
    }
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

